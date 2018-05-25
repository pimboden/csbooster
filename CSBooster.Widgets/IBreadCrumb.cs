using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public interface IBreadCrumb
    {
        string BreadCrumpImage { get; set; }
        List<Control> BreadCrumbs { get; set; }
        void RenderBreadCrumbs();
        void RenderDetailPageBreadCrumbs(DataObject dataObject);
        void RenderOverviewPageBreadCrumbs(QuickParameters quickParameters);
        void RenderAdminPageBreadCrumbs(string title);
    }
}
