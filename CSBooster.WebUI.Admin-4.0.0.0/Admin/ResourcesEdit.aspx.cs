// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;

namespace _4screen.CSB.WebUI.Admin
{
    public partial class ResourcesEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //((MasterPages_SiteAdmin)this.Master).SetNavigationItem("ResourcesEdit");

            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.SiteAdmin);

            // TODO: Get resrouce name from query string

            if (!IsPostBack)
            {
                ResXResourceReader resxReader = new ResXResourceReader(string.Format("{0}/App_Resources/Templates.resx", WebRootPath.Instance.ToString()));

                this.RepResources.DataSource = resxReader.Cast<DictionaryEntry>().OrderBy(x => x.Key.ToString());
                this.RepResources.DataBind();

                resxReader.Close();
            }
        }

        protected void RepResourcesItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DictionaryEntry resource = (DictionaryEntry)e.Item.DataItem;

                e.Item.ID = resource.Key.ToString();

                Literal name = (Literal)e.Item.FindControl("LitName");
                name.Text = resource.Key.ToString();

                PlaceHolder ph = (PlaceHolder)e.Item.FindControl("PhText");
                string value = resource.Value.ToString();
                if (value.Length < 100)
                {
                    TextBox textBox = new TextBox();
                    textBox.Text = value;
                    textBox.ID = "TxtValue";
                    textBox.CssClass = "CSB_admin_monotype";
                    textBox.Attributes.Add("style", "width:100%");
                    ph.Controls.Add(textBox);
                }
                else
                {
                    TextBox textBox = new TextBox();
                    textBox.Text = value;
                    textBox.TextMode = TextBoxMode.MultiLine;
                    textBox.Rows = 5;
                    textBox.ID = "TxtValue";
                    textBox.CssClass = "CSB_admin_monotype";
                    textBox.Attributes.Add("style", "width:100%");
                    ph.Controls.Add(textBox);
                }
            }
        }

        protected void OnSaveClick(object sender, EventArgs e)
        {
            List<string> resourceNames = new List<string>();
            ResXResourceReader resxReader = new ResXResourceReader(string.Format("{0}/App_Resources/Templates.resx", WebRootPath.Instance.ToString()));
            IDictionaryEnumerator resxEnumerator = resxReader.GetEnumerator();
            while (resxEnumerator.MoveNext())
            {
                resourceNames.Add(resxEnumerator.Key.ToString());
            }
            resxReader.Close();

            ResXResourceWriter resxWriter = new ResXResourceWriter(string.Format("{0}\\App_Resources/Templates.resx", WebRootPath.Instance.ToString()));
            try
            {
                foreach (string name in resourceNames)
                {
                    string value = Request.Form[string.Format("{0}${1}$TxtValue", this.RepResources.UniqueID, name)];
                    resxWriter.AddResource(name, value);
                }
                resxWriter.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}
