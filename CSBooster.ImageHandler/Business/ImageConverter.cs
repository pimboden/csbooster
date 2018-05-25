//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#0.1.0.0		05.12.2006 / TS
//  Updated:   #1.0.0.0    29.08.2007 /PI
//                         Addede method GetOnlineCropingArea()
//  Updated:   #1.0.4.0    05.11.2007 / AW
//                         - Upload support for png and gif added
//  Updated:   #1.0.5.0    18.12.2007 / AW
//                         - Gif animation attribute added
//******************************************************************************
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using Leadtools;
using Leadtools.Codecs;
using Leadtools.ImageProcessing;
using Leadtools.ImageProcessing.Effects;

namespace _4screen.CSB.ImageHandler.Business
{
    public class ImageHandler
    {
        #region FIELDS

        public enum ReturnPath
        {
            AbsPath,
            Url,
            OnlineCropingArea
        }

        private Leadtools.RasterImage img;
        private RasterCodecs rasterCodecs;
        private XmlDocument xmlActionProfile;
        private CodecsImageInfo imageInfo;

        private string strActionProfile = "";
        private string strAnnotate = "";
        private string strTargetImage = "";
        private string strTargetImageUrl = "";
        private string strImgFormat = "";
        private string strImgOutPath = "";
        private string strVirtualImgOutUrl = "";
        private string strImgPreExt = "";
        private string strImgPostExt = "";
        private string strImageName;
        private string strFolderId;
        private string strMediaServer;
        private string strApplicationURL;
        private string mediaStoragePath;
        private string sourceImage;

        private int intResolution = 72;
        private int intImgQuality = 0;
        private int intSetCropLeft = 0;
        private int intSetCropTop = 0;
        private int intSetCropWidth = 0;
        private int intSetCropHeight = 0;
        private float flVisualCropFactor = 1;

        #endregion FIELDS

        #region PROPERTIES

        public string ImageName
        {
            get { return strImageName; }
        }

        public string TargetImage
        {
            get { return strTargetImage; }
        }

        public string TargetImageUrl
        {
            get { return strTargetImageUrl; }
        }

        public int SetCropLeft
        {
            set { intSetCropLeft = value; }
        }

        public int SetCropTop
        {
            set { intSetCropTop = value; }
        }

        public int SetCropWidth
        {
            set { intSetCropWidth = value; }
        }

        public int SetCropHeight
        {
            set { intSetCropHeight = value; }
        }

        public float VisualCropFactor
        {
            set { flVisualCropFactor = value; }
        }

        public CodecsImageInfo ImageInfo
        {
            get { return imageInfo; }
        }

        public int OriginalCropWidth(string ActionProfile)
        {
            try
            {
                return Convert.ToInt32(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + ActionProfile + "']/action[@ID='onlinecroparea']/cropWidth").InnerText.Trim());
            }
            catch
            {
                return -1;
            }
        }

