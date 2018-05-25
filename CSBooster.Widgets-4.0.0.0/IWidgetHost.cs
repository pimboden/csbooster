// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Data;

namespace _4screen.CSB.Widget
{
    public interface IWidgetHost
    {
        bool IsReadOnly { get; set; }
        int ColumnWidth { get; set; }
        int ContentPadding { get; set; }
        Guid ParentCommunityID { get; set; }
        int ParentObjectType { get; set; }
        PageType ParentPageType { get; set; }
        OutputTemplateElement OutputTemplate { get; set; }
        hitbl_WidgetInstance_IN WidgetInstance { get; set; }
        SiteContext SiteContext { get; set; }
        Guid TagWord { get; set; }
        string LangCode { get; set; }
        Guid InstanceID { set; get; }
        void SetWidgetTitle(string title);
    }
}