// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using Telerik.Web.UI;

namespace _4screen.CSB.WebUI.UserControls.Repeaters
{
    public partial class TelerikRotator : System.Web.UI.UserControl, IRepeater, ISettings
    {
        public bool BottomPagerVisible { get; set; }
        public bool TopPagerVisible { get; set; }
        public string TopPagerCustomText { get; set; }
        public string BottomPagerCustomText { get; set; }
        public int PagerBreak { get; set; }
        public QuickParameters QuickParameters { get; set; }
        public string Title { get; set; }
        public bool HasContent { get; set; }
        public string ItemNameSingular { get; set; }
        public string ItemNamePlural { get; set; }
        public string OutputTemplate { get; set; }
        public bool RenderHtml { get; set; }
        public Dictionary<string, object> Settings { get; set; }

        protected override void OnInit(EventArgs e)
        {
            Rr.Width = (int)Settings["Width"];
            Rr.ItemWidth = (int)Settings["Width"];
            Reload();
        }

        protected void OnRotatorItemDataBound(object sender, RadRotatorEventArgs e)
        {
            DataObject dataObject = (DataObject)e.Item.DataItem;
            PlaceHolder ph = (PlaceHolder)e.Item.FindControl("PhItem");
            Control ctrl = LoadControl(string.Format("~/UserControls/Templates/{0}", this.OutputTemplate));
            ((IDataObjectWorker)ctrl).DataObject = dataObject;
            ph.Controls.Add(ctrl);
        }

        public void Reload()
        {
            DataObjectList<DataObject> list = DataObjects.LoadByReflection(QuickParameters);
            Rr.DataSource = list;
            Rr.DataBind();
            HasContent = list.ItemTotal > 0;
        }
    }
}