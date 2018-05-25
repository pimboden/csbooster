// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.DataAccess.Data;
using _4screen.CSB.WebUI.Code;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class ProfileDeleteCommunity : ProfileQuestionsControl
    {
        private bool usedSingleSignOn;
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DataObjectUser user = DataObject.Load<DataObjectUser>(UserProfile.Current.UserId);
            if (user.State != ObjectState.Added && !string.IsNullOrEmpty(user.FacebookUserId))
            {
                usedSingleSignOn = true;
                PnlPassword.Visible = false;
            }
            PnlPassword.ID = null;
        }

        protected void OnDeleteProfileClick(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Visible = false;
                MembershipUser membershipUser = Membership.GetUser(UserProfile.Current.UserName);
                if (usedSingleSignOn || membershipUser.ChangePassword(TxtPw.Text, Constants.DEFAULT_USER_PASSWORD))
                {
                    try
                    {
                        List<string> friendIds = new List<string>();
                        if (cbxInfo.Checked)
                            friendIds = GetFriends();

                        SubSonic.StoredProcedure sp = SPs.HispUserDeleteaspnetuser(UserProfile.Current.UserId);
                        sp.Execute();

                        if (cbxInfo.Checked)
                            SendMessages(friendIds);

                        try { Response.Redirect("/Pages/Other/logout.ashx", true); }
                        catch { return; }
                    }
                    catch
                    {
                        lblMsg.Text = language.GetString("MessageProfileDeleteError");
                        lblMsg.Visible = true;
                    }
                }
                else
                {
                    lblMsg.Text = language.GetString("MessageProfileDeletePassword");
                    lblMsg.Visible = true;
                }
            }
            catch
            {
                lblMsg.Text = language.GetString("MessageProfileDeletePassword");
                lblMsg.Visible = true;
            }
        }

        private static List<string> GetFriends()
        {
            List<string> friendIds = new List<string>();
            DataObjectList<DataObjectFriend> friends = DataObjects.Load<DataObjectFriend>(new QuickParametersFriends
            {
                Udc = UserDataContext.GetUserDataContext(),
                SortBy = QuickSort.StartDate,
                CurrentUserID = UserProfile.Current.UserId,
                OnlyWithImage = false,
                OnlyNotBlocked = true
            });
            foreach (DataObjectFriend friend in friends)
            {
                if (friend.MsgFrom == ";Nobody;")
                    continue;

                friendIds.Add(friend.ObjectID.Value.ToString());
            }

            return friendIds;
        }

        private void SendMessages(List<string> friendIds)
        {
            string body = GuiLanguage.GetGuiLanguage("Templates").GetString("EmailProfileDelete");
            body = body.Replace("<%SITE_URL%>", _4screen.CSB.Common.SiteConfig.SiteURL);
            body = body.Replace("<%FROM_USERNAME%>", UserProfile.Current.UserName);

            foreach (string friendId in friendIds)
            {
                Message message = new Message(_4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
                message.MsgID = Guid.NewGuid();
                message.FromUserID = UserProfile.Current.UserId;
                message.UserId = new Guid(friendId);
                message.TypOfMessage = (int)MessageTypes.NormalMessage;
                message.Subject = language.GetString("TextProfileDeleteSubject");
                message.MsgText = body;
                message.ShowInInbox = true;
                message.ShowInOutbox = false;
                message.SendNormalMessage();
            }
        }
    }
}