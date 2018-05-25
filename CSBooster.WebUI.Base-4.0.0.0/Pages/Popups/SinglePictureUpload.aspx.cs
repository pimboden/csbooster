//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		29.08.2007 / PI
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.WebUI.UserControls.Wizards;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class SinglePictureUpload : System.Web.UI.Page
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base");
        private Guid objectId = Guid.Empty;
        private string actionProfileCropCheckAndArchive = string.Empty;
        private string actionProfileResizeExtraSmall = string.Empty;
        private string actionProfileResizeSmall = string.Empty;
        private string actionProfileResizeMedium = string.Empty;
        private string actionProfileResizeLarge = string.Empty;
        private string actionProfileCrop = string.Empty;
        private Dictionary<PictureVersion, PictureFormat> imageTypes = new Dictionary<PictureVersion, PictureFormat>();
        private DataObject dataObject;
        private DataObject template;

        protected void Page_Load(object sender, EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.WizardSpezial);

            objectId = Request.QueryString["OID"].ToGuid();

            this.Page.Header.DataBind();

            dataObject = DataObject.Load<DataObject>(objectId, null, false);
            if (dataObject.State == ObjectState.Added)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["TemplateID"]))
                {
                    Guid templateId = Request.QueryString["TemplateID"].ToGuid();
                    template = DataObject.Load<DataObject>(templateId, null, false); ;

                    dataObject = new DataObjectPicture(UserDataContext.GetUserDataContext());

                    dataObject.ObjectID = objectId;
                    dataObject.Title = GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUnnamed");

                    dataObject.CommunityID = template.CommunityID;
                    dataObject.Status = template.Status;
                    dataObject.ShowState = ObjectShowState.InProgress;
                    dataObject.Copyright = template.Copyright;
                    dataObject.Geo_Lat = template.Geo_Lat;
                    dataObject.Geo_Long = template.Geo_Long;
                    dataObject.Zip = template.Zip;
                    dataObject.City = template.City;
                    dataObject.Street = template.Street;
                    dataObject.CountryCode = template.CountryCode;
                    dataObject.Insert(UserDataContext.GetUserDataContext());
                }
            }

            Tagging.ObjectID = dataObject.ObjectID;
            Tagging.ObjectType = dataObject.ObjectType;
            Tagging.OverrideTagHandlerControl = "/UserControls/ObjectTagHandlerSimple.ascx";
            Tagging.Visible = !string.IsNullOrEmpty(Request.QueryString["TemplateID"]);

            if (!string.IsNullOrEmpty(Request.QueryString["ReturnURL"]))
            {
                lbtnCancel0.OnClientClick = string.Format("RedirectParentPage('{0}');CloseWindow()", Encoding.ASCII.GetString(Convert.FromBase64String(Request.QueryString["ReturnURL"])));
                lbtnCancel.OnClientClick = string.Format("RedirectParentPage('{0}');CloseWindow()", Encoding.ASCII.GetString(Convert.FromBase64String(Request.QueryString["ReturnURL"])));
                lbtnClose.OnClientClick = string.Format("RedirectParentPage('{0}');CloseWindow()", Encoding.ASCII.GetString(Convert.FromBase64String(Request.QueryString["ReturnURL"])));
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("function ShowUploadingPanel(){\n");
            sb.AppendFormat("document.getElementById('{0}').style.visibility='visible';\n", (object)pnlUploading.ClientID);
            sb.AppendFormat("document.getElementById('{0}').style.display='inline';\n", (object)pnlUploading.ClientID);
            sb.AppendFormat("document.getElementById('{0}').style.visibility='hidden';\n", (object)pnlUpload.ClientID);
            sb.AppendFormat("document.getElementById('{0}').style.display='none';\n", (object)pnlUpload.ClientID);
            sb.AppendFormat("__doPostBack('{0}','');\n", (object)buttonSubmit.UniqueID);
            sb.Append("return false;\n");
            sb.Append("}\n");
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowUploadingPanel", sb.ToString(), true);

            string uploadFolder = string.Format(@"{0}\{1}\P", System.Configuration.ConfigurationManager.AppSettings["ConverterRootPathUpload"], objectId);
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);
            RadUpload1.TargetPhysicalFolder = uploadFolder;
        }

        protected void buttonSubmit_Click(object sender, System.EventArgs e)
        {
            BindValidResults();
            BindInvalidResults();
        }

        private void BindValidResults()
        {
            _4screen.CSB.ImageHandler.Business.ImageHandler imageHandler = new _4screen.CSB.ImageHandler.Business.ImageHandler(_4screen.CSB.Common.SiteConfig.MediaDomainName, ConfigurationManager.AppSettings["ConverterRootPath"], dataObject.UserID.ToString(), objectId.ToString(), true, Server.MapPath("/Configurations"));

            if (RadUpload1.UploadedFiles.Count > 0)
            {
                pnlUploadFinish.Visible = false;
                Telerik.Web.UI.UploadedFile uploadedFile = RadUpload1.UploadedFiles[0];
                if (!string.IsNullOrEmpty(Request.QueryString["TemplateID"]))
                {
                    Guid templateId = Request.QueryString["TemplateID"].ToGuid();
                    template = DataObject.Load<DataObject>(templateId, null, false);
                }
                if (template != null && template.State != ObjectState.Added)
                    Helper.SetActions(Helper.GetPictureFormatFromFilename(uploadedFile.GetName()), template.ObjectType, out actionProfileCropCheckAndArchive, out actionProfileResizeLarge, out actionProfileResizeMedium, out actionProfileResizeSmall, out actionProfileResizeExtraSmall, out actionProfileCrop, imageTypes);
                else
                    Helper.SetActions(Helper.GetPictureFormatFromFilename(uploadedFile.GetName()), dataObject.ObjectType, out actionProfileCropCheckAndArchive, out actionProfileResizeLarge, out actionProfileResizeMedium, out actionProfileResizeSmall, out actionProfileResizeExtraSmall, out actionProfileCrop, imageTypes);
                string originalExtension = uploadedFile.GetName().Substring(uploadedFile.GetName().LastIndexOf('.'));
                string archivedFilename = System.IO.Path.Combine(imageHandler.GetPhysicalImagePath(actionProfileCropCheckAndArchive), imageHandler.ImageName + originalExtension);
                uploadedFile.SaveAs(archivedFilename, true);

                imageHandler.DoConvert(archivedFilename, actionProfileCropCheckAndArchive, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                archivedFilename = imageHandler.TargetImage;
                int originalImageHeight = imageHandler.ImageInfo.Height;
                int originalImageWidth = imageHandler.ImageInfo.Width;
                int cropLimitHeight = imageHandler.OriginalCropHeight(actionProfileCropCheckAndArchive);
                int cropLimitWidth = imageHandler.OriginalCropWidth(actionProfileCropCheckAndArchive);
                int maxVisualWidthOrHeight = imageHandler.GetMaxVisualWidthOrHeight(actionProfileCropCheckAndArchive);

                if (originalImageHeight < cropLimitHeight && originalImageWidth < cropLimitWidth) // No need for cropping
                {
                    imageHandler.DoConvert(archivedFilename, actionProfileResizeExtraSmall, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    imageHandler.DoConvert(archivedFilename, actionProfileResizeSmall, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    if (actionProfileResizeMedium != null)
                        imageHandler.DoConvert(archivedFilename, actionProfileResizeMedium, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    imageHandler.DoConvert(archivedFilename, actionProfileResizeLarge, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    SaveDataObject(imageHandler.ImageName);

                    if (string.IsNullOrEmpty(Request.QueryString["ReturnURL"]))
                        litScript.Text = "<script language=\"javascript\">$telerik.$(function() { RefreshParentPage(true); CloseWindow(); } );</script>";
                    else
                        litScript.Text = string.Format("<script language=\"javascript\">$telerik.$(function() {{ RedirectParentPage('{0}'); CloseWindow(); }} );</script>", Encoding.ASCII.GetString(Convert.FromBase64String(Request.QueryString["ReturnURL"])));
                }
                else
                {
                    ViewState["sourceImg"] = archivedFilename;
                    ViewState["ImageWidth"] = originalImageWidth;
                    ViewState["ImageHeight"] = originalImageHeight;
                    ViewState["MaxSize"] = maxVisualWidthOrHeight;

                    pnlCroping.Visible = true;
                    pnlUploadFinish.Visible = false;
                    pnlUpload.Visible = false;
                    if (!string.IsNullOrEmpty(Request.QueryString["TemplateID"]))
                    {
                        this.PnlTitle.Visible = true;
                    }

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
        }

        private void BindInvalidResults()
        {
            if (RadUpload1.InvalidFiles.Count > 0)
            {
                repeaterInvalidResults.Visible = true;
                repeaterInvalidResults.DataSource = RadUpload1.InvalidFiles;
                repeaterInvalidResults.DataBind();
                lblFinish.Visible = false;
            }
            else
            {
                repeaterInvalidResults.Visible = false;
            }
        }

        protected void OnSaveClick(object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;
            UserDataContext udc = UserDataContext.GetUserDataContext();
            if (ViewState["sourceImg"] != null && ViewState["sourceImg"].ToString().Length > 0)
            {
                string archivedFilename = ViewState["sourceImg"].ToString();

                if (!string.IsNullOrEmpty(Request.QueryString["TemplateID"]))
                {
                    Guid templateId = Request.QueryString["TemplateID"].ToGuid();
                    template = DataObject.Load<DataObject>(templateId, null, false);
                }
                if (template != null && template.State != ObjectState.Added)
                    Helper.SetActions(Helper.GetPictureFormatFromFilename(archivedFilename), template.ObjectType, out actionProfileCropCheckAndArchive, out actionProfileResizeLarge, out actionProfileResizeMedium, out actionProfileResizeSmall, out actionProfileResizeExtraSmall, out actionProfileCrop, imageTypes);
                else
                    Helper.SetActions(Helper.GetPictureFormatFromFilename(archivedFilename), dataObject.ObjectType, out actionProfileCropCheckAndArchive, out actionProfileResizeLarge, out actionProfileResizeMedium, out actionProfileResizeSmall, out actionProfileResizeExtraSmall, out actionProfileCrop, imageTypes);


                int imageWidth = int.Parse(ViewState["ImageWidth"].ToString());
                int imageHeight = int.Parse(ViewState["ImageHeight"].ToString());
                float maxVisualWidthOrHeight = float.Parse(ViewState["MaxSize"].ToString());
                float scaleFactor = 1f;
                if (imageWidth > imageHeight && imageWidth > maxVisualWidthOrHeight)
                    scaleFactor = (float)imageWidth / maxVisualWidthOrHeight;
                else if (imageHeight >= imageWidth && imageHeight > maxVisualWidthOrHeight)
                    scaleFactor = (float)imageHeight / maxVisualWidthOrHeight;
                string[] cropCoords = Request.Form[this.Crop.UniqueID + "$cords"].Split(';');

                if (cropCoords.Length == 4)
                {
                    _4screen.CSB.ImageHandler.Business.ImageHandler imageHandler = new _4screen.CSB.ImageHandler.Business.ImageHandler(_4screen.CSB.Common.SiteConfig.MediaDomainName, ConfigurationManager.AppSettings["ConverterRootPath"], dataObject.UserID.ToString(), string.Empty, false, Server.MapPath("/Configurations"));
                    imageHandler.SetCropLeft = Convert.ToInt32(cropCoords[0]);
                    imageHandler.SetCropTop = Convert.ToInt32(cropCoords[1]);
                    imageHandler.SetCropWidth = Convert.ToInt32(cropCoords[2]) + Convert.ToInt32(cropCoords[0]);
                    imageHandler.SetCropHeight = Convert.ToInt32(cropCoords[3]) + Convert.ToInt32(cropCoords[1]);
                    imageHandler.VisualCropFactor = scaleFactor;
                    imageHandler.DoConvert(archivedFilename, actionProfileCrop, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    string croppedFilname = imageHandler.TargetImage;
                    imageHandler = new _4screen.CSB.ImageHandler.Business.ImageHandler(_4screen.CSB.Common.SiteConfig.MediaDomainName, ConfigurationManager.AppSettings["ConverterRootPath"], dataObject.UserID.ToString(), string.Empty, false, Server.MapPath("/Configurations"));
                    imageHandler.DoConvert(croppedFilname, actionProfileResizeExtraSmall, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    imageHandler.DoConvert(croppedFilname, actionProfileResizeSmall, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    if (actionProfileResizeMedium != null)
                        imageHandler.DoConvert(croppedFilname, actionProfileResizeMedium, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    SaveDataObject(imageHandler.ImageName);
                }
                else
                {
                    _4screen.CSB.ImageHandler.Business.ImageHandler imageHandler = new _4screen.CSB.ImageHandler.Business.ImageHandler(_4screen.CSB.Common.SiteConfig.MediaDomainName, ConfigurationManager.AppSettings["ConverterRootPath"], dataObject.UserID.ToString(), string.Empty, false, Server.MapPath("/Configurations"));
                    imageHandler.DoConvert(archivedFilename, actionProfileResizeExtraSmall, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    imageHandler.DoConvert(archivedFilename, actionProfileResizeSmall, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    if (actionProfileResizeMedium != null)
                        imageHandler.DoConvert(archivedFilename, actionProfileResizeMedium, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    imageHandler.DoConvert(archivedFilename, actionProfileResizeLarge, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    SaveDataObject(imageHandler.ImageName);
                }

                if (!string.IsNullOrEmpty(Request.QueryString["ParentRadWin"]))
                    litScript.Text = string.Format("<script language=\"javascript\">$telerik.$(function() {{ RefreshRadWindow('{0}');CloseWindow(); }} );</script>", Request.QueryString["ParentRadWin"]);
                else if (!string.IsNullOrEmpty(Request.QueryString["ReturnURL"]))
                    litScript.Text = string.Format("<script language=\"javascript\">$telerik.$(function() {{ RedirectParentPage('{0}');CloseWindow(); }} );</script>", Encoding.ASCII.GetString(Convert.FromBase64String(Request.QueryString["ReturnURL"])));
                else
                    litScript.Text = string.Format("<script language=\"javascript\">$telerik.$(function() {{ RefreshParentPage(true);CloseWindow(); }} );</script>");
            }
        }

        protected void OnCancelClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["TemplateID"]))
            {
                DataObject dataObject = DataObject.Load<DataObject>(objectId, null, false);
                if (dataObject.State != ObjectState.Added)
                    dataObject.Delete(UserDataContext.GetUserDataContext());
            }
            litScript.Text = "<script language=\"javascript\">$telerik.$(function() { CloseWindow(); } );</script>";
        }

        private void SaveDataObject(string imageName)
        {
            dataObject.Image = imageName;
            foreach (var imageType in imageTypes)
                dataObject.SetImageType(imageType.Key, imageType.Value);

            if (!string.IsNullOrEmpty(this.TxtTitle.Text))
                dataObject.Title = Common.Extensions.StripHTMLTags(this.TxtTitle.Text);

            if (!string.IsNullOrEmpty(Request.QueryString["TemplateID"]))
            {
                Tagging.Save(dataObject);
                dataObject.ShowState = ObjectShowState.Published;
            }

            dataObject.Update(UserDataContext.GetUserDataContext());

            if (dataObject.ObjectType == Helper.GetObjectTypeNumericID("User"))
            {
                UserProfile.Current.UserPictureURL = string.Format("/{0}/P/S/{1}.jpg", objectId, imageName);
                UserProfile.Current.Save();
            }
        }
    }
}