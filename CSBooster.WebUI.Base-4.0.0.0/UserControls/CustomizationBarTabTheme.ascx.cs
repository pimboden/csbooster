//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		25.09.2007 / PI
//  Updated:   
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class CustomizationBarTabTheme : System.Web.UI.UserControl
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");

        private DataTable dtThemes;
        private HitblCommunityCty hitblCommunity;

        public Guid CommunityID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            FillThemes();

            hitblCommunity = HitblCommunityCty.FetchByID(CommunityID);
            rblThemes.SelectedValue = hitblCommunity.CtyTheme;
        }

        private void FillThemes()
        {
            object themeDataTable = Cache.Get("CreateCommunityWizardDTThemes");
            if (themeDataTable != null)
            {
                dtThemes = (DataTable)themeDataTable;
            }
            else
            {
                dtThemes = new DataTable("Themes");
                dtThemes.Columns.Add("ThemeName", typeof(string));
                dtThemes.Columns.Add("ThemeText", typeof(string));

                string[] themeDirectories = Directory.GetDirectories(Server.MapPath("~/App_Themes"));
                foreach (string themeDirectory in themeDirectories)
                {
                    DirectoryInfo themeDirectoryInfo = new DirectoryInfo(themeDirectory);
                    if (!themeDirectoryInfo.Name.ToLower().StartsWith("hidden"))
                    {
                        string themeImage = string.Format("/App_Themes/NoImage.gif");
                        FileInfo[] fileInfo = themeDirectoryInfo.GetFiles(string.Format("{0}.gif", themeDirectoryInfo.Name));
                        if (fileInfo.Length > 0)
                        {
                            themeImage = string.Format("/App_Themes/{0}/{1}", themeDirectoryInfo.Name, fileInfo[0].Name);
                        }
                        DataRow drTheme = dtThemes.NewRow();
                        drTheme["ThemeName"] = themeDirectoryInfo.Name;
                        drTheme["ThemeText"] = string.Format("<div style='margin-top:5px;'><img src='{0}'/></div>", themeImage);
                        dtThemes.Rows.Add(drTheme);
                    }
                }
                Cache.Insert("CreateCommunityWizardDTThemes", dtThemes);
            }
            rblThemes.DataSource = dtThemes.DefaultView;
            rblThemes.DataTextField = "ThemeText";
            rblThemes.DataValueField = "ThemeName";
            rblThemes.DataBind();
            if (rblThemes.Items.Count > 0)
                rblThemes.SelectedIndex = 0;
        }

        protected void OnLayoutChangeClick(object sender, EventArgs e)
        {
            DataObject dataObject = DataObject.Load<DataObject>(CommunityID, null, false);
            if ((dataObject.GetUserAccess(UserDataContext.GetUserDataContext()) & ObjectAccessRight.Update) != ObjectAccessRight.Update)
                throw new Exception("Access rights missing");

            hitblCommunity.CtyTheme = rblThemes.SelectedValue;
            hitblCommunity.Save();

            string filteredQueryString = Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "tab" }, false);
            Response.Redirect(string.Format("{0}?tab=theme{1}", Request.GetRawPath(), filteredQueryString));
        }

        protected void OnCloseClick(object sender, EventArgs e)
        {
            string filteredQueryString = Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "edit", "tab" }, true);
            Response.Redirect(string.Format("{0}?{1}", Request.GetRawPath(), filteredQueryString));
        }
    }
}