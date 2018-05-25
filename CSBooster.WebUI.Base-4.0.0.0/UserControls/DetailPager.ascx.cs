using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class DetailPager : System.Web.UI.UserControl, IDataObjectWorker
    {
        private QuickParameters quickParameters;
        private int pageSize = 10;

        public DataObject DataObject { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string cookieName = "qm" + Helper.GetObjectType(DataObject.ObjectType).Id;
            if (Request.Cookies[cookieName] != null)
            {
                string qmCookie = Request.Cookies[cookieName].Value;
                quickParameters = QuickParameters.FromJson(HttpUtility.UrlDecode(qmCookie).Replace("&quot;", "\""));
                quickParameters.Udc = UserDataContext.GetUserDataContext();
                quickParameters.CurrentObjectID = DataObject.ObjectID;
                quickParameters.PageSize = pageSize;
                if (quickParameters.SortBy == QuickSort.Random)
                {
                    quickParameters.SortBy = QuickSort.InsertedDate;
                    quickParameters.Amount = 0;
                }
            }
            else
            {
                LoadDefaultQuickParameters();
            }

            DataObjectList<DataObject> dataObjects = DataObjects.Load<DataObject>(quickParameters);
            int currentIndex = dataObjects.FindIndex(x => x.ObjectID == DataObject.ObjectID);
            if (currentIndex != -1 && dataObjects.Count == 1)
            {
                Visible = false;
            }
            else if (currentIndex == -1)
            {
                LoadDefaultQuickParameters();
                dataObjects = DataObjects.Load<DataObject>(quickParameters);
                currentIndex = dataObjects.FindIndex(x => x.ObjectID == DataObject.ObjectID);
            }

            Guid previousObjectId;
            Guid nextObjectId;
            if (currentIndex != -1 && dataObjects.Count > 1)
            {
                // Get previous object
                if (currentIndex > 0)
                {
                    previousObjectId = dataObjects[currentIndex - 1].ObjectID.Value;
                }
                else
                {
                    if (dataObjects.PageTotal > 1) // The previous object is on another page
                    {
                        quickParameters.CurrentObjectID = null;
                        if (dataObjects.PageNumber > 1)
                            quickParameters.PageNumber = dataObjects.PageNumber - 1;
                        else
                            quickParameters.PageNumber = dataObjects.PageTotal;
                        DataObjectList<DataObject> previousDataObjects = DataObjects.Load<DataObject>(quickParameters);
                        previousObjectId = previousDataObjects[previousDataObjects.Count - 1].ObjectID.Value;
                    }
                    else // There's just 1 page, take the last object
                    {
                        previousObjectId = dataObjects[dataObjects.Count - 1].ObjectID.Value;
                    }
                }

                // Get next object
                if (currentIndex < Math.Min(pageSize, dataObjects.Count) - 1)
                {
                    nextObjectId = dataObjects[currentIndex + 1].ObjectID.Value;
                }
                else
                {
                    if (dataObjects.PageTotal > 1) // The next object is on another page
                    {
                        quickParameters.CurrentObjectID = null;
                        if (dataObjects.PageNumber < dataObjects.PageTotal)
                            quickParameters.PageNumber = dataObjects.PageNumber + 1;
                        else
                            quickParameters.PageNumber = 1;
                        DataObjectList<DataObject> nextDataObjects = DataObjects.Load<DataObject>(quickParameters);
                        nextObjectId = nextDataObjects[0].ObjectID.Value;
                    }
                    else // There's just 1 page, take the first object
                    {
                        nextObjectId = dataObjects[0].ObjectID.Value;
                    }
                }
                LnkPrevious.NavigateUrl = Helper.GetDetailLink(DataObject.ObjectType, previousObjectId.ToString());
                LnkNext.NavigateUrl = Helper.GetDetailLink(DataObject.ObjectType, nextObjectId.ToString());
            }
        }
        
        private void LoadDefaultQuickParameters()
        {
            quickParameters = new QuickParameters();
            quickParameters.Udc = UserDataContext.GetUserDataContext();
            quickParameters.CurrentObjectID = DataObject.ObjectID;
            quickParameters.PageSize = pageSize;
            quickParameters.SortBy = QuickSort.InsertedDate;
            quickParameters.Amount = 0;
        }
    }
}