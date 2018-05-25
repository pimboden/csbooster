//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		26.03.2007 / PI
//  Updated:   
//******************************************************************************

using System;
using _4screen.CSB.Common;

namespace _4screen.CSB.Widget
{
    public interface IDataObjectWidget
    {
        Guid CommunityID { get; set; }
        SiteContext SiteContext { get; set; }
        Guid TagWord { get; set; }
        string LangCode { get; set; }

        Guid InstanceID { set; get; }
    }
}