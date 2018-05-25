using System; 
using System.Text; 
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration; 
using System.Xml; 
using System.Xml.Serialization;
using SubSonic; 
using SubSonic.Utilities;
namespace _4screen.CSB.DataAccess.Business
{
    /// <summary>
    /// Controller class for hitbl_Page_PAG
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class HitblPagePagController
    {
        // Preload our schema..
        HitblPagePag thisSchemaLoad = new HitblPagePag();
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
        public HitblPagePagCollection FetchAll()
        {
            HitblPagePagCollection coll = new HitblPagePagCollection();
            Query qry = new Query(HitblPagePag.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public HitblPagePagCollection FetchByID(object PagId)
        {
            HitblPagePagCollection coll = new HitblPagePagCollection().Where("PAG_ID", PagId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public HitblPagePagCollection FetchByQuery(Query qry)
        {
            HitblPagePagCollection coll = new HitblPagePagCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object PagId)
        {
            return (HitblPagePag.Delete(PagId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object PagId)
        {
            return (HitblPagePag.Destroy(PagId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(Guid PagId,string PagTitle,DateTime PagCreatedDate,DateTime PagLastUpdate,Guid CtyId,int PagOrderNr)
	    {
		    HitblPagePag item = new HitblPagePag();
		    
            item.PagId = PagId;
            
            item.PagTitle = PagTitle;
            
            item.PagCreatedDate = PagCreatedDate;
            
            item.PagLastUpdate = PagLastUpdate;
            
            item.CtyId = CtyId;
            
            item.PagOrderNr = PagOrderNr;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(Guid PagId,string PagTitle,DateTime PagCreatedDate,DateTime PagLastUpdate,Guid CtyId,int PagOrderNr)
	    {
		    HitblPagePag item = new HitblPagePag();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.PagId = PagId;
				
			item.PagTitle = PagTitle;
				
			item.PagCreatedDate = PagCreatedDate;
				
			item.PagLastUpdate = PagLastUpdate;
				
			item.CtyId = CtyId;
				
			item.PagOrderNr = PagOrderNr;
				
	        item.Save(UserName);
	    }
    }
}
