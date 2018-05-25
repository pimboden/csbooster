using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using System;
using System.Reflection;
using System.Data;

namespace _4screen.CSB.Widget
{
    public partial class Info : WidgetBase
    {
        protected GuiLanguage languageProfile = GuiLanguage.GetGuiLanguage("ProfileData");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        public override bool ShowObject(string settingsXml)
        {
            DataObject dataObject = null;
            if (this._Host.ParentPageType == PageType.Detail && !string.IsNullOrEmpty(Request.QueryString["OID"]))
            { 
                if (string.IsNullOrEmpty(Request.QueryString["OT"])) 
                    dataObject = DataObject.LoadByReflection(Request.QueryString["OID"].ToGuid());   
                else
                    dataObject = DataObject.LoadByReflection(Request.QueryString["OID"].ToGuid(), Helper.GetObjectTypeNumericID(Request.QueryString["OT"]));   
            }
            else if (this._Host.ParentPageType == PageType.None)
            {
                dataObject = DataObject.Load<DataObject>(CommunityID);
                if (dataObject.ObjectType == 19)
                    dataObject = DataObject.Load<DataObjectUser>(dataObject.UserID);   
                else if (dataObject.ObjectType != 1)
                    return false;
            }
            else
            {
                return false;
            }

            if (dataObject != null)
            {
                Fill(dataObject);
                return true;
            }
            else
                return false;
          }

