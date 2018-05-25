// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class UserShortInfo : System.Web.UI.UserControl, IDataObjectWorker
    {
        private DataObjectUser user;
        private GuiLanguage languageProfile = GuiLanguage.GetGuiLanguage("ProfileData");
        private GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        public DataObject DataObject { get; set; }
        public string FolderParams { get; set; }
        public QuickParameters QuickParameters { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            user = (DataObjectUser)DataObject;

            SUA.DataObjectUser = user;
            SUA.LinkActive = true;

            PrintObjectLinks();
            PrintUserDetails();
        }

        private void PrintObjectLinks()
        {
            StringBuilder sb = new StringBuilder();

            List<_4screen.CSB.DataAccess.Business.InfoObject> liInfo = InfoObjects.LoadForUser(UserDataContext.GetUserDataContext(), user.UserID, null);
            foreach (DataAccess.Business.InfoObject item in liInfo)
            {
                if (Helper.IsObjectTypeEnabled(item.ObjectType) && Helper.GetObjectType(item.ObjectType).HasOverview)
                    sb.AppendFormat("<div><a href=\"{0}&XUI={1}\">{2} ({3})</a></div>", Helper.GetOverviewLink(item.ObjectType, false), user.UserID, Helper.GetObjectName(item.ObjectType, false), item.Count);
            }

            LitObjects.Text = sb.ToString();
        }

        public void PrintUserDetails()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div style=\"margin-bottom:5px;\">{0}: {1}</div>", languageProfile.GetString("LableInfoMemberSince"), user.Inserted.ToShortDateString());
            sb.AppendFormat("<div style=\"margin-bottom:5px;\">{0}: {1}</div>", languageProfile.GetString("LabelInfoVisits"), user.ViewCount);

            LitInfo.Text = sb.ToString();

            sb = new StringBuilder();
            if (Request.IsAuthenticated)
            {
                sb.AppendFormat("<a class=\"inputButton\" href=\"javascript:radWinOpen('/Pages/Popups/MessageSend.aspx?MsgType=msg&recid={0}', '{1}', 510, 490);\">{2}</a>", user.UserID, languageShared.GetString("CommandSendMessage").StripForScript(), languageShared.GetString("CommandSendMessage"));
            }
            LitInfo2.Text = sb.ToString();
        }
    }
}