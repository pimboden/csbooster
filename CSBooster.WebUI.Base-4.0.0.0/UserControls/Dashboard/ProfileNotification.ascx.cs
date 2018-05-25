// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.Common.Notification;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Notification.Business;
using _4screen.CSB.WebUI.Code;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class ProfileNotification : ProfileQuestionsControl
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");
        RegistrationList registrationListGlobal;
        RegistrationList registrationListLocal;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            LoadNotification();
        }

        private void LoadNotification()
        {
            registrationListGlobal = new RegistrationList(UserDataContext.GetUserDataContext(), WebRootPath.Instance.ToString());
            registrationListGlobal.Load(false, null, null, null, 0, null, null, UserProfile.Current.UserId, true);

            registrationListLocal = new RegistrationList(UserDataContext.GetUserDataContext(), WebRootPath.Instance.ToString());
            registrationListLocal.Load(false, null, null, null, 0, null, null, UserProfile.Current.UserId, false);

            PrintGlobalNotifications(PhGlobal);
            PrintSpecificNotifications(PhMyObjects, true);
            PrintSpecificNotifications(PhOtherObjects, false);
        }

        private void PrintGlobalNotifications(PlaceHolder panel)
        {
            bool showAlertsPanel = false;
            panel.Controls.Clear();

            PlaceHolder ph = new PlaceHolder();
            ph.Controls.Add(new LiteralControl(@"<table class='dashboardAlertsSettings' border='0' cellpadding='5' cellspacing='0'>
                                                   <tr>
                                                      <th>Ereignis</th>
                                                      <th>Bedingungen</th>
                                                      <th>Typ / Wann</th>
                                                      <th></th>
                                                   </tr>"));

            foreach (Registration item in registrationListGlobal)
            {
                if (item.IsGlobal)
                {
                    showAlertsPanel = true;

                    ph.Controls.Add(new LiteralControl(string.Format("<tr>")));
                    ph.Controls.Add(new LiteralControl(string.Format("<td>{0}</td>", GetEventString(item.Identifier))));
                    ph.Controls.Add(new LiteralControl(string.Format("<td>")));
                    if (item.ObjectID.HasValue)
                    {
                        DataObject obj = DataObject.Load<DataObject>(item.ObjectID, ObjectShowState.Published, false);
                        string userOrCommunityPrefix = obj.ObjectType == Helper.GetObjectTypeNumericID("User") ? string.Format("{0}: ", language.GetString("TextAlertsFromObjects")) : string.Format("{0}: ", language.GetString("TextAlertsFromCommunity"));
                        ph.Controls.Add(new LiteralControl(string.Format("<div>{0} {1}</div>", userOrCommunityPrefix, CreateTitle(obj))));
                    }
                    if (item.TagWords != null && item.TagWords.Count > 0)
                    {
                        List<string> tagWordList = new List<string>();
                        string[] tagWordStrings = new string[3];
                        foreach (TagWord tagWord in item.TagWords)
                        {
                            DataObjectTag dataObjectTag = DataObject.Load<DataObjectTag>(tagWord.TagID.ToGuid());
                            tagWordStrings[tagWord.GroupID - 1] += dataObjectTag.Title + ",";
                        }
                        foreach (string tagWordString in tagWordStrings)
                        {
                            if (!string.IsNullOrEmpty(tagWordString))
                            {
                                tagWordList.Add(tagWordString.TrimEnd(','));
                            }
                        }
                        ph.Controls.Add(new LiteralControl(string.Format("<div>Tag Words: {0}</div>", Helper.GetTagWordString(tagWordList))));
                    }
                    ph.Controls.Add(new LiteralControl(string.Format("<div style=\"margin-top:10px;\">{0}<div class=\"clearBoth\"></div></div>", CreateImageList(item.ObjectTypeList, false))));
                    ph.Controls.Add(new LiteralControl(string.Format("</td>")));
                    ph.Controls.Add(new LiteralControl(string.Format("<td><div>")));

                    DropDownList ddlCar = new DropDownList();
                    ddlCar.Width = 80;
                    ddlCar.ID = string.Format("ddlCar{0}", item.ID);
                    FillCarrierDDl(ddlCar, item.Carriers);
                    ph.Controls.Add(ddlCar);
                    ph.Controls.Add(new LiteralControl(string.Format("</div><div style=\"margin-top:5px;\">")));

                    DropDownList ddlCarCol = new DropDownList();
                    ddlCarCol.Width = 80;
                    ddlCarCol.ID = string.Format("ddlCarCol{0}", item.ID);
                    FillCarrierColDDl(ddlCarCol);
                    ph.Controls.Add(ddlCarCol);
                    ph.Controls.Add(new LiteralControl(string.Format("</div></td>")));

                    if (item.Carriers.CheckedCarrier() != null)
                    {
                        Carrier CheckedCar = item.Carriers.CheckedCarrier();
                        ddlCar.SelectedValue = Convert.ToString((int)CheckedCar.Type);
                        ddlCarCol.SelectedValue = Convert.ToString((int)CheckedCar.Collect);
                    }
                    else
                    {
                        ddlCar.SelectedIndex = 0;
                        ddlCarCol.SelectedIndex = 0;
                    }

                    HtmlGenericControl td = new HtmlGenericControl("td");
                    LinkButton linkButton = new LinkButton();
                    linkButton.Text = string.Format("<img src=\"/Library/Images/Layout/cmd_delete.png\" title=\"Löschen\">");
                    linkButton.Click += new EventHandler(OnGlobalDeleteClick);
                    linkButton.CommandArgument = item.ID;
                    linkButton.ID = item.ID;
                    td.Controls.Add(linkButton);
                    ph.Controls.Add(td);
                    ph.Controls.Add(new LiteralControl(string.Format("</tr>")));
                }
            }

            ph.Controls.Add(new LiteralControl("</table>"));

            if (showAlertsPanel)
                panel.Controls.Add(ph);
            else
                panel.Controls.Add(new LiteralControl(language.GetString("MessageNoAlerts")));
        }

        private void PrintSpecificNotifications(PlaceHolder panel, bool myContent)
        {
            bool showAlertsPanel = false;
            panel.Controls.Clear();

            PlaceHolder ph = new PlaceHolder();
            ph.Controls.Add(new LiteralControl(@"<table class='dashboardAlertsSettings' border='0' cellpadding='5' cellspacing='0'>
                                                   <tr>
                                                      <th>Ereignis</th>
                                                      <th>Objekt</th>
                                                      <th>Typ / Wann</th>
                                                      <th></th>
                                                   </tr>"));

            foreach (Registration item in registrationListLocal)
            {
                if (!item.IsGlobal && item.ObjectID.HasValue)
                {
                    DataObject obj = DataObject.Load<DataObject>(item.ObjectID, ObjectShowState.Published, false);

                    if (myContent == (obj.UserID == UserProfile.Current.UserId))
                    {
                        showAlertsPanel = true;

                        ph.Controls.Add(new LiteralControl(string.Format("<tr>")));
                        ph.Controls.Add(new LiteralControl(string.Format("<td>{0}</td>", GetEventString(item.Identifier))));
                        ph.Controls.Add(new LiteralControl(string.Format("<td>{1} {0}</td>", CreateTitle(obj), CreateImageList(item.ObjectTypeList, true))));
                        ph.Controls.Add(new LiteralControl(string.Format("<td><div>")));

                        DropDownList ddlCar = new DropDownList();
                        ddlCar.Width = 80;
                        ddlCar.ID = string.Format("ddlCar{0}", item.ID);
                        FillCarrierDDl(ddlCar, item.Carriers);
                        ph.Controls.Add(ddlCar);
                        ph.Controls.Add(new LiteralControl(string.Format("</div><div style=\"margin-top:5px;\">")));

                        DropDownList ddlCarCol = new DropDownList();
                        ddlCarCol.Width = 80;
                        ddlCarCol.ID = string.Format("ddlCarCol{0}", item.ID);
                        FillCarrierColDDl(ddlCarCol);
                        ph.Controls.Add(ddlCarCol);
                        ph.Controls.Add(new LiteralControl(string.Format("</div></td>")));

                        if (item.Carriers.CheckedCarrier() != null)
                        {
                            Carrier CheckedCar = item.Carriers.CheckedCarrier();
                            ddlCar.SelectedValue = Convert.ToString((int)CheckedCar.Type);
                            ddlCarCol.SelectedValue = Convert.ToString((int)CheckedCar.Collect);
                        }
                        else
                        {
                            ddlCar.SelectedIndex = 0;
                            ddlCarCol.SelectedIndex = 0;
                        }

                        HtmlGenericControl td = new HtmlGenericControl("td");
                        LinkButton linkButton = new LinkButton();
                        linkButton.Text = string.Format("<img src=\"/Library/Images/Layout/cmd_delete.png\" title=\"{0}\">", language.GetString("CommandAlertsDelete"));
                        linkButton.Click += new EventHandler(OnLocalDeleteClick);
                        linkButton.ID = item.ID;
                        linkButton.CommandArgument = item.ID;
                        td.Controls.Add(linkButton);
                        ph.Controls.Add(td);
                        ph.Controls.Add(new LiteralControl(string.Format("</tr>")));
                    }
                }
            }

            ph.Controls.Add(new LiteralControl("</table>"));

            if (showAlertsPanel)
                panel.Controls.Add(ph);
            else
                panel.Controls.Add(new LiteralControl(language.GetString("MessageNoAlerts")));
        }

        private string GetEventString(EventIdentifier ident)
        {
            string key = "TextAlerts" + ident.ToString();
            return language.GetString(key); 
        }

        private string CreateTitle(DataObject obj)
        {
            return string.Format("<a href='{0}'>{1}</a>", Helper.GetDetailLink(obj.ObjectType, obj.ObjectID.Value.ToString()), obj.Title.CropString(32));
        }

        private void FillCarrierColDDl(DropDownList ddlCarCol)
        {
            ddlCarCol.Items.Add(new ListItem(language.GetString("TextAlertsImmediately"), Convert.ToString((int)CarrierCollect.Immediately)));
            ddlCarCol.Items.Add(new ListItem(language.GetString("TextAlertsDaily"), Convert.ToString((int)CarrierCollect.Daily)));
            ddlCarCol.Items.Add(new ListItem(language.GetString("TextAlertsWeekly"), Convert.ToString((int)CarrierCollect.Weekly)));
            ddlCarCol.Items.Add(new ListItem(language.GetString("TextAlertsMonthly"), Convert.ToString((int)CarrierCollect.Monthly)));
        }

        private void FillCarrierDDl(DropDownList ddlCar, CarrierList carrierList)
        {
            foreach (Carrier carrier in carrierList)
            {
                if (carrier.Type != CarrierType.None)
                {
                    ListItem lst = new ListItem(GetCarrierText(carrier.Type), Convert.ToString((int)carrier.Type));
                    ddlCar.Items.Add(lst);
                }
            }
        }

        private string GetCarrierText(CarrierType cType)
        {
            string key = "TextAlerts" + cType.ToString();
            return language.GetString(key); 
        }

        private string CreateImageList(ObjTypeList list, bool isObjectNameSingular)
        {
            StringBuilder objectIcons = new StringBuilder();
            foreach (ObjType item in list)
            {
                if (!item.Checked)
                    continue;

                objectIcons.AppendFormat("<div class='icon'><img src='/Library/Images/Layout/{0}' title='{1}' /></div>", Helper.GetObjectIcon(item.Identifier), Helper.GetObjectName(item.Identifier, isObjectNameSingular));
            }
            return objectIcons.ToString();
        }

        protected void OnGlobalDeleteClick(object sender, EventArgs e)
        {
            DeleteRegistration(registrationListGlobal.GetItemByID(((LinkButton)sender).CommandArgument));
        }

        protected void OnLocalDeleteClick(object sender, EventArgs e)
        {
            DeleteRegistration(registrationListLocal.GetItemByID(((LinkButton)sender).CommandArgument));
        }

        private void DeleteRegistration(Registration registrationItem)
        {
            foreach (var carrier in registrationItem.Carriers)
            {
                carrier.Checked = false;
            }
            registrationItem.Carriers.Item(CarrierType.None).Checked = true;
            registrationItem.Save();

            LoadNotification();
        }

        public void Save()
        {
            SaveItems(registrationListGlobal);
            SaveItems(registrationListLocal);

            LoadNotification();
        }

        public void SaveItems(RegistrationList list)
        {
            foreach (Registration item in list)
            {
                string strDdlCarName = string.Format("{0}$ddlCar{1}", this.UniqueID, item.ID);
                string strddlCarVal = Request.Form[strDdlCarName];

                if (string.IsNullOrEmpty(strddlCarVal))
                    continue;

                string strDdlCarColName = string.Format("{0}$ddlCarCol{1}", this.UniqueID, item.ID);
                string strddlCarColVal = Request.Form[strDdlCarColName];

                item.Carriers.SetChecked((CarrierType)(Convert.ToInt32(strddlCarVal)), true);
                Carrier carr = item.Carriers.CheckedCarrier();
                carr.Collect = (CarrierCollect)(Convert.ToInt32(strddlCarColVal));
                foreach (ObjType itemObj in item.ObjectTypeList.GetEnumeratorOnlyAvailably)
                {
                    itemObj.Checked = true;
                }
                item.Save();
            }
        }
    }
}