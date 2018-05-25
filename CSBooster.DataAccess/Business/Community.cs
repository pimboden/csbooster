//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		26.03.2007 / PI
//  Updated:   #1.1.0.0    15.01.2008 /PI
//                         Deletetd som obsolete code   
//******************************************************************************

using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Xml;
using System.Linq;
using SubSonic;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public class Community
    {
        private bool isUserOwner;

        public Guid CommunityId { get; set; }
        public Guid UserId { get; set; }
        public DataObject ProfileOrCommunity { get; set; }
        public bool IsUserMember { get; set; }
        public bool IsUserCreator { get; set; }
        public string HeaderStyle { get; set; }
        public string BodyStyle { get; set; }
        public string FooterStyle { get; set; }
        public bool LoadSucces { get; set; }

        public bool IsUserOwner
        {
            get { return isUserOwner; }
            set { isUserOwner = value; }
        }

        private void InitProps()
        {
            LoadSucces = false;
            CommunityId = Guid.Empty;
            UserId = Guid.Empty;
            IsUserMember = false;
            isUserOwner = false;
            IsUserCreator = false;
            HeaderStyle = string.Empty;
            BodyStyle = string.Empty;
            FooterStyle = string.Empty;
            ProfileOrCommunity = null;
        }

        public Community(Guid communityId)
        {
            InitProps();
            LoadProperties(communityId);
        }

        /// <summary>
        /// It may also be used with GUID... Usefull when the CN Querystring Parameter is beein used.
        /// </summary>
        /// <param name="communityName"></param>
        public Community(string communityName)
        {
            InitProps();
            if (!communityName.IsGuid())
            {
                LoadProperties(communityName);
            }
            else
            {
                LoadProperties(new Guid(communityName));
            }
        }

        private void LoadProperties(string communityName)
        {
            string strStyleInfo = string.Empty;
            IDataReader idr = null;
            try
            {
                if (!string.IsNullOrEmpty(communityName))
                {
                    idr = HitblCommunityCty.FetchByParameter(HitblCommunityCty.Columns.CtyVirtualUrl, SubSonic.Comparison.Equals, communityName);
                    FillProperties(idr);
                }
            }
            catch
            {
            }
            finally
            {
                if (idr != null && !idr.IsClosed)
                {
                    idr.Close();
                }
            }
        }

        private void LoadProperties(Guid communityId)
        {
            string strStyleInfo = string.Empty;
            IDataReader idr = null;
            try
            {
                if (!communityId.Equals(Guid.Empty))
                {
                    idr = HitblCommunityCty.FetchByParameter(HitblCommunityCty.Columns.CtyId, SubSonic.Comparison.Equals, communityId);
                    FillProperties(idr);
                }
            }
            catch
            {
            }
            finally
            {
                if (idr != null && !idr.IsClosed)
                {
                    idr.Close();
                }
            }
        }

        private void FillProperties(IDataReader idr)
        {
            if (idr.Read())
            {
                if (!string.IsNullOrEmpty(idr[HitblCommunityCty.Columns.CtyBodyStyle].ToString()))
                {
                    BodyStyle = idr[HitblCommunityCty.Columns.CtyBodyStyle].ToString();
                }
                if (!string.IsNullOrEmpty(idr[HitblCommunityCty.Columns.CtyFooterStyle].ToString()))
                {
                    FooterStyle = idr[HitblCommunityCty.Columns.CtyFooterStyle].ToString();
                }
                if (!string.IsNullOrEmpty(idr[HitblCommunityCty.Columns.CtyHeaderStyle].ToString()))
                {
                    HeaderStyle = idr[HitblCommunityCty.Columns.CtyHeaderStyle].ToString();
                }

                UserDataContext udc = UserDataContext.GetUserDataContext();
                CommunityId = new Guid(idr[HitblCommunityCty.Columns.CtyId].ToString());
                UserId = udc.UserID;

                bool isProfile = (bool)idr[HitblCommunityCty.Columns.CtyIsProfile];
                if (isProfile)
                    ProfileOrCommunity = DataObject.Load<DataObjectProfileCommunity>(CommunityId, null, false);
                else
                    ProfileOrCommunity = DataObject.Load<DataObjectCommunity>(CommunityId, null, false);

                if (udc.CurrentContext.Request.IsAuthenticated)
                {
                    IsUserMember = Community.GetIsUserMember(UserId, CommunityId, out isUserOwner);
                    IsUserCreator = UserId.Equals(ProfileOrCommunity.UserID.Value);
                }
                else
                {
                    IsUserCreator = false;
                    IsUserMember = false;
                    isUserOwner = false;
                }
                LoadSucces = true;
            }
        }

        public static Guid InsertNewWidget(Guid pageId, Guid widgetId, string langCode)
        {
            try
            {
                WidgetElement widget = WidgetSection.CachedInstance.Widgets.Cast<WidgetElement>().Where(w => w.Id == widgetId).Single();

                HitblWidgetInstanceIn newWidgetIns = new HitblWidgetInstanceIn();
                newWidgetIns.InsId = Guid.NewGuid();
                newWidgetIns.InsColumnNo = 0;
                newWidgetIns.InsCreatedDate = newWidgetIns.InsLastUpdate = DateTime.Now;
                newWidgetIns.InsExpanded = true;
                newWidgetIns.InsOrderNo = 0;
                newWidgetIns.InsPagId = pageId;
                newWidgetIns.InsXmlStateData = widget.Settings.Value;
                newWidgetIns.WdgId = widgetId;
                newWidgetIns.Save();

                HitblWidgetInstanceTextWit newWidgetInsTxt = new HitblWidgetInstanceTextWit();
                newWidgetInsTxt.InsId = newWidgetIns.InsId;
                newWidgetInsTxt.WitLangCode = langCode;
                newWidgetInsTxt.WitTitle = GuiLanguage.GetGuiLanguage("WidgetsBase").GetString(widget.TitleKey);
                newWidgetInsTxt.Save();

                SPs.HispMoveWidgetInstance(newWidgetIns.InsId, 0, 0).Execute();

                return newWidgetIns.InsId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CreateDefaultWidgets(Guid pageID, string pageType, string page, string langCode)
        {
            try
            {
                XmlDocument xmlDefaultWidgets = new XmlDocument();
                StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath(string.Format("{0}/Configurations/DefaultWidgets.config", SiteConfig.SiteVRoot)));
                xmlDefaultWidgets.LoadXml(sr.ReadToEnd());
                sr.Close();

                XmlNodeList xmlLayoutCols = xmlDefaultWidgets.SelectNodes(string.Format("//root/Community[@pageType='{0}' and @page='{1}']/Column", pageType, page));

                foreach (XmlNode xmlLayoutCol in xmlLayoutCols)
                {
                    try
                    {
                        int intColNr = Convert.ToInt32(xmlLayoutCol.Attributes["number"].Value);
                        XmlNodeList xmlWidgtes = xmlLayoutCol.SelectNodes("widget");
                        int intOrderNr = 0;
                        foreach (XmlNode xmlWidget in xmlWidgtes)
                        {
                            try
                            {
                                bool isFixed = false;
                                bool hideIfNoContent = false;
                                string roleVisibility = string.Empty;
                                string templateID = string.Empty;
                                string title = string.Empty;
                                string outputTemplate = string.Empty;
                                if (xmlWidget.Attributes["IsFixed"] != null)
                                {
                                    bool.TryParse(xmlWidget.Attributes["IsFixed"].Value, out isFixed);
                                }
                                if (xmlWidget.Attributes["HideIfNoContent"] != null)
                                {
                                    bool.TryParse(xmlWidget.Attributes["HideIfNoContent"].Value, out hideIfNoContent);
                                }
                                if (xmlWidget.Attributes["ViewRoles"] != null)
                                {
                                    roleVisibility = xmlWidget.Attributes["ViewRoles"].Value.Replace(',', Constants.TAG_DELIMITER);
                                }
                                if (xmlWidget.Attributes["WTP_ID"] != null)
                                {
                                    templateID = xmlWidget.Attributes["WTP_ID"].Value;
                                }
                                if (xmlWidget.Attributes["OutputTemplate"] != null)
                                {
                                    outputTemplate = xmlWidget.Attributes["OutputTemplate"].Value;
                                }

                                if (xmlWidget.Attributes["Title"] != null)
                                {
                                    title = xmlWidget.Attributes["Title"].Value;
                                }
                                Guid widgetId = new Guid(xmlWidget.Attributes["id"].Value);
                                string predefinedContent = string.Empty;
                                if (xmlWidget.HasChildNodes)
                                {
                                    predefinedContent = xmlWidget.InnerText;
                                }
                                WidgetElement widget = WidgetSection.CachedInstance.Widgets.Cast<WidgetElement>().Where(w => w.Id == widgetId).Single();
                                HitblWidgetInstanceIn newWidgetIns = new HitblWidgetInstanceIn();
                                newWidgetIns.InsId = Guid.NewGuid();
                                newWidgetIns.InsColumnNo = intColNr;
                                newWidgetIns.InsCreatedDate = newWidgetIns.InsLastUpdate = DateTime.Now;
                                newWidgetIns.InsExpanded = true;
                                newWidgetIns.InsOrderNo = intOrderNr;
                                newWidgetIns.InsPagId = pageID;
                                newWidgetIns.InsIsFixed = isFixed;
                                newWidgetIns.InsHideIfNoContent = hideIfNoContent;
                                newWidgetIns.InsViewRoles = roleVisibility;
                                if (templateID.Length > 0)
                                    newWidgetIns.WtpId = templateID.ToGuid();

                                if (outputTemplate.Length > 0)
                                    newWidgetIns.InsOutputTemplate = outputTemplate.ToGuid();

                                newWidgetIns.InsXmlStateData = predefinedContent.Length == 0 ? widget.Settings.Value : predefinedContent;
                                newWidgetIns.WdgId = widgetId;
                                newWidgetIns.Save();
                                HitblWidgetInstanceTextWit newWidgetInsTxt = new HitblWidgetInstanceTextWit();
                                newWidgetInsTxt.InsId = newWidgetIns.InsId;
                                newWidgetInsTxt.WitLangCode = langCode;

                                if (title.Length == 0)
                                {
                                    newWidgetInsTxt.WitTitle = GuiLanguage.GetGuiLanguage("WidgetsBase").GetString(widget.TitleKey);
                                }
                                else
                                {
                                    newWidgetInsTxt.WitTitle = title;
                                }
                                newWidgetInsTxt.Save();
                                if (templateID.Length > 0)
                                {
                                    SPs.HispWidgetTemplatesIncreaseCount(pageID, newWidgetIns.WtpId).Execute();
                                }
                                intOrderNr++;

                            }
                            catch
                            { }
                        }
                    }
                    catch
                    { }
                }
            }
            catch
            { }
        }

        public static string GetCategoryTagFromCheckboxList(CheckBoxList cblSubCategories)
        {
            string strTags = string.Empty;

            foreach (ListItem li in cblSubCategories.Items)
            {
                if (li.Selected)
                {
                    HitblMainMan objSubMain = HitblMainMan.FetchByID(Convert.ToInt32(li.Value));
                    strTags += objSubMain.ManTitle + Constants.TAG_DELIMITER;
                }
            }
            return strTags.TrimEnd(' ');
        }


        public static bool GetIsUserMember(Guid userId, Guid communityId, out bool isUserOwner)
        {
            bool isUserMember = false;
            isUserOwner = false;
            IDataReader idr = null;
            try
            {
                MembershipUser membUsr = Membership.GetUser(userId, false);
                if (membUsr != null)
                {
                    isUserMember = Roles.IsUserInRole(membUsr.UserName, "Admin");
                    isUserOwner = isUserMember;
                }
                if (!isUserMember)
                {
                    idr = SPs.HispCommunityIsUserMember(communityId, userId).GetReader();
                    if (idr.Read())
                    {
                        isUserMember = true;
                        if (idr["CUR_IsOwner"] != DBNull.Value && Convert.ToBoolean(idr["CUR_IsOwner"]))
                        {
                            isUserOwner = true;
                        }
                    }
                }
            }
            finally
            {
                if (idr != null && !idr.IsClosed)
                    idr.Close();
            }
            return isUserMember;
        }

        public static bool GetIsUserMember(Guid userId, Guid communityId)
        {
            bool isUserOwner = false;
            return GetIsUserMember(userId, communityId, out isUserOwner);
        }

        public static bool GetIsUserOwner(Guid userId, Guid communityId)
        {
            bool isUserOwner = false;
            GetIsUserMember(userId, communityId, out isUserOwner);
            return isUserOwner;
        }

        public static bool GetIsUserOwner(Guid userId, Guid communityId, out bool isUserMember)
        {
            bool isUserOwner = false;
            isUserMember = GetIsUserMember(userId, communityId, out isUserOwner);
            return isUserOwner;
        }

        public static Guid CreateUserProfileCommunity(UserProfile userProfile)
        {
            string layoutName = CustomizationSection.CachedInstance.DefaultLayouts.ProfileCommunity;
            Guid communityIdD = Guid.NewGuid();
            Guid currentPageId = Guid.NewGuid();
            string strUserKey = userProfile.UserId.ToString();

            DataObjectProfileCommunity dataObjectCommunity = new DataObjectProfileCommunity(UserDataContext.GetUserDataContext(userProfile.Nickname));
            dataObjectCommunity.Status = ObjectStatus.Public;
            dataObjectCommunity.Title = "Profile_" + strUserKey;
            dataObjectCommunity.CommunityID = communityIdD;
            dataObjectCommunity.Insert();

            HitblCommunityCty community = new HitblCommunityCty();
            community.CtyInsertedDate = DateTime.Now;
            community.CtyUpdatedDate = DateTime.Now;
            community.CtyVirtualUrl = "Profile_" + strUserKey;
            community.UsrIdInserted = userProfile.UserId;
            community.UsrIdUpdated = userProfile.UserId;
            community.CtyIsProfile = true;
            community.CtyStatus = (int)CommunityStatus.Initializing;
            community.CtyLayout = layoutName;
            community.CtyTheme = Constants.DEFAULT_THEME;
            community.CtyId = communityIdD;
            community.Save();

            HirelCommunityUserCur.Insert(communityIdD, userProfile.UserId, true, 0, DateTime.Now, Guid.Empty);
            HirelCommunityUserCur.Insert(community.CtyId, new Guid(Constants.ADMIN_USERID), true, 0, DateTime.Now, Guid.Empty);

            userProfile.ProfileCommunityID = communityIdD;
            userProfile.Save();
            currentPageId = PagesConfig.CreateNewPage(communityIdD, "Profile", "Private", "Dashboard").PagId;
            currentPageId = PagesConfig.CreateNewPage(communityIdD, "Profile", "Start", "Home").PagId;
            community.CtyStatus = (int)CommunityStatus.Ready;
            community.Save();

            FriendHandler.TransferFriendAsCommunityMember(communityIdD, userProfile.UserId);

            return communityIdD;
        }

        public static Guid GetIDByVirtualUrl(string virtualUrl)
        {
            Guid communityID = Guid.Empty;
            IDataReader idr = null;
            try
            {
                idr = HitblCommunityCty.FetchByParameter(HitblCommunityCty.Columns.CtyVirtualUrl, SubSonic.Comparison.Like, virtualUrl);
                if (idr.Read())
                {
                    communityID = new Guid(idr[HitblCommunityCty.Columns.CtyId].ToString());
                }
            }
            finally
            {
                if (idr != null && !idr.IsClosed)
                {
                    idr.Close();
                }
            }
            return communityID;
        }
    }
}