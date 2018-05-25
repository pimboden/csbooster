using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace _4screen.CSB.DataAccess.Data
{
    public class EventArtists
    {
        public static List<Business.EventArtist> Load(Guid? Id, string Name, int? Type, string CountryISOCode, string Region, string ZIP, string City, int amount)
        {
            SqlDataReader sqlReader = null;
            List<Business.EventArtist> list = new List<_4screen.CSB.DataAccess.Business.EventArtist>();
            try
            {
                sqlReader = GetReaderAll(Id, Name, Type, CountryISOCode, Region, ZIP, City, amount);
                while (sqlReader.Read())
                {
                    Business.EventArtist item = new Business.EventArtist();
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

        public static List<Business.EventArtist> LoadAttached(Guid EventId, int amount)
        {
            SqlDataReader sqlReader = null;
            List<Business.EventArtist> list = new List<_4screen.CSB.DataAccess.Business.EventArtist>();
            try
            {
                sqlReader = GetReaderAttached(EventId, amount);
                while (sqlReader.Read())
                {
                    Business.EventArtist item = new Business.EventArtist();
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

        public static Business.EventArtist Get(Guid Id)
        {
            SqlDataReader sqlReader = null;
            Business.EventArtist objEventArtist = null;
            try
            {
                sqlReader = GetReaderAll(Id, null, null, null, null, null, null, 1);
                if (sqlReader.Read())
                {
                    objEventArtist = new Business.EventArtist();
                    FillObject(objEventArtist, sqlReader);
                }
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
                sqlReader = null;
            }
            return objEventArtist;
        }

        private static void FillObject(_4screen.CSB.DataAccess.Business.EventArtist item, SqlDataReader sqlReader)
        {
            item.ID = new Guid(sqlReader["ART_ID"].ToString());

            item.Name = sqlReader["ART_Name"].ToString();

            item.Type = Convert.ToInt32(sqlReader["EAT_Typ"]);

            item.CountryISOCode = sqlReader["ART_Country_ISO"] != DBNull.Value ? sqlReader["ART_Country_ISO"].ToString() : string.Empty;

            item.Region = sqlReader["ART_Region"] != DBNull.Value ? sqlReader["ART_Region"].ToString() : string.Empty;

            item.ZIP = sqlReader["ART_Zip"] != DBNull.Value ? sqlReader["ART_Zip"].ToString() : string.Empty;

            item.City = sqlReader["ART_City"] != DBNull.Value ? sqlReader["ART_City"].ToString() : string.Empty;

            item.GeoLat = sqlReader["ART_Geo_Lat"] != DBNull.Value ? Convert.ToDouble(sqlReader["ART_Geo_Lat"]) : double.MinValue;

            item.GeoLong = sqlReader["ART_Geo_Long"] != DBNull.Value ? Convert.ToDouble(sqlReader["ART_Geo_Long"]) : double.MinValue;

            item.URL = sqlReader["ART_URL"] != DBNull.Value ? sqlReader["ART_URL"].ToString() : string.Empty;

            item.CommunityURL = sqlReader["ART_CTYURL"] != DBNull.Value ? sqlReader["ART_CTYURL"].ToString() : string.Empty;
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
                GetData.CommandText = "hisp_Event_Artist_LoadAttached";

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
                GetData.CommandText = "hisp_Event_Artist_LoadAll";

                if (Id != null && Id.Value != Guid.Empty)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_ID", SqlDbType.UniqueIdentifier, Id.Value));
                if (!string.IsNullOrEmpty(Name))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Name", SqlDbType.NVarChar, 52, SqlHelper.PrepareLike(Name, true, true)));
                if (Type != null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@EAT_Typ", SqlDbType.Int, Type));
                if (!string.IsNullOrEmpty(CountryISOCode))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Country_ISO", SqlDbType.NVarChar, 5, SqlHelper.PrepareLike(CountryISOCode, false, false)));
                if (!string.IsNullOrEmpty(Region))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Region", SqlDbType.NVarChar, 27, SqlHelper.PrepareLike(Region, true, true)));
                if (!string.IsNullOrEmpty(ZIP))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Zip", SqlDbType.NVarChar, 10, SqlHelper.PrepareLike(ZIP, true, true)));
                if (!string.IsNullOrEmpty(City))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_City", SqlDbType.NVarChar, 52, SqlHelper.PrepareLike(City, true, true)));

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