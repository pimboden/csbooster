// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;

namespace _4screen.CSB.DataObj.UserControls.Templates
{
    public partial class SmallOutputSurvey : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        private Business.DataObjectSurvey dataObjectSurvey;
        public DataObject DataObject { get; set; }
        public Dictionary<string, object> Settings { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (DataObject is Business.DataObjectSurvey)
                dataObjectSurvey = (Business.DataObjectSurvey)DataObject;
            else
                dataObjectSurvey = DataAccess.Business.DataObject.Load<Business.DataObjectSurvey>(DataObject.ObjectID, null, false);


            lnkTitle.Text = dataObjectSurvey.Title;
            lnkTitle.NavigateUrl = Helper.GetDetailLink(dataObjectSurvey.ObjectType, dataObjectSurvey.ObjectID.Value.ToString());
            litDesc.Text = dataObjectSurvey.HeaderLinked;
            if (string.IsNullOrEmpty(litDesc.Text))
                litDesc.Text = "-";

            litDesc.ID = null;
            lnkTitle.ID = null;
        }
    }
}