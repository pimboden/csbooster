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
using System.Text; 
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Data
{
    internal static class MainTags
    {
        public static List<Business.MainTag> Load(int? parentId, int? level)
        {
            string key = "MainTags";
            if (parentId.HasValue)
                key += "p" + parentId.Value.ToString();
            if (level.HasValue)
                key += "l" + level.Value.ToString();

            QuickCacheHandler cacheHandler = new QuickCacheHandler(key);
            cacheHandler.AlternateCacheMinutes = 60;
            cacheHandler.ItemPriority = System.Web.Caching.CacheItemPriority.BelowNormal;

            List<Business.MainTag> list = cacheHandler.Get() as List<Business.MainTag>; 
            if (list != null)
                return list;

            list = new List<Business.MainTag>();
            SqlDataReader sqlReader = null;
            try
            {
                sqlReader = GetReader(parentId, level);
                if (sqlReader != null)
                {
                    while (sqlReader.Read())
                    {
                        Business.MainTag item = new Business.MainTag();
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

        private static SqlDataReader GetReader(int? parentId, int? level)
        {
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;

            SqlConnection Conn = new SqlConnection(strConn);
            SqlDataReader sqlReader = null;
            SqlCommand GetData = new SqlCommand();
            try
            {
                GetData.Connection = Conn;
                GetData.CommandType = CommandType.Text;

                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT MAN_ID, MAN_Level, MAN_MAN_ID, MAN_Title, MAN_Status, MAN_Order, TGW_ID FROM hitbl_Main_MAN WHERE MAN_Status = 1 ");
                if (parentId.HasValue)
                {
                    sql.Append("AND MAN_MAN_ID = @MAN_MAN_ID ");
                    GetData.Parameters.AddWithValue("@MAN_MAN_ID", parentId); 
                }
                if (level.HasValue)
                {
                    sql.Append("AND MAN_Level = @MAN_Level ");
                    GetData.Parameters.AddWithValue("@MAN_Level", level);
                }
                sql.Append("ORDER BY MAN_Level, MAN_Order, MAN_Title");

                GetData.CommandText = sql.ToString();
                
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

        private static void FillObject(Business.MainTag item, SqlDataReader sqlReader)
        {
            item.Id = Convert.ToInt32(sqlReader["MAN_ID"]);
            if (sqlReader["MAN_MAN_ID"] != DBNull.Value)  
                item.ParentId = Convert.ToInt32(sqlReader["MAN_MAN_ID"]);
            item.Level = Convert.ToInt32(sqlReader["MAN_Level"]);
            item.Order = Convert.ToInt32(sqlReader["MAN_Order"]);
            item.Title = sqlReader["MAN_Title"].ToString();
            item.TagWordId = sqlReader["TGW_ID"].ToString().ToGuid();
        }
    }
}