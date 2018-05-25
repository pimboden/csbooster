// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget.Settings
{
    public partial class Board : System.Web.UI.UserControl, IWidgetSettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetBoard");  
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (!IsPostBack) 
                FillControls();
        }

        public bool Save()
        {
            try
            {
                XmlDocument xmlDocument = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "Comments", (CbxComments.Checked && DivComments.Visible));
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "Contents", (CbxContents.Checked && DivContents.Visible));
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "Favorites", (CbxFavorites.Checked && DivFavorites.Visible));
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "Friends", (CbxFriends.Checked && DivFriends.Visible));
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "Membership", (CbxMembership.Checked && DivMembership.Visible));
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "Message", (CbxMessage.Checked && DivMessage.Visible));
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "Notifications", (CbxNotifications.Checked && DivNotifications.Visible));
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "Properties", (CbxProperties.Checked && DivProperties.Visible));
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "Survey", (CbxSurvey.Checked && DivSurvey.Visible));

                return _4screen.CSB.DataAccess.Business.Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
            }
            catch
            {
                return false;
            }
        }

        private void FillControls()
        {
            XmlDocument xmlDocument = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);

            this.CbxComments.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Comments", true);
            this.CbxContents.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Contents", false);
            this.CbxFavorites.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Favorites", false);
            this.CbxFriends.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Friends", true);
            this.CbxMembership.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Membership", true);
            this.CbxMessage.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Message", true);
            this.CbxNotifications.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Notifications", false);
            this.CbxProperties.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Properties", true);
            this.CbxSurvey.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Survey", false);

            CustomizationSection customization = CustomizationSection.CachedInstance;

            if (ParentDataObject.ObjectType == 1)
            {
                DivComments.Visible = false;
                DivContents.Visible = false;
                DivFavorites.Visible = false;
                DivFriends.Visible = false;
                DivMembership.Visible = false;
                DivMessage.Visible = false;
                DivNotifications.Visible = false;
                DivProperties.Visible = false;
                DivSurvey.Visible = false;

                DivMembership.Visible = customization.Modules["Memberships"].Enabled;
                DivMessage.Visible = customization.Modules["Messaging"].Enabled;
            }
            else
            {
                DivComments.Visible = true;
                DivContents.Visible = true;
                DivFavorites.Visible = true;
                DivFriends.Visible = true;
                DivMembership.Visible = true;
                DivMessage.Visible = true;
                DivNotifications.Visible = true;
                DivProperties.Visible = true;
                DivSurvey.Visible = true;

                DivMessage.Visible = customization.Modules["Messaging"].Enabled;
                DivFriends.Visible = customization.Modules["Friends"].Enabled;
                DivComments.Visible = customization.Modules["Comments"].Enabled;
                DivNotifications.Visible = customization.Modules["Alerts"].Enabled;
                DivFavorites.Visible = customization.Modules["Favorites"].Enabled;
                DivMembership.Visible = customization.Modules["Memberships"].Enabled;
            }

        }
    }
}