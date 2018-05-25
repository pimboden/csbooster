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
using System.Web.UI.WebControls;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using System.Web.UI;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget.Settings
{
    public partial class FormAdvFields : System.Web.UI.UserControl, IWidgetSettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetFormAdv");
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (!IsPostBack)
                FillControls(); 
        }

        private void FillControls()
        {
            XmlDocument xmlDocument = Utils.LoadWidgetInstanceSettings(InstanceId);
            txtTit.Text = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "txtTit", string.Empty);


            int intGrp = 0;
            foreach (XmlNode xmlGrp in xmlDocument.SelectNodes("/root/grp"))
            {
                intGrp++;
                if (intGrp > 1)
                    break;

                string strCbxType = string.Format("cbx{0}type", intGrp);
                ((CheckBox)Helper.FindControl(this, strCbxType)).Checked = XmlHelper.GetElementValue(xmlGrp, strCbxType, false);

                strCbxType = string.Format("cbx{0}typeMust", intGrp);
                ((CheckBox)Helper.FindControl(this, strCbxType)).Checked = XmlHelper.GetElementValue(xmlGrp, strCbxType, false);

                for (int i = 0; i < 10; i++)
                {
                    string strTxt = string.Format("txt{0}t{1}", intGrp, i);
                    ((TextBox)Helper.FindControl(this, strTxt)).Text = XmlHelper.GetElementValue(xmlGrp, strTxt, string.Empty);
                }
            }

            for (int i = 0; i < 10; i++)
            {
                string formField = string.Format("TxtForm{0}", i);
                ((TextBox)Helper.FindControl(this, formField)).Text = XmlHelper.GetElementValue(xmlDocument.DocumentElement, formField, string.Empty);

                formField = string.Format("CbxForm{0}Must", i);
                ((CheckBox)Helper.FindControl(this, formField)).Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, formField, false);
            }


        }

        public bool Save()
        {
            try
            {
                XmlDocument xmlDocument = Utils.LoadWidgetInstanceSettings(InstanceId);

                if (xmlDocument.DocumentElement == null)
                    XmlHelper.CreateRoot(xmlDocument, "root");

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "uid", UserDataContext.GetUserDataContext().UserID); 
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "txtTit", txtTit.Text);

                for (int intGrp = 1; intGrp < 2; intGrp++)
                {
                    XmlNode xmlGrp = xmlDocument.DocumentElement.SelectSingleNode("grp");
                    if (xmlGrp == null)
                        xmlGrp = XmlHelper.AppendNode(xmlDocument.DocumentElement, "grp");

                    string strCbxType = string.Format("cbx{0}type", intGrp);
                    XmlHelper.SetElementInnerText(xmlGrp, strCbxType, ((CheckBox)Helper.FindControl(this, strCbxType)).Checked);

                    strCbxType = string.Format("cbx{0}typeMust", intGrp);
                    XmlHelper.SetElementInnerText(xmlGrp, strCbxType, ((CheckBox)Helper.FindControl(this, strCbxType)).Checked);


                    for (int i = 0; i < 10; i++)
                    {
                        string strTxt = string.Format("txt{0}t{1}", intGrp, i);
                        XmlHelper.SetElementInnerText(xmlGrp, strTxt, ((TextBox)Helper.FindControl(this, strTxt)).Text.Trim());
                    }
                }

                for (int i = 0; i < 10; i++)
                {
                    string formField = string.Format("TxtForm{0}", i);
                    XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, formField, ((TextBox)Helper.FindControl(this, formField)).Text);

                    formField = string.Format("CbxForm{0}Must", i);
                    XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, formField, ((CheckBox)Helper.FindControl(this, formField)).Checked);
                }

                return Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
            }
            catch
            {
                return false;
            }
        }
    }
}