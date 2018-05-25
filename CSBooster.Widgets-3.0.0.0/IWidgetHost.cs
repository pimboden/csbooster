//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#0.0.0.0		26.03.2007 / PI
//  Updated:    #0.5.1.0        13.06.2007 / TS
//									- Widget-Title extended with AddLinkAll methode
//******************************************************************************
using System;
using _4screen.CSB.Common;

namespace _4screen.CSB.Widget
{
    public interface IWidgetHost
    {
        void SaveState(string state);
        string GetState();
        void Maximize();
        void Minimize();
        void Close();
        bool IsFirstLoad { get; }
        void ExtendWidgetTitle(string titleExtension);
        void SetEditWidget(bool EditMode);
        int ColumnWidth { get; set; }
        Guid ParentCommunityID { get; set; }
        int ParentObjectType { get; set; }
        PageType ParentPageType { get; set; }
        OutputTemplateElement OutputTemplate { get; set; }
    }
}