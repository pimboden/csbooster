using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using _4screen.CSB.Common;  

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectPollAnswer
    {
        public static void FillObject(Business.DataObjectPollAnswer item, SqlDataReader sqlReader)
        {
            item.Answer = (int)(sqlReader["Answer"]);
            item.Position = (int)(sqlReader["Position"]);
            item.Comment = sqlReader["Comment"].ToString();
            item.IP = sqlReader["IP"].ToString();
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return ", hiobj_PollAnswer.*";
        }

        public static string GetInsertSQL(Business.DataObjectPollAnswer item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_PollAnswer ([OBJ_ID],[Position],[Answer],[Comment],[IP]) VALUES (@OBJ_ID,@Position,@Answer,@AnswerComment,@AnswerIP)";
        }

        public static string GetUpdateSQL(Business.DataObjectPollAnswer item, SqlParameterCollection parameters)
        {
//            SetParameters(item, parameters);
//            return "UPDATE hiobj_PollAnswer SET [Answer] = @Answer, [Comment] = @AnswerComment";
            return string.Empty;
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return "INNER JOIN hiobj_PollAnswer ON hiobj_PollAnswer.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
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

        private static void SetParameters(Business.DataObjectPollAnswer item, SqlParameterCollection parameters)
        {
            parameters.AddWithValue("@Position", item.Position);
            parameters.AddWithValue("@Answer", item.Answer);
            if (!string.IsNullOrEmpty(item.Comment))  
                parameters.AddWithValue("@AnswerComment", item.Comment);
            else
                parameters.AddWithValue("@AnswerComment", DBNull.Value);
            parameters.AddWithValue("@AnswerIP", item.IP);
        }

        public static bool HasVoted(Guid questionId, int parentType, int childType, UserDataContext udc)
        {
            bool retVal = true;
            SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_DataObject_PollQuestion_HasVoted";
                GetData.Parameters.AddWithValue("@Parent_ID", questionId);
                GetData.Parameters.AddWithValue("@ParentType", parentType);
                GetData.Parameters.AddWithValue("@ChildType", childType);
                GetData.Parameters.AddWithValue("@USR_ID", udc.UserID);
                GetData.Parameters.AddWithValue("@IP", udc.UserIP.CropString(20));

                Conn.Open();
                retVal  = (GetData.ExecuteScalar().ToString() == "1");
            }
            finally
            {
                Conn.Close();
            }
            return retVal;
        }
    }
}
