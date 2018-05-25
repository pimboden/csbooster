// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Collections.Generic;
using System.Text;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using SiteConfig=_4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.Widget.UserControls.Templates
{
    public partial class CommunityLists : System.Web.UI.UserControl, IDataObjectWorker
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetCommunityLists");
        protected string MediaDomainName = SiteConfig.MediaDomainName;

        public DataObject DataObject { get; set; }
        public string FolderParams { get; set; }
        public QuickParameters QuickParameters { get; set; }

        protected DataObjectCommunity DataObjectCommunity
        {
            get { return (DataObjectCommunity)DataObject; }
        }


        protected string CommunityPictureURL(PictureVersion size)
        {
            return MediaDomainName + DataObject.GetImage(size);
        }

        protected string CommunityDetailURL
        {
            get
            {
                return Helper.GetDetailLink(DataObject.ObjectType, DataObjectCommunity.VirtualURL, true);
            }
        }

        protected string UserDetailURL
        {
            get
            {
                return Helper.GetDetailLink(2, DataObjectCommunity.Nickname);
            }
        }

        protected string CommunityObjectInfo()
        {
            return CommunityObjectInfo(null);
        }

        protected string CommunityObjectInfo(string cssClass)
        {
            string cssAttr = string.Empty;
            if (!string.IsNullOrEmpty(cssClass))
                cssAttr = string.Format(" class=\"{0}\" ", cssClass);

            List<InfoObject> liInfo = InfoObjects.LoadForCommunity(UserDataContext.GetUserDataContext(), DataObject.CommunityID, null);
            StringBuilder sb = new StringBuilder(100 * liInfo.Count);
            foreach (DataAccess.Business.InfoObject item in liInfo)
            {
                if (Helper.IsObjectTypeEnabled(item.ObjectType))
                    sb.AppendFormat("<div{0}><a href=\"{1}&XCN={2}\">{3} ({4})</a></div>", cssAttr, Helper.GetOverviewLink(item.ObjectType).Replace("&XCN=", ""), DataObject.CommunityID, Helper.GetObjectName(item.ObjectType, false), item.Count);
            }

            return sb.ToString();
        }
    }
}