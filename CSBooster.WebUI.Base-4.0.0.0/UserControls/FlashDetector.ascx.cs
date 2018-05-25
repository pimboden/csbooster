//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		24.08.2007 / PI
//                         Detects flash... Redirects to the same page if detection has not been done.
//  Updated:   
//******************************************************************************

using System;
using System.Text;
using _4screen.CSB.Common;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class FlashDetector : System.Web.UI.UserControl
    {
        private bool blnHasFlash;

        public bool HasFlash
        {
            get { return blnHasFlash; }
            set { blnHasFlash = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string qs = Request.QueryString["Flash"];
            if (!string.IsNullOrEmpty(qs))
            {
                if (qs.ToLower() == "true")
                {
                    HasFlash = true;
                }
                else
                {
                    HasFlash = false;
                }
            }
            else
            {
                string currentURL = Request.ServerVariables["SCRIPT_NAME"];
                if (!string.IsNullOrEmpty(Request.ServerVariables["QUERY_STRING"]))
                {
                    currentURL += "?" + Request.ServerVariables["QUERY_STRING"] + "&";
                }
                else
                {
                    currentURL += "?";
                }
                StringBuilder sbFlashDetector = new StringBuilder();
                sbFlashDetector.AppendFormat("<script src='/Library/Scripts/AC_OETags.js' language='javascript'></script>\n");
                sbFlashDetector.Append("<script language='JavaScript' type='text/javascript'>\n");
                sbFlashDetector.Append("var requiredMajorVersion = 9; \n");
                sbFlashDetector.Append("var requiredMinorVersion = 0; \n");
                sbFlashDetector.Append("var requiredRevision = 0; \n");
                sbFlashDetector.Append("var hasReqestedVersion = DetectFlashVer(requiredMajorVersion, requiredMinorVersion, requiredRevision);\n");
                sbFlashDetector.AppendFormat("var flashPage   = '{0}Flash=true';\n", currentURL);
                sbFlashDetector.AppendFormat("var noFlashPage = '{0}Flash=false';\n", currentURL);
                sbFlashDetector.Append("if (hasReqestedVersion) {\n");
                sbFlashDetector.Append("window.location = flashPage;\n");
                sbFlashDetector.Append("} \n");
                sbFlashDetector.Append("else {  // flash is too old or we can't detect the plugin\n");
                sbFlashDetector.Append("window.location = noFlashPage;\n");
                sbFlashDetector.Append("}\n");
                sbFlashDetector.Append("</script>\n");
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "FlashDetector", sbFlashDetector.ToString(), false);
            }

        }
    }
}