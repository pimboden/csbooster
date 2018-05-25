using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace _4screen.CSB.Widget
{
    public interface IAdminPageMaster
    {
        IBreadCrumb BreadCrumb { get; set; }
    }
}
