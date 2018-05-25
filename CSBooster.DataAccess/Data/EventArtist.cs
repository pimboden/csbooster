using System;
using System.Data;
using System.Data.SqlClient;

namespace _4screen.CSB.DataAccess.Data
{
    public static class EventArtist
    {
        public static Business.EventArtist Load(Guid Id)
        {
            return EventArtists.Get(Id);
        }


        internal static void Insert(Business.EventArtist item)
        {
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;

            Guid ID = Guid.NewGuid();
            if (item.ID.HasValue && item.ID.Value != Guid.Empty)
                ID = item.ID.Value;
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_Event_Artist_Insert";
                GetData.Parameters.Add(SqlHelper.AddParameter("@ART_ID", SqlDbType.UniqueIdentifier, ID));
                GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Name", SqlDbType.NVarChar, 50, item.Name));
                GetData.Parameters.Add(SqlHelper.AddParameter("@EAT_Typ", SqlDbType.Int, item.Type));

                if (!string.IsNullOrEmpty(item.CountryISOCode))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Country_ISO", SqlDbType.NVarChar, 5, item.CountryISOCode));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Country_ISO", SqlDbType.NVarChar));

                if (!string.IsNullOrEmpty(item.Region))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Region", SqlDbType.NVarChar, 25, item.Region));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Region", SqlDbType.NVarChar));
                if (!string.IsNullOrEmpty(item.ZIP))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Zip", SqlDbType.NVarChar, 8, item.ZIP));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Zip", SqlDbType.NVarChar));

                if (!string.IsNullOrEmpty(item.City))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_City", SqlDbType.NVarChar, 50, item.City));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_City", SqlDbType.NVarChar));

                if (item.GeoLat != null && item.GeoLat != double.MinValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Geo_Lat", SqlDbType.Float, item.GeoLat.Value));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Geo_Lat", SqlDbType.Float));

                if (item.GeoLong != null && item.GeoLong != double.MinValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Geo_Long", SqlDbType.Float, item.GeoLong.Value));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Geo_Long", SqlDbType.Float));

                if (!string.IsNullOrEmpty(item.URL))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_URL", SqlDbType.NVarChar, 250, item.URL));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_URL", SqlDbType.NVarChar));

                if (!string.IsNullOrEmpty(item.CommunityURL))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_CTYURL", SqlDbType.NVarChar, 250, item.CommunityURL));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_CTYURL", SqlDbType.NVarChar));

                Conn.Open();
                GetData.ExecuteNonQuery();
                item.ID = ID;
            }
            finally
            {
                Conn.Close();
            }
        }

        internal static void AttachToEvent(Guid ArtistId, Guid EventId)
        {
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;

            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_Event_Artist_AttachToEvent";
                GetData.Parameters.Add(SqlHelper.AddParameter("@ART_ID", SqlDbType.UniqueIdentifier, ArtistId));
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, EventId));
                Conn.Open();
                GetData.ExecuteNonQuery();
            }
            finally
            {
                Conn.Close();
            }
        }

        internal static void DetachFromEvent(Guid ArtistId, Guid EventId)
        {
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;

            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_Event_Artist_DetachFromEvent";
                GetData.Parameters.Add(SqlHelper.AddParameter("@ART_ID", SqlDbType.UniqueIdentifier, ArtistId));
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, EventId));
                Conn.Open();
                GetData.ExecuteNonQuery();
            }
            finally
            {
                Conn.Close();
            }
        }

        internal static void Update(Business.EventArtist item)
        {
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;


            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_Event_Artist_Update";
                GetData.Parameters.Add(SqlHelper.AddParameter("@ART_ID", SqlDbType.UniqueIdentifier, item.ID.Value));
                GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Name", SqlDbType.NVarChar, 50, item.Name));
                GetData.Parameters.Add(SqlHelper.AddParameter("@EAT_Typ", SqlDbType.Int, item.Type));

                if (!string.IsNullOrEmpty(item.CountryISOCode))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Country_ISO", SqlDbType.NVarChar, 5, item.CountryISOCode));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Country_ISO", SqlDbType.NVarChar));

                if (!string.IsNullOrEmpty(item.Region))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Region", SqlDbType.NVarChar, 25, item.Region));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Region", SqlDbType.NVarChar));
                if (!string.IsNullOrEmpty(item.ZIP))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Zip", SqlDbType.NVarChar, 8, item.ZIP));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Zip", SqlDbType.NVarChar));

                if (!string.IsNullOrEmpty(item.City))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_City", SqlDbType.NVarChar, 50, item.City));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_City", SqlDbType.NVarChar));

                if (item.GeoLat != null && item.GeoLat != double.MinValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Geo_Lat", SqlDbType.Float, item.GeoLat.Value));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Geo_Lat", SqlDbType.Float));

                if (item.GeoLong != null && item.GeoLong != double.MinValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Geo_Long", SqlDbType.Float, item.GeoLong.Value));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_Geo_Long", SqlDbType.Float));

                if (!string.IsNullOrEmpty(item.URL))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_URL", SqlDbType.NVarChar, 250, item.URL));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_URL", SqlDbType.NVarChar));

                if (!string.IsNullOrEmpty(item.CommunityURL))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_CTYURL", SqlDbType.NVarChar, 250, item.CommunityURL));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ART_CTYURL", SqlDbType.NVarChar));

                Conn.Open();
                GetData.ExecuteNonQuery();
            }
            finally
            {
                Conn.Close();
            }
        }
    }
}