// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data;
using System.Data.SqlClient;
using _4screen.CSB.Common;

//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//	Created:	#0.5.2.0		04.07.2007 / TS
//									- Handling zur Überwachung von Action-Prozessen (zB. Video-Konvertierung)
//******************************************************************************

namespace _4screen.CSB.DataAccess.Data
{
    internal class MonitoringLog
    {
        private string strConn = Helper.GetSiemeConnectionString();

        internal void Insert(Business.MonitoringLog item)
        {
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = Conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "hisp_MonitoringLog_Insert";
                cmd.Parameters.Add(SqlHelper.AddParameter("@MOL_Transaction_ID", SqlDbType.NVarChar, 50, item.TransactionID));
                cmd.Parameters.Add(SqlHelper.AddParameter("@MOL_BaseAction", SqlDbType.NVarChar, 250, item.BaseAction));
                cmd.Parameters.Add(SqlHelper.AddParameter("@MOL_State", SqlDbType.Int, (int)item.State));
                if (item.ObjectID.HasValue)
                    cmd.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, item.ObjectID));
                else
                    cmd.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, DBNull.Value));
                cmd.Parameters.Add(SqlHelper.AddParameter("@MOL_Step", SqlDbType.Int, (int)item.Step));
                cmd.Parameters.Add(SqlHelper.AddParameter("@MOL_StepDescription", SqlDbType.NVarChar, 500, item.StepDescription));
                cmd.Parameters.Add(SqlHelper.AddParameter("@MOL_Message", SqlDbType.NVarChar, item.Message));

                Conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                Conn.Close();
            }
        }
    } // END CLASS
} // END NAMESPACE