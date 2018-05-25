using System;
using System.Collections.Generic; 
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.Common.Widget;
using _4screen.CSB.DataAccess;   

namespace _4screen.CSB.Widget
{
    public partial class FormAdv : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(settingsXml);

            Control control = LoadControl("~/UserControls/Form.ascx");

            IForm ascx = (IForm)control;
            ascx.CreatorUserId = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "uid ", string.Empty).ToGuid(); 
            ascx.AdressSave = (FormAdressSave)XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DdlAdressSave", 0);
            ascx.AdressCommentShow = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxAdressCommentShow", false);
            ascx.AdressShow = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxAdressShow", false);
            ascx.FormText = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "txtTit", string.Empty);
            ascx.HasContent = true;
            ascx.MustAuthenticated = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxMustAuth", true);
            ascx.PostText = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "TxtNach", string.Empty);
            ascx.PreText = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "TxtVor", string.Empty);
            ascx.ReceptorEmail = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "TxtEmail ", string.Empty);
            ascx.ReceptorUserId = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DdlUser ", string.Empty).ToNullableGuid();
            ascx.SendCopyToUserAs = (CarrierType)XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DdlUserCopy ", 0);
            ascx.SendFormAs = (CarrierType)XmlHelper.GetElementValue(xmlDocument.DocumentElement, "RblSendAs ", 0);
            ascx.SubjectText = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "TxtSubject ", string.Empty);
            ascx.TextBoxMust = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CBTextMust ", false);
            ascx.TextBoxShow = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CBText ", false);

            int intGrp = 1;
            foreach(XmlNode xmlGrp in xmlDocument.SelectNodes("/root/grp"))
            {
                string strCbxType = string.Format("cbx{0}type", intGrp);
                ascx.OptionsMulti = XmlHelper.GetElementValue(xmlGrp, strCbxType, false);
                strCbxType = string.Format("cbx{0}typeMust", intGrp);
                ascx.OptionMust = XmlHelper.GetElementValue(xmlGrp, strCbxType, true);

                List<string> optionTexts = new List<string>();
                for (int i = 0; i < 10; i++)
                {
                    string strTxt = string.Format("txt{0}t{1}", intGrp, i);
                    optionTexts.Add(XmlHelper.GetElementValue(xmlGrp, strTxt, string.Empty));
                }
                ascx.OptionTexts = optionTexts; 
                break;
            }

            List<string> formFieldTexts = new List<string>();
            List<bool> formFieldMusts = new List<bool>();
            for (int i = 0; i < 10; i++)
            {
                string formField = string.Format("TxtForm{0}", i);
                formFieldTexts.Add(XmlHelper.GetElementValue(xmlDocument.DocumentElement, formField, string.Empty));

                formField = string.Format("CbxForm{0}Must", i);
                formFieldMusts.Add(XmlHelper.GetElementValue(xmlDocument.DocumentElement, formField, false));
            }
            ascx.FormFieldTexts = formFieldTexts;
            ascx.FormFieldMusts = formFieldMusts;
            ascx.FillAdressData();  

            PhDet.Controls.Add(control);

            return true;
        }

        //public override bool ShowObject(string configXml)
        //{
        //    userDataContext = SiteContext.Udc;
        //    this.configXml = configXml;
        //    string strPrefix = InstanceID.ToString().Replace("-", string.Empty);
        //    StringBuilder sb = new StringBuilder(1000);

        //    if (configXml.Length > 0)
        //    {
        //        XmlDocument xmlDocument = new XmlDocument();
        //        xmlDocument.LoadXml(configXml);

        //        string title = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "txtTit", string.Empty);
        //        if (!string.IsNullOrEmpty(title))
        //        {
        //            LitTitle.Text = title;
        //        }

        //        bool hasTextbox = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CBText", false);
        //        if (hasTextbox)
        //        {
        //            TextBox textBox = new TextBox();
        //            textBox.ID = "Txt";
        //            textBox.Attributes.Add("style", "width:99%;height:100px;");
        //            textBox.TextMode = TextBoxMode.MultiLine;
        //            Pnl0.Controls.Add(textBox);
        //            Pnl0.Visible = true;
        //        }

        //        int groupIndex = 0;
        //        foreach (XmlNode xmlGrp in xmlDocument.SelectNodes("/root/grp"))
        //        {
        //            groupIndex++;
        //            bool isCheckbox = XmlHelper.GetElementValue(xmlGrp, string.Format("cbx{0}type", groupIndex), false);
        //            for (int i = 0; i < 10; i++)
        //            {
        //                string choiceValue = XmlHelper.GetElementValue(xmlGrp, string.Format("txt{0}t{1}", groupIndex, i), string.Empty);
        //                if (choiceValue.Length > 0)
        //                {
        //                    Pnl1.Visible = true;
        //                    if (isCheckbox)
        //                    {
        //                        CheckBox checkBox = new CheckBox();
        //                        checkBox.Text = choiceValue;
        //                        checkBox.ID = string.Format("opt{0}_{1}", groupIndex, i);
        //                        HtmlGenericControl div = new HtmlGenericControl("div");
        //                        div.Controls.Add(checkBox);
        //                        Pnl1.Controls.Add(div);
        //                    }
        //                    else
        //                    {
        //                        RadioButton radioButton = new RadioButton();
        //                        radioButton.Text = choiceValue;
        //                        radioButton.ID = string.Format("opt{0}_{1}", groupIndex, i);
        //                        radioButton.GroupName = "Group1";
        //                        HtmlGenericControl div = new HtmlGenericControl("div");
        //                        div.Controls.Add(radioButton);
        //                        Pnl1.Controls.Add(div);
        //                    }
        //                }
        //            }
        //        }

        //        for (int i = 0; i < 10; i++)
        //        {
        //            string formFieldName = XmlHelper.GetElementValue(xmlDocument.DocumentElement, string.Format("TxtForm{0}", i), string.Empty);
        //            if (formFieldName.Length > 0)
        //            {
        //                Pnl2.Visible = true;
        //                TextBox textBox = new TextBox();
        //                textBox.Attributes.Add("style", "width:99%");
        //                textBox.ID = string.Format("TxtForm{0}", i);
        //                HtmlGenericControl div1 = new HtmlGenericControl("div");
        //                div1.Attributes.Add("class", "CSB_input_label");
        //                div1.Controls.Add(new LiteralControl(formFieldName));
        //                HtmlGenericControl div2 = new HtmlGenericControl("div");
        //                div2.Attributes.Add("class", "CSB_input_cnt");
        //                div2.Controls.Add(textBox);
        //                HtmlGenericControl div3 = new HtmlGenericControl("div");
        //                div3.Attributes.Add("class", "CSB_input_block");
        //                div3.Controls.Add(div1);
        //                div3.Controls.Add(div2);
        //                Pnl2.Controls.Add(div3);
        //            }
        //        }
        //        Pnl2.Controls.Add(new LiteralControl("<div class=\"CSB_clear\"></div>"));
        //    }
        //    return true;
        //}

        //private void SendData()
        //{
        //    if (configXml.Length > 0)
        //    {
        //        XmlDocument xmlDocument = new XmlDocument();
        //        xmlDocument.LoadXml(configXml);

        //        StringBuilder sb = new StringBuilder(500);
        //        string title = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "txtTit", string.Empty);
        //        sb.AppendFormat("<div><b>{0}</b></div>", title);

        //        if (XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CBText", false))
        //        {
        //            sb.Append("<div style=\"margin-top:10px;\">");
        //            string Txt = Request.Form[string.Format("{0}$Txt", UniqueID)];
        //            sb.Append(Txt);
        //            sb.Append("</div>");
        //        }

        //        sb.Append("<div style=\"margin-top:10px;\">");
        //        int groupIndex = 0;
        //        foreach (XmlNode xmlGrp in xmlDocument.SelectNodes("/root/grp"))
        //        {
        //            groupIndex++;
        //            for (int i = 0; i < 10; i++)
        //            {
        //                string value = Request.Form[string.Format("{0}$opt{1}_{2}", UniqueID, groupIndex, i)];
        //                if (!string.IsNullOrEmpty(value))
        //                {
        //                    string choiceValue = XmlHelper.GetElementValue(xmlGrp, string.Format("txt{0}t{1}", groupIndex, i), string.Empty);
        //                    sb.AppendFormat("<div>{0}</div>", choiceValue);
        //                }
        //            }
        //        }
        //        sb.Append("</div>");

        //        sb.Append("<div style=\"margin-top:10px;\">");
        //        for (int i = 0; i < 10; i++)
        //        {
        //            string formFieldName = XmlHelper.GetElementValue(xmlDocument.DocumentElement, string.Format("TxtForm{0}", i), string.Empty);
        //            string formFieldValue = Request.Form[string.Format("{0}$TxtForm{1}", UniqueID, i)];
        //            if (formFieldName.Length > 0)
        //            {
        //                sb.AppendFormat("<div class=\"CSB_input_block\"><div class=\"CSB_input_label\">{0}</div><div class=\"CSB_input_cnt\">{1}</div></div>", formFieldName, formFieldValue);
        //            }
        //        }
        //        sb.Append("<div class=\"CSB_clear\"></div>");
        //        sb.Append("</div>");

        //        Message msg = new Message(SiteContext);
        //        if (userDataContext.IsAuthenticated)
        //            msg.FromUserID = userDataContext.UserID;
        //        else
        //            msg.FromUserID = new Guid(XmlHelper.GetElementValue(xmlDocument.DocumentElement, "uid", Guid.NewGuid().ToString()));
        //        msg.UserId = new Guid(XmlHelper.GetElementValue(xmlDocument.DocumentElement, "uid", Guid.NewGuid().ToString()));
        //        msg.IsRead = false;
        //        msg.Subject = string.Format("Formular: {0}", title.CropString(32));
        //        msg.MsgText = sb.ToString();
        //        msg.SendNormalMessage(true, false);
        //        PnlForm.Visible = false;
        //        LitMessage.Text = "Erfolgreich gesendet!";
        //    }
        //}

        //protected void lbtnSend_Click(object sender, EventArgs e)
        //{
        //    SendData();
        //}
    }
}