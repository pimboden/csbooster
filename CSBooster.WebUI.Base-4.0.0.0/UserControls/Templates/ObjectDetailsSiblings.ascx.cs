// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using SiteConfig = _4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class ObjectDetailsSiblings : System.Web.UI.UserControl, IBrowsable, ISettings, IDataObjectWorker
    {
        public enum SpecialLoadType
        {
            Default = 0,
            LoadTypeEventJoinedUser = 1
        }

        private bool autoSelectCurrentPage = true;
        private SpecialLoadType enuSpecialLoadType = SpecialLoadType.Default;
        private DataObject dataObject;
        private int currentPage = 1;
        private int pageSize = 4;
        private int itemsPerRow = 4;
        private int totalItems;
        private string folderParams = null;
        private Guid? folderId = null;
        private int currentItemPosX = 0;
        private int currentItemPosY = 0;

        public Dictionary<string, object> Settings { get; set; }

        public DataObject DataObject
        {
            get { return dataObject; }
            set { dataObject = value; }
        }

        public string FolderParams { get; set; }
        public QuickParameters QuickParameters { get; set; }

        public SpecialLoadType LoadType
        {
            get { return enuSpecialLoadType; }
            set { enuSpecialLoadType = value; }
        }

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Helper.GetObjectType(dataObject.objectType).HasOverview)
                return;

            GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");

            if (!string.IsNullOrEmpty(Request.Form["__EVENTTARGET"]) && Request.Form["__EVENTTARGET"].Contains(this.Pager.UniqueID))
                autoSelectCurrentPage = false;

            this.Pager.PageSize = pageSize;
            this.Pager.BrowsableControl = this;

            this.DLObjects.RepeatLayout = RepeatLayout.Flow;
            this.DLObjects.RepeatColumns = itemsPerRow;
            //this.DLObjects.ItemStyle.Width = new Unit((int)Math.Round(100.0f / (float)itemsPerRow, 0) + "%");

            if (LoadType == SpecialLoadType.LoadTypeEventJoinedUser)
            {
                this.Pager.ItemNameSingular = language.GetString("SharedUserSingular");
                this.Pager.ItemNamePlural = language.GetString("SharedUserPlural");
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["CFID"]))
            {
                string[] folderIds = Request.QueryString["CFID"].Split(';');

                string parentFolderIds = string.Empty;
                for (int i = 0; i < folderIds.Length - 1; i++)
                    parentFolderIds += folderIds[i] + ";";
                if (parentFolderIds.Length > 0)
                    parentFolderIds = "&CFID=" + parentFolderIds;
                parentFolderIds = parentFolderIds.TrimEnd(';');

                folderId = folderIds[folderIds.Length - 1].ToNullableGuid();
                folderParams = "&CFID=" + Request.QueryString["CFID"];
                this.Pager.CustomText = string.Format("<a class=\"\" href=\"{0}{1}\">Ordner zeigen</a>", Helper.GetDetailLink("Folder", folderId.Value.ToString(), false), parentFolderIds);
            }
            else
            {
                DataObject community = DataObject.Load<DataObject>(dataObject.CommunityID);
                string userCommunityParamter = community.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity") ? string.Format("&XUI={0}&XCN=", community.UserID) : string.Format("&XCN={0}", community.ObjectID);
                this.Pager.CustomText = string.Format("<a class=\"\" href=\"{0}{1}\">{2}</a>", Helper.GetOverviewLink(dataObject.ObjectType, false).Replace("&XCN=", ""), userCommunityParamter, language.GetString("CommandeShowOverview"));
            }

            RestoreState();
            Reload();
            SaveState();
        }

        private void RestoreState()
        {
            string idPrefix = this.UniqueID + "$";
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBPageNum")))
                currentPage = int.Parse(Request.Params.Get(idPrefix + "PBPageNum"));
        }

        private void SaveState()
        {
            if (this.currentPage != 0)
                this.PBPageNum.Value = "" + currentPage;
        }

        protected void OnObjectsItemDataBound(object sender, DataListItemEventArgs e)
        {
            DataObject DataObject = (DataObject)e.Item.DataItem;
            PlaceHolder item = (PlaceHolder)e.Item.FindControl("PhItem");
            SetOutput(item, (IDataObjectWorker)this.LoadControl("~/UserControls/Templates/SmallOutputObject.ascx"), DataObject);
        }

        private void SetOutput(PlaceHolder ph, IDataObjectWorker outputControl, DataObject DataObject)
        {
            outputControl.DataObject = DataObject;
            if (outputControl is IFolderParameters)
                ((IFolderParameters)outputControl).FolderParameters = folderParams;
            ph.Controls.Add((Control)outputControl);
        }

        protected void Reload()
        {
            UserDataContext udc = UserDataContext.GetUserDataContext();

            List<DataObject> siblings = null;
            if (this.LoadType == SpecialLoadType.LoadTypeEventJoinedUser)
            {
                /* TODO: Joined user über obj to obj realisieren
                 * 
                 * DataObjectEvent objEvent = (DataObjectEvent)dataObject;
                siblings = DataObjects.LoadByObjectID<DataObject>(objEvent.JoinedUser.Split(';'), new QuickParameters() { Udc = udc, ShowState = ObjectShowState.Published });
                totalItems = siblings.Count;*/
            }
            else if (folderId.HasValue)
            {
                siblings = DataObjects.Load<DataObject>(new QuickParameters { RelationParams = new RelationParams { ParentObjectID = folderId }, ShowState = ObjectShowState.Published, Amount = 0, Direction = QuickSortDirection.Asc, PageNumber = currentPage, PageSize = pageSize, SortBy = QuickSort.RelationSortNumber, Udc = UserDataContext.GetUserDataContext() });
                totalItems = ((DataObjectList<DataObject>)siblings).ItemTotal;
            }
            else
            {
                QuickParameters quickParameters = new QuickParameters() { Udc = udc, Amount = 0, PageSize = pageSize, PageNumber = currentPage, ObjectType = dataObject.ObjectType, CommunityID = dataObject.CommunityID, ShowState = ObjectShowState.Published, WithCopy = true };
                quickParameters.QuerySourceType = QuerySourceType.Page;

                if (autoSelectCurrentPage)
                {
                    quickParameters.CurrentObjectID = dataObject.ObjectID;
                    quickParameters.IgnoreCache = true;
                }

                siblings = DataObjects.Load<DataObject>(quickParameters);
                totalItems = ((DataObjectList<DataObject>)siblings).ItemTotal;
                currentPage = ((DataObjectList<DataObject>)siblings).PageNumber;
            }

            this.DLObjects.DataSource = siblings;
            this.DLObjects.DataBind();

            this.Pager.InitPager(currentPage, totalItems);
        }

        // Interface IShiftable
        public int GetNumberItems()
        {
            return totalItems;
        }

        public void SetCurrentPage(int currentPage)
        {
            this.currentPage = currentPage;
            SaveState();
            Reload();
        }
    }
}