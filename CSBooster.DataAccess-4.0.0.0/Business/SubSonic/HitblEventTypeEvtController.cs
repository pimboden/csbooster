// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.ComponentModel;
using SubSonic;

namespace _4screen.CSB.DataAccess.Business
{
    /// <summary>
    /// Controller class for hitbl_Event_Type_EVT
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class HitblEventTypeEvtController
    {
        // Preload our schema..
        HitblEventTypeEvt thisSchemaLoad = new HitblEventTypeEvt();
        private string userName = String.Empty;
        protected string UserName
        {
            get
            {
				if (userName.Length == 0) 
				{
    				if (System.Web.HttpContext.Current != null)
    				{
						userName=System.Web.HttpContext.Current.User.Identity.Name;
					}
					else
					{
						userName=System.Threading.Thread.CurrentPrincipal.Identity.Name;
					}
				}
				return userName;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public HitblEventTypeEvtCollection FetchAll()
        {
            HitblEventTypeEvtCollection coll = new HitblEventTypeEvtCollection();
            Query qry = new Query(HitblEventTypeEvt.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public HitblEventTypeEvtCollection FetchByID(object EvtId)
        {
            HitblEventTypeEvtCollection coll = new HitblEventTypeEvtCollection().Where("EVT_ID", EvtId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public HitblEventTypeEvtCollection FetchByQuery(Query qry)
        {
            HitblEventTypeEvtCollection coll = new HitblEventTypeEvtCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object EvtId)
        {
            return (HitblEventTypeEvt.Delete(EvtId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object EvtId)
        {
            return (HitblEventTypeEvt.Destroy(EvtId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int EvtId,string EvtName,int EvtSortOrder)
	    {
		    HitblEventTypeEvt item = new HitblEventTypeEvt();
		    
            item.EvtId = EvtId;
            
            item.EvtName = EvtName;
            
            item.EvtSortOrder = EvtSortOrder;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int EvtId,string EvtName,int EvtSortOrder)
	    {
		    HitblEventTypeEvt item = new HitblEventTypeEvt();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.EvtId = EvtId;
				
			item.EvtName = EvtName;
				
			item.EvtSortOrder = EvtSortOrder;
				
	        item.Save(UserName);
	    }
    }
}
