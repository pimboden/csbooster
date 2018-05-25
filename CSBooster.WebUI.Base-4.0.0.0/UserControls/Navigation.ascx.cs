using System;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.DataAccess.Data;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using SiteConfig=_4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class Navigation : System.Web.UI.UserControl,IMinimalControl
    {
        #region Fields

        private string allowedEditRoles = "Admin";
        private UserDataContext udc = null;
        private string navigationId = string.Empty;
        private string outputASCXPath = string.Empty;
        private const int PAGESIZE = 10;
        private DataAccess.Business.Navigation.NavigationType _NavType = _4screen.CSB.DataAccess.Business.Navigation.NavigationType.TreeView;
        
        #endregion

        #region Properties

        public bool HasContent { get; set; }
        public string Prop1 { get; set; }
        public string Prop2 { get; set; }

        public string OutputASCXPath
        {
            get
            {
                return outputASCXPath;
            }
            set
            {
                outputASCXPath = value;
            }
        }

        public DataAccess.Business.Navigation.NavigationType NavType
        {
            get
            {
                return _NavType;
            }
            set
            {
                _NavType = value;
            }
        }

        public string AllowedEditRoles
        {
            set
            {

                if (AreRoles(value))
                {
                    allowedEditRoles = value;
                }
                else
                {
                    throw new SiemeArgumentException("Navigation", "AllowedEditRoles", "AllowedEditRoles", "The value contains invalid system roles");
                }
            }
            get
            {
                if (string.IsNullOrEmpty(allowedEditRoles))
                {
                    allowedEditRoles = "Admin";
                }
                return allowedEditRoles;
            }
        }

        public string NavigationID
        {
            get
            {
                return navigationId;
            }
            set
            {
                navigationId = value;
            }
        }

        #endregion

        private bool AreRoles(string value)
        {
            bool retVal = true;
            if (string.IsNullOrEmpty(value))
            {
                retVal = false;
            }
            else
            {
                string[] SystemRoles = Roles.GetAllRoles();
                string[] givenRoles = value.Split(',');
                foreach (string givenRole in givenRoles)
                {

                    retVal &= SystemRoles.Contains(givenRole, new CaseInsensitiveComparer());
                    if (!retVal)
                        break;
                }
            }
            return retVal;
        }

        private bool IsUserEditor()
        {
            return AllowedEditRoles.Split(',').Contains(udc.UserRole, new CaseInsensitiveComparer());
        }

        private string GetNavigationValue(bool ignoreCache)
        {
            CSBooster_DataContext csb = new CSBooster_DataContext(Helper.GetSiemeConnectionString());

            string CacheKey = string.Format("{0}_{1}_{2}_{3}", NavigationID, CultureHandler.GetCurrentNeutralLanguageCode().ToLower(), udc.UserRole.ToLower(),(int)NavType);
            string navXml = Cache[CacheKey] as string;
            if (navXml == null || ignoreCache)
            {
                navXml = "<Tree/>";
                var Navi = csb.hisp_Navigation_GetPreChache(NavigationID.ToGuid(), CultureHandler.GetCurrentNeutralLanguageCode(), udc.UserRole).FirstOrDefault();
                if (Navi != null)
                {
                    navXml = Navi.NPC_PreCacheXML;
                }
                navXml = DataAccess.Business.Navigation.TranformXML(navXml, NavType);
                Cache.Insert(CacheKey, navXml);

            }
            UserDataInfo udi = Cache["UID_" + udc.UserID] as UserDataInfo;
            if (udi == null || ignoreCache)
            {
                udi = new UserDataInfo(udc.UserID);
                Cache.Insert("UID_" + udc.UserID, udi, null, DateTime.Now.AddMinutes(5), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            navXml = navXml.Replace("##NEW_MAIL_COUNT##", udi.UnreadMessagesCount.ToString()).Replace("##FRIEND_REQUEST_COUNT##", udi.FriendRequestCount.ToString()).Replace("##ALERT_COUNT##", udi.AlertCount.ToString());
            navXml = navXml.Replace("##CURRENT_PAGE##", Server.UrlEncode(Request.Url.PathAndQuery));
            return navXml;
        }

        private void ShowNormalMode()
        {
            phNormalView.Visible = true;

            LoadOutputNavigation();

        }

        private void LoadOutputNavigation()
        {
            string NavigationXML = GetNavigationValue(false);
            Control ctrlOutput = this.LoadControl(OutputASCXPath);
                IMinimalControl iCtrlOutput = ctrlOutput as IMinimalControl;
            iCtrlOutput.Prop1 = NavigationXML;
             phNormalView.Controls.Add(ctrlOutput);
             HasContent = iCtrlOutput.HasContent;
        }


        protected override void OnInit(EventArgs e)
        {
            HasContent = true;
            lnkE.ToolTip = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("TooltipNavigationEdit");
            udc = UserDataContext.GetUserDataContext();
            //Print the languagebar here for the events to to fire
            ShowNormalMode();
            lnkE.Visible = IsUserEditor();
            lnkE.NavigateUrl = string.Format("/admin/NavigationsEdit.aspx?NavID={0}&Src={1}", NavigationID, Server.UrlEncode(Request.Url.PathAndQuery));
        }

 
    }
}