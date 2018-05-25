//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		26.09.2007 / PI
//  Updated:   
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class CustomizationBarTabLayout : System.Web.UI.UserControl
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");

        private Community community;
        private DataTable dtLayouts;
        private HitblCommunityCty hitblCommunity;
        private UserDataContext udc;
        protected string originalLayout;

        public Guid CommunityID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            udc = UserDataContext.GetUserDataContext();
            community = new Community(CommunityID);

            FillLayouts();
            LoadData();
        }

        private void FillLayouts()
        {
            string pageType = "Community";
            if (community.ProfileOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
                pageType = "Profile";
            else if (community.ProfileOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("Page"))
                pageType = "Page";

            object layoutsDataTable = Cache.Get(string.Format("Create{0}WizardDTLayouts_{1}", pageType, udc.UserRole.ToUpper()));
            if (layoutsDataTable != null)
            {
                dtLayouts = (DataTable)layoutsDataTable;
            }
            else
            {
                dtLayouts = new DataTable("Layouts");
                dtLayouts.Columns.Add("LayoutName", typeof(string));
                dtLayouts.Columns.Add("LayoutText", typeof(string));

                var layouts = Layouts.GetLayouts();

                foreach (var layout in layouts)
                {
                    if ((layout.Roles == "*" || layout.Roles.ToUpper().Contains(udc.UserRole.ToUpper())) &&
                        (layout.PageTypes == "*" || layout.PageTypes.ToUpper().Contains(pageType.ToUpper())))
                    {
                        DataRow drLayout = dtLayouts.NewRow();
                        drLayout["LayoutName"] = layout.Name;
                        drLayout["LayoutText"] = string.Format("<div style='margin-top:5px;'><img src='/App_Layouts/{0}/icon.gif'/></div>", layout.Name);
                        dtLayouts.Rows.Add(drLayout);
                    }
                }
                Cache.Insert(string.Format("Create{0}WizardDTLayouts_{1}", pageType, udc.UserRole.ToUpper()), dtLayouts);
            }
            rblLayouts.DataSource = dtLayouts.DefaultView;
            rblLayouts.DataTextField = "LayoutText";
            rblLayouts.DataValueField = "LayoutName";
            rblLayouts.DataBind();
            if (rblLayouts.Items.Count > 0)
            {
                rblLayouts.SelectedIndex = 0;
                originalLayout = rblLayouts.SelectedValue;
            }
        }

        private void LoadData()
        {
            hitblCommunity = HitblCommunityCty.FetchByID(CommunityID);

            string currentLayoutName = HitblCommunityCty.FetchByID(CommunityID).CtyLayout;

            foreach (ListItem listItem in rblLayouts.Items)
            {
                if (listItem.Value == currentLayoutName)
                {
                    rblLayouts.SelectedIndex = rblLayouts.Items.IndexOf(listItem);
                    originalLayout = listItem.Value;
                }
                else
                {
                    listItem.Attributes.Remove("onclick");
                    listItem.Attributes.Add("onclick", string.Format("LayoutChange({0}, {1})", Layouts.GetLayout(currentLayoutName).NumberDropZones, Layouts.GetLayout(listItem.Value).NumberDropZones));
                }
            }
        }

        protected void OnLayoutChangeClick(object sender, EventArgs e)
        {
            DataObject dataObject = DataObject.Load<DataObject>(CommunityID, null, false);
            if ((dataObject.GetUserAccess(UserDataContext.GetUserDataContext()) & ObjectAccessRight.Update) != ObjectAccessRight.Update)
                throw new Exception("Access rights missing");

            hitblCommunity.CtyLayout = rblLayouts.SelectedValue;
            hitblCommunity.Save();
            int previousColumnCount = Layouts.GetLayout(originalLayout).NumberDropZones;
            int newColumnCount = Layouts.GetLayout(rblLayouts.SelectedValue).NumberDropZones;
            if (previousColumnCount > newColumnCount)
            {
                SPs.HispWidgetInstanceReorderColumns(hitblCommunity.CtyId, newColumnCount).Execute();
            }
            originalLayout = rblLayouts.SelectedValue;

            string filteredQueryString = Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "tab" }, false);
            Response.Redirect(string.Format("{0}?tab=layout{1}", Request.GetRawPath(), filteredQueryString));
        }

        protected void OnCloseClick(object sender, EventArgs e)
        {
            string filteredQueryString = Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "edit", "tab" }, true);
            Response.Redirect(string.Format("{0}?{1}", Request.GetRawPath(), filteredQueryString));
        }
    }
}