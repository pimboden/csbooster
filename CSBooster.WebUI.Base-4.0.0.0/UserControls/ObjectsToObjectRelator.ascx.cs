// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.WebUI.UserControls.Dashboard;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using Telerik.Web.UI;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class ObjectsToObjectRelator : System.Web.UI.UserControl, IObjectsToObjectRelator
    {
        private int maxChildObjects = 500;
        private bool excludeSystemObjects = true;
        private DataObject parentDataObject = null;
        private DataObjectList<DataObject> relatedObjects;
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");

        public Guid? ParentObjectID { get; set; }
        public List<string> ChildObjectTypes { get; set; }
        public Guid? UserId { get; set; }
        public string RelationType { get; set; }

        public bool ExcludeSystemObjects
        {
            get { return excludeSystemObjects; }
            set { excludeSystemObjects = value; }
        }

        public int MaxChildObjects
        {
            get { return maxChildObjects; }
            set { maxChildObjects = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(Page).Scripts.Add(new ScriptReference("/library/scripts/jquery.js"));
            ScriptManager.GetCurrent(Page).Scripts.Add(new ScriptReference("/library/scripts/jquery.ui.js"));

            if (ChildObjectTypes.Count == 1)
            {
                LabelChooseObjects.LabelKey = "LabelChoose" + Helper.GetObjectType(ChildObjectTypes[0]).Id;
                LabelRelatedObjects.LabelKey = "LabelRelated" + Helper.GetObjectType(ChildObjectTypes[0]).Id;
                LabelChooseObjects.TooltipKey = "TooltipChoose" + Helper.GetObjectType(ChildObjectTypes[0]).Id;
                LabelRelatedObjects.TooltipKey = "TooltipRelated" + Helper.GetObjectType(ChildObjectTypes[0]).Id;
            }

            if (ParentObjectID.HasValue)
            {
                parentDataObject = DataObject.Load<DataObject>(ParentObjectID, null, false);
                if (parentDataObject.State != ObjectState.Added)
                {
                    MyContent.PageSize = 15;
                    MyContent.Sort = CustomizationSection.CachedInstance.MyContent.DefaultSortOrder;
                    MyContent.SortDirection = MyContent.Sort == QuickSort.Title ? QuickSortDirection.Asc : QuickSortDirection.Desc;
                    MyContent.MyContentMode = MyContentMode.Related;
                    MyContent.Settings = new Dictionary<string, object>();
                    MyContent.ObjectType = Helper.GetObjectType(ChildObjectTypes[0]).NumericId;

                    MyContentSearch.MyContentMode = MyContentMode.Related;
                    MyContentSearch.ObjectTypes = ChildObjectTypes;
                    MyContentSearch.ObjectType = Helper.GetObjectType(ChildObjectTypes[0]).NumericId;
                    MyContentSearch.MyContent = MyContent;

                    relatedObjects = DataObjects.Load<DataObject>(new QuickParameters
                    {
                        ObjectTypes = string.Join(",", ChildObjectTypes.ConvertAll(x => Helper.GetObjectTypeNumericID(x).ToString()).ToArray()),
                        UserID = UserId,
                        ShowState = ObjectShowState.Published,
                        Amount = 0,
                        Direction = QuickSortDirection.Asc,
                        PageNumber = 0,
                        PageSize = 999999,
                        SortBy = QuickSort.RelationSortNumber,
                        Udc = UserDataContext.GetUserDataContext(),
                        IgnoreCache = true,
                        QuerySourceType = QuerySourceType.MyContent,
                        RelationParams = new RelationParams
                        {
                            ParentObjectID = ParentObjectID,
                            Udc = UserDataContext.GetUserDataContext(),
                            ExcludeSystemObjects = ExcludeSystemObjects,
                            RelationType = RelationType
                        }
                    });

                    foreach (DataObject relatedObject in relatedObjects)
                    {
                        Control control = LoadControl("~/UserControls/Dashboard/MyContentRelatedBox.ascx");
                        ((MyContentRelatedBox)control).DataObject = relatedObject;
                        ((MyContentRelatedBox)control).IsSource = false;
                        phRelObj.Controls.Add(control);
                    }
                }
            }
        }

        public bool Save()
        {
            string[] relatedObjectIds = Request.Form.GetValues("relatedObjectId") ?? new string[0];

            foreach (DataObject removedDataObject in relatedObjects)
                DataObject.RelDelete(new RelationParams { ParentObjectID = ParentObjectID, RelationType = RelationType, ChildObjectID = removedDataObject.ObjectID, ChildObjectType = removedDataObject.ObjectType, Udc = UserDataContext.GetUserDataContext(), ExcludeSystemObjects = ExcludeSystemObjects });

            int sortOrder = 0;
            foreach (string relatedObjectId in relatedObjectIds)
            {
                DataObject addedDataObject = DataObject.Load<DataObject>(relatedObjectId.ToGuid(), null, false);
                if (addedDataObject.State != ObjectState.Added)
                {
                    DataObject.RelInsert(new RelationParams { ParentObjectID = ParentObjectID, ParentObjectType = parentDataObject.ObjectType, RelationType = RelationType, ChildObjectID = addedDataObject.ObjectID, ChildObjectType = addedDataObject.ObjectType }, sortOrder);
                    sortOrder++;
                }
            }
            return true;
        }
    }
}