// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI.WebControls;

namespace _4screen.CSB.WebUI.UserControls.Wizards.Helpers
{
    public partial class NewsLinks : System.Web.UI.UserControl
    {
        private string title;
        private Uri url;

        public LinkButton RemoveButton { get; set; }

        public string Title
        {
            get { return this.TxtTitle.Text; }
            set { title = value; }
        }

        public Uri URL
        {
            get { return !string.IsNullOrEmpty(this.TxtURL.Text) ? new Uri(this.TxtURL.Text) : null; }
            set { url = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.TxtTitle.Text = title != null ? title : string.Empty;
            this.TxtURL.Text = url != null ? url.ToString() : string.Empty;

            if (RemoveButton != null)
                this.PhRemove.Controls.Add(RemoveButton);
        }
    }
}