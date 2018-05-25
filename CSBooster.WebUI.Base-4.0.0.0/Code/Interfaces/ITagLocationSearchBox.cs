using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4screen.CSB.WebUI
{
    public interface ITagLocationSearchBox
    {
        bool IncludeGeoSearch { get; set; }
        TextBox SearchTextBox { get; }
        TextBox LocationTextBox { get; }
        DropDownList RadiusDropDownList { get; }
        DropDownList CountryDropDownList { get; }
        HiddenField CoordsHiddenField { get; }
        LinkButton FindLinkButton { get; }
        HyperLink ResetLink { get; }
        HyperLink FindLink { get; }
    }
}
