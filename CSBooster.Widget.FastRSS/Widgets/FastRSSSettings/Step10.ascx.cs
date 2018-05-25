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
using _4screen.CSB.DataAccess;
using System.Web.UI;

namespace _4screen.CSB.Widget
{
    public partial class FastRSSSettings_Step10 : StepsASCX
    {
        #region FIELDS

        private Guid InstanceID;
        private Control ctrlRoleVisibilityAndFixation = null;
        private IRoleVisibilityAndFixation RoleVisibilityAndFixation = null;

        #endregion FIELDS

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            InstanceID = ObjectID.Value;
            string strXml = LoadInstanceData(InstanceID);

            XmlDocument xmlDom = new XmlDocument();
            if (!string.IsNullOrEmpty(strXml))
            {
                xmlDom.LoadXml(strXml);
            }
            txtURL.Text = XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtURL", string.Empty);
            ddlFC.SelectItem(XmlHelper.GetElementValue(xmlDom.DocumentElement, "ddlFC", 3));
            cbxDesc.Checked = XmlHelper.GetElementValue(xmlDom.DocumentElement, "cbxDesc", true);
            ctrlRoleVisibilityAndFixation = LoadControl("~/WidgetControls/RoleVisibilityAndFixation.ascx");
            RoleVisibilityAndFixation = (IRoleVisibilityAndFixation)ctrlRoleVisibilityAndFixation;
            ctrlRoleVisibilityAndFixation.ID = "RVAF";
            RoleVisibilityAndFixation.InstanceID = InstanceID;
            phRF.Controls.Add(ctrlRoleVisibilityAndFixation);
        }

        #region PRIVATE METHODES

        public override bool SaveStep(int NextStep)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                base.SaveStep(NextStep);
                try
                {
                    RoleVisibilityAndFixation.Save();
                    XmlDocument xmlDom = new XmlDocument();
                    XmlHelper.CreateRoot(xmlDom, "root");

                    xmlDom.DocumentElement.SetAttribute("version", "2.0");
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "txtURL", txtURL.Text);
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "ddlFC", ddlFC.SelectedValue);
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "cbxDesc", cbxDesc.Checked);
                    return SaveInstanceData(InstanceID, xmlDom.OuterXml);
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        #endregion PRIVATE METHODES
    }
}