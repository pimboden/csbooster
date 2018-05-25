// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data.SqlClient;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectArticle
    {
        public static void FillObject(Business.DataObjectArticle item, SqlDataReader sqlReader)
        {
            if (sqlReader["ArticleText"] != DBNull.Value) item.ArticleText = sqlReader["ArticleText"].ToString();
            if (sqlReader["ArticleTextLinked"] != DBNull.Value) item.ArticleTextLinked = sqlReader["ArticleTextLinked"].ToString();
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return ", hiobj_Article.*";
        }

        public static string GetInsertSQL(Business.DataObjectArticle item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_Article ([OBJ_ID],[ArticleText],[ArticleTextLinked]) VALUES (@OBJ_ID,@ArticleText,@ArticleTextLinked)";
        }

        public static string GetUpdateSQL(Business.DataObjectArticle item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "UPDATE hiobj_Article SET [ArticleText] = @ArticleText, [ArticleTextLinked] = @ArticleTextLinked";
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return "INNER JOIN hiobj_Article ON hiobj_Article.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
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
                    return string.Format(" OR FREETEXT(hiobj_Article.*, '{0}', LANGUAGE 0x0)\r\n", qParas.GeneralSearch.Replace("'", "''"));
                else if (qParas.CatalogSearchType == DBCatalogSearchType.ContainsTable)
                    return " OR CONTAINS(hiobj_Article.*, @ObjectGeneralSearch, LANGUAGE 0x0)\r\n";
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

        private static void SetParameters(Business.DataObjectArticle item, SqlParameterCollection parameters)
        {
            if (!string.IsNullOrEmpty(item.ArticleText)) parameters.AddWithValue("@ArticleText", item.ArticleText);
            else parameters.AddWithValue("@ArticleText", DBNull.Value);
            if (!string.IsNullOrEmpty(item.articleTextLinked)) parameters.AddWithValue("@ArticleTextLinked", item.articleTextLinked);
            else parameters.AddWithValue("@ArticleTextLinked", DBNull.Value);
        }
    }
}
