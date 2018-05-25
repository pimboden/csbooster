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
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Data
{
    internal static class InfoObjects
    {
        public static List<Business.InfoObject> Load(Guid? communityID, Guid? userID, int? objectType, Guid? userIDLogedIn)
        {
            List<Business.InfoObject> list = new List<Business.InfoObject>();
            SqlDataReader sqlReader = null;
            try
            {
                sqlReader = GetReader(userIDLogedIn, communityID, userID, objectType);
                if (sqlReader != null)
                {
                    while (sqlReader.Read())
                    {
                        Business.InfoObject item = new Business.InfoObject();
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

            return list;
        }

        public static SqlDataReader GetReader(Guid? userIDLogedIn, Guid? communityID, Guid? userID, int? objectType)
        {
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;

            SqlConnection Conn = new SqlConnection(strConn);
            SqlDataReader sqlReader = null;
            SqlCommand GetData = new SqlCommand();
            try
            {
                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                if (communityID.HasValue)
                {
                    GetData.CommandText = "hisp_Info_Object_Community_Load";
                    GetData.Parameters.Add(SqlHelper.AddParameter("@CTY_ID", SqlDbType.UniqueIdentifier, communityID.Value));
                }
                else if (userID.HasValue)
                {
                    GetData.CommandText = "hisp_Info_Object_User_Load";
                    GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier, userID.Value));
                }

                if (objectType != null && objectType != 0)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Type", SqlDbType.Int, (int) objectType));

                if (userIDLogedIn.HasValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID_LogedIn", SqlDbType.UniqueIdentifier, userIDLogedIn.Value));

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

        private static void FillObject(Business.InfoObject item, SqlDataReader sqlReader)
        {
            item.Count = Convert.ToInt32(sqlReader["OBJ_Count"].ToString());
            item.ObjectType = Convert.ToInt32(sqlReader["OBJ_Type"]);
        }
    }
}