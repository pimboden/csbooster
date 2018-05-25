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
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class MessageGroup : System.Web.UI.UserControl
    {
        private string strSelectColor = "";
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");

        public string SelectColor
        {
            get { return strSelectColor; }
            set { strSelectColor = value; }
        }

        private MessageGroupTypes enuGroupType;

        public MessageGroupTypes GroupType
        {
            get { return enuGroupType; }
            set { enuGroupType = value; }
        }

        public string UserID
        {
            get { return UserProfile.Current.UserId.ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            FillTable();
        }

        private void FillTable()
        {
            this.btnAdd.ToolTip = language.GetString("TooltipMessageGroupAdd");
            this.txtAdd.Text = language.GetString("LableMessageGroupName");    
            int intGroupID = 0;
            if (Request.QueryString["grpid"] != null)
                intGroupID = int.Parse(Request.QueryString["grpid"]);

            string strTarget;
            if (this.GroupType == MessageGroupTypes.Inbox)
                strTarget = "MyMessagesInbox.aspx";
            else
                strTarget = "MyMessagesOutbox.aspx";

            pnlGrp.Controls.Clear();

            List<DataAccess.Business.MessageGroup> list = DataAccess.Business.MessageGroup.Load(this.UserID, this.GroupType);
            pnlGrp.Controls.Add(new LiteralControl("<table width='100%' cellpadding='0' cellspacing='0' border='0'>"));
            foreach (DataAccess.Business.MessageGroup item in list)
            {
                string strBG = string.Empty;
                /*if (item.GroupID == intGroupID)
               strBG = string.Format("bgcolor='{0}'", this.strSelectColor);*/

                pnlGrp.Controls.Add(new LiteralControl("<tr>"));
                if (item.GroupID > 0)
                {
                    int UnreadCount =  Messages.GetInboxUnreadCount(UserProfile.Current.UserId, item.GroupID);
                    string NewInfo = UnreadCount > 0 ? string.Format("&nbsp;({0})",UnreadCount) : string.Empty;
                    pnlGrp.Controls.Add(new LiteralControl(string.Format("<td {3} class=\"\"><a class=\"\" href='/UserControls/Dashboard/{0}?grpid={1}'>{2}{4}</a></td>", strTarget, item.GroupID, item.Title, strBG, NewInfo)));

                    ImageButton btnDel = new ImageButton();
                    btnDel.ImageUrl = string.Format("/Library/Images/Layout/cmd_delete.png");
                    btnDel.CommandArgument = item.GroupID.ToString();
                    btnDel.ToolTip = language.GetString("TooltipMessageGroupDelGroup");
                    btnDel.Click += new ImageClickEventHandler(btnDel_Click);

                    pnlGrp.Controls.Add(new LiteralControl(string.Format("<td {0} align=\"right\">", strBG)));
                    pnlGrp.Controls.Add(btnDel);
                    pnlGrp.Controls.Add(new LiteralControl("</td>"));
                }
                else
                {
                    pnlGrp.Controls.Add(new LiteralControl(string.Format("<td {3} class=\"\" width='100%' colspan='2'><a class=\"\" href='/UserControls/Dashboard/{0}?grpid={1}'>{2}</a></td>", strTarget, item.GroupID, item.Title, strBG)));
                }

                pnlGrp.Controls.Add(new LiteralControl("</tr>"));
            }

            pnlGrp.Controls.Add(new LiteralControl("</table>"));
        }

        void btnDel_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            if (btn != null)
            {
                DataAccess.Business.MessageGroup.Delete(this.UserID, this.GroupType, int.Parse(btn.CommandArgument));
                FillTable();
                ((IReloadable)this.Page).Reload();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtAdd.Text.Trim().Length > 0)
            {
                if (!DataAccess.Business.MessageGroup.Exist(this.UserID, this.GroupType, txtAdd.Text.Trim()))
                {
                    DataAccess.Business.MessageGroup.Add(this.UserID, this.GroupType, txtAdd.Text.Trim());
                }
                txtAdd.Text = string.Empty; 
            }

            FillTable();
            ((IReloadable)this.Page).Reload();
        }
    }
}