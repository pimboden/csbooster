// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using _4screen.Utils.Net;
using _4screen.Utils.Web;

namespace _4screen.CSB.Common
{
    public static class Helper
    {
        private static Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);

        [Obsolete("Use Extension Method IsGuid of String class", false)]
        public static bool IsGuid(string candidate)
        {
            bool isValid = false;
            if (candidate != null)
            {
                if (isGuid.IsMatch(candidate))
                {
                    isValid = true;
                }
            }
            return isValid;
        }

        public static string GeneratePassword(int length, int numberSpecialCharacters)
        {
            char[] regularCharacters = "abcdefgijkmnopqrstwxyzABCDEFGHJKLMNPQRSTWXYZ123456789".ToArray();
            char[] specialCharacters = "*$-+?_&=!%{}/".ToArray();

            byte[] randomBytes = new byte[4];

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            int seed = (randomBytes[0] & 0x7f) << 24 |
                        randomBytes[1] << 16 |
                        randomBytes[2] << 8 |
                        randomBytes[3];

            Random random = new Random(seed);

            int[] specialCharacterPositions = new int[numberSpecialCharacters];
            for (int i = 0; i < specialCharacterPositions.Length; i++)
                specialCharacterPositions[i] = random.Next(length - 1);

            char[] password = new char[length];
            for (int i = 0; i < length; i++)
            {
                if (specialCharacterPositions.Contains(i))
                    password[i] = specialCharacters[random.Next(specialCharacters.Length - 1)];
                else
                    password[i] = regularCharacters[random.Next(regularCharacters.Length - 1)];
            }

            return new string(password);
        }

