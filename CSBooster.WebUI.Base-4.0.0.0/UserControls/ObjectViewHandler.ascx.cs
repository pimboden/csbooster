using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using Telerik.Web.UI;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class ObjectViewHandler : System.Web.UI.UserControl, IViewHandler
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");
        private bool visiblilityEnabled;
        private bool rolesEnabled;
        private bool showStateEnabled;
        private bool managedEnabled;

        public DataObject DataObject { get; set; }
        public DataObject ParentDataObject { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (DataObject != null && DataObject.ObjectType == Helper.GetObjectTypeNumericID("Page"))
            {
                EnableRoles();
                EnableShowState(DataObject.ShowState);
            }
            else if (DataObject != null && DataObject.ObjectType == Helper.GetObjectTypeNumericID("Community"))
            {
                EnableManaged();
                EnableShowState(DataObject.ShowState);
                EnableVisibility(DataObject.Status, DataObject.FriendVisibility);
            }
            else if (DataObject != null)
            {
                if (ParentDataObject != null && ParentDataObject.ObjectType == Helper.GetObjectTypeNumericID("Community"))
                {
                    bool isMember;
                    bool isOwner = Community.GetIsUserOwner(UserProfile.Current.UserId, ParentDataObject.ObjectID.Value, out isMember);
                    DataObjectCommunity doCommunity = DataObject.Load<DataObjectCommunity>(DataObject.ObjectID, null, true);
                    if (!doCommunity.Managed || isOwner)
                        EnableShowState(DataObject.ShowState);
                    else
                        lblInfo.Text = language.GetString("MessageVisibilityTransmit");
                }
                else
                {
                    EnableShowState(DataObject.ShowState);
                    EnableVisibility(DataObject.Status, DataObject.FriendVisibility);
                }
            }
            else
            {
                EnableShowState(ObjectShowState.Published);
                EnableVisibility(ObjectStatus.Public, 0);
            }

            pnlView.Visible = visiblilityEnabled || managedEnabled || rolesEnabled || showStateEnabled;
        }

        private void EnableShowState(ObjectShowState objectShowState)
        {
            showStateEnabled = true;
            TRShowState.Visible = true;
            rdST.Items.Add(new RadComboBoxItem(ObjectShowState.Draft.GetText(), string.Format("{0}", (int)ObjectShowState.Draft)));
            rdST.Items.Add(new RadComboBoxItem(ObjectShowState.Published.GetText(), string.Format("{0}", (int)ObjectShowState.Published)));
            rdST.SelectedIndex = objectShowState == ObjectShowState.Published ? 1 : 0;
            rdST.Enabled = true;
        }

        private void EnableRoles()
        {
            rolesEnabled = true;
            TRRoles.Visible = true;
            rcRoles.Items.Clear();
            rcRoles.ZIndex = 99999;
            rcRoles.Height = Unit.Pixel(120);
            rcRoles.AllowCustomText = true;
            rcRoles.Text = string.Empty;
            rcRoles.Attributes.Add("OnChange", "setComboboxText('" + rcRoles.ClientID + "');stopPropagation(event);");
            foreach (string key in DataObject.RoleRight.Keys)
            {
                RadComboBoxItem rcItem = new RadComboBoxItem(key, key);
                CheckBox checkbox = new CheckBox();
                checkbox.ID = "CheckBox";
                checkbox.Text = key;
                checkbox.Attributes.Add("onClick", "setComboboxText('" + rcRoles.ClientID + "');stopPropagation(event);");
                rcItem.Controls.Add(checkbox);
                checkbox.Checked = DataObject.RoleRight[key];
                if (checkbox.Checked)
                {
                    rcRoles.Text += rcItem.Text + ",";
                }
                rcRoles.Items.Add(rcItem);
            }
            rcRoles.Text = rcRoles.Text.TrimEnd(new char[] { ',' });
        }

        private void EnableManaged()
        {
            managedEnabled = true;
            TRManaged.Visible = true;
            rcMng.Items.Clear();
            rcMng.Items.Add(new RadComboBoxItem(language.GetString("LableVisibilityManaged0"), "0"));
            rcMng.Items.Add(new RadComboBoxItem(language.GetString("LableVisibilityManaged1"), "1"));
        }

        private void EnableVisibility(ObjectStatus status, FriendType friend)
        {
            visiblilityEnabled = true;
            TRVisibility.Visible = true;

            RbtPublic.Text = language.GetString("LableVisibilityVisibility0");
            RbtPublic.Checked = status == ObjectStatus.Public;
            RbtPrivat.Text = language.GetString("LableVisibilityVisibility1");
            RbtPrivat.Checked = status == ObjectStatus.Private;
            RbtUnlisted.Text = language.GetString("LableVisibilityVisibility2");
            RbtUnlisted.Checked = status == ObjectStatus.Unlisted;

            CbxFriends.Items.Clear();
            foreach (int i in Enum.GetValues(typeof(FriendType)))
            {
                string key = string.Format("TextFriendType{0}", i);
                string text = language.GetString(key);
                if (!string.IsNullOrEmpty(text))
                {
                    ListItem listItem = new ListItem(text, i.ToString());
                    FriendType check = (FriendType)i;
                    if ((friend & check) == check)
                        listItem.Selected = true;
                    CbxFriends.Items.Add(listItem);
                }
            }
        }

        public ObjectStatus GetObjectStatus()
        {
            ObjectStatus objectStatus = ObjectStatus.Private;
            if (visiblilityEnabled)
            {
                if (RbtPublic.Checked)
                    objectStatus = ObjectStatus.Public;
                else if (RbtPrivat.Checked)
                    objectStatus = ObjectStatus.Private;
                else if (RbtUnlisted.Checked)
                    objectStatus = ObjectStatus.Unlisted;
            }
            else
            {
                if (ParentDataObject != null && ParentDataObject.ObjectType == Helper.GetObjectTypeNumericID("Page"))
                    objectStatus = ObjectStatus.Public;
                else if (ParentDataObject != null && ParentDataObject.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
                    objectStatus = ObjectStatus.Public;
                else if (ParentDataObject != null && ParentDataObject.ObjectType == Helper.GetObjectTypeNumericID("Community"))
                    objectStatus = DataObject.Load<DataObjectCommunity>(ParentDataObject.CommunityID, null, true).Status;
                else
                    objectStatus = ObjectStatus.Public;
            }
            return objectStatus;
        }

        public bool IsManaged()
        {
            bool managed = false;

            if (managedEnabled)
            {
                if (!string.IsNullOrEmpty(rcMng.SelectedValue) && rcMng.SelectedValue == "1")
                    managed = true;
            }
            else if (ParentDataObject != null && ParentDataObject.ObjectType == Helper.GetObjectTypeNumericID("Community"))
            {
                Community community = new Community(ParentDataObject.CommunityID.Value);
                managed = ((DataObjectCommunity)community.ProfileOrCommunity).Managed && !community.IsUserOwner;
            }
            if (showStateEnabled)
            {
                if (rdST.SelectedValue != ((int)ObjectShowState.Published).ToString())
                    managed = true;
            }
            return managed;
        }

        public string GetRoles()
        {
            string roles = string.Empty;
            if (rolesEnabled)
            {
                for (int i = 0; i < rcRoles.Items.Count; i++)
                {
                    string checkboxId = rcRoles.UniqueID + "$i" + i + "$Checkbox";
                    if (Request.Form[checkboxId] != null)
                    {
                        roles += rcRoles.Items[i].Text + Constants.TAG_DELIMITER;
                    }
                }
                roles = roles.TrimEnd(Constants.TAG_DELIMITER);
            }
            return roles;
        }

        public FriendType GetFriendType()
        {
            FriendType friendVisibility = 0;
            if (visiblilityEnabled)
            {
                foreach (ListItem item in CbxFriends.Items)
                {
                    if (item.Selected)
                    {
                        friendVisibility |= (FriendType)Convert.ToInt32(item.Value);
                    }
                }
            }
            return friendVisibility;
        }
    }
}
