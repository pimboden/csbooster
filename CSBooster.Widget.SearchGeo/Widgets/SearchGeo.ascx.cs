using System.Web.UI;

namespace _4screen.CSB.Widget
{
    public partial class SearchGeo : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            Control searchBox = LoadControl("~/Pages/Overview/UserControls/TagLocationSearchBox.ascx");
            searchBox.ID = "SB";
            PnlCnt.Controls.Add(searchBox);
            return true;
        }
    }
}