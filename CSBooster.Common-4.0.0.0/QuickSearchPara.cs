// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace _4screen.CSB.Common
{
    public static class QuickSearchCacheHelper
    {
        public static string CacheString(List<QuickSearchParameters> parameters)
        {
            StringBuilder sb = new StringBuilder(20*parameters.Count);
            foreach (QuickSearchParameters item in parameters)
                sb.Append(item.CacheString());

            return sb.ToString();
        }
    }

    public class QuickSearchParaOnline : QuickSearchPara
    {
        private string strSearchValue = string.Empty;

        public QuickSearchParaOnline()
        {
        }

        public QuickSearchParaOnline(string searchValue)
        {
            strSearchValue = searchValue;
        }

        public string SearchValue
        {
            get { return strSearchValue; }
            set { strSearchValue = value; }
        }

        public override string GetSql()
        {
            if (!string.IsNullOrEmpty(SearchValue))
            {
                DateTime dat = new DateTime(long.Parse(SearchValue));
                return string.Format("(hivw_Quick_User_Search_Base.LastActivityDate > CONVERT(DATETIME,'{0}-{1}-{2} {3}:{4}:{5}',120))", dat.Year, dat.Month, dat.Day, dat.Hour, dat.Minute, dat.Second);
            }
            else
            {
                return string.Empty;
            }
        }

        public override string GetChacheString()
        {
            if (!string.IsNullOrEmpty(SearchValue))
                return string.Concat("onl_", SearchValue);
            else
                return string.Empty;
        }
    }

    public class QuickSearchParameters : List<QuickSearchPara>
    {
        public enum ResultRelevance
        {
            Low = 1,
            Medium = 2,
            High = 3
        }

        private QuickSearchPara.SearchParaStrategy enuSearchStrategy = QuickSearchPara.SearchParaStrategy.Or;
        private ResultRelevance enuRelevance = ResultRelevance.Medium;

        public ResultRelevance Relevance
        {
            get { return enuRelevance; }
            set { enuRelevance = value; }
        }

        public QuickSearchPara.SearchParaStrategy SearchStrategy
        {
            get { return enuSearchStrategy; }
            set { enuSearchStrategy = value; }
        }

        public string CacheString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (QuickSearchPara item in this)
            {
                sb.Append(item.GetChacheString());
            }
            return sb.ToString();
        }

        public static List<QuickSearchParameters> GetExtendedSearchParameter(Hashtable hasSearchPara)
        {
            QuickSearchParameters itemParas = null;
            List<QuickSearchParameters> listParas = new List<QuickSearchParameters>();

            string strKm = "0";
            if (hasSearchPara.ContainsKey("km"))
                strKm = hasSearchPara["km"].ToString();
            else
                strKm = "0";

            foreach (string strName in hasSearchPara.Keys)
            {
                string strValue = hasSearchPara[strName].ToString();

                if (strName == "onl")
                {
                    if (DateTime.MinValue.ToString() != strValue)
                    {
                        itemParas = new QuickSearchParameters();
                        itemParas.Add(new QuickSearchParaOnline(strValue));
                        listParas.Add(itemParas);
                    }
                }
                else if (strName == "pic")
                {
                    if (strValue == "1")
                    {
                        itemParas = new QuickSearchParameters();
                        itemParas.Add(new QuickSearchParaPicture(QuickSearchPara.SearchParaValuePictrue.OnlyWith));
                        listParas.Add(itemParas);
                    }
                    else if (strValue == "0")
                    {
                        itemParas = new QuickSearchParameters();
                        itemParas.Add(new QuickSearchParaPicture(QuickSearchPara.SearchParaValuePictrue.Wathever));
                        listParas.Add(itemParas);
                    }
                }
                else if (strName == "sex")
                {
                    if (strValue == "1")
                    {
                        itemParas = new QuickSearchParameters();
                        itemParas.Add(new QuickSearchParaGender(QuickSearchPara.SearchParaValueGender.Frau));
                        listParas.Add(itemParas);
                    }
                    else if (strValue == "0")
                    {
                        itemParas = new QuickSearchParameters();
                        itemParas.Add(new QuickSearchParaGender(QuickSearchPara.SearchParaValueGender.Mann));
                        listParas.Add(itemParas);
                    }
                }
                else if (strName == "alt")
                {
                    string[] strAlter = strValue.Split('-');
                    if (strAlter.Length > 0 && strAlter[0] != "0")
                    {
                        int intVon = Convert.ToInt32(strAlter[0]);
                        int intBis = Convert.ToInt32(strAlter[1]);
                        itemParas = new QuickSearchParameters();
                        itemParas.Add(new QuickSearchParaAge(intVon, intBis));
                        listParas.Add(itemParas);
                    }
                }
                else if (strName == "sta")
                {
                    if (strValue.Length > 0)
                    {
                        itemParas = new QuickSearchParameters();
                        itemParas.Add(new QuickSearchParaCivilianStatus(strValue));
                        listParas.Add(itemParas);
                    }
                }
                else if (strName == "att")
                {
                    if (strValue.Length > 0)
                    {
                        itemParas = new QuickSearchParameters();
                        itemParas.Add(new QuickSearchParaAttractedTo(strValue));
                        listParas.Add(itemParas);
                    }
                }
                else if (strName == "eye")
                {
                    if (strValue.Length > 0)
                    {
                        itemParas = new QuickSearchParameters();
                        itemParas.Add(new QuickSearchParaEyeColor(strValue));
                        listParas.Add(itemParas);
                    }
                }
                else if (strName == "hai")
                {
                    if (strValue.Length > 0)
                    {
                        itemParas = new QuickSearchParameters();
                        itemParas.Add(new QuickSearchParaHairColor(strValue));
                        listParas.Add(itemParas);
                    }
                }
                else if (strName == "boh")
                {
                    string[] bodyHeight = strValue.Split('-');
                    if (bodyHeight.Length > 1 && bodyHeight[0] != "0")
                    {
                        int from = Convert.ToInt32(bodyHeight[0]);
                        int to = Convert.ToInt32(bodyHeight[1]);
                        itemParas = new QuickSearchParameters();
                        itemParas.Add(new QuickSearchParaBodyHeight(from, to));
                        listParas.Add(itemParas);
                    }
                }
                else if (strName == "bow")
                {
                    string[] bodyWeight = strValue.Split('-');
                    if (bodyWeight.Length > 1 && bodyWeight[0] != "0")
                    {
                        int from = Convert.ToInt32(bodyWeight[0]);
                        int to = Convert.ToInt32(bodyWeight[1]);
                        itemParas = new QuickSearchParameters();
                        itemParas.Add(new QuickSearchParaBodyWeight(from, to));
                        listParas.Add(itemParas);
                    }
                }
                else if (strName == "nik")
                {
                    if (strValue.Length > 0)
                    {
                        itemParas = new QuickSearchParameters();
                        itemParas.Add(new QuickSearchParaNickname(strValue, false));
                        listParas.Add(itemParas);
                    }
                }
                else if (strName == "plz")
                {
                    if (strValue.Length > 0)
                    {
                        itemParas = new QuickSearchParameters();
                        itemParas.Add(new QuickSearchParaZip(strValue, Convert.ToInt32(strKm)));
                        listParas.Add(itemParas);
                    }
                }
                else if (strName.IndexOf("InterestTopic") > -1)
                {
                    if (strValue.Length > 0)
                    {
                        itemParas = new QuickSearchParameters();
                        itemParas.Add(new QuickSearchParaInterestTopic(strName, strValue, false));
                        listParas.Add(itemParas);
                    }
                }
                else if (strName == "reg")
                {
                    if (strValue.Length > 0)
                    {
                        itemParas = new QuickSearchParameters();
                        itemParas.Add(new QuickSearchParaKanton(strValue));
                        listParas.Add(itemParas);
                    }
                }
            }
            return listParas;
        }
    }

    public class QuickSearchPara
    {
        public enum SearchParaStrategy
        {
            Or = 0,
            And = 1
        }

        public enum SearchParaValueGender
        {
            Mann = 0,
            Frau = 1,
            Wathever = 9
        }

        public enum SearchParaValuePictrue
        {
            Wathever = 0,
            OnlyWith = 1,
            OnlyWithout = 2
        }

        public virtual string GetSql()
        {
            return string.Empty;
        }

        public virtual string GetChacheString()
        {
            return string.Empty;
        }
    }

    public class QuickSearchParaCivilianStatus : QuickSearchPara
    {
        private string strSearchValue = string.Empty;

        public QuickSearchParaCivilianStatus()
        {
        }

        public QuickSearchParaCivilianStatus(string searchValue)
        {
            strSearchValue = searchValue;
        }

        public string SearchValue
        {
            get { return strSearchValue; }
            set { strSearchValue = value; }
        }

        public override string GetSql()
        {
            if (SearchValue.Length > 0)
            {
                return string.Format("(hivw_Quick_User_Search_Base.Status = N';{0};')", SearchValue.Replace("'", "''"));
            }
            else
            {
                return string.Empty;
            }
        }

        public override string GetChacheString()
        {
            if (SearchValue.Length > 0)
                return string.Concat("sta_", SearchValue);
            else
                return string.Empty;
        }
    }

    public class QuickSearchParaAttractedTo : QuickSearchPara
    {
        private string strSearchValue = string.Empty;

        public QuickSearchParaAttractedTo()
        {
        }

        public QuickSearchParaAttractedTo(string searchValue)
        {
            strSearchValue = searchValue;
        }

        public string SearchValue
        {
            get { return strSearchValue; }
            set { strSearchValue = value; }
        }

        public override string GetSql()
        {
            if (SearchValue.Length > 0)
            {
                return string.Format("(hivw_Quick_User_Search_Base.AttractedTo = N';{0};')", SearchValue.Replace("'", "''"));
            }
            else
            {
                return string.Empty;
            }
        }

        public override string GetChacheString()
        {
            if (SearchValue.Length > 0)
                return string.Concat("sta_", SearchValue);
            else
                return string.Empty;
        }
    }

    public class QuickSearchParaEyeColor : QuickSearchPara
    {
        private string strSearchValue = string.Empty;

        public QuickSearchParaEyeColor()
        {
        }

        public QuickSearchParaEyeColor(string searchValue)
        {
            strSearchValue = searchValue;
        }

        public string SearchValue
        {
            get { return strSearchValue; }
            set { strSearchValue = value; }
        }

        public override string GetSql()
        {
            if (SearchValue.Length > 0)
            {
                return string.Format("(hivw_Quick_User_Search_Base.EyeColor = N';{0};')", SearchValue.Replace("'", "''"));
            }
            else
            {
                return string.Empty;
            }
        }

        public override string GetChacheString()
        {
            if (SearchValue.Length > 0)
                return string.Concat("sta_", SearchValue);
            else
                return string.Empty;
        }
    }

    public class QuickSearchParaHairColor : QuickSearchPara
    {
        private string strSearchValue = string.Empty;

        public QuickSearchParaHairColor()
        {
        }

        public QuickSearchParaHairColor(string searchValue)
        {
            strSearchValue = searchValue;
        }

        public string SearchValue
        {
            get { return strSearchValue; }
            set { strSearchValue = value; }
        }

        public override string GetSql()
        {
            if (SearchValue.Length > 0)
            {
                return string.Format("(hivw_Quick_User_Search_Base.HairColor = N';{0};')", SearchValue.Replace("'", "''"));
            }
            else
            {
                return string.Empty;
            }
        }

        public override string GetChacheString()
        {
            if (SearchValue.Length > 0)
                return string.Concat("sta_", SearchValue);
            else
                return string.Empty;
        }
    }

    public class QuickSearchParaCommunityMember : QuickSearchPara
    {
        private string strSearchValue = string.Empty;

        public QuickSearchParaCommunityMember()
        {
        }

        public QuickSearchParaCommunityMember(string searchValue)
        {
            strSearchValue = searchValue;
        }

        public string SearchValue
        {
            get { return strSearchValue; }
            set { strSearchValue = value; }
        }

        public override string GetSql()
        {
            if (SearchValue.Length > 0)
            {
                return string.Format("((hirel_Community_User_CUR.CTY_ID = '{0}'))", SearchValue);
            }
            else
            {
                return string.Empty;
            }
        }

        public override string GetChacheString()
        {
            if (SearchValue.Length > 0)
                return string.Concat("com_", SearchValue);
            else
                return string.Empty;
        }
    }

    public class QuickSearchParaNickname : QuickSearchPara
    {
        private string strSearchValue = string.Empty;
        private bool blnSearchExactly = false;

        public QuickSearchParaNickname()
        {
        }

        public QuickSearchParaNickname(bool searchExactly)
        {
            blnSearchExactly = searchExactly;
        }

        public QuickSearchParaNickname(string searchValue, bool searchExactly)
        {
            strSearchValue = searchValue;
            blnSearchExactly = searchExactly;
        }

        public string SearchValue
        {
            get { return strSearchValue; }
            set { strSearchValue = value; }
        }

        public bool SearchExactly
        {
            get { return blnSearchExactly; }
            set { blnSearchExactly = value; }
        }

        public override string GetSql()
        {
            if (SearchValue.Length > 0)
            {
                if (SearchExactly)
                    return string.Format("(hivw_Quick_User_Search_Base.Nickname = N'{0}')", SearchValue.Replace("'", "''"));
                else
                    return string.Format("(hivw_Quick_User_Search_Base.Nickname LIKE N'%{0}%')", SearchValue.Replace("'", "''"));
            }
            else
            {
                return string.Empty;
            }
        }

        public override string GetChacheString()
        {
            if (SearchValue.Length > 0)
            {
                if (SearchExactly)
                    return string.Concat("nike_", SearchValue);
                else
                    return string.Concat("nik_", SearchValue);
            }
            else
                return string.Empty;
        }
    }

    public class QuickSearchParaVorname : QuickSearchPara
    {
        private string strSearchValue = string.Empty;
        private bool blnSearchExactly = false;

        public QuickSearchParaVorname()
        {
        }

        public QuickSearchParaVorname(bool searchExactly)
        {
            blnSearchExactly = searchExactly;
        }

        public QuickSearchParaVorname(string searchValue, bool searchExactly)
        {
            strSearchValue = searchValue;
            blnSearchExactly = searchExactly;
        }

        public string SearchValue
        {
            get { return strSearchValue; }
            set { strSearchValue = value; }
        }

        public bool SearchExactly
        {
            get { return blnSearchExactly; }
            set { blnSearchExactly = value; }
        }

        public override string GetSql()
        {
            if (SearchValue.Length > 0)
            {
                if (SearchExactly)
                    return string.Format("(hivw_Quick_User_Search_Base.Vorname = N'{0}')", SearchValue.Replace("'", "''"));
                else
                    return string.Format("(hivw_Quick_User_Search_Base.Vorname LIKE N'%{0}%')", SearchValue.Replace("'", "''"));
            }
            else
            {
                return string.Empty;
            }
        }

        public override string GetChacheString()
        {
            if (SearchValue.Length > 0)
            {
                if (SearchExactly)
                    return string.Concat("vone_", SearchValue);
                else
                    return string.Concat("von_", SearchValue);
            }
            else
                return string.Empty;
        }
    }

    public class QuickSearchParaName : QuickSearchPara
    {
        private string strSearchValue = string.Empty;
        private bool blnSearchExactly = false;

        public QuickSearchParaName()
        {
        }

        public QuickSearchParaName(bool searchExactly)
        {
            blnSearchExactly = searchExactly;
        }

        public QuickSearchParaName(string searchValue, bool searchExactly)
        {
            strSearchValue = searchValue;
            blnSearchExactly = searchExactly;
        }

        public string SearchValue
        {
            get { return strSearchValue; }
            set { strSearchValue = value; }
        }

        public bool SearchExactly
        {
            get { return blnSearchExactly; }
            set { blnSearchExactly = value; }
        }

        public override string GetSql()
        {
            if (SearchValue.Length > 0)
            {
                if (SearchExactly)
                    return string.Format("(hivw_Quick_User_Search_Base.Name = N'{0}')", SearchValue.Replace("'", "''"));
                else
                    return string.Format("(hivw_Quick_User_Search_Base.Name LIKE N'%{0}%')", SearchValue.Replace("'", "''"));
            }
            else
            {
                return string.Empty;
            }
        }

        public override string GetChacheString()
        {
            if (SearchValue.Length > 0)
            {
                if (SearchExactly)
                    return string.Concat("name_", SearchValue);
                else
                    return string.Concat("nam_", SearchValue);
            }
            else
                return string.Empty;
        }
    }

    public class QuickSearchParaCity : QuickSearchPara
    {
        private string strSearchValue = string.Empty;
        private bool blnSearchExactly = false;

        public QuickSearchParaCity()
        {
        }

        public QuickSearchParaCity(bool searchExactly)
        {
            blnSearchExactly = searchExactly;
        }

        public QuickSearchParaCity(string searchValue, bool searchExactly)
        {
            strSearchValue = searchValue;
            blnSearchExactly = searchExactly;
        }

        public string SearchValue
        {
            get { return strSearchValue; }
            set { strSearchValue = value; }
        }

        public bool SearchExactly
        {
            get { return blnSearchExactly; }
            set { blnSearchExactly = value; }
        }

        public override string GetSql()
        {
            if (SearchValue.Length > 0)
            {
                if (SearchExactly)
                    return string.Format("(hivw_Quick_User_Search_Base.City = N'{0}')", SearchValue.Replace("'", "''"));
                else
                    return string.Format("(hivw_Quick_User_Search_Base.City LIKE N'%{0}%')", SearchValue.Replace("'", "''"));
            }
            else
            {
                return string.Empty;
            }
        }

        public override string GetChacheString()
        {
            if (SearchValue.Length > 0)
            {
                if (SearchExactly)
                    return string.Concat("cite_", SearchValue);
                else
                    return string.Concat("cit_", SearchValue);
            }
            else
                return string.Empty;
        }
    }

    public class QuickSearchParaAge : QuickSearchPara
    {
        private int intSearchValueMin = 0;
        private int intSearchValueMax = 999;

        public QuickSearchParaAge()
        {
        }

        public QuickSearchParaAge(int searchValue)
        {
            intSearchValueMin = searchValue;
            intSearchValueMax = searchValue;
        }

        public QuickSearchParaAge(int searchValueMin, int searchValueMax)
        {
            intSearchValueMin = searchValueMin;
            intSearchValueMax = searchValueMax;
        }

        public int SearchValueMin
        {
            get { return intSearchValueMin; }
            set { intSearchValueMin = value; }
        }

        public int SearchValueMax
        {
            get { return intSearchValueMax; }
            set { intSearchValueMax = value; }
        }

        public override string GetSql()
        {
            if (SearchValueMin > 0 || SearchValueMax > 0)
            {
                return string.Format("(DATEPART(yy, GETDATE()) - DATEPART(yy, hivw_Quick_User_Search_Base.Birthday) >= {0} AND DATEPART(yy, GETDATE()) - DATEPART(yy, hivw_Quick_User_Search_Base.Birthday) <= {1})", SearchValueMin, SearchValueMax);
            }
            else
            {
                return string.Empty;
            }
        }

        public override string GetChacheString()
        {
            if (SearchValueMin > 0 || SearchValueMax > 0)
            {
                return string.Concat("age_", SearchValueMin, "-", SearchValueMax);
            }
            else
                return string.Empty;
        }
    }

    public class QuickSearchParaBodyHeight : QuickSearchPara
    {
        private int intSearchValueMin = 0;
        private int intSearchValueMax = 999;

        public QuickSearchParaBodyHeight()
        {
        }

        public QuickSearchParaBodyHeight(int searchValue)
        {
            intSearchValueMin = searchValue;
            intSearchValueMax = searchValue;
        }

        public QuickSearchParaBodyHeight(int searchValueMin, int searchValueMax)
        {
            intSearchValueMin = searchValueMin;
            intSearchValueMax = searchValueMax;
        }

        public int SearchValueMin
        {
            get { return intSearchValueMin; }
            set { intSearchValueMin = value; }
        }

        public int SearchValueMax
        {
            get { return intSearchValueMax; }
            set { intSearchValueMax = value; }
        }

        public override string GetSql()
        {
            if (SearchValueMin > 0 || SearchValueMax > 0)
            {
                return string.Format("hivw_Quick_User_Search_Base.BodyHeight >= {0} AND hivw_Quick_User_Search_Base.BodyHeight <= {1}", SearchValueMin, SearchValueMax);
            }
            else
            {
                return string.Empty;
            }
        }

        public override string GetChacheString()
        {
            if (SearchValueMin > 0 || SearchValueMax > 0)
            {
                return string.Concat("boh_", SearchValueMin, "-", SearchValueMax);
            }
            else
            {
                return string.Empty;
            }
        }
    }

    public class QuickSearchParaBodyWeight : QuickSearchPara
    {
        private int intSearchValueMin = 0;
        private int intSearchValueMax = 999;

        public QuickSearchParaBodyWeight()
        {
        }

        public QuickSearchParaBodyWeight(int searchValue)
        {
            intSearchValueMin = searchValue;
            intSearchValueMax = searchValue;
        }

        public QuickSearchParaBodyWeight(int searchValueMin, int searchValueMax)
        {
            intSearchValueMin = searchValueMin;
            intSearchValueMax = searchValueMax;
        }

        public int SearchValueMin
        {
            get { return intSearchValueMin; }
            set { intSearchValueMin = value; }
        }

        public int SearchValueMax
        {
            get { return intSearchValueMax; }
            set { intSearchValueMax = value; }
        }

        public override string GetSql()
        {
            if (SearchValueMin > 0 || SearchValueMax > 0)
            {
                return string.Format("hivw_Quick_User_Search_Base.BodyWeight >= {0} AND hivw_Quick_User_Search_Base.BodyWeight <= {1}", SearchValueMin, SearchValueMax);
            }
            else
            {
                return string.Empty;
            }
        }

        public override string GetChacheString()
        {
            if (SearchValueMin > 0 || SearchValueMax > 0)
            {
                return string.Concat("bow_", SearchValueMin, "-", SearchValueMax);
            }
            else
            {
                return string.Empty;
            }
        }
    }

    public class QuickSearchParaInterestTopic : QuickSearchPara
    {
        private string strSearchValue = string.Empty;
        private bool blnSearchExactly = false;
        private string strDBFieldName = string.Empty;

        public QuickSearchParaInterestTopic(string dBFieldName)
        {
            strDBFieldName = dBFieldName;
        }

        public QuickSearchParaInterestTopic(string dBFieldName, bool searchExactly)
        {
            strDBFieldName = dBFieldName;
            blnSearchExactly = searchExactly;
        }

        public QuickSearchParaInterestTopic(string dBFieldName, string searchValue, bool searchExactly)
        {
            strDBFieldName = dBFieldName;
            strSearchValue = searchValue;
            blnSearchExactly = searchExactly;
        }

        public string SearchValue
        {
            get { return strSearchValue; }
            set { strSearchValue = value; }
        }

        public bool SearchExactly
        {
            get { return blnSearchExactly; }
            set { blnSearchExactly = value; }
        }

        public override string GetSql()
        {
            if (SearchValue.Length > 0)
            {
                if (SearchExactly)
                    return string.Format("(hivw_Quick_User_Search_Base.{0} = N';{1};')", strDBFieldName, SearchValue.Replace("'", "''"));
                else
                    return string.Format("(hivw_Quick_User_Search_Base.{0} LIKE N'%;{1};%')", strDBFieldName, SearchValue.Replace("'", "''"));
            }
            else
            {
                return string.Empty;
            }
        }

        public override string GetChacheString()
        {
            if (SearchValue.Length > 0)
            {
                if (SearchExactly)
                    return string.Concat(strDBFieldName, "e_", SearchValue);
                else
                    return string.Concat(strDBFieldName, "_", SearchValue);
            }
            else
                return string.Empty;
        }
    }

    public class QuickSearchParaTalent : QuickSearchPara
    {
        private string strSearchValue = string.Empty;

        public QuickSearchParaTalent()
        {
        }

        public QuickSearchParaTalent(string searchValue)
        {
            strSearchValue = searchValue;
        }

        public string SearchValue
        {
            get { return strSearchValue; }
            set { strSearchValue = value; }
        }

        public override string GetSql()
        {
            if (SearchValue.Length > 0)
            {
                return string.Format("(hivw_Quick_User_Search_Base.Talent LIKE N'%{0}%')", SearchValue.Replace("'", "''"));
            }
            else
            {
                return string.Empty;
            }
        }

        public override string GetChacheString()
        {
            if (SearchValue.Length > 0)
                return string.Concat("tal_", SearchValue);
            else
                return string.Empty;
        }
    }

    public class QuickSearchParaIntressen : QuickSearchPara
    {
        private string strSearchValue = string.Empty;

        public QuickSearchParaIntressen()
        {
        }

        public QuickSearchParaIntressen(string searchValue)
        {
            strSearchValue = searchValue;
        }

        public string SearchValue
        {
            get { return strSearchValue; }
            set { strSearchValue = value; }
        }

        public override string GetSql()
        {
            if (SearchValue.Length > 0)
            {
                return string.Format("(hivw_Quick_User_Search_Base.Interesst LIKE N'%{0}%')", SearchValue.Replace("'", "''"));
            }
            else
            {
                return string.Empty;
            }
        }

        public override string GetChacheString()
        {
            if (SearchValue.Length > 0)
                return string.Concat("int_", SearchValue);
            else
                return string.Empty;
        }
    }

    public class QuickSearchParaZip : QuickSearchPara
    {
        private string strSearchValue = string.Empty;
        private int intSearchRangeKm = 0;

        public QuickSearchParaZip()
        {
        }

        public QuickSearchParaZip(string searchValue)
        {
            strSearchValue = searchValue;
        }

        public QuickSearchParaZip(string searchValue, int searchRangeKm)
        {
            strSearchValue = searchValue;
            intSearchRangeKm = searchRangeKm;
        }

        public string SearchValue
        {
            get { return strSearchValue; }
            set { strSearchValue = value; }
        }

        public int SearchRangeKm
        {
            get { return intSearchRangeKm; }
            set { intSearchRangeKm = value; }
        }

        public override string GetSql()
        {
            if (SearchValue.Length > 0)
            {
                if (SearchRangeKm == 0)
                    return string.Format("NOT (hivw_Quick_User_Search_Base.Zip = '') AND (ISNULL(dbo.hifu_PLZ_Distance(hivw_Quick_User_Search_Base.Zip, 'ch', '{0}', 'ch'), 999999) <= {1})", SearchValue.Replace("'", "''"), 1);
                else
                    return string.Format("NOT (hivw_Quick_User_Search_Base.Zip = '') AND (ISNULL(dbo.hifu_PLZ_Distance(hivw_Quick_User_Search_Base.Zip, 'ch', '{0}', 'ch'), 999999) <= {1})", SearchValue.Replace("'", "''"), SearchRangeKm);
            }
            else
            {
                return string.Empty;
            }
        }

        public override string GetChacheString()
        {
            if (SearchValue.Length > 0)
                return string.Concat("zip_", SearchValue, SearchRangeKm);
            else
                return string.Empty;
        }
    }

    public class QuickSearchParaKanton : QuickSearchPara
    {
        private string strSearchValue = string.Empty;

        public QuickSearchParaKanton()
        {
        }

        public QuickSearchParaKanton(string searchValue)
        {
            strSearchValue = searchValue;
        }

        public string SearchValue
        {
            get { return strSearchValue; }
            set { strSearchValue = value; }
        }

        public override string GetSql()
        {
            if (SearchValue.Length > 0)
            {
                return string.Format("(hivw_Quick_User_Search_Base.Kanton = N'{0}')", SearchValue.Replace("'", "''"));
            }
            else
            {
                return string.Empty;
            }
        }

        public override string GetChacheString()
        {
            if (SearchValue.Length > 0)
                return string.Concat("kan_", SearchValue);
            else
                return string.Empty;
        }
    }

    public class QuickSearchParaLand : QuickSearchPara
    {
        private string strSearchValue = string.Empty;

        public QuickSearchParaLand()
        {
        }

        public QuickSearchParaLand(string searchValue)
        {
            strSearchValue = searchValue;
        }

        public string SearchValue
        {
            get { return strSearchValue; }
            set { strSearchValue = value; }
        }

        public override string GetSql()
        {
            if (SearchValue.Length > 0)
            {
                return string.Format(" (hivw_Quick_User_Search_Base.Land = N';{0};')", SearchValue.Replace("'", "''"));
            }
            else
            {
                return string.Empty;
            }
        }

        public override string GetChacheString()
        {
            if (SearchValue.Length > 0)
                return string.Concat("lan_", SearchValue);
            else
                return string.Empty;
        }
    }

    public class QuickSearchParaPicture : QuickSearchPara
    {
        private SearchParaValuePictrue enuSearchValue = SearchParaValuePictrue.OnlyWith;

        public QuickSearchParaPicture()
        {
        }

        public QuickSearchParaPicture(SearchParaValuePictrue searchValue)
        {
            enuSearchValue = searchValue;
        }

        public SearchParaValuePictrue SearchValue
        {
            get { return enuSearchValue; }
            set { enuSearchValue = value; }
        }

        public override string GetSql()
        {
            if (enuSearchValue == SearchParaValuePictrue.OnlyWith)
            {
                return "(NOT (hivw_Quick_User_Search_Base.Picture IS NULL))";
            }
            else if (enuSearchValue == SearchParaValuePictrue.OnlyWithout)
            {
                return "(hivw_Quick_User_Search_Base.Picture IS NULL)";
            }
                //else if (enuSearchValue == SearchParaValuePictrue.Wathever)
                //{
                //   return "(hitbl_DataObject_OBJ.OBJ_URLImageSmall IS NULL)";
                //}
            else
            {
                return string.Empty;
            }
        }

        public override string GetChacheString()
        {
            if (enuSearchValue == SearchParaValuePictrue.OnlyWith)
                return "pic_t";
            else if (enuSearchValue == SearchParaValuePictrue.OnlyWithout)
                return "pic_f";
            else
                return string.Empty;
        }
    }

    public class QuickSearchParaGender : QuickSearchPara
    {
        private SearchParaValueGender enuSearchValue = SearchParaValueGender.Frau;

        public QuickSearchParaGender()
        {
        }

        public QuickSearchParaGender(SearchParaValueGender searchValue)
        {
            enuSearchValue = searchValue;
        }

        public SearchParaValueGender SearchValue
        {
            get { return enuSearchValue; }
            set { enuSearchValue = value; }
        }

        public override string GetSql()
        {
            if (enuSearchValue == SearchParaValueGender.Frau)
            {
                return "(hivw_Quick_User_Search_Base.Gender = N';1;')";
            }
            else if (enuSearchValue == SearchParaValueGender.Mann)
            {
                return "(hivw_Quick_User_Search_Base.Gender = N';0;')";
            }
                //else if (enuSearchValue == SearchParaValueGender.Wathever)
                //{
                //   return "((UAN_ViewType = 1 AND UAN_Question_Friendlyname = N'rblGender') AND (UAN_AnswerString <> N''))";
                //}
            else
            {
                return string.Empty;
            }
        }

        public override string GetChacheString()
        {
            if (enuSearchValue == SearchParaValueGender.Frau)
                return "sex_f";
            else if (enuSearchValue == SearchParaValueGender.Mann)
                return "sex_m";
            else
                return string.Empty;
        }
    }

    public class QuickSearchParaFeatrued : QuickSearchPara
    {
        private bool? blnSearchValue;

        public QuickSearchParaFeatrued()
        {
        }

        public QuickSearchParaFeatrued(bool? searchValue)
        {
            blnSearchValue = searchValue;
        }

        public bool? SearchValue
        {
            get { return blnSearchValue; }
            set { blnSearchValue = value; }
        }

        public override string GetSql()
        {
            if (blnSearchValue.HasValue && blnSearchValue.Value)
            {
                return " (hivw_Quick_User_Search_Base.OBJ_Featured = 1)";
            }
            else if (blnSearchValue.HasValue && !blnSearchValue.Value)
            {
                return " (hivw_Quick_User_Search_Base.OBJ_Featured = 0)";
            }
            else
            {
                return string.Empty;
            }
        }

        public override string GetChacheString()
        {
            if (blnSearchValue.HasValue && blnSearchValue.Value)
            {
                return "feat_t";
            }
            else if (blnSearchValue.HasValue && !blnSearchValue.Value)
            {
                return "feat_f";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}