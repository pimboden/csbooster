// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class CntEdit : System.Web.UI.UserControl, ISettings
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");
        public Dictionary<string, object> Settings { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Guid? objectId = null;

            if (Settings != null && Settings.ContainsKey("ObjectID"))
                objectId = Settings["ObjectID"].ToString().ToNullableGuid();
            else if (!string.IsNullOrEmpty(Request.QueryString["OID"]))
                objectId = Request.QueryString["OID"].ToNullableGuid();

            if (objectId.HasValue)
            {
                try
                {
                    UserDataContext udc = UserDataContext.GetUserDataContext();
                    if (udc.IsAuthenticated)
                    {
                        DataObject dataObject = DataObject.Load<DataObject>(objectId.Value, null, false);
                        if (dataObject.State != ObjectState.Added)
                        {
                            if (dataObject.UserID.Value == udc.UserID || udc.IsAdmin)
                            {
                                if ((dataObject.GetUserAccess(UserDataContext.GetUserDataContext()) & ObjectAccessRight.Update) == ObjectAccessRight.Update)
                                {
                                    this.PnlEditTrig.Visible = true;
                                    this.RTT.Visible = true;
                                    RenderControls(PhFunc2, dataObject);
                                }
                            }
                        }
                    }
                }
                catch (SiemeSecurityException)
                {
                    Response.Redirect("/Pages/Static/AccessDenied.aspx", true);
                }
            }
        }

        private void RenderControls(PlaceHolder ph, DataObject dataObject)
        {
            if (dataObject.ObjectType != Helper.GetObjectTypeNumericID("Community") &&
                dataObject.ObjectType != Helper.GetObjectTypeNumericID("Page") &&
                dataObject.ObjectType != Helper.GetObjectTypeNumericID("User"))
            {
                HtmlGenericControl li = new HtmlGenericControl("li");

                bool isOwner = false;
                bool isMember = false;
                isOwner = Community.GetIsUserOwner(UserProfile.Current.UserId, dataObject.CommunityID.Value, out isMember);

                if (dataObject.ObjectType != Helper.GetObjectTypeNumericID("User"))
                {
                    string editWizardUrl = string.Format("{0}", Helper.GetEditWizardLink(dataObject.ObjectType, dataObject.ObjectID.ToString(), _4screen.CSB.Common.SiteConfig.UsePopupWindows));
                    string editWizardLink;
                    if (_4screen.CSB.Common.SiteConfig.UsePopupWindows)
                        editWizardLink = string.Format("javascript:radWinOpen('{0}', '{1} {2}', 800, 500, false, null, 'wizardWin')", editWizardUrl, Helper.GetObjectName(dataObject.ObjectType, true).StripForScript(), languageShared.GetString("CommandProcess").ToLower().StripForScript());
                    else
                        editWizardLink = string.Format("{0}&ReturnUrl={1}", editWizardUrl, System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(Request.RawUrl)));

                    ph.Controls.Add(new LiteralControl(string.Format("<li class=\"edit\"><a href=\"{0}\">{1} {2}</a></li>", editWizardLink, Helper.GetObjectName(dataObject.ObjectType, true), languageShared.GetString("CommandProcess").ToLower())));
                }

                if (dataObject.ShowState == ObjectShowState.Draft && (isOwner || SecuritySection.CachedInstance.CurrentUserHasAccess("DataObjectShowStateEdit")))
                {
                    LinkButton publishButton = new LinkButton();
                    publishButton.Text = language.GetString("CommandPageDraftToPublic");
                    publishButton.CommandArgument = dataObject.ObjectID.Value.ToString();
                    publishButton.Click += new EventHandler(OnPublishButtonClick);
                    li.Attributes.Add("class", "publish");
                    li.Controls.Add(publishButton);
                    ph.Controls.Add(li);
                }
                else if (dataObject.ShowState == ObjectShowState.Published && (isOwner || SecuritySection.CachedInstance.CurrentUserHasAccess("DataObjectShowStateEdit") || dataObject.UserID.Value == UserProfile.Current.UserId))
                {
                    LinkButton withdrawButton = new LinkButton();
                    withdrawButton.Text = language.GetString("CommandPagePublicToDraft");
                    withdrawButton.CommandArgument = dataObject.ObjectID.Value.ToString();
                    withdrawButton.Click += new EventHandler(OnWithdrawButtonClick);
                    li.Attributes.Add("class", "withdraw");
                    li.Controls.Add(withdrawButton);
                    ph.Controls.Add(li);
                }

                if (CustomizationSection.CachedInstance.MyContent.FeaturedEditEnabled && SecuritySection.CachedInstance.CurrentUserHasAccess("DataObjectFeaturedEdit"))
                {
                    li = new HtmlGenericControl("li");
                    li.Attributes.Add("class", "featured");
                    DropDownList dropDownList = new DropDownList();
                    dropDownList.ID = string.Format("DDLFeat_{0}", dataObject.ObjectID.Value);
                    dropDownList.SelectedIndexChanged += new EventHandler(OnFeaturedValueChange);
                    dropDownList.AutoPostBack = true;
                    Dictionary<int, string> featuredValues = DataAccessConfiguration.LoadObjectFeaturedValues();
                    foreach (var featuredValue in featuredValues)
                    {
                        ListItem listItem = new ListItem(featuredValue.Value, featuredValue.Key.ToString());
                        if (dataObject.Featured == featuredValue.Key)
                            listItem.Selected = true;
                        dropDownList.Items.Add(listItem);
                    }
                    li.Controls.Add(dropDownList);
                    ph.Controls.Add(li);
                }

                ph.Controls.Add(new LiteralControl(string.Format("<li class=\"delete\"><a onclick=\"{0}\" href=\"javascript:void(0)\">{1} {2}</a></li>", GetDeleteUrl(dataObject), Helper.GetObjectName(dataObject.ObjectType, true), languageShared.GetString("CommandDelete").ToLower())));
                ph.Controls.Add(new LiteralControl(string.Format("<li class=\"mycnt\"><a href=\"{0}&T={1}&W={2}&C=&I={3}\">{4}</a></li>", Helper.GetDashboardLink(Common.Dashboard.ManageContent), Helper.GetObjectType(dataObject.ObjectType).Id, dataObject.ObjectID.Value, (dataObject.UserID == UserProfile.Current.UserId).ToString().ToLower(), language.GetString("CommandPageMyContent"))));
            }
        }

        private string GetDeleteUrl(DataObject dataObject)
        {
            return string.Format("DeleteDataObject('/Pages/popups/DeleteObject.aspx?type={0}&id={1}');", (int)dataObject.ObjectType, dataObject.ObjectID);
        }

        protected void OnFeaturedValueChange(object sender, EventArgs e)
        {
            DropDownList dropDownList = (DropDownList)sender;
            int featuredValue = int.Parse(dropDownList.SelectedValue);
            string objectId = dropDownList.ID.Replace("DDLFeat_", "");

            DataObject dataObject = DataObject.Load<DataObject>(objectId.ToGuid(), null, true);
            if (dataObject.State != ObjectState.Added)
            {
                dataObject.Featured = featuredValue;
                dataObject.Update(UserDataContext.GetUserDataContext());

                Response.Redirect(Request.RawUrl);
            }
        }

        protected void OnPublishButtonClick(object sender, EventArgs e)
        {
            DataObject dataObject = DataObject.Load<DataObject>(((LinkButton)sender).CommandArgument.ToGuid(), null, true);
            dataObject.ShowState = ObjectShowState.Published;
            dataObject.Update(UserDataContext.GetUserDataContext());

            Response.Redirect(Request.RawUrl);
        }

        protected void OnWithdrawButtonClick(object sender, EventArgs e)
        {
            DataObject dataObject = DataObject.Load<DataObject>(((LinkButton)sender).CommandArgument.ToGuid(), null, true);
            dataObject.ShowState = ObjectShowState.Draft;
            dataObject.Update(UserDataContext.GetUserDataContext());

            Response.Redirect(Request.RawUrl);
        }
    }
}
