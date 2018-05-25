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

namespace _4screen.CSB.WebUI.UserControls.Wizards
{
    public partial class MultipleObjects : StepsASCX
    {
        private Guid? uploadSession = null;
        private List<StepsASCX> subSteps = new List<StepsASCX>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["UploadSession"]))
                uploadSession = Request.QueryString["UploadSession"].ToGuid();

            string uploadFolder = string.Format(@"{0}\{1}\{2}", System.Configuration.ConfigurationManager.AppSettings["ConverterRootPathUpload"], UserProfile.Current.UserId, Helper.GetMediaFolder(ObjectType));

            DataObjectList<DataObject> dataObjectsByUploadSession = DataObjects.Load<DataObject>(new QuickParameters { Udc = UserDataContext.GetUserDataContext(UserProfile.Current.UserName), ObjectType = ObjectType, SortBy = QuickSort.InsertedDate, Direction = QuickSortDirection.Asc, GroupID = uploadSession, DisablePaging = true, IgnoreCache = true, QuerySourceType = QuerySourceType.MyContent });
            if (dataObjectsByUploadSession.Count == 0) // No objects found for groupId=uploadSession
            {
                string[] uploadedFiles = Directory.GetFiles(uploadFolder, string.Format("{0}*.*", uploadSession.ToString().Replace("-", "")));
                Array.Sort(uploadedFiles);
                for (int i = 0; i < uploadedFiles.Length; i++)
                {
                    FileInfo uploadedFileInfo = new FileInfo(uploadedFiles[i]);
                    StepsASCX subStep = InitSubStep(uploadedFileInfo, null, i, uploadedFiles.Length);
                }
            }
            else
            {
                for (int i = 0; i < dataObjectsByUploadSession.Count; i++)
                {
                    StepsASCX subStep = InitSubStep(null, dataObjectsByUploadSession[i].ObjectID, i, dataObjectsByUploadSession.Count);
                }
            }
        }

        private StepsASCX InitSubStep(FileInfo uploadedFileInfo, Guid? objectId, int number, int numberTotal)
        {
            bool isLast = (number < numberTotal - 1) ? false : true;
            StepsASCX subStep = null;
            if (ObjectType == Helper.GetObjectTypeNumericID("Picture"))
            {
                subStep = (StepsASCX)this.LoadControl("~/UserControls/Wizards/Picture.ascx");
                if (uploadedFileInfo != null)
                    ((Picture)subStep).FileInfo = uploadedFileInfo;
                if (numberTotal > 1)
                    ((Picture)subStep).IsSubStep = true;
            }
            else if (ObjectType == Helper.GetObjectTypeNumericID("Video"))
            {
                subStep = (StepsASCX)this.LoadControl("~/UserControls/Wizards/Video.ascx");
                if (uploadedFileInfo != null)
                    ((Video)subStep).FileInfo = uploadedFileInfo;
                if (numberTotal > 1)
                    ((Video)subStep).IsSubStep = true;
            }
            else if (ObjectType == Helper.GetObjectTypeNumericID("Audio"))
            {
                subStep = (StepsASCX)this.LoadControl("~/UserControls/Wizards/Audio.ascx");
                if (uploadedFileInfo != null)
                    ((Audio)subStep).FileInfo = uploadedFileInfo;
                if (numberTotal > 1)
                    ((Audio)subStep).IsSubStep = true;
            }
            else if (ObjectType == Helper.GetObjectTypeNumericID("Document"))
            {
                subStep = (StepsASCX)this.LoadControl("~/UserControls/Wizards/Document.ascx");
                if (uploadedFileInfo != null)
                    ((Document)subStep).FileInfo = uploadedFileInfo;
                if (numberTotal > 1)
                    ((Document)subStep).IsSubStep = true;
            }

            subStep.AccessMode = this.AccessMode;
            subStep.ObjectType = this.ObjectType;
            subStep.StepNumber = this.StepNumber;
            subStep.WizardId = this.WizardId;
            subStep.CommunityID = this.CommunityID;

            if (objectId.HasValue)
                subStep.ObjectID = objectId.Value;

            this.subSteps.Add(subStep);
            this.PhCnt.Controls.Add(subStep);
            if (!isLast)
                this.PhCnt.Controls.Add(new LiteralControl("<div class=\"CSB_input_separator\"></div>"));

            return subStep;
        }

        public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            bool subStepsSaved = true;

            foreach (StepsASCX subStep in subSteps)
            {
                if (!subStep.SaveStep(ref queryString))
                    subStepsSaved = false;
            }

            return subStepsSaved;
        }
    }
}