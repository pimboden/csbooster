//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		17.08.2007 / PI
//******************************************************************************
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;
using Telerik.Web.UI;

namespace _4screen.CSB.Widget.Settings
{
    public partial class Shop : System.Web.UI.UserControl, IWidgetSettings
    {
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

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "rcbBasketType", rcbBasketType.SelectedValue);

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
            rcbBasketType.SelectedIndex = rcbBasketType.Items.IndexOf(rcbBasketType.Items.FindItemByValue(XmlHelper.GetElementValue(xmlDocument.DocumentElement, "rcbBasketType", "Small")));
        }
    }
}
