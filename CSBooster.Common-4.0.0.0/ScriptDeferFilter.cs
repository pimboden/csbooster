// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.IO;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for ScriptDeferFilter
/// </summary>
namespace _4screen.CSB.Common
{
    /// <summary>
    /// Summary description for ScriptDeferFilter
    /// </summary>
    public class ScriptDeferFilter : Stream
    {
        private Stream responseStream;
        private long position;

        /// <summary>
        /// When this is true, script blocks are suppressed and captured for 
        /// later rendering
        /// </summary>
        private bool captureScripts;

        /// <summary>
        /// Holds all script blocks that are injected by the controls
        /// The script blocks will be moved after the form tag renders
        /// </summary>
        private StringBuilder scriptBlocks;

        private Encoding encoding;

        /// <summary>
        /// Holds characters from last Write(...) call where the start tag did not
        /// end and thus the remaining characters need to be preserved in a buffer so 
        /// that a complete tag can be parsed
        /// </summary>
        private char[] pendingBuffer = null;

        /// <summary>
        /// When this is true, it means the last script tag tag started from a Write(...) call
        /// was marked as pinned, which means it must not be moved and must be rendered
        /// exactly where it is.
        /// </summary>
        private bool lastScriptTagIsPinned = false;

        public ScriptDeferFilter(HttpResponse response)
        {
            encoding = response.Output.Encoding;
            responseStream = response.Filter;

            scriptBlocks = new StringBuilder(5000);
            // When this is on, script blocks are captured and not written to output
            captureScripts = true;
        }

        #region Filter overrides

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Close()
        {
            FlushPendingBuffer();
            responseStream.Close();
        }

        private void FlushPendingBuffer()
        {
            /// Some characters were left in the buffer 
            if (null != pendingBuffer)
            {
                WriteOutput(pendingBuffer, 0, pendingBuffer.Length);
                pendingBuffer = null;
            }
        }

        public override void Flush()
        {
            FlushPendingBuffer();
            responseStream.Flush();
        }

        public override long Length
        {
            get { return 0; }
        }

        public override long Position
        {
            get { return position; }
            set { position = value; }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return responseStream.Seek(offset, origin);
        }

        public override void SetLength(long length)
        {
            responseStream.SetLength(length);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return responseStream.Read(buffer, offset, count);
        }

        #endregion

