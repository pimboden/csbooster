// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System.Web.UI.WebControls;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public partial class OnlineUser : WidgetBase
    {
        protected GuiLanguage languageWidget = GuiLanguage.GetGuiLanguage("WidgetOnlineUser");
        private bool hasContent = false;

        public override bool ShowObject(string settingsXml)
        {
            XmlDocument xmlDom = new XmlDocument();
            xmlDom.LoadXml(settingsXml);

            LoadOnlineUsers(XmlHelper.GetElementValue(xmlDom.DocumentElement, "UserCount", 4));

            return hasContent;
        }

        private void LoadOnlineUsers(int anzahl)
        {
            UserDataContext udc = UserDataContext.GetUserDataContext();

            DataObjectList<DataObjectUser> usersOnline = DataObjects.Load<DataObjectUser>(new QuickParametersUser()
                                                                                              {
                                                                                                  Udc = udc,
                                                                                                  Amount = anzahl,
                                                                                                  PageNumber = 1,
                                                                                                  PageSize = anzahl,
                                                                                                  SortBy = QuickSort.Random,
                                                                                                  IsOnline = true,
                                                                                                  DisablePaging = true
                                                                                              });

            hasContent = usersOnline.Count > 0;

            this.RepUsersOnline.DataSource = usersOnline;
            this.RepUsersOnline.DataBind();
        }

        protected void OnRepUsersOnlineBound(object sender, RepeaterItemEventArgs e)
        {
            DataObjectUser user = (DataObjectUser) e.Item.DataItem;

            HyperLink userLink = (HyperLink) e.Item.FindControl("LnkUser");
            userLink.Text = user.Nickname;
            userLink.NavigateUrl = SiteConfig.SiteVRoot + Helper.GetDetailLink(Helper.GetObjectTypeNumericID("User"), user.Nickname);
            userLink.ID = null;

            Literal memberDate = (Literal) e.Item.FindControl("MemberDate");
            memberDate.Text = user.Inserted.ToShortDateString();
            memberDate.ID = null;
        }
    }
}