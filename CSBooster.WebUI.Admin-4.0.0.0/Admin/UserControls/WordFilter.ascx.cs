// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Data;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Admin.UserControls
{
    public partial class WordFilter : System.Web.UI.UserControl
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Admin");

        public hitbl_FilterBadWords_FBW WordFilterObject { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void RenderControls(bool isPostBack)
        {
            if (!isPostBack)
            {
                this.TxtWord.Text = WordFilterObject.FBW_Word;
                this.CBExact.Checked = WordFilterObject.FBW_IsExactMatch;
                this.DDLAction.SelectedValue = WordFilterObject.FBW_Action;
            }
            this.LbtnSave.Click +=
            delegate(object sender1, EventArgs e1)
            {
                OnSaveClick(sender1, e1, WordFilterObject);
            };
            this.LbtnDelete.Click +=
            delegate(object sender1, EventArgs e1)
            {
                OnDeleteClick(sender1, e1, WordFilterObject);
            };
        }

        protected void OnSaveClick(object sender, EventArgs e, hitbl_FilterBadWords_FBW WordFilterObject)
        {
            if (!string.IsNullOrEmpty(this.TxtWord.Text))
            {
                CSBooster_DataContext csbDC = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
                csbDC.hitbl_FilterBadWords_FBWs.Attach(WordFilterObject);

                WordFilterObject.FBW_Word = this.TxtWord.Text;
                WordFilterObject.FBW_IsExactMatch = this.CBExact.Checked;
                WordFilterObject.FBW_Action = this.DDLAction.SelectedValue;

                csbDC.SubmitChanges();
            }
        }

        public void OnDeleteClick(object sender, EventArgs e, hitbl_FilterBadWords_FBW WordFilterObject)
        {
            CSBooster_DataContext csbDC = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            csbDC.hitbl_FilterBadWords_FBWs.Attach(WordFilterObject);
            csbDC.hitbl_FilterBadWords_FBWs.DeleteOnSubmit(WordFilterObject);
            csbDC.SubmitChanges();

            ((IReloadable)this.Page).Reload();
        }
    }
}