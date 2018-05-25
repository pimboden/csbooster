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
    public partial class SmallOutputHTMLContent : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        #region ISettings Members

        public Dictionary<string, object> Settings { get; set; }

        #endregion

        private Business.DataObjectHTMLContent dataObjectHTMLContent;
        private bool showCommunity = false;

        public DataAccess.Business.DataObject DataObject { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (DataObject is Business.DataObjectHTMLContent)
                dataObjectHTMLContent = (Business.DataObjectHTMLContent)DataObject;
            else
                dataObjectHTMLContent = DataAccess.Business.DataObject.Load<Business.DataObjectHTMLContent>(DataObject.ObjectID, null, false);

            LnkTitle.Text = dataObjectHTMLContent.Title;
            LnkTitle.NavigateUrl = Helper.GetDetailLink(dataObjectHTMLContent.ObjectType, dataObjectHTMLContent.ObjectID.Value.ToString());
            LitDesc.Text = dataObjectHTMLContent.Description.StripHTMLTags().CropString(50);
            if (string.IsNullOrEmpty(LitDesc.Text))
                LitDesc.Text = "-";

            PhTitle.ID = null;
            PhDesc.ID = null;
            LitDesc.ID = null;
            LnkTitle.ID = null;
        }

    }
}