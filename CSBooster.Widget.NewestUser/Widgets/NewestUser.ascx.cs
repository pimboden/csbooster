// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public partial class NewestUser : WidgetBase
    {
        protected GuiLanguage languageWidget = GuiLanguage.GetGuiLanguage("WidgetNewestUser");
        private bool hasContent = false;

        public override bool ShowObject(string settingsXml)
        {
            XmlDocument xmlDom = new XmlDocument();
            xmlDom.LoadXml(settingsXml);

            LoadNewesUsers(XmlHelper.GetElementValue(xmlDom.DocumentElement, "UserCount", 6));
            allUsers.NavigateUrl = string.Format("{0}{1}", SiteContext.VRoot, Constants.Links["NICE_LINK_TO_USER_OVERVIEW"]);
            allUsers.Text = languageWidget.GetString("LabelAllUsers");
            allUsers.ID = null;

            return hasContent;
        }

        private void LoadNewesUsers(int anzahl)
        {
            DataObjectList<DataObjectUser> newUsers = DataObjects.Load<DataObjectUser>(new QuickParametersUser()
                                                                                           {
                                                                                               Udc = UserDataContext.GetUserDataContext(),
                                                                                               Amount = anzahl,
                                                                                               PageNumber = 1,
                                                                                               PageSize = anzahl,
                                                                                               DisablePaging = true,
                                                                                               SortBy = QuickSort.InsertedDate,
                                                                                               IgnoreCache = false,
                                                                                           });

            hasContent = newUsers.Count > 0;

            this.RepNewUsers.DataSource = newUsers;
            this.RepNewUsers.DataBind();
        }

        protected void OnRepNewUsersBound(object sender, RepeaterItemEventArgs e)
        {
            DataObjectUser user = (DataObjectUser) e.Item.DataItem;
            PlaceHolder ph = e.Item.FindControl("phSUO") as PlaceHolder;
            Control ctrl = LoadControl("~/UserControls/Templates/SmallOutputUser2.ascx");
            ISmallOutputUser userOutput = ctrl as ISmallOutputUser;
            userOutput.UserName = user.Nickname;
            userOutput.UserPictureURL = SiteConfig.MediaDomainName + user.GetImage(PictureVersion.S);
            userOutput.PrimaryColor = user.PrimaryColor;
            userOutput.SecondaryColor = user.SecondaryColor;
            userOutput.UserDetailURL = SiteConfig.SiteVRoot + Helper.GetDetailLink(Helper.GetObjectTypeNumericID("User"), user.Nickname);
            ctrl.ID = null;
            ph.Controls.Add(ctrl);
        }
    }
}