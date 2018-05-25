// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Admin
{
    public partial class TagWordGroup : System.Web.UI.Page
    {
        private string returnUrl;
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        Control ctrl = null;
        IObjectsToObjectRelator iOTOR = null;
        private DataObjectTag tag;
        private Guid? ObjectID;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.SiteAdmin);

            ObjectID = Request.QueryString["OID"].ToNullableGuid();
            tag = DataObject.Load<DataObjectTag>(ObjectID, null, true);
            ctrl = LoadControl("~/UserControls/ObjectsToObjectRelator.ascx");
            iOTOR = ctrl as IObjectsToObjectRelator;
            iOTOR.ParentObjectID = ObjectID;
            iOTOR.ExcludeSystemObjects = false;
            //iOTOR.LabelText = "Sub-Tags";
            iOTOR.RelationType = "Hierarchy";
            iOTOR.ChildObjectTypes = new List<string>() { "Tag" };
            phOTOR.Controls.Add(ctrl);

        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            iOTOR.Save();
            Response.Redirect("/Admin/TagWords.aspx");
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Admin/TagWords.aspx");
        }
    }
}
