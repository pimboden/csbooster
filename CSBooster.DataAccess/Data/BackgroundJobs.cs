//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		24.10.2007 / PT
//             #1.2.0.0    23.01.2008 / PT   QuickLoad (SQL) anpassen / Objekttypen erweitert 
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Data
{
    internal static class BackgroundJobs
    {
        public static void ExecuteStoreProcedure(string name)
        {
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;

            SqlConnection Conn = new SqlConnection(strConn);
            SqlCommand GetData = new SqlCommand();
            try
            {
                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = name;
                GetData.CommandTimeout = 3600; 

                Conn.Open();
                GetData.ExecuteNonQuery();
                Conn.Close();  
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }
        }
   }
}