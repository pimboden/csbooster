using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class CommunityMemberOwner : System.Web.UI.Page
    {
        private int intCount = 0;
        private bool isOwner = false;
        private bool isMember = false;
        private Guid currentCommunityId;
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        private GuiLanguage language = GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base");

        protected void Page_Load(object sender, EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Memberships);

            btnCan.OnClientClick = "return CloseWindow()";
        }

        protected override void OnInit(EventArgs e)
        {
            if (Request.QueryString["CN"].IsGuid())
            {
                currentCommunityId = new Guid(Request.QueryString["CN"]);
                if (UserProfile.Current.UserId != Guid.Empty)
                    isOwner = Community.GetIsUserOwner(UserProfile.Current.UserId, currentCommunityId, out isMember);

                if (isOwner)
                {
                    litMsg.Visible = false;
                    LoadMember();
                }
            }
        }

        private void LoadMember()
        {
            List<Member> list = Members.Load(currentCommunityId);
            list = list.OrderBy(x => x.UserName).ToList();
            foreach (Member item in list)
            {
                if (item.IsOwner)
                    intCount++;
            }

            rep.DataSource = list;
            rep.DataBind();
        }

        protected void rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Member item = e.Item.DataItem as Member;

            Literal li = e.Item.FindControl("liNam") as Literal;
            li.Text = item.UserName;

            PlaceHolder ph = e.Item.FindControl("phMem") as PlaceHolder;

            if (!UserDataContext.GetUserDataContext(item.UserName).IsAdmin)
            {
                LinkButton btn = new LinkButton();
                btn.Text = language.GetString("CommandRemoveMember");
                btn.CssClass = "inputButton";
                btn.ID = string.Concat("del", item.UserId.ToString());
                btn.CommandArgument = item.UserId.ToString();
                btn.Click += new EventHandler(OnRemoveMemberClick);
                if (intCount < 2 && item.IsOwner)
                    btn.Enabled = false;
                ph.Controls.Add(btn);

                ph = e.Item.FindControl("phOwn") as PlaceHolder;
                CheckBox cbx = new CheckBox();
                cbx.ID = string.Concat("own", item.UserId.ToString());
                cbx.Text = language.GetString("LableOwner");
                cbx.Checked = item.IsOwner;
                ph.Controls.Add(cbx);
            }
        }

        void OnRemoveMemberClick(object sender, EventArgs e)
        {
            string strCtyID = Request.QueryString["CN"];
            if (!string.IsNullOrEmpty(strCtyID))
            {
                LinkButton btn = sender as LinkButton;
                if (!string.IsNullOrEmpty(btn.CommandArgument))
                    Member.RemoveMember(new Guid(strCtyID), new Guid(btn.CommandArgument));


            }
            LoadMember();
            litMsg.Visible = false;
        }

        protected void OnSaveMembersClick(object sender, EventArgs e)
        {
            if (isOwner)
            {
                litMsg.Visible = false;
                string strCtyID = Request.QueryString["CN"];
                if (!string.IsNullOrEmpty(strCtyID))
                {
                    List<Member> list = Members.Load(new Guid(strCtyID));
                    foreach (Member item in list)
                    {
                        item.IsOwner = false;
                    }

                    foreach (string strKey in Request.Form.AllKeys)
                    {
                        if (!string.IsNullOrEmpty(strKey) && strKey.IndexOf("own") > 0)
                        {
                            foreach (Member item in list)
                            {
                                if (strKey.EndsWith(item.UserId.ToString()))
                                {
                                    item.IsOwner = true;
                                    break;
                                }
                            }
                        }
                    }

                    if (Members.Save(list))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CloseWindow", "$telerik.$(function() { GetRadWindow().Close(); } );", true);
                    }
                    else
                    {
                        litMsg.Text = language.GetString("MessageLastOwner");
                        litMsg.Visible = true;
                    }
                }
            }
        }
    }
}