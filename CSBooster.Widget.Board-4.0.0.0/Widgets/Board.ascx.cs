// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Web.UI;
using System.Xml;
using _4screen.CSB.DataAccess;

namespace _4screen.CSB.Widget
{
    public partial class Board : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            var asdf = base.WidgetHost;

            XmlDocument xmlDom = new XmlDocument();
            xmlDom.LoadXml(settingsXml);

            Control control = null;
            if (!string.IsNullOrEmpty(Request.QueryString["CN"]))
            {
                control = LoadControl("~/Widgets/BoardCommunity.ascx");
                BoardCommunity ctrl = control as BoardCommunity;
                ctrl.ShowMembership = XmlHelper.GetElementValue(xmlDom.DocumentElement, "Membership", true);
                ctrl.ShowMessage = XmlHelper.GetElementValue(xmlDom.DocumentElement, "Message", true);   
            }
            else
            {
                control = LoadControl("~/Widgets/BoardProfile.ascx");
                BoardProfile ctrl = control as BoardProfile;
                ctrl.ShowComments = XmlHelper.GetElementValue(xmlDom.DocumentElement, "Comments", true);
                ctrl.ShowContents = XmlHelper.GetElementValue(xmlDom.DocumentElement, "Contents", false);
                ctrl.ShowFavorites = XmlHelper.GetElementValue(xmlDom.DocumentElement, "Favorites", false);
                ctrl.ShowFriends = XmlHelper.GetElementValue(xmlDom.DocumentElement, "Friends", true);
                ctrl.ShowMembership = XmlHelper.GetElementValue(xmlDom.DocumentElement, "Membership", true);
                ctrl.ShowMessage = XmlHelper.GetElementValue(xmlDom.DocumentElement, "Message", true);
                ctrl.ShowNotifications = XmlHelper.GetElementValue(xmlDom.DocumentElement, "Notifications", false);
                ctrl.ShowProperties = XmlHelper.GetElementValue(xmlDom.DocumentElement, "Properties", true);
                ctrl.ShowSurvey = XmlHelper.GetElementValue(xmlDom.DocumentElement, "Survey", false);
            }

            control.ID = "Board";
            this.PhCnt.Controls.Add(control);

            return true;
        }
    }
}