        protected void Fill(DataObject dataObject)
        {
            StringBuilder sb = new StringBuilder();

            if (dataObject is DataObjectUser)
            {
                try
                {
                    DataObjectUser user = (DataObjectUser)dataObject;

                    //////////////////////////////////////////
                    // Geschlecht, Alter
                    //////////////////////////////////////////
                    bool blnAdded = false;

                    if (user.SexShow && !string.IsNullOrEmpty(user.Sex))
                    {
                        if (user.Sex != ";-1;")
                        {
                            sb.Append("<div class=\"CSB_wdg_info\">");
                            if (user.Sex == ";0;")
                                sb.AppendFormat("{0} <b>{1}</b>", languageProfile.GetString("LableInfoStart"), languageProfile.GetString("SexMale"));
                            else if (user.Sex == ";1;")
                                sb.AppendFormat("{0} <b>{1}</b>", languageProfile.GetString("LableInfoStart"), languageProfile.GetString("SexFemale"));

                            blnAdded = true;
                        }
                    }

                    if (user.BirthdayShow && user.Birthday.HasValue)
                    {
                        int years = DateTime.Now.Year - user.Birthday.Value.Year;
                        if (DateTime.Now.Month < user.Birthday.Value.Month || (DateTime.Now.Month == user.Birthday.Value.Month && DateTime.Now.Day < user.Birthday.Value.Day))
                            years--;

                        if (years > 0)
                        {
                            if (!blnAdded)
                                sb.AppendFormat("<div class=\"CSB_wdg_info\">{0} ", languageProfile.GetString("LableInfoStart"));
                            else
                                sb.AppendFormat(" {0} ", languageProfile.GetString("LableInfoAnd"));

                            sb.AppendFormat("<b>{0} {1}</b>", years, languageProfile.GetString("LableInfoAge"));

                            blnAdded = true;
                        }
                    }

                    if (user.LanguagesShow && !string.IsNullOrEmpty(user.Languages) && user.Languages != ";Andere;")
                    {
                        if (!blnAdded)
                            sb.AppendFormat("<div class=\"CSB_wdg_info\">{0} ", languageProfile.GetString("LableInfoStartLang"));
                        else
                            sb.AppendFormat(" {0} ", languageProfile.GetString("LableInfoAndLang"));

                        sb.AppendFormat("<b>{0}</b>", user.Languages.Replace(";", string.Empty));

                        blnAdded = true;
                    }

                    if (blnAdded)
                        sb.Append(".</div>");

                    //////////////////////////////////////////
                    // Beziehung, Neigung, Augen, Haare, Grösse, Gewicht
                    //////////////////////////////////////////
                    if ((user.RelationStatus != null && user.RelationStatusShow && !string.IsNullOrEmpty(user.RelationStatus)) ||
                        (user.AttractedTo != null && user.AttractedToShow && !string.IsNullOrEmpty(user.AttractedTo)) ||
                        (user.EyeColor != null && user.EyeColorShow && !string.IsNullOrEmpty(user.EyeColor)) ||
                        (user.HairColor != null && user.HairColorShow && !string.IsNullOrEmpty(user.HairColor)) ||
                        (user.BodyHeightShow && user.BodyHeight != 0) ||
                        (user.BodyWeightShow && user.BodyWeight != 0))
                    {
                        sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"CSB_wdg_info\">");
                        if (user.RelationStatus != null && user.RelationStatusShow && !string.IsNullOrEmpty(user.RelationStatus))
                        {
                            sb.AppendFormat("<tr><td>{0}:&nbsp;</td>", ProfileDataHelper.GetProfileDataTitle(SiteConfig.SiteVRoot, UserProfileDataKey.Status));
                            try
                            {
                                Dictionary<string, string> names = ProfileDataHelper.GetProfileDataNames(SiteConfig.SiteVRoot, UserProfileDataKey.Status);
                                string value = user.RelationStatus.Trim(new char[] { ';' });
                                sb.AppendFormat("<td><b>{0}</b></td>", names[value]);
                            }
                            catch { }
                            sb.Append("</tr>");
                        }
                        if (user.AttractedTo != null && user.AttractedToShow && !string.IsNullOrEmpty(user.AttractedTo))
                        {
                            sb.AppendFormat("<tr><td>{0}:&nbsp;</td>", ProfileDataHelper.GetProfileDataTitle(SiteConfig.SiteVRoot, UserProfileDataKey.AttractedTo));
                            try
                            {
                                Dictionary<string, string> names = ProfileDataHelper.GetProfileDataNames(SiteConfig.SiteVRoot, UserProfileDataKey.AttractedTo);
                                string value = user.AttractedTo.Trim(new char[] { ';' });
                                sb.AppendFormat("<td><b>{0}</b></td>", names[value]);
                            }
                            catch { }
                            sb.Append("</tr>");
                        }
                        if (user.EyeColor != null && user.EyeColorShow && !string.IsNullOrEmpty(user.EyeColor))
                        {
                            sb.AppendFormat("<tr><td>{0}:&nbsp;</td>", ProfileDataHelper.GetProfileDataTitle(SiteConfig.SiteVRoot, UserProfileDataKey.EyeColor));
                            try
                            {
                                Dictionary<string, string> names = ProfileDataHelper.GetProfileDataNames(SiteConfig.SiteVRoot, UserProfileDataKey.EyeColor);
                                string value = user.EyeColor.Trim(new char[] { ';' });
                                sb.AppendFormat("<td><b>{0}</b></td>", names[value]);
                            }
                            catch { }
                            sb.Append("</tr>");
                        }
                        if (user.HairColor != null && user.HairColorShow && !string.IsNullOrEmpty(user.HairColor))
                        {
                            sb.AppendFormat("<tr><td>{0}:&nbsp;</td>", ProfileDataHelper.GetProfileDataTitle(SiteConfig.SiteVRoot, UserProfileDataKey.HairColor));
                            try
                            {
                                Dictionary<string, string> names = ProfileDataHelper.GetProfileDataNames(SiteConfig.SiteVRoot, UserProfileDataKey.HairColor);
                                string value = user.HairColor.Trim(new char[] { ';' });
                                sb.AppendFormat("<td><b>{0}</b></td>", names[value]);
                            }
                            catch { }
                            sb.Append("</tr>");
                        }
                        if (user.BodyHeightShow && user.BodyHeight != 0)
                        {
                            sb.AppendFormat("<tr><td>{0}:&nbsp;</td>", ProfileDataHelper.GetProfileDataTitle(SiteConfig.SiteVRoot, UserProfileDataKey.BodyHeight));
                            try
                            {
                                sb.AppendFormat("<td><b>{0} cm</b></td>", user.BodyHeight);
                            }
                            catch { }
                            sb.Append("</tr>");
                        }
                        if (user.BodyWeightShow && user.BodyWeight != 0)
                        {
                            sb.AppendFormat("<tr><td>{0}:&nbsp;</td>", ProfileDataHelper.GetProfileDataTitle(SiteConfig.SiteVRoot, UserProfileDataKey.BodyWeight));
                            try
                            {
                                sb.AppendFormat("<td><b>{0} kg</b></td>", user.BodyWeight);
                            }
                            catch { }
                            sb.Append("</tr>");
                        }
                        sb.Append("</table>");
                    }
                    //////////////////////////////////////////
                    // Wohnort
                    //////////////////////////////////////////
                    if (user.AddressCityShow && !string.IsNullOrEmpty(user.AddressCity))
                    {
                        sb.AppendFormat("<div class=\"CSB_wdg_info\">{0} ", languageProfile.GetString("LableInfoStartCity"));
                        sb.AppendFormat("<b>{0}</b>", user.AddressCity);

                        if (user.AddressLandShow && !string.IsNullOrEmpty(user.AddressLand))
                        {
                            string landKey = "LableInfoContry" + user.AddressLand.Replace(";", "").ToUpper();
                            switch (user.AddressLand)
                            {
                                case ";CH;":
                                    //sb.AppendFormat(", <b>{0}</b>.", languageProfile.GetString("LableInfoContryCH"));
                                    sb.Append(".");
                                    break;
                                default:
                                    sb.AppendFormat(", <b>{0}</b>.", languageProfile.GetString(landKey));
                                    break;
                            }
                        }
                        sb.Append("</div>");
                    }
                    else if (user.AddressLandShow && !string.IsNullOrEmpty(user.AddressLand))
                    {
                        string landKey = "LableInfoContry" + user.AddressLand.Replace(";", "").ToUpper();
                        switch (user.AddressLand)
                        {
                            case ";CH;":
                                sb.AppendFormat("<div class=\"CSB_wdg_info\">{0} {1} <b>{2}</b>.</div>", languageProfile.GetString("LableInfoStartCity"), languageProfile.GetString("LableInfoStartCityExt"), languageProfile.GetString(landKey));
                                break;
                            default:
                                sb.AppendFormat("<div class=\"CSB_wdg_info\">{0} <b>{1}</b>.</div>", languageProfile.GetString("LableInfoStartCity"), languageProfile.GetString(landKey));
                                break;
                        }
                        if (dataObject.Geo_Lat > double.MinValue)
                        {
                            sb.AppendFormat(string.Format(@"<div class=""CSB_wdg_info""><a href=""javascript:radWinOpen('{0}/Pages/Popups/DetailMap.aspx?ObjID={1}', '{2}', 640, 480, true)"" class=""CSB_button1"">{3}</a></div>", SiteConfig.SiteVRoot, dataObject.ObjectID, languageShared.GetString("TitleMap").StripForScript(), languageShared.GetString("CommandShowMap")));
                        }
                    }

                    //////////////////////////////////////////
                    // Kommunikation
                    //////////////////////////////////////////
                    List<string> communication = new List<string>();
                    if (user.Mobile != null && user.MobileShow && !string.IsNullOrEmpty(user.Mobile))
                    {
                        communication.Add(string.Format("<b>{0} [{1}]</b>", languageProfile.GetString("Mobile"), user.Mobile));
                    }
                    if (user.Phone != null && user.PhoneShow && !string.IsNullOrEmpty(user.Phone))
                    {
                        communication.Add(string.Format("<b>{0} [{1}]</b>", languageProfile.GetString("Phone"), user.Phone));
                    }
                    if (user.MSN != null && user.MSNShow && !string.IsNullOrEmpty(user.MSN))
                    {
                        communication.Add(string.Format("<b>{0} [{1}]</b>", languageProfile.GetString("MSN"), user.MSN));
                    }
                    if (user.Yahoo != null && user.YahooShow && !string.IsNullOrEmpty(user.Yahoo))
                    {
                        communication.Add(string.Format("<b>{0} [{1}]</b>", languageProfile.GetString("Yahoo"), user.Yahoo));
                    }
                    if (user.Skype != null && user.SkypeShow && !string.IsNullOrEmpty(user.Skype))
                    {
                        communication.Add(string.Format("<b>{0} [{1}]</b>", languageProfile.GetString("Skype"), user.Skype));
                    }
                    if (user.ICQ != null && user.ICQShow && !string.IsNullOrEmpty(user.ICQ))
                    {
                        communication.Add(string.Format("<b>{0} [{1}]</b>", languageProfile.GetString("ICQ"), user.ICQ));
                    }
                    if (user.AIM != null && user.AIMShow && !string.IsNullOrEmpty(user.AIM))
                    {
                        communication.Add(string.Format("<b>{0} [{1}]</b>", languageProfile.GetString("AIM"), user.AIM));
                    }

                    string communicationString = string.Empty;
                    for (int i = 0; i < communication.Count; i++)
                    {
                        communicationString += communication[i];
                        if (i == communication.Count - 2)
                            communicationString += string.Format(" {0} ", languageProfile.GetString("LableInfoAnd"));
                        else if (i < communication.Count - 1)
                            communicationString += ", ";
                    }

                    if (!string.IsNullOrEmpty(communicationString))
                    {
                        sb.AppendFormat("<div class=\"CSB_wdg_info\">{0} {1}.</div>", languageProfile.GetString("LableInfoCommunication"), communicationString);
                    }

                    //////////////////////////////////////////
                    // Homepage
                    //////////////////////////////////////////
                    if (user.Homepage != null && user.HomepageShow && !string.IsNullOrEmpty(user.Homepage))
                    {
                        if (user.Homepage.StartsWith("http"))
                            sb.AppendFormat("<div class=\"CSB_wdg_info\"><b><a href='{0}' target='_blank'>{1}</a></b></div>", user.Homepage, languageProfile.GetString("LableInfoHomepage"));
                        else
                            sb.AppendFormat("<div class=\"CSB_wdg_info\"><b><a href='http://{0}' target='_blank'>{1}</a></b></div>", user.Homepage, languageProfile.GetString("LableInfoHomepage"));
                    }

                    //////////////////////////////////////////
                    // Blog
                    //////////////////////////////////////////
                    if (user.Blog != null && user.BlogShow && !string.IsNullOrEmpty(user.Blog))
                    {
                        if (user.Blog.StartsWith("http"))
                            sb.AppendFormat("<div class=\"CSB_wdg_info\"><b><a href='{0}' target='_blank'>{1}</a></b></div>", user.Blog, languageProfile.GetString("LableInfoBlog"));
                        else
                            sb.AppendFormat("<div class=\"CSB_wdg_info\"><b><a href='http://{0}' target='_blank'>{1}</a></b></div>", user.Blog, languageProfile.GetString("LableInfoBlog"));
                    }

                    //////////////////////////////////////////
                    // Beruf
                    //////////////////////////////////////////
                    if (user.Beruf != null && user.BerufShow && !string.IsNullOrEmpty(user.Beruf))
                    {
                        sb.AppendFormat("<div class=\"CSB_wdg_info\">{0} <b>{1}</b></div>", languageProfile.GetString("LableInfoBeruf"), user.Beruf);
                    }

                    //////////////////////////////////////////
                    // Motto
                    //////////////////////////////////////////
                    if (user.Lebensmoto != null && user.LebensmotoShow && !string.IsNullOrEmpty(user.Lebensmoto))
                    {
                        sb.AppendFormat("<div class=\"CSB_wdg_info\">{0} <b>{1}</b></div>", languageProfile.GetString("LableInfoMotto"), user.Lebensmoto);
                    }

                    //////////////////////////////////////////
                    // Talente
                    //////////////////////////////////////////
                    PrintProfileData(new List<string>() { "Talent1", "Talent2", "Talent3", "Talent4", "Talent5", "Talent6", "Talent7", "Talent8", "Talent9", "Talent10" }, languageProfile.GetString("LableInfoTelentSigular"), languageProfile.GetString("LableInfoTelentPlural"), sb, user);

                    //Dictionary<string, string> themen = LoadThemen();

                    for (int i = 1; i <= 10; i++)
                    {
                        string DBFieldName = string.Format("InterestTopic{0}", i);
                        //////////////////////////////////////////
                        // InterestTopic
                        //////////////////////////////////////////
                        PropertyInfo propertyInfo = typeof(DataObjectUser).GetProperty(DBFieldName);
                        object interest = propertyInfo.GetValue(user, null);

                        PropertyInfo propertyInfo2 = typeof(DataObjectUser).GetProperty(DBFieldName + "Show");
                        bool interestShow = (bool)propertyInfo2.GetValue(user, null);

                        if (interest != null && interestShow && !string.IsNullOrEmpty(interest.ToString()))
                        {
                            InterestTopic interestTopic = ProfileDataHelper.GetInterestTopic(DBFieldName);
                            //PrintProfileDataThemen(interest.ToString().Trim(';').Split(';'), themen, interestTopic.NameSingular, interestTopic.NamePlural, sb);
                        }
                    }

                    //////////////////////////////////////////
                    // Sonstige Interessen
                    //////////////////////////////////////////
                    PrintProfileData(new List<string>() { "Interesst1", "Interesst2", "Interesst3", "Interesst4", "Interesst5", "Interesst6", "Interesst7", "Interesst8", "Interesst9", "Interesst10" }, languageProfile.GetString("LableInfoInterstSigular"), languageProfile.GetString("LableInfoInterstSigular"), sb, user);

                    sb.AppendFormat("<div class=\"CSB_wdg_info\">{0}:&nbsp;{1}<br/><br/>", languageProfile.GetString("LableInfoMemberSince"), dataObject.Inserted.ToShortDateString());
                    sb.AppendFormat("{0}: {1}</div>", languageProfile.GetString("LabelInfoVisits"), dataObject.ViewCount);
                }
                catch
                { }
            }
            else // DataObject is not a user
            {
                sb.AppendFormat("<div class=\"CSB_wdg_info\">{0}</div>", dataObject.DescriptionLinked);

                string tags = dataObject.TagList;
                string[] tagArray = tags.Split(Constants.TAG_DELIMITER);
                List<string> tagList = new List<string>();
                foreach (string tag in tagArray)
                {
                    tag.TrimStart(new char[] { ' ' });
                    tag.TrimEnd(new char[] { ' ' });
                    if (!tagList.Contains(tag) && !string.IsNullOrEmpty(tag))
                        tagList.Add(tag);
                }

                sb.AppendFormat("<div class=\"CSB_wdg_info\">{0}:<br/>{1} {2}<br/><br/>", languageProfile.GetString("LabelInfoCreated"), dataObject.Inserted.ToShortDateString(), dataObject.Inserted.ToShortTimeString());
                sb.AppendFormat("{0}: {1}</div>", languageProfile.GetString("LabelInfoVisits"), dataObject.ViewCount);

                sb.AppendFormat("<div class=\"CSB_wdg_info\"><b>{0}</b><br/>", languageProfile.GetString("LabelInfoTagPlural"));
                for (int i = 0; i < tagList.Count; i++)
                {
                    sb.AppendFormat("{0}", tagList[i]);
                    if (i != tagList.Count - 1)
                        sb.AppendFormat(" {0} ", Constants.TAG_DELIMITER);
                }
                sb.AppendFormat("</div>");


                if (dataObject.Geo_Lat > double.MinValue)
                {
                    sb.AppendFormat(string.Format(@"<div class=""CSB_msg_img_btn"" style=""width:136px;padding-top:5px;padding-left:10px;""> <a href=""javascript:radWinOpen('{0}/Pages/Popups/DetailMap.aspx?ObjID={1}', '{2}', 640, 480, true)"" ><img src=""{0}/Library/Images/btn/l_green.png"" width=""9"" height=""19""><img src=""{0}/Library/Images/btn/m_green.png"" width=""118"" height=""19""><img src=""{0}/Library/Images/btn/r_green.png"" width=""9"" height=""19""></a> <a href=""javascript:radWinOpen('{0}/Pages/Popups/DetailMap.aspx?ObjID={1}', '{2}', 640, 480, true)"" ><div class=""CSB_msg_img_btn_txt"">{3}</div></a></div>", SiteConfig.SiteVRoot, dataObject.ObjectID, languageShared.GetString("TitleMap").StripForScript(), languageShared.GetString("CommandShowMap")));
                }
            }
            this.LitInfo.Text = sb.ToString();
        }

