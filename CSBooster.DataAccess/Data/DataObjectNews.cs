using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectNews
    {
        public static void FillObject(Business.DataObjectNews item, SqlDataReader sqlReader)
        {
            if (sqlReader["NewsText"] != DBNull.Value) item.NewsText = sqlReader["NewsText"].ToString();
            if (sqlReader["NewsTextLinked"] != DBNull.Value) item.NewsTextLinked = sqlReader["NewsTextLinked"].ToString();
            if (sqlReader["ReferenceURL"] != DBNull.Value) item.ReferenceURL = new Uri(sqlReader["ReferenceURL"].ToString());
            if (sqlReader["Links"] != DBNull.Value) item.newsLinksXml.LoadXml(sqlReader["Links"].ToString());
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return ", hiobj_News.*";
        }

        public static string GetInsertSQL(Business.DataObjectNews item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_News ([OBJ_ID],[NewsText],[NewsTextLinked],[ReferenceURL],[Links]) VALUES (@OBJ_ID,@NewsText,@NewsTextLinked,@ReferenceURL,@Links)";
        }

        public static string GetUpdateSQL(Business.DataObjectNews item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "UPDATE hiobj_News SET [NewsText] = @NewsText, [NewsTextLinked] = @NewsTextLinked, [ReferenceURL] = @ReferenceURL, [Links] =  @Links";
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return "INNER JOIN hiobj_News ON hiobj_News.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
        }

        public static string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        public static string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            if (!string.IsNullOrEmpty(qParas.GeneralSearch))
            {
                if (qParas.CatalogSearchType == DBCatalogSearchType.FreetextTable)
                    return string.Format(" OR FREETEXT(hiobj_News.*, '{0}', LANGUAGE 0x0)\r\n", qParas.GeneralSearch.Replace("'", "''"));
                else if (qParas.CatalogSearchType == DBCatalogSearchType.ContainsTable)
                    return " OR CONTAINS(hiobj_News.*, @ObjectGeneralSearch, LANGUAGE 0x0)\r\n";
                else
                    return string.Empty;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        private static void SetParameters(Business.DataObjectNews item, SqlParameterCollection parameters)
        {
            if (!string.IsNullOrEmpty(item.NewsText)) parameters.AddWithValue("@NewsText", item.NewsText);
            else parameters.AddWithValue("@NewsText", DBNull.Value);
            if (!string.IsNullOrEmpty(item.newsTextLinked)) parameters.AddWithValue("@NewsTextLinked", item.newsTextLinked);
            else parameters.AddWithValue("@NewsTextLinked", DBNull.Value);
            if (item.ReferenceURL != null) parameters.AddWithValue("@ReferenceURL", item.ReferenceURL.ToString());
            else parameters.AddWithValue("@ReferenceURL", DBNull.Value);
            parameters.AddWithValue("@Links", item.newsLinksXml.OuterXml);
        }
    }
}
