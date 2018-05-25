//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		08.04.2009 / PT
//******************************************************************************

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.Common.Widget;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class Form : System.Web.UI.UserControl, IForm
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage languageProfile = GuiLanguage.GetGuiLanguage("ProfileData");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");

        public Guid CreatorUserId { get; set; }
        public string FormText { get; set; }
        public bool TextBoxShow { get; set; }
        public bool TextBoxMust { get; set; }
        public bool OptionsMulti { get; set; }
        public bool OptionMust { get; set; }
        public List<string> OptionTexts { get; set; }
        public List<string> FormFieldTexts { get; set; }
        public List<bool> FormFieldMusts { get; set; }
        public bool AdressShow { get; set; }
        public bool AdressCommentShow { get; set; }
        public FormAdressSave AdressSave { get; set; }
        public bool MustAuthenticated { get; set; }
        public string SubjectText { get; set; }
        public string PreText { get; set; }
        public string PostText { get; set; }
        public CarrierType SendCopyToUserAs { get; set; }
        public CarrierType SendFormAs { get; set; }
        public Guid? ReceptorUserId { get; set; }
        public string ReceptorEmail { get; set; }
        public bool HasContent { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            UserDataContext udc = UserDataContext.GetUserDataContext();

            this.RevEMail.ValidationExpression = Constants.REGEX_EMAIL;
            this.CbxAdressSave.Text = language.GetString("LableFormSaveAddress");

            string valGrp = Guid.NewGuid().ToString();
            this.RevEMail.ValidationGroup = valGrp;
            this.RfvAddressCity.ValidationGroup = valGrp;
            this.RfvAddressStreet.ValidationGroup = valGrp;
            this.RfvAddressZip.ValidationGroup = valGrp;
            this.RfvEMail.ValidationGroup = valGrp;
            this.RfvField0.ValidationGroup = valGrp;
            this.RfvField1.ValidationGroup = valGrp;
            this.RfvField2.ValidationGroup = valGrp;
            this.RfvField3.ValidationGroup = valGrp;
            this.RfvField4.ValidationGroup = valGrp;
            this.RfvField5.ValidationGroup = valGrp;
            this.RfvField6.ValidationGroup = valGrp;
            this.RfvField7.ValidationGroup = valGrp;
            this.RfvField8.ValidationGroup = valGrp;
            this.RfvField9.ValidationGroup = valGrp;
            this.RfvMobile.ValidationGroup = valGrp;
            this.RfvName.ValidationGroup = valGrp;
            this.RfvPhone.ValidationGroup = valGrp;
            this.RfvSex.ValidationGroup = valGrp;
            this.RfvText.ValidationGroup = valGrp;
            this.RfvVorname.ValidationGroup = valGrp;
            this.lbtnSend.ValidationGroup = valGrp;

            if (!string.IsNullOrEmpty(FormText))
            {
                LitText.Text = FormText;
                LitText.Visible = true;
                PnlText.Visible = true;
            }

            if (TextBoxShow)
            {
                TxtText.Visible = true;
                RfvText.Enabled = TextBoxMust;
            }


            int index = 0;
            if (OptionTexts != null)
            {
                foreach (string choiceValue in OptionTexts)
                {
                    if (!string.IsNullOrEmpty(choiceValue))
                    {
                        PnlOption.Visible = true;
                        if (OptionsMulti)
                        {
                            CblOption.Items.Add(new ListItem(choiceValue, index.ToString()));
                        }
                        else
                        {
                            RblOption.Items.Add(new ListItem(choiceValue, index.ToString()));
                        }
                    }
                    index++;
                }
            }

            if (PnlOption.Visible)
            {
                if (OptionsMulti)
                {
                    CblOption.Visible = true;
                    // muss noch gemacht werden
                }
                else
                {
                    RblOption.Visible = true;
                    if (OptionMust)
                        RblOption.SelectedIndex = 0;
                }
            }

            if (FormFieldTexts != null)
            {
                index = 0;
                foreach (string label in FormFieldTexts)
                {
                    if (!string.IsNullOrEmpty(label))
                    {
                        PnlFields.Visible = true;

                        string nameID = string.Format("PnlField{0}", index);
                        Panel pnl = (Panel)Helper.FindControl(PnlFields, nameID);
                        if (pnl != null)
                        {
                            pnl.Visible = true;
                            nameID = string.Format("LitField{0}", index);
                            Literal lit = (Literal)Helper.FindControl(PnlFields, nameID);
                            lit.Text = label;

                            nameID = string.Format("RfvField{0}", index);
                            RequiredFieldValidator rfv = (RequiredFieldValidator)Helper.FindControl(PnlFields, nameID);
                            rfv.Enabled = FormFieldMusts[index];
                        }
                    }
                    index++;
                }
            }

            bool adressShow = false;
            bool showOnlyEmail = false;
            if (AdressShow)
                adressShow = true;
            else if (!udc.IsAuthenticated && SendCopyToUserAs == CarrierType.EMail)
                showOnlyEmail = true;


            if (adressShow || showOnlyEmail)
            {
                Dictionary<string, UserAddressFields> list = DataAccessConfiguration.GetUserAddressFields();

                PnlAdress.Visible = true;

                if (list.ContainsKey("EMail"))
                {
                    this.PnlEMail.Visible = list["EMail"].Active;
                    if (SendCopyToUserAs == CarrierType.EMail || showOnlyEmail)
                    {
                        this.RfvEMail.Enabled = true;
                        this.RevEMail.Enabled = true;
                    }
                    else
                    {
                        this.RfvEMail.Enabled = (PnlEMail.Visible && list["EMail"].Must);
                        this.RevEMail.Enabled = (PnlEMail.Visible && list["EMail"].Must);
                    }
                    this.RfvEMail.Visible = this.RfvEMail.Enabled;
                    this.RevEMail.Visible = this.RevEMail.Enabled;
                }

                if (adressShow)
                {
                    if (list.ContainsKey("Phone"))
                    {
                        this.PnlPhone.Visible = list["Phone"].Active;
                        this.RfvPhone.Enabled = (PnlPhone.Visible && list["Phone"].Must);
                        this.RfvPhone.Visible = this.RfvPhone.Enabled;
                    }

                    if (list.ContainsKey("Mobile"))
                    {
                        this.PnlMobile.Visible = list["Mobile"].Active;
                        this.RfvMobile.Enabled = (PnlMobile.Visible && list["Mobile"].Must);
                        this.RfvMobile.Visible = this.RfvMobile.Enabled;
                    }

                    if (list.ContainsKey("Sex"))
                    {
                        this.PnlSex.Visible = list["Sex"].Active;
                        this.RfvSex.Enabled = (PnlSex.Visible && list["Sex"].Must);
                        this.RfvSex.Visible = this.RfvSex.Enabled;
                    }

                    if (list.ContainsKey("Name"))
                    {
                        this.PnlName.Visible = list["Name"].Active;
                        this.RfvName.Enabled = (PnlName.Visible && list["Name"].Must);
                        this.RfvName.Visible = this.RfvName.Enabled;
                    }

                    if (list.ContainsKey("Vorname"))
                    {
                        this.PnlVorname.Visible = list["Vorname"].Active;
                        this.RfvVorname.Enabled = (PnlVorname.Visible && list["Vorname"].Must);
                        this.RfvVorname.Visible = this.RfvVorname.Enabled;
                    }

                    if (list.ContainsKey("AddressStreet"))
                    {
                        this.PnlAddressStreet.Visible = list["AddressStreet"].Active;
                        this.RfvAddressStreet.Enabled = (PnlAddressStreet.Visible && list["AddressStreet"].Must);
                        this.RfvAddressStreet.Visible = this.RfvAddressStreet.Enabled;
                    }

                    if (list.ContainsKey("AddressZip"))
                    {
                        this.PnlAddressZip.Visible = list["AddressZip"].Active;
                        this.RfvAddressZip.Enabled = (PnlAddressZip.Visible && list["AddressZip"].Must);
                        this.RfvAddressZip.Visible = this.RfvAddressZip.Enabled;
                    }

                    if (list.ContainsKey("AddressCity"))
                    {
                        this.AddressCity.Visible = list["AddressCity"].Active;
                        this.RfvAddressCity.Enabled = (AddressCity.Visible && list["AddressCity"].Must);
                        this.RfvAddressCity.Visible = this.RfvAddressCity.Enabled;
                    }

                    if (AdressCommentShow)
                        this.PnlComment.Visible = true;

                    if (this.AdressSave == FormAdressSave.Ask && udc.IsAuthenticated)
                    {
                        PnlAdressSave.Visible = true;
                    }
                }
            }

            if (MustAuthenticated && !udc.IsAuthenticated)
            {
                PnlSend.Visible = false;
                PnlLogin.Visible = true;
                this.HplLogin.NavigateUrl = Constants.Links["LINK_TO_LOGIN_PAGE"].Url.Replace("##CURRENT_PAGE##", Server.UrlEncode(Request.Url.PathAndQuery));
            }
            else if (!MustAuthenticated && !udc.IsAuthenticated)
            {
                PnlCheck.Visible = true;
            }
        }

        public void FillAdressData()
        {
            if (AdressShow)
            {
                UserDataContext udc = UserDataContext.GetUserDataContext();
                if (udc.IsAuthenticated)
                {
                    DataObjectUser user = DataObjectUser.Load<DataObjectUser>(udc.UserID, null, true);
                    if (user.Sex.IndexOf("-1") > -1)
                        Helper.Ddl_SelectItem(this.Sex, -1);
                    else if (user.Sex.IndexOf("0") > -1)
                        Helper.Ddl_SelectItem(this.Sex, 0);
                    else if (user.Sex.IndexOf("1") > -1)
                        Helper.Ddl_SelectItem(this.Sex, 1);
                    else
                        Helper.Ddl_SelectItem(this.Sex, -1);

                    this.Name.Text = user.Name;
                    this.Vorname.Text = user.Vorname;
                    this.AddressStreet.Text = user.AddressStreet;
                    this.AddressZip.Text = user.AddressZip;
                    this.AddressCity.Text = user.AddressCity;
                    this.EMail.Text = Membership.GetUser(udc.UserID).Email;
                    this.Phone.Text = user.Phone;
                    this.Mobile.Text = user.Mobile;
                }
            }
        }

        private void SaveAdressData()
        {
            if (AdressShow)
            {
                UserDataContext udc = UserDataContext.GetUserDataContext();
                if (udc.IsAuthenticated)
                {
                    DataObjectUser user = DataObjectUser.Load<DataObjectUser>(udc.UserID, null, true);
                    if (this.PnlSex.Visible)
                    {
                        if (this.Sex.SelectedItem.Value == "0")
                            user.Sex = ";0;";
                        else if (this.Sex.SelectedItem.Value == "1")
                            user.Sex = ";1;";
                    }

                    if (this.PnlName.Visible)
                        user.Name = this.Name.Text;
                    if (this.PnlVorname.Visible)
                        user.Vorname = this.Vorname.Text;
                    if (this.PnlAddressStreet.Visible)
                        user.AddressStreet = this.AddressStreet.Text;
                    if (this.PnlAddressZip.Visible)
                        user.AddressZip = this.AddressZip.Text;
                    if (this.PnlAddressZip.Visible)
                        user.AddressCity = this.AddressCity.Text;
                    //this.EMail.Text = Membership.GetUser(udc.UserId).Email;
                    if (this.PnlPhone.Visible)
                        user.Phone = this.Phone.Text;
                    if (this.PnlMobile.Visible)
                        user.Mobile = this.Mobile.Text;

                    user.Update(UserDataContext.GetUserDataContext());
                }
            }
        }

        private string CreateMessage()
        {

            StringBuilder sb = new StringBuilder(500);

            sb.Append("<div>");
            if (!string.IsNullOrEmpty(PreText))
            {
                sb.AppendFormat("<div>{0}</div>", PreText);
            }

            if (!string.IsNullOrEmpty(FormText))
            {
                sb.AppendFormat("<div><b>{0}</b></div>", FormText);
            }

            if (TextBoxShow)
            {
                sb.Append("<div style=\"margin-top:10px;\">");
                sb.Append((string)TxtText.Text);
                sb.Append("</div>");
            }

            bool blnFirst = true;

            for (int i = 0; i < 10; i++)
            {
                int index = -1;
                if (OptionsMulti)
                {
                    if (i < CblOption.Items.Count && CblOption.Items[i].Selected)
                        index = Convert.ToInt32((string)CblOption.Items[i].Value);
                }
                else
                {
                    if (i < RblOption.Items.Count && RblOption.Items[i].Selected)
                        index = Convert.ToInt32((string)RblOption.Items[i].Value);
                }

                if (index > -1)
                {
                    if (blnFirst)
                    {
                        sb.Append("<div style=\"margin-top:10px;\">");
                        sb.AppendFormat("<div><b>{0}</b></div>", language.GetString("TextFormOptions"));
                        blnFirst = false;
                    }
                    sb.AppendFormat("<div>{0}</div>", OptionTexts[index]);
                }
            }
            if (!blnFirst)
            {
                sb.Append("</div>");
            }

            blnFirst = true;
            for (int i = 0; i < 10; i++)
            {
                string value = Request.Form[string.Format("{0}$TxtField{1}", UniqueID, i)];
                if (!string.IsNullOrEmpty(value))
                {
                    if (blnFirst)
                    {
                        sb.Append("<div style=\"margin-top:10px;\">");
                        sb.AppendFormat("<div><b>{0}</b></div>", language.GetString("TextFormFields"));
                        blnFirst = false;
                    }
                    sb.AppendFormat("<div>{0}: {1}</div>", FormFieldTexts[i], value);
                }
            }
            if (!blnFirst)
            {
                sb.Append("</div>");
            }

            if (AdressShow)
            {
                sb.Append("<div style=\"margin-top:10px;\">");
                sb.AppendFormat("<div><b>{0}</b></div>", languageProfile.GetString("TitleAddress"));

                if (PnlSex.Visible)
                    sb.AppendFormat("<div>{0}: {1}</div>", languageProfile.GetString("LableTitle"), this.Sex.SelectedItem.Text);
                if (PnlName.Visible)
                    sb.AppendFormat("<div>{0}: {1}</div>", languageProfile.GetString("Name"), this.Name.Text);
                if (PnlVorname.Visible)
                    sb.AppendFormat("<div>{0}: {1}</div>", languageProfile.GetString("FirstName"), this.Vorname.Text);
                if (PnlAddressStreet.Visible)
                    sb.AppendFormat("<div>{0}: {1}</div>", languageProfile.GetString("Street"), this.AddressStreet.Text);
                if (PnlAddressZip.Visible)
                    sb.AppendFormat("<div>{0} / {1}: {2} {3}</div>", languageProfile.GetString("Zip"), languageProfile.GetString("City"), this.AddressZip.Text, this.AddressCity.Text);
                if (PnlEMail.Visible)
                    sb.AppendFormat("<div>{0}: {1}</div>", languageShared.GetString("LabelEmail"), this.EMail.Text);
                if (PnlPhone.Visible)
                    sb.AppendFormat("<div>{0}: {1}</div>", languageProfile.GetString("Phone"), this.Phone.Text);
                if (PnlMobile.Visible)
                    sb.AppendFormat("<div>{0}: {1}</div>", languageProfile.GetString("Mobile"), this.Mobile.Text);
                if (PnlComment.Visible)
                    sb.AppendFormat("<div>{0}: {1}</div>", languageShared.GetString("LableAddressComment"), this.Comment.Text);
                sb.Append("</div>");
            }

            if (!string.IsNullOrEmpty(PostText))
            {
                sb.AppendFormat("<div>{0}</div>", PostText);
            }

            sb.Append("<div class=\"clearBoth\"></div>");
            sb.Append("</div>");

            return sb.ToString();
        }

        private bool Send()
        {
            SmtpSection smtpSec = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            string senderEmail = !string.IsNullOrEmpty(this.EMail.Text) ? this.EMail.Text : null;
            string senderName = null;
            if (!string.IsNullOrEmpty(this.Vorname.Text) && !string.IsNullOrEmpty(this.Name.Text))
                senderName = this.Vorname.Text + " " + this.Name.Text;
            else if (!string.IsNullOrEmpty(this.Vorname.Text))
                senderName = this.Vorname.Text;
            else if (!string.IsNullOrEmpty(this.Name.Text))
                senderName = this.Name.Text;
            string senderAddress = smtpSec.From;
            if (!string.IsNullOrEmpty(senderName))
                senderAddress = Regex.Replace(senderAddress, ".*(<.*?>).*", senderName + " $1");
            MailAddress replyAddress = null;
            if (!string.IsNullOrEmpty(senderEmail) && !string.IsNullOrEmpty(senderName))
                replyAddress = new MailAddress(senderEmail, senderName);
            else if (!string.IsNullOrEmpty(senderEmail))
                replyAddress = new MailAddress(senderEmail);
            else
                replyAddress = new MailAddress(smtpSec.From);

            UserDataContext udc = UserDataContext.GetUserDataContext();
            bool blnRet = true;
            try
            {
                string body = CreateMessage();
                if (SendFormAs == CarrierType.CSBMessage)
                {
                    Message msg = new Message(new SiteContext());
                    if (udc.IsAuthenticated)
                        msg.FromUserID = udc.UserID;
                    else
                        msg.FromUserID = CreatorUserId;

                    msg.UserId = ReceptorUserId.Value;

                    msg.IsRead = false;
                    msg.Subject = SubjectText;
                    msg.MsgText = body;
                    msg.SendNormalMessage(true, false);
                }
                else if (SendFormAs == CarrierType.EMail)
                {
                    MailMessage objMail = new MailMessage();
                    objMail.From = new MailAddress(senderAddress);
                    if (ReceptorUserId.HasValue)
                        objMail.To.Add(new MailAddress(Membership.GetUser(ReceptorUserId.Value).Email));
                    foreach (string email in ReceptorEmail.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(email))
                            objMail.To.Add(new MailAddress(email));
                    }
                    objMail.ReplyTo = replyAddress;
                    objMail.Subject = SubjectText;
                    objMail.Body = body;
                    objMail.IsBodyHtml = true;
                    SmtpClient objSmtp = new SmtpClient();
                    objSmtp.Send(objMail);
                }

                if (SendCopyToUserAs == CarrierType.CSBMessage && udc.IsAuthenticated)
                {
                    Message msg = new Message(new SiteContext());
                    msg.FromUserID = CreatorUserId;
                    msg.UserId = udc.UserID;

                    msg.IsRead = false;
                    msg.Subject = SubjectText;
                    msg.MsgText = body;
                    msg.SendNormalMessage(true, false);
                }
                else if (SendCopyToUserAs == CarrierType.EMail && udc.IsAuthenticated)
                {
                    MailMessage objMail = new MailMessage();
                    objMail.From = new MailAddress(senderAddress);
                    objMail.To.Add(new MailAddress(Membership.GetUser(udc.UserID).Email, udc.Nickname));
                    objMail.Subject = SubjectText;
                    objMail.Body = body;
                    objMail.IsBodyHtml = true;
                    SmtpClient objSmtp = new SmtpClient();
                    objSmtp.Send(objMail);
                }
                else if (SendCopyToUserAs == CarrierType.EMail && !udc.IsAuthenticated)
                {
                    MailMessage objMail = new MailMessage();
                    objMail.From = new MailAddress(senderAddress);
                    MailAddress userCopyAddress = string.IsNullOrEmpty(senderName) ? new MailAddress(this.EMail.Text) : new MailAddress(this.EMail.Text, senderName);
                    objMail.To.Add(userCopyAddress);
                    objMail.Subject = SubjectText;
                    objMail.Body = body;
                    objMail.IsBodyHtml = true;
                    SmtpClient objSmtp = new SmtpClient();
                    objSmtp.Send(objMail);
                }

            }
            catch
            {
                blnRet = false;
            }
            finally
            {
                if (udc.IsAuthenticated)
                {
                    if (AdressSave == FormAdressSave.Always || (PnlAdressSave.Visible && CbxAdressSave.Checked))
                        SaveAdressData();
                }
            }
            return blnRet;
        }

        private void Init()
        {
            try
            {
                this.TxtText.Text = string.Empty;
                this.TxtField0.Text = string.Empty;
                this.TxtField1.Text = string.Empty;
                this.TxtField2.Text = string.Empty;
                this.TxtField3.Text = string.Empty;
                this.TxtField4.Text = string.Empty;
                this.TxtField5.Text = string.Empty;
                this.TxtField6.Text = string.Empty;
                this.TxtField7.Text = string.Empty;
                this.TxtField8.Text = string.Empty;
                this.TxtField9.Text = string.Empty;

                foreach (ListItem item in CblOption.Items)
                {
                    item.Selected = false;
                }

                foreach (ListItem item in RblOption.Items)
                {
                    item.Selected = false;
                }

                this.Sex.SelectedIndex = -1;
                this.Name.Text = string.Empty;
                this.Vorname.Text = string.Empty;
                this.AddressStreet.Text = string.Empty;
                this.AddressZip.Text = string.Empty;
                this.AddressCity.Text = string.Empty;
                this.EMail.Text = string.Empty;
                this.Phone.Text = string.Empty;
                this.Mobile.Text = string.Empty;
                this.Comment.Text = string.Empty;
            }
            catch
            {
                // do nothing
            }

        }

        protected void lbtnSend_Click(object sender, EventArgs e)
        {
            if (PnlCheck.Visible)
            {
                this.CCCheck.ValidateCaptcha(this.FormShieldTextBox.Text);
                if (!this.CCCheck.UserValidated)
                {
                    LitCheck.Visible = true;
                    return;
                }
            }

            Send();
            Init();
            pnlResult.Visible = false;
            LitUserFeedback.Visible = true;
            LitUserFeedback.Text = language.GetString("MessageFormSent");
            LitCheck.Visible = false;
        }

    }
}