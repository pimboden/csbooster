// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace _4screen.CSB.DataAccess.Data
{
    public class CSBooster_DataAccessMRS : CSBooster_DataContext
    {
        [Function(Name = "dbo.hisp_Navigation_GetNavigations")]
        [ResultType(typeof(RecordNumerInfos))]
        [ResultType(typeof(hitbl_NavigationStructure_NST))]

        public IMultipleResults hisp_Navigation_GetNavigations([Parameter(Name = "Amount", DbType = "Int")] System.Nullable<int> amount, [Parameter(Name = "PageNumber", DbType = "Int")] System.Nullable<int> pageNumber, [Parameter(Name = "PageSize", DbType = "Int")] System.Nullable<int> pageSize)
        {

            IExecuteResult result =this.ExecuteMethodCall(this,((MethodInfo)(MethodInfo.GetCurrentMethod())),amount, pageNumber, pageSize);


            return (IMultipleResults)(result.ReturnValue);

        }

        public class RecordNumerInfos
        {
            public int PageTotal{get;set;}
            public int RowTotal{get;set;}
        }
    }
}
