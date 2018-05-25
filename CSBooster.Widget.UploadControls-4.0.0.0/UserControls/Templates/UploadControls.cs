// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using SiteConfig = _4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.Widget.UserControls.Templates
{
    public partial class UploadControls : System.Web.UI.UserControl, ISettings
    {
        #region FIELDS

        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetUploadControls");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        public Dictionary<string, object> Settings { get; set; }

        private int objectType = 0;
        private string communityID = "";
        private UserDataContext udc;
        private Community community;
        private string uploadWhereMsg = string.Empty;

        #endregion FIELDS

        #region PROPERTIES

        public string UploadWhereMsg
        {
            get
            {
                return uploadWhereMsg;
            }
        }

        #endregion PROPERTIES


        protected void Page_Load(object sender, EventArgs e)
        {
            udc = UserDataContext.GetUserDataContext();

            if (Settings.ContainsKey("ParentObjectType") && !string.IsNullOrEmpty(Settings["ParentObjectType"].ToString()))
            {
                objectType = (int)Settings["ParentObjectType"];
            }

            if (objectType == Helper.GetObjectTypeNumericID("Community"))
            {
                if (Settings.ContainsKey("ParentCommunityID") && !string.IsNullOrEmpty(Settings["ParentCommunityID"].ToString()))
                {
                    communityID = Settings["ParentCommunityID"].ToString();
                    uploadWhereMsg = language.GetString("MsgUploadToCommunity");
                }
                else
                {
                    communityID = UserProfile.Current.ProfileCommunityID.ToString();
                    uploadWhereMsg = language.GetString("MsgUploadToProfil");
                }
            }
            else
            {
                communityID = UserProfile.Current.ProfileCommunityID.ToString();
                uploadWhereMsg = language.GetString("MsgUploadToProfil");
            }

            community = new Community(communityID);
        }

        protected string SetCreatePageLink()
        {
            StringBuilder sb = new StringBuilder(1000);
            if (udc.IsAuthenticated && udc.UserRole == "Admin")
            {
                string strUploadUrl = string.Format("{0}&OID={1}&ReturnUrl={2}", Helper.GetUploadWizardLink("Page", SiteConfig.UsePopupWindows), Guid.NewGuid().ToString(), System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(Request.RawUrl)));

                if (SiteConfig.UsePopupWindows)
                {
                    strUploadUrl = string.Format("javascript:radWinOpen('{0}', '{1}', 800, 500, false, null, 'wizardWin')", strUploadUrl, languageShared.GetString("LabelCreatePage").StripForScript());
                }

                sb.AppendFormat("<a href=\"{0}\" rel=\"nofollow\">{1}</a>", strUploadUrl, languageShared.GetString("LabelCreatePage"));
            }

            return sb.ToString();
        }

        protected string SetCreateGroupLink()
        {
            StringBuilder sb = new StringBuilder(1000);

            if (community != null && community.ProfileOrCommunity != null
               && community.ProfileOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("Community")
               && !community.ProfileOrCommunity.ParentObjectID.HasValue
               && ((((DataObjectCommunity)community.ProfileOrCommunity).CreateGroupUser == CommunityUsersType.Owners && community.IsUserOwner) ||
                    (((DataObjectCommunity)community.ProfileOrCommunity).CreateGroupUser == CommunityUsersType.Members && community.IsUserMember) ||
                     (udc.IsAuthenticated && udc.UserRole == "Admin")
                   )
               )
            {
                string initQuerySegment = string.Format("&XCN={0}&OID={1}", communityID, Guid.NewGuid().ToString());
                string returnUrlQuerySegment = "&ReturnUrl=" + System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(Request.RawUrl));

                string strUploadUrl = string.Format("{0}{1}{2}", Helper.GetUploadWizardLink("Community", SiteConfig.UsePopupWindows), initQuerySegment, returnUrlQuerySegment);

                if (SiteConfig.UsePopupWindows)
                {
                    strUploadUrl = string.Format("javascript:radWinOpen('{0}', '{1}', 800, 500, false, null, 'wizardWin')", strUploadUrl, languageShared.GetString("LabelCreateGroup").StripForScript());
                }

                sb.AppendFormat("<a href=\"{0}\" rel=\"nofollow\">{1}</a>", strUploadUrl, languageShared.GetString("LabelCreateGroup"));
            }

            return sb.ToString();
        }

        protected string SetUploadLinks()
        {
            StringBuilder sb = new StringBuilder(1000);
            List<SiteObjectType> lstOTs = Helper.GetObjectTypes();

            var uploadObjectTypes =
                from t in lstOTs.Where(x => x.NameCreateMenuKey.Length > 0 && Array.Exists(x.AllowedRoles.Split(','), y => UserDataContext.GetUserDataContext().UserRoles.Contains(y)))
                select new
                {
                    t.Id,
                    t.NumericId,
                    t.IsMultiUpload,
                    CreateMenuTitle = string.IsNullOrEmpty(t.LocalizationBaseFileName) ? GuiLanguage.GetGuiLanguage("SiteObjects").GetString(t.NameCreateMenuKey) : GuiLanguage.GetGuiLanguage(t.LocalizationBaseFileName).GetString(t.NameCreateMenuKey)
                };

            foreach (var type in uploadObjectTypes)
            {
                string initQuerySegment = type.NumericId != Helper.GetObjectTypeNumericID("Community") ? "&XCN=" + communityID : string.Empty;
                initQuerySegment += !type.IsMultiUpload ? "&OID=" + Guid.NewGuid().ToString() : string.Empty;
                string returnUrlQuerySegment = "&ReturnUrl=" + System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(Request.RawUrl));
                string strUploadUrl = string.Format("{0}{1}{2}", Helper.GetUploadWizardLink(type.NumericId, SiteConfig.UsePopupWindows), initQuerySegment, returnUrlQuerySegment);

                if (SiteConfig.UsePopupWindows)
                {
                    strUploadUrl = string.Format("javascript:radWinOpen('{0}', '{1}', 800, 500, true, null, 'wizardWin')", strUploadUrl, type.CreateMenuTitle);
                }

                sb.AppendFormat("<li class=\"{0}\"><a href=\"{1}\" rel=\"nofollow\">{2}</a></li>", type.Id.ToLower(), strUploadUrl, type.CreateMenuTitle);
            }

            return sb.ToString();
        }


    }   // END CLASS
}   // END NAMESPACE
