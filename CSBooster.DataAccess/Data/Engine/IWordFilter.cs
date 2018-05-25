//******************************************************************************
//  Company:    4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:     CSBooster.DataAccess - FilterEngine
//
//  Created:    #1.0.0.0                10.08.2007 11:02:36 / aw
//  Updated:   
//******************************************************************************

using System;

namespace _4screen.CSB.DataAccess.Data
{
    internal interface IWordFilter
    {
        string Process(string value, Type type, FilterObjectTypes filterObjectType, Guid objectId, Guid userId);
    }
}