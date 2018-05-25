//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#2.0.0.0		27.03.2008 / AW
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class CustomizationBarTabStyles : System.Web.UI.UserControl
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");
        public Guid CommunityID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.SHHeader.CommunityID = CommunityID;
            this.SHBody.CommunityID = CommunityID;
            this.SHFooter.CommunityID = CommunityID;
        }

        protected void OnSaveClick(object sender, EventArgs e)
        {
            try
            {
                this.SHHeader.Save();
                this.SHBody.Save();
                this.SHFooter.Save();
                pnlStatus.Visible = true;
                litStatus.Controls.Add(new LiteralControl(language.GetString("MessageStyleSave")));
            }
            catch
            {
                pnlStatus.Visible = true;
                litStatus.Controls.Add(new LiteralControl(language.GetString("MessageStyleNotSave")));
            }
        }

        protected void OnCloseClick(object sender, EventArgs e)
        {
            string filteredQueryString = Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "edit", "tab" }, true);
            Response.Redirect(string.Format("{0}?{1}", Request.GetRawPath(), filteredQueryString));
        }
    }
}