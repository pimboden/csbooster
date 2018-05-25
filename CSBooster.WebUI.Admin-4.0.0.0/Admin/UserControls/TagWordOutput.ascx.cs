// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.WebUI
{
    public partial class Admin_UserControls_TagWordOutput : System.Web.UI.UserControl
    {
        private UserDataContext userDataContext;

        public DataObjectTag Tag { get; set; }

        private bool IsSynomyTag = false;
        private bool IsGroupTag = false;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void RenderControls()
        {
            userDataContext = UserDataContext.GetUserDataContext();
            LitTW.Text = Tag.Title;
            string subTags = GetSubTags();
            string synonyms = GetSynonyms();
            string synonymFor = GetSynonymFor();
            LitGR.Text = string.Format("<div style='width:150px; overflow:hidden;' title='{0}'>{1}</div>", subTags.Replace("'", ""), subTags);
            LitSyn.Text = string.Format("<div style='width:150px; overflow:hidden;' title='{0}'>{1}</div>", synonyms.Replace("'", ""), synonyms);
            if (!IsSynomyTag)
                LitSynF.Text = string.Format("<div style='width:150px; overflow:hidden;' title='{0}'>{1}</div>", synonymFor.Replace("'", ""), synonymFor);
            hlnkSyn.NavigateUrl = string.Format("/Admin/TagWordSynonym.aspx?OID={0}",  Tag.ObjectID.Value);
            hlnkGR.NavigateUrl = string.Format("/Admin/TagWordGroup.aspx?OID={0}",  Tag.ObjectID.Value);
            if (synonymFor.Length > 0)
            {
                //It is already synonym for other... therefore can't chose own synonyms
                hlnkSyn.Visible = false;
            }
        }

        private string GetSynonyms()
        {
            string retVal = string.Empty;
            QuickParameters qp = new QuickParameters
                                        {
                                            Udc = userDataContext,
                                            ShowState = null,
                                            IgnoreCache = true,
                                            ObjectType = 5,
                                            RelationParams = new RelationParams { RelationType = "Synonym", ParentObjectID = Tag.ObjectID, ExcludeSystemObjects = false }
                                        };
            DataObjectList<DataObject> tags = DataObjects.Load<DataObject>(qp);
            IsSynomyTag = tags.Count > 0;
            foreach (DataObject tag in tags)
            {
                retVal += string.Format("{0},", tag.Title);
            }
            return retVal.TrimEnd(',');
        }

        private string GetSynonymFor()
        {
            string retVal = string.Empty;
            QuickParameters qp = new QuickParameters
            {
                Udc = userDataContext,
                ShowState = null,
                IgnoreCache = true,
                ObjectType = 5,
                RelationParams = new RelationParams { RelationType = "Synonym", ChildObjectType = 5, ChildObjectID = Tag.ObjectID, ExcludeSystemObjects = false }
            };
            DataObjectList<DataObject> tags = DataObjects.Load<DataObject>(qp);
            foreach (DataObject dataObj in tags)
            {
                retVal += string.Format("{0},", dataObj.Title);
            }
            return retVal.TrimEnd(',');
        }

        private string GetSubTags()
        {
            string retVal = string.Empty;
            QuickParameters qp = new QuickParameters
            {
                Udc = userDataContext,

                RelationParams = new RelationParams { RelationType = "Hierarchy", ParentObjectID = Tag.ObjectID, ExcludeSystemObjects = false }
            };
            DataObjectList<DataObject> tags = DataObjects.Load<DataObject>(qp);
            IsGroupTag = tags.Count > 0;
            foreach (DataObject dataObj in tags)
            {
                retVal += string.Format("{0},", dataObj.Title);
            }
            return retVal.TrimEnd(',');
        }



        protected void OnGrClick(object sender, EventArgs e)
        {
        }

        protected void OnSynClick(object sender, EventArgs e)
        {
        }

    }
}
