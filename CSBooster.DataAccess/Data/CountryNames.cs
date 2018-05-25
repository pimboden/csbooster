//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		....
//
//  Created:	#1.0.0.0		27.08.2007 15:17:00 / pt
//  Updated:   
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Data
{
    internal static class CountryNames
    {
        public static List<Business.CountryName> Load(string langCode)
        {
            QuickCacheHandler cacheHandler = new QuickCacheHandler("CountryNames" + langCode);
            cacheHandler.AlternateCacheMinutes = 60;
            cacheHandler.ItemPriority = System.Web.Caching.CacheItemPriority.BelowNormal;
   
            List<Business.CountryName> list = cacheHandler.Get() as List<Business.CountryName>; 
            if (list != null)
                return list;

            list = new List<Business.CountryName>();
            SqlDataReader sqlReader = null;
            try
            {
                sqlReader = GetReader(langCode);
                if (sqlReader != null)
                {
                    while (sqlReader.Read())
                    {
                        Business.CountryName item = new Business.CountryName();
                        FillObject(item, sqlReader);
                        list.Add(item);
                    }
                }
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
                sqlReader = null;
            }

            cacheHandler.Insert(list);  
            return list;
        }

        private static SqlDataReader GetReader(string langCode)
        {
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;

            SqlConnection Conn = new SqlConnection(strConn);
            SqlDataReader sqlReader = null;
            SqlCommand GetData = new SqlCommand();
            try
            {
                GetData.Connection = Conn;
                GetData.CommandType = CommandType.Text;
                GetData.CommandText = "SELECT LangCode, CountryName, CountryCode FROM hitbl_CountryName_COU WHERE (LangCode = @LangCode) AND (Active = 1) ORDER BY CountryName"; 
                
                GetData.Parameters.AddWithValue("@LangCode", langCode); 
                Conn.Open();
                sqlReader = GetData.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }
            return sqlReader;
        }

        private static void FillObject(Business.CountryName item, SqlDataReader sqlReader)
        {
            item.Name = sqlReader["CountryName"].ToString();
            item.LangCode = sqlReader["LangCode"].ToString();
            item.CountryCode = sqlReader["CountryCode"].ToString();
        }
    }
}