        //private Dictionary<string, string> LoadThemen()
        //{
        //    Dictionary<string, string> list = new Dictionary<string, string>();
        //    IDataReader idrMans = null;
        //    try
        //    {
        //        idrMans = HitblMainMan.FetchAll();
        //        while (idrMans.Read())
        //        {
        //            list.Add(idrMans[HitblMainMan.Columns.ManId].ToString(), idrMans[HitblMainMan.Columns.ManTitle].ToString());
        //        }
        //    }
        //    finally
        //    {
        //        if (idrMans != null && !idrMans.IsClosed)
        //            idrMans.Close();
        //    }
        //    return list;
        //}

        private void PrintProfileDataThemen(string[] themenIds, Dictionary<string, string> themen, string nameSingular, string namePlural, StringBuilder sb)
        {
            List<string> stringDataList = new List<string>();
            foreach (string themenId in themenIds)
            {
                stringDataList.Add("<b>" + themen[themenId] + "</b>");
            }

            string dataString = string.Empty;
            for (int i = 0; i < stringDataList.Count; i++)
            {
                dataString += stringDataList[i];
                if (i == stringDataList.Count - 2)
                    dataString += string.Format(" {0} ", languageProfile.GetString("LableInfoAnd"));
                else if (i < stringDataList.Count - 1)
                    dataString += ", ";
            }

            if (!string.IsNullOrEmpty(dataString) && stringDataList.Count > 1)
                sb.AppendFormat("<div class=\"CSB_wdg_info\">{0} {1}.</div>", namePlural, dataString);
            else if (!string.IsNullOrEmpty(dataString))
                sb.AppendFormat("<div class=\"CSB_wdg_info\">{0} {1}.</div>", nameSingular, dataString);
        }

