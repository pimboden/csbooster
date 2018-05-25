using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectFolder
    {
        public static void FillObject(Business.DataObjectFolder item, SqlDataReader sqlReader)
        {
            item.AllowMemberEdit = (bool)sqlReader["AllowMemberEdit"];
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return ", hiobj_Folder.*";
        }

        public static string GetInsertSQL(Business.DataObjectFolder item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_Folder ([OBJ_ID],[AllowMemberEdit]) VALUES (@OBJ_ID,@AllowMemberEdit)";
        }

        public static string GetUpdateSQL(Business.DataObjectFolder item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "UPDATE hiobj_Folder SET [AllowMemberEdit] = @AllowMemberEdit";
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return "INNER JOIN hiobj_Folder ON hiobj_Folder.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
        }

        public static string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        public static string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            string retString = string.Empty;
            return retString;
        }

        public static string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        private static void SetParameters(Business.DataObjectFolder item, SqlParameterCollection parameters)
        {
            parameters.AddWithValue("@AllowMemberEdit", item.AllowMemberEdit);
        }
    }
}
