// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Linq;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using SiteConfig=_4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.M.UserControls.Templates
{
    public partial class DetailsObject : System.Web.UI.UserControl, IDataObjectWorker
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Mobile");

        public DataObject DataObject { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            lnkBack.Text = Helper.GetObjectName(DataObject.ObjectType, false);
            lnkBack.NavigateUrl = Helper.GetMobileOverviewLink(DataObject.ObjectType);
            lnkBack.ID = null;
            litBack.Text = Helper.GetObjectName(DataObject.ObjectType, true);

            string imageUrl = DataObject.GetImage(PictureVersion.M, false);
            if (!string.IsNullOrEmpty(imageUrl))
                litImg.Text = string.Format("<img src='{0}{1}' />", SiteConfig.MediaDomainName, imageUrl);
            litTitle.Text = _4screen.Utils.Extensions.EscapeForXHTML(DataObject.Title);

            var copyrightConfig = Helper.LoadConfig("Copyrights.config", string.Format("{0}/Configurations/Copyrights.config", WebRootPath.Instance.ToString()));
            string copyrightText = (from copyright in copyrightConfig.Element("Copyrights").Elements("Copyright") where int.Parse(copyright.Attribute("Value").Value) == DataObject.Copyright select copyright.Attribute("Name").Value).Single();
            litCopyright.Text = copyrightText;

            litDesc.Text = _4screen.Utils.Extensions.EscapeForXHTML(DataObject.Description);

            if (DataObject.Geo_Lat != Double.MinValue)
            {
                pnlGeoTag.Visible = true;
                pnlGeoTag.ID = null;
                lnkGeoTag.NavigateUrl = "http://maps.google.com/?q=" + DataObject.Geo_Lat + "," + DataObject.Geo_Long + "&z=14";
                lnkGeoTag.ID = null;
            }
        }
    }
}