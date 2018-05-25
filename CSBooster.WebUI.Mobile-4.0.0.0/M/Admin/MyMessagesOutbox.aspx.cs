// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.M.Admin
{
    public partial class MyMessagesOutbox : System.Web.UI.Page, IReloadable, IBrowsable
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Mobile");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        protected void Page_Load(object sender, EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Messaging);
        }

        public void Reload()
        {
            this.sent.Reload();
        }

        public int GetNumberItems()
        {
            return this.sent.GetNumberItems();
        }

        public void SetCurrentPage(int currentPage)
        {
            this.sent.SetCurrentPage(currentPage);
        }
    }
}