// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectTag
    {
        public static void FillObject(Business.DataObjectTag item, SqlDataReader sqlReader)
        {
            item.Relevance = sqlReader.GetDecimal(sqlReader.GetOrdinal("TGW_Relevance"));
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            if (qParas is Business.QuickParametersTag)
                return string.Empty;
            else
                return ", 0.0 AS TGW_Relevance";
        }

        public static string GetInsertSQL(Business.DataObjectTag item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_Tag ([OBJ_ID], [TagWordLower]) VALUES (@OBJ_ID, @TagWordLower)";
        }

        public static string GetUpdateSQL(Business.DataObjectTag item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return string.Empty;
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return "INNER JOIN hiobj_Tag ON hiobj_Tag.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
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

        private static void SetParameters(Business.DataObjectTag item, SqlParameterCollection parameters)
        {
            parameters.AddWithValue("@TagWordLower", item.TagWordLower);
        }

        public static List<Business.TagReferenceItem> GetReferencedTags(int objectType, Guid? userId, string communityList)
        {
            List<Business.TagReferenceItem> tags = new List<TagReferenceItem>();

            string connectionString = Helper.GetSiemeConnectionString();
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandType = CommandType.Text;

                StringBuilder sb = new StringBuilder();
                sb.Append(@"SELECT DISTINCT Tags.OBJ_ID, Tags.OBJ_Title
                            FROM hirel_ObjToObj_OTO
                            INNER JOIN hitbl_DataObject_OBJ AS Objects ON hirel_ObjToObj_OTO.OTO_Obj1_ID = Objects.OBJ_ID
                            INNER JOIN hitbl_DataObject_OBJ AS Tags ON hirel_ObjToObj_OTO.OTO_Obj2_ID = Tags.OBJ_ID
                            WHERE hirel_ObjToObj_OTO.OTO_Obj1_Type = @ObjectType AND hirel_ObjToObj_OTO.OTO_Obj2_Type = 5");
                sqlCommand.Parameters.Add(SqlHelper.AddParameter("@ObjectType", SqlDbType.Int, objectType));
                if (userId.HasValue)
                {
                    sb.Append(" AND Objects.USR_ID = @UserId");
                    sqlCommand.Parameters.Add(SqlHelper.AddParameter("@UserId", SqlDbType.UniqueIdentifier, userId.Value));
                }
                if (!string.IsNullOrEmpty(communityList))
                {
                    sb.AppendFormat(" AND Objects.CTY_ID IN ('{0}')", communityList.Replace("|", "','"));
                }
                sb.Append(" ORDER BY Tags.OBJ_Title ASC");

                sqlCommand.CommandText = sb.ToString();
                connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    tags.Add(new TagReferenceItem()
                                 {
                                     TagId = reader.GetGuid(0),
                                     Title = reader.GetString(1)
                                 });
                }
                reader.Close();
            }
            finally
            {
                connection.Close();
            }

            return tags;
        }
    }
}
