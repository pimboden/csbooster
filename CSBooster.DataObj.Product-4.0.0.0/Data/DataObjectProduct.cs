// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data.SqlClient;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataObj.Data
{
    internal class DataObjectProduct
    {
        public static void FillObject(Business.DataObjectProduct item, SqlDataReader sqlReader)
        {
            if (sqlReader["ProductRef"] != DBNull.Value) item.ProductRef = sqlReader["ProductRef"].ToString();
            if (sqlReader["ProductText"] != DBNull.Value) item.ProductText = sqlReader["ProductText"].ToString();
            if (sqlReader["ProductTextLinked"] != DBNull.Value) item.ProductTextLinked = sqlReader["ProductTextLinked"].ToString();
            if (sqlReader["Price1"] != DBNull.Value) item.Price1 = Convert.ToDouble( sqlReader["Price1"]);
            if (sqlReader["Price2"] != DBNull.Value) item.Price2 = Convert.ToDouble(sqlReader["Price2"]);
            if (sqlReader["Price3"] != DBNull.Value) item.Price3 = Convert.ToDouble(sqlReader["Price3"]);
            if (sqlReader["Porto"] != DBNull.Value) item.Porto = Convert.ToDouble(sqlReader["Porto"]);
        }

        public static string GetSelectSQL(DataAccess.Business.QuickParameters qParas)
        {
            return ", hiobj_Product.*";
        }

        public static string GetInsertSQL(Business.DataObjectProduct item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_Product ([OBJ_ID],[ProductRef],[ProductText],[ProductTextLinked],[Price1],[Price2],[Price3],[Porto]) VALUES (@OBJ_ID,@ProductRef,@ProductText,@ProductTextLinked,@Price1,@Price2,@Price3,@Porto)";
        }

        public static string GetUpdateSQL(Business.DataObjectProduct item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "UPDATE hiobj_Product SET [ProductRef] = @ProductRef, [ProductText] = @ProductText, [ProductTextLinked] = @ProductTextLinked, [Price1] = @Price1, [Price2] = @Price2, [Price3] = @Price3, [Porto] = @Porto";
        }

        public static string GetJoinSQL(DataAccess.Business.QuickParameters qParas)
        {
            return "INNER JOIN hiobj_Product ON hiobj_Product.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
        }

        public static string GetWhereSQL(DataAccess.Business.QuickParameters qParas)
        {
            return string.Empty;
        }

        public static string GetFullTextWhereSQL(DataAccess.Business.QuickParameters qParas)
        {
            if (!string.IsNullOrEmpty(qParas.GeneralSearch))
            {
                if (qParas.CatalogSearchType == DBCatalogSearchType.FreetextTable)
                    return string.Format(" OR FREETEXT(hiobj_Product.*, '{0}', LANGUAGE 0x0)\r\n", qParas.GeneralSearch.Replace("'", "''"));
                else if (qParas.CatalogSearchType == DBCatalogSearchType.ContainsTable)
                    return " OR CONTAINS(hiobj_Product.*, @ObjectGeneralSearch, LANGUAGE 0x0)\r\n";
                else
                    return string.Empty;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetOrderBySQL()
        {
            return string.Empty;
        }

        private static void SetParameters(Business.DataObjectProduct item, SqlParameterCollection parameters)
        {
            if (!string.IsNullOrEmpty(item.ProductRef)) parameters.AddWithValue("@ProductRef", item.ProductRef);
            else parameters.AddWithValue("@ProductRef", DBNull.Value);

            if (!string.IsNullOrEmpty(item.ProductText)) parameters.AddWithValue("@ProductText", item.ProductText);
            else parameters.AddWithValue("@ProductText", DBNull.Value);
            
            if (!string.IsNullOrEmpty(item.productTextLinked)) parameters.AddWithValue("@ProductTextLinked", item.productTextLinked);
            else parameters.AddWithValue("@ProductTextLinked", DBNull.Value);
            
            if (item.Price1.HasValue) parameters.AddWithValue("@Price1", item.Price1.Value);
            else parameters.AddWithValue("@Price1", DBNull.Value);
            
            if (item.Price2.HasValue) parameters.AddWithValue("@Price2", item.Price2.Value);
            else parameters.AddWithValue("@Price2", DBNull.Value);
            
            if (item.Price3.HasValue) parameters.AddWithValue("@Price3", item.Price3.Value);
            else parameters.AddWithValue("@Price3", DBNull.Value);

            if (item.Porto.HasValue) parameters.AddWithValue("@Porto", item.Porto.Value);
            else parameters.AddWithValue("@Porto", DBNull.Value);
        }
    }
}
