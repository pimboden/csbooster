//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		20.08.2007 / PI
//  Updated:   
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.Common.Notification;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Notification.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Pages.Popups.UserControls
{
    public partial class Notification : System.Web.UI.UserControl
    {
        #region FIELDS

        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base");
        protected GuiLanguage languageAlert = GuiLanguage.GetGuiLanguage("Pages.Admin.WebUI.Base");
        private RegistrationList listRegGlobal;
        private RegistrationList listRegLocal;
        private UserDataContext udc;
        private Guid? currentUserId;
        private string currentNickname;
        private string[] currentUserRoles;
        private string strConfigurationPath;
        private bool blnShowCloseButton = true;
        private bool blnShowSaveButton = true;

        #endregion FIELDS

        #region PROPERTIES

        public Guid? ObjectID { get; set; }

        public Guid? UserID { get; set; }

        public Guid? CommunityID { get; set; }

        public int ObjectType { get; set; }

        public int[] ObjectTypes { get; set; }

        public List<TagWord> TagWords { get; set; }

        public bool ShowCloseButton
        {
            get { return blnShowCloseButton; }
            set { blnShowCloseButton = value; }
        }

        public bool ShowSaveButton
        {
            get { return blnShowSaveButton; }
            set { blnShowSaveButton = value; }
        }

        #endregion PROPERTIES

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            udc = UserDataContext.GetUserDataContext();
            strConfigurationPath = Server.MapPath("/");

            currentUserId = UserProfile.Current.UserId;
            currentNickname = UserProfile.Current.Nickname;
            currentUserRoles = Roles.GetRolesForUser();

            listRegLocal = new RegistrationList(udc, strConfigurationPath);
            if (ObjectID.HasValue)
                listRegLocal.Load(true, ObjectID, null, null, ObjectType, null, null, currentUserId.Value, false);
            if (listRegLocal.Count > 0)
            {
                pnlLocal.Visible = true;
                printLocal();
            }
            else
            {
                pnlLocal.Visible = false;
            }
            listRegGlobal = new RegistrationList(udc, strConfigurationPath);
            listRegGlobal.Load(true, ObjectID, UserID, CommunityID, ObjectType, ObjectTypes, TagWords, currentUserId.Value, true);
            if (listRegGlobal.Count > 0)
            {
                pnlGlobal.Visible = true;
                printGlobal();
            }
            else
            {
                pnlGlobal.Visible = false;
            }

            lbC.Visible = ShowCloseButton;
            lbS.Visible = ShowSaveButton;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lbC.OnClientClick = "return CloseWindow()";
        }

        #region EVENTS

        protected void lbS_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CloseWindow", "$telerik.$(function() { CloseWindow(); } );", true);
            }
        }

        #endregion EVENTS

        #region PRIVATE METHODES

        private void printLocal()
        {
            SiteObjectType ot = Helper.GetObjectType(ObjectType);
            try
            {
                lblLT.Text = GuiLanguage.GetGuiLanguage("SiteObjects").GetString(string.Format("NotificationHeader{0}", ot.Id));
            }
            catch
            {
                lblLT.Text = GuiLanguage.GetGuiLanguage("SiteObjects").GetString(string.Format("NotificationHeaderDefault"));
            }

            lblLT.Text = "<div class=\"title\">" + lblLT.Text + "</div>";

            pnlLocal.Controls.Add(new LiteralControl(@"<table border='0' cellpadding='4' cellspacing='0'>"));
            pnlLocal.Controls.Add(new LiteralControl(string.Format(@"<tr><th class='col1'>{0}</th><th class='col2'>{1}</th><th class='col3'>{2}</th></tr>", language.GetString("LableAlertsEvent"), language.GetString("LableAlertsType"), language.GetString("LableAlertsWhen"))));

            foreach (Registration itemReg in listRegLocal)
            {
                if (itemReg.Identifier != EventIdentifier.NotDefined)
                {
                    pnlLocal.Controls.Add(new LiteralControl(string.Format(@"<tr><td style=""white-space:nowrap; width:250px"">{0}</td><td>", GetEventString(itemReg.Identifier, true))));
                    DropDownList ddlCar = new DropDownList();
                    ddlCar.ID = string.Format("ddlLCar{0}", (int)itemReg.Identifier);
                    FillCarrierDDl(ddlCar, itemReg.Carriers);

                    pnlLocal.Controls.Add(ddlCar);
                    pnlLocal.Controls.Add(new LiteralControl(string.Format("</td><td>")));

                    DropDownList ddlCarCol = new DropDownList();
                    ddlCarCol.ID = string.Format("ddlLCarCol{0}", (int)itemReg.Identifier);
                    FillCarrierColDDl(ddlCarCol);
                    pnlLocal.Controls.Add(ddlCarCol);

                    pnlLocal.Controls.Add(new LiteralControl(string.Format("</td></tr>")));
                    //Set The stored values
                    if (itemReg.Carriers.CheckedCarrier() != null)
                    {
                        Carrier CheckedCar = itemReg.Carriers.CheckedCarrier();
                        ddlCar.SelectedValue = Convert.ToString((int)CheckedCar.Type);
                        ddlCarCol.SelectedValue = Convert.ToString((int)CheckedCar.Collect);
                    }
                    else
                    {
                        ddlCar.SelectedIndex = 0;
                        ddlCarCol.SelectedIndex = 0;
                    }
                }
            }
            pnlLocal.Controls.Add(new LiteralControl(@"</table>"));

        }

        private void printGlobal()
        {
            switch (ObjectType)
            {
                case 1:
                    lblGT.Text = string.Format("<br>{0}:", language.GetString("TextAlertsGlobal"));
                    break;
                case 20:
                    string tagWordString = Helper.GetTagWordString(new List<string>() { Request.QueryString["TGL1"], Request.QueryString["TGL2"], Request.QueryString["TGL3"] });
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("<br>{0} ", language.GetString("TextAlertsFor"));
                    for (int i = 0; i < ObjectTypes.Length; i++)
                    {
                        sb.Append(Helper.GetObjectName(ObjectTypes[i], false));
                        if (i < ObjectTypes.Length - 2)
                            sb.Append(", ");
                        if (i != 0 && i == ObjectTypes.Length - 1)
                            sb.AppendFormat(" {0} ", languageShared.GetString("TextAnd"));
                    }
                    if (!string.IsNullOrEmpty(tagWordString))
                        sb.AppendFormat(" {0}:<br/><br/>{1}", language.GetString("TextAlertsWithTagwords"), tagWordString);
                    lblGT.Text = sb.ToString();
                    break;
                case 2:
                    lblGT.Text = string.Format("<br>{0}:", language.GetString("TextAlertsForUserObjects"));
                    break;
                default:
                    pnlGlobal.Visible = false;
                    break;
            }

            lblGT.Text = "<div class=\"title\">" + lblGT.Text + "</div>";

            pnlGlobal.Controls.Add(new LiteralControl(@"<table border='0' cellpadding='4' cellspacing='0'>"));
            pnlGlobal.Controls.Add(new LiteralControl(string.Format(@"<tr><th class='col1'>{0}</th><th class='col2'>{1}</th><th class='col3'>{2}</th></tr>", language.GetString("LableAlertsEvent"), language.GetString("LableAlertsType"), language.GetString("LableAlertsWhen"))));

            foreach (Registration itemReg in listRegGlobal)
            {
                if (itemReg.Identifier != EventIdentifier.NotDefined)
                {

                    pnlGlobal.Controls.Add(new LiteralControl(string.Format(@"<tr><td style=""white-space:nowrap; width:250px"">{0}</td><td>", GetEventString(itemReg.Identifier, false))));
                    DropDownList ddlCar = new DropDownList();
                    ddlCar.ID = string.Format("ddlGCar{0}", (int)itemReg.Identifier);
                    FillCarrierDDl(ddlCar, itemReg.Carriers);
                    pnlGlobal.Controls.Add(ddlCar);
                    pnlGlobal.Controls.Add(new LiteralControl(string.Format("</td><td>")));

                    DropDownList ddlCarCol = new DropDownList();
                    ddlCarCol.ID = string.Format("ddlGCarCol{0}", (int)itemReg.Identifier);
                    FillCarrierColDDl(ddlCarCol);
                    pnlGlobal.Controls.Add(ddlCarCol);

                    pnlGlobal.Controls.Add(new LiteralControl(string.Format("</td></tr>")));
                    //Set The stored values
                    if (itemReg.Carriers.CheckedCarrier() != null)
                    {
                        Carrier CheckedCar = itemReg.Carriers.CheckedCarrier();
                        ddlCar.SelectedValue = Convert.ToString((int)CheckedCar.Type);
                        ddlCarCol.SelectedValue = Convert.ToString((int)CheckedCar.Collect);
                    }
                    else
                    {
                        ddlCar.SelectedIndex = 0;
                        ddlCarCol.SelectedIndex = 0;
                    }
                }
            }
            pnlGlobal.Controls.Add(new LiteralControl(@"</table>"));
        }

        private void FillCarrierColDDl(DropDownList ddlCarCol)
        {
            ddlCarCol.Items.Add(new ListItem(languageAlert.GetString("TextAlertsImmediately"), Convert.ToString((int)CarrierCollect.Immediately)));
            ddlCarCol.Items.Add(new ListItem(languageAlert.GetString("TextAlertsDaily"), Convert.ToString((int)CarrierCollect.Daily)));
            ddlCarCol.Items.Add(new ListItem(languageAlert.GetString("TextAlertsWeekly"), Convert.ToString((int)CarrierCollect.Weekly)));
            ddlCarCol.Items.Add(new ListItem(languageAlert.GetString("TextAlertsMonthly"), Convert.ToString((int)CarrierCollect.Monthly)));
        }

        private void FillCarrierDDl(DropDownList ddlCar, CarrierList carrierList)
        {
            foreach (Carrier carrier in carrierList)
            {
                String itemText = string.Empty;
                ddlCar.Items.Add(new ListItem(GetCarrierText(carrier.Type), Convert.ToString((int)carrier.Type)));
            }
        }

        private string GetEventString(EventIdentifier ident, bool isGlobal)
        {
            string key = "TextAlerts" + ident.ToString();
            return languageAlert.GetString(key); 
        }

        private string GetCarrierText(CarrierType cType)
        {
            string key = "TextAlerts" + cType.ToString();
            return languageAlert.GetString(key); 
        }

        public bool Save()
        {
            try
            {
                SaveItems(listRegLocal, "$ddlLCar", "$ddlLCarCol");
                SaveItems(listRegGlobal, "$ddlGCar", "$ddlGCarCol");
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void SaveItems(RegistrationList registrationList, string carrierDropDownId, string intervalDropDownId)
        {
            foreach (Registration registrationItem in registrationList)
            {
                string carrierDropDown = string.Format("{0}{1}{2}", this.UniqueID, carrierDropDownId, (int)registrationItem.Identifier);
                string intervalDropDown = string.Format("{0}{1}{2}", this.UniqueID, intervalDropDownId, (int)registrationItem.Identifier);
                string carrierValue = Request.Form[carrierDropDown];
                string intervalValue = Request.Form[intervalDropDown];
                registrationItem.Carriers.SetChecked((CarrierType)(Convert.ToInt32(carrierValue)), true);
                Carrier carrier = registrationItem.Carriers.CheckedCarrier();
                carrier.Collect = (CarrierCollect)(Convert.ToInt32(intervalValue));

                registrationItem.ObjectID = this.ObjectID;
                registrationItem.UserID = this.UserID;
                registrationItem.CommunityID = this.CommunityID;
                registrationItem.TagWords = this.TagWords;
                if (this.ObjectTypes != null)
                {
                    registrationItem.ObjectTypeList.Clear();
                    foreach (int objectType in this.ObjectTypes)
                    {
                        registrationItem.ObjectTypeList.Add(new ObjType(registrationItem.ObjectTypeList, ObjectType) { Availably = true, Checked = true, Identifier = objectType });
                    }
                }
                else if (this.ObjectType != 0 && !registrationItem.IsGlobal)
                {
                    registrationItem.ObjectTypeList.Clear();
                    registrationItem.ObjectTypeList.Add(new ObjType(registrationItem.ObjectTypeList, ObjectType) { Availably = true, Checked = true, Identifier = ObjectType });
                }

                if (ObjectID.HasValue)
                {
                    DataObject dataObject = DataObject.Load<DataObject>(ObjectID.Value);
                    registrationItem.Title = dataObject.ObjectType == Helper.GetObjectTypeNumericID("User") ? dataObject.Nickname : dataObject.Title;
                }
                registrationItem.Save();
            }
        }

        #endregion PRIVATE METHODES

    }
}