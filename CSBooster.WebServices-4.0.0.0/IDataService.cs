// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.ServiceModel;
using System.ServiceModel.Web;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.WebServices
{
    [ServiceContract]
    public interface IDataService
    {
        /************************************************************
       * Generic methods
       ***********************************************************/

        [OperationContract]
        //[PrincipalPermission(SecurityAction.Demand, Role = "Basic")]
        [WebInvoke(Method = "GET", UriTemplate = "generic/{externalObjectId}", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Xml)]
        DataObjectGeneric GetGeneric(string externalObjectId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "generic", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Xml)]
        DataObjectGeneric CreateGeneric(DataObjectGeneric receivedGeneric);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "generic", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Xml)]
        DataObjectGeneric UpdateGeneric(DataObjectGeneric receivedGeneric);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "generic/{externalObjectId}", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Xml)]
        DataObjectGeneric DeleteGeneric(string externalObjectId);

        /************************************************************
       * News methods
       ***********************************************************/

        [OperationContract]
        //[PrincipalPermission(SecurityAction.Demand, Unrestricted = true)]
        [WebInvoke(Method = "GET", UriTemplate = "news/{externalObjectId}", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Xml)]
        DataObjectNews GetNews(string externalObjectId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "news", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Xml)]
        DataObjectNews CreateNews(DataObjectNews receivedNews);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "news", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Xml)]
        DataObjectNews UpdateNews(DataObjectNews receivedNews);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "news/{externalObjectId}", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Xml)]
        DataObjectNews DeleteNews(string externalObjectId);
    }
}