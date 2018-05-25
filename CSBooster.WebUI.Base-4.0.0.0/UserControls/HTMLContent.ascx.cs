using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Data;
using _4screen.Utils.Web;
using System.Web;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class HTMLContent : System.Web.UI.UserControl
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        private string allowedEditRoles = "Admin";
        private UserDataContext udc = null;
        private string contentId = string.Empty;
        private Guid communityId = Constants.DEFAULT_COMMUNITY_ID.ToGuid();

        public string AllowedEditRoles
        {
            set
            {

                if (AreRolesValid(value)) { allowedEditRoles = value; }
                else { throw new SiemeArgumentException("HTMLContent", "AllowedEditRoles", "AllowedEditRoles", "The value contains invalid system roles"); }
            }
            get
            {
                if (string.IsNullOrEmpty(allowedEditRoles)) { allowedEditRoles = "Admin"; }
                return allowedEditRoles;
            }
        }

        public string ContentId
        {
            get
            {
                if (string.IsNullOrEmpty(contentId)) { contentId = this.ID; }
                return contentId;
            }
            set
            {
                contentId = value;
            }
        }

        public Guid CommunityId
        {
            get { return communityId; }
            set { communityId = value; }
        }

        public bool IsEmbedded { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsEmbedded && PageInfo.CommunityId.HasValue)
                communityId = PageInfo.CommunityId.Value;

            udc = UserDataContext.GetUserDataContext();
            ShowNormalMode();
            litOutput.ID = null;

            if (string.IsNullOrEmpty(txtLN.Value))
                txtLN.Value = CultureHandler.GetCurrentSpecificLanguageCode(); //Here shoudd go the current User's Language

            //Print the languagebar here for the events to to fire
            if (_4screen.Utils.Web.SiteConfig.Languages.Count > 0)
            {
                PrintLanguageBar();
            }
        }

        protected void lbtn_Command(object sender, CommandEventArgs e)
        {
            txtLN.Value = e.CommandArgument.ToString();
            ShowEditMode();
        }

        protected void OnEditClick(object sender, EventArgs e)
        {
            ShowEditMode();
        }

        protected void OnSaveClick(object sender, EventArgs e)
        {
            string cacheKey = string.Format("{0}_{1}_{2}", communityId, ContentId, txtLN.Value);
            string stringContent = REd.Content;
            CSBooster_DataContext csb = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            csb.hisp_Content_SaveContent(communityId, ContentId, txtLN.Value, stringContent);
            try
            {
                Cache.Remove(cacheKey);
                Cache.Insert(cacheKey, stringContent);
            }
            catch
            {
            }
            ShowNormalMode();
        }

        protected void OnCancelClick(object sender, EventArgs e)
        {
            ShowNormalMode();
        }

        private static bool AreRolesValid(string value)
        {
            bool areRolesValid = true;
            if (string.IsNullOrEmpty(value))
            {
                areRolesValid = false;
            }
            else
            {
                string[] systemRoles = Roles.GetAllRoles();
                string[] givenRoles = value.Split(',');
                foreach (string givenRole in givenRoles)
                {

                    areRolesValid &= systemRoles.Contains(givenRole, new CaseInsensitiveComparer());
                    if (!areRolesValid)
                        break;
                }
            }
            return areRolesValid;
        }

        private bool IsUserEditor()
        {
            return AllowedEditRoles.Split(',').Contains(udc.UserRole, new CaseInsensitiveComparer());
        }

        private string GetContentValue(bool ignoreCache, string LangCode)
        {
            string cacheKey = string.Format("{0}_{1}_{2}", communityId, ContentId, LangCode);
            string stringContent = Cache[cacheKey] as string;
            if (stringContent == null || ignoreCache)
            {
                stringContent = string.Empty;
                CSBooster_DataContext csb = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
                var content = csb.hisp_Content_GetContent(communityId, ContentId, LangCode).SingleOrDefault();
                if (content != null)
                {
                    stringContent = content.Content;
                    Cache.Insert(cacheKey, stringContent);
                }
            }
            return stringContent;
        }

        private void PrintLanguageBar()
        {
            phL.Controls.Clear();
            REd.Languages.Clear();

            phL.Controls.Add(new LiteralControl("<ul>"));
            foreach (var langInfo in _4screen.Utils.Web.SiteConfig.Languages)
            {
                REd.Languages.Add(langInfo.Value, langInfo.Key);

                string strActive = langInfo.Key.ToLower() == txtLN.Value.ToLower() ? "contentEditorTabActive" : "contentEditorTabInactive";
                phL.Controls.Add(new LiteralControl(string.Format("<li class=\"{0}\">", strActive)));
                LinkButton lbtn = new LinkButton();
                lbtn.ID = string.Format("lbtn{0}", langInfo.Key);
                lbtn.Text = langInfo.Key;
                lbtn.CommandArgument = langInfo.Key;
                lbtn.Command += new CommandEventHandler(lbtn_Command);
                phL.Controls.Add(lbtn);
                phL.Controls.Add(new LiteralControl("</li>"));
            }
            phL.Controls.Add(new LiteralControl("</ul>"));

        }

        private void ShowEditMode()
        {
            lbtnE.Visible = false;
            litOutput.Visible = false;
            litOutput.ID = null;

            //Print the languagebar here for the events to fire
            if (_4screen.Utils.Web.SiteConfig.Languages.Count > 0)
                PrintLanguageBar();

            string content = GetContentValue(true, txtLN.Value);

            REd.Content = content;
            REd.Height = new Unit(200, UnitType.Pixel);
            REd.Language = CultureHandler.GetCurrentSpecificLanguageCode();
            REd.AutoResizeHeight = true;

            pnlEditMode.Visible = true;
        }

        private void ShowNormalMode()
        {
            string content = string.Empty;
            lbtnE.Visible = false;
            pnlEditMode.Visible = false;

            if (IsUserEditor())
                lbtnE.Visible = true;

            content = GetContentValue(false, CultureHandler.GetCurrentSpecificLanguageCode());
            content = content.Replace("##DATE##", DateTime.Now.ToString("dddd, dd. MMMM yyyy"));
            if (udc.IsAuthenticated)
                content = content.Replace("##LinkProfilePage##", Helper.GetDetailLink("User", udc.Nickname));
            else
                content = content.Replace("##LinkProfilePage##", Constants.Links["LINK_TO_REGISTRATION_PAGE"].Url);
            Match match = Regex.Match(content, "##LinkCreate(.*?)##");
            if (match.Success)
            {
                if (udc.IsAuthenticated)
                {
                    string objectType = match.Groups[1].Value;
                    string createLink = string.Format("{0}&XCN={1}&OID={2}&ReturnUrl={3}", Helper.GetUploadWizardLink(Helper.GetObjectTypeNumericID(objectType), _4screen.CSB.Common.SiteConfig.UsePopupWindows), UserProfile.Current.ProfileCommunityID, Guid.NewGuid(), System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(Request.RawUrl)));
                    content = Regex.Replace(content, "##LinkCreate.*?##", createLink);
                }
                else
                {
                    content = Regex.Replace(content, "##LinkCreate.*?##", Constants.Links["LINK_TO_REGISTRATION_PAGE"].Url);
                }
            }


            if (content.IndexOf("##SendReport##") > -1)
            {
                if (udc.IsAuthenticated)
                {
                    string objectType = string.Empty;
                    if (!string.IsNullOrEmpty(Request.QueryString["OT"]))
                        objectType = Helper.GetObjectTypeNumericID(Request.QueryString["OT"]).ToString();

                    string href = string.Format("javascript:radWinOpen('/Pages/Popups/MessageSend.aspx?MsgType=rep&RecType=report&ObjType={0}&URL={1}', '{2}', 450, 450, true)", objectType, System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(Request.RawUrl)), languageShared.GetString("CommandContentReport").StripForScript());
                    content = content.Replace("##SendReport##", href);
                }
                else
                {
                    content = content.Replace("##SendReport##", Constants.Links["LINK_TO_LOGIN_PAGE"].Url);
                }
            }

            //invite
            if (content.IndexOf("##SendPropose##") > -1)
            {
                string objectType = string.Empty;
                if (!string.IsNullOrEmpty(Request.QueryString["OT"]))
                    objectType = Helper.GetObjectTypeNumericID(Request.QueryString["OT"]).ToString();

                string href = string.Format("javascript:radWinOpen('/Pages/Popups/MessageSend.aspx?MsgType=rec&URL={0}', '{1}', 450, 450, true)", System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Empty)), languageShared.GetString("CommandRecommend").StripForScript());
                content = content.Replace("##SendPropose##", href);
            }

            litOutput.Text = content;
            litOutput.Visible = true;
        }
    }
}
