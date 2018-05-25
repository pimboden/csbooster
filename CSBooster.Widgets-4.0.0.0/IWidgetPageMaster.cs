// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Web.UI.HtmlControls;

namespace _4screen.CSB.Widget
{
    public interface IWidgetPageMaster
    {
        string HeaderStyle { get; set; }
        string BodyStyle { get; set; }
        string FooterStyle { get; set; }
        IBreadCrumb BreadCrumb { get; set; }
        HtmlMeta MetaDescription { get; set; }
        HtmlMeta MetaKeywords { get; set; }
        HtmlMeta MetaModifiedDate { get; set; }
        HtmlMeta MetaOgTitle { get; set; }
        HtmlMeta MetaOgType { get; set; }
        HtmlMeta MetaOgUrl { get; set; }
        HtmlMeta MetaOgImage { get; set; }
        HtmlMeta MetaOgSiteName { get; set; }
        HtmlMeta MetaOgDescription { get; set; }
        HtmlMeta MetaOgLatitude { get; set; }
        HtmlMeta MetaOgLongitude { get; set; }
        HtmlMeta MetaOgStreet { get; set; }
        HtmlMeta MetaOgCity { get; set; }
        HtmlMeta MetaOgZipCode { get; set; }
        HtmlMeta MetaOgCountryCode { get; set; }
        HtmlLink RssLink { get; set; }
        HtmlGenericControl BodyTag { get; set; }
    }
}
