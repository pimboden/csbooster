// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils;
using _4screen.Utils.Web;
using _4screen.WebControls;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class Sharing : System.Web.UI.UserControl, ISettings
    {
        private DataObject dataObject;
        private bool showSharingButtons;
        private bool showILike;
        private bool showUrl;
        private bool showEmbedCode;
        protected string permaLink = "";
        protected string sendUrl = "";
        protected string popupTitle = "";
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        public Dictionary<string, object> Settings { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            dataObject = (DataObject)Settings["DataObject"];

            InitCtrl();

            string detailLink = Server.UrlEncode(_4screen.CSB.Common.SiteConfig.SiteURL + Helper.GetDetailLink(dataObject.ObjectType, dataObject.ObjectID.ToString()));

            if (showSharingButtons)
            {
                PnlShareBtns.Visible = true;
                string title = Server.UrlEncode(dataObject.Title);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<li><a href=\"http://www.facebook.com/sharer.php?u={0}&t={1}\" target=\"_blank\" class=\"facebook\" title=\"{2}\"></a></li>", detailLink, title, (new TextControl() { LanguageFile = "UserControls.Templates.WebUI.Base", TextKey = "TooltipSharingFacebook" }).Text.EscapeForXml());
                sb.AppendFormat("<li><a href=\"http://twitter.com/home?status={0}%20-%20{1}\" target=\"_blank\" class=\"twitter\" title=\"{2}\"></a></li>", detailLink, title, (new TextControl() { LanguageFile = "UserControls.Templates.WebUI.Base", TextKey = "TooltipSharingTwitter" }).Text.EscapeForXml());
                LitSharing.Text = sb.ToString();
            }

            if (showILike)
            {
                PnlILike.Visible = true;
                ScriptManager.GetCurrent(Page).Scripts.Add(new ScriptReference("http://connect.facebook.net/de_DE/all.js#xfbml=1"));
                LitILike.Text = "<fb:like href=\"" + detailLink + "\" width=\"" + Settings["Width"] + "\"></fb:like>";
            }

            if (!showUrl)
                PnlShareUrl.Visible = false;

            if (showEmbedCode)
                RenderEmbedCode();
            else
                PnlEmbed.Visible = false;

            PnlShareBtns.ID = null;
            PnlILike.ID = null;
            PnlShareUrl.ID = null;
            PnlEmbed.ID = null;
            lBLUrl.ID = null;
        }

        private void InitCtrl()
        {
            if (Settings.ContainsKey("ShowExtSharing"))
                showSharingButtons = (bool)Settings["ShowExtSharing"];

            if (Settings.ContainsKey("ShowILike"))
                showILike = (bool)Settings["ShowILike"];

            if (Settings.ContainsKey("ShowSendUrl"))
                showUrl = (bool)Settings["ShowSendUrl"];

            if (Settings.ContainsKey("ShowEmbedAndCopy"))
                showEmbedCode = (bool)Settings["ShowEmbedAndCopy"];

            permaLink = string.Format("{0}{1}", _4screen.CSB.Common.SiteConfig.SiteURL, Helper.GetDetailLink(dataObject.ObjectType, dataObject.ObjectID.Value.ToString()));

            if (Settings.ContainsKey("UrlSend") && !string.IsNullOrEmpty(Settings["UrlSend"].ToString()))
                sendUrl = System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(Settings["UrlSend"].ToString()));
            else
                sendUrl = System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(Request.RawUrl));

            popupTitle = languageShared.GetString("CommandRecommend").StripForScript();
        }

        private void RenderEmbedCode()
        {
            // load embed code control
            string embedTemplate = "EmbedCode.ascx";
            if (Settings.ContainsKey("EmbedTemplate"))
                embedTemplate = (string)Settings["EmbedTemplate"];

            int embedVideoWidth = 400;
            if (Settings.ContainsKey("EmbedVideoWidth"))
                embedVideoWidth = (int)Settings["EmbedVideoWidth"];

            int embedVideoHeight = 300;
            if (Settings.ContainsKey("EmbedVideoHeight"))
                embedVideoHeight = (int)Settings["EmbedVideoHeight"];

            Control control = LoadControl("~/UserControls/Templates/" + embedTemplate);
            ((IDataObjectWorker)control).DataObject = dataObject;
            ((ISettings)control).Settings = new Dictionary<string, object>();
            ((ISettings)control).Settings.Add("EmbedVideoWidth", embedVideoWidth);
            ((ISettings)control).Settings.Add("EmbedVideoHeight", embedVideoHeight);
            this.PhEmbed.Controls.Add(control);
        }
    }
}
