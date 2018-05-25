// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.ServiceModel;
using System.ServiceModel.Activation;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.WebServices
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DataService : IDataService
    {
        /************************************************************
       * Generic methods
       ***********************************************************/

        public DataObjectGeneric GetGeneric(string externalObjectId)
        {
            return DataServiceGeneric.GetGeneric(externalObjectId);
        }

        public DataObjectGeneric CreateGeneric(DataObjectGeneric receivedGeneric)
        {
            return DataServiceGeneric.CreateGeneric(receivedGeneric);
        }

        public DataObjectGeneric UpdateGeneric(DataObjectGeneric receivedGeneric)
        {
            return DataServiceGeneric.UpdateGeneric(receivedGeneric);
        }

        public DataObjectGeneric DeleteGeneric(string externalObjectId)
        {
            return DataServiceGeneric.DeleteGeneric(externalObjectId);
        }

        /************************************************************
       * News methods
       ***********************************************************/

        public DataObjectNews GetNews(string externalObjectId)
        {
            return DataServiceNews.GetNews(externalObjectId);
        }

        public DataObjectNews CreateNews(DataObjectNews receivedNews)
        {
            return DataServiceNews.CreateNews(receivedNews);
        }

        public DataObjectNews UpdateNews(DataObjectNews receivedNews)
        {
            return DataServiceNews.UpdateNews(receivedNews);
        }

        public DataObjectNews DeleteNews(string externalObjectId)
        {
            return DataServiceNews.DeleteNews(externalObjectId);
        }
    }
}