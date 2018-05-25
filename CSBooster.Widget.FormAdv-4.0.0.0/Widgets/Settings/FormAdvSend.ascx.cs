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

namespace _4screen.CSB.Widget.Settings
{
    public partial class FormAdvSend : System.Web.UI.UserControl, IWidgetSettings
    {
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
            XmlDocument xmlDocument = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);

            UserDataContext udc = UserDataContext.GetUserDataContext();
            TxtSubject.Text = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "TxtSubject", "Formular");
            TxtVor.Text = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "TxtVor", string.Empty);
            TxtNach.Text = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "TxtNach", string.Empty);
            TxtEmail.Text = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "TxtEmail", string.Empty);
            TxtNickname.Text = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "TxtNickname", udc.Nickname);
            Helper.Ddl_SelectItem(DdlUserCopy, XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DdlUserCopy", 1));
            Helper.Rbl_SelectItem(RblSendAs, XmlHelper.GetElementValue(xmlDocument.DocumentElement, "RblSendAs", 1));
            string userID = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DdlUser", udc.UserID.ToString());

            FillUsers(userID, udc);

        }

        private void FillUsers(string userId, UserDataContext udc)
        {
            DdlUser.Items.Clear();
            DdlUser.Visible = false;
            TxtNickname.Visible = false;
            TxtEmail.Visible = false;

            LitUser.Visible = false;
            LitNickname.Visible = false;
            LitEmail.Visible = false;

            LitOr.Visible = false;

            if (udc.IsAdmin)
            {
                if (RblSendAs.SelectedItem.Value == "1")  // Email
                {
                    TxtNickname.Visible = true;
                    LitOr.Visible = true;  
                    TxtEmail.Visible = true;
                }
                else
                {
                    TxtNickname.Visible = true;
                    TxtEmail.Visible = false;
                }
            }
            else
            {
                if (this.ParentDataObject.ObjectType == 1) //Community
                {
                    DdlUser.Items.Clear();
                    QuickParametersUser paras = new QuickParametersUser();
                    paras.IgnoreCache = false;
                    paras.DisablePaging = true; 
                    paras.Amount = 1000;
                    paras.Udc = udc;
                    paras.ObjectType = 2; //
                    paras.MembershipParams = new MembershipParams { CommunityID = this.ParentDataObject.CommunityID, IsOwner = true, IsCreator = true };
                    DataObjectList<DataObjectUser> users = DataObjects.Load<DataObjectUser>(paras);
                    bool missing = true;
                    foreach (DataObjectUser user in users)
                    {
                        if (user.ObjectID.Value == udc.UserID)
                            missing = false;

                        DdlUser.Items.Add(new ListItem(user.Nickname, user.ObjectID.Value.ToString()));      
                    }
                    if (DdlUser.Items.Count == 0 || missing)
                    {
                        DdlUser.Items.Add(new ListItem(udc.Nickname, udc.UserID.ToString()));      
                    }
                    Helper.Ddl_SelectItem(DdlUser, userId);
                    if (DdlUser.SelectedIndex < 0) 
                        DdlUser.SelectedIndex = 0;

                    DdlUser.Visible = true; 
                }
                else if (this.ParentDataObject.ObjectType == 20) //Page
                {
                    if (RblSendAs.SelectedItem.Value == "1")  // Email
                    {
                        TxtNickname.Visible = true;
                        TxtEmail.Visible = true;
                    }
                    else
                    {
                        TxtNickname.Visible = true;
                        TxtEmail.Visible = false;
                    }
                }
                else if (this.ParentDataObject.ObjectType == 19) //ProfileCommunity
                {
                    TxtNickname.Visible = true;
                    TxtNickname.Text = udc.Nickname;
                    TxtNickname.Enabled = false;
                }
                else
                {
                    TxtNickname.Visible = true;
                    TxtNickname.Text = udc.Nickname;
                    TxtNickname.Enabled = false;
                }
            }

            LitNickname.Visible = TxtNickname.Visible;  
            LitEmail.Visible = TxtEmail.Visible;
            LitUser.Visible = DdlUser.Visible;   
        }

        public bool Save()
        {
            try
            {
                XmlDocument xmlDocument = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);

                if (xmlDocument.DocumentElement == null)
                    XmlHelper.CreateRoot(xmlDocument, "root");

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "TxtSubject", TxtSubject.Text);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "TxtVor", TxtVor.Text);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "TxtNach", TxtNach.Text);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "TxtEmail", TxtEmail.Text);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "TxtNickname", TxtNickname.Text);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "DdlUserCopy", DdlUserCopy.SelectedItem.Value);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "RblSendAs", RblSendAs.SelectedItem.Value);

                if (DdlUser.SelectedIndex > -1)
                    XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "DdlUser", DdlUser.SelectedItem.Value);
                else
                {
                    UserDataContext udc = UserDataContext.GetUserDataContext();
                    if (!string.IsNullOrEmpty(TxtNickname.Text) && TxtNickname.Text.ToLower() != udc.Nickname.ToLower())
                    {
                        UserDataContext test = UserDataContext.GetUserDataContext(TxtNickname.Text);
                        if (test.IsAuthenticated)
                        {
                            XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "DdlUser", test.UserID.ToString());
                        }
                        else
                        {
                            LitMsg.Visible = true;
                            return false;
                        }
                    }
                    else
                    {
                        XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "DdlUser", string.Empty);
                    }
                }

                return _4screen.CSB.DataAccess.Business.Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
            }
            catch
            {
                return false;
            }
        }

        protected void RblSendAs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}