using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public partial class DetailsEvent : System.Web.UI.UserControl, IDataObjectWorker
    {
        private DataObjectEvent dataObjectEvent;
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Mobile");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        public DataObject DataObject { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            lnkBack.Text = Helper.GetObjectName(DataObject.ObjectType, false);
            lnkBack.NavigateUrl = !string.IsNullOrEmpty(Request.QueryString["ReturnURL"]) ? Encoding.ASCII.GetString(Convert.FromBase64String(Request.QueryString["ReturnURL"])) : Helper.GetMobileOverviewLink(DataObject.ObjectType);
            lnkBack.ID = null;
            litBack.Text = Helper.GetObjectName(DataObject.ObjectType, true);

            if ((DataObject.GetUserAccess(UserDataContext.GetUserDataContext()) & ObjectAccessRight.Update) == ObjectAccessRight.Update)
            {
                lnkEdit.Visible = true;
                lnkEdit.NavigateUrl = "/M/Admin/EditEvent.aspx?OID=" + DataObject.ObjectID + "&ReturnUrl=" + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(Request.RawUrl));
            }

            dataObjectEvent = (DataObjectEvent)DataObject;

            PrintEvent();
        }

        private void PrintEvent()
        {
            LitTitle.Text = dataObjectEvent.Title.EscapeForXHTML();

            if (!string.IsNullOrEmpty(dataObjectEvent.Image))
            {
                Img.ImageUrl = _4screen.CSB.Common.SiteConfig.MediaDomainName + dataObjectEvent.GetImage(PictureVersion.S);
            }
            else
            {
                Img.Visible = false;
            }

            DataObjectLocation currentLocation = DataObjects.Load<DataObjectLocation>(new QuickParameters()
                                                                                          {
                                                                                              Udc = UserDataContext.GetUserDataContext(),
                                                                                              DisablePaging = true,
                                                                                              IgnoreCache = true,
                                                                                              RelationParams = new RelationParams()
                                                                                                                   {
                                                                                                                       ChildObjectID = dataObjectEvent.ObjectID
                                                                                                                   }
                                                                                          }).SingleOrDefault();
            if (currentLocation != null)
            {
                LnkLocation.Text = currentLocation.Title;
                LnkLocation.NavigateUrl = Helper.GetMobileDetailLink(currentLocation.ObjectType, currentLocation.ObjectID.ToString());
                LnkLocation.ID = null;
                if (!string.IsNullOrEmpty(currentLocation.Street))
                {
                    LitAddress.Text += currentLocation.Street + "<br/>";
                }
                if (!string.IsNullOrEmpty(currentLocation.City))
                {
                    if (!string.IsNullOrEmpty(currentLocation.Zip))
                        LitAddress.Text += currentLocation.Zip + " " + currentLocation.City + "<br/>";
                    else
                        LitAddress.Text += currentLocation.City + "<br/>";
                }
            }
            if (DataObject.Geo_Lat != Double.MinValue)
            {
                pnlGeoTag.Visible = true;
                pnlGeoTag.ID = null;
                lnkGeoTag.NavigateUrl = "http://maps.google.com/?q=" + DataObject.Geo_Lat + "," + DataObject.Geo_Long + "&z=14";
                lnkGeoTag.ID = null;
            }

            LitWhen.Text = dataObjectEvent.StartDate.ToShortDateString();
            if (dataObjectEvent.EndDate != DateTime.MaxValue && dataObjectEvent.EndDate != dataObjectEvent.StartDate)
            {
                LitWhen.Text += " - " + dataObjectEvent.EndDate.ToShortDateString();
            }
            if (!string.IsNullOrEmpty(dataObjectEvent.Time))
            {
                LitWhen.Text += "<br/>" + dataObjectEvent.Time.EscapeForXHTML();
            }

            if (!string.IsNullOrEmpty(dataObjectEvent.TagList))
            {
                LitType.Text = string.Join(", ", Helper.GetMappedTagWords(dataObjectEvent.TagList).ToArray());
            }

            if (dataObjectEvent.Website != null)
            {
                TrWebsite.Visible = true;
                LnkWebsite.Text = dataObjectEvent.Website.ToString().EscapeForXHTML();
                LnkWebsite.NavigateUrl = dataObjectEvent.Website.ToString();
            }
            LnkWebsite.ID = null;

            if (!string.IsNullOrEmpty(dataObjectEvent.Age))
            {
                TrAge.Visible = true;
                LitAge.Text = dataObjectEvent.Age.EscapeForXHTML();
            }

            if (!string.IsNullOrEmpty(dataObjectEvent.Price))
            {
                TrPrice.Visible = true;
                LitPrice.Text = dataObjectEvent.Price.EscapeForXHTML();
            }

            if (!string.IsNullOrEmpty(dataObjectEvent.Description))
            {
                TrDesc.Visible = true;
                LitDesc.Text = dataObjectEvent.Description.EscapeForXHTML();
            }

            if (!string.IsNullOrEmpty(dataObjectEvent.Content))
            {
                TrDesc.Visible = true;
                LitEvent.Text = dataObjectEvent.Content.EscapeForXHTML();
            }

            TrWebsite.ID = null;
            TrAge.ID = null;
            TrPrice.ID = null;
            LitDesc.ID = null;
        }
    }
}