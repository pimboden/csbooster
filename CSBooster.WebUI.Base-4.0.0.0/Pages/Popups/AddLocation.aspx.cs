using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class AddLocation : System.Web.UI.Page
    {
        private StepsASCX control;
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        protected void Page_Load(object sender, EventArgs e)
        {
            string locationControl = WizardSection.CachedInstance.Wizards[Helper.GetObjectType("Location").UploadWizard].Steps[0].Control;
            control = (StepsASCX)LoadControl(locationControl);
            control.ObjectID = Request.QueryString["OID"].ToGuid();
            control.CommunityID = Request.QueryString["CN"].ToGuid();
            PhLocation.Controls.Add(control);
        }

        protected void OnSaveClick(object sender, EventArgs e)
        {
            NameValueCollection dummyCollection = new NameValueCollection();
            bool locationSaved = control.SaveStep(ref dummyCollection);
            if (locationSaved)
                ScriptManager.RegisterStartupScript(this, GetType(), "CloseWindow", "$telerik.$(function() { GetRadWindow().Close({ TargetWindow: '" + Request.QueryString["ParentRadWin"] + "' }); } );", true);
        }

        protected void OnCancelClick(object sender, EventArgs e)
        {
            DataObjectLocation location = DataObject.Load<DataObjectLocation>(control.ObjectID);
            if (location.State != ObjectState.Added)
                location.Delete(UserDataContext.GetUserDataContext());
            ScriptManager.RegisterStartupScript(this, GetType(), "CloseWindow", "$telerik.$(function() { GetRadWindow().Close(); } );", true);
        }
    }
}
