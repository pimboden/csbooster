using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.M.UserControls.Templates
{
    public partial class DetailsLocation : System.Web.UI.UserControl, IDataObjectWorker
    {
        private DataObjectLocation location;
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Mobile");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        public DataObject DataObject { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            lnkBack.Text = Helper.GetObjectName(DataObject.ObjectType, false);
            lnkBack.NavigateUrl = Helper.GetMobileOverviewLink(DataObject.ObjectType);
            lnkBack.ID = null;
            litBack.Text = Helper.GetObjectName(DataObject.ObjectType, true);

            if ((DataObject.GetUserAccess(UserDataContext.GetUserDataContext()) & ObjectAccessRight.Update) == ObjectAccessRight.Update)
            {
                lnkEdit.Visible = true;
                lnkEdit.NavigateUrl = "/M/Admin/EditLocation.aspx?OID=" + DataObject.ObjectID + "&ReturnUrl=" + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(Request.RawUrl));
            }

            location = (DataObjectLocation)DataObject;

            PrintLocation();
        }

        private void PrintLocation()
        {
            LitTitle.Text = location.Title.EscapeForXHTML();

            if (!string.IsNullOrEmpty(location.Image))
            {
                Img.ImageUrl = _4screen.CSB.Common.SiteConfig.MediaDomainName + location.GetImage(PictureVersion.S);
            }
            else
            {
                Img.Visible = false;
            }

            LitAddress.Text += location.Title.EscapeForXHTML() + "<br/>";
            if (!string.IsNullOrEmpty(location.Street))
            {
                LitAddress.Text += location.Street.EscapeForXHTML() + "<br/>";
            }
            if (!string.IsNullOrEmpty(location.City))
            {
                if (!string.IsNullOrEmpty(location.Zip))
                    LitAddress.Text += location.Zip.EscapeForXHTML() + " " + location.City.EscapeForXHTML() + "<br/>";
                else
                    LitAddress.Text += location.City.EscapeForXHTML() + "<br/>";
            }
            if (DataObject.Geo_Lat != Double.MinValue)
            {
                pnlGeoTag.Visible = true;
                pnlGeoTag.ID = null;
                lnkGeoTag.NavigateUrl = "http://maps.google.com/?q=" + DataObject.Geo_Lat + "," + DataObject.Geo_Long + "&z=14";
                lnkGeoTag.ID = null;
            }

            if (!string.IsNullOrEmpty(location.TagList))
            {
                LitType.Text = string.Join(", ", Helper.GetMappedTagWords(location.TagList).ToArray());
            }

            if (location.Website != null)
            {
                TrWebsite.Visible = true;
                LnkWebsite.Text = location.Website.ToString().EscapeForXHTML();
                LnkWebsite.NavigateUrl = location.Website.ToString();
            }
            LnkWebsite.ID = null;

            if (!string.IsNullOrEmpty(location.Description))
            {
                TrDesc.Visible = true;
                LitDesc.Text = location.Description.EscapeForXHTML();
            }

            TrWebsite.ID = null;
            LitDesc.ID = null;
        }
    }
}