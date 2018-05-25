//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		20.08.2007 / PI
//  Updated:   
//******************************************************************************
using System;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Pages.Popups.UserControls
{
    public partial class ProfileFriendSetting : System.Web.UI.UserControl
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("ProfileData");

        public Guid UserID
        {
            get { return new Guid(ViewState["uid"].ToString()); }
            set { ViewState.Add("uid", value.ToString()); }
        }

        public Guid FriendID
        {
            get { return new Guid(ViewState["fid"].ToString()); }
            set { ViewState.Add("fid", value.ToString()); }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            
            RPB.Items[0].Text = language.GetString("PrivateData");
            RPB.Items[1].Text = language.GetString("CommunicationData");
            RPB.Items[2].Text = language.GetString("OtherData");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void lbS_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CloseWindow", "$telerik.$(function() { CloseWindow(); } );", true);
            }
        }

        public void LoadSetting()
        {
            /*listProfile = new UserProfileDatas();
         listProfile.LoadForFriend(UserProfileDataLoadType.LoadForFriendSetting, UserId, FriendID);
         SetCheckBox(listProfile[UserProfileDataKey.Vorname], "CBA_VORNAME", "CBS_VORNAME");
         SetCheckBox(listProfile[UserProfileDataKey.Name], "CBA_NAME", "CBS_NAME");
         SetCheckBox(listProfile[UserProfileDataKey.Sex], "CBA_SEX", "CBS_SEX");
         SetCheckBox(listProfile[UserProfileDataKey.Birthday], "CBA_GEB", "CBS_GEB");
         SetCheckBox(listProfile[UserProfileDataKey.City], "CBA_ORT", "CBS_ORT");
         SetCheckBox(listProfile[UserProfileDataKey.Land], "CBA_LAND", "CBS_LAND");
         SetCheckBox(listProfile[UserProfileDataKey.Languages], "CBA_LANG", "CBS_LANG");
         SetCheckBox(listProfile[UserProfileDataKey.Status], "CBA_REL", "CBS_REL");
         SetCheckBox(listProfile[UserProfileDataKey.AttractedTo], "CBA_NEIG", "CBS_NEIG");
         SetCheckBox(listProfile[UserProfileDataKey.EyeColor], "CBA_EYE", "CBS_EYE");
         SetCheckBox(listProfile[UserProfileDataKey.HairColor], "CBA_HAIR", "CBS_HAIR");
         SetCheckBox(listProfile[UserProfileDataKey.BodyHeight], "CBA_GRO", "CBS_GRO");
         SetCheckBox(listProfile[UserProfileDataKey.BodyWeight], "CBA_GEW", "CBS_GEW");
         SetCheckBox(listProfile[UserProfileDataKey.Mobile], "CBA_HAND", "CBS_HAND");
         SetCheckBox(listProfile[UserProfileDataKey.Phone], "CBA_TEL", "CBS_TEL");
         SetCheckBox(listProfile[UserProfileDataKey.MSN], "CBA_MSN", "CBS_MSN");
         SetCheckBox(listProfile[UserProfileDataKey.Yahoo], "CBA_YAH", "CBS_YAH");
         SetCheckBox(listProfile[UserProfileDataKey.Skype], "CBA_SKY", "CBS_SKY");
         SetCheckBox(listProfile[UserProfileDataKey.ICQ], "CBA_ICQ", "CBS_ICQ");
         SetCheckBox(listProfile[UserProfileDataKey.AIM], "CBA_AIM", "CBS_AIM");
         SetCheckBox(listProfile[UserProfileDataKey.Homepage], "CBA_HOME", "CBS_HOME");
         SetCheckBox(listProfile[UserProfileDataKey.Blog], "CBA_BLOG", "CBS_BLOG");*/
        }

        /*private void SetCheckBox(UserProfileData upd, string cba, string cbs)
      {
         CheckBox cbxCBA = Helper.FindControl(RPB, cba) as CheckBox;
         CheckBox cbxCBS = Helper.FindControl(RPB, cbs) as CheckBox; 

         cbxCBA.Checked = upd.ShowSetting;
         if (upd.ShowSetting)
         {
            cbxCBS.Checked = true;
            cbxCBS.Enabled = false;
         }
         else
         {
            cbxCBS.Checked = upd.ShowFriendSetting;
            cbxCBS.Enabled = true;
         }
      }*/

        /*private void GetCheckBox(UserProfileData upd, string cbs)
      {
         CheckBox cbxCBS = Helper.FindControl(RPB, cbs) as CheckBox;
         if (cbxCBS.Enabled)
            upd.ShowFriendSetting = cbxCBS.Checked;  
      }*/

        public bool Save()
        {
            try
            {
                /*listProfile = new UserProfileDatas();
            listProfile.LoadForFriend(UserProfileDataLoadType.LoadForFriendSetting, UserId, FriendID);
            GetCheckBox(listProfile[UserProfileDataKey.Vorname], "CBS_VORNAME");
            GetCheckBox(listProfile[UserProfileDataKey.Name], "CBS_NAME");
            GetCheckBox(listProfile[UserProfileDataKey.Sex], "CBS_SEX");
            GetCheckBox(listProfile[UserProfileDataKey.Birthday], "CBS_GEB");
            GetCheckBox(listProfile[UserProfileDataKey.City], "CBS_ORT");
            GetCheckBox(listProfile[UserProfileDataKey.Land], "CBS_LAND");
            GetCheckBox(listProfile[UserProfileDataKey.Languages], "CBS_LANG");
            GetCheckBox(listProfile[UserProfileDataKey.Status], "CBS_REL");
            GetCheckBox(listProfile[UserProfileDataKey.AttractedTo], "CBS_NEIG");
            GetCheckBox(listProfile[UserProfileDataKey.EyeColor], "CBS_EYE");
            GetCheckBox(listProfile[UserProfileDataKey.HairColor], "CBS_HAIR");
            GetCheckBox(listProfile[UserProfileDataKey.BodyHeight], "CBS_GRO");
            GetCheckBox(listProfile[UserProfileDataKey.BodyWeight], "CBS_GEW");
            GetCheckBox(listProfile[UserProfileDataKey.Mobile], "CBS_HAND");
            GetCheckBox(listProfile[UserProfileDataKey.Phone], "CBS_TEL");
            GetCheckBox(listProfile[UserProfileDataKey.MSN], "CBS_MSN");
            GetCheckBox(listProfile[UserProfileDataKey.Yahoo], "CBS_YAH");
            GetCheckBox(listProfile[UserProfileDataKey.Skype], "CBS_SKY");
            GetCheckBox(listProfile[UserProfileDataKey.ICQ], "CBS_ICQ");
            GetCheckBox(listProfile[UserProfileDataKey.AIM], "CBS_AIM");
            GetCheckBox(listProfile[UserProfileDataKey.Homepage], "CBS_HOME");
            GetCheckBox(listProfile[UserProfileDataKey.Blog], "CBS_BLOG");
            listProfile.Save();*/
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}