        private void PrintProfileData(List<string> dbFields, string nameSingular, string namePlural, StringBuilder sb, DataObjectUser dataObject)
        {
            List<string> stringDataList = new List<string>();
            foreach (string dbField in dbFields)
            {
                PropertyInfo propertyInfo = typeof(DataObjectUser).GetProperty(dbField);
                object interest = propertyInfo.GetValue(dataObject, null);

                PropertyInfo propertyInfo2 = typeof(DataObjectUser).GetProperty(dbField + "Show");
                bool interestShow = (bool)propertyInfo2.GetValue(dataObject, null);

                if (interest != null && interestShow && !string.IsNullOrEmpty(interest.ToString()))
                {
                    stringDataList.Add("<b>" + interest + "</b>");
                }
            }

            string dataString = string.Empty;
            for (int i = 0; i < stringDataList.Count; i++)
            {
                dataString += stringDataList[i];
                if (i == stringDataList.Count - 2)
                    dataString += string.Format(" {0} ", languageProfile.GetString("LableInfoAnd"));
                else if (i < stringDataList.Count - 1)
                    dataString += ", ";
            }

            if (!string.IsNullOrEmpty(dataString) && stringDataList.Count > 1)
                sb.AppendFormat("<div class=\"CSB_wdg_info\">{0} {1}.</div>", namePlural, dataString);
            else if (!string.IsNullOrEmpty(dataString))
                sb.AppendFormat("<div class=\"CSB_wdg_info\">{0} {1}.</div>", nameSingular, dataString);
        }

    }
}