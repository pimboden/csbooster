//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		30.08.2007 / TS
//  Updated:   
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using SubSonic;
using SiteConfig = _4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.Pages.Popups.UserControls
{
    public partial class ObjectLinking : System.Web.UI.UserControl
    {
        private DataObject dataObject;
        private string _CloseWindowJavaScript = string.Empty;
        private string _CloseWindowJSFunction = string.Empty;

        public DataObject DataObject
        {
            get { return dataObject; }
            set { dataObject = value; }
        }

        public string CloseWindowJavaScript
        {
            get { return _CloseWindowJavaScript; }
            set { _CloseWindowJavaScript = value; }
        }

        public string CloseWindowJSFunction
        {
            get { return _CloseWindowJSFunction; }
            set { _CloseWindowJSFunction = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UserDataContext udc = UserDataContext.GetUserDataContext();
            DataObjectCommunity myProfile = DataObject.Load<DataObjectCommunity>(UserProfile.Current.ProfileCommunityID);

            QuickParameters quickParameters = new QuickParameters()
                                                  {
                                                      Udc = udc,
                                                      UserID = UserProfile.Current.UserId,
                                                      IgnoreCache = true,
                                                      MembershipParams = new MembershipParams { UserID = UserProfile.Current.UserId }
                                                  };
            List<DataObjectCommunity> involvedCommunities = DataObjects.Load<DataObjectCommunity>(quickParameters);
            involvedCommunities.Insert(0, myProfile);
            this.CTYS.DataSource = involvedCommunities;
            this.CTYS.DataBind();

            if (!string.IsNullOrEmpty(CloseWindowJavaScript))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CloseWindowJavaScript", CloseWindowJavaScript, false);
            }
        }

        protected void CTYS_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataObjectCommunity community = (DataObjectCommunity)e.Item.DataItem;

            Literal literal = (Literal)e.Item.FindControl("CTYTITLE");
            if (community.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
                literal.Text = GuiLanguage.GetGuiLanguage("ProfileData").GetString("MyProfile");
            else
                literal.Text = community.Title;
            StoredProcedure sp = SPs.HispDataObjectFindInCommunity(dataObject.ObjectID, community.ObjectID.Value, null);
            sp.Execute();
            bool ObjectFound = Convert.ToBoolean(sp.OutputValues[0]);
            literal = (Literal)e.Item.FindControl("CTYSEL");
            if (!ObjectFound)
            {
                literal.Text = "<input type=\"checkbox\" name=\"CTYSEL\" value=\"" + community.ObjectID.Value.ToString() + "\"/>";
            }
            else
            {
                literal.Text = "<input type=\"checkbox\" name=\"CTYSEL\" checked='true' disabled='true' value=\"\" />";
            }
        }

        protected void OnCopyObject(object sender, EventArgs e)
        {
            String[] communityIds = Request.Form.GetValues("CTYSEL");
            if (communityIds != null)
            {
                foreach (string communityId in communityIds)
                {
                    if (communityId.Length > 0)
                    {
                        DataObject copiedDataObject = dataObject.CopyToCommunity(UserDataContext.GetUserDataContext(), communityId.ToGuid());
                        copiedDataObject.Insert(UserDataContext.GetUserDataContext());
                    }
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CloseWindowClosFunc", CloseWindowJSFunction, true);
            }
        }
    }
}