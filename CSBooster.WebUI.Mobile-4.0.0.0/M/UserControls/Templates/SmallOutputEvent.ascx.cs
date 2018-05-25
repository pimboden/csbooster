// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.DataAccess.Data;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using SiteConfig=_4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.M.UserControls.Templates
{
    public partial class SmallOutputEvent : System.Web.UI.UserControl, IDataObjectWorker
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Mobile");

        public DataObject DataObject { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            DataObjectEvent dataObjectEvent = (DataObjectEvent)DataObject;

            lnkDetail.NavigateUrl = string.Format("/M/ObjectDetail.aspx?OT=Event&OID={0}&LG={1}&ReturnUrl={2}", DataObject.ObjectID, Request.QueryString["LG"] ?? string.Empty, Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(Request.RawUrl)));
            lnkDetail.Target = "_self";
            lnkDetail.ID = null;

            string imageUrl = DataObject.GetImage(PictureVersion.XS, false);
            if (!string.IsNullOrEmpty(imageUrl))
                litImg.Text = string.Format("<img src='{0}{1}' />", SiteConfig.MediaDomainName, imageUrl);

            litDate.Text = DataObject.StartDate.ToShortDateString();
            if (DataObject.EndDate.Date != DataObject.StartDate.Date)
                litDate.Text += " - " + DataObject.EndDate.ToShortDateString();
            if (!string.IsNullOrEmpty(dataObjectEvent.Time))
                litDate.Text += " / " + dataObjectEvent.Time;

            litTitle.Text = _4screen.Utils.Extensions.EscapeForXHTML(DataObject.Title);
            litDesc.Text = _4screen.Utils.Extensions.EscapeForXHTML(DataObject.Description.StripHTMLTags().CropString(150));
        }
    }
}