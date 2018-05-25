// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using SiteConfig = _4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class SmallOutputEvent : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        public DataObject DataObject { get; set; }
        public Dictionary<string, object> Settings { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            DataObjectEvent dataObjectEvent = (DataObjectEvent)DataObject;

            Img.ImageUrl = SiteConfig.MediaDomainName + DataObject.GetImage(PictureVersion.S);
            LnkTitle.Text = DataObject.Title;
            LnkTitle.NavigateUrl = Helper.GetDetailLink(DataObject.ObjectType, DataObject.ObjectID.Value.ToString());
            LnkImg.NavigateUrl = LnkTitle.NavigateUrl;
            LitDate.Text = DataObject.StartDate.ToShortDateString();
            if (DataObject.EndDate.Date != DataObject.StartDate.Date)
                LitDate.Text += " - " + DataObject.EndDate.ToShortDateString();
            if (!string.IsNullOrEmpty(dataObjectEvent.Time))
                LitDate.Text += " / " + dataObjectEvent.Time;
            LitDesc.Text = DataObject.Description.StripHTMLTags().CropString(160);
            LitType.Text = string.Join(", ", Helper.GetMappedTagWords(dataObjectEvent.TagList).ToArray());
            LitPrice.Text = dataObjectEvent.Price;
            Img.ID = null;
            LnkTitle.ID = null;
            LnkImg.ID = null;
        }
    }
}