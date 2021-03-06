using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace _4screen.CSB.DataAccess.Data
{
    internal class Comment
    {
        private string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;

        public Guid Insert(Guid objectID, string text, Guid userID)
        {
            Guid retVal = Guid.Empty;
            Guid gNewID = Guid.NewGuid();

            StringBuilder sb = new StringBuilder(300 + text.Length);
            sb.Append("INSERT INTO hitbl_Comments_COM (COM_ID, OBJ_ID, COM_Text, USR_ID) ");
            sb.AppendFormat("VALUES ('{0}', '{1}', '{2}', '{3}') ", gNewID.ToString(), objectID, text.Replace("'", "''"), userID);
            sb.AppendFormat("UPDATE hitbl_DataObject_OBJ SET OBJ_CommentCount = OBJ_CommentCount + 1 WHERE OBJ_ID = '{0}'", objectID);

            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.Text;
                GetData.CommandText = sb.ToString();

                Conn.Open();
                GetData.ExecuteNonQuery();
                retVal = gNewID;
            }
            finally
            {
                Conn.Close();
            }

            return retVal;
        }

        /*public void Update(string id, string text)
      {
         StringBuilder sb = new StringBuilder(100 + text.Length);
         sb.Append("UPDATE hitbl_Comments_COM SET ");
         sb.AppendFormat("COM_Text = '{0}', COM_UpdatedDate = GETDATE() ", text.Replace("'", "''"));
         sb.AppendFormat("WHERE COM_ID = '{0}'", id);

         SqlConnection Conn = new SqlConnection(strConn);
         try
         {
            SqlCommand GetData = new SqlCommand();

            GetData.Connection = Conn;
            GetData.CommandType = CommandType.Text;
            GetData.CommandText = sb.ToString();

            Conn.Open();
            GetData.ExecuteNonQuery();
         }
         finally
         {
            Conn.Close();
         }
      }

      public void Delete(string id)
      {
         StringBuilder sb = new StringBuilder(200);
         sb.AppendFormat("UPDATE hitbl_DataObject_OBJ SET OBJ_CommentCount = OBJ_CommentCount - 1 FROM hitbl_DataObject_OBJ INNER JOIN hitbl_Comments_COM ON hitbl_DataObject_OBJ.OBJ_ID = hitbl_Comments_COM.OBJ_ID WHERE (hitbl_Comments_COM.COM_ID = '{0}') AND OBJ_CommentCount > 0 ", id);
         sb.AppendFormat("DELETE FROM hitbl_Comments_COM WHERE COM_ID = '{0}' ", id);


         SqlConnection Conn = new SqlConnection(strConn);
         try
         {
            SqlCommand GetData = new SqlCommand();

            GetData.Connection = Conn;
            GetData.CommandType = CommandType.Text;
            GetData.CommandText = sb.ToString();

            Conn.Open();
            GetData.ExecuteNonQuery();
         }
         finally
         {
            Conn.Close();
         }
      }*/
    }
}