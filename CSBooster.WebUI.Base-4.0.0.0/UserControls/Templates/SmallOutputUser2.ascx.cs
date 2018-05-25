// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Text;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class SmallOutputUser2 : System.Web.UI.UserControl, ISmallOutputUser
    {
        private string username;
        private string primaryColor;
        private string secondaryColor;
        private string userPictureURL;
        private string userDetailURL;
        private bool hasElementName;

        public DataObjectUser DataObjectUser { get; set; }
        public bool LinkActive { get; set; }

        public string UserName
        {
            get { return username; }
            set { username = value; }
        }

        public string PrimaryColor
        {
            get { return primaryColor; }
            set { primaryColor = value; }
        }

        public string SecondaryColor
        {
            get { return secondaryColor; }
            set { secondaryColor = value; }
        }

        public string UserPictureURL
        {
            get { return userPictureURL; }
            set { userPictureURL = value; }
        }

        public string UserDetailURL
        {
            get { return userDetailURL; }
            set { userDetailURL = value; }
        }

        public bool HasElementName
        {
            get { return hasElementName; }
            set { hasElementName = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.LIT.Text = this.GetHtml();
        }

        public string GetHtml()
        {
            if (DataObjectUser != null)
            {
                UserName = DataObjectUser.Nickname;
                if (!string.IsNullOrEmpty(DataObjectUser.FacebookUserId))
                    UserPictureURL = string.Format("http://graph.facebook.com/{0}/picture?type=square", DataObjectUser.FacebookUserId);
                else
                    UserPictureURL = SiteConfig.MediaDomainName + DataObjectUser.GetImage(PictureVersion.XS);
                PrimaryColor = DataObjectUser.PrimaryColor;
                SecondaryColor = DataObjectUser.SecondaryColor;
                if (LinkActive)
                    UserDetailURL = Helper.GetDetailLink(DataObjectUser.ObjectType, DataObjectUser.Nickname);
            }

            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(secondaryColor) && !string.IsNullOrEmpty(primaryColor))
            {
                sb.AppendFormat("<div class=\"userOutput\">");
                sb.AppendFormat("  <div class=\"userOutputColor1\">");
                if (!string.IsNullOrEmpty(userDetailURL))
                    sb.AppendFormat("<a href=\"{0}\">", userDetailURL);
                if (hasElementName)
                    sb.AppendFormat("    <img name=\"sco\" src=\"{0}{1}.png\" />", Constants.SECONDARY_COLOR_ICONS_PATH, secondaryColor);
                else
                    sb.AppendFormat("    <img src=\"{0}{1}.png\" />", Constants.SECONDARY_COLOR_ICONS_PATH, secondaryColor);
                if (!string.IsNullOrEmpty(userDetailURL))
                    sb.AppendFormat("</a>");
                sb.AppendFormat("  </div>");
                sb.AppendFormat("  <div class=\"userOutputImage\">");
                if (!string.IsNullOrEmpty(userDetailURL))
                    sb.AppendFormat("<a href=\"{0}\">", userDetailURL);
                sb.AppendFormat("    <img src=\"{0}\" />", userPictureURL);
                if (!string.IsNullOrEmpty(userDetailURL))
                    sb.AppendFormat("</a>");
                sb.AppendFormat("  </div>");
                sb.AppendFormat("  <div class=\"userOutputColor2\">");
                if (!string.IsNullOrEmpty(userDetailURL))
                    sb.AppendFormat("<a href=\"{0}\">", userDetailURL);
                if (hasElementName)
                    sb.AppendFormat("    <img name=\"pco\" src=\"{0}{1}.png\" />", Constants.PRIMARY_COLOR_ICONS_PATH, primaryColor);
                else
                    sb.AppendFormat("    <img src=\"{0}{1}.png\" />", Constants.PRIMARY_COLOR_ICONS_PATH, primaryColor);
                if (!string.IsNullOrEmpty(userDetailURL))
                    sb.AppendFormat("</a>");
                sb.AppendFormat("  </div>");
                sb.AppendFormat("</div>");
            }

            if (!string.IsNullOrEmpty(userDetailURL) && !string.IsNullOrEmpty(username))
            {
                sb.AppendFormat("<div class=\"userOutputName\"><a href=\"{0}\">{1}</a></div>", userDetailURL, username.CropString(16));
            }
            else if (!string.IsNullOrEmpty(username))
            {
                sb.AppendFormat("<div class=\"userOutputName\">{0}</div>", username.CropString(16));
            }

            return sb.ToString();
        }
    }
}