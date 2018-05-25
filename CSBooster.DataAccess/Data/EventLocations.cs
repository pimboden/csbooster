using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace _4screen.CSB.DataAccess.Data
{
    public class EventLocations
    {
        public static List<Business.EventLocation> Load(Guid? Id, string Name, int? Type, string CountryISOCode, string Region, string ZIP, string City, int amount)
        {
            SqlDataReader sqlReader = null;
            List<Business.EventLocation> list = new List<_4screen.CSB.DataAccess.Business.EventLocation>();
            try
            {
                sqlReader = GetReaderAll(Id, Name, Type, CountryISOCode, Region, ZIP, City, amount);
                while (sqlReader.Read())
                {
                    Business.EventLocation item = new Business.EventLocation();
                    FillObject(item, sqlReader);
                    list.Add(item);
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

        public static List<Business.EventLocation> LoadAttached(Guid EventId, int amount)
        {
            SqlDataReader sqlReader = null;
            List<Business.EventLocation> list = new List<_4screen.CSB.DataAccess.Business.EventLocation>();
            try
            {
                sqlReader = GetReaderAttached(EventId, amount);
                while (sqlReader.Read())
                {
                    Business.EventLocation item = new Business.EventLocation();
                    FillObject(item, sqlReader);
                    list.Add(item);
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

        public static Business.EventLocation Get(Guid Id)
        {
            SqlDataReader sqlReader = null;
            Business.EventLocation objEventLocation = null;
            try
            {
                sqlReader = GetReaderAll(Id, null, null, null, null, null, null, 1);
                if (sqlReader.Read())
                {
                    objEventLocation = new Business.EventLocation();
                    FillObject(objEventLocation, sqlReader);
                }
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
                sqlReader = null;
            }
            return objEventLocation;
        }

        private static void FillObject(_4screen.CSB.DataAccess.Business.EventLocation item, SqlDataReader sqlReader)
        {
            item.ID = new Guid(sqlReader["LOC_ID"].ToString());

            item.Name = sqlReader["LOC_Name"].ToString();

            item.Type = Convert.ToInt32(sqlReader["LOC_Typ"]);

            item.CountryISOCode = sqlReader["LOC_Country_ISO"] != DBNull.Value ? sqlReader["LOC_Country_ISO"].ToString() : string.Empty;

            item.Region = sqlReader["LOC_Region"] != DBNull.Value ? sqlReader["LOC_Region"].ToString() : string.Empty;

            item.ZIP = sqlReader["LOC_Zip"] != DBNull.Value ? sqlReader["LOC_Zip"].ToString() : string.Empty;

            item.City = sqlReader["LOC_City"] != DBNull.Value ? sqlReader["LOC_City"].ToString() : string.Empty;

            item.GeoLat = sqlReader["LOC_Geo_Lat"] != DBNull.Value ? Convert.ToDouble(sqlReader["LOC_Geo_Lat"]) : double.MinValue;

            item.GeoLong = sqlReader["LOC_Geo_Long"] != DBNull.Value ? Convert.ToDouble(sqlReader["LOC_Geo_Long"]) : double.MinValue;

            item.URL = sqlReader["LOC_URL"] != DBNull.Value ? sqlReader["LOC_URL"].ToString() : string.Empty;

            item.CommunityURL = sqlReader["LOC_CTYURL"] != DBNull.Value ? sqlReader["LOC_CTYURL"].ToString() : string.Empty;
        }

        private static SqlDataReader GetReaderAttached(Guid EventId, int amount)
        {
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;

            SqlConnection Conn = new SqlConnection(strConn);
            SqlDataReader sqlReader = null;
            SqlCommand GetData = new SqlCommand();
            try
            {
                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_Event_Location_LoadAttached";

                if (EventId != null && EventId != Guid.Empty)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, EventId));

                GetData.Parameters.Add(SqlHelper.AddParameter("@Amount", SqlDbType.Int, amount));
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

        private static SqlDataReader GetReaderAll(Guid? Id, string Name, int? Type, string CountryISOCode, string Region, string ZIP, string City, int amount)
        {
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;

            SqlConnection Conn = new SqlConnection(strConn);
            SqlDataReader sqlReader = null;
            SqlCommand GetData = new SqlCommand();
            try
            {
                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_Event_Location_LoadAll";

                if (Id != null && Id.Value != Guid.Empty)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@LOC_ID", SqlDbType.UniqueIdentifier, Id.Value));
                if (!string.IsNullOrEmpty(Name))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@LOC_Name", SqlDbType.NVarChar, 52, SqlHelper.PrepareLike(Name, true, true)));
                if (Type != null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@LOC_Typ", SqlDbType.Int, Type));
                if (!string.IsNullOrEmpty(CountryISOCode))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@LOC_Country_ISO", SqlDbType.NVarChar, 5, SqlHelper.PrepareLike(CountryISOCode, false, false)));
                if (!string.IsNullOrEmpty(Region))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@LOC_Region", SqlDbType.NVarChar, 27, SqlHelper.PrepareLike(Region, true, true)));
                if (!string.IsNullOrEmpty(ZIP))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@LOC_Zip", SqlDbType.NVarChar, 10, SqlHelper.PrepareLike(ZIP, true, true)));
                if (!string.IsNullOrEmpty(City))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@LOC_City", SqlDbType.NVarChar, 52, SqlHelper.PrepareLike(City, true, true)));

                GetData.Parameters.Add(SqlHelper.AddParameter("@Amount", SqlDbType.Int, amount));
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
    }
}