using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace CSBoosterObjectConvert
{
    class Program
    {
        public enum ObjectType
        {
            None = 0,
            Community = 1,
            User = 2,
            Picture = 3,
            Video = 4,
            Tag = 5,
            Audio = 6,
            Article = 7,
            Forum = 10,
            ForumTopic = 11,
            ForumTopicItem = 12,
            SlideShow = 13,
            Folder = 14,
            Event = 15,
            News = 16,
            PinboardSearch = 17,
            PinboardOffer = 18,
            ProfileCommunity = 19,
            Page = 20,
            Document = 21,
            Generic = 999
        }

        public enum PictureVersion
        {
            A,
            XS,
            S,
            M,
            L
        }

        static string connString = string.Empty;
        static bool updateDataObjectXml = false;

        static void Main(string[] args)
        {
            connString = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;    
            foreach (ObjectType objectType in Enum.GetValues(typeof(ObjectType)))
            {
                string tableName = string.Format("hiobj_{0}", objectType.ToString());

                if (CheckTable(tableName) >= 0)
                {
                    try
                    {
                        DeleteRecords(tableName);
                        int convertet = FillTable(objectType, tableName);
                        int soll = CheckTable(tableName);

                        if (soll != convertet)
                        {
                            string fehler = soll.ToString() + "/" + convertet.ToString(); 
                        }
                    }
                    catch (Exception exc)
                    {
                        string err = exc.ToString();
                    }
                }
            }
        }

        private static int FillTable(ObjectType objectType, string tableName)
        {
            SqlConnection con = null;

            int count = 0;
            try
            {
                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(string.Format("SELECT OBJ_ID FROM hitbl_DataObject_OBJ WHERE OBJ_Type = {0}", (int)objectType), con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    count++;
                    Guid id = reader.GetGuid(0);
                    if (objectType == ObjectType.Article)
                        TransferArticle(id);
                    else if (objectType == ObjectType.Audio)
                        TransferAudio(id);
                    else if (objectType == ObjectType.Community)
                        TransferCommunity(id);
                    else if (objectType == ObjectType.Document)
                        TransferDocument(id);
                    else if (objectType == ObjectType.Event)
                        TransferEvent(id);
                    else if (objectType == ObjectType.Folder)
                        TransferFolder(id);
                    else if (objectType == ObjectType.Forum)
                        TransferForum(id);
                    else if (objectType == ObjectType.ForumTopic)
                        TransferForumTopic(id);
                    else if (objectType == ObjectType.ForumTopicItem)
                        TransferForumTopicItem(id);
                    else if (objectType == ObjectType.Generic)
                        TransferGeneric(id);
                    else if (objectType == ObjectType.News)
                        TransferNews(id);
                    else if (objectType == ObjectType.Page)
                        TransferPage(id);
                    else if (objectType == ObjectType.Picture)
                        TransferPicture(id);
                    else if (objectType == ObjectType.PinboardOffer)
                        TransferPinboardOffer(id);
                    else if (objectType == ObjectType.PinboardSearch)
                        TransferPinboardSearch(id);
                    else if (objectType == ObjectType.SlideShow)
                        TransferSlideShow(id);
                    else if (objectType == ObjectType.Tag)
                        TransferTag(id);
                    else if (objectType == ObjectType.ProfileCommunity)
                        TransferProfileCommunity(id);
                    else if (objectType == ObjectType.User)
                        TransferUser(id);
                    else if (objectType == ObjectType.Video)
                        TransferVideo(id);
                    else
                    { 
                        string err = "fehler";
                    }

                 }
                reader.Close();

                if (objectType == ObjectType.User)
                    UpdateUserProfileData();
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }
            return count;
        }

        private static void DeleteRecords(string tableName)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(string.Format("DELETE FROM {0}", tableName), con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }
        }

        private static void TransferArticle(Guid id)
        {
            const string SQL = "INSERT INTO hiobj_Article (OBJ_ID, ArticleText, ArticleTextLinked) VALUES(@V0, @V1, @V2)";

            SqlConnection con = null;
            try
            {
                XmlDocument xmlDoc = GetObjectXml(id);

                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@V0", id);
                cmd.Parameters.AddWithValue("@V1", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "ArticleText", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V2", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "ArticleTextLinked", string.Empty).SqlNULL());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (updateDataObjectXml)
                    UpdateDataObjectXml(id);
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }
        }

        private static void TransferAudio(Guid id)
        {
            const string SQL = "INSERT INTO hiobj_Audio (OBJ_ID, OriginalFormat, SizeByte, Location, Interpreter, Producer, Album, Genere) VALUES(@V0, @V1, @V2, @V3, @V4, @V5, @V6, @V7)";

            SqlConnection con = null;
            try
            {
                XmlDocument xmlDoc = GetObjectXml(id);

                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@V0", id);
                cmd.Parameters.AddWithValue("@V1", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "OriginalFormat", 0));
                cmd.Parameters.AddWithValue("@V2", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "SizeByte", 0));
                cmd.Parameters.AddWithValue("@V3", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Location", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V4", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Interpreter", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V5", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Producer", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V6", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Album", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V7", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Genere", string.Empty).SqlNULL());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (updateDataObjectXml)
                    UpdateDataObjectXml(id);
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }
        }

        private static void TransferCommunity(Guid id)
        {
            const string SQL = "INSERT INTO hiobj_Community (OBJ_ID, VirtualURL, Managed, CreateGroupUser, UploadUsers, Emphasis) VALUES(@V0, @V1, @V2, @V3, @V4, @V5)";

            SqlConnection con = null;
            try
            {
                XmlDocument xmlDoc = GetObjectXml(id);

                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@V0", id);
                cmd.Parameters.AddWithValue("@V1", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "VirtualURL", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V2", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Managed", false));
                cmd.Parameters.AddWithValue("@V3", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "CreateGroupUser", 0));
                cmd.Parameters.AddWithValue("@V4", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "item.UploadUsers", 0));
                cmd.Parameters.AddWithValue("@V5", GetSpezialList(xmlDoc, "Emphasis"));

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (updateDataObjectXml)
                    UpdateDataObjectXml(id);
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }
        }

        private static void TransferDocument(Guid id)
        {
            const string SQL = "INSERT INTO hiobj_Document (OBJ_ID, SizeByte, URLDocument, Author, Version) VALUES(@V0, @V1, @V2, @V3, @V4)";

            SqlConnection con = null;
            try
            {
                XmlDocument xmlDoc = GetObjectXml(id);

                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@V0", id);
                cmd.Parameters.AddWithValue("@V1", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "SizeByte", 0));
                cmd.Parameters.AddWithValue("@V2", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "URLDocument", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V3", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Author", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V4", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Version", string.Empty).SqlNULL());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (updateDataObjectXml)
                    UpdateDataObjectXml(id);
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }

        }

        private static void TransferEvent(Guid id)
        {
            const string SQL = "INSERT INTO hiobj_Event (OBJ_ID, Content, Time, Age, Price, EventTypeIds, MusicStyleIds, JoinedUser) VALUES(@V0, @V1, @V2, @V3, @V4, @V5, @V6, @V7)";

            SqlConnection con = null;
            try
            {
                XmlDocument xmlDoc = GetObjectXml(id);

                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@V0", id);
                cmd.Parameters.AddWithValue("@V1", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Content", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V2", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Time", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V3", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Age", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V4", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Price", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V5", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "EventTypeIds", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V6", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "MusicStyleIds", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V7", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "JoinedUser", string.Empty).SqlNULL());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (updateDataObjectXml)
                    UpdateDataObjectXml(id);
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }
        }

        private static void TransferFolder(Guid id)
        {
            const string SQL = "INSERT INTO hiobj_Folder (OBJ_ID, AllowMemberEdit) VALUES(@V0, @V1)";

            SqlConnection con = null;
            try
            {
                XmlDocument xmlDoc = GetObjectXml(id);

                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@V0", id);
                cmd.Parameters.AddWithValue("@V1", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "AllowMemberEdit", false));

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (updateDataObjectXml)
                    UpdateDataObjectXml(id);
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }
        }

        private static void TransferForum(Guid id)
        {
            const string SQL = "INSERT INTO hiobj_Forum (OBJ_ID, PublicTopicCreationAllowed) VALUES(@V0, @V1)";

            SqlConnection con = null;
            try
            {
                XmlDocument xmlDoc = GetObjectXml(id);

                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@V0", id);
                cmd.Parameters.AddWithValue("@V1", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "PublicTopicCreationAllowed", true));

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (updateDataObjectXml)
                    UpdateDataObjectXml(id);
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }
        }

        private static void TransferForumTopic(Guid id)
        {
            const string SQL = "INSERT INTO hiobj_ForumTopic (OBJ_ID, TopicState, TopicItemCount, LastTopicItemDate, LastTopicItemID, LastTopicItemUserID, LastTopicItemNickname) VALUES(@V0, @V1, @V2, @V3, @V4, @V5, @V6)";

            SqlConnection con = null;
            try
            {
                XmlDocument xmlDoc = GetObjectXml(id);

                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@V0", id);
                cmd.Parameters.AddWithValue("@V1", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "TopicState", 1));
                cmd.Parameters.AddWithValue("@V2", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "TopicItemCount", 0));
                if (XmlHelper.GetElementValue(xmlDoc.DocumentElement, "LastTopicItemDate", DateTime.MinValue) != DateTime.MinValue)  
                    cmd.Parameters.AddWithValue("@V3", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "LastTopicItemDate", DateTime.MinValue));
                else
                    cmd.Parameters.AddWithValue("@V3", DBNull.Value);

                cmd.Parameters.AddWithValue("@V4", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "LastTopicItemID", string.Empty).SqlGuid());
                cmd.Parameters.AddWithValue("@V5", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "LastTopicItemUserID", string.Empty).SqlGuid());
                cmd.Parameters.AddWithValue("@V6", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "LastTopicItemNickname", string.Empty).SqlNULL());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (updateDataObjectXml)
                    UpdateDataObjectXml(id);
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }
        }

        private static void TransferForumTopicItem(Guid id)
        {
            const string SQL = "INSERT INTO hiobj_ForumTopicItem (OBJ_ID, TopicItemValue, TopicItemReference) VALUES(@V0, @V1, @V2)";

            SqlConnection con = null;
            try
            {
                XmlDocument xmlDoc = GetObjectXml(id);

                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@V0", id);
                cmd.Parameters.AddWithValue("@V1", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "TopicItemValue", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V2", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "TopicItemReference", string.Empty).SqlGuid());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (updateDataObjectXml)
                    UpdateDataObjectXml(id);
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }
        }

        private static void TransferGeneric(Guid id)
        {
            const string SQL = "INSERT INTO hiobj_Generic (OBJ_ID, GenericData) VALUES(@V0, @V1)";

            SqlConnection con = null;
            try
            {
                XmlDocument xmlDoc = GetObjectXml(id);

                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@V0", id);
                cmd.Parameters.AddWithValue("@V1", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "GenericData", string.Empty).SqlNULL());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (updateDataObjectXml)
                    UpdateDataObjectXml(id);
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }
        }

        private static void TransferNews(Guid id)
        {
            const string SQL = "INSERT INTO hiobj_News (OBJ_ID, NewsText, NewsTextLinked, ReferenceURL, Links) VALUES(@V0, @V1, @V2, @V3, @V4)";

            SqlConnection con = null;
            try
            {
                XmlDocument xmlDoc = GetObjectXml(id);

                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@V0", id);
                cmd.Parameters.AddWithValue("@V1", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "NewsText", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V2", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "NewsTextLinked", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V3", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "RefURL", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V4", GetSpezialList(xmlDoc, "Links"));


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (updateDataObjectXml)
                    UpdateDataObjectXml(id);
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }
        }

        private static void TransferPage(Guid id)
        {
            const string SQL = "INSERT INTO hiobj_Page (OBJ_ID, MetaTag, VirtualURL) VALUES(@V0, @V1, @V2)";

            SqlConnection con = null;
            try
            {
                XmlDocument xmlDoc = GetObjectXml(id);

                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@V0", id);
                cmd.Parameters.AddWithValue("@V1", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "MetaTag", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V2", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "VirtualURL", string.Empty).SqlNULL());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (updateDataObjectXml)
                    UpdateDataObjectXml(id);
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }

        }

        private static void TransferPicture(Guid id)
        {
            const string SQL = "INSERT INTO hiobj_Picture (OBJ_ID, Width, Height, AspectRatio, SizeByte, [External], URLReferenced, TypeReferenced, Site, PhotoNotes) VALUES(@V0, @V1, @V2, @V3, @V4, @V5, @V6, @V7, @V8, @V9)";

            SqlConnection con = null;
            try
            {
                XmlDocument xmlDoc = GetObjectXml(id);

                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@V0", id);
                cmd.Parameters.AddWithValue("@V1", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Width", 0));
                cmd.Parameters.AddWithValue("@V2", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Height", 0));
                cmd.Parameters.AddWithValue("@V3", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "AspectRatio", 1.0m));
                cmd.Parameters.AddWithValue("@V4", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "SizeByte", 0));
                cmd.Parameters.AddWithValue("@V5", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "External", false));
                cmd.Parameters.AddWithValue("@V6", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "URLReferenced", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V7", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "TypeReferenced", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V8", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Site", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V9", GetSpezialList(xmlDoc, "Note"));

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (updateDataObjectXml) 
                    UpdateDataObjectXml(id);
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }
        }

        private static void TransferPinboardOffer(Guid id)
        {
            const string SQL = "INSERT INTO hiobj_PinboardOffer (OBJ_ID, Price, Amount) VALUES(@V0, @V1, @V2)";

            SqlConnection con = null;
            try
            {
                XmlDocument xmlDoc = GetObjectXml(id);

                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@V0", id);
                cmd.Parameters.AddWithValue("@V1", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Price", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V2", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Amount", 0));

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (updateDataObjectXml)
                    UpdateDataObjectXml(id);
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }

        }

        private static void TransferPinboardSearch(Guid id)
        {
            const string SQL = "INSERT INTO hiobj_PinboardSearch (OBJ_ID, Price) VALUES(@V0, @V1)";

            SqlConnection con = null;
            try
            {
                XmlDocument xmlDoc = GetObjectXml(id);

                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@V0", id);
                cmd.Parameters.AddWithValue("@V1", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Price", string.Empty).SqlNULL());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (updateDataObjectXml)
                    UpdateDataObjectXml(id);
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }
        }

        private static void TransferSlideShow(Guid id)
        {
            const string SQL = "INSERT INTO hiobj_SlideShow (OBJ_ID, Effect) VALUES(@V0, @V1)";

            SqlConnection con = null;
            try
            {
                XmlDocument xmlDoc = GetObjectXml(id);

                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@V0", id);
                cmd.Parameters.AddWithValue("@V1", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Effect", string.Empty).SqlNULL());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (updateDataObjectXml)
                    UpdateDataObjectXml(id);
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }
        }

        private static void TransferTag(Guid id)
        {
            const string SQL = "INSERT INTO hiobj_Tag (OBJ_ID) VALUES(@V0)";

            SqlConnection con = null;
            try
            {
                //XmlDocument xmlDoc = GetObjectXml(id);

                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@V0", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (updateDataObjectXml)
                    UpdateDataObjectXml(id);
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }
        }

        private static void TransferProfileCommunity(Guid id)
        {
            const string SQL = "INSERT INTO hiobj_ProfileCommunity (OBJ_ID) VALUES(@V0)";

            SqlConnection con = null;
            try
            {
                //XmlDocument xmlDoc = GetObjectXml(id);

                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@V0", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (updateDataObjectXml)
                    UpdateDataObjectXml(id);
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }
        }


        private static void TransferUser(Guid id)
        {
            const string SQL = "INSERT INTO hiobj_User (OBJ_ID, EmphasisList, OpenID, PPID) VALUES(@V0, @V1, @V2, @V3)";

            SqlConnection con = null;
            try
            {
                XmlDocument xmlDoc = GetObjectXml(id);

                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@V0", id);
                cmd.Parameters.AddWithValue("@V1", GetSpezialList(xmlDoc, "Emphasis"));
                cmd.Parameters.AddWithValue("@V2", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "OpenID", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V3", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "PPID", string.Empty).SqlNULL());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (updateDataObjectXml)
                    UpdateDataObjectXml(id);
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }

        }

        private static void UpdateUserProfileData()
        {
            StringBuilder sql = new StringBuilder(1000);
            sql.AppendLine("UPDATE     hiobj_User");
            sql.AppendLine("SET              Vorname = hitbl_UserProfileData_UPD.UPD_Vorname, VornameShow = ISNULL(hitbl_UserProfileData_UPD.UPD_VornameShow, 0), ");
            sql.AppendLine("Name = hitbl_UserProfileData_UPD.UPD_Name, NameShow = ISNULL(hitbl_UserProfileData_UPD.UPD_NameShow, 0), Street = hitbl_UserProfileData_UPD.UPD_Street, ");
            sql.AppendLine("StreetShow = ISNULL(hitbl_UserProfileData_UPD.UPD_StreetShow, 0), Zip = hitbl_UserProfileData_UPD.UPD_Zip, ZipShow = ISNULL(hitbl_UserProfileData_UPD.UPD_ZipShow, 0), City = hitbl_UserProfileData_UPD.UPD_City, CityShow = ISNULL(hitbl_UserProfileData_UPD.UPD_CityShow, 0), ");
            sql.AppendLine("Land = hitbl_UserProfileData_UPD.UPD_Land, LandShow = ISNULL(hitbl_UserProfileData_UPD.UPD_LandShow, 0), Languages = hitbl_UserProfileData_UPD.UPD_Languages, LanguagesShow = ISNULL(hitbl_UserProfileData_UPD.UPD_LanguagesShow, 0), ");
            sql.AppendLine("Sex = hitbl_UserProfileData_UPD.UPD_Sex, SexShow = ISNULL(hitbl_UserProfileData_UPD.UPD_SexShow, 0), Birthday = hitbl_UserProfileData_UPD.UPD_Birthday, BirthdayShow = ISNULL(hitbl_UserProfileData_UPD.UPD_BirthdayShow, 0), Status = hitbl_UserProfileData_UPD.UPD_Status, ");
            sql.AppendLine("StatusShow = ISNULL(hitbl_UserProfileData_UPD.UPD_StatusShow, 0), AttractedTo = hitbl_UserProfileData_UPD.UPD_AttractedTo, AttractedToShow = ISNULL(hitbl_UserProfileData_UPD.UPD_AttractedToShow, 0), BodyHeight = hitbl_UserProfileData_UPD.UPD_BodyHeight, ");
            sql.AppendLine("BodyHeightShow = ISNULL(hitbl_UserProfileData_UPD.UPD_BodyHeightShow, 0), BodyWeight = hitbl_UserProfileData_UPD.UPD_BodyWeight, BodyWeightShow = ISNULL(hitbl_UserProfileData_UPD.UPD_BodyWeightShow, 0), ");
            sql.AppendLine("EyeColor = hitbl_UserProfileData_UPD.UPD_EyeColor, EyeColorShow = ISNULL(hitbl_UserProfileData_UPD.UPD_EyeColorShow, 0), HairColor = hitbl_UserProfileData_UPD.UPD_HairColor, HairColorShow = ISNULL(hitbl_UserProfileData_UPD.UPD_HairColorShow, 0), ");
            sql.AppendLine("PriColor = hitbl_UserProfileData_UPD.UPD_PriColor, PriColorShow = ISNULL(hitbl_UserProfileData_UPD.UPD_PriColorShow, 0), SecColor = hitbl_UserProfileData_UPD.UPD_SecColor, SecColorShow = ISNULL(hitbl_UserProfileData_UPD.UPD_SecColorShow, 0), ");
            sql.AppendLine("Mobile = hitbl_UserProfileData_UPD.UPD_Mobile, MobileShow = ISNULL(hitbl_UserProfileData_UPD.UPD_MobileShow, 0), Phone = hitbl_UserProfileData_UPD.UPD_Phone, PhoneShow = ISNULL(hitbl_UserProfileData_UPD.UPD_PhoneShow, 0), MSN = hitbl_UserProfileData_UPD.UPD_MSN, ");
            sql.AppendLine("MSNShow = ISNULL(hitbl_UserProfileData_UPD.UPD_MSNShow, 0), Yahoo = hitbl_UserProfileData_UPD.UPD_Yahoo, YahooShow = ISNULL(hitbl_UserProfileData_UPD.UPD_YahooShow, 0), Skype = hitbl_UserProfileData_UPD.UPD_Skype, ");
            sql.AppendLine("SkypeShow = ISNULL(hitbl_UserProfileData_UPD.UPD_SkypeShow, 0), ICQ = hitbl_UserProfileData_UPD.UPD_ICQ, ICQShow = ISNULL(hitbl_UserProfileData_UPD.UPD_ICQShow, 0), AIM = hitbl_UserProfileData_UPD.UPD_AIM, AIMShow = ISNULL(hitbl_UserProfileData_UPD.UPD_AIMShow, 0), ");
            sql.AppendLine("Homepage = hitbl_UserProfileData_UPD.UPD_Homepage, HomepageShow = ISNULL(hitbl_UserProfileData_UPD.UPD_HomepageShow, 0), Blog = hitbl_UserProfileData_UPD.UPD_Blog, BlogShow = ISNULL(hitbl_UserProfileData_UPD.UPD_BlogShow, 0), ");
            sql.AppendLine("Beruf = hitbl_UserProfileData_UPD.UPD_Beruf, BerufShow = ISNULL(hitbl_UserProfileData_UPD.UPD_BerufShow, 0), Lebensmoto = hitbl_UserProfileData_UPD.UPD_Lebensmoto, LebensmotoShow = ISNULL(hitbl_UserProfileData_UPD.UPD_LebensmotoShow, 0), ");
            sql.AppendLine("Talent1 = hitbl_UserProfileData_UPD.UPD_Talent1, Talent1Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent1Show, 0), Talent2 = hitbl_UserProfileData_UPD.UPD_Talent2, Talent2Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent2Show, 0), ");
            sql.AppendLine("Talent3 = hitbl_UserProfileData_UPD.UPD_Talent3, Talent3Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent3Show, 0), Talent4 = hitbl_UserProfileData_UPD.UPD_Talent4, Talent4Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent4Show, 0), ");
            sql.AppendLine("Talent5 = hitbl_UserProfileData_UPD.UPD_Talent5, Talent5Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent5Show, 0), Talent6 = hitbl_UserProfileData_UPD.UPD_Talent6, Talent6Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent6Show, 0), ");
            sql.AppendLine("Talent7 = hitbl_UserProfileData_UPD.UPD_Talent7, Talent7Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent7Show, 0), Talent8 = hitbl_UserProfileData_UPD.UPD_Talent8, Talent8Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent8Show, 0), ");
            sql.AppendLine("Talent9 = hitbl_UserProfileData_UPD.UPD_Talent9, Talent9Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent9Show, 0), Talent10 = hitbl_UserProfileData_UPD.UPD_Talent10, Talent10Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent10Show, 0),"); 
            sql.AppendLine("Talent11 = hitbl_UserProfileData_UPD.UPD_Talent11, Talent11Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent11Show, 0), Talent12 = hitbl_UserProfileData_UPD.UPD_Talent12, Talent12Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent12Show, 0), ");
            sql.AppendLine("Talent13 = hitbl_UserProfileData_UPD.UPD_Talent13, Talent13Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent13Show, 0), Talent14 = hitbl_UserProfileData_UPD.UPD_Talent14, Talent14Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent14Show, 0), ");
            sql.AppendLine("Talent15 = hitbl_UserProfileData_UPD.UPD_Talent15, Talent15Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent15Show, 0), Talent16 = hitbl_UserProfileData_UPD.UPD_Talent16, Talent16Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent16Show, 0), ");
            sql.AppendLine("Talent17 = hitbl_UserProfileData_UPD.UPD_Talent17, Talent17Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent17Show, 0), Talent18 = hitbl_UserProfileData_UPD.UPD_Talent18, Talent18Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent18Show, 0), ");
            sql.AppendLine("Talent19 = hitbl_UserProfileData_UPD.UPD_Talent19, Talent19Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent19Show, 0), Talent20 = hitbl_UserProfileData_UPD.UPD_Talent20, Talent20Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent20Show, 0), ");
            sql.AppendLine("Talent21 = hitbl_UserProfileData_UPD.UPD_Talent21, Talent21Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent21Show, 0), Talent22 = hitbl_UserProfileData_UPD.UPD_Talent22, Talent22Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent22Show, 0), ");
            sql.AppendLine("Talent23 = hitbl_UserProfileData_UPD.UPD_Talent23, Talent23Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent23Show, 0), Talent24 = hitbl_UserProfileData_UPD.UPD_Talent24, Talent24Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent24Show, 0), ");
            sql.AppendLine("Talent25 = hitbl_UserProfileData_UPD.UPD_Talent25, Talent25Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Talent25Show, 0), InterestTopic1 = hitbl_UserProfileData_UPD.UPD_InterestTopic1, ");
            sql.AppendLine("InterestTopic1Show = ISNULL(hitbl_UserProfileData_UPD.UPD_InterestTopic1Show, 0), InterestTopic3 = hitbl_UserProfileData_UPD.UPD_InterestTopic3, InterestTopic3Show = ISNULL(hitbl_UserProfileData_UPD.UPD_InterestTopic3Show, 0), ");
            sql.AppendLine("InterestTopic2 = hitbl_UserProfileData_UPD.UPD_InterestTopic2, InterestTopic2Show = ISNULL(hitbl_UserProfileData_UPD.UPD_InterestTopic2Show, 0), InterestTopic4 = hitbl_UserProfileData_UPD.UPD_InterestTopic4, ");
            sql.AppendLine("InterestTopic4Show = ISNULL(hitbl_UserProfileData_UPD.UPD_InterestTopic4Show, 0), InterestTopic5 = hitbl_UserProfileData_UPD.UPD_InterestTopic5, InterestTopic5Show = ISNULL(hitbl_UserProfileData_UPD.UPD_InterestTopic5Show, 0), ");
            sql.AppendLine("InterestTopic6 = hitbl_UserProfileData_UPD.UPD_InterestTopic6, InterestTopic6Show = ISNULL(hitbl_UserProfileData_UPD.UPD_InterestTopic6Show, 0), InterestTopic7 = hitbl_UserProfileData_UPD.UPD_InterestTopic7, ");
            sql.AppendLine("InterestTopic7Show = ISNULL(hitbl_UserProfileData_UPD.UPD_InterestTopic7Show, 0), InterestTopic8 = hitbl_UserProfileData_UPD.UPD_InterestTopic8, InterestTopic8Show = ISNULL(hitbl_UserProfileData_UPD.UPD_InterestTopic8Show, 0), ");
            sql.AppendLine("InterestTopic9 = hitbl_UserProfileData_UPD.UPD_InterestTopic9, InterestTopic9Show = ISNULL(hitbl_UserProfileData_UPD.UPD_InterestTopic9Show, 0), InterestTopic10 = hitbl_UserProfileData_UPD.UPD_InterestTopic10, ");
            sql.AppendLine("InterestTopic10Show = ISNULL(hitbl_UserProfileData_UPD.UPD_InterestTopic10Show, 0), Interesst1 = hitbl_UserProfileData_UPD.UPD_Interesst1, Interesst1Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Interesst1Show, 0), ");
            sql.AppendLine("Interesst2 = hitbl_UserProfileData_UPD.UPD_Interesst2, Interesst2Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Interesst2Show, 0), Interesst3 = hitbl_UserProfileData_UPD.UPD_Interesst3, Interesst3Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Interesst3Show, 0), ");
            sql.AppendLine("Interesst4 = hitbl_UserProfileData_UPD.UPD_Interesst4, Interesst4Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Interesst4Show, 0), Interesst5 = hitbl_UserProfileData_UPD.UPD_Interesst5, Interesst5Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Interesst5Show, 0), ");
            sql.AppendLine("Interesst6 = hitbl_UserProfileData_UPD.UPD_Interesst6, Interesst6Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Interesst6Show, 0), Interesst7 = hitbl_UserProfileData_UPD.UPD_Interesst7, Interesst7Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Interesst7Show, 0), ");
            sql.AppendLine("Interesst8 = hitbl_UserProfileData_UPD.UPD_Interesst8, Interesst8Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Interesst8Show, 0), Interesst9 = hitbl_UserProfileData_UPD.UPD_Interesst9, Interesst9Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Interesst9Show, 0), ");
            sql.AppendLine("Interesst10 = hitbl_UserProfileData_UPD.UPD_Interesst10, Interesst10Show = ISNULL(hitbl_UserProfileData_UPD.UPD_Interesst10Show, 0), MsgFrom = hitbl_UserProfileData_UPD.UPD_MsgFrom, MsgFromShow = ISNULL(hitbl_UserProfileData_UPD.UPD_MsgFromShow, 0), ");
            sql.AppendLine("GetMail = hitbl_UserProfileData_UPD.UPD_GetMail, GetMailShow = ISNULL(hitbl_UserProfileData_UPD.UPD_GetMailShow, 0), DisplayAds = hitbl_UserProfileData_UPD.UPD_DisplayAds, DisplayAdsShow = ISNULL(hitbl_UserProfileData_UPD.UPD_DisplayAdsShow, 0)");
            sql.AppendLine("FROM         hiobj_User INNER JOIN");
            sql.AppendLine("hitbl_UserProfileData_UPD ON hiobj_User.OBJ_ID = hitbl_UserProfileData_UPD.USR_ID");

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(sql.ToString(), con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }

            UpdateUserProfileDataNullShow();
        }

        private static void UpdateUserProfileDataNullShow()
        {
            StringBuilder fields = new StringBuilder();
            fields.Append("VornameShow, NameShow, StreetShow, ZipShow, CityShow, LandShow,"); 
            fields.Append("LanguagesShow, SexShow, BirthdayShow, StatusShow, AttractedToShow, BodyHeightShow, "); 
            fields.Append("BodyWeightShow, EyeColorShow, HairColorShow, PriColorShow, SecColorShow, MobileShow, ");
            fields.Append("PhoneShow, MSNShow, YahooShow, SkypeShow, ICQShow, AIMShow, HomepageShow, BlogShow, "); 
            fields.Append("BerufShow, LebensmotoShow, Talent1Show, Talent2Show, Talent3Show, Talent4Show, "); 
            fields.Append("Talent5Show, Talent6Show, Talent7Show, Talent8Show, Talent9Show, Talent10Show, "); 
            fields.Append("Talent11Show, Talent12Show, Talent13Show, Talent14Show, Talent15Show, Talent16Show,");  
            fields.Append("Talent17Show, Talent18Show, Talent19Show, Talent20Show, Talent21Show, Talent22Show, "); 
            fields.Append("Talent23Show, Talent24Show, Talent25Show, InterestTopic1Show, InterestTopic3Show, "); 
            fields.Append("InterestTopic2Show, InterestTopic4Show, InterestTopic5Show, InterestTopic6Show, InterestTopic7Show, "); 
            fields.Append("InterestTopic8Show, InterestTopic9Show, InterestTopic10Show, Interesst1Show, Interesst2Show, "); 
            fields.Append("Interesst3Show, Interesst4Show, Interesst5Show, Interesst6Show, Interesst7Show, "); 
            fields.Append("Interesst8Show, Interesst9Show, Interesst10Show, MsgFromShow, GetMailShow, ");
            fields.Append("DisplayAdsShow ");

            foreach (string field in fields.ToString().Split(','))
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("UPDATE hiobj_User SET {0} = 0 WHERE {0} IS NULL", field.Trim());

                SqlConnection con = null;
                try
                {
                    con = new SqlConnection(connString);
                    SqlCommand cmd = new SqlCommand(sql.ToString(), con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch
                {
                    if (con != null && con.State == System.Data.ConnectionState.Open)
                        con.Close();

                    throw;
                }
            }
        }

        private static void TransferVideo(Guid id)
        {
            const string SQL = "INSERT INTO hiobj_Video (OBJ_ID, OriginalFormat, Width, Height, SizeByte, DurationSecond, Location, OriginalLocation, ConvertMessage, [External], URLReferenced, AspectRatio, TypeReferenced, Site) VALUES(@V0, @V1, @V2, @V3, @V4, @V5, @V6, @V7, @V8, @V9, @V10, @V11, @V12, @V13)";

            SqlConnection con = null;
            try
            {
                XmlDocument xmlDoc = GetObjectXml(id);

                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@V0", id);
                cmd.Parameters.AddWithValue("@V1", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "OriginalFormat", 0));
                cmd.Parameters.AddWithValue("@V2", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Width", 0));
                cmd.Parameters.AddWithValue("@V3", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Height", 0));
                cmd.Parameters.AddWithValue("@V4", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "SizeByte", 0));
                cmd.Parameters.AddWithValue("@V5", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "DurationSecond", 0));
                cmd.Parameters.AddWithValue("@V6", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Location", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V7", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "OriginalLocation", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V8", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "ConvertMessage", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V9", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "External", false));
                cmd.Parameters.AddWithValue("@V10", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "URLReferenced", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V11", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "AspectRatio", 1.0m));
                cmd.Parameters.AddWithValue("@V12", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "TypeReferenced", string.Empty).SqlNULL());
                cmd.Parameters.AddWithValue("@V13", XmlHelper.GetElementValue(xmlDoc.DocumentElement, "Site", string.Empty).SqlNULL());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (updateDataObjectXml)
                    UpdateDataObjectXml(id);
            }
            catch
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                throw;
            }
        }

        private static void RemoveNotPictureVersion(XmlDocument xmlDoc)
        {
            XmlNodeList list = xmlDoc.DocumentElement.ChildNodes;
            for (int x = list.Count - 1; x >= 0; x--)
            {
                XmlNode node = list[x];
                bool del = true;

                if (node.Name.StartsWith("Picture"))
                {
                    foreach (string name in Enum.GetNames(typeof(PictureVersion)))
                    {
                        if (node.Name == string.Format("Picture{0}", name))
                        {
                            del = false;
                            break;
                        }
                    }
                }
                if (del)
                    xmlDoc.DocumentElement.RemoveChild(node);
                else
                    continue;
            }

        }

        private static string GetSpezialList(XmlDocument xmlOrg, string name)
        {
            XmlDocument xmlDoc = CloneXml(xmlOrg);
            XmlNodeList list = xmlDoc.DocumentElement.ChildNodes;
            for (int x = list.Count - 1; x >= 0; x--)
            {
                XmlNode node = list[x];
                if (node.Name != name)
                    xmlDoc.DocumentElement.RemoveChild(node);
                else
                    continue;
            }
            return xmlDoc.DocumentElement.OuterXml;   
        }


        private static XmlDocument CloneXml(XmlDocument xmlDoc)
        {
            XmlDocument xmlClone = new XmlDocument();
            xmlClone.LoadXml(xmlDoc.OuterXml);
            return xmlClone; 
        }

        private static void UpdateDataObjectXml(Guid id)
        {
            XmlDocument xmlOrg = GetObjectXml(id);
            RemoveNotPictureVersion(xmlOrg);

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand("UPDATE hitbl_DataObject_OBJ SET OBJ_SpezialXml = @V1 WHERE OBJ_ID = @V0", con);
                cmd.Parameters.AddWithValue("@V0", id);
                cmd.Parameters.AddWithValue("@V1", xmlOrg.DocumentElement.OuterXml);   
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();

                throw;
            }
        }

        private static XmlDocument GetObjectXml(Guid id)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.AppendChild(xmlDoc.CreateElement("Root"));

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(string.Format("SELECT OBJ_SpezialXml FROM hitbl_DataObject_OBJ WHERE OBJ_ID = '{0}'", id), con);
                con.Open();
                string ret = cmd.ExecuteScalar().ToString();
                if (!string.IsNullOrEmpty(ret))
                    xmlDoc.LoadXml(ret);
  
                con.Close();
            }
            catch
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();

                throw;

            }
            return xmlDoc; 
        }

        private static int CheckTable(string name)
        { 

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(string.Format("SELECT COUNT(*) FROM {0}", name), con);
                con.Open();
                string ret = cmd.ExecuteScalar().ToString();
                con.Close();
                return int.Parse(ret); ;
            }
            catch
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
 
                return -1;
            }
        }
    }
}
