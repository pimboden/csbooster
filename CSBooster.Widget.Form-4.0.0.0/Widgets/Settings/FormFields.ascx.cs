// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI.WebControls;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget.Settings
{
    public partial class FormFields : System.Web.UI.UserControl, IWidgetSettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetForm");
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            FillControls();
        }

        private void FillControls()
        {
            XmlDocument xmlDocument = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);
            txtTit.Text = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "txtTit", string.Empty);

            CBText.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CBText", false);
            CBTextMust.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CBTextMust", true);

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
                XmlDocument xmlDocument = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "uid", UserDataContext.GetUserDataContext().UserID);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "txtTit", txtTit.Text);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "CBText", CBText.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "CBTextMust", CBTextMust.Checked);

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


                return _4screen.CSB.DataAccess.Business.Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
            }
            catch
            {
                return false;
            }
        }

    }
}