// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using Telerik.Web.UI;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class MyContentSearch : System.Web.UI.UserControl
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");

        private string tagList;
        private string generalSearchWord;
        private string objectType;
        private string communityList;
        private bool showMyContentOnly;

        public MyContentMode MyContentMode { get; set; }
        public MyContent MyContent { get; set; }
        public int? ObjectType { get; set; }
        public List<string> ObjectTypes { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (MyContentMode == Common.MyContentMode.Selection)
                PnlType.Visible = false;
            TxtSearch.Attributes.Add("onkeypress", "return DoPostbackOnEnterKey(event, '" + this.LbtnSearch.UniqueID + "')");
            DdMyCont.Items[0].Text = language.GetString("LabelMyContent");
            DdMyCont.Items[1].Text = language.GetString("LabelMyContentInMycommunity");

            InitializeFromQueryString();

            if (ObjectType.HasValue)
                objectType = ObjectType.ToString();
            else if (!String.IsNullOrEmpty(Request.QueryString["T"]))
                objectType = Request.QueryString["T"];
            else
                objectType = Helper.GetObjectType("Picture").NumericId.ToString();

            if (IsPostBack && !string.IsNullOrEmpty(DdObjType.SelectedValue))
                objectType = DdObjType.SelectedValue;
            if (IsPostBack && !string.IsNullOrEmpty(DdMyCont.SelectedValue))
                showMyContentOnly = bool.Parse(DdMyCont.SelectedValue);

            InitializeSearchWordFilter();
            InitializeTypes();
            InitializeCommunities();
            InitializeMyContentFilter();
            InitializeTagFilter();
        }

        private void InitializeFromQueryString()
        {
            generalSearchWord = Request.QueryString["W"] ?? string.Empty;
            communityList = Request.QueryString["C"] ?? string.Empty;
            showMyContentOnly = !string.IsNullOrEmpty(Request.QueryString["I"]) ? bool.Parse(Request.QueryString["I"]) : false;
            tagList = Request.QueryString["TGL1"] ?? string.Empty;
        }

        private void InitializeSearchWordFilter()
        {
            TxtSearch.Text = generalSearchWord;
        }

        private void InitializeTypes()
        {
            DdObjType.Items.Clear();
            List<SiteObjectType> objectTypes = Helper.GetActiveUserContentObjectTypes(false);

            if (MyContentMode == Common.MyContentMode.Widgets)
                objectTypes.RemoveAll(x => x.DetailWidgetId == Guid.Empty);
            if (MyContentMode == Common.MyContentMode.Related)
                objectTypes.RemoveAll(x => !ObjectTypes.Exists(y => y == x.Id));

            var types = from allObjectTypes in objectTypes select new { ID = allObjectTypes.Id, FName = Helper.GetObjectName(allObjectTypes.NumericId, false) };

            DdObjType.DataSource = types;
            DdObjType.DataTextField = "FName";
            DdObjType.DataValueField = "ID";
            DdObjType.DataBind();

            if (UserDataContext.GetUserDataContext().UserRole == "Admin" && MyContentMode == Common.MyContentMode.Admin)
            {
                DdObjType.Items.Insert(0, new RadComboBoxItem(language.GetString("LabelPages"), "Page"));
            }
            DdObjType.SelectedValue = Helper.GetObjectType(objectType).Id;
            if (Helper.GetObjectType(objectType).Id == "Community")
            {
                DdComms.Items.Clear();
                DdComms.Text = language.GetString("LabelAll");
                DdComms.Enabled = false;
                if (!UserDataContext.GetUserDataContext().IsAdmin)
                    DdMyCont.Enabled = false;
            }
        }

        private void InitializeCommunities()
        {
            DdComms.Items.Clear();

            UserDataContext udc = UserDataContext.GetUserDataContext();

            DataObjectList<DataObject> involvedCommunities = null;
            if (!SecuritySection.CachedInstance.CurrentUserHasAccess("MyContentSearchAllCommunities"))
            {
                involvedCommunities = new DataObjectList<DataObject>();

                DataObjectList<DataObject> ownerCommunities = new DataObjectList<DataObject>();
                ownerCommunities.AddRange(DataObjects.Load<DataObject>(new QuickParameters() { Udc = udc, ShowState = null, IgnoreCache = true, MembershipParams = new MembershipParams { UserID = UserProfile.Current.UserId, IsOwner = true, IsCreator = null } }));
                ownerCommunities.AddRange(DataObjects.Load<DataObject>(new QuickParameters() { Udc = udc, ShowState = ObjectShowState.InProgress, IgnoreCache = true, MembershipParams = new MembershipParams { UserID = UserProfile.Current.UserId, IsOwner = null, IsCreator = true } }));
                ownerCommunities.AddRange(DataObjects.Load<DataObject>(new QuickParameters() { Udc = udc, ShowState = ObjectShowState.ConversionFailed, IgnoreCache = true, MembershipParams = new MembershipParams { UserID = UserProfile.Current.UserId, IsOwner = null, IsCreator = true } }));

                DataObjectList<DataObject> memberCommunities = new DataObjectList<DataObject>();
                memberCommunities = DataObjects.Load<DataObject>(new QuickParameters() { Udc = udc, ShowState = ObjectShowState.Published, IgnoreCache = true, MembershipParams = new MembershipParams { UserID = UserProfile.Current.UserId, IsOwner = false, IsCreator = null } });

                if (ownerCommunities.Count == 0)
                    DdMyCont.Enabled = false;

                if (memberCommunities.Count == 0 && ownerCommunities.Count == 0)
                    DdComms.Enabled = false;

                if (showMyContentOnly)
                {
                    involvedCommunities.AddRange(ownerCommunities);
                }
                else
                {
                    involvedCommunities.AddRange(ownerCommunities);
                    involvedCommunities.AddRange(memberCommunities);
                }

                DdComms.AllowCustomText = true;
                DdComms.Text = string.Empty;
                DdComms.ClearSelection();
                DdComms.Attributes.Add("OnChange", "setComboboxText('" + DdComms.ClientID + "')");

                string[] selectedCommunities = communityList.Split(Constants.TAG_DELIMITER);
                foreach (DataObject comm in involvedCommunities)
                {
                    RadComboBoxItem item = new RadComboBoxItem(comm.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity") ? language.GetString("LabelMyProfile") : comm.Title.CropString(25), comm.ObjectID.Value.ToString());
                    CheckBox checkbox = new CheckBox();
                    checkbox.ID = "CheckBox";
                    checkbox.Text = item.Text;
                    checkbox.Attributes.Add("onClick", "setComboboxText('" + DdComms.ClientID + "');stopPropagation(event);");
                    item.Controls.Add(checkbox);

                    foreach (string selectedCommunity in selectedCommunities)
                    {
                        if (item.Value.ToLower() == selectedCommunity.ToLower())
                        {
                            checkbox.Checked = true;
                            DdComms.Text += item.Text + ",";
                        }
                    }
                    DdComms.Items.Add(item);
                }

                DdComms.Text = DdComms.Text.TrimEnd(new char[] { ',' });
            }
            else
            {
                if (CustomizationSection.CachedInstance.MyContent.AdminCommunitySelectionEnabled)
                {
                    DataObjectList<DataObject> allCommunities = DataObjects.Load<DataObject>(new QuickParameters()
                                                                                                 {
                                                                                                     ObjectType = Helper.GetObjectTypeNumericID("Community"),
                                                                                                     Udc = UserDataContext.GetUserDataContext(),
                                                                                                     Amount = 100,
                                                                                                     PageNumber = 1,
                                                                                                     PageSize = 100,
                                                                                                     SortBy = QuickSort.Title
                                                                                                 });
                    DdComms.AllowCustomText = true;
                    DdComms.Text = string.Empty;
                    DdComms.ClearSelection();
                    DdComms.Attributes.Add("OnChange", "setComboboxText('" + DdComms.ClientID + "')");

                    string[] selectedCties = communityList.Split(Constants.TAG_DELIMITER);
                    foreach (DataObject comm in allCommunities)
                    {
                        RadComboBoxItem item = new RadComboBoxItem(comm.Title.CropString(25), comm.ObjectID.Value.ToString());
                        CheckBox checkbox = new CheckBox();
                        checkbox.ID = "CheckBox";
                        checkbox.Text = item.Text;
                        checkbox.Attributes.Add("onClick", "setComboboxText('" + DdComms.ClientID + "');stopPropagation(event);");
                        item.Controls.Add(checkbox);

                        if (!IsPostBack)
                        {
                            foreach (string selectedCty in selectedCties)
                            {
                                if (item.Value.ToLower() == selectedCty.ToLower())
                                {
                                    checkbox.Checked = true;
                                    DdComms.Text += item.Text + ",";
                                }
                            }
                        }
                        DdComms.Items.Add(item);
                    }

                    DdComms.Text = DdComms.Text.TrimEnd(new char[] { ',' });
                }
                else
                {
                    DdComms.Text = language.GetString("LabelAll");
                    DdComms.Enabled = false;
                }
            }

            if (DdComms.Items.Count == 0)
                PnlCommunities.Visible = false;
        }

        private void InitializeTagFilter()
        {
            Guid? userId = null;
            if (showMyContentOnly)
                userId = UserProfile.Current.UserId;
            List<TagReferenceItem> tags = DataObjectTag.GetReferencedTags(Helper.GetObjectType(objectType).NumericId, userId, communityList);
            RlbTags.DataSource = tags;
            RlbTags.DataBind();
        }

        private void InitializeMyContentFilter()
        {
            DdMyCont.Items.FindItemByValue(showMyContentOnly.ToString().ToLower()).Selected = true;
        }

        protected void OnFindClick(object sender, EventArgs e)
        {
            generalSearchWord = TxtSearch.Text;
            objectType = DdObjType.SelectedValue;
            communityList = string.Empty;
            if (objectType != "Community" && objectType != "Page")
            {
                for (int i = 0; i < DdComms.Items.Count; i++)
                {
                    string checkboxId = DdComms.UniqueID + "$i" + i + "$CheckBox";
                    if (!string.IsNullOrEmpty(Request.Form[checkboxId]))
                    {
                        communityList += DdComms.Items[i].Value + Constants.TAG_DELIMITER;
                    }
                }
                communityList = communityList.TrimEnd(Constants.TAG_DELIMITER);
            }

            tagList = string.Empty;
            for (int i = 0; i < RlbTags.Items.Count; i++)
                if (RlbTags.Items[i].Checked)
                    tagList += RlbTags.Items[i].Value + ",";
            tagList = tagList.TrimEnd(',');

            MyContent.ObjectType = Helper.GetObjectTypeNumericID(objectType);
            MyContent.SearchWord = generalSearchWord;
            MyContent.CommunityIds = communityList;
            MyContent.ShowMyContentOnly = bool.Parse(DdMyCont.SelectedValue);
            MyContent.TagList = tagList;
            MyContent.SetCurrentPage(1);
            MyContent.Reload();
            MyContent.Refresh();
        }

        protected void OnResetClick(object sender, EventArgs e)
        {
            InitializeFromQueryString();
            InitializeSearchWordFilter();
            InitializeTypes();
            InitializeCommunities();
            InitializeMyContentFilter();
            InitializeTagFilter();

            MyContent.ObjectType = Helper.GetObjectTypeNumericID(objectType);
            MyContent.SearchWord = string.Empty;
            MyContent.CommunityIds = string.Empty;
            MyContent.ShowMyContentOnly = null;
            MyContent.TagList = string.Empty;
            MyContent.InitializeFromQueryString();
            MyContent.SetCurrentPage(1);
            MyContent.Reload();
            MyContent.Refresh();
        }

        protected void OnDropDownChange(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            objectType = DdObjType.SelectedValue;
            showMyContentOnly = bool.Parse(DdMyCont.SelectedValue);

            if (objectType == "Community" || objectType == "Page")
            {
                DdComms.Items.Clear();
                DdComms.Text = language.GetString("LabelAll");
                DdComms.Enabled = false;
                DdMyCont.Enabled = false;
            }
            else
            {
                DdComms.Enabled = true;
                DdMyCont.Enabled = true;
                InitializeCommunities();
            }

            InitializeCommunities();
            InitializeTagFilter();
        }

        protected void OnTagItemDataBound(object sender, RadListBoxItemEventArgs e)
        {
            TagReferenceItem tag = (TagReferenceItem)e.Item.DataItem;
            RadListBoxItem item = (RadListBoxItem)e.Item;
            item.Value = tag.TagId.ToString();
            item.Text = Helper.GetMappedTagWord(tag.Title);
            if (tagList.Contains(tag.TagId.ToString()))
                item.Checked = true;
        }
    }
}