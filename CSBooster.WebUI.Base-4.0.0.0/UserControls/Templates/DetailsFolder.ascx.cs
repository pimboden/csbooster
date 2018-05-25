// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class DetailsFolder : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        protected DataObject dataObject;
        private DataObjectFolder dataObjectFolder;
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
            dataObjectFolder = (DataObjectFolder)dataObject;
            udc = UserDataContext.GetUserDataContext();

            if (!IsPostBack)
            {
                LoadFolder();
            }
        }

        private void LoadFolder()
        {
            DataObjectList<DataObject> folderContentDB = DataObjects.Load<DataObject>(new QuickParameters { RelationParams = new RelationParams { ParentObjectID = dataObjectFolder.ObjectID }, ShowState = ObjectShowState.Published, Amount = 0, Direction = QuickSortDirection.Asc, PageNumber = 0, PageSize = 999999, SortBy = QuickSort.RelationSortNumber, Udc = UserDataContext.GetUserDataContext() });

            DataObjectList<DataObject> folderContent = folderContentDB.Clone(false);
            if (txtSortType.Text == "Alphabetic")
                folderContent.Sort((x, y) => (x.Title.CompareTo(y.Title)));
            else if (txtSortType.Text == "ObjectType")
                folderContent.Sort((x, y) => (x.ObjectType.CompareTo(y.ObjectType)));

            if (folderContent.Count > 0)
            {
                DETSUBTITLE.Text = dataObjectFolder.DescriptionLinked;

                string folderId = dataObjectFolder.ObjectID.Value.ToString();
                if (!string.IsNullOrEmpty(Request.QueryString["CFID"]))
                    folderId = Request.QueryString["CFID"].TrimEnd(';') + ";" + folderId + ";";
                folderId = folderId.TrimEnd(';');

                StringBuilder sbTable = new StringBuilder();
                sbTable.Append("<table width='100%' cellpadding='2' cellspacing='0'>");
                sbTable.AppendFormat("<tr><td class=''><b>{0}</b></td><td class=''><b>{1}</b></td><td class='' align='center'><b>{2}</b></td><td class='' align='center'><b>{3}</b></td></tr>", language.GetString("TitleFolderPreview"), language.GetString("TitleFolderTitle"), language.GetString("TitleFolderUser"), language.GetString("TitleFolderType"));

                foreach (DataObject DataObject in folderContent)
                {
                    string detailLink = string.Concat(Helper.GetDetailLink(DataObject.ObjectType, DataObject.ObjectID.Value.ToString(), true));
                    string objectTypeIcon = string.Format("<img src=\"/Library/Images/Layout/{0}\" alt=\"{1}\" />", Helper.GetObjectIcon(DataObject.ObjectType), Helper.GetObjectName(DataObject.ObjectType, true));
                    sbTable.Append("<tr>");
                    sbTable.AppendFormat("<td class=''><a href='{0}&CFID={1}' class=''><img src='{2}{3}'></a></td>", detailLink, folderId, _4screen.CSB.Common.SiteConfig.MediaDomainName, DataObject.GetImage(PictureVersion.S));
                    sbTable.AppendFormat("<td class=''>&nbsp;<a href='{0}&CFID={1}' class='' title='{2}'>{3}</a></td>", detailLink, folderId, DataObject.Description, DataObject.Title);
                    sbTable.AppendFormat("<td class='' align='center'><a href='{0}' class=''>{1}</td>", Helper.GetDetailLink("User", DataObject.Nickname), DataObject.Nickname);
                    sbTable.AppendFormat("<td class='' align='center'>&nbsp;{0}</td>", objectTypeIcon);
                    sbTable.Append("</tr>");
                }
                sbTable.Append("</table>");
                pnlResults.Controls.Add(new LiteralControl(sbTable.ToString()));
            }
            else
            {
                pnlResults.Controls.Add(new LiteralControl(language.GetString("LabelFolderEmpty")));
            }
        }

        protected void lbtnSortFolderSort_Click(object sender, EventArgs e)
        {
            txtSortType.Text = "FolderSort";
            LoadFolder();
        }

        protected void lbtnSortAlphabetical_Click(object sender, EventArgs e)
        {
            txtSortType.Text = "Alphabetic";
            LoadFolder();
        }

        protected void lbtnSortType_Click(object sender, EventArgs e)
        {
            txtSortType.Text = "ObjectType";
            LoadFolder();
        }
    }
}