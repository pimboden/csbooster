// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
namespace _4screen.CSB.DataObj.Data
{
    public partial class SurveyDataClassDataContext
    {
        public SurveyDataClassDataContext() :
            base(System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString, mappingSource)
        {
            OnCreated();
        }

    }
}
