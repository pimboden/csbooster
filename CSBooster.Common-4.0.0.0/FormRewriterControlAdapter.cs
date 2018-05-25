// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System.Web;
using System.Web.UI;

namespace _4screen.CSB.Common
{
    public class FormRewriterControlAdapter : System.Web.UI.Adapters.ControlAdapter
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(new RewriteFormHtmlTextWriter(writer));
        }
    }

    public class RewriteFormHtmlTextWriter : HtmlTextWriter
    {
        public RewriteFormHtmlTextWriter(HtmlTextWriter writer) : base(writer)
        {
            InnerWriter = writer.InnerWriter;
        }

        public RewriteFormHtmlTextWriter(System.IO.TextWriter writer) : base(writer)
        {
            base.InnerWriter = writer;
        }

        public override void WriteAttribute(string name, string value, bool fEncode)
        {
            // If the attribute we are writing is the "action" attribute, and we are not on a sub-control, 
            // then replace the value to write with the raw URL of the request - which ensures that we'll
            // preserve the PathInfo value on postback scenarios

            if ((name == "action"))
            {
                HttpContext Context;
                Context = HttpContext.Current;

                if (Context.Items["ActionAlreadyWritten"] == null)
                {
                    //value = Context.Request.ServerVariables["HTTP_X_REWRITE_URL"];

                    // Indicate that we've already rewritten the <form>'s action attribute to prevent
                    // us from rewriting a sub-control under the <form> control

                    Context.Items["ActionAlreadyWritten"] = true;
                }
            }
            base.WriteAttribute(name, value, fEncode);
        }
    }
}