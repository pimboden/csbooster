// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class DetailsPinboardSearch : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        protected DataObject dataObject;
        private DataObjectPinboardSearch pinboardSearch;
        private UserDataContext udc;
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Templates.WebUI.Base");
        public Dictionary<string, object> Settings { get; set; }

        public DataObject DataObject
        {
            get { return dataObject; }
            set { dataObject = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            pinboardSearch = (DataObjectPinboardSearch)dataObject;
            udc = UserDataContext.GetUserDataContext();

            if (UserProfile.Current.IsAnonymous)
                this.CTCTBTN.Visible = false;

            this.CTCTBTN.NavigateUrl = string.Format("Javascript:radWinOpen('/Pages/popups/MessageSend.aspx?MsgType=pbs&recid={0}&objid={1}', '{2}', 510, 490, false, null)", pinboardSearch.UserID, pinboardSearch.ObjectID.Value.ToString(), language.GetString("CommandPinboardContactTitle").StripForScript());

            // Disable tooltip managers by default -> tooltip managers without targets don't work
            this.RTTM.Visible = false;
            if (pinboardSearch != null)
            {
                foreach (string tooltipId in _4screen.CSB.DataAccess.Business.AdWordHelper.GetCampaignObjectIds(pinboardSearch.ObjectID.Value))
                {
                    this.RTTM.TargetControls.Add(tooltipId, true);
                    this.RTTM.Visible = true;
                }
            }

            this.DESCLIT.Text = pinboardSearch.DescriptionLinked;

            decimal price;
            if (pinboardSearch.Price.EndsWith("0") || decimal.TryParse(pinboardSearch.Price, out price))
                this.PRICELIT.Text = string.Format("{0}: {1} CHF", language.GetString("CommandPinboardPrice"), pinboardSearch.Price);
            else if (pinboardSearch.Price != "")
                this.PRICELIT.Text = string.Format("{0}: {1}", language.GetString("CommandPinboardPrice"), pinboardSearch.Price);
            else
                this.PRICELIT.Text = string.Format("{0}: {1}", language.GetString("CommandPinboardPrice"), language.GetString("CommandPinboardNoPrice"));
        }

        protected void OnAjaxUpdate(object sender, Telerik.Web.UI.ToolTipUpdateEventArgs e)
        {
            string[] tooltipId = e.TargetControlID.Split(new char[] { '_' });
            if (tooltipId.Length == 4)
            {
                Literal literal = new Literal();
                literal.Text = _4screen.CSB.DataAccess.Business.AdWordHelper.GetCampaignContent(new Guid(tooltipId[0]), new Guid(tooltipId[1]), UserDataContext.GetUserDataContext(), tooltipId[2], "Popup");
                literal.Text = System.Text.RegularExpressions.Regex.Replace(literal.Text, @"(/Pages/Other/AdCampaignRedirecter.aspx\?CID=\w{8}-\w{4}-\w{4}-\w{4}-\w{12})", "$1&OID=" + tooltipId[1] + "&Word=" + tooltipId[2] + "&Type=PopupLink");
                e.UpdatePanel.ContentTemplateContainer.Controls.Add(literal);
            }
        }
    }
}