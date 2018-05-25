//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#2.0.0.0		14.07.2008 / PI
//  Updated:   
//******************************************************************************
using System;
namespace _4screen.CSB.Widget
{
    public interface ISourceAndTagSelector
    {
        Guid CommunityID { get; set; }
        string TagList1 { get; set; }
        string TagList2 { get; set; }
        string TagList3 { get; set; }
        string DataSourceIDs { get; set; }
        string DataSourceSelection { get; set; }
        Guid? UserID { get; set; }
        bool IncludeGroups { get; }
        bool Enabled { get; set; }


        void GetProperties();
    }
}