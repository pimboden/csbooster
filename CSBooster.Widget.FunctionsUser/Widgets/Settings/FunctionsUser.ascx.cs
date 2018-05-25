//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		17.08.2007 / PI
//                         Inherits StepsASCX
//                         Step with Basic Info
//  Updated:   
//******************************************************************************

using System;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.Common.Statistic;
using _4screen.CSB.DataAccess;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget.Settings
{
    public partial class FunctionsUser : System.Web.UI.UserControl, IWidgetSettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetFunctionsUser");  
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
                XmlDocument xmlDocument = Utils.LoadWidgetInstanceSettings(InstanceId);

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "Friends", (CbxFriends.Checked && DivFriends.Visible));
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "Membership", (CbxMembership.Checked && DivMembership.Visible));
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "Message", (CbxMessage.Checked && DivMessage.Visible));
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "Notifications", (CbxNotifications.Checked && DivNotifications.Visible));

                return Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
            }
            catch
            {
                return false;
            }
        }

        private void FillControls()
        {
            XmlDocument xmlDocument = Utils.LoadWidgetInstanceSettings(InstanceId);

            this.CbxFriends.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Friends", true);
            this.CbxMembership.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Membership", true);
            this.CbxMessage.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Message", true);
            this.CbxNotifications.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Notifications", true);

            CustomizationSection customization = CustomizationSection.CachedInstance;

            DivMessage.Visible = customization.Modules["Messaging"].Enabled;
            DivFriends.Visible = customization.Modules["Friends"].Enabled;
            DivNotifications.Visible = customization.Modules["Alerts"].Enabled;
            DivMembership.Visible = customization.Modules["Memberships"].Enabled;

        }
    }
}