// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Data;
using System.Data.SqlClient;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Data
{
    public static class Help
    {
        // load the short help text for the given ID's
        internal static string GetShortHelp(string ID, string subID, string langCD)
        {
            string retString = string.Empty;
            string strConn = Helper.GetSiemeConnectionString();

            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_Help_Load";
                GetData.Parameters.Add(SqlHelper.AddParameter("@ID", SqlDbType.NVarChar, 50, ID));
                GetData.Parameters.Add(SqlHelper.AddParameter("@SUBID", SqlDbType.NVarChar, 50, subID));
                GetData.Parameters.Add(SqlHelper.AddParameter("@LangCd", SqlDbType.Char, 5, langCD));

                Conn.Open();
                SqlDataReader DR = GetData.ExecuteReader(CommandBehavior.CloseConnection);

                if (DR.Read())
                {
                    retString = DR["HEL_ShortText"].ToString();
                }
                DR.Close();
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }

            return retString;
        }

        // load the long help text for the given ID's
        internal static string GetLongHelp(string ID, string subID, string langCD)
        {
            string retString = string.Empty;
            string strConn = Helper.GetSiemeConnectionString();

            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_Help_Load";
                GetData.Parameters.Add(SqlHelper.AddParameter("@ID", SqlDbType.NVarChar, 50, ID));
                GetData.Parameters.Add(SqlHelper.AddParameter("@SUBID", SqlDbType.NVarChar, 50, subID));
                GetData.Parameters.Add(SqlHelper.AddParameter("@LangCd", SqlDbType.Char, 5, langCD));

                Conn.Open();
                SqlDataReader DR = GetData.ExecuteReader(CommandBehavior.CloseConnection);

                if (DR.Read())
                {
                    retString = DR["HEL_Text"].ToString();
                }
                DR.Close();
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }

            return retString;
        }
    } // END CLASS
} // END NAMESPACE