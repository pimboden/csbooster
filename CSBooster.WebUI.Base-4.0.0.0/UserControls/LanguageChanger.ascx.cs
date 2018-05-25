using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using System.Globalization;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class LanguageChanger1 : System.Web.UI.UserControl
    {
        private string currentLanguage;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            currentLanguage = CultureHandler.GetCurrentSpecificLanguageCode();
            repLang.DataSource = _4screen.Utils.Web.SiteConfig.Cultures;
            repLang.DataMember = "Key";
            repLang.DataBind();
        }

        protected string GetLink(object langCode)
        {
            string queryStringWithoutLangParam = Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "LG" }, false);
            return string.Format("{0}?LG={1}{2}", Request.GetRawPath(), langCode, queryStringWithoutLangParam);
        }

        protected string GetNeutralCode(object langCode)
        {
            CultureInfo cu = new CultureInfo(langCode.ToString());
            return cu.Parent.Name;
        }

        protected string GetNeutralNativeName(object langCode)
        {
            CultureInfo cu = new CultureInfo(langCode.ToString());
            return cu.Parent.NativeName.ToUpper();
        }

        protected string GetNativeName(object langCode)
        {
            CultureInfo cu = new CultureInfo(langCode.ToString());
            return cu.NativeName;
        }

        protected string GetClass(object langCode)
        {
            CultureInfo cu = new CultureInfo(langCode.ToString());
            return string.Compare(currentLanguage, cu.Name, true) == 0 ? "languageActive" : "languageInactive";
        }
    }
}