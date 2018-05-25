using System.Configuration;

namespace _4screen.CSB.Widget.Data
{
    public partial class CSBDataDataContext
    {
        public CSBDataDataContext() : base(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString, mappingSource)
        {
        }
    }
}