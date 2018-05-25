// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Data;
using _4screen.CSB.WebUI.Admin.UserControls;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Admin
{
    public partial class WordFilterAdmin : System.Web.UI.Page, IReloadable
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Admin");

        protected void Page_Load(object sender, EventArgs e)
        {
            //((MasterPages_SiteAdmin)this.Master).SetNavigationItem("WordFilter");
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.SiteAdmin);

            foreach (ListItem item in this.DDLAction.Items)
            {
                item.Text = language.GetString(string.Format("LableFilterAction{0}", item.Value)); 
            }

            Reload();
        }

        public void Reload()
        {
            CSBooster_DataContext csbDC = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            RepFilterWords.DataSource = csbDC.hitbl_FilterBadWords_FBWs;
            RepFilterWords.DataBind();
        }

        protected void RepFilterWordsItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            hitbl_FilterBadWords_FBW wordFilter = e.Item.DataItem as hitbl_FilterBadWords_FBW;

            e.Item.ID = wordFilter.FBW_ID.ToString();

            WordFilter wordFilterOutput = e.Item.FindControl("WF") as WordFilter;
            wordFilterOutput.WordFilterObject = wordFilter;
            if (Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"].EndsWith("LbtnSave"))
                wordFilterOutput.RenderControls(true);
            else
                wordFilterOutput.RenderControls(false);
        }

        public void OnAddClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TxtWord.Text))
            {
                hitbl_FilterBadWords_FBW wordFilter = new hitbl_FilterBadWords_FBW();
                wordFilter.FBW_ID = Guid.NewGuid();
                wordFilter.FBW_Word = this.TxtWord.Text;
                wordFilter.FBW_IsExactMatch = this.CBExact.Checked;
                wordFilter.FBW_Action = this.DDLAction.SelectedValue;

                CSBooster_DataContext csbDC = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
                csbDC.hitbl_FilterBadWords_FBWs.InsertOnSubmit(wordFilter);
                csbDC.SubmitChanges();

                Reload();
            }
        }
    }
}
