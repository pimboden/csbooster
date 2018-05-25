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
using System.Web.UI;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.DataAccess;

namespace _4screen.CSB.Widget.Settings
{
    public partial class IFrame : System.Web.UI.UserControl, IWidgetSettings
    {
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        private Guid InstanceID;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            XmlDocument xmlDocument = Utils.LoadWidgetInstanceSettings(InstanceId);

            // fill the controls
            RntbHeight.Value = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "IFrameHeight", 100);

            TxtIFrameURL.Text = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "IFrameURL", string.Empty);
            if (string.IsNullOrEmpty(TxtIFrameURL.Text))
                TxtIFrameURL.Text = "http://";

            CbBorder.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "IFrameBorder", false);
            //TxtIFrameHeight.Text = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "IFrameHeight", string.Empty);

            //InstanceID = ObjectID.Value;
            //string strXml = LoadInstanceData(InstanceID);

            //XmlDocument xmlDom = new XmlDocument();
            //if (!string.IsNullOrEmpty(strXml))
            //{
            //    xmlDom.LoadXml(strXml);
            //}


            //ctrlRoleVisibilityAndFixation = LoadControl("~/WidgetControls/RoleVisibilityAndFixation.ascx");
            //RoleVisibilityAndFixation = (IRoleVisibilityAndFixation)ctrlRoleVisibilityAndFixation;
            //ctrlRoleVisibilityAndFixation.ID = "RVAF";
            //RoleVisibilityAndFixation.InstanceID = InstanceID;
            //phRF.Controls.Add(ctrlRoleVisibilityAndFixation);
        }

        public bool Save()
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlHelper.CreateRoot(xmlDocument, "root");

                string iFrameUrl = string.Empty;
                if (!string.IsNullOrEmpty(TxtIFrameURL.Text))
                {
                    if (!TxtIFrameURL.Text.ToLower().StartsWith("http"))
                        iFrameUrl = "http://" + TxtIFrameURL.Text;
                    else
                        iFrameUrl = TxtIFrameURL.Text;
                }

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "IFrameURL", iFrameUrl);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "IFrameHeight", (int)RntbHeight.Value.Value);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "IFrameBorder", CbBorder.Checked);

                return Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
            }
            catch
            {
                return false;
            }
        }

        //public override bool SaveStep(int NextStep)
        //{
        //    Page.Validate();
        //    if (Page.IsValid)
        //    {
        //        base.SaveStep(NextStep);
        //        try
        //        {
        //            RoleVisibilityAndFixation.Save();
        //            XmlDocument xmlDom = new XmlDocument();
        //            XmlHelper.CreateRoot(xmlDom, "root");

        //            xmlDom.DocumentElement.SetAttribute("version", "2.0");
        //            XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "IFrameURL", TxtIFrameURL.Text);
        //            XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "IFrameBorder", CbBorder.Checked);
        //            int iFrameHeight;
        //            if (int.TryParse(TxtIFrameHeight.Text, out iFrameHeight))
        //            {
        //                if (iFrameHeight == 0)
        //                    iFrameHeight = 100;
        //                if (iFrameHeight > 1000)
        //                    iFrameHeight = 1000;
        //                XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "IFrameHeight", iFrameHeight.ToString());
        //            }

        //            return SaveInstanceData(InstanceID, xmlDom.OuterXml);
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}