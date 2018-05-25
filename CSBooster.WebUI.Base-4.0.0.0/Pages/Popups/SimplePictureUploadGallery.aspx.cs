using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class SimplePictureUploadGallery : System.Web.UI.Page
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        private string folderId = string.Empty;
        private string actionProfileResizeLarge = "BgLargeResizedJpg";
        private string actionProfileResizeExtraSmall = "BgSmallResizedJpg";

        protected string Callback { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Callback = Request.QueryString["Callback"];

            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.WizardSpezial);

            if (!string.IsNullOrEmpty(Request.QueryString["CN"]))
                folderId = Request.QueryString["CN"];
            else if (!string.IsNullOrEmpty(Request.QueryString["UI"]))
                folderId = Request.QueryString["UI"];
            else if (!string.IsNullOrEmpty(Request.QueryString["ParentId"]))
                folderId = Request.QueryString["ParentId"];

            if (RadUpload.UploadedFiles.Count > 0)
            {
                _4screen.CSB.ImageHandler.Business.ImageHandler imageHandler = new _4screen.CSB.ImageHandler.Business.ImageHandler(_4screen.CSB.Common.SiteConfig.MediaDomainName, ConfigurationManager.AppSettings["ConverterRootPath"], folderId, null, true, Server.MapPath("/Configurations"));

                Telerik.Web.UI.UploadedFile uploadedFile = RadUpload.UploadedFiles[0];
                string originalExtension = uploadedFile.GetName().Substring(uploadedFile.GetName().LastIndexOf('.'));
                string archiveFolder = string.Format(@"{0}\{1}\BG\{2}", System.Configuration.ConfigurationManager.AppSettings["ConverterRootPathMedia"], folderId, PictureVersion.A);
                if (!Directory.Exists(archiveFolder))
                    Directory.CreateDirectory(archiveFolder);
                string archiveFilename = string.Format(@"{0}\{1}{2}", archiveFolder, imageHandler.ImageName, originalExtension);
                uploadedFile.SaveAs(archiveFilename);

                imageHandler.DoConvert(archiveFilename, actionProfileResizeLarge, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                imageHandler.DoConvert(archiveFilename, actionProfileResizeExtraSmall, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
            }

            LoadPictures();
        }

        private void LoadPictures()
        {
            string smallFolder = string.Format(@"{0}\{1}\BG\{2}", System.Configuration.ConfigurationManager.AppSettings["ConverterRootPathMedia"], folderId, PictureVersion.S);
            if (!Directory.Exists(smallFolder))
                Directory.CreateDirectory(smallFolder);

            string[] smallPictures = Directory.GetFiles(smallFolder);
            List<Image> images = new List<Image>();
            foreach (string smallPicture in smallPictures)
            {
                Image image = new Image();
                string smallPictureUrl = smallPicture.Replace(System.Configuration.ConfigurationManager.AppSettings["ConverterRootPathMedia"], "").Replace("\\", "/");
                image.ImageUrl = _4screen.CSB.Common.SiteConfig.MediaDomainName + smallPictureUrl;
                if (string.IsNullOrEmpty(Request.QueryString["Callback"]))
                    image.Attributes.Add("OnClick", string.Format("CloseWindow('{0}')", image.ImageUrl));
                else
                    image.Attributes.Add("OnClick", string.Format("CloseAndSetPicture('{0}')", image.ImageUrl));
                images.Add(image);
            }

            this.DLObjects.DataSource = images;
            this.DLObjects.DataBind();
        }

        protected void OnObjectsItemDataBound(object sender, DataListItemEventArgs e)
        {
            Image image = (Image)e.Item.DataItem;

            e.Item.FindControl("LnkImg").Controls.Add(image);

            LinkButton deleteButton = (LinkButton)e.Item.FindControl("LbtnDelete");
            string imageId = image.ImageUrl.Substring(image.ImageUrl.LastIndexOf('/') + 1, image.ImageUrl.LastIndexOf('.') - image.ImageUrl.LastIndexOf('/') - 1);
            deleteButton.CommandArgument = imageId;
        }

        protected void OnDeleteClick(object sender, EventArgs e)
        {
            string bgFolder = string.Format(@"{0}\{1}\BG", System.Configuration.ConfigurationManager.AppSettings["ConverterRootPathMedia"], folderId);

            string smallPicture = string.Format(@"{0}\{1}\{2}.jpg", bgFolder, PictureVersion.S, ((LinkButton)sender).CommandArgument);
            string largePicture = string.Format(@"{0}\{1}\{2}.jpg", bgFolder, PictureVersion.L, ((LinkButton)sender).CommandArgument);
            string archivedPicture = string.Format(@"{0}\{1}\{2}.jpg", bgFolder, PictureVersion.A, ((LinkButton)sender).CommandArgument);

            File.Delete(smallPicture);
            File.Delete(largePicture);
            File.Delete(archivedPicture);

            LoadPictures();
        }
    }
}