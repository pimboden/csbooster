// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using SiteConfig = _4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.UserControls.Wizards
{
    public partial class ObjectRelations : StepsASCX
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base");
        protected void Page_Load(object sender, EventArgs e)
        {
            OTOR.ParentObjectID = ObjectID.Value;
            OTOR.ChildObjectTypes = new List<string>() { Settings["ObjectType"] };
            OTOR.UserId = !UserDataContext.GetUserDataContext().IsAdmin ? UserProfile.Current.UserId : (Guid?)null;

            LabelUploadObjects.LabelKey = "LabelUpload" + Settings["ObjectType"];
            LabelUploadObjects.TooltipKey = "TooltipUpload" + Settings["ObjectType"];

            SiteObjectType objectType = Helper.GetObjectType(Settings["ObjectType"]);
            string initQuerySegment = objectType.NumericId != Helper.GetObjectTypeNumericID("Community") ? "&XCN=" + UserProfile.Current.ProfileCommunityID : string.Empty;
            initQuerySegment += !objectType.IsMultiUpload ? "&OID=" + Guid.NewGuid().ToString() : string.Empty;
            if (SiteConfig.UsePopupWindows)
                initQuerySegment += "&Callback=RefreshMyContent&CallbackWindow=wizardWin";
            else
                initQuerySegment += "&ReturnUrl=" + System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(Request.RawUrl));
            string createMenuTitle = string.IsNullOrEmpty(objectType.LocalizationBaseFileName) ? GuiLanguage.GetGuiLanguage("SiteObjects").GetString(objectType.NameCreateMenuKey) : GuiLanguage.GetGuiLanguage(objectType.LocalizationBaseFileName).GetString(objectType.NameCreateMenuKey);
            LnkUpload.NavigateUrl = string.Format("javascript:radWinOpen('{0}{1}', '{2}', 800, 500, true, null, 'wizardWin2')", Helper.GetUploadWizardLink(Settings["ObjectType"], true), initQuerySegment, createMenuTitle);
        }

        public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            if (OTOR.Save())
            {
                if (bool.Parse(Settings["InheritFirstRelatedPicture"]))
                {
                    DataObjectList<DataObject> objectList = DataObjects.Load<DataObject>(new QuickParameters
                                                                                             {
                                                                                                 Udc = UserDataContext.GetUserDataContext(),
                                                                                                 ObjectType = Helper.GetObjectTypeNumericID(Settings["ObjectType"]),
                                                                                                 DisablePaging = true,
                                                                                                 Amount = 1,
                                                                                                 SortBy = QuickSort.RelationSortNumber,
                                                                                                 Direction = QuickSortDirection.Asc,
                                                                                                 IgnoreCache = true,
                                                                                                 RelationParams = new RelationParams { ParentObjectID = ObjectID }
                                                                                             });
                    if (objectList.Count > 0)
                    {
                        DataObject dataObject = DataObject.Load<DataObject>(ObjectID.Value, null, false);
                        dataObject.Image = ObjectID.Value.ToString();
                        string mediaSource = string.Format(@"{0}\{1}\P\{{0}}\{2}.jpg", ConfigurationManager.AppSettings["ConverterRootPathMedia"], objectList[0].UserID, objectList[0].ObjectID);
                        string mediaTarget = string.Format(@"{0}\{1}\P\{{0}}\{2}.jpg", ConfigurationManager.AppSettings["ConverterRootPathMedia"], dataObject.UserID, dataObject.ObjectID);
                        foreach (var pictureFormat in objectList[0].PictureFormats)
                        {
                            if (!string.IsNullOrEmpty(pictureFormat.Value))
                            {
                                dataObject.SetImageType(pictureFormat.Key, (PictureFormat)Enum.Parse(typeof(PictureFormat), pictureFormat.Value));
                                File.Copy(string.Format(mediaSource, pictureFormat.Key), string.Format(mediaTarget, pictureFormat.Key), true);
                            }
                        }
                        dataObject.Update(UserDataContext.GetUserDataContext());
                    }
                }
                return true;
            }
            else
            {
                LitMsg.Text = "Fehler beim Speichern";
                return false;
            }
        }
    }
}