// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;

namespace _4screen.CSB.DataAccess.Data
{
    internal interface IWordFilter
    {
        string Process(string value, Type type, FilterObjectTypes filterObjectType, Guid objectId, Guid userId);
    }
}