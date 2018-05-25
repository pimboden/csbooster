// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget.UserControls.Repeaters
{
    public partial class AssemblyInfo : System.Web.UI.UserControl, IRepeater
    {
        private int numberItems;
        public bool BottomPagerVisible { get; set; }
        public bool TopPagerVisible { get; set; }
        public string TopPagerCustomText { get; set; }
        public string BottomPagerCustomText { get; set; }
        public  int PagerBreak { get; set; }
        public QuickParameters QuickParameters { get; set; }
        public string Title { get; set; }
        public bool HasContent { get; set; }
        public string ItemNameSingular { get; set; }
        public string ItemNamePlural { get; set; }
        public string OutputTemplate { get; set; }
        public bool RenderHtml { get; set; }


        protected override void OnInit(EventArgs e)
        {
            Reload();
        }

        public void Reload()
        {
            HasContent = false;
            string[] files = Directory.GetFiles(HttpContext.Current.Request.PhysicalApplicationPath + @"bin\", "*.dll");
            this.YMRP.DataSource = files;
            this.YMRP.DataBind();
            numberItems = files.Length;
        }

        protected void YMRP_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string item = e.Item.DataItem.ToString();
                PlaceHolder ph = e.Item.FindControl("Ph") as PlaceHolder;

                Control ctrl = LoadControl(string.Format("~/UserControls/Templates/{0}", this.OutputTemplate));

                ISettings settings = ctrl as ISettings;
                if (settings != null)
                {
                    if (settings.Settings == null)
                        settings.Settings = new System.Collections.Generic.Dictionary<string, object>();
                    settings.Settings.Add("File", item);
                    ph.Controls.Add(ctrl);
                    HasContent = true;
                }
            }
        }

        public int GetNumberItems()
        {
            return this.numberItems;
        }

    }
}