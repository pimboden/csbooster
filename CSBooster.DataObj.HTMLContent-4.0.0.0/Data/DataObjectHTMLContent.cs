// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Data.SqlClient;

namespace _4screen.CSB.DataObj.Data
{
    internal class DataObjectHTMLContent
    {
        public static void FillObject(Business.DataObjectHTMLContent item, SqlDataReader sqlReader)
        {

        }

        public static string GetSelectSQL(DataAccess.Business.QuickParameters paras)
        {
            return ", hiobj_HTMLContent.*";
        }

        public static string GetInsertSQL(Business.DataObjectHTMLContent item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return @"INSERT INTO hiobj_HTMLContent 
                    (OBJ_ID) 
            VALUES (@OBJ_ID)";
        }

        public static string GetUpdateSQL(Business.DataObjectHTMLContent item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return string.Empty;
        }

        public static string GetJoinSQL(DataAccess.Business.QuickParameters paras)
        {
            return "INNER JOIN hiobj_HTMLContent ON hiobj_HTMLContent.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
        }

        public static string GetWhereSQL(DataAccess.Business.QuickParameters paras)
        {
            return string.Empty;
        }

        public static string GetFullTextWhereSQL(DataAccess.Business.QuickParameters paras)
        {
            string retString = string.Empty;
            return retString;
        }


        public static string GetOrderBySQL()
        {
            return string.Empty;
        }

        private static void SetParameters(Business.DataObjectHTMLContent item, SqlParameterCollection parameters)
        {
            
        }

    }
}
