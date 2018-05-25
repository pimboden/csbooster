// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.DataAccess
{
    public interface IDataObjectFilter
    {
        void InsertFilter(DataObject dataObject);

        void UpdateFilter(DataObject dataObject, DataObject newDataObject);
    }
}
