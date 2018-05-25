// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data.SqlClient;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectPicture
    {
        public static void FillObject(Business.DataObjectPicture item, SqlDataReader sqlReader)
        {
            item.Width = (int)sqlReader["Width"];
            item.Height = (int)sqlReader["Height"];
            item.AspectRatio = Convert.ToDecimal(sqlReader["AspectRatio"]);
            item.SizeByte = (long)sqlReader["SizeByte"];
            item.External = (bool)sqlReader["External"];
            if (sqlReader["URLReferenced"] != DBNull.Value) item.URLReferenced = sqlReader["URLReferenced"].ToString();
            if (sqlReader["TypeReferenced"] != DBNull.Value) item.TypeReferenced = sqlReader["TypeReferenced"].ToString();
            if (sqlReader["Site"] != DBNull.Value) item.Site = sqlReader["Site"].ToString();
            if (sqlReader["PhotoNotes"] != DBNull.Value) item.photoNotesXml.LoadXml(sqlReader["PhotoNotes"].ToString());
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return ", hiobj_Picture.*";
        }

        public static string GetInsertSQL(Business.DataObjectPicture item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_Picture ([OBJ_ID],[Width],[Height],[AspectRatio],[SizeByte],[External],[URLReferenced],[TypeReferenced],[Site],[PhotoNotes]) VALUES (@OBJ_ID,@Width,@Height,@AspectRatio,@SizeByte,@External,@URLReferenced,@TypeReferenced,@Site,@PhotoNotes)";
        }

        public static string GetUpdateSQL(Business.DataObjectPicture item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "UPDATE hiobj_Picture SET [Width] = @Width, [Height] = @Height, [AspectRatio] = @AspectRatio, [SizeByte] =  @SizeByte, [External] = @External, [URLReferenced] = @URLReferenced, [TypeReferenced] = @TypeReferenced, [Site] = @Site, [PhotoNotes] = @PhotoNotes";
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return "INNER JOIN hiobj_Picture ON hiobj_Picture.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
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

        private static void SetParameters(Business.DataObjectPicture item, SqlParameterCollection parameters)
        {
            parameters.AddWithValue("@Width", item.Width);
            parameters.AddWithValue("@Height", item.Height);
            parameters.AddWithValue("@AspectRatio", item.AspectRatio);
            parameters.AddWithValue("@SizeByte", item.SizeByte);
            parameters.AddWithValue("@External", item.External);
            if (!string.IsNullOrEmpty(item.URLReferenced)) parameters.AddWithValue("@URLReferenced", item.URLReferenced);
            else parameters.AddWithValue("@URLReferenced", DBNull.Value);
            if (!string.IsNullOrEmpty(item.TypeReferenced)) parameters.AddWithValue("@TypeReferenced", item.TypeReferenced);
            else parameters.AddWithValue("@TypeReferenced", DBNull.Value);
            if (!string.IsNullOrEmpty(item.Site)) parameters.AddWithValue("@Site", item.Site);
            else parameters.AddWithValue("@Site", DBNull.Value);
            parameters.AddWithValue("@PhotoNotes", item.photoNotesXml.OuterXml);
        }
    }
}
