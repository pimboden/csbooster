// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DialogEngine
    {
        // Class for filtering a dialog list
        private class PageNameMatcher
        {
            private List<string> pageNames;

            internal PageNameMatcher(List<string> pageNames)
            {
                this.pageNames = pageNames;
            }

            internal bool IsMatch(Business.Dialog dialog)
            {
                foreach (string pageName in pageNames)
                {
                    if (dialog.PageName == pageName)
                        return true;
                }
                return false;
            }
        }

        private static List<Business.Dialog> dialogList = null;

        private static void InitDialogEngine()
        {
            dialogList = new List<Business.Dialog>();

            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            System.Data.SqlClient.SqlDataReader sqlDataReader = null;
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_Dialogs_LoadAll";
                sqlDataReader = sqlConnection.Command.ExecuteReader(CommandBehavior.CloseConnection);
                while (sqlDataReader.Read())
                {
                    Dictionary<string, string> parameterTable = new Dictionary<string, string>();
                    string rawParameters = sqlDataReader["DIA_Parameter"].ToString();
                    if (!string.IsNullOrEmpty(rawParameters))
                    {
                        string[] parametersArray = rawParameters.Split(new char[] {';'});
                        foreach (string parameterPair in parametersArray)
                        {
                            string[] parameterPairArray = parameterPair.Split(new char[] {'='});
                            if (parameterPairArray.Length == 2)
                            {
                                parameterTable.Add(parameterPairArray[0], parameterPairArray[1]);
                            }
                        }
                    }

                    MethodInfo methodInfo = typeof (Data.Dialog).GetMethod(sqlDataReader["DIA_Condition"].ToString(), BindingFlags.Static | BindingFlags.NonPublic, null, new Type[] {typeof (Guid), typeof (Business.Dialog)}, null);

                    Business.Dialog dialog = new Business.Dialog(new Guid(sqlDataReader["DIA_ID"].ToString()), sqlDataReader["DIA_Page"].ToString(), (DateTime) sqlDataReader["DIA_ActiveFromDate"], methodInfo, parameterTable, (int) sqlDataReader["DIA_ConditionTrueValue"], sqlDataReader["DIA_Title"].ToString(), sqlDataReader["DIA_Content"].ToString());
                    dialogList.Add(dialog);
                }
                sqlDataReader.Close();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        internal static List<Business.Dialog> GetDialogByPageName(List<string> pageNames, Guid userId)
        {
            if (dialogList == null)
                InitDialogEngine();

            List<Business.Dialog> dialogsToShow = new List<Business.Dialog>();
            PageNameMatcher pageNameMatcher = new PageNameMatcher(pageNames);
            List<Business.Dialog> matchedDialogs = dialogList.FindAll(pageNameMatcher.IsMatch);
            foreach (Business.Dialog dialog in matchedDialogs)
            {
                if (Data.Dialog.IsConditionTrue(userId, dialog))
                {
                    dialogsToShow.Add(dialog);
                }
            }
            return dialogsToShow;
        }
    }
}