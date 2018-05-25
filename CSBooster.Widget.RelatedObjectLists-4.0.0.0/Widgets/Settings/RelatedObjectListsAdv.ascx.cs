// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using Telerik.Web.UI;

namespace _4screen.CSB.Widget.Settings
{
    public partial class RelatedObjectListsAdv : System.Web.UI.UserControl, IWidgetSettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetRelatedObjectLists");
        protected GuiLanguage languageDataAccess = GuiLanguage.GetGuiLanguage("DataAccess");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                FillControls(true);
            else
                FillControls(false);
        }

        private void FillControls(bool fillData)
        {
            XmlDocument xmlDocument = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);
            if (fillData)
            {
                this.LitCommunity.Text = language.GetString("LabelCommunityMulti"); // "Community-IDs: (Komma getrennt.)";
                this.TxtTagList1.Text = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "TagList1", string.Empty).Replace(Constants.TAG_DELIMITER, ',');
                this.TxtTagList2.Text = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "TagList2", string.Empty).Replace(Constants.TAG_DELIMITER, ',');
                this.TxtTagList3.Text = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "TagList3", string.Empty).Replace(Constants.TAG_DELIMITER, ',');
                this.TxtUser.Text = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "UserId", string.Empty);
                this.TxtCommunity.Text = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CommunityId", string.Empty);
                this.TxtParentOID.Text = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ParentOID", string.Empty);
                this.CbxFeatured.SelectedValue = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Featured", "0");

                if (ParentDataObject.ObjectType == Helper.GetObjectTypeNumericID("User"))
                    this.CbxByUrl.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ByUrl", false);
                else
                    this.CbxByUrl.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ByUrl", true);
            }

            foreach (RadComboBoxItem item in this.CbxFeatured.Items)
            {
                if (item.Value == "0")
                    item.Text = languageShared.GetString("TextAll");
                else
                    item.Text = languageShared.GetString("ObjectFeaturedValue" + item.Value);   
            }

            this.CbxDataSource.Items.Clear();
            if (ParentDataObject.ObjectType == Helper.GetObjectTypeNumericID("Page"))
            {
                CbxDataSource.Items.Add(new RadComboBoxItem(language.GetString("TextDataSource1"), "1"));
                CbxDataSource.Items.Add(new RadComboBoxItem(language.GetString("TextDataSource2"), "2"));
                CbxDataSource.SelectedValue = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DataSource", "1");
            }
            else if (ParentDataObject.ObjectType == Helper.GetObjectTypeNumericID("Community"))
            {
                this.TxtUser.Text = string.Empty;
                if (!HasGroups(ParentDataObject.ObjectID.Value))
                {
                    CbxDataSource.Items.Add(new RadComboBoxItem(language.GetString("TextDataSource1"), "1"));
                    CbxDataSource.Items.Add(new RadComboBoxItem(language.GetString("TextDataSource3"), "3"));
                    CbxDataSource.SelectedValue = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DataSource", "3");
                }
                else
                {
                    CbxDataSource.Items.Add(new RadComboBoxItem(language.GetString("TextDataSource1"), "1"));
                    CbxDataSource.Items.Add(new RadComboBoxItem(language.GetString("TextDataSource3"), "3"));
                    CbxDataSource.Items.Add(new RadComboBoxItem(language.GetString("TextDataSource2"), "2"));
                    CbxDataSource.SelectedValue = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DataSource", "3");
                }
            }
            else if (ParentDataObject.ObjectType == Helper.GetObjectTypeNumericID("User"))
            {
                this.PnlByUrl.Visible = false;
                this.PnlDataSource.Visible = false; 
                CbxByUrl.Enabled = false; 
                CbxDataSource.Items.Add(new RadComboBoxItem(language.GetString("TextDataSource0"), "0"));
                CbxDataSource.SelectedValue = "0";
            }

            if (CbxDataSource.Items.Count > 0)  
                CbxDataSource_SelectedIndexChanged(null, null);

            if (CbxByUrl.Checked)
            {
                PnlParentOID.Visible = false;
                this.TxtParentOID.Text = string.Empty;
            }
            else
            {
                PnlParentOID.Visible = true;
            }
        }


        public bool Save()
        {
            try
            {
                XmlDocument xmlDocument = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);


                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ByUrl", CbxByUrl.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "Featured", this.CbxFeatured.SelectedValue);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "DataSource", this.CbxDataSource.SelectedValue);
                if (this.CbxDataSource.SelectedValue == "0")
                {
                    this.TxtUser.Text = string.Empty;
                    this.TxtCommunity.Text = string.Empty;
                }
                else if (this.CbxDataSource.SelectedValue == "1")
                {
                    this.TxtCommunity.Text = string.Empty;
                }
                else if (this.CbxDataSource.SelectedValue == "3")
                {
                    this.TxtCommunity.Text = string.Empty;
                }
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "UserId", this.TxtUser.Text.Trim());
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "CommunityId", this.TxtCommunity.Text.Trim().Replace(';', ','));

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "TagList1", this.TxtTagList1.Text.Trim().Replace(',', Constants.TAG_DELIMITER));
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "TagList2", this.TxtTagList2.Text.Trim().Replace(',', Constants.TAG_DELIMITER));
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "TagList3", this.TxtTagList3.Text.Trim().Replace(',', Constants.TAG_DELIMITER));

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ParentOID", this.TxtParentOID.Text.Trim());

                return _4screen.CSB.DataAccess.Business.Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
            }
            catch
            {
                return false;
            }
        }

        protected void CbxDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            string source = CbxDataSource.SelectedItem.Value;

            this.PnlCommunity.Visible = false;
            this.PnlUser.Visible = true;

            if (source == "0")
            {
                this.PnlUser.Visible = false;
            }
            else if (source == "2")
            {
                this.PnlCommunity.Visible = true;
                
            }
            else if (source == "3")
            {
                this.PnlCommunity.Visible = false;
            }
        }

        private bool HasGroups(Guid communityID)
        {
            QuickParameters quickParameters = new QuickParameters();
            quickParameters.Udc = UserDataContext.GetUserDataContext();
            quickParameters.ParentObjectID = communityID.ToString();
            quickParameters.ObjectType = 1;
            quickParameters.Amount = 1;
            quickParameters.DisablePaging = true;

            return (DataObjects.Load<DataObject>(quickParameters).Count > 0);
        }


    }
}
