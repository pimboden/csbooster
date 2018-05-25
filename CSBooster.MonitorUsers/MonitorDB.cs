using System.Configuration;
namespace CSBooster.MonitorUsers
{
	partial class MonitorDBDataContext
	{
		public MonitorDBDataContext()
			: base(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString, mappingSource)
		{
			//
			// TODO: Add constructor logic here
			//
		}

	}
}