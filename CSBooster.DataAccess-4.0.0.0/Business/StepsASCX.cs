// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Data;

namespace _4screen.CSB.DataAccess.Business
{
    public class StepsASCX : System.Web.UI.UserControl
    {
        public Guid? CommunityID { get; set; }
        public Guid? ObjectID { get; set; }
        public int ObjectType { get; set; }
        public SiteContext SiteContext { get; set; }
        public AccessMode AccessMode { get; set; }
        public int StepNumber { get; set; }
        public string WizardId { get; set; }
        public Dictionary<string, string> Settings { get; set; }

        public virtual bool SaveStep()
        {
            return true;
        }

        public virtual bool SaveStep(ref NameValueCollection queryString)
        {
            return true;
        }

        public virtual bool SaveStep(int NextStep)
        {
            return true;
        }

        public virtual bool Cancel()
        {
            return true;
        }

        public string LoadInstanceData(Guid instanceID)
        {
            CSBooster_DataContext wdc = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            return wdc.hisp_Widget_LoadInstanceData(instanceID).ElementAtOrDefault(0).INS_XmlStateData;
        }

        public bool SaveInstanceData(Guid instanceID, string xml)
        {
            CSBooster_DataContext wdc = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            int status = wdc.hisp_Widget_SaveInstanceData(instanceID, xml);
            return status == 0 ? true : false;
        }
    }
}