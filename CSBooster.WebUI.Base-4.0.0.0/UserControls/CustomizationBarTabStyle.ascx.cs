//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		25.09.2007 / PI
//  Updated:   
//******************************************************************************
using System;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class CustomizationBarTabStyle : System.Web.UI.UserControl
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        private StyleSection _section;
        private string previewElementId;
        private Community objCty;

        public Guid CommunityID { get; set; }

        public string PreviewElementId
        {
            get { return previewElementId; }
            set { previewElementId = value; }
        }

        public StyleSection Section
        {
            get { return _section; }
            set { _section = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.RbColor.Text = language.GetString("LableStyleColor");
            this.RbImage.Text = language.GetString("LableStylePicture");

            if (_section == StyleSection.Footer || _section == StyleSection.Header)
            {
                this.PnlFooterHeight.Visible = true;
            }

            string strJS = string.Format(@"
function StyleSelectPicture_{0}(sender, arg)
{{ 
   image = document.getElementById('{1}');
   tbImage = document.getElementById('{2}');
   previewElement = document.getElementById('{3}');
	if (arg)
	{{
	   image.src = arg;
      image.style.display = 'inline';
      tbImage.value = arg;
      
      var imageLarge = arg.replace(/BG\/S/i, ""BG/L"");
      previewElement.style.backgroundImage = ""url(""+imageLarge+"")"";
	}}
}}
function clientPicked_{0}(sender, eventArgs)
{{
   if(sender != undefined)
   {{
		color = eventArgs.get_color();
      document.getElementById('{4}').value = color;
		StyleColorChange('{3}', '{5}', '{4}');
   }}
   else
		color = 'Keine Farbe ausgewählt';
}}
", this.ClientID, Image.ClientID, TxtImage.ClientID, this.previewElementId, TxtColor.ClientID, PnlColorBox.ClientID);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), string.Format("StyleSelectPicture_{0}", this.ClientID), strJS, true);

            RbColor.GroupName = Section.ToString();
            RbImage.GroupName = Section.ToString();
            RbColor.Attributes.Add("OnClick", string.Format("StyleSwitchMode('{0}', this.id, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')", this.previewElementId, RbColor.ClientID, RbImage.ClientID, TxtColor.ClientID, TxtImage.ClientID, PnlColor.ClientID, PnlImage.ClientID, CbRepeatH.ClientID, CbRepeatV.ClientID));
            RbImage.Attributes.Add("OnClick", string.Format("StyleSwitchMode('{0}', this.id, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')", this.previewElementId, RbColor.ClientID, RbImage.ClientID, TxtColor.ClientID, TxtImage.ClientID, PnlColor.ClientID, PnlImage.ClientID, CbRepeatH.ClientID, CbRepeatV.ClientID));
            CbRepeatH.Attributes.Add("OnClick", string.Format("StyleSetRepeating('{0}', '{1}', '{2}')", this.previewElementId, CbRepeatH.ClientID, CbRepeatV.ClientID));
            CbRepeatV.Attributes.Add("OnClick", string.Format("StyleSetRepeating('{0}', '{1}', '{2}')", this.previewElementId, CbRepeatH.ClientID, CbRepeatV.ClientID));
            TxtColor.Attributes.Add("OnChange", string.Format("StyleColorChange('{0}', '{1}', '{2}')", this.previewElementId, PnlColorBox.ClientID, TxtColor.ClientID));
            rcp.OnClientColorPreview = string.Format("clientPicked_{0}", this.ClientID);
            TxtFooterHeight.Attributes.Add("OnKeyUp", string.Format("StyleSetFooterHeight('{0}', '{1}')", this.previewElementId, TxtFooterHeight.ClientID));

            objCty = new Community(CommunityID);
            if (objCty.LoadSucces)
            {
                string queryString = objCty.ProfileOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("Community") ? "CN=" + objCty.CommunityId : "UI=" + objCty.UserId;
                btnAddImage.NavigateUrl = string.Format("javascript:radWinOpen('/Pages/popups/SimplePictureUploadGallery.aspx?{0}', '{1}', 630, 460, false, 'StyleSelectPicture_{2}')", queryString, language.GetString("TitleStyleSelectPicture").StripForScript(), this.ClientID);

                LoadData();
            }
            else
            {
                throw new Exception("Community wurde nicht gefunden");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        private void LoadData()
        {
            InitData();
            if (Section == StyleSection.Header)
            {
                LoadData(objCty.HeaderStyle);
            }
            else if (Section == StyleSection.Body)
            {
                LoadData(objCty.BodyStyle);
            }
            else if (Section == StyleSection.Footer)
            {
                LoadData(objCty.FooterStyle);
            }
        }

        private void LoadData(string strSectionStyle)
        {
            if (string.IsNullOrEmpty(strSectionStyle) || strSectionStyle.Length == 0)
            {
                RbColor.Checked = true;
                RbImage.Checked = false;
                CbRepeatV.Checked = false;
                CbRepeatH.Checked = false;
                PnlColor.Style.Remove("display");
                PnlColor.Style.Add("display", "inline");
                TxtColor.Text = language.GetString("MessageStyleNoColor");
                TxtImage.Text = string.Empty;
                PnlColorBox.Attributes.Remove("style");
                PnlColorBox.Attributes.Add("style", "background-color:transparent;");
            }
            else
            {
                if (strSectionStyle.IndexOf("background-color") > -1)
                {
                    RbColor.Checked = true;
                    RbImage.Checked = false;
                    CbRepeatV.Checked = false;
                    CbRepeatH.Checked = false;
                    PnlColor.Style.Remove("display");
                    PnlColor.Style.Add("display", "inline");
                    string[] strStyle = strSectionStyle.Split(';');
                    TxtColor.Text = strStyle[0].Substring(strStyle[0].IndexOf(":") + 1);
                    rcp.SelectedColor = System.Drawing.ColorTranslator.FromHtml(TxtColor.Text);
                    PnlColorBox.Attributes.Remove("style");
                    PnlColorBox.Attributes.Add("style", String.Format("background-color:{0};", (object) TxtColor.Text));
                }
                else
                {
                    RbColor.Checked = false;
                    RbImage.Checked = true;
                    PnlImage.Style.Remove("display");
                    PnlImage.Style.Add("display", "inline");
                    Image.Style.Remove("display");
                    Image.Style.Add("display", "inline");
                    string[] bgImgStyle = strSectionStyle.Split(';');
                    string strImageUrl = bgImgStyle[0].Substring(bgImgStyle[0].IndexOf("url(") + 5);
                    strImageUrl = strImageUrl.Substring(0, strImageUrl.LastIndexOf("'"));
                    Image.ImageUrl = strImageUrl.Replace("BG/L", "BG/S");
                    TxtImage.Text = Image.ImageUrl;
                    string strRepeatType = bgImgStyle[1].Substring(18);
                    if (strRepeatType == "no-repeat")
                    {
                        CbRepeatH.Checked = false;
                        CbRepeatV.Checked = false;
                    }
                    else if (strRepeatType == "repeat")
                    {
                        CbRepeatH.Checked = true;
                        CbRepeatV.Checked = true;
                    }
                    else if (strRepeatType == "repeat-y")
                    {
                        CbRepeatH.Checked = false;
                        CbRepeatV.Checked = true;
                    }
                    else if (strRepeatType == "repeat-x")
                    {
                        CbRepeatH.Checked = true;
                        CbRepeatV.Checked = false;
                    }
                    if (bgImgStyle.Length == 4)
                    {
                        try
                        {
                            string footerHeight = bgImgStyle[2].Substring(7, bgImgStyle[2].Length - 9);
                            TxtFooterHeight.Text = footerHeight;
                        }
                        catch { }
                    }
                }
            }
        }
        private void InitData()
        {
            RbColor.Checked = false;
            RbImage.Checked = false;
            CbRepeatV.Checked = false;
            CbRepeatH.Checked = false;
            TxtColor.Text = language.GetString("MessageStyleNoColor");
            PnlColor.Style.Add("display", "none");
            PnlImage.Style.Add("display", "none");
            Image.Style.Add("display", "none");
            Image.ImageUrl = string.Empty;
        }

        public void Reset()
        {
            DataObject dataObject = DataObject.Load<DataObject>(CommunityID);
            if ((dataObject.GetUserAccess(UserDataContext.GetUserDataContext()) & ObjectAccessRight.Update) != ObjectAccessRight.Update)
                throw new Exception("Access rights missing");

            HitblCommunityCty comm = HitblCommunityCty.FetchByID(CommunityID);
            if (Section == StyleSection.Header)
            {
                comm.CtyHeaderStyle = string.Empty;
            }
            else if (Section == StyleSection.Body)
            {
                comm.CtyBodyStyle = string.Empty;
            }
            else if (Section == StyleSection.Footer)
            {
                comm.CtyFooterStyle = string.Empty;
            }
            comm.Save();
        }

        public void Save()
        {
            DataObject dataObject = DataObject.Load<DataObject>(CommunityID);

            if ((dataObject.GetUserAccess(UserDataContext.GetUserDataContext()) & ObjectAccessRight.Update) != ObjectAccessRight.Update)
                throw new Exception("Access rights missing");

            string strStyle = string.Empty;
            if (RbColor.Checked)
            {
                if (TxtColor.Text.Trim().Length > 0 && TxtColor.Text.Trim() != language.GetString("MessageStyleNoColor"))
                {
                    try
                    {
                        System.Drawing.Color clr = System.Drawing.ColorTranslator.FromHtml(TxtColor.Text);
                        strStyle = String.Format("background-color:{0};background-image: none;", (object) TxtColor.Text);
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                //Image set
                if (TxtImage.Text.Length > 0)
                {
                    int footerHeight = 20;
                    try { footerHeight = int.Parse(TxtFooterHeight.Text); }
                    catch { }
                    strStyle = String.Format("background-image:url('{0}');", (object) TxtImage.Text.Replace("BG/S", "BG/L"));
                    if (CbRepeatV.Checked && CbRepeatH.Checked)
                    {
                        strStyle += "background-repeat:repeat;";
                    }
                    else if (!CbRepeatV.Checked && !CbRepeatH.Checked)
                    {
                        strStyle += "background-repeat:no-repeat;";
                    }
                    else if (CbRepeatV.Checked && !CbRepeatH.Checked)
                    {
                        strStyle += "background-repeat:repeat-y;";
                    }
                    else if (!CbRepeatV.Checked && CbRepeatH.Checked)
                    {
                        strStyle += "background-repeat:repeat-x;";
                    }
                    if (footerHeight > 20)
                    {
                        strStyle += "height:" + TxtFooterHeight.Text + "px;";
                    }
                }
            }
            HitblCommunityCty comm = HitblCommunityCty.FetchByID(CommunityID);
            if (Section == StyleSection.Header)
            {
                comm.CtyHeaderStyle = strStyle;
            }
            else if (Section == StyleSection.Body)
            {
                comm.CtyBodyStyle = strStyle;
            }
            else if (Section == StyleSection.Footer)
            {
                comm.CtyFooterStyle = strStyle;
            }
            comm.Save();
            objCty = new Community(CommunityID);
            LoadData();
        }
    }
}