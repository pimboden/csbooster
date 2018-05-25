// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class MyCommInvitations : System.Web.UI.UserControl, IReloadable, IBrowsable
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        private UserDataContext udc;
        public UserDataContext Udc
        {
            get
            {
                if (udc == null)
                    udc = UserDataContext.GetUserDataContext();
                return udc;
            }
        }

        private int pageSize = 8;
        private int currentPage = 1;
        private int numberItems;



        protected void Page_Load(object sender, EventArgs e)
        {
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
            pager1.ItemNameSingular = language.GetString("TextInvitationSingular");
            pager1.ItemNamePlural = language.GetString("TextInvitationPlural");
            pager1.BrowsableControl = this;
            pager2.ItemNameSingular = language.GetString("TextInvitationSingular");
            pager2.ItemNamePlural = language.GetString("TextInvitationPlural");
            pager2.BrowsableControl = this;

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


        protected void OnInvitationItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Message item = (Message)e.Item.DataItem;

            Panel panel = (Panel)e.Item.FindControl("UD");
            RenderCommunity(panel, item);
        }

        private void RenderCommunity(Panel pnl, Message item)
        {
            try
            {
                XmlDocument xmlData = new XmlDocument();
                xmlData.LoadXml(item.XMLData);
                XmlNode xmlnCommunity = xmlData.SelectSingleNode("//root/communityid");
                string strCommId = xmlnCommunity.InnerText;
                DataObjectCommunity qo = DataObject.Load<DataObjectCommunity>(strCommId.ToNullableGuid(), ObjectShowState.Published, false);
                PlaceHolder phObjectOutput = new PlaceHolder();
                phObjectOutput.Controls.Add(new LiteralControl("<div class=\"invitation\">"));
                phObjectOutput.Controls.Add(new LiteralControl("<div class=\"\">"));
                phObjectOutput.Controls.Add(new LiteralControl("<div class=\"\">"));
                phObjectOutput.Controls.Add(new LiteralControl(string.Format(@"<img src='{0}{1}' title='{2}' class='' />", _4screen.CSB.Common.SiteConfig.MediaDomainName, qo.GetImage(PictureVersion.XS), qo.Title)));
                phObjectOutput.Controls.Add(new LiteralControl("</div>"));
                phObjectOutput.Controls.Add(new LiteralControl("</div>"));
                Community comm = new Community(qo.CommunityID.Value);

                string CommTitle = "";
                if (comm != null && comm.ProfileOrCommunity != null && !string.IsNullOrEmpty(comm.ProfileOrCommunity.Title))
                {
                    // set community/Profillink
                    CommTitle = comm.ProfileOrCommunity.Title;
                    if (comm.ProfileOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
                    {
                        CommTitle = GuiLanguage.GetGuiLanguage("SiteObjects").GetString("ProfileCommunity");
                    }
                    CommTitle = CommTitle.CropString(14);
                }
                phObjectOutput.Controls.Add(new LiteralControl(string.Format(@"<div class="""">{0}</div>", CommTitle)));



                string strTmp = qo.Nickname;
                if (strTmp.Length > 9)
                    strTmp = strTmp.Substring(0, 7) + "..";

                if (Guid.Equals(UserProfile.Current.UserId, qo.UserID.Value))
                {
                    phObjectOutput.Controls.Add(new LiteralControl(string.Format(@"<div class="""" title=""{1}: {2}"">von: {0}</div>", strTmp, language.GetString("LableCratedBy"), qo.Nickname)));
                }
                else
                {
                    phObjectOutput.Controls.Add(new LiteralControl(string.Format(@"<div class="""">von: <a class="""" href=""{0}{2}"" title=""{1}: {2}"">{3}</a></div>", Constants.Links["NICE_LINK_TO_USER_DETAIL"].Url, language.GetString("LableCratedBy"), qo.Nickname, strTmp)));

                }

                // set title
                string PlainTextDescription = qo.Title;
                if (PlainTextDescription.Length == 0)
                {
                    PlainTextDescription = language.GetString("LableNoTitle");
                }
                else
                {  //To make the text readable, replace BRs with " "
                    PlainTextDescription = Regex.Replace(PlainTextDescription, "<BR />", " ", RegexOptions.IgnoreCase);
                    //Now convert to plain text
                    PlainTextDescription = PlainTextDescription.StripHTMLTags();
                }
                string PlainTextCropped = PlainTextDescription.CropString(30);
                phObjectOutput.Controls.Add(new LiteralControl(string.Format(@"<div class="""" title=""{0}: {1}"">{2}</div>", language.GetString("LableTitle"), PlainTextDescription, PlainTextCropped)));

                phObjectOutput.Controls.Add(new LiteralControl(@"<div class="""">"));

                LinkButton btnJoinComm = new LinkButton();
                btnJoinComm.Text = languageShared.GetString("CommandAcciding");
                btnJoinComm.CssClass = "";
                btnJoinComm.CommandArgument = qo.ObjectID.Value.ToString() + "§" + item.MsgID + "§" + item.FromUserID.ToString();
                btnJoinComm.Click += new EventHandler(btnJoinComm_Click);
                phObjectOutput.Controls.Add(btnJoinComm);
                phObjectOutput.Controls.Add(new LiteralControl("<br/>"));

                LinkButton btnRejectComm = new LinkButton();
                btnRejectComm.Text = languageShared.GetString("CommandReject");
                btnRejectComm.CssClass = "";
                btnRejectComm.CommandArgument = qo.ObjectID.Value.ToString() + "§" + item.MsgID + "§" + item.FromUserID.ToString();
                btnRejectComm.Click += new EventHandler(btnRejectComm_Click);
                phObjectOutput.Controls.Add(btnRejectComm);
                phObjectOutput.Controls.Add(new LiteralControl("<br/>"));

                phObjectOutput.Controls.Add(new LiteralControl(string.Format(@"</div>")));
                phObjectOutput.Controls.Add(new LiteralControl(string.Format(@"<p class="""">&nbsp;</p>")));
                phObjectOutput.Controls.Add(new LiteralControl(@"</div>"));


                pnl.Controls.Add(phObjectOutput);
            }
            catch (Exception ex)
            {
                pnl.Controls.Add(new LiteralControl(string.Format("{0}<br/>{1}", ex.Message, ex.StackTrace)));
            }
        }

        void btnJoinComm_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            if (btn != null)
            {
                string[] commargs = btn.CommandArgument.Split('§');
                Guid ctyID = new Guid(commargs[0]);
                Guid msgId = new Guid(commargs[1]);
                Guid invitedBy = new Guid(commargs[2]);
                try
                {
                    HirelCommunityUserCur.Insert(ctyID, UserProfile.Current.UserId, false, 0, DateTime.Now, invitedBy);
                    SPs.HispDataObjectAddMemberCount(ctyID, 1).Execute();
                }
                catch
                {

                }
                Message.DeleteMessage(msgId, UserProfile.Current.UserId);
                Message.DeleteMessage(msgId, invitedBy);
            }
            this.Reload();
        }

        void btnRejectComm_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            if (btn != null)
            {
                string[] commargs = btn.CommandArgument.Split('§');
                Guid ctyID = new Guid(commargs[0]);
                Guid msgId = new Guid(commargs[1]);
                Guid invitedBy = new Guid(commargs[2]);
                Message.DeleteMessage(msgId, UserProfile.Current.UserId);
                Message.DeleteMessage(msgId, invitedBy);
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

            this.invRepeater.DataSource = Messages.GetInbox(UserProfile.Current.UserId, null, null, null, null, null, null, null, null, null, (int)MessageTypes.InviteToCommunity, currentPage, pageSize, "DateSent", "Asc", out numberItems, _4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));

            this.invRepeater.DataBind();
            // Check if the number of messages got smaller and the page would be out of bounce
            checkedPage = this.pager1.CheckPageRange(this.currentPage, numberItems);
            if (checkedPage != currentPage)
            {
                this.currentPage = checkedPage;
                this.PBPageNum.Value = "" + checkedPage;
                this.invRepeater.DataSource = Messages.GetInbox(UserProfile.Current.UserId, null, null, null, null, null, null, null, null, null, (int)MessageTypes.InviteToCommunity, currentPage, pageSize, "DateSent", "Asc", out numberItems, _4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
                this.invRepeater.DataBind();
            }
            this.pager1.InitPager(currentPage, numberItems);
            this.pager2.InitPager(currentPage, numberItems);

            if (numberItems > 0)
                this.noitem.Visible = false;
            else
                this.noitem.Visible = true;
        }

    }
}