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

namespace _4screen.CSB.Widget
{
    public partial class KyteVideoSettings_Step10 : StepsASCX
    {
        private Guid InstanceID;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            InstanceID = new Guid(ObjectID);
            string strXml = LoadInstanceData(InstanceID);

            XmlDocument xmlDom = new XmlDocument();
            if (!string.IsNullOrEmpty(strXml))
            {
                xmlDom.LoadXml(strXml);
            }

            txtChannel.Text = XmlHelper.GetElementValueCDATA(xmlDom.DocumentElement, "KyteChannel", string.Empty);
        }

        public override bool SaveStep(int NextStep)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                base.SaveStep(NextStep);
                try
                {
                    XmlDocument xmlDom = new XmlDocument();
                    XmlHelper.CreateRoot(xmlDom, "root");

                    xmlDom.DocumentElement.SetAttribute("version", "2.0");
                    XmlHelper.SetElementInnerTextCDATA(xmlDom.DocumentElement, "KyteChannel", txtChannel.Text);
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
    }
}