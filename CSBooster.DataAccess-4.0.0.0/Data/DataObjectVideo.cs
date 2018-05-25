// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data.SqlClient;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectVideo
    {
        public static void FillObject(Business.DataObjectVideo item, SqlDataReader sqlReader)
        {
            item.OriginalFormat = (_4screen.CSB.Common.VideoFormat)Enum.Parse(typeof(_4screen.CSB.Common.VideoFormat), sqlReader["OriginalFormat"].ToString());
            item.Width = (int)sqlReader["Width"];
            item.Height = (int)sqlReader["Height"];
            item.SizeByte = (int)sqlReader["SizeByte"];
            item.DurationSecond = (int)sqlReader["DurationSecond"];
            if (sqlReader["OriginalLocation"] != DBNull.Value) item.OriginalLocation = sqlReader["OriginalLocation"].ToString();
            if (sqlReader["ConvertMessage"] != DBNull.Value) item.ConvertMessage = sqlReader["ConvertMessage"].ToString();
            item.External = (bool)sqlReader["External"];
            if (sqlReader["URLReferenced"] != DBNull.Value) item.URLReferenced = sqlReader["URLReferenced"].ToString();
            item.AspectRatio = decimal.Parse(sqlReader["AspectRatio"].ToString());
            if (sqlReader["TypeReferenced"] != DBNull.Value) item.TypeReferenced = sqlReader["TypeReferenced"].ToString();
            if (sqlReader["Site"] != DBNull.Value) item.Site = sqlReader["Site"].ToString();
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return ", hiobj_Video.*";
        }

        public static string GetInsertSQL(Business.DataObjectVideo item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_Video ([OBJ_ID],[Width],[Height],[AspectRatio],[SizeByte],[External],[URLReferenced],[TypeReferenced],[Site],[OriginalLocation],[ConvertMessage]) VALUES (@OBJ_ID,@Width,@Height,@AspectRatio,@SizeByte,@External,@URLReferenced,@TypeReferenced,@Site,@OriginalLocation,@ConvertMessage)";
        }

        public static string GetUpdateSQL(Business.DataObjectVideo item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "UPDATE hiobj_Video SET [Width] = @Width, [Height] = @Height, [AspectRatio] = @AspectRatio, [SizeByte] =  @SizeByte, [External] = @External, [URLReferenced] = @URLReferenced, [TypeReferenced] = @TypeReferenced, [Site] = @Site, [OriginalLocation] = @OriginalLocation, [ConvertMessage] = @ConvertMessage";
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return "INNER JOIN hiobj_Video ON hiobj_Video.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
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

        public static string GetOrderBySQL(Business.QuickParameters paras, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        private static void SetParameters(Business.DataObjectVideo item, SqlParameterCollection parameters)
        {
            parameters.AddWithValue("@OriginalFormat", item.OriginalFormat);
            parameters.AddWithValue("@Width", item.Width);
            parameters.AddWithValue("@Height", item.Height);
            parameters.AddWithValue("@SizeByte", item.SizeByte);
            parameters.AddWithValue("@DurationSecond", item.DurationSecond);
            if (!string.IsNullOrEmpty(item.OriginalLocation)) parameters.AddWithValue("@OriginalLocation", item.OriginalLocation);
            else parameters.AddWithValue("@OriginalLocation", DBNull.Value);
            if (!string.IsNullOrEmpty(item.ConvertMessage)) parameters.AddWithValue("@ConvertMessage", item.ConvertMessage);
            else parameters.AddWithValue("@ConvertMessage", DBNull.Value);
            parameters.AddWithValue("@External", item.External);
            if (!string.IsNullOrEmpty(item.URLReferenced)) parameters.AddWithValue("@URLReferenced", item.URLReferenced);
            else parameters.AddWithValue("@URLReferenced", DBNull.Value);
            parameters.AddWithValue("@AspectRatio", item.AspectRatio);
            if (!string.IsNullOrEmpty(item.TypeReferenced)) parameters.AddWithValue("@TypeReferenced", item.TypeReferenced);
            else parameters.AddWithValue("@TypeReferenced", DBNull.Value);
            if (!string.IsNullOrEmpty(item.Site)) parameters.AddWithValue("@Site", item.Site);
            else parameters.AddWithValue("@Site", DBNull.Value);
        }

 

    }
}
