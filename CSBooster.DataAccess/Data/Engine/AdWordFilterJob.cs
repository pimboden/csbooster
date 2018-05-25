//******************************************************************************
//  Company:    4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:     CSBooster.DataAccess - FilterEngine
//
//  Created:    #1.0.0.0                10.08.2007 11:02:36 / aw
//  Updated:   
//******************************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Data
{
    internal class AdWordFilterJob
    {
        private bool blnCancel = false;

        public bool Cancel
        {
            get { return blnCancel; }
            set { blnCancel = value; }
        }

        internal void ProcessDataObjectsAll()
        {
            FilterEngine.InitFilterAdWords();

            blnCancel = false;

            Dictionary<string, FilterObject> adWordFilterObjects = FilterEngine.GetAdWordFilterObjects();
            foreach (KeyValuePair<string, FilterObject> adWordFilterObject in adWordFilterObjects)
            {
                if (blnCancel)
                    break;

                int counter = 0;
                Console.WriteLine("Loading " + adWordFilterObject.Value.TypeName);
                SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
                System.Data.SqlClient.SqlDataReader sqlDataReader = null;
                try
                {
                    sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Command.CommandText = "hisp_DataObject_LoadIdsByType";
                    sqlConnection.Command.Parameters.Add(new SqlParameter("@ObjectTypeId", SqlDbType.Int));
                    sqlConnection.Command.Parameters["@ObjectTypeId"].Value = adWordFilterObject.Value.ObjectTypeId;
                    sqlDataReader = sqlConnection.Command.ExecuteReader(CommandBehavior.CloseConnection);
                    DateTime startTime = DateTime.Now;
                    while (sqlDataReader.Read())
                    {
                        if (blnCancel)
                            break;

                        FilterObjectAdWords(adWordFilterObject.Value.TypeName, adWordFilterObject.Value.ObjectTypeId, sqlDataReader["OBJ_ID"].ToString().ToGuid());
                        counter++;
                    }
                    sqlDataReader.Close();
                    DateTime endTime = DateTime.Now;
                    TimeSpan duration = endTime - startTime;
                    Console.WriteLine(counter + " items took " + duration + " secs");
                }
                catch (Exception e)
                {
                    Console.WriteLine("*** Error: " + e);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        internal void ProcessDataObjectsInCampaign(Guid campaignId)
        {
            FilterEngine.InitFilterAdWords();

            Dictionary<string, FilterObject> adWordFilterObjects = FilterEngine.GetAdWordFilterObjects();
            Dictionary<int, string> typeNameMapping = new Dictionary<int, string>();
            foreach (KeyValuePair<string, FilterObject> adWordFilterObject in adWordFilterObjects)
            {
                typeNameMapping.Add(adWordFilterObject.Value.ObjectTypeId, adWordFilterObject.Value.TypeName);
            }

            int counter = 0;
            Console.WriteLine("Loading " + campaignId);
            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            System.Data.SqlClient.SqlDataReader sqlDataReader = null;
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_Filter_CampaignObjects_LoadById";
                sqlConnection.Command.Parameters.Add(new SqlParameter("@CampaignId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@CampaignId"].Value = campaignId;
                sqlDataReader = sqlConnection.Command.ExecuteReader(CommandBehavior.CloseConnection);
                DateTime startTime = DateTime.Now;
                while (sqlDataReader.Read())
                {
                    FilterObjectAdWords(typeNameMapping[(int)sqlDataReader["OBJ_Type"]], (int)sqlDataReader["OBJ_Type"], sqlDataReader["OBJ_ID"].ToString().ToGuid());
                    counter++;
                }
                sqlDataReader.Close();
                DateTime endTime = DateTime.Now;
                TimeSpan duration = endTime - startTime;
                Console.WriteLine(counter + " items took " + duration + " secs");
            }
            catch (Exception e)
            {
                Console.WriteLine("*** Error: " + e);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        internal void ProcessDataObjectsForUser(Guid userId)
        {
            FilterEngine.InitFilterAdWords();

            Dictionary<string, FilterObject> adWordFilterObjects = FilterEngine.GetAdWordFilterObjects();
            Dictionary<int, string> typeNameMapping = new Dictionary<int, string>();
            foreach (KeyValuePair<string, FilterObject> adWordFilterObject in adWordFilterObjects)
            {
                typeNameMapping.Add(adWordFilterObject.Value.ObjectTypeId, adWordFilterObject.Value.TypeName);
            }

            int counter = 0;
            Console.WriteLine("Loading " + userId);
            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            System.Data.SqlClient.SqlDataReader sqlDataReader = null;
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_DataObject_LoadIdsByUserId";
                sqlConnection.Command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@UserId"].Value = userId;
                sqlDataReader = sqlConnection.Command.ExecuteReader(CommandBehavior.CloseConnection);
                DateTime startTime = DateTime.Now;
                while (sqlDataReader.Read())
                {
                    if (typeNameMapping.ContainsKey((int)sqlDataReader["OBJ_Type"]))
                    {
                        FilterObjectAdWords(typeNameMapping[(int)sqlDataReader["OBJ_Type"]], (int)sqlDataReader["OBJ_Type"], sqlDataReader["OBJ_ID"].ToString().ToGuid());
                        counter++;
                    }
                }
                sqlDataReader.Close();
                DateTime endTime = DateTime.Now;
                TimeSpan duration = endTime - startTime;
                Console.WriteLine(counter + " items took " + duration + " secs");
            }
            catch (Exception e)
            {
                Console.WriteLine("*** Error: " + e);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void FilterObjectAdWords(string typeName, int objectType, Guid objectId)
        {
            UserDataContext userDataContext = UserDataContext.GetUserDataContext();
            MethodInfo loadMethod = typeof(Business.DataObject).GetMethod("Load", new Type[] { typeof(Guid), typeof(ObjectShowState), typeof(bool) });
            Type type = null;
            if (!string.IsNullOrEmpty(Helper.GetObjectType(objectType).Assembly))
            {
                Assembly assembly = Assembly.Load(Helper.GetObjectType(objectType).Assembly);
                type = assembly.GetType(typeName);
            }
            else
            {
                type = Type.GetType(typeName);
            }
            MethodInfo genericLoadMethod = loadMethod.MakeGenericMethod(type);
            Business.DataObject dataObject = (Business.DataObject)genericLoadMethod.Invoke(null, new object[] { objectId, null, true });
            if (FilterEngine.FilterObjectAdWordsWithoutInit(dataObject))
            {
                dataObject.UpdateBackground();
            }
        }
    }
}