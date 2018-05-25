//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#2.0.0.0		17.02.2009 / AW
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _4screen.CSB.Widget
{
    public interface ISettings
    {
        Dictionary<string, object> Settings { get; set; }
    }
}