        public static bool IsClosedUserGroup()
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ClosedUserGroup"]))
                return Convert.ToBoolean(ConfigurationManager.AppSettings["ClosedUserGroup"]);
            else
                return false;
        }

        public static string GetRegistrationActivationCode()
        {
            int length = 0;
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["RegistrationActivationCodeLength"]))
                int.TryParse(ConfigurationManager.AppSettings["RegistrationActivationCodeLength"], out length);

            string activationCode = Guid.NewGuid().ToString().Replace("-", "").ToLower();
            if (length >= 1 && length <= 33)
                return activationCode.Substring(0, length);
            else
                return activationCode;
        }

        public static string GetSiemeConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;
        }

        public static string GetDefaultUserPrimaryColor()
        {
            try
            {
                return ConfigurationManager.AppSettings["DefaultUserPrimaryColor"];
            }
            catch
            {
                return "3";
            }
        }
        public static string GetDefaultUserSecondaryColor()
        {
            try
            {
                return ConfigurationManager.AppSettings["DefaultUserSecondaryColor"];
            }
            catch
            {
                return "5";
            }
        }

        #region User search

        /// <summary>
        /// Beispiel String:
        /// 'spv_sex_1spv_nick_philippspv_ort_luzern'
        /// Syntax:
        /// 'spv_nnn_vvvspv_nnn_vvvvspv_nnn_nnnn'
        /// nnn = name
        /// vvv = value
        /// </summary>
        public static Hashtable ExtractSearchParameters(string searchPara)
        {
            Hashtable has = new Hashtable();

            searchPara = searchPara.Trim();

            int intPos = searchPara.IndexOf("_spv");
            while (intPos > -1)
            {
                int intEndName = searchPara.IndexOf("_", intPos + 4);
                string strName = searchPara.Substring(intPos + 4, intEndName - (intPos + 4));
                string strValue = string.Empty;
                int intEndValue = searchPara.IndexOf("_spv", intEndName);
                if (intEndValue > intEndName)
                    strValue = searchPara.Substring(intEndName + 1, intEndValue - (intEndName + 1));
                else if (intEndName < searchPara.Length)
                    strValue = searchPara.Substring(intEndName + 1);

                if (strValue.Length > 0)
                    has.Add(strName, strValue);

                intPos = intEndValue;
            }
            return has;
        }

        public static string CreateSearchParameters(Hashtable parameters)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string strName in parameters.Keys)
            {
                if (parameters[strName].ToString().Trim().Length > 0)
                {
                    sb.AppendFormat("_spv{0}_{1}", strName, parameters[strName].ToString().Trim());
                }
            }
            return sb.ToString();
        }

        #endregion

        /// <summary>
        /// Erstellt aus einem sting im Format 'HH:MM:SS' ein gültiges TimeSpan-Objekt
        /// </summary>
        public static TimeSpan ConvertFromString(string time)
        {
            if (time.Length == 0)
                return new TimeSpan(0, 0, 0);

            string[] strTime = time.Split(':');
            if (strTime.Length == 1)
                return new TimeSpan(Convert.ToInt32(strTime[0]), 0, 0);
            else if (strTime.Length == 2)
                return new TimeSpan(Convert.ToInt32(strTime[0]), Convert.ToInt32(strTime[1]), 0);
            else if (strTime.Length == 3)
                return new TimeSpan(Convert.ToInt32(strTime[0]), Convert.ToInt32(strTime[1]), Convert.ToInt32(strTime[2]));
            else
                return new TimeSpan(0, 0, 0);
        }

        public static string GetFilteredQueryString(NameValueCollection queryString, List<string> excludeList, bool withoutLeadingAmp)
        {
            List<string> loweredExludeList = excludeList.ConvertAll(x => x.ToLower());
            StringBuilder sb = new StringBuilder();
            foreach (string queryStringKey in queryString.AllKeys)
                if (!loweredExludeList.Contains(queryStringKey.ToLower()))
                    sb.AppendFormat("&{0}={1}", queryStringKey, HttpUtility.UrlEncodeUnicode(queryString[queryStringKey]));
            string filteredQueryString = sb.ToString();

            if (withoutLeadingAmp && filteredQueryString.Length > 0)
                filteredQueryString = filteredQueryString.Remove(0, 1);

            return filteredQueryString;
        }

        public static PictureFormat GetPictureFormatFromFilename(string filename)
        {
            string extension = filename.Substring(filename.LastIndexOf('.')).Replace(".", "").ToLower();
            switch (extension)
            {
                case "jpg":
                    return PictureFormat.Jpg;
                case "gif":
                    return PictureFormat.Gif;
                case "png":
                    return PictureFormat.Png;
                default:
                    return PictureFormat.Unknown;
            }
        }

        public static Control FindControl(Control CurrCtrl, string CtrlID)
        {
            Control CtrlFound;
            if (CurrCtrl != null)
            {
                CtrlFound = CurrCtrl.FindControl(CtrlID);
                if (CtrlFound != null)
                {
                    return CtrlFound;
                }
                foreach (Control ctrl in CurrCtrl.Controls)
                {
                    CtrlFound = FindControl(ctrl, CtrlID);
                    if (CtrlFound != null)
                    {
                        return CtrlFound;
                    }
                }
            }
            return null;
        }

        public static void Rbl_SelectItem(RadioButtonList Rbl, int Value)
        {
            Rbl_SelectItem(Rbl, Value.ToString());
        }

        public static void Rbl_SelectItem(RadioButtonList Rbl, string Value)
        {
            if (Rbl.Items.FindByValue(Value) != null)
                Rbl.SelectedIndex = Rbl.Items.IndexOf(Rbl.Items.FindByValue(Value));
            else if (Rbl.Items.Count > 0)
                Rbl.SelectedIndex = 0;
        }

        public static void Ddl_SelectItem(DropDownList Ddl, int Value)
        {
            Ddl_SelectItem(Ddl, Value.ToString());
        }

        public static void Ddl_SelectItem(DropDownList Ddl, string Value)
        {
            if (Ddl.Items.FindByValue(Value) != null)
                Ddl.SelectedIndex = Ddl.Items.IndexOf(Ddl.Items.FindByValue(Value));
            else if (Ddl.Items.Count > 0)
                Ddl.SelectedIndex = 0;
        }

        public static string GetTagWordString(List<string> rawTagsWordList)
        {
            StringBuilder sb = new StringBuilder();
            List<string> tags = new List<string>();
            foreach (string rawTagWords in rawTagsWordList)
            {
                if (!string.IsNullOrEmpty(rawTagWords))
                    GetTagWordOrString(rawTagWords, tags);
            }
            for (int i = 0; i < tags.Count; i++)
            {
                if (tags.Count > 1)
                    sb.AppendFormat("({0})", tags[i]);
                else
                    sb.AppendFormat("{0}", tags[i]);
                if (i < tags.Count - 1)
                    sb.Append(" und ");
            }
            return sb.ToString();
        }

        private static void GetTagWordOrString(string tagWords, List<string> tags)
        {
            StringBuilder sb = new StringBuilder();
            string[] tagList = tagWords.Split(',');
            for (int i = 0; i < tagList.Length; i++)
            {
                sb.Append(tagList[i]);
                if (i < tagList.Length - 2)
                    sb.Append(", ");
                else if (i < tagList.Length - 1)
                    sb.Append(" oder ");
            }
            if (sb.Length > 0)
                tags.Add(sb.ToString());
        }

        public static string GetMappedTagWord(string tag)
        {
            string mappedTagWord = GuiLanguage.GetGuiLanguage("Shared").GetString("Text" + tag);
            return !mappedTagWord.StartsWith("[") ? mappedTagWord : tag;
        }

        public static List<string> GetMappedTagWords(string tags)
        {
            return tags.Split(Constants.TAG_DELIMITER).ToList().ConvertAll(x => GetMappedTagWord(x));
        }

        public static string PrepareLike(string Value, bool AtBegin, bool AtEnd)
        {
            if (!string.IsNullOrEmpty(Value))
            {
                Value = Value.Replace("'", "''");
                if (!Value.EndsWith("%") && AtEnd)
                {
                    if (Value.EndsWith("*"))
                        Value = string.Concat(Value.Substring(0, Value.Length - 1), "%");
                    else
                        Value = string.Concat(Value, "%");
                }
                if (!Value.StartsWith("%") && AtBegin)
                {
                    if (Value.StartsWith("*"))
                        Value = string.Concat("%", Value.Substring(1, Value.Length));
                    else
                        Value = string.Concat("%", Value);
                }
            }
            return Value.Trim();
        }

        public static void QuickSortNodes(XmlNode ParentNode, string AttributeName, bool NumericSort)
        {
            if (ParentNode.ChildNodes.Count > 1)
                DoQuickSort(0, ParentNode.ChildNodes.Count - 1, ParentNode, AttributeName, NumericSort);
        }

        private static void DoQuickSort(int intLeft, int intRight, XmlNode xmlnodeParentNode, string strAttributeName, object objNumericSort)
        {
            int intL;
            int intR;
            int intX;
            XmlAttribute xmlattAttrLeft;
            XmlNode xmlnodeLeft;
            XmlAttribute xmlattAttrRight;
            XmlNode xmlnodeRight;
            XmlAttribute xmlattAttrMitte;
            XmlNode xmlnodeMitte;
            XmlNode xmlnodeWork = null;
            intL = intLeft;
            intR = intRight;
            intX = (intLeft + intRight) / 2;
            xmlnodeMitte = xmlnodeParentNode.ChildNodes[intX];
            xmlattAttrMitte = (XmlAttribute)xmlnodeMitte.Attributes.GetNamedItem(strAttributeName);
            if (objNumericSort == null)
            {
                int i = 0;
                objNumericSort = int.TryParse(xmlattAttrMitte.Value, out i);
            }
            do
            {
                do
                {
                    xmlnodeLeft = xmlnodeParentNode.ChildNodes[intL];
                    xmlattAttrLeft = (XmlAttribute)xmlnodeLeft.Attributes.GetNamedItem(strAttributeName);
                    if (Convert.ToBoolean(objNumericSort))
                    {
                        if (Convert.ToInt32(xmlattAttrLeft.Value) < Convert.ToInt32(xmlattAttrMitte.Value))
                            intL = intL + 1;
                        else
                            break;
                    }
                    else
                    {
                        if (xmlattAttrLeft.Value.CompareTo(xmlattAttrMitte.Value) < 0)
                            intL = intL + 1;
                        else
                            break;
                    }
                }
                while (true);
                do
                {
                    xmlnodeRight = xmlnodeParentNode.ChildNodes[intR];
                    xmlattAttrRight = (XmlAttribute)xmlnodeRight.Attributes.GetNamedItem(strAttributeName);
                    if (Convert.ToBoolean(objNumericSort))
                    {
                        if (Convert.ToInt32(xmlattAttrMitte.Value) < Convert.ToInt32(xmlattAttrRight.Value))
                            intR = intR - 1;
                        else
                            break;
                    }
                    else
                    {
                        if (xmlattAttrMitte.Value.CompareTo(xmlattAttrRight.Value) < 0)
                            intR = intR - 1;
                        else
                            break;
                    }
                }
                while (true);
                if (intL < intR)
                {
                    if (intR < xmlnodeParentNode.ChildNodes.Count - 1)
                        xmlnodeWork = xmlnodeParentNode.ChildNodes[intR + 1];
                    xmlnodeParentNode.RemoveChild(xmlnodeRight);
                    xmlnodeParentNode.InsertBefore(xmlnodeRight, xmlnodeLeft);
                    if (xmlnodeWork != null)
                    {
                        xmlnodeParentNode.RemoveChild(xmlnodeLeft);
                        xmlnodeParentNode.InsertBefore(xmlnodeLeft, xmlnodeWork);
                        xmlnodeWork = null;
                    }
                    else
                    {
                        xmlnodeParentNode.RemoveChild(xmlnodeLeft);
                        xmlnodeParentNode.AppendChild(xmlnodeLeft);
                    }
                }
                if (intL <= intR)
                {
                    intL = intL + 1;
                    intR = intR - 1;
                }
            }
            while (intL <= intR);
            if (intLeft < intR)
                DoQuickSort(intLeft, intR, xmlnodeParentNode, strAttributeName, objNumericSort);
            if (intL < intRight)
                DoQuickSort(intL, intRight, xmlnodeParentNode, strAttributeName, objNumericSort);
        }

        public static void SetActions(PictureFormat pictureFormat, int objectTypeKey, out string actionProfileCropCheckAndArchive, out string actionProfileResizeLarge, out string actionProfileResizeMedium, out string actionProfileResizeSmall, out string actionProfileResizeExtraSmall, out string actionProfileCrop, Dictionary<PictureVersion, PictureFormat> imageTypes)
        {
            SiteObjectType objectType = Helper.GetObjectType(objectTypeKey);

            imageTypes.Clear();

            actionProfileResizeExtraSmall = null;
            if (!string.IsNullOrEmpty(objectType.ImageActionExtraSmall))
            {
                actionProfileResizeExtraSmall = objectType.ImageActionExtraSmall;
                imageTypes.Add(PictureVersion.XS, PictureFormat.Jpg);
            }

            actionProfileResizeSmall = null;
            if (!string.IsNullOrEmpty(objectType.ImageActionSmall))
            {
                actionProfileResizeSmall = objectType.ImageActionSmall;
                imageTypes.Add(PictureVersion.S, PictureFormat.Jpg);
            }

            actionProfileResizeMedium = null;
            if (!string.IsNullOrEmpty(objectType.ImageActionMedium))
            {
                actionProfileResizeMedium = objectType.ImageActionMedium;
                imageTypes.Add(PictureVersion.M, PictureFormat.Jpg);
            }

            actionProfileResizeLarge = objectType.ImageActionLarge + pictureFormat;
            imageTypes.Add(PictureVersion.L, pictureFormat);

            actionProfileCropCheckAndArchive = objectType.CropCheckAndArchive;
            imageTypes.Add(PictureVersion.A, pictureFormat);

            actionProfileCrop = objectType.ImageActionCrop + pictureFormat;
        }

        public static ConfigurationSection LoadSectionFromFile(string filename, string sectionName, string cacheKey)
        {
            ConfigurationSection configurationSection = null;

            if (HttpRuntime.Cache[cacheKey] != null)
            {
                configurationSection = (ConfigurationSection)HttpRuntime.Cache[cacheKey];
            }
            else
            {
                System.Configuration.ConfigurationFileMap fileMap = new ConfigurationFileMap(filename);
                System.Configuration.Configuration configuration = System.Configuration.ConfigurationManager.OpenMappedMachineConfiguration(fileMap);
                configurationSection = (ConfigurationSection)configuration.GetSection(sectionName);

                HttpRuntime.Cache.Insert(cacheKey, configurationSection, new System.Web.Caching.CacheDependency(filename), System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, null);
            }

            return configurationSection;
        }

        public static void SaveSectionToFile(string filename, string sectionName, ConfigurationSection configurationSection)
        {
            System.Configuration.ConfigurationFileMap fileMap = new ConfigurationFileMap(filename);
            System.Configuration.Configuration configuration = System.Configuration.ConfigurationManager.OpenMappedMachineConfiguration(fileMap);
            configuration.Sections.Remove(sectionName);
            ConfigurationSection reflectedSection = Activator.CreateInstance(configurationSection.GetType()) as ConfigurationSection;
            string currentSectionXml = ((ISerializableSection)configurationSection).Serialize(configurationSection, sectionName, ConfigurationSaveMode.Full);
            reflectedSection.SectionInformation.SetRawXml(currentSectionXml);
            configuration.Sections.Add(sectionName, reflectedSection);
            ConfigurationManager.RefreshSection(configurationSection.SectionInformation.SectionName);
            configuration.Save();
        }

        public static XDocument LoadConfig(string cacheName, string configFile)
        {
            XDocument doc = null;
            if (HttpRuntime.Cache[cacheName] != null)
            {
                doc = (XDocument)HttpRuntime.Cache[cacheName];
            }
            else
            {
                doc = XDocument.Load(configFile);
                System.Web.Caching.CacheDependency dep = new System.Web.Caching.CacheDependency(configFile);
                HttpRuntime.Cache.Insert(cacheName, doc, dep, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, null);
            }
            return doc;
        }

        //[Obsolete("Use Extension Method CropString of String class", true)]
        //public static string CropString(string value, int length)
        //{
        //    if (value.Length > length && length > 3)
        //    {
        //        value = value.Substring(0, length - 3);
        //        value += "...";
        //    }
        //    return value;
        //}

        //[Obsolete("Use Extension Method StripHTMLTags of String class", true)]
        //public static string StripHTMLTags(string value)
        //{
        //    return Regex.Replace(value, "<.*?>", "");
        //}


        #region Link methods
        #endregion

        public static string GetOverviewLink(int objectId)
        {
            return Helper.GetOverviewLink((object)objectId, true);
        }

        public static string GetOverviewLink(string objectName)
        {
            int objectId;
            if (int.TryParse(objectName, out objectId))
            {
                return Helper.GetOverviewLink(objectId);
            }
            else
            {
                return Helper.GetOverviewLink((object)objectName, true);
            }
        }

        public static string GetOverviewLink(int objectId, bool showNiceLinks)
        {
            return Helper.GetOverviewLink((object)objectId, showNiceLinks);
        }

        public static string GetOverviewLink(string objectName, bool showNiceLinks)
        {
            int objectId;
            if (int.TryParse(objectName, out objectId))
            {
                return Helper.GetOverviewLink(objectId, showNiceLinks);
            }
            else
            {
                return Helper.GetOverviewLink((object)objectName, showNiceLinks);
            }
        }

        private static string GetOverviewLink(object objectTypeKey, bool showNiceLinks)
        {
            string overviewLink = string.Empty;
            SiteObjectType ot = Helper.GetObjectType(objectTypeKey);
            if (showNiceLinks)
                overviewLink = Constants.Links[ot.NiceLinkToOverviewKey].Url.ToLocalizedLink();
            else
                overviewLink = Constants.Links[ot.LinkToOverviewKey].Url;
            return overviewLink;
        }

        public static string GetDetailLink(int objectId, string objectKey)
        {
            return GetDetailLink((object)objectId, objectKey, true);
        }

        public static string GetDetailLink(string objectName, string objectKey)
        {
            int objectId;
            if (int.TryParse(objectName, out objectId))
                return GetDetailLink(objectId, objectKey, true);
            else
                return GetDetailLink((object)objectName, objectKey, true);
        }

        public static string GetDetailLink(int objectId, string objectKey, bool showNiceLinks)
        {
            return GetDetailLink((object)objectId, objectKey, showNiceLinks);
        }

        public static string GetDetailLink(string objectName, string objectKey, bool showNiceLinks)
        {
            int objectId;
            if (int.TryParse(objectName, out objectId))
            {
                return GetDetailLink(objectId, objectKey, showNiceLinks);
            }
            else
            {
                return GetDetailLink((object)objectName, objectKey, showNiceLinks);
            }
        }

        private static string GetDetailLink(object objectTypeKey, string objectKey, bool showNiceLinks)
        {
            SiteObjectType ot = Helper.GetObjectType(objectTypeKey);
            string detailLink = string.Empty;
            if (showNiceLinks)
                detailLink = string.Format("{0}{1}", Constants.Links[ot.NiceLinkToDetailKey], objectKey).ToLocalizedLink();
            else
                detailLink = string.Format("{0}{1}", Constants.Links[ot.LinkToDetailKey], objectKey);
            return detailLink;
        }

        public static string GetDashboardLink(Dashboard dashboard)
        {
            return Helper.GetDetailLink("User", UserProfile.Current.UserId.ToString(), false) + "&P=dashboard&dashboard=" + dashboard;
        }

        public static string GetEditWizardLink(int objectId, string objectKey, bool usePopupWindows)
        {
            return Helper.GetEditWizardLink((object)objectId, objectKey, usePopupWindows);
        }

        public static string GetEditWizardLink(string objectName, string objectKey, bool usePopupWindows)
        {
            int objectId;
            if (int.TryParse(objectName, out objectId))
            {
                return Helper.GetEditWizardLink(objectId, objectKey, usePopupWindows);
            }
            else
            {
                return Helper.GetEditWizardLink((object)objectName, objectKey, usePopupWindows);
            }
        }

        private static string GetEditWizardLink(object objectTypeKey, string objectKey, bool usePopupWindows)
        {
            SiteObjectType ot = Helper.GetObjectType(objectTypeKey);
            string baseUrl = usePopupWindows ? Constants.Links["LINK_TO_WIZARD"].Url + "?" : Constants.Links[ot.LinkToWizardKey].Url + "&";
            return string.Format("{0}WizardID={1}&OID={2}", baseUrl, ot.EditWizard, objectKey).ToLocalizedLink();
        }

        public static string GetUploadWizardLink(object objectTypeKey, bool usePopupWindows)
        {
            SiteObjectType ot = Helper.GetObjectType(objectTypeKey);
            string baseUrl = usePopupWindows ? Constants.Links["LINK_TO_WIZARD"].Url + "?" : Constants.Links[ot.LinkToWizardKey].Url + "&";
            return string.Format("{0}WizardID={1}", baseUrl, ot.UploadWizard).ToLocalizedLink();
        }

        public static string GetWizardLink(string wizardId, bool usePopupWindows)
        {
            string baseUrl = usePopupWindows ? Constants.Links["LINK_TO_WIZARD"].Url + "?" : Constants.Links["LINK_TO_DEFAULT_WIZARD"].Url + "&";
            return string.Format("{0}WizardID={1}", baseUrl, wizardId).ToLocalizedLink();
        }

        public static string GetMobileDetailLink(object objectTypeKey, string objectKey)
        {
            SiteObjectType ot = Helper.GetObjectType(objectTypeKey);
            return Constants.Links[ot.NiceLinkToMobileDetailKey].Url + objectKey.ToLocalizedLink();
        }

        public static string GetMobileOverviewLink(object objectTypeKey)
        {
            SiteObjectType ot = Helper.GetObjectType(objectTypeKey);
            return Constants.Links[ot.NiceLinkToMobileOverviewKey].Url.ToLocalizedLink();
        }

        #region Object properties
        #endregion

        public static int GetObjectTypeNumericID(int objectId)
        {
            return GetObjectTypeNumericID((object)objectId);
        }

        public static int GetObjectTypeNumericID(string objectName)
        {
            int objectId;
            if (int.TryParse(objectName, out objectId))
            {
                return GetObjectTypeNumericID(objectId);
            }
            else
            {
                return GetObjectTypeNumericID((object)objectName);

            }
        }

        private static int GetObjectTypeNumericID(object objectTypeKey)
        {
            try
            {
                return Helper.GetObjectType(objectTypeKey).NumericId;

            }
            catch
            {
                return 0;
            }
        }
        public static string GetDefaultURLImageSmall(string objectName)
        {
            int objectId;
            if (int.TryParse(objectName, out objectId))
            {
                return GetDefaultURLImageSmall(objectId);
            }
            else
            {
                return GetDefaultURLImageSmall(PictureVersion.S, (object)objectName);
            }
        }

        public static string GetDefaultURLImageSmall(int objectId)
        {
            return GetDefaultURLImageSmall(PictureVersion.S, (object)objectId);
        }

        public static string GetDefaultURLImageSmall(PictureVersion version, string objectName)
        {
            int objectId;
            if (int.TryParse(objectName, out objectId))
            {
                return GetDefaultURLImageSmall(version, objectId);
            }
            else
            {
                return GetDefaultURLImageSmall(version, (object)objectName);
            }
        }

        public static string GetDefaultURLImageSmall(PictureVersion version, int objectId)
        {
            return GetDefaultURLImageSmall(version, (object)objectId);
        }

        private static string GetDefaultURLImageSmall(PictureVersion version, object objectTypeKey)
        {
            try
            {
                return Helper.GetObjectType(objectTypeKey).DefaultImageURL;

            }
            catch
            {
                return Constants.DEFIMG;
            }
        }

        public static SiteObjectType GetObjectType(int objectId)
        {
            return GetObjectType((object)objectId);
        }

        public static SiteObjectType GetObjectType(string objectName)
        {
            int objectId;
            if (int.TryParse(objectName, out objectId))
            {
                return GetObjectType(objectId);
            }
            else
            {

                return GetObjectType((object)objectName);

            }
        }

        private static SiteObjectType GetObjectType(object objectTypeKey)
        {
            SiteObjectType ot = null;
            if (objectTypeKey is int)
            {
                ot = SiteObjectSection.CachedInstance.SiteObjects[objectTypeKey];
            }
            else
            {
                string key = objectTypeKey as string;
                ot = (from SiteObjectType sObject in SiteObjectSection.CachedInstance.SiteObjects.LINQEnumarable.Where(x => x.Id.ToLower() == key.ToLower())
                      select sObject).SingleOrDefault();
            }
            return ot;
        }

        public static List<SiteObjectType> GetActiveUserContentObjectTypes(bool excludeCommunity)
        {
            return (from sObjects in SiteObjectSection.CachedInstance.SiteObjects.LINQEnumarable.Where(x => x.IsActive == true && Array.Exists(x.AllowedRoles.Split(','), y => UserDataContext.GetUserDataContext().UserRoles.Contains(y)) && (x.Id.ToLower() != "community" || !excludeCommunity)) select sObjects).ToList<SiteObjectType>();
        }

        public static List<SiteObjectType> GetActiveFolderObjectTypes()
        {
            return (from sObjects in SiteObjectSection.CachedInstance.SiteObjects.LINQEnumarable.Where(x => x.IsActive == true && x.IsFolderContent == true)
                    select sObjects).ToList<SiteObjectType>();
        }

        public static List<SiteObjectType> GetObjectTypes()
        {
            return (from sObjects in SiteObjectSection.CachedInstance.SiteObjects.LINQEnumarable.Where(x => x.IsActive)
                    select sObjects).ToList();
        }

        public static string GetObjectName(int objectId, bool singular)
        {
            return Helper.GetObjectName((object)objectId, singular);
        }

        public static string GetObjectName(string objectName, bool singular)
        {
            int objectId;
            if (int.TryParse(objectName, out objectId))
            {
                return GetObjectName(objectId, singular);
            }
            else
            {
                return Helper.GetObjectName((object)objectName, singular);
            }

        }

        private static string GetObjectName(object objectTypeKey, bool singular)
        {

            string objectName = singular ? "Objekt" : "Objekte";

            try
            {
                SiteObjectType ot = GetObjectType(objectTypeKey);
                string localizationBaseFileName = "SiteObjects";
                if (!string.IsNullOrEmpty(ot.LocalizationBaseFileName))
                {
                    localizationBaseFileName = ot.LocalizationBaseFileName;
                }
                objectName = singular ? GuiLanguage.GetGuiLanguage(localizationBaseFileName).GetString(ot.NameSingularKey)
                    : GuiLanguage.GetGuiLanguage(localizationBaseFileName).GetString(ot.NamePluralKey);
            }
            catch
            {
            }
            return objectName;
        }

        public static string GetObjectIcon(int objectId)
        {
            return Helper.GetObjectIcon((object)objectId);
        }

        public static string GetObjectIcon(string objectName)
        {
            int objectId;
            if (int.TryParse(objectName, out objectId))
            {
                return GetObjectIcon(objectId);
            }
            else
            {
                return Helper.GetObjectIcon((object)objectName);
            }
        }

        private static string GetObjectIcon(object objectTypeKey)
        {
            return Helper.GetObjectType(objectTypeKey).Icon;
        }

        public static bool IsObjectTypeEnabled(string objectName)
        {
            int objectId;
            if (int.TryParse(objectName, out objectId))
            {
                return Helper.IsObjectTypeEnabled(objectId);
            }
            else
            {

                return Helper.IsObjectTypeEnabled((object)objectName);

            }
        }

        public static bool IsObjectTypeEnabled(int objectId)
        {
            return Helper.IsObjectTypeEnabled((object)objectId);
        }

        private static bool IsObjectTypeEnabled(object objectTypeKey)
        {
            var objectType = Helper.GetObjectType(objectTypeKey);

            if (objectType != null)
                return Helper.GetObjectType(objectTypeKey).IsActive;

            return false;
        }

        public static string GetAllowedExtensions(int objectId)
        {
            return Helper.GetAllowedExtensions((object)objectId);
        }

        public static string GetAllowedExtensions(string objectName)
        {
            int objectId;
            if (int.TryParse(objectName, out objectId))
            {
                return Helper.GetAllowedExtensions(objectId);
            }
            else
            {

                return Helper.GetAllowedExtensions((object)objectName);

            }
        }

        private static string GetAllowedExtensions(object objectTypeKey)
        {
            return Helper.GetObjectType(objectTypeKey).AllowedFileExtensions;
        }

        public static string GetAllowedExtensionsFlash(int objectId)
        {
            return Helper.GetAllowedExtensionsFlash((object)objectId);
        }

        public static string GetAllowedExtensionsFlash(string objectName)
        {
            int objectId;
            if (int.TryParse(objectName, out objectId))
            {
                return Helper.GetAllowedExtensionsFlash(objectId);
            }
            else
            {
                return Helper.GetAllowedExtensionsFlash((object)objectName);

            }
        }

        private static string GetAllowedExtensionsFlash(object objectTypeKey)
        {
            SiteObjectType ot = Helper.GetObjectType(objectTypeKey);
            string ObjectTypeId = ot.Id.ToLower();
            string allowedExt = ot.AllowedFileExtensions;
            string allowedExtFlash = string.Empty;
            if (ObjectTypeId == "audio" ||
                ObjectTypeId == "document" ||
                ObjectTypeId == "picture" ||
                ObjectTypeId == "video"
                )
            {
                allowedExtFlash = string.Format("{0}(*{1})|*{1}", Helper.GetObjectName(objectTypeKey, false), string.Join("\\;*", allowedExt.Split(',')));

            }
            else
            {
                allowedExtFlash = "all|*.*";

            }
            return allowedExtFlash;
        }

        public static string GetMediaFolder(string objectName)
        {
            int objectId;
            if (int.TryParse(objectName, out objectId))
            {
                return Helper.GetMediaFolder(objectId);
            }
            else
            {
                return Helper.GetMediaFolder((object)objectName);
            }
        }

        public static string GetMediaFolder(int objectId)
        {
            return Helper.GetMediaFolder((object)objectId);
        }

        public static string GetMediaFolder(object objectTypeKey)
        {
            return Helper.GetObjectType(objectTypeKey).MediaFolder;
        }

        public static string GetVideoBaseURL()
        {
            if (bool.Parse(ConfigurationManager.AppSettings["UseAmazoneS3"]))
            {
                return "http://" + ConfigurationManager.AppSettings["AmazoneS3Bucket"].ToLower() + ".s3.amazonaws.com";
            }
            else
            {
                return ConfigurationManager.AppSettings["MediaDomainName"];
            }
        }

        public static XDocument LoadMobileConfig()
        {
            return LoadConfig("Mobile.config", string.Format(@"{0}\Configurations\Mobile.config", WebRootPath.Instance.ToString()));
        }

        #region XML Transformation

        public static string TransformDataObject(int objectType, XmlDocument xmlDocument, string urlXSLT, string outputType, string baseUrlXSLT, string templatesUrl, XsltArgumentList argumentList)
        {
            StringBuilder sb = new StringBuilder();

            string output = null;
            string defaultUrlXSLT = string.Format("{0}DataObject.xslt", templatesUrl);
            string defaultUrlXSLTTyped = string.Format("{0}DataObject{1}.xslt", templatesUrl, Helper.GetObjectType(objectType).Id);
            if (!string.IsNullOrEmpty(urlXSLT))
            {
                if (!string.IsNullOrEmpty(baseUrlXSLT))
                {
                    urlXSLT = urlXSLT.Replace("#BASE_URL_XSLT#", baseUrlXSLT);
                }
                else
                {
                    urlXSLT = urlXSLT.Replace("#BASE_URL_XSLT#", templatesUrl);
                }

                urlXSLT = urlXSLT.Replace("#TYPE#", outputType);
            }

            try
            {
                output = Helper.TransformXML(xmlDocument, argumentList, urlXSLT);
            }
            catch (Exception e1)
            {
                sb.Append("url=" + urlXSLT + ", message=" + e1.Message + ", trace=" + e1.StackTrace);
            }
            if (output == null)
                try
                {
                    output = Helper.TransformXML(xmlDocument, argumentList, defaultUrlXSLTTyped);
                }
                catch (Exception e2)
                {
                    sb.Append("url=" + defaultUrlXSLTTyped + ", message=" + e2.Message + ", trace=" + e2.StackTrace);
                }
            if (output == null)
                try
                {
                    output = Helper.TransformXML(xmlDocument, argumentList, defaultUrlXSLT);
                }
                catch (Exception e3)
                {
                    sb.Append("url=" + defaultUrlXSLT + ", message=" + e3.Message + ", trace=" + e3.StackTrace);
                }

            return output ?? sb.ToString();
        }

        public static string TransformXML(string xmlUrlOrData, XsltArgumentList argumentList, string xslUrlorData)
        {
            string xmlData = xmlUrlOrData;
            if (Uri.IsWellFormedUriString(xmlUrlOrData, UriKind.Absolute))
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(xmlUrlOrData);
                xmlData = Http.DownloadContent(request, null);
            }

            string xslData = xslUrlorData;
            if (Uri.IsWellFormedUriString(xslUrlorData, UriKind.Absolute))
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(xslUrlorData);
                xslData = Http.DownloadContent(request, null);
            }

            if (!string.IsNullOrEmpty(xmlData) && !string.IsNullOrEmpty(xslData))
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.ConformanceLevel = ConformanceLevel.Fragment;
                xmlWriterSettings.OmitXmlDeclaration = true;

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlData);

                XslCompiledTransform xslTransform = new XslCompiledTransform();
                StringReader reader = new StringReader(xslData);
                XmlReader xmlReader = XmlReader.Create(reader);
                xslTransform.Load(xmlReader);
                xmlReader.Close();
                reader.Close();

                StringWriter writer = new StringWriter();
                XmlWriter xmlWriter = XmlWriter.Create(writer, xmlWriterSettings);
                xslTransform.Transform(xmlDocument, argumentList, xmlWriter);
                xmlWriter.Close();
                writer.Close();

                return writer.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static string TransformXML(XmlNode xmlNode, XsltArgumentList argumentList, string xslUrl)
        {
            if (Uri.IsWellFormedUriString(xslUrl, UriKind.Absolute))
            {
                XslCompiledTransform xslTransform = (XslCompiledTransform)HttpRuntime.Cache[xslUrl];
                if (xslTransform == null)
                {
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(xslUrl);
                    xslTransform = new XslCompiledTransform();
                    XmlReader xmlReader = XmlReader.Create(request.GetResponse().GetResponseStream());
                    xslTransform.Load(xmlReader);
                    xmlReader.Close();
                    HttpRuntime.Cache.Insert(xslUrl, xslTransform);
                }

                StringWriter writer = new StringWriter();
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.ConformanceLevel = ConformanceLevel.Fragment;
                xmlWriterSettings.OmitXmlDeclaration = true;
                XmlWriter xmlWriter = XmlWriter.Create(writer, xmlWriterSettings);
                xslTransform.Transform(xmlNode, argumentList, xmlWriter);
                xmlWriter.Close();
                writer.Close();

                return writer.ToString();
            }
            else
            {
                return null;
            }
        }

        #endregion
    }

    public class CompareFileByDate : IComparer
    {
        int IComparer.Compare(Object a, Object b)
        {
            FileInfo fia = new FileInfo((string)a);
            FileInfo fib = new FileInfo((string)b);

            DateTime cta = fia.CreationTime;
            DateTime ctb = fib.CreationTime;

            return DateTime.Compare(cta, ctb) * (-1);
        }
    }

    public class StringLogicalComparer
    {
        public static int Compare(string s1, string s2)
        {
            //get rid of special cases
            if ((s1 == null) && (s2 == null))
                return 0;
            else if (s1 == null)
                return -1;
            else if (s2 == null)
                return 1;

            if ((s1.Equals(string.Empty) && (s2.Equals(string.Empty))))
                return 0;
            else if (s1.Equals(string.Empty))
                return -1;
            else if (s2.Equals(string.Empty))
                return -1;

            //WE style, special case
            bool sp1 = Char.IsLetterOrDigit(s1, 0);
            bool sp2 = Char.IsLetterOrDigit(s2, 0);
            if (sp1 && !sp2)
                return 1;
            if (!sp1 && sp2)
                return -1;

            int i1 = 0, i2 = 0; //current index
            int r = 0; // temp result
            while (true)
            {
                bool c1 = Char.IsDigit(s1, i1);
                bool c2 = Char.IsDigit(s2, i2);
                if (!c1 && !c2)
                {
                    bool letter1 = Char.IsLetter(s1, i1);
                    bool letter2 = Char.IsLetter(s2, i2);
                    if ((letter1 && letter2) || (!letter1 && !letter2))
                    {
                        if (letter1 && letter2)
                        {
                            r = Char.ToLower(s1[i1]).CompareTo(Char.ToLower(s2[i2]));
                        }
                        else
                        {
                            r = s1[i1].CompareTo(s2[i2]);
                        }
                        if (r != 0)
                            return r;
                    }
                    else if (!letter1 && letter2)
                        return -1;
                    else if (letter1 && !letter2)
                        return 1;
                }
                else if (c1 && c2)
                {
                    r = CompareNum(s1, ref i1, s2, ref i2);
                    if (r != 0)
                        return r;
                }
                else if (c1)
                {
                    return -1;
                }
                else if (c2)
                {
                    return 1;
                }
                i1++;
                i2++;
                if ((i1 >= s1.Length) && (i2 >= s2.Length))
                {
                    return 0;
                }
                else if (i1 >= s1.Length)
                {
                    return -1;
                }
                else if (i2 >= s2.Length)
                {
                    return -1;
                }
            }
        }

        private static int CompareNum(string s1, ref int i1, string s2, ref int i2)
        {
            int nzStart1 = i1, nzStart2 = i2; // nz = non zero
            int end1 = i1, end2 = i2;

            ScanNumEnd(s1, i1, ref end1, ref nzStart1);
            ScanNumEnd(s2, i2, ref end2, ref nzStart2);
            int start1 = i1;
            i1 = end1 - 1;
            int start2 = i2;
            i2 = end2 - 1;

            int nzLength1 = end1 - nzStart1;
            int nzLength2 = end2 - nzStart2;

            if (nzLength1 < nzLength2)
                return -1;
            else if (nzLength1 > nzLength2)
                return 1;

            for (int j1 = nzStart1, j2 = nzStart2; j1 <= i1; j1++, j2++)
            {
                int r = s1[j1].CompareTo(s2[j2]);
                if (r != 0)
                    return r;
            }
            // the nz parts are equal
            int length1 = end1 - start1;
            int length2 = end2 - start2;
            if (length1 == length2)
                return 0;
            if (length1 > length2)
                return -1;
            return 1;
        }

        //lookahead
        private static void ScanNumEnd(string s, int start, ref int end, ref int nzStart)
        {
            nzStart = start;
            end = start;
            bool countZeros = true;
            while (Char.IsDigit(s, end))
            {
                if (countZeros && s[end].Equals('0'))
                {
                    nzStart++;
                }
                else
                    countZeros = false;
                end++;
                if (end >= s.Length)
                    break;
            }
        }
    }
}