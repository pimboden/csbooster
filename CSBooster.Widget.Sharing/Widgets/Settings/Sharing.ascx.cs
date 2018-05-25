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
    public partial class Sharing : System.Web.UI.UserControl, IWidgetSettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetSharing");  
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

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ShowExtSharing", CbxShowExtSharing.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ShowSendUrl", CbxShowSendUrl.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ShowEmbedAndCopy", CbxShowEmbedAndCopy.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "EmbedVideoWidth", (int)RntbEmbedWidth.Value);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "EmbedVideoHeight", (int)RntbEmbedHeight.Value);

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

            this.CbxShowExtSharing.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowExtSharing", true);
            this.CbxShowSendUrl.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowSendUrl", true);
            this.CbxShowEmbedAndCopy.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowEmbedAndCopy", true);
            this.RntbEmbedWidth.Value = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "EmbedVideoWidth", 400);
            this.RntbEmbedHeight.Value = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "EmbedVideoHeight", 300);
        }
    }
}