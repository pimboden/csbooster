// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Text;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.WebUI.Code;
using _4screen.CSB.WebUI.UserControls.Templates;
using _4screen.Utils.Web;
using Telerik.Web.UI;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class ProfileBasisInfo : ProfileQuestionsControl
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");
        protected GuiLanguage languageProfile = GuiLanguage.GetGuiLanguage("ProfileData");

        DataObjectUser user = null;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            LoadAnswers();

            this.PnlPicCol.Visible = CustomizationSection.CachedInstance.Profile["PictureColors"].Enabled;
            this.PnlPic.Visible = CustomizationSection.CachedInstance.Profile["Picture"].Enabled;
            this.PnlCol.Visible = CustomizationSection.CachedInstance.Profile["Colors"].Enabled;
            this.PnlNam.Visible = CustomizationSection.CachedInstance.Profile["Name"].Enabled;
            this.PnlSN.Visible = CustomizationSection.CachedInstance.Profile["Surname"].Enabled;
            this.PnlLN.Visible = CustomizationSection.CachedInstance.Profile["Lastname"].Enabled;
            this.PnlGenBir.Visible = CustomizationSection.CachedInstance.Profile["GenderBirthday"].Enabled;
            this.PnlGen.Visible = CustomizationSection.CachedInstance.Profile["Gender"].Enabled;
            this.PnlBir.Visible = CustomizationSection.CachedInstance.Profile["Birthday"].Enabled;
            this.PnlAdr.Visible = CustomizationSection.CachedInstance.Profile["UserLocation"].Enabled;
            this.PnlAdr2.Visible = CustomizationSection.CachedInstance.Profile["Address"].Enabled;
            this.PnlCou.Visible = CustomizationSection.CachedInstance.Profile["Country"].Enabled;
            this.PnlLang.Visible = CustomizationSection.CachedInstance.Profile["Language"].Enabled;
            this.PnlPer.Visible = CustomizationSection.CachedInstance.Profile["Personal"].Enabled;
            this.PnlRel.Visible = CustomizationSection.CachedInstance.Profile["PersonalRelationship"].Enabled;
            this.PnlAtr.Visible = CustomizationSection.CachedInstance.Profile["PersonalAttractedTo"].Enabled;
            this.PnlEye.Visible = CustomizationSection.CachedInstance.Profile["PersonalEyeColor"].Enabled;
            this.PnlHai.Visible = CustomizationSection.CachedInstance.Profile["PersonalHairColor"].Enabled;
            this.PnlHei.Visible = CustomizationSection.CachedInstance.Profile["PersonalBodyHeight"].Enabled;
            this.PnlWei.Visible = CustomizationSection.CachedInstance.Profile["PersonalBodyWeight"].Enabled;
        }

        private void FillLayout(string primaryColor, string secondaryColor)
        {
            txtPC.Text = primaryColor;
            txtSC.Text = secondaryColor;

            int intPri = 0;
            StringBuilder strPri = new StringBuilder(500);
            int intSec = 0;
            StringBuilder strSec = new StringBuilder(500);

            strPri.Append("<table border='0' cellpadding='0' cellspacing='2'>");
            strSec.Append("<table border='0' cellpadding='0' cellspacing='2'>");
            for (int h = 1; h < 5; h++)
            {
                strPri.Append("<tr>");
                strSec.Append("<tr>");
                for (int v = 1; v < 5; v++)
                {
                    intPri++; intSec++;

                    strPri.Append("<td>");
                    strSec.Append("<td>");

                    if (primaryColor == intPri.ToString())
                        strPri.AppendFormat("<a href=\"javascript:SelectColor('pco', {0})\"><img id='pco{0}' src='/Library/Images/User/sm/sc_selected/{0}.png' width='25px' height='25px' alt='' style='border-width:0px;'/></a>", intPri);
                    else
                        strPri.AppendFormat("<a href=\"javascript:SelectColor('pco', {0})\"><img id='pco{0}' src='/Library/Images/User/sm/sc/{0}.png' width='25px' height='25px' alt='' style='border-width:0px;'/></a>", intPri);

                    if (secondaryColor == intSec.ToString())
                        strSec.AppendFormat("<a href=\"javascript:SelectColor('sco', {0})\"><img id='sco{0}' src='/Library/Images/User/sm/sc_selected/{0}.png' width='25px' height='25px' alt='' style='border-width:0px;'/></a>", intPri);
                    else
                        strSec.AppendFormat("<a href=\"javascript:SelectColor('sco', {0})\"><img id='sco{0}' src='/Library/Images/User/sm/sc/{0}.png' width='25px' height='25px' alt='' style='border-width:0px;'/></a>", intPri);


                    strPri.Append("</td>");
                    strSec.Append("</td>");
                }
                strPri.Append("</tr>");
                strSec.Append("</tr>");
            }
            strPri.Append("</table>");
            strSec.Append("</table>");

            litPC.Text = strPri.ToString();
            litSC.Text = strSec.ToString();

            RegisterJS();
        }

        private void RegisterJS()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine("function SelectColor(t, pco)");
            str.AppendLine("{");
            str.AppendLine(" var uisc = document.getElementsByName(t);");
            str.AppendLine(" for(i = 1; i < 17; i++)");
            str.AppendLine(" {");
            str.AppendLine("  var obj = document.getElementById(t + i);");
            str.AppendLine("  if (i == pco)");
            str.AppendLine("  {");
            str.AppendLine("   for (j = 0; j < uisc.length; j++)");
            str.AppendLine("   {");
            str.AppendLine(@"   uisc[j].src = uisc[j].src.replace(/\d{1,2}\.png/, i+'.png');");
            str.AppendLine("   }");
            str.AppendLine("   obj.src = obj.src.replace('sm/sc/','sm/sc_selected/');");
            str.AppendLine("   if (t == 'pco')");
            str.AppendLine("   {");
            str.AppendLine(String.Format(" document.getElementById('{0}').value = i;", (object)txtPC.ClientID));
            str.AppendLine("   }");
            str.AppendLine("   else");
            str.AppendLine("   {");
            str.AppendLine(String.Format(" document.getElementById('{0}').value = i;", (object)txtSC.ClientID));
            str.AppendLine("   }");
            str.AppendLine("  }");
            str.AppendLine("  else");
            str.AppendLine("  {");
            str.AppendLine("   obj.src = obj.src.replace('sm/sc_selected/','sm/sc/');");
            str.AppendLine("  }");
            str.AppendLine(" }");
            str.AppendLine("}");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SelectColor", str.ToString(), true);
        }

        private void LoadAnswers()
        {
            try
            {
                user = DataObject.Load<DataObjectUser>(UserProfile.Current.UserId, null, true);

                cbxName.Checked = user.VornameShow;
                FillTextboxAnswer(txtVorname, user.Vorname, AnswerType.String);
                FillTextboxAnswer(txtName, user.Name, AnswerType.String);

                FillDropDownListAnswer(rblGender, user.Sex, FillListBy.Value);
                cbxGeb.Checked = user.BirthdayShow;
                FillRadDatePickerAnswer((RadDatePicker)datBirthday, user.Birthday);

                cbxStr.Checked = user.AddressStreetShow;
                FillTextboxAnswer(txtStreet, user.AddressStreet, AnswerType.String);
                FillTextboxAnswer(txtZip, user.AddressZip, AnswerType.String);
                FillTextboxAnswer(txtCity, user.AddressCity, AnswerType.String);
                FillDropDownListAnswer(ddlLand, user.AddressLand, FillListBy.Value);
                FillDropDownListAnswer(ddlLanguages, user.Languages, FillListBy.Value);

                LitStatus.Text = ProfileDataHelper.GetProfileDataTitle(string.Empty, UserProfileDataKey.Status);
                ProfileDataHelper.FillProfileDataList(string.Empty, DdlStatus, UserProfileDataKey.Status, false);
                CbPersonalShow.Checked = user.RelationStatusShow;
                FillDropDownListAnswer(DdlStatus, user.RelationStatus, FillListBy.Value);
                LitAttractedTo.Text = ProfileDataHelper.GetProfileDataTitle(string.Empty, UserProfileDataKey.AttractedTo);
                ProfileDataHelper.FillProfileDataList(string.Empty, DdlAttractedTo, UserProfileDataKey.AttractedTo, false);
                FillDropDownListAnswer(DdlAttractedTo, user.AttractedTo, FillListBy.Value);
                LitEyeColor.Text = ProfileDataHelper.GetProfileDataTitle(string.Empty, UserProfileDataKey.EyeColor);
                ProfileDataHelper.FillProfileDataList(string.Empty, DdlEyeColor, UserProfileDataKey.EyeColor, false);
                FillDropDownListAnswer(DdlEyeColor, user.EyeColor, FillListBy.Value);
                LitHairColor.Text = ProfileDataHelper.GetProfileDataTitle(string.Empty, UserProfileDataKey.HairColor);
                ProfileDataHelper.FillProfileDataList(string.Empty, DdlHairColor, UserProfileDataKey.HairColor, false);
                FillDropDownListAnswer(DdlHairColor, user.HairColor, FillListBy.Value);
                LitBodyHeight.Text = ProfileDataHelper.GetProfileDataTitle(string.Empty, UserProfileDataKey.BodyHeight);
                if (user.BodyHeight != 0)
                    FillTextboxAnswer(TxtBodyHeight, user.BodyHeight, AnswerType.Int);
                LitBodyWeight.Text = ProfileDataHelper.GetProfileDataTitle(string.Empty, UserProfileDataKey.BodyWeight);
                if (user.BodyWeight != 0)
                    FillTextboxAnswer(TxtBodyWeight, user.BodyWeight, AnswerType.Int);

                Control ctrl = this.LoadControl("/UserControls/Templates/SmallOutputUser2.ascx");
                ((SmallOutputUser2)ctrl).DataObjectUser = user;
                ((SmallOutputUser2)ctrl).HasElementName = true;
                phU.Controls.Clear();
                phU.Controls.Add(ctrl);

                if (!string.IsNullOrEmpty(user.FacebookUserId))
                {
                    btnAddImage.Visible = false;
                    lbtnDelete.Visible = false;
                }
                else if (string.IsNullOrEmpty(UserProfile.Current.UserPictureURL) || UserProfile.Current.UserPictureURL == Helper.GetObjectType("User").DefaultImageURL)
                {
                    btnAddImage.NavigateUrl = string.Format("javascript:radWinOpen('/Pages/popups/SinglePictureUpload.aspx?OID={0}&OT={1}&ReturnURL={2}', 'Profilbild hochladen', 400, 100, false, null)", UserProfile.Current.UserId.ToString(), (int)Helper.GetObjectTypeNumericID("User"), System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(Helper.GetDashboardLink(Common.Dashboard.SettingsBasic))));
                    btnAddImage.Visible = true;
                    lbtnDelete.Visible = false;
                }
                else
                {
                    btnAddImage.Visible = false;
                    lbtnDelete.Visible = true;
                }
            }
            catch
            {
            }
            finally
            {
                FillLayout(user.PrimaryColor, user.SecondaryColor);
            }
        }


        public void Save()
        {
            user = DataObject.Load<DataObjectUser>(UserProfile.Current.UserId, null, true);

            user.Name = txtName.Text;
            user.NameShow = cbxName.Checked;
            user.Vorname = txtVorname.Text;
            user.VornameShow = cbxName.Checked;

            user.Sex = GetComaSeparatetdValues(rblGender.Items);
            user.SexShow = cbxGeb.Checked;

            if (datBirthday.SelectedDate != null)
                user.Birthday = datBirthday.SelectedDate.Value;
            else
                user.Birthday = null;
            user.BirthdayShow = cbxGeb.Checked;

            user.AddressStreet = txtStreet.Text;
            user.AddressStreetShow = cbxStr.Checked;
            user.AddressZip = txtZip.Text;
            user.AddressZipShow = cbxStr.Checked;
            user.AddressCity = txtCity.Text;
            user.AddressCityShow = cbxStr.Checked;
            user.AddressLand = GetComaSeparatetdValues(ddlLand.Items);
            user.AddressLandShow = cbxStr.Checked;
            user.Languages = GetComaSeparatetdValues(ddlLanguages.Items);

            user.RelationStatus = GetComaSeparatetdValues(DdlStatus.Items);
            user.RelationStatusShow = CbPersonalShow.Checked;
            user.AttractedTo = GetComaSeparatetdValues(DdlAttractedTo.Items);
            user.AttractedToShow = CbPersonalShow.Checked;
            user.EyeColor = GetComaSeparatetdValues(DdlEyeColor.Items);
            user.EyeColorShow = CbPersonalShow.Checked;
            user.HairColor = GetComaSeparatetdValues(DdlHairColor.Items);
            user.HairColorShow = CbPersonalShow.Checked;

            int height, weight = 0;
            if (int.TryParse(TxtBodyHeight.Text, out height))
                user.BodyHeight = height;
            else
                user.BodyHeight = 0;
            user.BodyHeightShow = CbPersonalShow.Checked;
            if (int.TryParse(TxtBodyWeight.Text, out weight))
                user.BodyWeight = weight;
            else
                user.BodyWeight = 0;
            user.BodyWeightShow = CbPersonalShow.Checked;

            if (txtPC.Text.Length > 0)
            {
                user.PrimaryColor = txtPC.Text;
            }
            if (txtSC.Text.Length > 0)
            {
                user.SecondaryColor = txtSC.Text;
            }

            if (cbxStr.Checked)
            {
                user.Zip = txtZip.Text;
                user.City = txtCity.Text;
                if (ddlLand.SelectedValue != "Andere")
                    user.CountryCode = GetComaSeparatetdValues(ddlLand.Items).Replace(";", string.Empty);
                else
                    user.CountryCode = string.Empty;
            }
            else
            {
                user.Zip = string.Empty;
                user.City = string.Empty;
                user.CountryCode = string.Empty;
            }

            user.Update(UserDataContext.GetUserDataContext());

            UserDataContext udc = UserDataContext.GetUserDataContext();
            if (!(string.IsNullOrEmpty(txtName.Text) && string.IsNullOrEmpty(txtVorname.Text)))
                _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent("PROFILE_USER_NAME", udc);

            if (DdlStatus.SelectedIndex > 0 && datBirthday.SelectedDate != null)
                _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent("PROFILE_USER_BASICS", udc);

            if (!(string.IsNullOrEmpty(txtStreet.Text) && string.IsNullOrEmpty(txtZip.Text) && string.IsNullOrEmpty(txtCity.Text)))
                _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent("PROFILE_USER_ADDRESS", udc);

            LoadAnswers();

            txtPC.Text = "";
            txtSC.Text = "";
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            UserProfile.Current.UserPictureURL = string.Format("{0}", Helper.GetObjectType("User").DefaultImageURL);
            UserProfile.Current.Save();

            lbtnDelete.Visible = false;
            btnAddImage.Visible = true;
            user.Image = string.Empty;
            user.Update(UserDataContext.GetUserDataContext());

            LoadAnswers();
        }
    }
}