        public override void Write(byte[] buffer, int offset, int count)
        {
            // If we are not capturing script blocks anymore, just redirect to response stream
            if (!captureScripts)
            {
                responseStream.Write(buffer, offset, count);
                return;
            }

            /* 
			 * Script and HTML can be in one of the following combinations in the specified buffer:          
			 * .....<script ....>.....</script>.....
			 * <script ....>.....</script>.....
			 * <script ....>.....</script>
			 * <script ....>.....</script> .....
			 * ....<script ....>..... 
			 * <script ....>..... 
			 * .....</script>.....
			 * .....</script>
			 * <script>.....
			 * .... </script>
			 * ......
			 * Here, "...." means html content between and outside script tags
			*/

            char[] content;
            char[] charBuffer = encoding.GetChars(buffer, offset, count);

            /// If some bytes were left for processing during last Write call
            /// then consider those into the current buffer
            if (null != pendingBuffer)
            {
                content = new char[charBuffer.Length + pendingBuffer.Length];
                Array.Copy(pendingBuffer, 0, content, 0, pendingBuffer.Length);
                Array.Copy(charBuffer, 0, content, pendingBuffer.Length, charBuffer.Length);
                pendingBuffer = null;
            }
            else
            {
                content = charBuffer;
            }

            int scriptTagStart = 0;
            int lastScriptTagEnd = 0;
            bool scriptTagStarted = false;

            int pos;
            for (pos = 0; pos < content.Length; pos++)
            {
                // See if tag start
                char c = content[pos];
                if (c == '<')
                {
                    /*
						 Make sure there are enough characters available in the buffer to finish 
						 tag start. This will happen when a tag partially starts but does not end
						 For example, a partial script tag
						 <script
						 Or it's the ending html tag or some tag closing that ends the whole response
						 </html>
					*/
                    if (pos + "script pin".Length > content.Length)
                    {
                        // a tag started but there are less than 10 characters available. So, let's
                        // store the remaining content in a buffer and wait for another Write(...) or
                        // flush call.
                        pendingBuffer = new char[content.Length - pos];
                        Array.Copy(content, pos, pendingBuffer, 0, content.Length - pos);
                        break;
                    }

                    int tagStart = pos;

                    // Check if it's a tag ending
                    if (content[pos + 1] == '/')
                    {
                        pos += 2; // go past the </ 

                        // See if script tag is ending
                        if (isScriptTag(content, pos))
                        {
                            if (lastScriptTagIsPinned)
                            {
                                // The last script tag was pinned. So, it will not be moved
                                lastScriptTagIsPinned = false;

                                // This this tag as just another tag has just closed
                                pos++;
                            }
                            else
                            {
                                /// Script tag just ended. Two scenarios can happend:
                                /// This can be a partial buffer where the script beginning tag is not present
                                /// This can be a partial buffer of a pinned script tag
                                pos = pos + "script>".Length;

                                scriptBlocks.Append(content, scriptTagStart, pos - scriptTagStart);
                                scriptBlocks.Append(Environment.NewLine);

                                lastScriptTagEnd = pos;
                                scriptTagStarted = false;

                                pos--; // continue will increase pos by one again
                                continue;
                            }
                        }
                        else if (isBodyTag(content, pos))
                        {
                            /// body tag has just end. Time for rendering all the script
                            /// blocks we have suppressed so far and stop capturing script blocks

                            if (scriptBlocks.Length > 0)
                            {
                                // Render all pending html output till now
                                WriteOutput(content, lastScriptTagEnd, tagStart - lastScriptTagEnd);

                                // Render the script blocks
                                RenderAllScriptBlocks();

                                // Stop capturing for script blocks
                                captureScripts = false;

                                // Write from the body tag start to the end of the inut buffer and return
                                // from the function. We are done.
                                WriteOutput(content, tagStart, content.Length - tagStart);
                                return;
                            }
                        }
                        else
                        {
                            // some other tag's closing. safely skip one character as smallest
                            // html tag is one character e.g. <b>. just an optimization to save one loop
                            pos++;
                        }
                    }
                    else
                    {
                        if (isScriptTag(content, pos + 1))
                        {
                            // If the script tag is marked to be pinned, then it won't be moved.
                            // it will be considered as a regular html tag
                            lastScriptTagIsPinned = isPinned(content, pos + 1);

                            if (!lastScriptTagIsPinned)
                            {
                                /// Script tag started. Record the position as we will 
                                /// capture the whole script tag including its content
                                /// and store in an internal buffer.
                                scriptTagStart = pos;

                                // Write html content since last script tag closing upto this script tag 
                                WriteOutput(content, lastScriptTagEnd, scriptTagStart - lastScriptTagEnd);

                                // Skip the tag start to save some loops
                                pos += "<script".Length;

                                scriptTagStarted = true;
                            }
                            else
                            {
                                pos++;
                            }
                        }
                        else
                        {
                            // some other tag started
                            // safely skip 2 character because the smallest tag is one character e.g. <b>
                            // just an optimization to eliminate one loop 
                            pos++;
                        }
                    }
                }
            }

            // If a script tag is partially sent to buffer, then the remaining content
            // is part of the last script block
            if (scriptTagStarted)
            {
                scriptBlocks.Append(content, scriptTagStart, pos - scriptTagStart);
            }
            else
            {
                /// Render the characters since the last script tag ending
                WriteOutput(content, lastScriptTagEnd, pos - lastScriptTagEnd);
            }
        }

        /// <summary>
        /// Render collected scripts blocks all together
        /// </summary>
        private void RenderAllScriptBlocks()
        {
            string output = CombineScripts.CombineScriptBlocks(scriptBlocks.ToString());
            byte[] scriptBytes = encoding.GetBytes(output);
            responseStream.Write(scriptBytes, 0, scriptBytes.Length);
        }

        private void WriteOutput(char[] content, int pos, int length)
        {
            if (length == 0)
                return;

            byte[] buffer = encoding.GetBytes(content, pos, length);
            responseStream.Write(buffer, 0, buffer.Length);
        }

        private void WriteOutput(string content)
        {
            byte[] buffer = encoding.GetBytes(content);
            responseStream.Write(buffer, 0, buffer.Length);
        }

        private bool isScriptTag(char[] content, int pos)
        {
            if (pos + 5 < content.Length)
                return ((content[pos] == 's' || content[pos] == 'S') && (content[pos + 1] == 'c' || content[pos + 1] == 'C') && (content[pos + 2] == 'r' || content[pos + 2] == 'R') && (content[pos + 3] == 'i' || content[pos + 3] == 'I') && (content[pos + 4] == 'p' || content[pos + 4] == 'P') && (content[pos + 5] == 't' || content[pos + 5] == 'T'));
            else
                return false;
        }

        private bool isPinned(char[] content, int pos)
        {
            if (pos + 5 + 3 < content.Length)
                return ((content[pos + 7] == 'p' || content[pos + 7] == 'P') && (content[pos + 8] == 'i' || content[pos + 8] == 'I') && (content[pos + 9] == 'n' || content[pos + 9] == 'N'));
            else
                return false;
        }

        private bool isBodyTag(char[] content, int pos)
        {
            if (pos + 3 < content.Length)
                return ((content[pos] == 'b' || content[pos] == 'B') && (content[pos + 1] == 'o' || content[pos + 1] == 'O') && (content[pos + 2] == 'd' || content[pos + 2] == 'D') && (content[pos + 3] == 'y' || content[pos + 3] == 'Y'));
            else
                return false;
        }
    }
}