﻿//******************************************************************************
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
using System.Web.UI;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.DataAccess;

namespace _4screen.CSB.Widget.Settings
{
    public partial class UserLists : System.Web.UI.UserControl, IWidgetSettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetUserLists");
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        private Guid InstanceID;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            XmlDocument xmlDocument = Utils.LoadWidgetInstanceSettings(InstanceId);

            // fill the controls
            RntbUsers.Value = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "PageSize", 5);
            this.CbxPagerTop.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowPagerTop", false);
            this.CbxPagerBottom.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowPagerBottom", true);
        }

        public bool Save()
        {
            try
            {
                XmlDocument xmlDocument = Utils.LoadWidgetInstanceSettings(InstanceId);

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "PageSize", (int)RntbUsers.Value.Value);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ShowPagerTop", CbxPagerTop.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ShowPagerBottom", CbxPagerBottom.Checked);

                return Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
            }
            catch
            {
                return false;
            }
        }

    }
}