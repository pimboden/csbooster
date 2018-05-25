// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using ElementIT.PowUpload;

namespace _4screen.CSB.WebUI.UserControls.Wizards
{
    public partial class FileUpload : StepsASCX
    {
        private string uploadSession = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            uploadSession = Request.QueryString["UploadSession"];
            if (string.IsNullOrEmpty(uploadSession))
                uploadSession = Guid.NewGuid().ToString();

            string flashSupported = Request.QueryString["Flash"];
            if (!string.IsNullOrEmpty(flashSupported) && flashSupported.ToLower() == "true")
            {
                InitFlashUpload();
                ProcessUploadedFiles();
            }
            else if (!string.IsNullOrEmpty(flashSupported) && flashSupported.ToLower() == "false")
            {
                InitAlternativeUpload();
                if (ProcessUploadedFiles() > 0)
                    Response.Redirect(string.Format("{0}?{1}&UploadSession={2}&Step={3}", Request.Url.LocalPath, Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "Step", "Flash", "UploadSession" }, true), uploadSession, StepNumber + 1));
            }
        }

        private int ProcessUploadedFiles()
        {
            int uploadedFiles = 0;
            PowUpload powUpload = new PowUpload(uploadSession);
            string userId = Request.QueryString["XUI"];

            if (powUpload != null && powUpload.Files != null && !string.IsNullOrEmpty(userId))
            {
                string uploadFolder = string.Format(@"{0}\{1}\{2}", System.Configuration.ConfigurationManager.AppSettings["ConverterRootPathUpload"], userId, Helper.GetMediaFolder(ObjectType));
                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);

                for (int i = 0; i < powUpload.Files.Count; i++)
                {
                    UploadedFile uploadedFile = powUpload.Files[i];
                    if (uploadedFile.ClientFilePath != "" && uploadedFile.IsComplete)
                    {
                        string allowedExtensions = Helper.GetAllowedExtensions(ObjectType);
                        if (allowedExtensions.Contains(uploadedFile.Extension.ToLower()))
                        {
                            uploadedFile.SaveAs(Path.Combine(uploadFolder, uploadSession.Replace("-", "") + uploadedFile.SafeFileName), true);
                            uploadedFiles++;
                        }
                    }
                }
            }
            return uploadedFiles;
        }

        private void InitFlashUpload()
        {
            /* Replace symbols "" with the &quot; at all parameters values and 
         symbols ""&"" with the ""%26"" at URL values or &amp; at other values!
         The same parameters values should be set for EMBED object below. */

            GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

            string[] flashVarsList = new string[33];
            flashVarsList[0] = string.Format("fileTypes={0}", Helper.GetAllowedExtensionsFlash(ObjectType));
            flashVarsList[1] = string.Format("maxFileCount={0}", Helper.GetObjectType(ObjectType).UploadMaxFileCount);
            flashVarsList[2] = string.Format("maxFileSize={0}", Helper.GetObjectType(ObjectType).UploadMaxFileSize);
            flashVarsList[3] = string.Format("maxFileSizeTotal={0}", Helper.GetObjectType(ObjectType).UploadMaxFileSizeTotal);
            flashVarsList[4] = string.Format(@"uploadButtonText={0}", languageShared.GetStringForUrl("CommandUpload"));
            flashVarsList[5] = string.Format(@"browseButtonText={0}...", languageShared.GetStringForUrl("CommandAdd"));
            flashVarsList[6] = string.Format(@"removeButtonText={0}", languageShared.GetStringForUrl("CommandRemove"));
            flashVarsList[7] = string.Format(@"clearListButtonText={0}", languageShared.GetStringForUrl("CommandClearList"));
            flashVarsList[8] = string.Format(@"totalSizeText={0}", languageShared.GetStringForUrl("TextTotalSizeText"));
            flashVarsList[9] = string.Format(@"cancelButtonText={0}", languageShared.GetStringForUrl("CommandCancel"));
            flashVarsList[10] = string.Format(@"progressUploadCompleteText={0}", languageShared.GetStringForUrl("TextProgressUploadCompleteText"));
            flashVarsList[11] = string.Format(@"progressUploadingText={0}", languageShared.GetStringForUrl("TextProgressUploadingText"));
            flashVarsList[12] = string.Format(@"progressUploadCanceledText={0}", languageShared.GetStringForUrl("TextProgressUploadCanceledText"));
            flashVarsList[13] = string.Format(@"progressUploadStoppedText={0}", languageShared.GetStringForUrl("TextUploadStoppedText"));
            flashVarsList[14] = string.Format(@"progressMainText={0}", languageShared.GetStringForUrl("TextProgressMainText"));
            flashVarsList[15] = string.Format(@"retryDialogYesLabel={0}", languageShared.GetStringForUrl("TextYes"));
            flashVarsList[16] = string.Format(@"retryDialogNoLabel={0}", languageShared.GetStringForUrl("TextNo"));
            flashVarsList[17] = string.Format(@"retryDialogCaption={0}", languageShared.GetStringForUrl("TextRetryDialogCaption"));
            flashVarsList[18] = string.Format(@"fileSizeExceedMessage={0}", languageShared.GetStringForUrl("TextFileSizeExceedMessage"));
            flashVarsList[19] = string.Format(@"fileSizeTotalExceedMessage={0}", languageShared.GetStringForUrl("TextSizeTotalExceedMessage"));
            flashVarsList[20] = string.Format(@"filesCountExceedMessage={0}", languageShared.GetStringForUrl("TextFilesCountExceedMessage"));
            flashVarsList[21] = string.Format(@"zeroSizeMessage={0}", languageShared.GetStringForUrl("TextZeroSizeMessage"));
            flashVarsList[22] = string.Format(@"sortAscLabel={0}", languageShared.GetStringForUrl("TextSortAscLabel"));
            flashVarsList[23] = string.Format(@"sortDescLabel={0}", languageShared.GetStringForUrl("TextSortDescLabel"));
            flashVarsList[24] = string.Format(@"sortByNameLabel={0}", languageShared.GetStringForUrl("TextSortByNameLabel"));
            flashVarsList[25] = string.Format(@"sortBySizeLabel={0}", languageShared.GetStringForUrl("TextSortBySizeLabel"));
            flashVarsList[26] = string.Format(@"sortByDateLabel={0}", languageShared.GetStringForUrl("TextSortByDateLabel"));
            flashVarsList[27] = @"javaScriptEventsPrefix=MultiPowUpload&backgroundColor=#FFFFFF&listBackgroundColor=#FFFFFF";
            flashVarsList[28] = string.Format("redirectUploadUrl={0}?{1}%26UploadSession={2}%26Step={3}", Request.Url.LocalPath, Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "Step", "Flash", "UploadSession" }, true).Replace("&", "%26"), uploadSession, StepNumber + 1);
            flashVarsList[29] = string.Format("uploadUrl={0}?{1}%26UploadSession={2}%26XUI={3}", Request.Url.LocalPath, Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "UploadSession", "XUI" }, true).Replace("&", "%26"), uploadSession, UserProfile.Current.UserId);
            flashVarsList[30] = @"showLink=No";
            flashVarsList[31] = @"labelUploadVisible=false";
            flashVarsList[32] = @"useExternalInterface=Yes";
            string flashVars = string.Join("&", flashVarsList);

            this.PhCnt.Controls.Add(new LiteralControl(string.Format(@"
         <OBJECT id=""powUploadO"" width=""100%"" height=""400"">
            <PARAM NAME=""FlashVars"" VALUE=""{0}"">
            <PARAM NAME=""Movie"" VALUE=""/Library/scripts/FlashPlayer/ElementITMultiPowUpload1.7.swf"">
            <PARAM NAME=""WMode"" VALUE=""transparent"">
            <PARAM NAME=""AllowScriptAccess"" VALUE=""always"">
            <embed id=""powUploadE"" bgcolor=""#FFFFFF"" src=""/Library/scripts/FlashPlayer/ElementITMultiPowUpload1.7.swf"" type=""application/x-shockwave-flash"" width=""100%"" height=""380"" flashvars=""{0}""></embed>
         </OBJECT>
         ", flashVars)));
        }

        private void InitAlternativeUpload()
        {
            string uploadUrl = string.Format("{0}?{1}&UploadSession={2}&XUI={3}", Request.Url.LocalPath, Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "UploadSession", "XUI" }, true), uploadSession, UserProfile.Current.UserId);

            this.Page.Form.Enctype = "multipart/form-data";
            this.PhCnt.Controls.Add(new LiteralControl(string.Format(@"
         <div align=""left"" id=""ClassicUpload"">
         Lade den Flashplayer herunter um einfacher Dateien hochladen zu können!<br/>
         <a href ='http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash' target='_blank'>Flashplayer herunterladen</a><br/>
         <script language=""javascript"">
         var classicFilesCount = 1;
         document.forms[0].action = '{0}';
         function AddFile()
         {{	
            var lastInputFile = document.getElementById(""file"" + classicFilesCount);
            var filename = lastInputFile.value.toLowerCase();
            var AllowedFileExtensions = ""{1}"";
            if(filename.length > 0)
            {{
               var filext = filename.substr(filename.lastIndexOf('.'));
               if(AllowedFileExtensions.indexOf(filext) > -1)
               {{
                  classicFilesCount ++;	
                  var elementNewFile = document.createElement(""font"");	
                  elementNewFile.innerHTML = '<BR><input type=""file"" name=""file' + classicFilesCount + '"" onChange=""javascript: AddFile();"" ID=""file' + classicFilesCount + '"" style=""width:100%;"">';
                  document.getElementById(""classicFilesList"").appendChild(elementNewFile);
               }}
               else
               {{
                  alert('Gültige Deteien: {1}');
               }}
            }}
         }}
         </script>
         <p id=""classicFilesList"">
         <input onChange=""javascript: AddFile();"" type=""file"" name=""file1"" id=""file1"" style=""width:100%;"">
         </p>			
         <input type=""submit"" name=""ClassicSubmitButton"" value=""Upload""></div>", uploadUrl, Helper.GetAllowedExtensions(ObjectType))));
        }
    }
}