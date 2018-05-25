// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data.SqlClient;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectAudio
    {
        public static void FillObject(Business.DataObjectAudio item, SqlDataReader sqlReader)
        {
            item.OriginalFormat = (_4screen.CSB.Common.AudioFormat)Enum.Parse(typeof(_4screen.CSB.Common.AudioFormat), sqlReader["OriginalFormat"].ToString());
            item.SizeByte = (int)sqlReader["SizeByte"];
            if (sqlReader["Location"] != DBNull.Value) item.Location = sqlReader["Location"].ToString();
            if (sqlReader["Interpreter"] != DBNull.Value) item.Interpreter = sqlReader["Interpreter"].ToString();
            if (sqlReader["Producer"] != DBNull.Value) item.Producer = sqlReader["Producer"].ToString();
            if (sqlReader["Album"] != DBNull.Value) item.Album = sqlReader["Album"].ToString();
            if (sqlReader["Genere"] != DBNull.Value) item.Genere = sqlReader["Genere"].ToString();
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return ", hiobj_Audio.*";
        }

        public static string GetInsertSQL(Business.DataObjectAudio item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_Audio ([OBJ_ID],[OriginalFormat],[SizeByte],[Location],[Interpreter],[Producer],[Album],[Genere]) VALUES (@OBJ_ID,@OriginalFormat,@SizeByte,@Location,@Interpreter,@Producer,@Album,@Genere)";
        }

        public static string GetUpdateSQL(Business.DataObjectAudio item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "UPDATE hiobj_Audio SET [OriginalFormat] = @OriginalFormat, [SizeByte] = @SizeByte, [Location] = @Location, [Interpreter] = @Interpreter, [Producer] =  @Producer, [Album] = @Album, [Genere] = @Genere";
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return "INNER JOIN hiobj_Audio ON hiobj_Audio.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
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
                    return string.Format(" OR FREETEXT(hiobj_Audio.*, '{0}', LANGUAGE 0x0)\r\n", qParas.GeneralSearch.Replace("'", "''"));
                else if (qParas.CatalogSearchType == DBCatalogSearchType.ContainsTable)
                    return " OR CONTAINS(hiobj_Audio.*, @ObjectGeneralSearch, LANGUAGE 0x0)\r\n";
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

        private static void SetParameters(Business.DataObjectAudio item, SqlParameterCollection parameters)
        {
            parameters.AddWithValue("@OriginalFormat", item.OriginalFormat);
            parameters.AddWithValue("@SizeByte", item.SizeByte);
            if (!string.IsNullOrEmpty(item.Location)) parameters.AddWithValue("@Location", item.Location);
            else parameters.AddWithValue("@Location", DBNull.Value);
            if (!string.IsNullOrEmpty(item.Interpreter)) parameters.AddWithValue("@Interpreter", item.Interpreter);
            else parameters.AddWithValue("@Interpreter", DBNull.Value);
            if (!string.IsNullOrEmpty(item.Producer)) parameters.AddWithValue("@Producer", item.Producer);
            else parameters.AddWithValue("@Producer", DBNull.Value);
            if (!string.IsNullOrEmpty(item.Album)) parameters.AddWithValue("@Album", item.Album);
            else parameters.AddWithValue("@Album", DBNull.Value);
            if (!string.IsNullOrEmpty(item.Genere)) parameters.AddWithValue("@Genere", item.Genere);
            else parameters.AddWithValue("@Genere", DBNull.Value);
        }
    }
}
