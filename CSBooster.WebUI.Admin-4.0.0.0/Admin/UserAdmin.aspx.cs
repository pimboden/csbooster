// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Admin
{
    public partial class UserAdmin : System.Web.UI.Page, IReloadable
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
            QuickParametersUser quickParameters = new QuickParametersUser();
            quickParameters.Udc = UserDataContext.GetUserDataContext();
            quickParameters.ObjectType =Helper.GetObjectTypeNumericID("User");
            quickParameters.PageSize = 50;
            quickParameters.Amount = 50;
            quickParameters.Nickname = txtSrchGnr.Text;
            if (CBLocked.Checked)
                quickParameters.IsUserLocked = CBLocked.Checked;
            quickParameters.IgnoreCache = true;
            DataObjectList<DataObjectUser> users = DataObjects.Load<DataObjectUser>(quickParameters);
            bool removed = users.Remove(users.Find(x => x.ObjectID.Value == Constants.ADMIN_USERID.ToGuid()));
            rptUser.DataSource = users;
            rptUser.DataBind();
        }

        protected void rptUser_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataObjectUser qo = e.Item.DataItem as DataObjectUser;
                e.Item.ID = qo.ObjectID.Value.ToString();
                Admin_UserControls_UserOutput userOutput = e.Item.FindControl("UOut") as Admin_UserControls_UserOutput;
                userOutput.User = qo;
                userOutput.RenderControls();
            }
        }
    }
}