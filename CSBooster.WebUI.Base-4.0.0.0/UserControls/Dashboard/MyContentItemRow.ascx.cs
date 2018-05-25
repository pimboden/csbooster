// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class MyContentItemRow : System.Web.UI.UserControl
    {
        private Dictionary<int, string> featuredValues;
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");
        private GuiLanguage languageSiteObjects = GuiLanguage.GetGuiLanguage("SiteObjects");
        public delegate void DoSearchDelegate();

        public DataObject DataObject { get; set; }
        public DoSearchDelegate DoSearch { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            featuredValues = DataAccessConfiguration.LoadObjectFeaturedValues();

            Img.ImageUrl = _4screen.CSB.Common.SiteConfig.MediaDomainName + DataObject.GetImage(PictureVersion.S);
            Img.ID = null;

            LnkDet1.NavigateUrl = Helper.GetDetailLink(DataObject.ObjectType, DataObject.ObjectID.ToString());
            LnkDet1.ID = null;

            LnkDet2.NavigateUrl = LnkDet1.NavigateUrl;
            LnkDet2.Text = DataObject.Title.StripHTMLTags();
            LnkDet2.ID = null;

            LnkAuthor.NavigateUrl = Helper.GetDetailLink(Helper.GetObjectTypeNumericID("User"), DataObject.Nickname);
            LnkAuthor.Text = DataObject.Nickname;
            LnkAuthor.ID = null;

            DataObject profileOrCommunity = DataObject.Load<DataObject>(DataObject.CommunityID.Value);
            string communityCssClass = string.Empty;
            string communityTooltip = string.Empty;
            if (profileOrCommunity.State != ObjectState.Added)
            {
                string communityName = string.Empty;
                string communityUrl = string.Empty;
                if (profileOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("Community"))
                {
                    DataObjectCommunity community = DataObject.Load<DataObjectCommunity>(DataObject.CommunityID.Value);
                    communityName = profileOrCommunity.Title.CropString(14);
                    communityUrl = string.Format("{0}", Helper.GetDetailLink(Helper.GetObjectTypeNumericID("Community"), community.VirtualURL));
                    if (!community.Managed)
                    {
                        communityCssClass = "cty";
                        communityTooltip = languageSiteObjects.GetString("Community");
                    }
                    else
                    {
                        communityCssClass = "mcty";
                        communityTooltip = language.GetString("TooltipManagedCommunity");
                    }
                }
                else if (profileOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
                {
                    communityName = profileOrCommunity.Nickname.CropString(14);
                    communityUrl = string.Format("{0}", Helper.GetDetailLink(Helper.GetObjectTypeNumericID("User"), profileOrCommunity.Nickname));
                    communityCssClass = "prof";
                    communityTooltip = languageSiteObjects.GetString("ProfileCommunity");
                }
                else
                {
                    communityName = profileOrCommunity.Title.CropString(14);
                    communityUrl = string.Format("{0}", Helper.GetDetailLink(profileOrCommunity.ObjectType, profileOrCommunity.ObjectID.ToString()));
                    communityCssClass = "cty";
                    communityTooltip = Helper.GetObjectName(profileOrCommunity.ObjectType, true);
                }
                LnkCty.NavigateUrl = communityUrl;
                LnkCty.Text = communityName;
                LnkCty.ID = null;
            }
            else
            {
                communityCssClass = "errcty";
                communityTooltip = language.GetString("TooltipCommunityError");
            }
            PnlLoc.CssClass = communityCssClass;
            PnlLoc.ID = null;
            PnlLoc.ToolTip = communityTooltip;

            // Info icons
            PhInfo.Controls.Add(new LiteralControl(string.Format("<span class=\"icon {0}\" title=\"{1}\"></span>", DataObject.Status.ToString().ToLower(), language.GetString(string.Format("LableCommunity{0}", DataObject.Status)))));

            if (CustomizationSection.CachedInstance.MyContent.FeaturedEditEnabled)
            {
                PhInfo.Controls.Add(new LiteralControl(string.Format("<span class=\"icon featured\" title=\"{0}\">{1}</span>", featuredValues[DataObject.Featured], DataObject.Featured)));
            }

            switch (DataObject.ShowState)
            {
                case ObjectShowState.Published:
                    PhInfo.Controls.Add(new LiteralControl(string.Format("<span class=\"icon published\" title=\"{0}\"></span>", languageShared.GetString("TextShowStatePublished"))));
                    break;
                case ObjectShowState.Draft:
                    PhInfo.Controls.Add(new LiteralControl(string.Format("<span class=\"icon draft\" title=\"{0}\"></span>", languageShared.GetString("TextShowStateDraft"))));
                    break;
                case ObjectShowState.InProgress:
                    PhInfo.Controls.Add(new LiteralControl(string.Format("<span class=\"icon working\" title=\"{0}\"></span>", languageShared.GetString("TextShowStateInProgress"))));
                    break;
                case ObjectShowState.ConversionFailed:
                    PhInfo.Controls.Add(new LiteralControl(string.Format("<span class=\"icon failed\" title=\"{0}\"></span>", languageShared.GetString("TextShowStateConversionFailed"))));
                    break;
            }

            if (DataObject.Geo_Lat != double.MinValue)
                PhInfo.Controls.Add(new LiteralControl(string.Format("<span class=\"icon geo\" title=\"{0} {1}\"></span>", Helper.GetObjectName(DataObject.ObjectType, true), language.GetString("TextObjectHasGEO"))));

            // Function icons

            // Funktioniert nicht richtig (ist sprach abhängig) PT fragen 
            //functions.Controls.Add(GetInfoTooltip(DataObject));
            //LiteralControl infoLink = new LiteralControl();
            //infoLink.Text = string.Format("<a id=\"ITTT_{0}\" class=\"icon popup\" href=\"javascript:void(0)\"></a>", DataObject.ObjectID);
            //functions.Controls.Add(infoLink);

            if (CustomizationSection.CachedInstance.MyContent.FeaturedEditEnabled && SecuritySection.CachedInstance.CurrentUserHasAccess("DataObjectFeaturedEdit"))
            {
                PhFunc.Controls.Add(GetEditTooltip(DataObject));
                LiteralControl editLink = new LiteralControl();
                editLink.Text = string.Format("<a id=\"ETTT_{0}\" class=\"icon popup2\" href=\"javascript:void(0)\"></a>", DataObject.ObjectID);
                PhFunc.Controls.Add(editLink);
            }

            PhFunc.Controls.Add(new LiteralControl(string.Format("<a class=\"icon edit\" href=\"{0}\" title=\"{1} {2}\"></a>", GetEditUrl(DataObject), Helper.GetObjectName(DataObject.ObjectType, true), language.GetString("TextObjectEdit"))));

            if (DataObject.ShowState == ObjectShowState.Draft || DataObject.ShowState == ObjectShowState.Published)
            {
                bool isOwner = false;
                bool isMember = false;
                if (UserProfile.Current.UserId != Guid.Empty)
                    isOwner = Community.GetIsUserOwner(UserProfile.Current.UserId, DataObject.CommunityID.Value, out isMember);

                if (DataObject.ShowState == ObjectShowState.Draft && (isOwner || SecuritySection.CachedInstance.CurrentUserHasAccess("DataObjectShowStateEdit")))
                {
                    LinkButton publishButton = new LinkButton();
                    publishButton.CssClass = "icon publish";
                    publishButton.ToolTip = language.GetString("TooltipShowStateDraftToPublish");
                    publishButton.CommandArgument = DataObject.ObjectID.ToString();
                    publishButton.Click += new EventHandler(OnPublishButtonClick);
                    PhFunc.Controls.Add(publishButton);
                }
                else if (DataObject.ShowState == ObjectShowState.Published && (isOwner || SecuritySection.CachedInstance.CurrentUserHasAccess("DataObjectShowStateEdit") || DataObject.UserID == UserProfile.Current.UserId))
                {
                    LinkButton withdrawButton = new LinkButton();
                    withdrawButton.CssClass = "icon withdraw";
                    withdrawButton.ToolTip = language.GetString("TooltipShowStatePublishToDraft");
                    withdrawButton.CommandArgument = DataObject.ObjectID.ToString();
                    withdrawButton.Click += new EventHandler(OnWithdrawButtonClick);
                    PhFunc.Controls.Add(withdrawButton);
                }
            }

            if (DataObject.ObjectType == Helper.GetObjectTypeNumericID("Community"))
            {
                PhFunc.Controls.Add(new LiteralControl(string.Format("<a class=\"icon members\" href=\"Javascript:radWinOpen('/Pages/popups/CommunityMemberOwner.aspx?CN={0}', '{1}', 450, 440, false, null)\" title=\"{2}\"></a>", DataObject.CommunityID, language.GetString("TitleMemberEdit").StripForScript(), language.GetString("TitleMemberEdit"))));
                PhFunc.Controls.Add(new LiteralControl(string.Format("<a class=\"icon msg\" href=\"Javascript:radWinOpen('/Pages/popups/MessageSend.aspx?MsgType=msg&RecType=member&ObjType=Community&ObjId={0}', '{1}', 510, 430, false, null)\" title=\"{2}\"></a>", DataObject.CommunityID, language.GetString("TitleMessageToAllMembers").StripForScript(), language.GetString("TitleMessageToAllMembers"))));
            }

            PhFunc.Controls.Add(new LiteralControl(string.Format("<a class=\"icon delete\" onclick=\"{0}\" href=\"javascript:void(0)\" title=\"{1} {2}\"></a>", GetDeleteUrl(DataObject), Helper.GetObjectName(DataObject.ObjectType, true), language.GetString("TextObjectDelete"))));
        }

        private Telerik.Web.UI.RadToolTip GetEditTooltip(DataObject DataObject)
        {
            Telerik.Web.UI.RadToolTip tooltip = GetTooltip(string.Format(@"ETT_{0}", DataObject.ObjectID), string.Format(@"ETTT_{0}", DataObject.ObjectID));

            DropDownList dropDownList = new DropDownList();
            dropDownList.ID = string.Format("DDLFeat_{0}", DataObject.ObjectID);
            dropDownList.SelectedIndexChanged += new EventHandler(OnFeaturedValueChange);
            dropDownList.AutoPostBack = true;
            foreach (var featuredValue in featuredValues)
            {
                ListItem listItem = new ListItem(featuredValue.Value, featuredValue.Key.ToString());
                if (DataObject.Featured == featuredValue.Key)
                    listItem.Selected = true;
                dropDownList.Items.Add(listItem);
            }
            tooltip.Controls.Add(dropDownList);

            return tooltip;
        }

        private Telerik.Web.UI.RadToolTip GetTooltip(string tooltipId, string targetId)
        {
            Telerik.Web.UI.RadToolTip tooltip = new Telerik.Web.UI.RadToolTip();
            tooltip.ShowEvent = Telerik.Web.UI.ToolTipShowEvent.OnMouseOver;
            tooltip.Position = Telerik.Web.UI.ToolTipPosition.TopRight;
            tooltip.RelativeTo = Telerik.Web.UI.ToolTipRelativeDisplay.Element;
            tooltip.HideEvent = Telerik.Web.UI.ToolTipHideEvent.LeaveToolTip;
            tooltip.ID = tooltipId;
            tooltip.IsClientID = true;
            tooltip.TargetControlID = targetId;
            return tooltip;
        }

        private Telerik.Web.UI.RadToolTip GetInfoTooltip(DataObject DataObject)
        {
            Telerik.Web.UI.RadToolTip tooltip = GetTooltip(string.Format(@"ITT_{0}", DataObject.ObjectID), string.Format(@"ITTT_{0}", DataObject.ObjectID));
            ObjectDetailsSmall objectDetailsSmall = (ObjectDetailsSmall)this.LoadControl(typeof(ObjectDetailsSmall), null);
            objectDetailsSmall.DataObject = DataObject;
            Literal literal = new Literal();
            literal.Text = objectDetailsSmall.GetContent();
            tooltip.Controls.Add(literal);
            return tooltip;
        }

        private string GetDeleteUrl(DataObject qo)
        {
            return string.Format("DeleteDataObject('/Pages/popups/DeleteObject.aspx?type={0}&id={1}');", (int)qo.ObjectType, qo.ObjectID);
        }

        private string GetEditUrl(DataObject qo)
        {
            string editWizardLink = string.Empty;
            string editWizardUrl = string.Format("{0}&Refresh=false", Helper.GetEditWizardLink(qo.ObjectType, qo.ObjectID.ToString(), _4screen.CSB.Common.SiteConfig.UsePopupWindows));
            if (_4screen.CSB.Common.SiteConfig.UsePopupWindows)
                editWizardLink = string.Format("javascript:radWinOpen('{0}', '{1} {2}', 800, 500, false, 'RefreshMyContent', 'wizardWin')", editWizardUrl, Helper.GetObjectName(qo.ObjectType, true).StripForScript(), language.GetString("TextObjectEdit").StripForScript());
            else
                editWizardLink = string.Format("{0}&ReturnUrl={1}", editWizardUrl, System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(Request.RawUrl)));
            return editWizardLink;
        }

        protected void OnFeaturedValueChange(object sender, EventArgs e)
        {
            DropDownList dropDownList = (DropDownList)sender;
            int featuredValue = int.Parse(dropDownList.SelectedValue);
            Guid objectId = dropDownList.ID.Replace("DDLFeat_", "").ToGuid();
            DataObject dataObject = DataObject.Load<DataObject>(objectId, null, true);
            if (dataObject.State != ObjectState.Added)
            {
                dataObject.Featured = featuredValue;
                dataObject.Update(UserDataContext.GetUserDataContext());

                DoSearch();
            }
        }

        protected void OnPublishButtonClick(object sender, EventArgs e)
        {
            DataObject dataObject = DataObject.Load<DataObject>(((LinkButton)sender).CommandArgument.ToGuid(), null, true);
            dataObject.ShowState = ObjectShowState.Published;
            dataObject.Update(UserDataContext.GetUserDataContext());

            DoSearch();
        }

        protected void OnWithdrawButtonClick(object sender, EventArgs e)
        {
            DataObject dataObject = DataObject.Load<DataObject>(((LinkButton)sender).CommandArgument.ToGuid(), null, true);
            dataObject.ShowState = ObjectShowState.Draft;
            dataObject.Update(UserDataContext.GetUserDataContext());

            DoSearch();
        }
    }
}