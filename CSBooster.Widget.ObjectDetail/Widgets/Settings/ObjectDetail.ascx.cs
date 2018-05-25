// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace _4screen.CSB.Widget.Settings
{
    public partial class ObjectDetail : System.Web.UI.UserControl, IWidgetSettings
    {
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        private string objectType;
        private string template = string.Empty;

        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        private GuiLanguage languageDataAccess = GuiLanguage.GetGuiLanguage("DataAccess");
        private GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetObjectDetail");

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            FillControls();
        }

        private void FillControls()
        {
            XmlDocument xmlDocument = Utils.LoadWidgetInstanceSettings(InstanceId);

            this.CbxOrderBy.Items.Add(new RadComboBoxItem(language.GetString("TextSourceManual"), "-1"));
            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.Viewed.ToString()), QuickSort.Viewed.ToString("D")));
            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.StartDate.ToString()), QuickSort.StartDate.ToString("D")));
            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.Commented.ToString()), QuickSort.Commented.ToString("D")));
            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.RatedAverage.ToString()), QuickSort.RatedAverage.ToString("D")));
            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.Random.ToString()), QuickSort.Random.ToString("D")));

            CbxOrderBy.SelectedValue = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Source", "-1");

            CbxByUrl.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ByUrl", false);
            objectType = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ObjectType", "0");
            if (objectType == "0")
            {
                CbxByUrl.Checked = true;
                CbxByUrl.Enabled = false;
            }

            TxtSelected.Value = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ObjectID", string.Empty);

            CbxByUrl_CheckedChanged(null, null);
        }

        protected void lbtnSR_Click(object sender, EventArgs e)
        {
            template = Helper.GetObjectType(objectType).SearchForSelectCtrl;

            Guid UserID = UserProfile.Current.UserId;
            QuickParameters qp = new QuickParameters();
            qp.Direction = QuickSortDirection.Desc;
            qp.SortBy = QuickSort.StartDate;

            qp.Title = TxtSearch.Text;
            qp.Description = TxtSearch.Text;

            string[] tagLists = TxtTagword.Text.ToLower().Replace("  ", " ").Replace(" or ", "¦").Split('¦');
            if (tagLists.Length > 0)
                qp.Tags1 = QuickParameters.GetDelimitedTagIds(tagLists[0].Trim().Replace(" ", Constants.TAG_DELIMITER.ToString()), Constants.TAG_DELIMITER);
            if (tagLists.Length > 1)
                qp.Tags2 = QuickParameters.GetDelimitedTagIds(tagLists[1].Trim().Replace(" ", Constants.TAG_DELIMITER.ToString()), Constants.TAG_DELIMITER);
            if (tagLists.Length > 2)
                qp.Tags3 = QuickParameters.GetDelimitedTagIds(tagLists[2].Trim().Replace(" ", Constants.TAG_DELIMITER.ToString()), Constants.TAG_DELIMITER);

            qp.Udc = UserDataContext.GetUserDataContext();
            qp.ObjectType = Helper.GetObjectTypeNumericID(objectType);
            qp.ShowState = ObjectShowState.Published;
            qp.DisablePaging = true;
            qp.CatalogSearchType = DBCatalogSearchType.ContainsTable;

            if (ParentDataObject.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
            {
                qp.Udc = UserDataContext.GetUserDataContext(Constants.ANONYMOUS_USERID);
                qp.CommunityID = null;
                qp.Communities = null;
                qp.UserID = UserID;
            }
            //If its a CMSPage Search  Text  is the name of an object that is in any Public community
            else if (ParentDataObject.ObjectType == Helper.GetObjectTypeNumericID("Page"))
            {
                qp.Udc = UserDataContext.GetUserDataContext(Constants.ANONYMOUS_USERID);
                qp.CommunityID = null;
                qp.Communities = null;
                qp.UserID = null;
            }
            else if (ParentDataObject.ObjectType == Helper.GetObjectTypeNumericID("Community"))
            {
                qp.Udc = UserDataContext.GetUserDataContext();
                qp.CommunityID = ParentDataObject.CommunityID;
            }

            this.OBJOVW.DataSource = DataObjects.LoadByReflection(qp);
            this.OBJOVW.DataBind();
        }

        protected void OBJOVW_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataObject dataObject = (DataObject)e.Item.DataItem;
            PlaceHolder ph = (PlaceHolder)e.Item.FindControl("PhItem");

            Control ctrl = LoadControl(string.Format("~/UserControls/Templates/{0}", template));
            IDataObjectWorker dataObjectWorker = ctrl as IDataObjectWorker;
            if (dataObjectWorker != null)
            {
                dataObjectWorker.DataObject = dataObject;
            }

            LiteralControl lit = new LiteralControl();
            if (TxtSelected.Value == dataObject.ObjectID.ToString())
                lit.Text = string.Format("<div id=\"{0}\" onclick=\"javascript:SelectElement('{0}', '{1}');\" class=\"CSB_wi_selected\">", dataObject.ObjectID, TxtSelected.ClientID);
            else
                lit.Text = string.Format("<div id=\"{0}\" onclick=\"javascript:SelectElement('{0}', '{1}');\" class=\"CSB_wi_not_selected\">", dataObject.ObjectID, TxtSelected.ClientID);

            ph.Controls.Add(lit);
            ph.Controls.Add(ctrl);
            ph.Controls.Add(new LiteralControl("</div>"));

        }

        protected void CbxByUrl_CheckedChanged(object sender, EventArgs e)
        {
            PhSearch.Visible = !CbxByUrl.Checked;
        }

        public bool Save()
        {
            try
            {
                XmlDocument xmlDocument = Utils.LoadWidgetInstanceSettings(InstanceId);

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ByUrl", CbxByUrl.Checked);
                if (PhSearch.Visible)
                {
                    XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "Source", CbxOrderBy.SelectedValue);
                    XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ObjectID", TxtSelected.Value);
                }
                else
                {
                    XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ObjectID", "");
                }

                return Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
            }
            catch
            {
                return false;
            }
        }

    }
}
