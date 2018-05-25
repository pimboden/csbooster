// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class TagLocationSearchBoxCompact : System.Web.UI.UserControl, ITagLocationSearchBox
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");

        public bool IncludeGeoSearch { get; set; }

        public TextBox SearchTextBox
        {
            get { return IncludeGeoSearch ? this.TxtSearch : this.TxtSearch2; }
        }

        public TextBox LocationTextBox
        {
            get { return this.TxtLoc; }
        }

        public DropDownList RadiusDropDownList
        {
            get { return this.DDRadius; }
        }

        public DropDownList CountryDropDownList
        {
            get { return this.DDCountry; }
        }

        public HiddenField CoordsHiddenField
        {
            get { return this.HidCoords; }
        }

        public LinkButton FindLinkButton
        {
            get { return IncludeGeoSearch ? this.LbtnFind : this.LbtnFind2; }
        }

        public HyperLink ResetLink
        {
            get { return this.LnkReset; }
        }

        public HyperLink FindLink
        {
            get { return IncludeGeoSearch ? this.LnkFind : this.LnkFind2; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PnlGeo.Visible = IncludeGeoSearch;
            PnlSimple.Visible = !IncludeGeoSearch;
        }
    }
}