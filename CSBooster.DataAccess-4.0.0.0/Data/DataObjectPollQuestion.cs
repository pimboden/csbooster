// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data.SqlClient;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectPollQuestion
    {
        public static void FillObject(Business.DataObjectPollQuestion item, SqlDataReader sqlReader)
        {
            item.PollType = (Business.DataObjectPollQuestion.QuestionPollType)(int)(sqlReader["PollType"]);
            item.AnonymousAllowed = bool.Parse(sqlReader["AnonymousAllowed"].ToString());
            item.ShowResult = (Business.DataObjectPollQuestion.QuestionShowResult)(int)(sqlReader["ShowResult"]);
            item.ShowAnswerCount = (Business.DataObjectPollQuestion.QuestionShowAnswerCount)(int)(sqlReader["ShowAnswerCount"]);
            item.PollLayout = (Business.DataObjectPollQuestion.QuestionPollLayout)(int)(sqlReader["PollLayout"]);
            if (sqlReader["Answers"] != DBNull.Value) item.answerXml.LoadXml(sqlReader["Answers"].ToString());
            item.TextRight = sqlReader["TextRight"].ToString();
            item.TextFalse = sqlReader["TextFalse"].ToString();
            item.TextPartially = sqlReader["TextPartially"].ToString(); 
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return ", hiobj_PollQuestion.*";
        }

        public static string GetInsertSQL(Business.DataObjectPollQuestion item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_PollQuestion ([OBJ_ID],[PollType],[AnonymousAllowed],[ShowAnswerCount],[ShowResult],[PollLayout],[Answers],[TextRight],[TextFalse],[TextPartially]) VALUES (@OBJ_ID,@PollType,@AnonymousAllowed,@ShowAnswerCount,@ShowResult,@PollLayout,@Answers,@TextRight,@TextFalse,@TextPartially)";
        }

        public static string GetUpdateSQL(Business.DataObjectPollQuestion item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "UPDATE hiobj_PollQuestion SET [PollType]=@PollType,[AnonymousAllowed]=@AnonymousAllowed,[ShowAnswerCount]=@ShowAnswerCount,[ShowResult]=@ShowResult,[PollLayout]=@PollLayout,[Answers]=@Answers,[TextRight]=@TextRight,[TextFalse]=@TextFalse,[TextPartially]=@TextPartially";
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return "INNER JOIN hiobj_PollQuestion ON hiobj_PollQuestion.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
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

        private static void SetParameters(Business.DataObjectPollQuestion item, SqlParameterCollection parameters)
        {
            parameters.AddWithValue("@PollType", (int)item.PollType);
            parameters.AddWithValue("@AnonymousAllowed", item.AnonymousAllowed);
            parameters.AddWithValue("@ShowAnswerCount", item.ShowAnswerCount);
            parameters.AddWithValue("@ShowResult", item.ShowResult);
            parameters.AddWithValue("@PollLayout", (int)item.PollLayout); 
            parameters.AddWithValue("@Answers", item.answerXml.OuterXml);
            if (!string.IsNullOrEmpty(item.TextRight)) 
                parameters.AddWithValue("@TextRight", item.TextRight);
            else
                parameters.AddWithValue("@TextRight", DBNull.Value);

            if (!string.IsNullOrEmpty(item.TextFalse)) 
                parameters.AddWithValue("@TextFalse", item.TextFalse);
            else
                parameters.AddWithValue("@TextFalse", DBNull.Value);

            if (!string.IsNullOrEmpty(item.TextPartially)) 
                parameters.AddWithValue("@TextPartially", item.TextPartially);
            else
                parameters.AddWithValue("@TextPartially", DBNull.Value);
        }
    }
}