        public int OriginalCropHeight(string ActionProfile)
        {
            try
            {
                return Convert.ToInt32(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + ActionProfile + "']/action[@ID='onlinecroparea']/cropHeight").InnerText.Trim());
            }
            catch
            {
                return -1;
            }
        }

        public int GetMaxVisualWidthOrHeight(string ActionProfile)
        {
            try
            {
                return Convert.ToInt32(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + ActionProfile + "']/action[@ID='onlinecroparea']/maxVisualCropWidth").InnerText.Trim());
            }
            catch
            {
                return -1;
            }
        }

        private static string ApplicationPath
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Request.PhysicalApplicationPath;
                }
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        #endregion PROPERTIES

        public ImageHandler(string mediaServer, string mediaStoragePath, string folderId, string imageName, bool newImageName, string configFilePath)
        {
            if (newImageName)
                strImageName = System.Guid.NewGuid().ToString();
            else
                strImageName = "";

            if (!string.IsNullOrEmpty(imageName))
                strImageName = imageName;

            strFolderId = folderId;
            strMediaServer = mediaServer;
            this.mediaStoragePath = mediaStoragePath;
            if (!configFilePath.EndsWith(@"\"))
                configFilePath += @"\";

            xmlActionProfile = new XmlDocument();
            xmlActionProfile.Load(configFilePath + "CSBImageActionProfile.xml");
        }

        public string DoConvert(string sourceImage, string actionProfile, ReturnPath retPath)
        {
            return DoTheConvert(sourceImage, actionProfile, "", retPath);
        }

        public string DoConvert(string sourceImage, string actionProfile, string annotate, ReturnPath retPath)
        {
            return DoTheConvert(sourceImage, actionProfile, annotate, retPath);
        }

        public void SaveStreamToJpeg(Stream sourceImage, string imageName)
        {
            try
            {
                RasterCodecs.Startup();
                rasterCodecs = new RasterCodecs();
                img = rasterCodecs.Load(sourceImage);
                Leadtools.RasterImageFormat rif = Leadtools.RasterImageFormat.Jpeg;
                rasterCodecs.Save(img, imageName, rif, img.BitsPerPixel);
            }
            finally
            {
                img.Dispose();
                RasterCodecs.Shutdown();
            }
        }

        private string DoTheConvert(string sourceImage, string actionProfile, string annotate, ReturnPath retPath)
        {
            string strRetFile = "";
            strActionProfile = actionProfile;
            strAnnotate = annotate;
            this.sourceImage = sourceImage;

            try
            {
                RasterCodecs.Startup();
                RasterSupport.Unlock(RasterSupportType.Pro, "LhwcFdF3jN");
                bool isLocked = RasterSupport.IsLocked(RasterSupportType.Pro);
                rasterCodecs = new RasterCodecs();
                imageInfo = rasterCodecs.GetInformation(sourceImage, true);
                if (imageInfo.TotalPages > 0 && imageInfo.Format == Leadtools.RasterImageFormat.Gif)
                {
                    rasterCodecs.Options.Gif.Load.AnimationLoop = imageInfo.Gif.AnimationLoop;
                    rasterCodecs.Options.Gif.Save.AnimationLoop = imageInfo.Gif.AnimationLoop;
                    rasterCodecs.Options.Gif.Save.UseAnimationLoop = imageInfo.Gif.HasAnimationLoop;

                    if (imageInfo.Gif.HasAnimationBackground)
                        rasterCodecs.Options.Gif.Save.AnimationBackground = imageInfo.Gif.AnimationBackground;
                    rasterCodecs.Options.Gif.Save.UseAnimationBackground = imageInfo.Gif.HasAnimationBackground;
                    // #1.0.5.0

                    if (imageInfo.Gif.HasAnimationPalette)
                        rasterCodecs.Options.Gif.Save.SetAnimationPalette(imageInfo.Gif.GetAnimationPalette());
                    rasterCodecs.Options.Gif.Save.UseAnimationPalette = imageInfo.Gif.HasAnimationPalette;

                    rasterCodecs.Options.Gif.Save.AnimationWidth = imageInfo.Gif.AnimationWidth;
                    rasterCodecs.Options.Gif.Save.AnimationHeight = imageInfo.Gif.AnimationHeight;
                }
                img = rasterCodecs.Load(sourceImage);

                // Load convert action profile
                if (Init(sourceImage, strActionProfile))
                {
                    // loop on actions
                    LoopActions();
                    SaveImage();

                    // add a copyright or something like this to the image
                    if (xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='annotate']") != null && strTargetImage.Length > 0)
                    {
                        Annotate();
                    }

                    if (retPath == ReturnPath.AbsPath)
                        strRetFile = strTargetImage;
                    else if (retPath == ReturnPath.Url)
                        strRetFile = strTargetImageUrl;
                }
            }

            finally
            {
                img.Dispose();
                RasterCodecs.Shutdown();
            }

            return strRetFile;
        }

        #region image processing

        private void Crop()
        {
            bool blnBestFit = false;
            int intBestFitWidth = 0;
            int intBestFitHeight = 0;
            int intCropLeft = 0;
            int intCropRight = 0;
            int intCropTop = 0;
            int intCropBottom = 0;
            int intCropping = 0;

            #region load properties from action profile xml

            try
            {
                blnBestFit = Convert.ToBoolean(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='crop']/bestFit").InnerText.Trim());
                intBestFitWidth = Convert.ToInt32(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='crop']/bestFitWidth").InnerText.Trim());
                intBestFitHeight = Convert.ToInt32(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='crop']/bestFitHeight").InnerText.Trim());
                intCropLeft = Convert.ToInt32(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='crop']/left").InnerText.Trim());
                intCropRight = Convert.ToInt32(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='crop']/right").InnerText.Trim());
                intCropTop = Convert.ToInt32(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='crop']/top").InnerText.Trim());
                intCropBottom = Convert.ToInt32(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='crop']/bottom").InnerText.Trim());
            }
            catch
            {
            }

            #endregion load properties from action profile xml

            CropCommand cmd = new CropCommand();
            SizeCommand cmd1 = new SizeCommand();
            cmd1.Flags = Leadtools.RasterSizeFlags.Resample;

            if (intSetCropWidth > 0 && intSetCropHeight > 0)
            {
                // do a crop based on the input parameters
                intSetCropLeft = Convert.ToInt32((float)intSetCropLeft * flVisualCropFactor);
                intSetCropTop = Convert.ToInt32((float)intSetCropTop * flVisualCropFactor);
                intSetCropWidth = Convert.ToInt32((float)intSetCropWidth * flVisualCropFactor);
                intSetCropHeight = Convert.ToInt32((float)intSetCropHeight * flVisualCropFactor);

                cmd.Rectangle = new Rectangle(intSetCropLeft, intSetCropTop, intSetCropWidth - intSetCropLeft, intSetCropHeight - intSetCropTop);
            }
            else if (blnBestFit)
            {
                // first size the image to the closer region and then crop the rest
                float flFactor = (float)img.Width / (float)img.Height;
                int intWidthDiff = intBestFitWidth - img.Width;
                int intCalcWidthDiff = intWidthDiff;
                if (intCalcWidthDiff < 0)
                {
                    intCalcWidthDiff = intCalcWidthDiff * -1;
                }

                int intHeightDiff = intBestFitHeight - img.Height;
                int intCalcHeightDiff = intHeightDiff;
                if (intCalcHeightDiff < 0)
                {
                    intCalcHeightDiff = intCalcHeightDiff * -1;
                }

                if (intCalcHeightDiff < intCalcWidthDiff)
                {
                    // crop width
                    intCropping = (intBestFitWidth - img.Width) / 2;
                    if (intCropping < 0)
                        intCropping = intCropping * -1;
                    cmd.Rectangle = new Rectangle(intCropping, 0, intBestFitWidth, img.Height);
                }
                else
                {
                    // crop height
                    intCropping = (intBestFitHeight - img.Height) / 2;
                    if (intCropping < 0)
                        intCropping = intCropping * -1;
                    cmd.Rectangle = new Rectangle(0, intCropping, img.Width, intBestFitHeight);
                }
            }
            else
            {
                // do a crop based on the config parameters
                cmd.Rectangle = new Rectangle(intCropLeft, intCropTop, img.Width - intCropLeft - intCropRight, img.Height - intCropTop - intCropBottom);
            }

            if (imageInfo.TotalPages > 0 && imageInfo.Format == Leadtools.RasterImageFormat.Gif) // #1.0.4.0
            {
                rasterCodecs.Options.Gif.Save.AnimationWidth = cmd.Rectangle.Height;
                rasterCodecs.Options.Gif.Save.AnimationHeight = cmd.Rectangle.Width;
                for (int i = 1; i <= img.PageCount; i++)
                {
                    img.Page = i;
                    cmd.Run(img);
                }
            }
            else
            {
                cmd.Run(img);
            }
        }

        private void Annotate()
        {
            string strTempAnnotate = "";
            string strTmpFile = System.IO.Path.GetTempPath() + System.Guid.NewGuid().ToString() + "." + strImgFormat;

            #region load properties from action profile xml

            try
            {
                strTempAnnotate = xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='annotate']/copyright").InnerText.Trim();
                if (strTempAnnotate.Length > 0)
                    strAnnotate = strTempAnnotate;
            }
            catch
            {
            }

            #endregion load properties from action profile xml

            if (strAnnotate.Length > 0)
            {
                Bitmap bmp = null;
                Graphics gra = null;
                SolidBrush topColor = null;
                Font fnt = null;

                try
                {
                    File.Copy(strTargetImage, strTmpFile);

                    bmp = new Bitmap(strTmpFile);
                    gra = Graphics.FromImage(bmp);

                    StringFormat strFmt = new StringFormat();
                    strFmt.Alignment = StringAlignment.Center;

                    topColor = new SolidBrush(Color.Yellow);
                    Rectangle area = new Rectangle(0, bmp.Height / 2, bmp.Width, bmp.Height);
                    fnt = new Font("Verdana", 10, FontStyle.Bold);
                    gra.DrawString(strAnnotate, fnt, topColor, area, strFmt);

                    topColor.Dispose();
                    fnt.Dispose();
                    gra.Dispose();

                    bmp.Save(strTargetImage);
                }
                finally
                {
                    topColor.Dispose();
                    fnt.Dispose();
                    gra.Dispose();
                    bmp.Dispose();

                    File.Delete(strTmpFile);
                }
            }
        }

        private void Resize()
        {
            string interpolation = "normal";
            int width = 0;
            int height = 0;

            #region load properties from action profile xml

            try
            {
                interpolation = xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='resize']/interpolation").InnerText.Trim();
                width = Convert.ToInt32(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='resize']/width").InnerText.Trim());
                height = Convert.ToInt32(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='resize']/height").InnerText.Trim());
            }
            catch
            {
            }

            #endregion load properties from action profile xml

            if (height > 0 && width > 0)
            {
                SizeCommand cmd = new SizeCommand();

                switch (interpolation)
                {
                    case "resample":
                        cmd.Flags = Leadtools.RasterSizeFlags.Resample;
                        break;
                    case "bicubic":
                        cmd.Flags = Leadtools.RasterSizeFlags.Bicubic;
                        break;
                    case "favorblack":
                        cmd.Flags = Leadtools.RasterSizeFlags.FavorBlack;
                        break;
                }

                cmd.Height = height;
                cmd.Width = width;

                if (imageInfo.TotalPages > 0 && imageInfo.Format == Leadtools.RasterImageFormat.Gif) // #1.0.4.0
                {
                    rasterCodecs.Options.Gif.Save.AnimationWidth = height;
                    rasterCodecs.Options.Gif.Save.AnimationHeight = width;
                    for (int i = 1; i <= img.PageCount; i++)
                    {
                        img.Page = i;
                        cmd.Run(img);
                    }
                }
                else
                {
                    cmd.Run(img);
                }
            }
        }

        private void ResizeToMax()
        {
            string interpolation = "normal";
            int maxWidth = 0;
            int maxHeight = 0;
            int resizeHeight = 0;
            int resizeWidth = 0;
            float fltRatio = 0;

            #region load properties from action profile xml

            try
            {
                interpolation = xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='resizetomax']/interpolation").InnerText.Trim();
                maxWidth = Convert.ToInt32(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='resizetomax']/maxwidth").InnerText.Trim());
                maxHeight = Convert.ToInt32(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='resizetomax']/maxheight").InnerText.Trim());
            }
            catch
            {
            }

            #endregion load properties from action profile xml

            SizeCommand cmd = new SizeCommand();
            if (maxHeight > 0 || maxWidth > 0)
            {
                switch (interpolation)
                {
                    case "resample":
                        cmd.Flags = Leadtools.RasterSizeFlags.Resample;
                        break;
                    case "bicubic":
                        cmd.Flags = Leadtools.RasterSizeFlags.Bicubic;
                        break;
                    case "favorblack":
                        cmd.Flags = Leadtools.RasterSizeFlags.FavorBlack;
                        break;
                    case "normal":
                        cmd.Flags = Leadtools.RasterSizeFlags.Resample;
                        break;
                }

                resizeHeight = img.Height;
                resizeWidth = img.Width;

                if (maxHeight == 0)
                {
                    //The Height will be recalculated based on maxWidth
                    if (img.Width > maxWidth)
                    {
                        resizeWidth = maxWidth;
                        fltRatio = (float)img.Height / (float)img.Width;
                        resizeHeight = Convert.ToInt32(fltRatio * (float)maxWidth);
                    }
                }
                else if (maxWidth == 0)
                {
                    //The Width will be recalculated based on maxheight
                    if (img.Height > maxHeight)
                    {
                        resizeHeight = maxHeight;
                        fltRatio = (float)img.Width / (float)img.Height;
                        resizeWidth = Convert.ToInt32(fltRatio * (float)maxHeight);
                    }
                }
                else
                {
                    if (img.Height <= maxHeight && img.Width <= maxWidth)
                    {
                        //Both are smaller or equal... leave deimesions
                        resizeHeight = img.Height;
                        resizeWidth = img.Width;
                    }
                    else if (img.Height > maxHeight && img.Width <= maxWidth)
                    {
                        // The image is higher than maxheigth but more narrow than maxwidth -->calculate width based on max height
                        resizeHeight = maxHeight;
                        fltRatio = (float)img.Width / (float)img.Height;
                        resizeWidth = Convert.ToInt32(fltRatio * (float)maxHeight);
                    }
                    else if (img.Width > maxWidth && img.Height <= maxHeight)
                    {
                        // The image is smaller than maxheigth but more width than maxwidth -->calculate height based on max width
                        resizeWidth = maxWidth;
                        fltRatio = (float)img.Height / (float)img.Width;
                        resizeHeight = Convert.ToInt32(fltRatio * (float)maxWidth);
                    }
                    else if (img.Width > maxWidth && img.Height > maxHeight && img.Width > img.Height)
                    {
                        resizeWidth = maxWidth;
                        fltRatio = (float)img.Height / (float)img.Width;
                        resizeHeight = Convert.ToInt32(fltRatio * (float)maxWidth);
                    }
                    else if (img.Width > maxWidth && img.Height > maxHeight && img.Width <= img.Height)
                    {
                        resizeHeight = maxHeight;
                        fltRatio = (float)img.Width / (float)img.Height;
                        resizeWidth = Convert.ToInt32(fltRatio * (float)maxHeight);
                    }
                }
                cmd.Height = resizeHeight;
                cmd.Width = resizeWidth;

                if (imageInfo.TotalPages > 0 && imageInfo.Format == Leadtools.RasterImageFormat.Gif) // #1.0.4.0
                {
                    rasterCodecs.Options.Gif.Save.AnimationWidth = resizeWidth;
                    rasterCodecs.Options.Gif.Save.AnimationHeight = resizeHeight;
                    for (int i = 1; i <= img.PageCount; i++)
                    {
                        img.Page = i;
                        cmd.Run(img);
                    }
                }
                else
                {
                    cmd.Run(img);
                }
            }
        }

        private void ResizeToMin()
        {
            string interpolation = "normal";
            int width = 0;
            int height = 0;
            int resizeHeight = 0;
            int resizeWidth = 0;
            float fltRatio = 0;

            #region load properties from action profile xml

            try
            {
                interpolation = xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='resizetomin']/interpolation").InnerText.Trim();
                width = Convert.ToInt32(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='resizetomin']/width").InnerText.Trim());
                height = Convert.ToInt32(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='resizetomin']/height").InnerText.Trim());
            }
            catch
            {
            }

            #endregion load properties from action profile xml

            SizeCommand cmd = new SizeCommand();
            if (height > 0 || width > 0)
            {
                switch (interpolation)
                {
                    case "resample":
                        cmd.Flags = Leadtools.RasterSizeFlags.Resample;
                        break;
                    case "bicubic":
                        cmd.Flags = Leadtools.RasterSizeFlags.Bicubic;
                        break;
                    case "favorblack":
                        cmd.Flags = Leadtools.RasterSizeFlags.FavorBlack;
                        break;
                    case "normal":
                        cmd.Flags = Leadtools.RasterSizeFlags.Resample;
                        break;
                }

                resizeHeight = img.Height;
                resizeWidth = img.Width;

                if (img.Height > img.Width) // Portrait
                {
                    resizeWidth = width;
                    fltRatio = (float)img.Height / (float)img.Width;
                    resizeHeight = Convert.ToInt32(fltRatio * (float)width);
                }
                else // Landscape
                {
                    resizeHeight = height;
                    fltRatio = (float)img.Width / (float)img.Height;
                    resizeWidth = Convert.ToInt32(fltRatio * (float)height);
                }

                cmd.Height = resizeHeight;
                cmd.Width = resizeWidth;

                if (imageInfo.TotalPages > 0 && imageInfo.Format == Leadtools.RasterImageFormat.Gif) // #1.0.4.0
                {
                    rasterCodecs.Options.Gif.Save.AnimationWidth = resizeWidth;
                    rasterCodecs.Options.Gif.Save.AnimationHeight = resizeHeight;
                    for (int i = 1; i <= img.PageCount; i++)
                    {
                        img.Page = i;
                        cmd.Run(img);
                    }
                }
                else
                {
                    cmd.Run(img);
                }
            }
        }

        private void Watermark()
        {
            int? left = null;
            int? top = null;
            try
            {
                left = Convert.ToInt32(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='watermark']/left").InnerText.Trim());
                top = Convert.ToInt32(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='watermark']/top").InnerText.Trim());
            }
            catch { }
            string watermarkFile = xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='watermark']/watermarkFile").InnerText.Trim();

            Leadtools.RasterImage watermark = rasterCodecs.Load(ApplicationPath + watermarkFile);
            Rectangle destinationRectangle;
            if (left.HasValue && top.HasValue)
            {
                destinationRectangle = new Rectangle(left.Value, top.Value, watermark.Width, watermark.Height);
            }
            else
            {
                left = Math.Max(0, (img.Width - watermark.Width) / 2);
                top = Math.Max(0, (img.Height - watermark.Height) / 2);
                destinationRectangle = new Rectangle(left.Value, top.Value, watermark.Width, watermark.Height);
            }

            // Jpg + png + png mask
            RasterImage watermarkMask = watermark.CreateAlphaImage();
            FeatherAlphaBlendCommand command = new FeatherAlphaBlendCommand(watermark, new Point(0, 0), destinationRectangle, watermarkMask, new Point(0, 0));
            command.Run(img);

            // Jpg + jpg
            // CombineFastCommand combine = new CombineFastCommand();
            // combine.DestinationImage = img;
            // combine.DestinationRectangle = new Rectangle(left, top, watermark.Width, watermark.Height);
            // combine.SourcePoint = new Point(0, 0);
            // combine.Flags = CombineFastCommandFlags.SourceCopy;
            // combine.Run(watermark);

            watermark.Dispose();
        }

        private void Rotate()
        {
            string strInterpolation = "normal";
            int intBackgroundColor = 0;
            int intAngle = 0;
            bool blnResize = false;

            #region load properties from action profile xml

            try
            {
                strInterpolation = xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='rotate']/interpolation").InnerText.Trim();
                intBackgroundColor = Convert.ToInt32(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='rotate']/backColor").InnerText.Trim());
                intAngle = Convert.ToInt32(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='rotate']/angle").InnerText.Trim());
                blnResize = Convert.ToBoolean(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/action[@ID='rotate']/resize").InnerText.Trim());
            }
            catch
            {
            }

            #endregion load properties from action profile xml

            if (intAngle > 0)
            {
                RotateCommand cmd = new RotateCommand();

                cmd.Angle = intAngle * 100;
                if (intBackgroundColor > 0)
                    cmd.FillColor = Leadtools.RasterColor.FromRgb(intBackgroundColor);

                if (blnResize)
                {
                    if (strInterpolation == "resample")
                        cmd.Flags = RotateCommandFlags.Resample & RotateCommandFlags.Resize;
                    else if (strInterpolation == "bicubic")
                        cmd.Flags = RotateCommandFlags.Bicubic & RotateCommandFlags.Resize;
                    else
                        cmd.Flags = RotateCommandFlags.Resize;
                }
                else
                {
                    if (strInterpolation == "resample")
                        cmd.Flags = RotateCommandFlags.Resample;
                    else if (strInterpolation == "bicubic")
                        cmd.Flags = RotateCommandFlags.Bicubic;
                }

                cmd.Run(img);
            }
        }

        private void SaveImage()
        {
            Leadtools.RasterImageFormat rif;

            switch (strImgFormat)
            {
                case "jpg":
                    rif = Leadtools.RasterImageFormat.Jpeg;
                    rasterCodecs.Options.Jpeg.Save.QualityFactor = intImgQuality * 255 / 100;
                    rasterCodecs.Options.Jpeg.Save.QualityFactor = Math.Min(rasterCodecs.Options.Jpeg.Save.QualityFactor, 255);
                    rasterCodecs.Options.Jpeg.Save.QualityFactor = Math.Max(rasterCodecs.Options.Jpeg.Save.QualityFactor, 2);
                    rasterCodecs.Save(img, strTargetImage, rif, 24);
                    break;
                case "gif":
                    rif = Leadtools.RasterImageFormat.Gif;
                    rasterCodecs.Save(img, strTargetImage, rif, img.BitsPerPixel, 1, img.PageCount, 1, CodecsSavePageMode.Overwrite);
                    break;
                case "png":
                    rif = Leadtools.RasterImageFormat.Png;
                    rasterCodecs.Save(img, strTargetImage, rif, img.BitsPerPixel);
                    break;
            }
        }

        public string GetPhysicalImagePath(string actionProfile)
        {
            strImgOutPath = xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + actionProfile + "']/fileOutPath").InnerText.Trim();
            strImgOutPath = strImgOutPath.Replace("##FOLDERID##", strFolderId);
            strImgOutPath = strImgOutPath.Replace("##MEDIAPATH##", mediaStoragePath);

            if (strImgOutPath.Length > 0 && !Directory.Exists(strImgOutPath))
                Directory.CreateDirectory(strImgOutPath);

            if (strImgOutPath.Length > 0 && !strImgOutPath.EndsWith(@"\"))
                strImgOutPath += @"\";

            return strImgOutPath;
        }

        public string GetVirtualImageUrl(string actionProfile)
        {
            try
            {
                strVirtualImgOutUrl = xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + actionProfile + "']/VirtualOutPath").InnerText.Trim();
                strVirtualImgOutUrl = strVirtualImgOutUrl.Replace("##FOLDERID##", strFolderId);
                strVirtualImgOutUrl = strVirtualImgOutUrl.Replace("##MEDIASERVER##", strMediaServer);

                if (strVirtualImgOutUrl.Length > 0 && !strVirtualImgOutUrl.EndsWith("/"))
                    strVirtualImgOutUrl += "/";

                return strVirtualImgOutUrl;
            }
            catch
            {
                return "";
            }
        }

        #endregion image processing

        #region handling of action profile xml configuration and do base inits

        private bool Init(string sourceImage, string actionProfile)
        {
            FileInfo fi = new FileInfo(sourceImage);

            // use same image name as original
            if (strImageName.Length == 0)
                strImageName = fi.Name.Replace(fi.Extension, "");

            strImgOutPath = GetPhysicalImagePath(actionProfile);
            if (strImgOutPath.Length == 0)
                strImgOutPath = fi.DirectoryName;

            if (!strImgOutPath.EndsWith(@"\"))
                strImgOutPath += @"\";

            strVirtualImgOutUrl = GetVirtualImageUrl(actionProfile);

            // handling of action profile xml configuration
            strApplicationURL = xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/ApplicationURL").InnerText.Trim();
            strImgFormat = xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/format").InnerText.Trim();
            if (string.IsNullOrEmpty(strImgFormat))
                strImgFormat = fi.Extension.Replace(".", "").ToLower();
            intImgQuality = Convert.ToInt32(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/quality").InnerText.Trim());
            strImgPreExt = xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/fileNamePreExtension").InnerText.Trim();
            strImgPostExt = xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/fileNamePostExtension").InnerText.Trim();
            intResolution = Convert.ToInt32(xmlActionProfile.SelectSingleNode("//root/actionProfile[@ID='" + strActionProfile + "']/resolution").InnerText.Trim());

            // do base inits

            if (fi != null)
            {
                strTargetImage = strImgOutPath + strImgPreExt + strImageName + strImgPostExt + "." + strImgFormat;
                strTargetImageUrl = strVirtualImgOutUrl + strImgPreExt + strImageName + strImgPostExt + "." + strImgFormat;
            }
            return true;
        }

        private void LoopActions()
        {
            foreach (XmlNode objNode in
                xmlActionProfile.SelectNodes("//root/actionProfile[@ID='" + strActionProfile + "']/action"))
            {
                XmlElement objAction = (XmlElement)objNode;
                string strActionCommand = objAction.GetAttribute("ID").ToLower();

                switch (strActionCommand)
                {
                    case "resize":
                        Resize();
                        break;
                    case "crop":
                        Crop();
                        break;
                    case "rotate":
                        Rotate();
                        break;
                    case "resizetomax":
                        ResizeToMax();
                        break;
                    case "resizetomin":
                        ResizeToMin();
                        break;
                    case "watermark":
                        Watermark();
                        break;
                    case "annotate":
                        break;
                }
            }
        }

        #endregion handling of action profile xml configuration
    }
}