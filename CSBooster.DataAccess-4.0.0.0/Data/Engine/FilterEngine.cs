// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Data
{
    internal class FilterEngine
    {
        private static Dictionary<string, FilterObject> badWordFilterObjects = new Dictionary<string, FilterObject>();
        private static Dictionary<string, FilterObject> adWordFilterObjects = new Dictionary<string, FilterObject>();
        private static List<IWordFilter> badWordFilters = new List<IWordFilter>();
        private static List<IWordFilter> adWordFilters = new List<IWordFilter>();
        private static FilterEngineConfig filterEngineConfig;
        private static bool userWantsFiltering = true;

        static FilterEngine()
        {
            // Read filter object config
            FilterEngineConfig.ReadFilterObjectsConfig("badWordFilter", badWordFilterObjects);
            FilterEngineConfig.ReadFilterObjectsConfig("adWordFilter", adWordFilterObjects);

            // Read filter engine config
            filterEngineConfig = new FilterEngineConfig();
        }

        internal static void InitFilterBadWords()
        {
            badWordFilters.Clear();

            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            sqlConnection.Command.CommandType = CommandType.StoredProcedure;

            // Read bad words
            try
            {
                sqlConnection.Command.CommandText = "hisp_Filter_BadWords_LoadAll";
                System.Data.SqlClient.SqlDataReader sqlDataReader = sqlConnection.Command.ExecuteReader(CommandBehavior.CloseConnection);
                while (sqlDataReader.Read())
                {
                    try
                    {
                        string[] actions = sqlDataReader["FBW_Action"].ToString().Split(new char[] {';'});
                        BadWordFilterActions badWordFilterActions = BadWordFilterActions.None;
                        foreach (string action in actions)
                        {
                            badWordFilterActions |= (BadWordFilterActions) Enum.Parse(typeof (BadWordFilterActions), action);
                        }

                        Dictionary<string, string> parameterTable = new Dictionary<string, string>();
                        string rawParameters = sqlDataReader["FBW_Parameter"].ToString();
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

                        string word = sqlDataReader["FBW_Word"].ToString();
                        if (!string.IsNullOrEmpty(word))
                        {
                            badWordFilters.Add(new BadWordFilter(word, bool.Parse(sqlDataReader["FBW_IsExactMatch"].ToString()), badWordFilterActions, parameterTable));
                        }
                    }
                    catch
                    {
                    }
                }

                sqlDataReader.Close();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        internal static void InitFilterAdWords()
        {
            adWordFilters.Clear();

            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            sqlConnection.Command.CommandType = CommandType.StoredProcedure;
            try
            {
                // Read ad words
                sqlConnection.Command.CommandText = "hisp_Filter_AdWords_LoadAll";
                System.Data.SqlClient.SqlDataReader sqlDataReader = sqlConnection.Command.ExecuteReader(CommandBehavior.CloseConnection);
                while (sqlDataReader.Read())
                {
                    try
                    {
                        string word = sqlDataReader["FAW_Word"].ToString();
                        if (!string.IsNullOrEmpty(word))
                        {
                            adWordFilters.Add(new AdWordFilter(word, bool.Parse(sqlDataReader["FAW_IsExactMatch"].ToString()), (AdWordFilterActions) Enum.Parse(typeof (AdWordFilterActions), sqlDataReader["FAW_Action"].ToString()), new Guid(sqlDataReader["FAW_CampaignId"].ToString())));
                        }
                    }
                    catch
                    {
                    }
                }
                sqlDataReader.Close();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        internal static FilterEngineConfig GetFilterEngineConfig()
        {
            return filterEngineConfig;
        }

        internal static Dictionary<string, FilterObject> GetAdWordFilterObjects()
        {
            return adWordFilterObjects;
        }

        internal static string GetApplicationPath()
        {
            return WebRootPath.Instance.ToString();
        }

        internal static string FilterStringBadWords(string value, FilterObjectTypes filterObjectTypes, Guid objectId, Guid userId)
        {
            InitFilterBadWords();

            Type type = null;
            Business.DataObject dataObject = Business.DataObject.Load<Business.DataObject>(objectId);
            // TODO: Make comment a dataobject and we don't need such hacks
            try
            {
                type = Type.GetType(string.Format("_4screen.CSB.DataAccess.Business.DataObject{0}", dataObject.ObjectType));
            }
            catch
            {
            }

            // Apply the filters
            foreach (BadWordFilter badWordFilter in badWordFilters)
            {
                value = badWordFilter.Process(value, type, filterObjectTypes, objectId, userId);
            }
            return value;
        }

        internal static void FilterObject(Business.DataObject item)
        {
            FilterObjectBadWords(item);
            FilterObjectAdWords(item);
        }

        internal static void FilterObjectBadWords(Business.DataObject item)
        {
            InitFilterBadWords();

            // Check if the current object has to be filtered for bad words
            if (badWordFilterObjects.ContainsKey(item.GetType().ToString()))
            {
                FilterObjectProperties(item, badWordFilters, badWordFilterObjects);
            }
        }

        internal static bool FilterObjectAdWords(Business.DataObject item)
        {
            InitFilterAdWords();
            userWantsFiltering = AdWordHelper.UserWantsAds(item.UserID.Value);

            bool hasItemChanged = false;
            // Check if the current object has to be filtered for ad words
            if (adWordFilterObjects.ContainsKey(item.GetType().ToString()))
            {
                AdWordHelper.ResetCampaignObjects(item.ObjectID.Value);
                hasItemChanged = FilterObjectProperties(item, adWordFilters, adWordFilterObjects);
            }
            return hasItemChanged;
        }

        internal static bool FilterObjectAdWordsWithoutInit(Business.DataObject item)
        {
            userWantsFiltering = AdWordHelper.UserWantsAds(item.UserID.Value);

            bool hasItemChanged = false;
            // Check if the current object has to be filtered for ad words
            if (adWordFilterObjects.ContainsKey(item.GetType().ToString()))
            {
                AdWordHelper.ResetCampaignObjects(item.ObjectID.Value);
                hasItemChanged = FilterObjectProperties(item, adWordFilters, adWordFilterObjects);
            }
            return hasItemChanged;
        }

        private static bool FilterObjectProperties(Business.DataObject item, List<IWordFilter> wordFilters, Dictionary<string, FilterObject> wordFilterObjects)
        {
            bool hasItemChanged = false;
            Type filterObjectType = item.GetType();
            FilterObject filterObject = wordFilterObjects[item.GetType().ToString()];
            foreach (FilterObjectProperty filterObjectProperty in filterObject.Properties)
            {
                // Get value for filtering
                PropertyInfo filterObjectPropertyName = filterObjectType.GetProperty(filterObjectProperty.Name, typeof (string));
                string propertyValue = (string) filterObjectPropertyName.GetValue(item, null);

                // Apply the filters
                string processedPropertyValue = propertyValue;
                if (userWantsFiltering) // Don't apply filter, if the user disabled them
                {
                    foreach (IWordFilter wordFilter in wordFilters)
                    {
                        processedPropertyValue = wordFilter.Process(processedPropertyValue, item.GetType(), FilterObjectTypes.DataObject, item.ObjectID.Value, item.UserID.Value);
                    }
                }

                // If the filter had some matches, store filtered value
                if (processedPropertyValue != propertyValue && userWantsFiltering)
                    // Don't store value, if the user disabled the filter
                {
                    hasItemChanged = true;
                    if (filterObjectProperty.LinkedName != "")
                    {
                        PropertyInfo filterObjectPropertyLinkedName = filterObjectType.GetProperty(filterObjectProperty.LinkedName, typeof (string));
                        filterObjectPropertyLinkedName.SetValue(item, processedPropertyValue, null);
                    }
                    else
                    {
                        filterObjectPropertyName.SetValue(item, processedPropertyValue, null);
                    }
                }
                else // If the filter didn't had any matches
                {
                    if (filterObjectProperty.LinkedName != "")
                    {
                        PropertyInfo filterObjectPropertyLinkedName = filterObjectType.GetProperty(filterObjectProperty.LinkedName, typeof (string));
                        string propertyLinkedValue = (string) filterObjectPropertyLinkedName.GetValue(item, null);

                        // If the linked property has been set before, clear it
                        if (propertyLinkedValue != processedPropertyValue)
                        {
                            hasItemChanged = true;
                            filterObjectPropertyLinkedName.SetValue(item, "", null);
                        }
                        else if (propertyLinkedValue != processedPropertyValue && !userWantsFiltering)
                            // Reset value, if the user disabled the filter
                        {
                            hasItemChanged = true;
                            filterObjectPropertyLinkedName.SetValue(item, "", null);
                        }
                    }
                }
            }
            return hasItemChanged;
        }
    }
}