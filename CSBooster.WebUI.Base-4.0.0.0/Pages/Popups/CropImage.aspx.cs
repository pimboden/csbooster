//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#2.0.0.0		02.09.2008 / AW
//******************************************************************************
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class CropImage : System.Web.UI.Page
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base");
        private string objectId = string.Empty;
        private string actionProfileCropCheckAndArchive = string.Empty;
        private string actionProfileResizeExtraSmall = string.Empty;
        private string actionProfileResizeSmall = string.Empty;
        private string actionProfileResizeMedium = string.Empty;
        private string actionProfileResizeLarge = string.Empty;
        private string actionProfileCrop = string.Empty;
        private DataObject dataObject;
        private string archivedFilename;

        protected void Page_Load(object sender, EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.WizardSpezial);

            objectId = Request.QueryString["OID"];

            dataObject = DataObject.Load<DataObject>(objectId.ToGuid(), null, false);

            string imageUrl = dataObject.GetImage(PictureVersion.A);
            archivedFilename = System.Configuration.ConfigurationManager.AppSettings["ConverterRootPathMedia"] + imageUrl.Replace("/", "\\");
            imageUrl = _4screen.CSB.Common.SiteConfig.MediaDomainName + imageUrl;
            Helper.SetActions(Helper.GetPictureFormatFromFilename(archivedFilename), dataObject.ObjectType, out actionProfileCropCheckAndArchive, out actionProfileResizeLarge, out actionProfileResizeMedium, out actionProfileResizeSmall, out actionProfileResizeExtraSmall, out actionProfileCrop, new Dictionary<PictureVersion, PictureFormat>());

            if (!IsPostBack)
            {
                _4screen.CSB.ImageHandler.Business.ImageHandler imageHandler = new _4screen.CSB.ImageHandler.Business.ImageHandler(_4screen.CSB.Common.SiteConfig.MediaDomainName, ConfigurationManager.AppSettings["ConverterRootPath"], dataObject.UserID.Value.ToString(), objectId, true, Server.MapPath("/Configurations"));
                imageHandler.DoConvert(archivedFilename, actionProfileCropCheckAndArchive, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);

                int originalImageHeight = imageHandler.ImageInfo.Height;
                int originalImageWidth = imageHandler.ImageInfo.Width;
                int maxVisualWidthOrHeight = imageHandler.GetMaxVisualWidthOrHeight(actionProfileCropCheckAndArchive);

                ViewState["ImageWidth"] = originalImageWidth;
                ViewState["ImageHeight"] = originalImageHeight;
                ViewState["MaxSize"] = maxVisualWidthOrHeight;

                if (originalImageHeight > originalImageWidth && originalImageHeight > maxVisualWidthOrHeight)
                {
                    Crop.CroppedImageHeight = maxVisualWidthOrHeight;
                    Crop.CroppedImageWidth = (int)((float)maxVisualWidthOrHeight * (float)originalImageWidth / (float)originalImageHeight);
                }
                else if (originalImageHeight < originalImageWidth && originalImageWidth > maxVisualWidthOrHeight)
                {
                    Crop.CroppedImageHeight = (int)((float)maxVisualWidthOrHeight * (float)originalImageHeight / (float)originalImageWidth);
                    Crop.CroppedImageWidth = maxVisualWidthOrHeight;
                }
                else if (originalImageWidth > maxVisualWidthOrHeight)
                {
                    Crop.CroppedImageHeight = maxVisualWidthOrHeight;
                    Crop.CroppedImageWidth = maxVisualWidthOrHeight;
                }
                else
                {
                    Crop.CroppedImageHeight = originalImageHeight;
                    Crop.CroppedImageWidth = originalImageWidth;
                }
                Crop.AllowQualityLoss = true;

                FileStream stream = File.Open(archivedFilename, FileMode.Open);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Dispose();
                Crop.SourceImage = buffer;
            }
        }

        protected void OnCropClick(object sender, EventArgs e)
        {
            _4screen.CSB.ImageHandler.Business.ImageHandler imageHandler = new _4screen.CSB.ImageHandler.Business.ImageHandler(_4screen.CSB.Common.SiteConfig.MediaDomainName, ConfigurationManager.AppSettings["ConverterRootPath"], dataObject.UserID.Value.ToString(), objectId, false, Server.MapPath("/Configurations"));

            int imageWidth = int.Parse(ViewState["ImageWidth"].ToString());
            int imageHeight = int.Parse(ViewState["ImageHeight"].ToString());
            float maxVisualWidthOrHeight = float.Parse(ViewState["MaxSize"].ToString());
            float scaleFactor = 1f;
            if (imageWidth > imageHeight && imageWidth > maxVisualWidthOrHeight)
                scaleFactor = (float)imageWidth / maxVisualWidthOrHeight;
            else if (imageHeight >= imageWidth && imageHeight > maxVisualWidthOrHeight)
                scaleFactor = (float)imageHeight / maxVisualWidthOrHeight;
            string[] cropCoords = Request.Form[this.Crop.UniqueID + "$cords"].Split(';');

            imageHandler.SetCropLeft = Convert.ToInt32(cropCoords[0]);
            imageHandler.SetCropTop = Convert.ToInt32(cropCoords[1]);
            imageHandler.SetCropWidth = Convert.ToInt32(cropCoords[2]) + Convert.ToInt32(cropCoords[0]);
            imageHandler.SetCropHeight = Convert.ToInt32(cropCoords[3]) + Convert.ToInt32(cropCoords[1]);
            imageHandler.VisualCropFactor = scaleFactor;

            imageHandler.DoConvert(archivedFilename, actionProfileCrop, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);

            string croppedFilname = imageHandler.TargetImage;
            imageHandler = new _4screen.CSB.ImageHandler.Business.ImageHandler(_4screen.CSB.Common.SiteConfig.MediaDomainName, ConfigurationManager.AppSettings["ConverterRootPath"], dataObject.UserID.Value.ToString(), string.Empty, false, Server.MapPath("/Configurations"));
            imageHandler.DoConvert(croppedFilname, actionProfileResizeExtraSmall, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
            imageHandler.DoConvert(croppedFilname, actionProfileResizeSmall, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
            if (actionProfileResizeMedium != null)
                imageHandler.DoConvert(croppedFilname, actionProfileResizeMedium, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);

            if (!string.IsNullOrEmpty(Request.QueryString["ParentRadWin"]))
                LitScript.Text = string.Format("<script language=\"javascript\">$telerik.$(function() {{ RefreshRadWindow('{0}');CloseWindow(); }} );</script>", Request.QueryString["ParentRadWin"]);
            else if (!string.IsNullOrEmpty(Request.QueryString["ReturnURL"]))
                LitScript.Text = string.Format("<script language=\"javascript\">$telerik.$(function() {{ RedirectParentPage('{0}');CloseWindow(); }} );</script>", Request.QueryString["ReturnURL"]);
            else
                LitScript.Text = string.Format("<script language=\"javascript\">$telerik.$(function() {{ RefreshParentPage();CloseWindow(); }} );</script>");
        }
    }
}