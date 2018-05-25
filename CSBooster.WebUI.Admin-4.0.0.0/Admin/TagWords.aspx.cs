// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Admin
{
    public partial class TagWords : System.Web.UI.Page, IReloadable
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Admin");

        protected void Page_Load(object sender, EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.SiteAdmin);

            Search();
        }

        public void Reload()
        {
            Search();
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            if (txtSrchGnr.Text.Trim().Length > 0)
            {
                QuickParameters quickParameters = new QuickParameters();
                quickParameters.Udc = UserDataContext.GetUserDataContext();
                quickParameters.ObjectType = 5;
                quickParameters.PageSize = 50;
                quickParameters.Amount = 50;
                quickParameters.Title = txtSrchGnr.Text;
                quickParameters.IgnoreCache = true;
                quickParameters.SortBy = QuickSort.Title;
                quickParameters.Direction = QuickSortDirection.Asc;
                DataObjectList<DataObjectTag> tags = DataObjects.Load<DataObjectTag>(quickParameters);
                rptTag.DataSource = tags;
                rptTag.DataBind();
            }
        }

        protected void rptTag_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataObjectTag qo = e.Item.DataItem as DataObjectTag;
                e.Item.ID = qo.ObjectID.Value.ToString();
                Admin_UserControls_TagWordOutput userOutput = e.Item.FindControl("TOut") as Admin_UserControls_TagWordOutput;
                userOutput.Tag = qo;
                userOutput.RenderControls();
            }
        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            if (txtSrchGnr.Text.Trim().Length > 0)
            {
                if (DataObjectTag.GetTagID(txtSrchGnr.Text.Trim()) == Guid.Empty)
                {
                    DataObjectTag tag = new DataObjectTag();
                    tag.Title = txtSrchGnr.Text;
                    tag.CommunityID = Constants.DEFAULT_COMMUNITY_ID.ToGuid();
                    tag.UserID = Constants.ANONYMOUS_USERID.ToGuid();
                    tag.ShowState = ObjectShowState.Published;
                    tag.Status = ObjectStatus.Public;
                    tag.Insert(UserDataContext.GetUserDataContext());

                    HttpRuntime.Cache.Remove("MainTagsAndSynonyms");
                }
            }
            Search();
        }

    }
}
