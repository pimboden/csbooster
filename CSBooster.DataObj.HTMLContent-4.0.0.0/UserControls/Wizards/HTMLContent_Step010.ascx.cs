// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Specialized;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.DataObj.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.DataObj.UserControls.Wizards
{
    public partial class HTMLContent_Step010 : StepsASCX
    {
        private DataObjectHTMLContent htmlContent;

        protected void Page_Load(object sender, EventArgs e)
        {
            htmlContent = DataAccess.Business.DataObject.Load<DataObjectHTMLContent>(ObjectID, null, true);

            if (htmlContent.State == ObjectState.Added)
            {
                htmlContent.ObjectID = ObjectID;
                htmlContent.Title = GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUnnamed");
                htmlContent.CommunityID = CommunityID;
                htmlContent.ShowState = ObjectShowState.Draft;
                htmlContent.StartDate = DateTime.Now;
                htmlContent.EndDate = DateTime.Now.AddYears(10);
                htmlContent.Insert(UserDataContext.GetUserDataContext());
                htmlContent.Title = string.Empty;
            }
            htmlContent.SetValuesFromQuerySting();
            FillEditForm();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            RDPEndDate.SelectedDate = DateTime.Now.AddYears(10);
        }

        private void FillEditForm()
        {
            TxtTitle.Text = htmlContent.Title;
            TxtEditor.ContextMenus.FindByTagName("IMG").Enabled = true;
            TxtEditor.Content = htmlContent.Description;
            RDPStartDate.SelectedDate = htmlContent.StartDate;
            RDPEndDate.SelectedDate = htmlContent.EndDate;
            FillHiddenValues();
        }

        public override bool SaveStep(ref NameValueCollection queryString)
        {
            try
            {
                htmlContent.Title = TxtTitle.Text.StripHTMLTags();
                htmlContent.Description = TxtEditor.Content;
                htmlContent.StartDate = RDPStartDate.SelectedDate.HasValue ? RDPStartDate.SelectedDate.Value : DateTime.Now;
                htmlContent.EndDate = RDPEndDate.SelectedDate.HasValue ? RDPEndDate.SelectedDate.Value : DateTime.Now.AddYears(10);

                _4screen.CSB.DataAccess.Business.Utils.SetPictureRelationsFromContent(TxtEditor.Content, htmlContent, "HtmlContentPictures", true);
                //htmlContent.SetRelationsFromContent(TxtEditor.Content, "Img_", "HTMLContent", true);
                GetHiddenValues();
                htmlContent.Update(UserDataContext.GetUserDataContext());

                return true;
            }
            catch (Exception ex)
            {
                LitMsg.Text = "Fehler beim Speichern: " + ex.Message;
                return false;
            }
        }

        private void GetHiddenValues()
        {
            if (AccessMode == AccessMode.Insert)
                htmlContent.StartDate = DateTime.Now;
            htmlContent.TagList = HFTagWords.Value.StripHTMLTags();
            htmlContent.Status = (ObjectStatus)Enum.Parse(typeof(ObjectStatus), HFStatus.Value);
            htmlContent.ShowState = (ObjectShowState)Enum.Parse(typeof(ObjectShowState), HFShowState.Value);
            htmlContent.Copyright = int.Parse(HFCopyright.Value);
            double geoLat;
            if (double.TryParse(HFGeoLat.Value, out geoLat))
                htmlContent.Geo_Lat = geoLat;
            double geoLong;
            if (double.TryParse(HFGeoLong.Value, out geoLong))
                htmlContent.Geo_Long = geoLong;
            htmlContent.Zip = HFZip.Value;
            htmlContent.City = HFCity.Value;
            htmlContent.Region = HFRegion.Value;
            htmlContent.CountryCode = HFCountry.Value;
        }

        private void FillHiddenValues()
        {
            HFTagWords.Value = htmlContent.TagList.Replace(Constants.TAG_DELIMITER, ',');
            HFStatus.Value = ((int)htmlContent.Status).ToString();
            HFShowState.Value = ((int)htmlContent.ShowState).ToString();
            HFCopyright.Value = htmlContent.Copyright.ToString();
            if (htmlContent.Geo_Lat != double.MinValue && htmlContent.Geo_Long != double.MinValue)
            {
                HFGeoLat.Value = htmlContent.Geo_Lat.ToString();
                HFGeoLong.Value = htmlContent.Geo_Long.ToString();
            }
            HFZip.Value = htmlContent.Zip;
            HFCity.Value = htmlContent.City;
            HFRegion.Value = htmlContent.Region;
            HFCountry.Value = htmlContent.CountryCode;
        }
    }
}