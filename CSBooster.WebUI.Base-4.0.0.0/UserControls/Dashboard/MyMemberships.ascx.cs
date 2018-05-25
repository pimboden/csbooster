// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class MyMemberships : System.Web.UI.UserControl, IReloadable, IBrowsable
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");
        protected GuiLanguage languageSiteObjects = GuiLanguage.GetGuiLanguage("SiteObjects");

        private UserDataContext udc;
        private bool? blnIgnoreCache = null;

        public UserDataContext Udc
        {
            get
            {
                if (udc == null)
                    udc = UserDataContext.GetUserDataContext();
                return udc;
            }
        }

        private int pageSize = 10;
        private int currentPage = 1;
        private int numberItems;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitMember();
            RestoreState();
            Reload();
            SaveState();
        }

        protected override void OnInit(EventArgs e)
        {
        }

        // Restore page state without using viewstates
        private void RestoreState()
        {
            string idPrefix = this.UniqueID + "$";
            string idPrefixAlt = this.ClientID + "_";
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBPageNum")))
                currentPage = int.Parse(Request.Params.Get(idPrefix + "PBPageNum"));
        }

        private void SaveState()
        {
            if (this.currentPage != 0)
                this.PBPageNum.Value = "" + currentPage;
        }

        private void ClearState()
        {
        }

        private void InitMember()
        {
            pager1.ItemNameSingular = languageSiteObjects.GetString("Community");
            pager1.ItemNamePlural = languageSiteObjects.GetString("Communities");
            pager1.BrowsableControl = this;
            pager2.ItemNameSingular = languageSiteObjects.GetString("Community");
            pager2.ItemNamePlural = languageSiteObjects.GetString("Communities");
            pager2.BrowsableControl = this;
        }

        protected void OnMembershipItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataObjectCommunity item = (DataObjectCommunity)e.Item.DataItem;

            Panel panel = (Panel)e.Item.FindControl("UD");
            RenderCommunity(panel, item);
        }

        private void RenderCommunity(Panel pnl, DataObjectCommunity item)
        {
            PlaceHolder ph = new PlaceHolder();
            ph.Controls.Add(new LiteralControl("<div class=\"membership\">"));
            ph.Controls.Add(new LiteralControl("<div class=\"\">"));
            ph.Controls.Add(new LiteralControl("<div class=\"\">"));
            ph.Controls.Add(new LiteralControl(string.Format("<img src='{0}{1}' title='{2}' class='CSB_img105' />", _4screen.CSB.Common.SiteConfig.MediaDomainName, item.GetImage(PictureVersion.XS), item.Title)));
            ph.Controls.Add(new LiteralControl("</div>"));
            ph.Controls.Add(new LiteralControl("</div>"));

            // set community/Profillink
            string CommTitle = item.Title.CropString(14);
            ph.Controls.Add(new LiteralControl(string.Format(@"<div class=""""><a class="""" href=""/Default.aspx?CN={0}"" title=""Gehe zu Community: {2}"">{1}</a></div>", item.CommunityID, CommTitle, item.Title)));

            string strTmp = item.Nickname;
            if (strTmp.Length > 9)
                strTmp = strTmp.Substring(0, 7) + "..";

            if (Guid.Equals(UserProfile.Current.UserId, item.UserID.Value))
            {
                ph.Controls.Add(new LiteralControl(string.Format(@"<div class="""" title=""{2}: {1}"">{3}: {0}</div>", strTmp, item.Nickname, language.GetString("LableCratedBy"), language.GetString("LableFrom"))));
            }
            else
            {
                ph.Controls.Add(new LiteralControl(string.Format(@"<div class="""">von: <a class="""" href=""{0}{1}"" title=""{3}: {1}"">{2}</a></div>", Constants.Links["NICE_LINK_TO_USER_DETAIL"].Url, item.Nickname, strTmp, language.GetString("LableCratedBy"))));

            }
            ph.Controls.Add(new LiteralControl(@"<div class="""">"));
            ph.Controls.Add(new LiteralControl(string.Format(@"<a id=""SpnInf_{0}"" href=""javascript:void(0)"" class="""">{1}</a>", item.ObjectID, language.GetString("LableInfo"))));
            ph.Controls.Add(new LiteralControl("<br/>"));

            Telerik.Web.UI.RadToolTip tooltip = new Telerik.Web.UI.RadToolTip();
            tooltip.TargetControlID = string.Format(@"SpnInf_{0}", item.ObjectID);
            tooltip.IsClientID = true;
            tooltip.EnableEmbeddedSkins = false;
            tooltip.ShowEvent = Telerik.Web.UI.ToolTipShowEvent.OnClick;
            tooltip.Position = Telerik.Web.UI.ToolTipPosition.TopRight;
            tooltip.RelativeTo = Telerik.Web.UI.ToolTipRelativeDisplay.Element;
            tooltip.HideEvent = Telerik.Web.UI.ToolTipHideEvent.LeaveToolTip;
            ObjectDetailsSmall objectDetailsSmall = this.LoadControl("~/UserControls/ObjectDetailsSmall.ascx") as ObjectDetailsSmall;

            objectDetailsSmall.DataObject = item;
            Literal literal = new Literal();
            literal.Text = objectDetailsSmall.GetContent();
            tooltip.Controls.Add(literal);
            ph.Controls.Add(tooltip);

            //Löschen
            if (item.UserID.Value != UserProfile.Current.UserId)
            {
                LinkButton btnRem = new LinkButton();
                btnRem.Text = language.GetString("CommandMembershipRelease");
                btnRem.CssClass = "";
                btnRem.CommandArgument = item.CommunityID.Value.ToString();
                btnRem.Click += new EventHandler(btnRem_Click);
                ph.Controls.Add(btnRem);
                ph.Controls.Add(new LiteralControl("<br/>"));
            }
            else
            {
                ph.Controls.Add(new LiteralControl(@"<span class="""">&nbsp;</span><br/>"));
            }

            ph.Controls.Add(new LiteralControl(string.Format(@"</div>")));
            ph.Controls.Add(new LiteralControl(@"</div>"));
            pnl.Controls.Add(ph);
        }

        void btnRem_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            if (btn != null)
            {
                string strID = btn.CommandArgument;
                Member.RemoveMember(new Guid(strID), UserProfile.Current.UserId);
                blnIgnoreCache = true;
            }
            this.Reload();
        }

        // Interface IBrowsable
        public int GetNumberItems()
        {
            return this.numberItems;
        }

        public void SetCurrentPage(int currentPage)
        {
            this.currentPage = currentPage;
            SaveState();
            Reload();
        }

        // Interface IReloadable
        public void Reload()
        {
            int checkedPage = 0;
            if (currentPage == 1)
                blnIgnoreCache = true;
            this.memRepeater.DataSource = DataObjects.Load<DataObjectCommunity>( new QuickParameters() { Udc = Udc, PageNumber = currentPage, PageSize = pageSize, ShowState = ObjectShowState.Published, IgnoreCache = blnIgnoreCache , MembershipParams = new MembershipParams{UserID =UserProfile.Current.UserId, IsCreator=null, IsOwner = null  }});
            checkedPage = ((DataObjectList<DataObjectCommunity>)this.memRepeater.DataSource).PageTotal;
            numberItems = ((DataObjectList<DataObjectCommunity>)this.memRepeater.DataSource).ItemTotal;
            blnIgnoreCache = null;
            this.memRepeater.DataBind();
            // Check if the number of messages got smaller and the page would be out of bounce
            checkedPage = this.pager1.CheckPageRange(this.currentPage, numberItems);
            if (checkedPage != currentPage)
            {
                this.currentPage = checkedPage;
                this.PBPageNum.Value = "" + checkedPage;
                this.memRepeater.DataSource = DataObjects.Load<DataObjectCommunity>(new QuickParameters() { Udc = Udc, PageNumber = currentPage, PageSize = pageSize, ShowState = ObjectShowState.Published, IgnoreCache = blnIgnoreCache, MembershipParams = new MembershipParams { UserID = UserProfile.Current.UserId, IsCreator = null, IsOwner = null } });
                numberItems = ((DataObjectList<DataObjectCommunity>)this.memRepeater.DataSource).ItemTotal;
                this.memRepeater.DataBind();
            }
            this.pager1.InitPager(currentPage, numberItems);
            this.pager2.InitPager(currentPage, numberItems);

            if (numberItems > 0)
                this.noitem.Visible = false;

            blnIgnoreCache = null;
        }

        protected void ttmg_AjaxUpdate(object sender, Telerik.Web.UI.ToolTipUpdateEventArgs e)
        {
            try
            {
                if (e.TargetControlID.StartsWith("SpnInf_"))
                {
                    //Info Button
                    string ObjectID = e.TargetControlID.Substring(e.TargetControlID.IndexOf('_') + 1);

                    DataObject obj = DataObject.Load<DataObject>(ObjectID.ToGuid(),null, false);
                    if (obj.State != ObjectState.Added)
                    {
                        ObjectDetailsSmall objectDetailsSmall = (ObjectDetailsSmall)this.LoadControl("~/UserControls/ObjectDetailsSmall.ascx");
                        objectDetailsSmall.DataObject = obj;
                        objectDetailsSmall.ID = string.Format("ODS_{0}", ObjectID);
                        e.UpdatePanel.ContentTemplateContainer.Controls.Add(objectDetailsSmall);
                    }
                }
            }
            catch
            {
            }
        }
    }
}