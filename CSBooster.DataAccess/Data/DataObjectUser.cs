// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Data;
using System.Data.SqlClient;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectUser
    {
        public static void FillObject(Business.DataObjectUser item, SqlDataReader sqlReader)
        {
            if (sqlReader["EmphasisList"] != DBNull.Value) item.emphasisListXml.LoadXml(sqlReader["EmphasisList"].ToString());
            if (sqlReader["OpenID"] != DBNull.Value) item.OpenID = sqlReader["OpenID"].ToString();
            if (sqlReader["PPID"] != DBNull.Value) item.PPID = sqlReader["PPID"].ToString();
            if (sqlReader["Vorname"] != DBNull.Value) item.Vorname = sqlReader["Vorname"].ToString();
            item.VornameShow = Convert.ToBoolean(sqlReader["VornameShow"]);
            if (sqlReader["Name"] != DBNull.Value) item.Name = sqlReader["Name"].ToString();
            item.NameShow = Convert.ToBoolean(sqlReader["NameShow"]);
            if (sqlReader["Street"] != DBNull.Value) item.AddressStreet = sqlReader["Street"].ToString();
            item.AddressStreetShow = Convert.ToBoolean(sqlReader["StreetShow"]);
            if (sqlReader["Zip"] != DBNull.Value) item.AddressZip = sqlReader["Zip"].ToString();
            item.AddressZipShow = Convert.ToBoolean(sqlReader["ZipShow"]);
            if (sqlReader["City"] != DBNull.Value) item.AddressCity = sqlReader["City"].ToString();
            item.AddressCityShow = Convert.ToBoolean(sqlReader["CityShow"]);
            if (sqlReader["Land"] != DBNull.Value) item.AddressLand = sqlReader["Land"].ToString();
            item.AddressLandShow = Convert.ToBoolean(sqlReader["LandShow"]);
            if (sqlReader["Languages"] != DBNull.Value) item.Languages = sqlReader["Languages"].ToString();
            item.LanguagesShow = Convert.ToBoolean(sqlReader["LanguagesShow"]);
            if (sqlReader["Sex"] != DBNull.Value) item.Sex = sqlReader["Sex"].ToString();
            item.SexShow = Convert.ToBoolean(sqlReader["SexShow"]);
            if (sqlReader["Birthday"] != DBNull.Value) item.Birthday = (DateTime)sqlReader["Birthday"];
            item.BirthdayShow = Convert.ToBoolean(sqlReader["BirthdayShow"]);
            if (sqlReader["Status"] != DBNull.Value) item.RelationStatus = sqlReader["Status"].ToString();
            item.RelationStatusShow = Convert.ToBoolean(sqlReader["StatusShow"]);
            if (sqlReader["AttractedTo"] != DBNull.Value) item.AttractedTo = sqlReader["AttractedTo"].ToString();
            item.AttractedToShow = Convert.ToBoolean(sqlReader["AttractedToShow"]);
            if (sqlReader["BodyHeight"] != DBNull.Value) item.BodyHeight = Convert.ToInt32(sqlReader["BodyHeight"]);
            item.BodyHeightShow = Convert.ToBoolean(sqlReader["BodyHeightShow"]);
            if (sqlReader["BodyWeight"] != DBNull.Value) item.BodyWeight = Convert.ToInt32(sqlReader["BodyWeight"]);
            item.BodyWeightShow = Convert.ToBoolean(sqlReader["BodyWeightShow"]);
            if (sqlReader["EyeColor"] != DBNull.Value) item.EyeColor = sqlReader["EyeColor"].ToString();
            item.EyeColorShow = Convert.ToBoolean(sqlReader["EyeColorShow"]);
            if (sqlReader["HairColor"] != DBNull.Value) item.HairColor = sqlReader["HairColor"].ToString();
            item.HairColorShow = Convert.ToBoolean(sqlReader["HairColorShow"]);
            if (sqlReader["PriColor"] != DBNull.Value) item.PrimaryColor = sqlReader["PriColor"].ToString();
            item.PrimaryColorShow = Convert.ToBoolean(sqlReader["PriColorShow"]);
            if (sqlReader["SecColor"] != DBNull.Value) item.SecondaryColor = sqlReader["SecColor"].ToString();
            item.SecondaryColorShow = Convert.ToBoolean(sqlReader["SecColorShow"]);
            if (sqlReader["Mobile"] != DBNull.Value) item.Mobile = sqlReader["Mobile"].ToString();
            item.MobileShow = Convert.ToBoolean(sqlReader["MobileShow"]);
            if (sqlReader["Phone"] != DBNull.Value) item.Phone = sqlReader["Phone"].ToString();
            item.PhoneShow = Convert.ToBoolean(sqlReader["PhoneShow"]);
            if (sqlReader["MSN"] != DBNull.Value) item.MSN = sqlReader["MSN"].ToString();
            item.MSNShow = Convert.ToBoolean(sqlReader["MSNShow"]);
            if (sqlReader["Yahoo"] != DBNull.Value) item.Yahoo = sqlReader["Yahoo"].ToString();
            item.YahooShow = Convert.ToBoolean(sqlReader["YahooShow"]);
            if (sqlReader["Skype"] != DBNull.Value) item.Skype = sqlReader["Skype"].ToString();
            item.SkypeShow = Convert.ToBoolean(sqlReader["SkypeShow"]);
            if (sqlReader["ICQ"] != DBNull.Value) item.ICQ = sqlReader["ICQ"].ToString();
            item.ICQShow = Convert.ToBoolean(sqlReader["ICQShow"]);
            if (sqlReader["AIM"] != DBNull.Value) item.AIM = sqlReader["AIM"].ToString();
            item.AIMShow = Convert.ToBoolean(sqlReader["AIMShow"]);
            if (sqlReader["Homepage"] != DBNull.Value) item.Homepage = sqlReader["Homepage"].ToString();
            item.HomepageShow = Convert.ToBoolean(sqlReader["HomepageShow"]);
            if (sqlReader["Blog"] != DBNull.Value) item.Blog = sqlReader["Blog"].ToString();
            item.BlogShow = Convert.ToBoolean(sqlReader["BlogShow"]);
            if (sqlReader["Beruf"] != DBNull.Value) item.Beruf = sqlReader["Beruf"].ToString();
            item.BerufShow = Convert.ToBoolean(sqlReader["BerufShow"]);
            if (sqlReader["Lebensmoto"] != DBNull.Value) item.Lebensmoto = sqlReader["Lebensmoto"].ToString();
            item.LebensmotoShow = Convert.ToBoolean(sqlReader["LebensmotoShow"]);
            if (sqlReader["Talent1"] != DBNull.Value) item.Talent1 = sqlReader["Talent1"].ToString();
            item.Talent1Show = Convert.ToBoolean(sqlReader["Talent1Show"]);
            if (sqlReader["Talent2"] != DBNull.Value) item.Talent2 = sqlReader["Talent2"].ToString();
            item.Talent2Show = Convert.ToBoolean(sqlReader["Talent2Show"]);
            if (sqlReader["Talent3"] != DBNull.Value) item.Talent3 = sqlReader["Talent3"].ToString();
            item.Talent3Show = Convert.ToBoolean(sqlReader["Talent3Show"]);
            if (sqlReader["Talent4"] != DBNull.Value) item.Talent4 = sqlReader["Talent4"].ToString();
            item.Talent4Show = Convert.ToBoolean(sqlReader["Talent4Show"]);
            if (sqlReader["Talent5"] != DBNull.Value) item.Talent5 = sqlReader["Talent5"].ToString();
            item.Talent5Show = Convert.ToBoolean(sqlReader["Talent5Show"]);
            if (sqlReader["Talent6"] != DBNull.Value) item.Talent6 = sqlReader["Talent6"].ToString();
            item.Talent6Show = Convert.ToBoolean(sqlReader["Talent6Show"]);
            if (sqlReader["Talent7"] != DBNull.Value) item.Talent7 = sqlReader["Talent7"].ToString();
            item.Talent7Show = Convert.ToBoolean(sqlReader["Talent7Show"]);
            if (sqlReader["Talent8"] != DBNull.Value) item.Talent8 = sqlReader["Talent8"].ToString();
            item.Talent8Show = Convert.ToBoolean(sqlReader["Talent8Show"]);
            if (sqlReader["Talent9"] != DBNull.Value) item.Talent9 = sqlReader["Talent9"].ToString();
            item.Talent9Show = Convert.ToBoolean(sqlReader["Talent9Show"]);
            if (sqlReader["Talent10"] != DBNull.Value) item.Talent10 = sqlReader["Talent10"].ToString();
            item.Talent10Show = Convert.ToBoolean(sqlReader["Talent10Show"]);
            if (sqlReader["Talent11"] != DBNull.Value) item.Talent11 = sqlReader["Talent11"].ToString();
            item.Talent11Show = Convert.ToBoolean(sqlReader["Talent11Show"]);
            if (sqlReader["Talent12"] != DBNull.Value) item.Talent12 = sqlReader["Talent12"].ToString();
            item.Talent12Show = Convert.ToBoolean(sqlReader["Talent12Show"]);
            if (sqlReader["Talent13"] != DBNull.Value) item.Talent13 = sqlReader["Talent13"].ToString();
            item.Talent13Show = Convert.ToBoolean(sqlReader["Talent13Show"]);
            if (sqlReader["Talent14"] != DBNull.Value) item.Talent14 = sqlReader["Talent14"].ToString();
            item.Talent14Show = Convert.ToBoolean(sqlReader["Talent14Show"]);
            if (sqlReader["Talent15"] != DBNull.Value) item.Talent15 = sqlReader["Talent15"].ToString();
            item.Talent15Show = Convert.ToBoolean(sqlReader["Talent15Show"]);
            if (sqlReader["Talent16"] != DBNull.Value) item.Talent16 = sqlReader["Talent16"].ToString();
            item.Talent16Show = Convert.ToBoolean(sqlReader["Talent16Show"]);
            if (sqlReader["Talent17"] != DBNull.Value) item.Talent17 = sqlReader["Talent17"].ToString();
            item.Talent17Show = Convert.ToBoolean(sqlReader["Talent17Show"]);
            if (sqlReader["Talent18"] != DBNull.Value) item.Talent18 = sqlReader["Talent18"].ToString();
            item.Talent18Show = Convert.ToBoolean(sqlReader["Talent18Show"]);
            if (sqlReader["Talent19"] != DBNull.Value) item.Talent19 = sqlReader["Talent19"].ToString();
            item.Talent19Show = Convert.ToBoolean(sqlReader["Talent19Show"]);
            if (sqlReader["Talent20"] != DBNull.Value) item.Talent20 = sqlReader["Talent20"].ToString();
            item.Talent20Show = Convert.ToBoolean(sqlReader["Talent20Show"]);
            if (sqlReader["Talent21"] != DBNull.Value) item.Talent21 = sqlReader["Talent21"].ToString();
            item.Talent21Show = Convert.ToBoolean(sqlReader["Talent21Show"]);
            if (sqlReader["Talent22"] != DBNull.Value) item.Talent22 = sqlReader["Talent22"].ToString();
            item.Talent22Show = Convert.ToBoolean(sqlReader["Talent22Show"]);
            if (sqlReader["Talent23"] != DBNull.Value) item.Talent23 = sqlReader["Talent23"].ToString();
            item.Talent23Show = Convert.ToBoolean(sqlReader["Talent23Show"]);
            if (sqlReader["Talent24"] != DBNull.Value) item.Talent24 = sqlReader["Talent24"].ToString();
            item.Talent24Show = Convert.ToBoolean(sqlReader["Talent24Show"]);
            if (sqlReader["Talent25"] != DBNull.Value) item.Talent25 = sqlReader["Talent25"].ToString();
            item.Talent25Show = Convert.ToBoolean(sqlReader["Talent25Show"]);
            if (sqlReader["InterestTopic1"] != DBNull.Value) item.InterestTopic1 = sqlReader["InterestTopic1"].ToString();
            item.InterestTopic1Show = Convert.ToBoolean(sqlReader["InterestTopic1Show"]);
            if (sqlReader["InterestTopic2"] != DBNull.Value) item.InterestTopic2 = sqlReader["InterestTopic2"].ToString();
            item.InterestTopic2Show = Convert.ToBoolean(sqlReader["InterestTopic2Show"]);
            if (sqlReader["InterestTopic3"] != DBNull.Value) item.InterestTopic3 = sqlReader["InterestTopic3"].ToString();
            item.InterestTopic3Show = Convert.ToBoolean(sqlReader["InterestTopic3Show"]);
            if (sqlReader["InterestTopic4"] != DBNull.Value) item.InterestTopic4 = sqlReader["InterestTopic4"].ToString();
            item.InterestTopic4Show = Convert.ToBoolean(sqlReader["InterestTopic4Show"]);
            if (sqlReader["InterestTopic5"] != DBNull.Value) item.InterestTopic5 = sqlReader["InterestTopic5"].ToString();
            item.InterestTopic5Show = Convert.ToBoolean(sqlReader["InterestTopic5Show"]);
            if (sqlReader["InterestTopic6"] != DBNull.Value) item.InterestTopic6 = sqlReader["InterestTopic6"].ToString();
            item.InterestTopic6Show = Convert.ToBoolean(sqlReader["InterestTopic6Show"]);
            if (sqlReader["InterestTopic7"] != DBNull.Value) item.InterestTopic7 = sqlReader["InterestTopic7"].ToString();
            item.InterestTopic7Show = Convert.ToBoolean(sqlReader["InterestTopic7Show"]);
            if (sqlReader["InterestTopic8"] != DBNull.Value) item.InterestTopic8 = sqlReader["InterestTopic8"].ToString();
            item.InterestTopic8Show = Convert.ToBoolean(sqlReader["InterestTopic8Show"]);
            if (sqlReader["InterestTopic9"] != DBNull.Value) item.InterestTopic9 = sqlReader["InterestTopic9"].ToString();
            item.InterestTopic9Show = Convert.ToBoolean(sqlReader["InterestTopic9Show"]);
            if (sqlReader["InterestTopic10"] != DBNull.Value) item.InterestTopic10 = sqlReader["InterestTopic10"].ToString();
            item.InterestTopic10Show = Convert.ToBoolean(sqlReader["InterestTopic10Show"]);
            if (sqlReader["Interesst1"] != DBNull.Value) item.Interesst1 = sqlReader["Interesst1"].ToString();
            item.Interesst1Show = Convert.ToBoolean(sqlReader["Interesst1Show"]);
            if (sqlReader["Interesst2"] != DBNull.Value) item.Interesst2 = sqlReader["Interesst2"].ToString();
            item.Interesst2Show = Convert.ToBoolean(sqlReader["Interesst2Show"]);
            if (sqlReader["Interesst3"] != DBNull.Value) item.Interesst3 = sqlReader["Interesst3"].ToString();
            item.Interesst3Show = Convert.ToBoolean(sqlReader["Interesst3Show"]);
            if (sqlReader["Interesst4"] != DBNull.Value) item.Interesst4 = sqlReader["Interesst4"].ToString();
            item.Interesst4Show = Convert.ToBoolean(sqlReader["Interesst4Show"]);
            if (sqlReader["Interesst5"] != DBNull.Value) item.Interesst5 = sqlReader["Interesst5"].ToString();
            item.Interesst5Show = Convert.ToBoolean(sqlReader["Interesst5Show"]);
            if (sqlReader["Interesst6"] != DBNull.Value) item.Interesst6 = sqlReader["Interesst6"].ToString();
            item.Interesst6Show = Convert.ToBoolean(sqlReader["Interesst6Show"]);
            if (sqlReader["Interesst7"] != DBNull.Value) item.Interesst7 = sqlReader["Interesst7"].ToString();
            item.Interesst7Show = Convert.ToBoolean(sqlReader["Interesst7Show"]);
            if (sqlReader["Interesst8"] != DBNull.Value) item.Interesst8 = sqlReader["Interesst8"].ToString();
            item.Interesst8Show = Convert.ToBoolean(sqlReader["Interesst8Show"]);
            if (sqlReader["Interesst9"] != DBNull.Value) item.Interesst9 = sqlReader["Interesst9"].ToString();
            item.Interesst9Show = Convert.ToBoolean(sqlReader["Interesst9Show"]);
            if (sqlReader["Interesst10"] != DBNull.Value) item.Interesst10 = sqlReader["Interesst10"].ToString();
            item.Interesst10Show = Convert.ToBoolean(sqlReader["Interesst10Show"]);
            if (sqlReader["MsgFrom"] != DBNull.Value) item.MsgFrom = sqlReader["MsgFrom"].ToString();
            item.MsgFromShow = Convert.ToBoolean(sqlReader["MsgFromShow"]);
            if (sqlReader["GetMail"] != DBNull.Value) item.GetMail = sqlReader["GetMail"].ToString();
            item.GetMailShow = Convert.ToBoolean(sqlReader["GetMailShow"]);
            item.DisplayAds = sqlReader["DisplayAds"] != DBNull.Value ? Convert.ToBoolean(sqlReader["DisplayAds"]) : true;
            item.DisplayAdsShow = Convert.ToBoolean(sqlReader["DisplayAdsShow"]);
            if (sqlReader["LastActivityDate"] != DBNull.Value) item.IsUserOnline = (Convert.ToDateTime(sqlReader["LastActivityDate"]).Add(Business.DataAccessConfiguration.UserOnlineTimeGap()) > DateTime.Now.ToUniversalTime());
        }

        public static string GetSelectSQL(Business.QuickParameters paras, SqlParameterCollection parameters)
        {
            return @",
      hiobj_User.EmphasisList,
      hiobj_User.OpenID,
      hiobj_User.PPID,
      hiobj_User.Vorname,
      hiobj_User.VornameShow,
      hiobj_User.Name,
      hiobj_User.NameShow,
      hiobj_User.Street,
      hiobj_User.StreetShow,
      hiobj_User.Zip,
      hiobj_User.ZipShow,
      hiobj_User.City,
      hiobj_User.CityShow,
      hiobj_User.Land,
      hiobj_User.LandShow,
      hiobj_User.Languages,
      hiobj_User.LanguagesShow,
      hiobj_User.Sex,
      hiobj_User.SexShow,
      hiobj_User.Birthday,
      hiobj_User.BirthdayShow,
      hiobj_User.Status,
      hiobj_User.StatusShow,
      hiobj_User.AttractedTo,
      hiobj_User.AttractedToShow,
      hiobj_User.BodyHeight,
      hiobj_User.BodyHeightShow,
      hiobj_User.BodyWeight,
      hiobj_User.BodyWeightShow,
      hiobj_User.EyeColor,
      hiobj_User.EyeColorShow,
      hiobj_User.HairColor,
      hiobj_User.HairColorShow,
      hiobj_User.PriColor,
      hiobj_User.PriColorShow,
      hiobj_User.SecColor,
      hiobj_User.SecColorShow,
      hiobj_User.Mobile,
      hiobj_User.MobileShow,
      hiobj_User.Phone,
      hiobj_User.PhoneShow,
      hiobj_User.MSN,
      hiobj_User.MSNShow,
      hiobj_User.Yahoo,
      hiobj_User.YahooShow,
      hiobj_User.Skype,
      hiobj_User.SkypeShow,
      hiobj_User.ICQ,
      hiobj_User.ICQShow,
      hiobj_User.AIM,
      hiobj_User.AIMShow,
      hiobj_User.Homepage,
      hiobj_User.HomepageShow,
      hiobj_User.Blog,
      hiobj_User.BlogShow,
      hiobj_User.Beruf,
      hiobj_User.BerufShow,
      hiobj_User.Lebensmoto,
      hiobj_User.LebensmotoShow,
      hiobj_User.Talent1,
      hiobj_User.Talent1Show,
      hiobj_User.Talent2,
      hiobj_User.Talent2Show,
      hiobj_User.Talent3,
      hiobj_User.Talent3Show,
      hiobj_User.Talent4,
      hiobj_User.Talent4Show,
      hiobj_User.Talent5,
      hiobj_User.Talent5Show,
      hiobj_User.Talent6,
      hiobj_User.Talent6Show,
      hiobj_User.Talent7,
      hiobj_User.Talent7Show,
      hiobj_User.Talent8,
      hiobj_User.Talent8Show,
      hiobj_User.Talent9,
      hiobj_User.Talent9Show,
      hiobj_User.Talent10,
      hiobj_User.Talent10Show,
      hiobj_User.Talent11,
      hiobj_User.Talent11Show,
      hiobj_User.Talent12,
      hiobj_User.Talent12Show,
      hiobj_User.Talent13,
      hiobj_User.Talent13Show,
      hiobj_User.Talent14,
      hiobj_User.Talent14Show,
      hiobj_User.Talent15,
      hiobj_User.Talent15Show,
      hiobj_User.Talent16,
      hiobj_User.Talent16Show,
      hiobj_User.Talent17,
      hiobj_User.Talent17Show,
      hiobj_User.Talent18,
      hiobj_User.Talent18Show,
      hiobj_User.Talent19,
      hiobj_User.Talent19Show,
      hiobj_User.Talent20,
      hiobj_User.Talent20Show,
      hiobj_User.Talent21,
      hiobj_User.Talent21Show,
      hiobj_User.Talent22,
      hiobj_User.Talent22Show,
      hiobj_User.Talent23,
      hiobj_User.Talent23Show,
      hiobj_User.Talent24,
      hiobj_User.Talent24Show,
      hiobj_User.Talent25,
      hiobj_User.Talent25Show,
      hiobj_User.InterestTopic1,
      hiobj_User.InterestTopic1Show,
      hiobj_User.InterestTopic2,
      hiobj_User.InterestTopic2Show,
      hiobj_User.InterestTopic3,
      hiobj_User.InterestTopic3Show,
      hiobj_User.InterestTopic4,
      hiobj_User.InterestTopic4Show,
      hiobj_User.InterestTopic5,
      hiobj_User.InterestTopic5Show,
      hiobj_User.InterestTopic6,
      hiobj_User.InterestTopic6Show,
      hiobj_User.InterestTopic7,
      hiobj_User.InterestTopic7Show,
      hiobj_User.InterestTopic8,
      hiobj_User.InterestTopic8Show,
      hiobj_User.InterestTopic9,
      hiobj_User.InterestTopic9Show,
      hiobj_User.InterestTopic10,
      hiobj_User.InterestTopic10Show,
      hiobj_User.Interesst1,
      hiobj_User.Interesst1Show,
      hiobj_User.Interesst2,
      hiobj_User.Interesst2Show,
      hiobj_User.Interesst3,
      hiobj_User.Interesst3Show,
      hiobj_User.Interesst4,
      hiobj_User.Interesst4Show,
      hiobj_User.Interesst5,
      hiobj_User.Interesst5Show,
      hiobj_User.Interesst6,
      hiobj_User.Interesst6Show,
      hiobj_User.Interesst7,
      hiobj_User.Interesst7Show,
      hiobj_User.Interesst8,
      hiobj_User.Interesst8Show,
      hiobj_User.Interesst9,
      hiobj_User.Interesst9Show,
      hiobj_User.Interesst10,
      hiobj_User.Interesst10Show,
      hiobj_User.MsgFrom,
      hiobj_User.MsgFromShow,
      hiobj_User.GetMail,
      hiobj_User.GetMailShow,
      hiobj_User.DisplayAds,
      hiobj_User.DisplayAdsShow,
      UserObj_aspnet_Users.LastActivityDate
      ";
        }

        public static string GetInsertSQL(Business.DataObjectUser item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return @"INSERT INTO hiobj_User ([OBJ_ID]
      ,[EmphasisList]
      ,[OpenID]
      ,[PPID]
      ,[Vorname]
      ,[VornameShow]
      ,[Name]
      ,[NameShow]
      ,[Street]
      ,[StreetShow]
      ,[Zip]
      ,[ZipShow]
      ,[City]
      ,[CityShow]
      ,[Land]
      ,[LandShow]
      ,[Languages]
      ,[LanguagesShow]
      ,[Sex]
      ,[SexShow]
      ,[Birthday]
      ,[BirthdayShow]
      ,[Status]
      ,[StatusShow]
      ,[AttractedTo]
      ,[AttractedToShow]
      ,[BodyHeight]
      ,[BodyHeightShow]
      ,[BodyWeight]
      ,[BodyWeightShow]
      ,[EyeColor]
      ,[EyeColorShow]
      ,[HairColor]
      ,[HairColorShow]
      ,[PriColor]
      ,[PriColorShow]
      ,[SecColor]
      ,[SecColorShow]
      ,[Mobile]
      ,[MobileShow]
      ,[Phone]
      ,[PhoneShow]
      ,[MSN]
      ,[MSNShow]
      ,[Yahoo]
      ,[YahooShow]
      ,[Skype]
      ,[SkypeShow]
      ,[ICQ]
      ,[ICQShow]
      ,[AIM]
      ,[AIMShow]
      ,[Homepage]
      ,[HomepageShow]
      ,[Blog]
      ,[BlogShow]
      ,[Beruf]
      ,[BerufShow]
      ,[Lebensmoto]
      ,[LebensmotoShow]
      ,[Talent1]
      ,[Talent1Show]
      ,[Talent2]
      ,[Talent2Show]
      ,[Talent3]
      ,[Talent3Show]
      ,[Talent4]
      ,[Talent4Show]
      ,[Talent5]
      ,[Talent5Show]
      ,[Talent6]
      ,[Talent6Show]
      ,[Talent7]
      ,[Talent7Show]
      ,[Talent8]
      ,[Talent8Show]
      ,[Talent9]
      ,[Talent9Show]
      ,[Talent10]
      ,[Talent10Show]
      ,[Talent11]
      ,[Talent11Show]
      ,[Talent12]
      ,[Talent12Show]
      ,[Talent13]
      ,[Talent13Show]
      ,[Talent14]
      ,[Talent14Show]
      ,[Talent15]
      ,[Talent15Show]
      ,[Talent16]
      ,[Talent16Show]
      ,[Talent17]
      ,[Talent17Show]
      ,[Talent18]
      ,[Talent18Show]
      ,[Talent19]
      ,[Talent19Show]
      ,[Talent20]
      ,[Talent20Show]
      ,[Talent21]
      ,[Talent21Show]
      ,[Talent22]
      ,[Talent22Show]
      ,[Talent23]
      ,[Talent23Show]
      ,[Talent24]
      ,[Talent24Show]
      ,[Talent25]
      ,[Talent25Show]
      ,[InterestTopic1]
      ,[InterestTopic1Show]
      ,[InterestTopic2]
      ,[InterestTopic2Show]
      ,[InterestTopic3]
      ,[InterestTopic3Show]
      ,[InterestTopic4]
      ,[InterestTopic4Show]
      ,[InterestTopic5]
      ,[InterestTopic5Show]
      ,[InterestTopic6]
      ,[InterestTopic6Show]
      ,[InterestTopic7]
      ,[InterestTopic7Show]
      ,[InterestTopic8]
      ,[InterestTopic8Show]
      ,[InterestTopic9]
      ,[InterestTopic9Show]
      ,[InterestTopic10]
      ,[InterestTopic10Show]
      ,[Interesst1]
      ,[Interesst1Show]
      ,[Interesst2]
      ,[Interesst2Show]
      ,[Interesst3]
      ,[Interesst3Show]
      ,[Interesst4]
      ,[Interesst4Show]
      ,[Interesst5]
      ,[Interesst5Show]
      ,[Interesst6]
      ,[Interesst6Show]
      ,[Interesst7]
      ,[Interesst7Show]
      ,[Interesst8]
      ,[Interesst8Show]
      ,[Interesst9]
      ,[Interesst9Show]
      ,[Interesst10]
      ,[Interesst10Show]
      ,[MsgFrom]
      ,[MsgFromShow]
      ,[GetMail]
      ,[GetMailShow]
      ,[DisplayAds]
      ,[DisplayAdsShow]) 

        VALUES (@OBJ_ID,
      @EmphasisList,
      @OpenID,
      @PPID,
      @Vorname,
      @VornameShow,
      @Name,
      @NameShow,
      @Street,
      @StreetShow,
      @Zip,
      @ZipShow,
      @City,
      @CityShow,
      @Land,
      @LandShow,
      @Languages,
      @LanguagesShow,
      @Sex,
      @SexShow,
      @Birthday,
      @BirthdayShow,
      @Status,
      @StatusShow,
      @AttractedTo,
      @AttractedToShow,
      @BodyHeight,
      @BodyHeightShow,
      @BodyWeight,
      @BodyWeightShow,
      @EyeColor,
      @EyeColorShow,
      @HairColor,
      @HairColorShow,
      @PriColor,
      @PriColorShow,
      @SecColor,
      @SecColorShow,
      @Mobile,
      @MobileShow,
      @Phone,
      @PhoneShow,
      @MSN,
      @MSNShow,
      @Yahoo,
      @YahooShow,
      @Skype,
      @SkypeShow,
      @ICQ,
      @ICQShow,
      @AIM,
      @AIMShow,
      @Homepage,
      @HomepageShow,
      @Blog,
      @BlogShow,
      @Beruf,
      @BerufShow,
      @Lebensmoto,
      @LebensmotoShow,
      @Talent1,
      @Talent1Show,
      @Talent2,
      @Talent2Show,
      @Talent3,
      @Talent3Show,
      @Talent4,
      @Talent4Show,
      @Talent5,
      @Talent5Show,
      @Talent6,
      @Talent6Show,
      @Talent7,
      @Talent7Show,
      @Talent8,
      @Talent8Show,
      @Talent9,
      @Talent9Show,
      @Talent10,
      @Talent10Show,
      @Talent11,
      @Talent11Show,
      @Talent12,
      @Talent12Show,
      @Talent13,
      @Talent13Show,
      @Talent14,
      @Talent14Show,
      @Talent15,
      @Talent15Show,
      @Talent16,
      @Talent16Show,
      @Talent17,
      @Talent17Show,
      @Talent18,
      @Talent18Show,
      @Talent19,
      @Talent19Show,
      @Talent20,
      @Talent20Show,
      @Talent21,
      @Talent21Show,
      @Talent22,
      @Talent22Show,
      @Talent23,
      @Talent23Show,
      @Talent24,
      @Talent24Show,
      @Talent25,
      @Talent25Show,
      @InterestTopic1,
      @InterestTopic1Show,
      @InterestTopic2,
      @InterestTopic2Show,
      @InterestTopic3,
      @InterestTopic3Show,
      @InterestTopic4,
      @InterestTopic4Show,
      @InterestTopic5,
      @InterestTopic5Show,
      @InterestTopic6,
      @InterestTopic6Show,
      @InterestTopic7,
      @InterestTopic7Show,
      @InterestTopic8,
      @InterestTopic8Show,
      @InterestTopic9,
      @InterestTopic9Show,
      @InterestTopic10,
      @InterestTopic10Show,
      @Interesst1,
      @Interesst1Show,
      @Interesst2,
      @Interesst2Show,
      @Interesst3,
      @Interesst3Show,
      @Interesst4,
      @Interesst4Show,
      @Interesst5,
      @Interesst5Show,
      @Interesst6,
      @Interesst6Show,
      @Interesst7,
      @Interesst7Show,
      @Interesst8,
      @Interesst8Show,
      @Interesst9,
      @Interesst9Show,
      @Interesst10,
      @Interesst10Show,
      @MsgFrom,
      @MsgFromShow,
      @GetMail,
      @GetMailShow,
      @DisplayAds,
      @DisplayAdsShow)
      ";
        }

        public static string GetUpdateSQL(Business.DataObjectUser item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return @"UPDATE hiobj_User 
            SET       [EmphasisList] = @EmphasisList ,
      [OpenID] = @OpenID,
      [PPID] = @PPID ,
      [Vorname] = @Vorname ,
      [VornameShow] = @VornameShow ,
      [Name] = @Name ,
      [NameShow] = @NameShow ,
      [Street] = @Street ,
      [StreetShow] = @StreetShow ,
      [Zip] = @Zip ,
      [ZipShow] = @ZipShow ,
      [City] = @City ,
      [CityShow] = @CityShow ,
      [Land] = @Land ,
      [LandShow] = @LandShow ,
      [Languages] = @Languages ,
      [LanguagesShow] = @LanguagesShow ,
      [Sex] = @Sex ,
      [SexShow] = @SexShow ,
      [Birthday] = @Birthday ,
      [BirthdayShow] = @BirthdayShow ,
      [Status] = @Status ,
      [StatusShow] = @StatusShow ,
      [AttractedTo] = @AttractedTo ,
      [AttractedToShow] = @AttractedToShow ,
      [BodyHeight] = @BodyHeight ,
      [BodyHeightShow] = @BodyHeightShow ,
      [BodyWeight] = @BodyWeight ,
      [BodyWeightShow] = @BodyWeightShow ,
      [EyeColor] = @EyeColor ,
      [EyeColorShow] = @EyeColorShow ,
      [HairColor] = @HairColor ,
      [HairColorShow] = @HairColorShow ,
      [PriColor] = @PriColor ,
      [PriColorShow] = @PriColorShow ,
      [SecColor] = @SecColor ,
      [SecColorShow] = @SecColorShow ,
      [Mobile] = @Mobile ,
      [MobileShow] = @MobileShow ,
      [Phone] = @Phone ,
      [PhoneShow] = @PhoneShow ,
      [MSN] = @MSN ,
      [MSNShow] = @MSNShow ,
      [Yahoo] = @Yahoo ,
      [YahooShow] = @YahooShow ,
      [Skype] = @Skype ,
      [SkypeShow] = @SkypeShow ,
      [ICQ] = @ICQ ,
      [ICQShow] = @ICQShow,
      [AIM] = @AIM ,
      [AIMShow] = @AIMShow ,
      [Homepage] = @Homepage ,
      [HomepageShow] = @HomepageShow ,
      [Blog] = @Blog ,
      [BlogShow] = @BlogShow ,
      [Beruf] = @Beruf ,
      [BerufShow] = @BerufShow ,
      [Lebensmoto] = @Lebensmoto ,
      [LebensmotoShow] = @LebensmotoShow ,
      [Talent1] = @Talent1 ,
      [Talent1Show] = @Talent1Show ,
      [Talent2] = @Talent2 ,
      [Talent2Show] = @Talent2Show ,
      [Talent3] = @Talent3 ,
      [Talent3Show] = @Talent3Show ,
      [Talent4] = @Talent4 ,
      [Talent4Show] = @Talent4Show ,
      [Talent5] = @Talent5 ,
      [Talent5Show] = @Talent5Show ,
      [Talent6] = @Talent6 ,
      [Talent6Show] = @Talent6Show ,
      [Talent7] = @Talent7 ,
      [Talent7Show] = @Talent7Show ,
      [Talent8] = @Talent8 ,
      [Talent8Show] = @Talent8Show ,
      [Talent9] = @Talent9 ,
      [Talent9Show] = @Talent9Show ,
      [Talent10] = @Talent10 ,
      [Talent10Show] = @Talent10Show ,
      [Talent11] = @Talent11 ,
      [Talent11Show] = @Talent11Show ,
      [Talent12] = @Talent12 ,
      [Talent12Show] = @Talent12Show ,
      [Talent13] = @Talent13 ,
      [Talent13Show] = @Talent13Show ,
      [Talent14] = @Talent14 ,
      [Talent14Show] = @Talent14Show ,
      [Talent15] = @Talent15 ,
      [Talent15Show] = @Talent15Show ,
      [Talent16] = @Talent16 ,
      [Talent16Show] = @Talent16Show ,
      [Talent17] = @Talent17 ,
      [Talent17Show] = @Talent17Show ,
      [Talent18] = @Talent18 ,
      [Talent18Show] = @Talent18Show ,
      [Talent19] = @Talent19 ,
      [Talent19Show] = @Talent19Show ,
      [Talent20] = @Talent20 ,
      [Talent20Show] = @Talent20Show ,
      [Talent21] = @Talent21 ,
      [Talent21Show] = @Talent21Show ,
      [Talent22] = @Talent22 ,
      [Talent22Show] = @Talent22Show ,
      [Talent23] = @Talent23 ,
      [Talent23Show] = @Talent23Show ,
      [Talent24] = @Talent24 ,
      [Talent24Show] = @Talent24Show ,
      [Talent25] = @Talent25 ,
      [Talent25Show] = @Talent25Show ,
      [InterestTopic1] = @InterestTopic1 ,
      [InterestTopic1Show] = @InterestTopic1Show ,
      [InterestTopic2] = @InterestTopic2 ,
      [InterestTopic2Show] = @InterestTopic2Show ,
      [InterestTopic3] = @InterestTopic3 ,
      [InterestTopic3Show] = @InterestTopic3Show ,
      [InterestTopic4] = @InterestTopic4 ,
      [InterestTopic4Show] = @InterestTopic4Show ,
      [InterestTopic5] = @InterestTopic5 ,
      [InterestTopic5Show] = @InterestTopic5Show ,
      [InterestTopic6] = @InterestTopic6 ,
      [InterestTopic6Show] = @InterestTopic6Show ,
      [InterestTopic7] = @InterestTopic7 ,
      [InterestTopic7Show] = @InterestTopic7Show ,
      [InterestTopic8] = @InterestTopic8 ,
      [InterestTopic8Show] = @InterestTopic8Show ,
      [InterestTopic9] = @InterestTopic9 ,
      [InterestTopic9Show] = @InterestTopic9Show ,
      [InterestTopic10] = @InterestTopic10 ,
      [InterestTopic10Show] = @InterestTopic10Show ,
      [Interesst1] = @Interesst1 ,
      [Interesst1Show] = @Interesst1Show ,
      [Interesst2] = @Interesst2 ,
      [Interesst2Show] = @Interesst2Show ,
      [Interesst3] = @Interesst3 ,
      [Interesst3Show] = @Interesst3Show ,
      [Interesst4] = @Interesst4 ,
      [Interesst4Show] = @Interesst4Show ,
      [Interesst5] = @Interesst5 ,
      [Interesst5Show] = @Interesst5Show ,
      [Interesst6] = @Interesst6 ,
      [Interesst6Show] = @Interesst6Show ,
      [Interesst7] = @Interesst7 ,
      [Interesst7Show] = @Interesst7Show ,
      [Interesst8] = @Interesst8 ,
      [Interesst8Show] = @Interesst8Show ,
      [Interesst9] = @Interesst9 ,
      [Interesst9Show] = @Interesst9Show ,
      [Interesst10] = @Interesst10 ,
      [Interesst10Show] = @Interesst10Show ,
      [MsgFrom] = @MsgFrom ,
      [MsgFromShow] = @MsgFromShow ,
      [GetMail] = @GetMail ,
      [GetMailShow] = @GetMailShow ,
      [DisplayAds] = @DisplayAds ,
      [DisplayAdsShow] = @DisplayAdsShow";
        }

        public static string GetJoinSQL(Business.QuickParameters paras, SqlParameterCollection parameters)
        {
            string joinString = @"INNER JOIN hiobj_User ON (hiobj_User.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID)
                     INNER JOIN aspnet_Users as UserObj_aspnet_Users ON hiobj_User.OBJ_ID = UserObj_aspnet_Users.UserId
                   ";

            if (paras != null && paras is Business.QuickParametersUser)
            {
                Business.QuickParametersUser qpUser = paras as Business.QuickParametersUser;
                if (qpUser.CommunityIDMember.HasValue)
                {
                    joinString += @"inner Join hirel_Community_User_CUR As UserObj_CommUserRel on (hiobj_User.OBJ_ID = UserObj_CommUserRel.USR_ID)
                               ";
                }
                if (qpUser.IsUserLocked.HasValue)
                {
                    joinString += @"inner Join aspnet_Membership As UserObj_aspnet_Membership on (hiobj_User.OBJ_ID = UserObj_aspnet_Membership.UserId)
                               ";
                }
            }
            return joinString;
        }

        public static string GetWhereSQL(Business.QuickParameters paras, SqlParameterCollection parameters)
        {
            string whereSentence = string.Empty;

            if (paras != null && paras is Business.QuickParametersUser)
            {
                Business.QuickParametersUser qpUser = paras as Business.QuickParametersUser;
                try
                {
                    if (qpUser.IsOnline.HasValue)
                    {
                        if (qpUser.IsOnline.Value)
                        {
                            DateTime dat = DateTime.Now.ToUniversalTime().Subtract(Business.DataAccessConfiguration.UserOnlineTimeGap());
                            whereSentence += string.Format("(LastActivityDate > CONVERT(DATETIME,'{0}-{1}-{2} {3}:{4}:{5}',120)) AND\r\n", dat.Year, dat.Month, dat.Day, dat.Hour, dat.Minute, dat.Second);
                        }
                        else
                        {
                            DateTime dat = DateTime.Now.ToUniversalTime().Subtract(Business.DataAccessConfiguration.UserOnlineTimeGap());
                            whereSentence += string.Format("(LastActivityDate < CONVERT(DATETIME,'{0}-{1}-{2} {3}:{4}:{5}',120)) AND\r\n", dat.Year, dat.Month, dat.Day, dat.Hour, dat.Minute, dat.Second);
                        }
                    }
                    if (!string.IsNullOrEmpty(qpUser.Vorname))
                    {
                        whereSentence += string.Format("((hiobj_User.Vorname like N'%{0}%')  AND (hiobj_User.VornameShow = 1)) AND\r\n", qpUser.Vorname.Replace("'", "''"));
                    }
                    if (!string.IsNullOrEmpty(qpUser.Name))
                    {
                        whereSentence += string.Format("((hiobj_User.Name like N'%{0}%')  AND (hiobj_User.NameShow = 1)) AND\r\n", qpUser.Name.Replace("'", "''"));
                    }
                    if (!string.IsNullOrEmpty(qpUser.Sex))
                    {
                        whereSentence += string.Format("((hiobj_User.Sex like N';{0};')  AND (hiobj_User.SexShow = 1)) AND\r\n", qpUser.Sex.Replace("'", "''"));
                    }
                    if (!string.IsNullOrEmpty(qpUser.RelationStatus))
                    {
                        whereSentence += string.Format("((hiobj_User.Status like N';{0};')  AND (hiobj_User.StatusShow = 1)) AND\r\n", qpUser.RelationStatus.Replace("'", "''"));
                    }
                    if (!string.IsNullOrEmpty(qpUser.AttractedTo))
                    {
                        whereSentence += string.Format("((hiobj_User.AttractedTo like N';{0};')  AND (hiobj_User.AttractedToShow = 1)) AND\r\n", qpUser.AttractedTo.Replace("'", "''"));
                    }
                    if (!string.IsNullOrEmpty(qpUser.EyeColor))
                    {
                        whereSentence += string.Format("((hiobj_User.EyeColor like N';{0};')  AND (hiobj_User.EyeColorShow = 1)) AND\r\n", qpUser.EyeColor.Replace("'", "''"));
                    }
                    if (!string.IsNullOrEmpty(qpUser.HairColor))
                    {
                        whereSentence += string.Format("((hiobj_User.HairColor like N';{0};')  AND (hiobj_User.HairColorShow = 1)) AND\r\n", qpUser.HairColor.Replace("'", "''"));
                    }
                    if (qpUser.BodyWeightFrom.HasValue || qpUser.BodyWeightTo.HasValue)
                    {
                        int searchValueMin = qpUser.BodyWeightFrom.HasValue ? qpUser.BodyWeightFrom.Value : 0;
                        int searchValueMax = qpUser.BodyWeightTo.HasValue ? qpUser.BodyWeightTo.Value : 999;
                        whereSentence += string.Format("((hiobj_User.BodyWeight Between {0} AND {1})  AND (hiobj_User.BodyWeightShow = 1)) AND\r\n", searchValueMin, searchValueMax);

                    }
                    if (qpUser.BodyHeightFrom.HasValue || qpUser.BodyHeightTo.HasValue)
                    {
                        int searchValueMin = qpUser.BodyHeightFrom.HasValue ? qpUser.BodyHeightFrom.Value : 0;
                        int searchValueMax = qpUser.BodyHeightTo.HasValue ? qpUser.BodyHeightTo.Value : 999;
                        whereSentence += string.Format("((hiobj_User.BodyHeight Between {0} AND {1})  AND (hiobj_User.BodyHeightShow = 1)) AND\r\n", searchValueMin, searchValueMax);

                    }
                    if (qpUser.AgeFrom.HasValue || qpUser.AgeTo.HasValue)
                    {
                        DateTime searchValueMin = qpUser.AgeFrom.HasValue ? DateTime.Now.AddYears(-1* qpUser.AgeFrom.Value).GetStartOfYear() : DateTime.Now.AddYears(-120);
                        DateTime searchValueMax = qpUser.AgeFrom.HasValue ? DateTime.Now.AddYears(-1 * qpUser.AgeFrom.Value).GetEndOfYear() : DateTime.Now;
                        whereSentence += string.Format("((hiobj_User.Birthday Between CONVERT(DATETIME,'{0}-{1}-{2} {3}:{4}:{5}',120) AND CONVERT(DATETIME,'{6}-{7}-{8} {9}:{10}:{11}',120))  AND (hiobj_User.BirthdayShow = 1)) AND\r\n", searchValueMin.Year, searchValueMin.Month, searchValueMin.Day, searchValueMin.Hour, searchValueMin.Minute, searchValueMin.Second, searchValueMax.Year, searchValueMax.Month, searchValueMax.Day, searchValueMax.Hour, searchValueMax.Minute, searchValueMax.Second);
                    }
                    if (qpUser.CommunityIDMember.HasValue)
                    {
                        whereSentence += string.Format("(UserObj_CommUserRel.CTY_ID = '{0}') AND\r\n", qpUser.CommunityIDMember.Value);
                    }
                    if (!string.IsNullOrEmpty(qpUser.AddressCity))
                    {
                        whereSentence += string.Format("((hiobj_User.City like  N'%{0}%')  AND (hiobj_User.CityShow = 1)) AND\r\n", qpUser.AddressCity.Replace("'", "''"));
                    }

                    if (!string.IsNullOrEmpty(qpUser.AddressZip))
                    {
                        int searchRangeKm =0;
                        if (qpUser.AddressRangeKM.HasValue)
                        {
                            searchRangeKm = qpUser.AddressRangeKM.Value;
                        }
                        if (!qpUser.AddressRangeKM.HasValue)
                            whereSentence += string.Format("((hiobj_User.Zip =  N'{0}')  AND (hiobj_User.ZipShow = 1)) AND\r\n", qpUser.AddressZip.Replace("'", "''"));
                        else if (qpUser.AddressRangeKM.HasValue)
                        {
                            if (searchRangeKm == 0)
                            {
                                whereSentence += string.Format("not ((hiobj_User.Zip ='')  AND (hiobj_User.ZipShow = 1)) AND (ISNULL(dbo.hifu_PLZ_Distance(hiobj_User.Zip , 'ch', '{0}', 'ch'), 999999) <= {1})  AND\r\n", qpUser.AddressZip.Replace("'", "''"),1);
                            }
                            else
                            {
                                whereSentence += string.Format("not ((hiobj_User.Zip ='')  AND (hiobj_User.ZipShow = 1)) AND (ISNULL(dbo.hifu_PLZ_Distance(hiobj_User.Zip , 'ch', '{0}', 'ch'), 999999) <= {1})  AND\r\n", qpUser.AddressZip.Replace("'", "''"), searchRangeKm);

                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(qpUser.Talent))
                    {
                        string talent = qpUser.Talent.Replace("'", "''");
                        whereSentence += string.Format("(\r\n");
                        whereSentence += string.Format("(hiobj_User.Talent1 Like N'%{0}%' AND hiobj_User.Talent1Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent2 Like N'%{0}%' AND hiobj_User.Talent2Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent3 Like N'%{0}%' AND hiobj_User.Talent3Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent4 Like N'%{0}%' AND hiobj_User.Talent4Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent5 Like N'%{0}%' AND hiobj_User.Talent5Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent6 Like N'%{0}%' AND hiobj_User.Talent6Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent7 Like N'%{0}%' AND hiobj_User.Talent7Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent8 Like N'%{0}%' AND hiobj_User.Talent8Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent9 Like N'%{0}%' AND hiobj_User.Talent9Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent10 Like N'%{0}%' AND hiobj_User.Talent10Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent11 Like N'%{0}%' AND hiobj_User.Talent11Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent12 Like N'%{0}%' AND hiobj_User.Talent12Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent13 Like N'%{0}%' AND hiobj_User.Talent13Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent14 Like N'%{0}%' AND hiobj_User.Talent14Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent15 Like N'%{0}%' AND hiobj_User.Talent15Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent16 Like N'%{0}%' AND hiobj_User.Talent16Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent17 Like N'%{0}%' AND hiobj_User.Talent17Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent18 Like N'%{0}%' AND hiobj_User.Talent18Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent19 Like N'%{0}%' AND hiobj_User.Talent19Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent20 Like N'%{0}%' AND hiobj_User.Talent20Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent21 Like N'%{0}%' AND hiobj_User.Talent21Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent22 Like N'%{0}%' AND hiobj_User.Talent22Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent23 Like N'%{0}%' AND hiobj_User.Talent23Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent24 Like N'%{0}%' AND hiobj_User.Talent24Show=1) OR \r\n", talent);
                        whereSentence += string.Format("(hiobj_User.Talent25 Like N'%{0}%' AND hiobj_User.Talent25Show=1) ", talent);
                        whereSentence += string.Format(") AND\r\n");
                    }
                    if (!string.IsNullOrEmpty(qpUser.InterestTopic))
                    {
                        string interestTopic = qpUser.InterestTopic.Replace("'", "''");
                        whereSentence += string.Format("(\r\n");
                        whereSentence += string.Format("(hiobj_User.InterestTopic1 Like N'%{0}%' AND hiobj_User.InterestTopic1Show=1) OR \r\n", interestTopic);
                        whereSentence += string.Format("(hiobj_User.InterestTopic2 Like N'%{0}%' AND hiobj_User.InterestTopic2Show=1) OR \r\n", interestTopic);
                        whereSentence += string.Format("(hiobj_User.InterestTopic3 Like N'%{0}%' AND hiobj_User.InterestTopic3Show=1) OR \r\n", interestTopic);
                        whereSentence += string.Format("(hiobj_User.InterestTopic4 Like N'%{0}%' AND hiobj_User.InterestTopic4Show=1) OR \r\n", interestTopic);
                        whereSentence += string.Format("(hiobj_User.InterestTopic5 Like N'%{0}%' AND hiobj_User.InterestTopic5Show=1) OR \r\n", interestTopic);
                        whereSentence += string.Format("(hiobj_User.InterestTopic6 Like N'%{0}%' AND hiobj_User.InterestTopic6Show=1) OR \r\n", interestTopic);
                        whereSentence += string.Format("(hiobj_User.InterestTopic7 Like N'%{0}%' AND hiobj_User.InterestTopic7Show=1) OR \r\n", interestTopic);
                        whereSentence += string.Format("(hiobj_User.InterestTopic8 Like N'%{0}%' AND hiobj_User.InterestTopic8Show=1) OR \r\n", interestTopic);
                        whereSentence += string.Format("(hiobj_User.InterestTopic9 Like N'%{0}%' AND hiobj_User.InterestTopic9Show=1) OR \r\n", interestTopic);
                        whereSentence += string.Format("(hiobj_User.InterestTopic10 Like N'%{0}%' AND hiobj_User.InterestTopic10Show=1) \r\n", interestTopic);
                        whereSentence += string.Format(") AND\r\n");
                    }
                    if (!string.IsNullOrEmpty(qpUser.Interesst))
                    {
                        string interesst = qpUser.Interesst.Replace("'", "''");
                        whereSentence += string.Format("(\r\n");
                        whereSentence += string.Format("(hiobj_User.Interesst1 Like N'%{0}%' AND hiobj_User.Interesst1Show=1) OR \r\n", interesst);
                        whereSentence += string.Format("(hiobj_User.Interesst2 Like N'%{0}%' AND hiobj_User.Interesst2Show=1) OR \r\n", interesst);
                        whereSentence += string.Format("(hiobj_User.Interesst3 Like N'%{0}%' AND hiobj_User.Interesst3Show=1) OR \r\n", interesst);
                        whereSentence += string.Format("(hiobj_User.Interesst4 Like N'%{0}%' AND hiobj_User.Interesst4Show=1) OR \r\n", interesst);
                        whereSentence += string.Format("(hiobj_User.Interesst5 Like N'%{0}%' AND hiobj_User.Interesst5Show=1) OR \r\n", interesst);
                        whereSentence += string.Format("(hiobj_User.Interesst6 Like N'%{0}%' AND hiobj_User.Interesst6Show=1) OR \r\n", interesst);
                        whereSentence += string.Format("(hiobj_User.Interesst7 Like N'%{0}%' AND hiobj_User.Interesst7Show=1) OR \r\n", interesst);
                        whereSentence += string.Format("(hiobj_User.Interesst8 Like N'%{0}%' AND hiobj_User.Interesst8Show=1) OR \r\n", interesst);
                        whereSentence += string.Format("(hiobj_User.Interesst9 Like N'%{0}%' AND hiobj_User.Interesst9Show=1) OR \r\n", interesst);
                        whereSentence += string.Format("(hiobj_User.Interesst10 Like N'%{0}%' AND hiobj_User.Interesst10Show=1) \r\n", interesst);
                        whereSentence += string.Format(") AND\r\n");
                    }
                    if (qpUser.IsUserLocked.HasValue)
                    {
                       
                         whereSentence += string.Format("(UserObj_aspnet_Membership.IsLockedOut = {0}) AND\r\n",Convert.ToInt32(qpUser.IsUserLocked.Value));
                   }
                }
                catch
                {
                    whereSentence = string.Empty;
                }
            }
            return whereSentence;
        }

        public static string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            string retString = string.Empty;
            return retString;
        }

        public static string GetOrderBySQL(Business.QuickParameters paras, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        private static void SetParameters(Business.DataObjectUser item, SqlParameterCollection parameters)
        {
            parameters.AddWithValue("@EmphasisList", item.emphasisListXml.OuterXml);

            if (!string.IsNullOrEmpty(item.OpenID)) parameters.AddWithValue("@OpenID", item.OpenID);
            else parameters.AddWithValue("@OpenID", DBNull.Value);

            if (!string.IsNullOrEmpty(item.PPID)) parameters.AddWithValue("@PPID", item.PPID);
            else parameters.AddWithValue("@PPID", DBNull.Value);

            if (!string.IsNullOrEmpty(item.Vorname)) parameters.AddWithValue("@Vorname", item.Vorname);
            else parameters.AddWithValue("@Vorname", DBNull.Value);
            parameters.AddWithValue("@VornameShow", item.VornameShow);

            if (!string.IsNullOrEmpty(item.Name)) parameters.AddWithValue("@Name", item.Name);
            else parameters.AddWithValue("@Name", DBNull.Value);
            parameters.AddWithValue("@NameShow", item.NameShow);

            if (!string.IsNullOrEmpty(item.AddressStreet)) parameters.AddWithValue("@Street", item.AddressStreet);
            else parameters.AddWithValue("@Street", DBNull.Value);
            parameters.AddWithValue("@StreetShow", item.AddressStreetShow);

            if (!string.IsNullOrEmpty(item.AddressZip)) parameters.AddWithValue("@Zip", item.AddressZip);
            else parameters.AddWithValue("@Zip", DBNull.Value);
            parameters.AddWithValue("@ZipShow", item.AddressZipShow);

            if (!string.IsNullOrEmpty(item.AddressCity)) parameters.AddWithValue("@City", item.AddressCity);
            else parameters.AddWithValue("@City", DBNull.Value);
            parameters.AddWithValue("@CityShow", item.AddressCityShow);

            if (!string.IsNullOrEmpty(item.AddressLand)) parameters.AddWithValue("@Land", item.AddressLand);
            else parameters.AddWithValue("@Land", DBNull.Value);
            parameters.AddWithValue("@LandShow", item.AddressLandShow);

            if (!string.IsNullOrEmpty(item.Languages)) parameters.AddWithValue("@Languages", item.Languages);
            else parameters.AddWithValue("@Languages", DBNull.Value);
            parameters.AddWithValue("@LanguagesShow", item.LanguagesShow);

            if (!string.IsNullOrEmpty(item.Sex)) parameters.AddWithValue("@Sex", item.Sex);
            else parameters.AddWithValue("@Sex", DBNull.Value);
            parameters.AddWithValue("@SexShow", item.SexShow);

            if (item.Birthday != null) parameters.AddWithValue("@Birthday", item.Birthday);
            else parameters.AddWithValue("@Birthday", DBNull.Value);
            parameters.AddWithValue("@BirthdayShow", item.BirthdayShow);

            if (!string.IsNullOrEmpty(item.RelationStatus)) parameters.AddWithValue("@Status", item.RelationStatus);
            else parameters.AddWithValue("@Status", DBNull.Value);
            parameters.AddWithValue("@StatusShow", item.RelationStatusShow);

            if (!string.IsNullOrEmpty(item.AttractedTo)) parameters.AddWithValue("@AttractedTo", item.AttractedTo);
            else parameters.AddWithValue("@AttractedTo", DBNull.Value);
            parameters.AddWithValue("@AttractedToShow", item.AttractedToShow);

            if (item.BodyHeight > 0) parameters.AddWithValue("@BodyHeight", item.BodyHeight);
            else parameters.AddWithValue("@BodyHeight", DBNull.Value);
            parameters.AddWithValue("@BodyHeightShow", item.BodyHeightShow);

            if (item.BodyWeight > 0) parameters.AddWithValue("@BodyWeight", item.BodyWeight);
            else parameters.AddWithValue("@BodyWeight", DBNull.Value);
            parameters.AddWithValue("@BodyWeightShow", item.BodyWeightShow);

            if (!string.IsNullOrEmpty(item.EyeColor)) parameters.AddWithValue("@EyeColor", item.EyeColor);
            else parameters.AddWithValue("@EyeColor", DBNull.Value);
            parameters.AddWithValue("@EyeColorShow", item.EyeColorShow);

            if (!string.IsNullOrEmpty(item.HairColor)) parameters.AddWithValue("@HairColor", item.HairColor);
            else parameters.AddWithValue("@HairColor", DBNull.Value);
            parameters.AddWithValue("@HairColorShow", item.HairColorShow);

            if (!string.IsNullOrEmpty(item.PrimaryColor)) parameters.AddWithValue("@PriColor", item.PrimaryColor);
            else parameters.AddWithValue("@PriColor", DBNull.Value);
            parameters.AddWithValue("@PriColorShow", item.PrimaryColorShow);

            if (!string.IsNullOrEmpty(item.SecondaryColor)) parameters.AddWithValue("@SecColor", item.SecondaryColor);
            else parameters.AddWithValue("@SecColor", DBNull.Value);
            parameters.AddWithValue("@SecColorShow", item.SecondaryColorShow);

            if (!string.IsNullOrEmpty(item.Mobile)) parameters.AddWithValue("@Mobile", item.Mobile);
            else parameters.AddWithValue("@Mobile", DBNull.Value);
            parameters.AddWithValue("@MobileShow", item.MobileShow);

            if (!string.IsNullOrEmpty(item.Phone)) parameters.AddWithValue("@Phone", item.Phone);
            else parameters.AddWithValue("@Phone", DBNull.Value);
            parameters.AddWithValue("@PhoneShow", item.PhoneShow);

            if (!string.IsNullOrEmpty(item.MSN)) parameters.AddWithValue("@MSN", item.MSN);
            else parameters.AddWithValue("@MSN", DBNull.Value);
            parameters.AddWithValue("@MSNShow", item.MSNShow);

            if (!string.IsNullOrEmpty(item.Yahoo)) parameters.AddWithValue("@Yahoo", item.Yahoo);
            else parameters.AddWithValue("@Yahoo", DBNull.Value);
            parameters.AddWithValue("@YahooShow", item.YahooShow);

            if (!string.IsNullOrEmpty(item.Skype)) parameters.AddWithValue("@Skype", item.Skype);
            else parameters.AddWithValue("@Skype", DBNull.Value);
            parameters.AddWithValue("@SkypeShow", item.SkypeShow);

            if (!string.IsNullOrEmpty(item.ICQ)) parameters.AddWithValue("@ICQ", item.ICQ);
            else parameters.AddWithValue("@ICQ", DBNull.Value);
            parameters.AddWithValue("@ICQShow", item.ICQShow);

            if (!string.IsNullOrEmpty(item.AIM)) parameters.AddWithValue("@AIM", item.AIM);
            else parameters.AddWithValue("@AIM", DBNull.Value);
            parameters.AddWithValue("@AIMShow", item.AIMShow);

            if (!string.IsNullOrEmpty(item.Homepage)) parameters.AddWithValue("@Homepage", item.Homepage);
            else parameters.AddWithValue("@Homepage", DBNull.Value);
            parameters.AddWithValue("@HomepageShow", item.HomepageShow);

            if (!string.IsNullOrEmpty(item.Blog)) parameters.AddWithValue("@Blog", item.Blog);
            else parameters.AddWithValue("@Blog", DBNull.Value);
            parameters.AddWithValue("@BlogShow", item.BlogShow);

            if (!string.IsNullOrEmpty(item.Beruf)) parameters.AddWithValue("@Beruf", item.Beruf);
            else parameters.AddWithValue("@Beruf", DBNull.Value);
            parameters.AddWithValue("@BerufShow", item.BerufShow);

            if (!string.IsNullOrEmpty(item.Lebensmoto)) parameters.AddWithValue("@Lebensmoto", item.Lebensmoto);
            else parameters.AddWithValue("@Lebensmoto", DBNull.Value);
            parameters.AddWithValue("@LebensmotoShow", item.LebensmotoShow);

            if (!string.IsNullOrEmpty(item.Talent1)) parameters.AddWithValue("@Talent1", item.Talent1);
            else parameters.AddWithValue("@Talent1", DBNull.Value);
            parameters.AddWithValue("@Talent1Show", item.Talent1Show);

            if (!string.IsNullOrEmpty(item.Talent2)) parameters.AddWithValue("@Talent2", item.Talent2);
            else parameters.AddWithValue("@Talent2", DBNull.Value);
            parameters.AddWithValue("@Talent2Show", item.Talent2Show);

            if (!string.IsNullOrEmpty(item.Talent3)) parameters.AddWithValue("@Talent3", item.Talent3);
            else parameters.AddWithValue("@Talent3", DBNull.Value);
            parameters.AddWithValue("@Talent3Show", item.Talent3Show);

            if (!string.IsNullOrEmpty(item.Talent4)) parameters.AddWithValue("@Talent4", item.Talent4);
            else parameters.AddWithValue("@Talent4", DBNull.Value);
            parameters.AddWithValue("@Talent4Show", item.Talent4Show);

            if (!string.IsNullOrEmpty(item.Talent5)) parameters.AddWithValue("@Talent5", item.Talent5);
            else parameters.AddWithValue("@Talent5", DBNull.Value);
            parameters.AddWithValue("@Talent5Show", item.Talent5Show);

            if (!string.IsNullOrEmpty(item.Talent6)) parameters.AddWithValue("@Talent6", item.Talent6);
            else parameters.AddWithValue("@Talent6", DBNull.Value);
            parameters.AddWithValue("@Talent6Show", item.Talent6Show);

            if (!string.IsNullOrEmpty(item.Talent7)) parameters.AddWithValue("@Talent7", item.Talent7);
            else parameters.AddWithValue("@Talent7", DBNull.Value);
            parameters.AddWithValue("@Talent7Show", item.Talent7Show);

            if (!string.IsNullOrEmpty(item.Talent8)) parameters.AddWithValue("@Talent8", item.Talent8);
            else parameters.AddWithValue("@Talent8", DBNull.Value);
            parameters.AddWithValue("@Talent8Show", item.Talent8Show);

            if (!string.IsNullOrEmpty(item.Talent9)) parameters.AddWithValue("@Talent9", item.Talent9);
            else parameters.AddWithValue("@Talent9", DBNull.Value);
            parameters.AddWithValue("@Talent9Show", item.Talent9Show);

            if (!string.IsNullOrEmpty(item.Talent10)) parameters.AddWithValue("@Talent10", item.Talent10);
            else parameters.AddWithValue("@Talent10", DBNull.Value);
            parameters.AddWithValue("@Talent10Show", item.Talent10Show);

            if (!string.IsNullOrEmpty(item.Talent11)) parameters.AddWithValue("@Talent11", item.Talent11);
            else parameters.AddWithValue("@Talent11", DBNull.Value);
            parameters.AddWithValue("@Talent11Show", item.Talent11Show);

            if (!string.IsNullOrEmpty(item.Talent12)) parameters.AddWithValue("@Talent12", item.Talent12);
            else parameters.AddWithValue("@Talent12", DBNull.Value);
            parameters.AddWithValue("@Talent12Show", item.Talent12Show);

            if (!string.IsNullOrEmpty(item.Talent13)) parameters.AddWithValue("@Talent13", item.Talent13);
            else parameters.AddWithValue("@Talent13", DBNull.Value);
            parameters.AddWithValue("@Talent13Show", item.Talent13Show);

            if (!string.IsNullOrEmpty(item.Talent14)) parameters.AddWithValue("@Talent14", item.Talent14);
            else parameters.AddWithValue("@Talent14", DBNull.Value);
            parameters.AddWithValue("@Talent14Show", item.Talent14Show);

            if (!string.IsNullOrEmpty(item.Talent15)) parameters.AddWithValue("@Talent15", item.Talent15);
            else parameters.AddWithValue("@Talent15", DBNull.Value);
            parameters.AddWithValue("@Talent15Show", item.Talent15Show);

            if (!string.IsNullOrEmpty(item.Talent16)) parameters.AddWithValue("@Talent16", item.Talent16);
            else parameters.AddWithValue("@Talent16", DBNull.Value);
            parameters.AddWithValue("@Talent16Show", item.Talent16Show);

            if (!string.IsNullOrEmpty(item.Talent17)) parameters.AddWithValue("@Talent17", item.Talent17);
            else parameters.AddWithValue("@Talent17", DBNull.Value);
            parameters.AddWithValue("@Talent17Show", item.Talent17Show);

            if (!string.IsNullOrEmpty(item.Talent18)) parameters.AddWithValue("@Talent18", item.Talent18);
            else parameters.AddWithValue("@Talent18", DBNull.Value);
            parameters.AddWithValue("@Talent18Show", item.Talent18Show);

            if (!string.IsNullOrEmpty(item.Talent19)) parameters.AddWithValue("@Talent19", item.Talent19);
            else parameters.AddWithValue("@Talent19", DBNull.Value);
            parameters.AddWithValue("@Talent19Show", item.Talent19Show);

            if (!string.IsNullOrEmpty(item.Talent20)) parameters.AddWithValue("@Talent20", item.Talent20);
            else parameters.AddWithValue("@Talent20", DBNull.Value);
            parameters.AddWithValue("@Talent20Show", item.Talent20Show);

            if (!string.IsNullOrEmpty(item.Talent21)) parameters.AddWithValue("@Talent21", item.Talent21);
            else parameters.AddWithValue("@Talent21", DBNull.Value);
            parameters.AddWithValue("@Talent21Show", item.Talent21Show);

            if (!string.IsNullOrEmpty(item.Talent22)) parameters.AddWithValue("@Talent22", item.Talent22);
            else parameters.AddWithValue("@Talent22", DBNull.Value);
            parameters.AddWithValue("@Talent22Show", item.Talent22Show);

            if (!string.IsNullOrEmpty(item.Talent23)) parameters.AddWithValue("@Talent23", item.Talent23);
            else parameters.AddWithValue("@Talent23", DBNull.Value);
            parameters.AddWithValue("@Talent23Show", item.Talent23Show);

            if (!string.IsNullOrEmpty(item.Talent24)) parameters.AddWithValue("@Talent24", item.Talent24);
            else parameters.AddWithValue("@Talent24", DBNull.Value);
            parameters.AddWithValue("@Talent24Show", item.Talent24Show);

            if (!string.IsNullOrEmpty(item.Talent25)) parameters.AddWithValue("@Talent25", item.Talent25);
            else parameters.AddWithValue("@Talent25", DBNull.Value);
            parameters.AddWithValue("@Talent25Show", item.Talent25Show);

            if (!string.IsNullOrEmpty(item.InterestTopic1)) parameters.AddWithValue("@InterestTopic1", item.InterestTopic1);
            else parameters.AddWithValue("@InterestTopic1", DBNull.Value);
            parameters.AddWithValue("@InterestTopic1Show", item.InterestTopic1Show);

            if (!string.IsNullOrEmpty(item.InterestTopic2)) parameters.AddWithValue("@InterestTopic2", item.InterestTopic2);
            else parameters.AddWithValue("@InterestTopic2", DBNull.Value);
            parameters.AddWithValue("@InterestTopic2Show", item.InterestTopic2Show);

            if (!string.IsNullOrEmpty(item.InterestTopic3)) parameters.AddWithValue("@InterestTopic3", item.InterestTopic3);
            else parameters.AddWithValue("@InterestTopic3", DBNull.Value);
            parameters.AddWithValue("@InterestTopic3Show", item.InterestTopic3Show);

            if (!string.IsNullOrEmpty(item.InterestTopic4)) parameters.AddWithValue("@InterestTopic4", item.InterestTopic4);
            else parameters.AddWithValue("@InterestTopic4", DBNull.Value);
            parameters.AddWithValue("@InterestTopic4Show", item.InterestTopic4Show);

            if (!string.IsNullOrEmpty(item.InterestTopic5)) parameters.AddWithValue("@InterestTopic5", item.InterestTopic5);
            else parameters.AddWithValue("@InterestTopic5", DBNull.Value);
            parameters.AddWithValue("@InterestTopic5Show", item.InterestTopic5Show);

            if (!string.IsNullOrEmpty(item.InterestTopic6)) parameters.AddWithValue("@InterestTopic6", item.InterestTopic6);
            else parameters.AddWithValue("@InterestTopic6", DBNull.Value);
            parameters.AddWithValue("@InterestTopic6Show", item.InterestTopic6Show);

            if (!string.IsNullOrEmpty(item.InterestTopic7)) parameters.AddWithValue("@InterestTopic7", item.InterestTopic7);
            else parameters.AddWithValue("@InterestTopic7", DBNull.Value);
            parameters.AddWithValue("@InterestTopic7Show", item.InterestTopic7Show);

            if (!string.IsNullOrEmpty(item.InterestTopic8)) parameters.AddWithValue("@InterestTopic8", item.InterestTopic8);
            else parameters.AddWithValue("@InterestTopic8", DBNull.Value);
            parameters.AddWithValue("@InterestTopic8Show", item.InterestTopic8Show);

            if (!string.IsNullOrEmpty(item.InterestTopic9)) parameters.AddWithValue("@InterestTopic9", item.InterestTopic9);
            else parameters.AddWithValue("@InterestTopic9", DBNull.Value);
            parameters.AddWithValue("@InterestTopic9Show", item.InterestTopic9Show);

            if (!string.IsNullOrEmpty(item.InterestTopic10)) parameters.AddWithValue("@InterestTopic10", item.InterestTopic10);
            else parameters.AddWithValue("@InterestTopic10", DBNull.Value);
            parameters.AddWithValue("@InterestTopic10Show", item.InterestTopic10Show);

            if (!string.IsNullOrEmpty(item.Interesst1)) parameters.AddWithValue("@Interesst1", item.Interesst1);
            else parameters.AddWithValue("@Interesst1", DBNull.Value);
            parameters.AddWithValue("@Interesst1Show", item.Interesst1Show);

            if (!string.IsNullOrEmpty(item.Interesst2)) parameters.AddWithValue("@Interesst2", item.Interesst2);
            else parameters.AddWithValue("@Interesst2", DBNull.Value);
            parameters.AddWithValue("@Interesst2Show", item.Interesst2Show);

            if (!string.IsNullOrEmpty(item.Interesst3)) parameters.AddWithValue("@Interesst3", item.Interesst3);
            else parameters.AddWithValue("@Interesst3", DBNull.Value);
            parameters.AddWithValue("@Interesst3Show", item.Interesst3Show);

            if (!string.IsNullOrEmpty(item.Interesst4)) parameters.AddWithValue("@Interesst4", item.Interesst4);
            else parameters.AddWithValue("@Interesst4", DBNull.Value);
            parameters.AddWithValue("@Interesst4Show", item.Interesst4Show);

            if (!string.IsNullOrEmpty(item.Interesst5)) parameters.AddWithValue("@Interesst5", item.Interesst5);
            else parameters.AddWithValue("@Interesst5", DBNull.Value);
            parameters.AddWithValue("@Interesst5Show", item.Interesst5Show);

            if (!string.IsNullOrEmpty(item.Interesst6)) parameters.AddWithValue("@Interesst6", item.Interesst6);
            else parameters.AddWithValue("@Interesst6", DBNull.Value);
            parameters.AddWithValue("@Interesst6Show", item.Interesst6Show);

            if (!string.IsNullOrEmpty(item.Interesst7)) parameters.AddWithValue("@Interesst7", item.Interesst7);
            else parameters.AddWithValue("@Interesst7", DBNull.Value);
            parameters.AddWithValue("@Interesst7Show", item.Interesst7Show);

            if (!string.IsNullOrEmpty(item.Interesst8)) parameters.AddWithValue("@Interesst8", item.Interesst8);
            else parameters.AddWithValue("@Interesst8", DBNull.Value);
            parameters.AddWithValue("@Interesst8Show", item.Interesst8Show);

            if (!string.IsNullOrEmpty(item.Interesst9)) parameters.AddWithValue("@Interesst9", item.Interesst9);
            else parameters.AddWithValue("@Interesst9", DBNull.Value);
            parameters.AddWithValue("@Interesst9Show", item.Interesst9Show);

            if (!string.IsNullOrEmpty(item.Interesst10)) parameters.AddWithValue("@Interesst10", item.Interesst10);
            else parameters.AddWithValue("@Interesst10", DBNull.Value);
            parameters.AddWithValue("@Interesst10Show", item.Interesst10Show);

            if (!string.IsNullOrEmpty(item.MsgFrom)) parameters.AddWithValue("@MsgFrom", item.MsgFrom);
            else parameters.AddWithValue("@MsgFrom", DBNull.Value);
            parameters.AddWithValue("@MsgFromShow", item.MsgFromShow);

            if (!string.IsNullOrEmpty(item.GetMail)) parameters.AddWithValue("@GetMail", item.GetMail);
            else parameters.AddWithValue("@GetMail", DBNull.Value);
            parameters.AddWithValue("@GetMailShow", item.GetMailShow);

            parameters.AddWithValue("@DisplayAds", item.DisplayAds);

            parameters.AddWithValue("@DisplayAdsShow", item.DisplayAdsShow);
        }

        public static int IsOnlineTotal()
        {
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;
            int isOnline = 0;

            SqlConnection connection = new SqlConnection(strConn);
            SqlCommand command = new SqlCommand();
            try
            {
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "hisp_User_IsOnlineTotal";

                command.Parameters.Add(SqlHelper.AddParameter("@LastActivityDate", SqlDbType.DateTime, DateTime.Now.ToUniversalTime().Subtract(Business.DataAccessConfiguration.UserOnlineTimeGap())));
                connection.Open();
                isOnline = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
            catch (Exception e)
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                    connection.Close();
                throw e;
            }

            return isOnline;
        }

        public static bool IsUserLockedOut(Guid userId)
        {
            bool retValue = true;
            CSBooster_DataAccessDataContext csb = new CSBooster_DataAccessDataContext();
            var rets = csb.hisp_User_GetUserLockOut(userId);
            foreach (var ret in rets)
            {
                retValue = ret.IsLockedOut;
            }
            return retValue;
        }

        public static void LockOut(Guid userId)
        {
            CSBooster_DataAccessDataContext csb = new CSBooster_DataAccessDataContext();
            csb.hisp_User_LockUnlockUser(userId, true);
        }

        public static void Unlock(Guid userId)
        {
            CSBooster_DataAccessDataContext csb = new CSBooster_DataAccessDataContext();
            csb.hisp_User_LockUnlockUser(userId, false);
        }
    }
}
