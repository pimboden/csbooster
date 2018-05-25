using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
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
        HtmlGenericControl BodyTag { get; set; }
    }
}
