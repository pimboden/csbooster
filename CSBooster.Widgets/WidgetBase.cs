using System;
using System.Configuration;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public abstract class WidgetBase : System.Web.UI.UserControl, IWidget, IDataObjectWidget
    {
        protected IWidgetHost _Host;
        private Guid _TagWord;
        private Guid _CommunityID;
        private string _LangCode;
        private Guid _InstanceID;
        private SiteContext siteContext;

        public Guid CommunityID
        {
            get { return _CommunityID; }
            set { _CommunityID = value; }
        }

        public string LangCode
        {
            get { return _LangCode; }
            set { _LangCode = value; }
        }

        public Guid InstanceID
        {
            get { return _InstanceID; }
            set { _InstanceID = value; }
        }

        public SiteContext SiteContext
        {
            get { return siteContext; }
            set { siteContext = value; }
        }

        public Guid TagWord
        {
            get { return _TagWord; }
            set { _TagWord = value; }
        }

        void IWidget.Init(IWidgetHost host)
        {
            _Host = host;
        }

        void IWidget.ShowSettings()
        {
            Visible = true;
            _Host.SetEditWidget(false);
            string strTitle = GuiLanguage.GetGuiLanguage("Widgets").GetString("TitleWidgetConfig") + ": ";
            CSBooster_WidgetDataContext wdc = new CSBooster_WidgetDataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);
            var widgetInstance = wdc.hitbl_WidgetInstance_INs.Single(w => w.INS_ID == InstanceID);

            var widgetInstanceText = widgetInstance.hitbl_WidgetInstanceText_WITs.SingleOrDefault(wt => wt.INS_ID == InstanceID && wt.WIT_LangCode == _LangCode);
            if (widgetInstanceText != null)
            {
                strTitle += widgetInstanceText.WIT_Title;
            }

            strTitle = string.Empty;

            string script = string.Format("radWinOpen('{0}/Pages/popups/WidgetEdit.aspx?WID={1}&Ins={2}&CTYID={3}', '{4}', 735, 575);", siteContext.VRoot, widgetInstance.WDG_ID, widgetInstance.INS_ID, CommunityID, strTitle);
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "radWinOpen", script, true);
            (this as IWidget).HideSettings();
        }

        void IWidget.HideSettings()
        {
        }

        void IWidget.Minimized()
        {
        }

        void IWidget.Maximized()
        {
        }

        void IWidget.Closed()
        {
        }

        protected void CloseWidget_Click(object sender, EventArgs e)
        {
            _Host.Close();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                bool blnIsOwner = false;
                CSBooster_WidgetDataContext wdc = new CSBooster_WidgetDataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);

                //MembershipUser memUser = Membership.GetUser(siteContext.Udc.UserID);
                //if (memUser != null)
                if (this._Host.ParentObjectType != 20 && siteContext.Udc.IsAuthenticated)
                {
                    var result = wdc.hisp_Community_IsUserMember(CommunityID, siteContext.Udc.UserID).ElementAtOrDefault(0);
                    if (result != null)
                    {
                        blnIsOwner = result.CUR_IsOwner;
                    }
                }

                var result2 = wdc.hisp_Widget_LoadInstanceData(InstanceID).ElementAtOrDefault(0);
                if (result2 != null && result2.INS_XmlStateData != null)
                {
                    if (!ShowObject(result2.INS_XmlStateData))
                    {
                        bool showWidget = false;
                        if (siteContext.Udc.IsAuthenticated)
                        {
                            showWidget = UserDataContext.GetUserDataContext().IsAdmin;
                            if (!showWidget && (this._Host.ParentObjectType == 1 || this._Host.ParentObjectType == 19))
                            {
                                showWidget = DataObject.IsUserOwner(CommunityID, siteContext.Udc.UserID);
                                if (showWidget)
                                {
                                    var widgetInstance = wdc.hitbl_WidgetInstance_INs.Single(w => w.INS_ID == InstanceID);
                                    if (widgetInstance.INS_IsFixed)
                                        showWidget = false;
                                }
                            }
                        }
                        //Means That there was nothing found. Depending on the widgets Settings Hide the Widget
                        if (!showWidget)
                        {
                            Control widgetControl = WidgetHelper.GetWidgetHost(this, 0, 5);
                            if (widgetControl != null)
                                widgetControl.Visible = false;
                        }
                        else
                        {
                            Controls.Add(new LiteralControl(string.Format("<div class=\"CTY_widget_hide_empty\">{0}</div>", GuiLanguage.GetGuiLanguage("Widgets").GetString("MessageShowOnlyForAdmin"))));
                        }
                    }
                }
                else if (blnIsOwner)
                {
                    if (Request.Form["__EVENTTARGET"] == null || !Request.Form["__EVENTTARGET"].EndsWith("WCl"))
                    {
                        (this as IWidget).ShowSettings();
                        Controls.Clear();
                        Controls.Add(new LiteralControl(string.Format("<B style='color:#FFFFFF'>ACHTUNG : {0}<br/>WidgetID={1}</B>", _InstanceID.ToString(), GuiLanguage.GetGuiLanguage("Widgets").GetString("MessageMissingConfigOrError"))));
                    }
                }
                else if (!blnIsOwner)
                {
                    Visible = false;
                    Controls.Clear();
                    throw new Exception(string.Format("Widget not configurated {0}", GetType().ToString()));
                }
            }
            catch (Exception ex)
            {
                Visible = true;
                Controls.Clear();
                Controls.Add(new LiteralControl(string.Format(GuiLanguage.GetGuiLanguage("Widgets").GetString("MessageWidgetError"), GetType().ToString(), ex.Message)));
            }
        }
        /// <summary>
        /// Return if Content has been found
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public abstract bool ShowObject(string strXml);
    }
}