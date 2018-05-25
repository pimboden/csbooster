using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Collections.Generic;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using Telerik.Web.UI;

namespace _4screen.CSB.WebUI.UserControls
{
    public class TagHandlerCheckBoxTemplate : ITemplate
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");

        public void InstantiateIn(Control container)
        {
            CheckBox checkbox = new CheckBox();
            checkbox.ID = "CheckBox";
            checkbox.Attributes.Add("onClick", "stopPropagation(event);");
            checkbox.DataBinding += new EventHandler(CheckboxDataBinding);
            container.Controls.Add(checkbox);
        }

        private void CheckboxDataBinding(object sender, EventArgs e)
        {
            CheckBox target = (CheckBox)sender;
            RadComboBoxItem item = (RadComboBoxItem)target.BindingContainer;
            string itemText = (string)DataBinder.Eval(item, "Text");
            target.Text = itemText;
        }
    }

    public partial class ObjectTagHandler : System.Web.UI.UserControl, ITagHandler
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");

        private bool mainComboBoxInvisible;
        private bool mainComboBoxReadOnly;
        private bool categoryComboBoxLinked;
        private bool categoryComboBoxMultiSelect;
        private bool tagTextBoxInvisible;
        private string currentTags = string.Empty;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ReadSettings();

            if (mainComboBoxReadOnly)
                this.ddlMainCat.Enabled = false;
            else if (mainComboBoxInvisible)
                this.MAINTAGPNL.Visible = false;
            if (tagTextBoxInvisible)
                this.TAGPNL.Visible = false;

            PrintTagHandler();
        }

        private void ReadSettings()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Server.MapPath("/configurations/TagHandler.config"));

            mainComboBoxInvisible = false;
            mainComboBoxReadOnly = false;
            XmlNode xmlNode = (XmlNode)xmlDocument.SelectSingleNode("//Tagging/TagHandler/MainTag");
            if (xmlNode.InnerText.ToLower() == "hidden")
                mainComboBoxInvisible = true;
            else if (xmlNode.InnerText.ToLower() == "readonly")
                mainComboBoxReadOnly = true;

            categoryComboBoxLinked = false;
            xmlNode = (XmlElement)xmlDocument.SelectSingleNode("//Tagging/TagHandler/CategoryTag/Behavior");
            if (xmlNode.InnerText.ToLower() == "maintaglinked")
                categoryComboBoxLinked = true;

            categoryComboBoxMultiSelect = true;
            xmlNode = (XmlElement)xmlDocument.SelectSingleNode("//Tagging/TagHandler/CategoryTag/MultiSelect");
            if (xmlNode.InnerText.ToLower() == "no")
                categoryComboBoxMultiSelect = false;

            tagTextBoxInvisible = false;
            xmlNode = (XmlElement)xmlDocument.SelectSingleNode("//Tagging/TagHandler/FreeTag");
            if (xmlNode.InnerText.ToLower() == "hidden")
                tagTextBoxInvisible = true;
        }

        private void PrintTagHandler()
        {
            string tags = currentTags;

            if (IsPostBack)
            {
                string mainTags = ddlMainCat.Text;
                if (!string.IsNullOrEmpty(mainTags))
                {
                    if (!IsTagInTagString(tags, mainTags))
                        tags = mainTags + Constants.TAG_DELIMITER + tags;
                }

            }

            LoadGroups(ref tags);
            LoadSubGroups(ref tags);
            this.TxtFreeTags.Text = tags.Trim(Constants.TAG_DELIMITER).Replace(Constants.TAG_DELIMITER, ',');
        }

        private void LoadGroups(ref string tags)
        {
            this.ddlMainCat.Items.Clear();
            this.ddlMainCat.Items.Add(new RadComboBoxItem(" ", ""));

            List<MainTag> tagList = MainTags.Load(null, 1);
            bool oneSelected = false;
            foreach (MainTag tagItem in tagList)
            {
                RadComboBoxItem item = new RadComboBoxItem(tagItem.Title, tagItem.Id.ToString());
                if (!oneSelected && IsTagInTagString(tags, item.Text))
                {

                    item.Selected = true;
                    oneSelected = true;
                    tags = tags.Replace(item.Text.ToLower(), string.Empty).Replace(string.Format("{0}{0}", Constants.TAG_DELIMITER), Constants.TAG_DELIMITER.ToString());
                }
                this.ddlMainCat.Items.Add(item);
            }
        }

        Func<string, string, bool> IsTagInTagString = (i, j) => i.Contains(string.Format("{0}{1}", j.ToLower(), Constants.TAG_DELIMITER)) || i.EndsWith(j.ToLower());

        private void LoadSubGroups(ref string tags)
        {
            this.ddlSubCat.Items.Clear();
            if (categoryComboBoxMultiSelect)
            {
                this.ddlSubCat.AllowCustomText = true;
                this.ddlSubCat.Text = string.Empty;
                this.ddlSubCat.Attributes.Add("OnChange", "setComboboxText('" + ddlSubCat.ClientID + "')");
            }

            List<MainTag> tagList;
            if (categoryComboBoxLinked && !string.IsNullOrEmpty(this.ddlMainCat.SelectedValue))
                tagList = MainTags.Load(int.Parse(this.ddlMainCat.SelectedValue), 2);
            else
                tagList = MainTags.Load(null, 2);

            bool oneSelected = false;
            foreach (MainTag tagItem in tagList)
            {
                RadComboBoxItem item = new RadComboBoxItem(tagItem.Title, tagItem.Id.ToString());
                if (categoryComboBoxMultiSelect)
                {
                    CheckBox checkbox = new CheckBox();
                    checkbox.ID = "CheckBox";
                    checkbox.Text = item.Text;
                    checkbox.Attributes.Add("onClick", "setComboboxText('" + ddlSubCat.ClientID + "');stopPropagation(event);");
                    item.Controls.Add(checkbox);

                    if (IsTagInTagString(tags, item.Text))
                    {
                        checkbox.Checked = true;
                        this.ddlSubCat.Text += item.Text + ",";
                        tags = tags.Replace(item.Text.ToLower(), string.Empty).Replace(string.Format("{0}{0}", Constants.TAG_DELIMITER), Constants.TAG_DELIMITER.ToString());
                    }

                }
                else if (!oneSelected && IsTagInTagString(tags, item.Text))
                {
                    oneSelected = true;
                    item.Selected = true;
                    tags = tags.Replace(item.Text.ToLower(), string.Empty).Replace(string.Format("{0}{0}", Constants.TAG_DELIMITER), Constants.TAG_DELIMITER.ToString());
                }
                this.ddlSubCat.Items.Add(item);

            }
        }

        protected void OnSelectedGroupChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string sub = string.Empty;
            if (categoryComboBoxLinked)
                LoadSubGroups(ref sub);
        }


        public void SetTags(string tags)
        {
            currentTags = tags;
        }

        public string GetTags()
        {
            // Get tags from dropdowns
            string tags = ddlMainCat.Text;
            if (categoryComboBoxMultiSelect)
            {
                for (int i = 0; i < ddlSubCat.Items.Count; i++)
                {
                    string checkboxId = ddlSubCat.UniqueID + "$i" + i + "$Checkbox";
                    if (Request.Form[checkboxId] != null)
                    {
                        tags += Constants.TAG_DELIMITER + ddlSubCat.Items[i].Text;
                    }
                }
            }
            else
            {
                tags += Constants.TAG_DELIMITER + ddlSubCat.Text;
            }
            tags = tags.Trim(Constants.TAG_DELIMITER);

            // Get free tags
            if (Request.Form[TxtFreeTags.UniqueID] != null)
            {
                string[] tagList = Request.Form[TxtFreeTags.UniqueID].Split(new char[] { ',', ';' });
                for (int i = 0; i < tagList.Length; i++)
                {
                    tags += Constants.TAG_DELIMITER + tagList[i].Trim();
                }
            }
            tags = tags.Trim(Constants.TAG_DELIMITER);

            return tags;
        }
    }
}