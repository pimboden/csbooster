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
    /// Controller class for hitbl_WidgetInstanceText_WIT
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class HitblWidgetInstanceTextWitController
    {
        // Preload our schema..
        HitblWidgetInstanceTextWit thisSchemaLoad = new HitblWidgetInstanceTextWit();
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
        public HitblWidgetInstanceTextWitCollection FetchAll()
        {
            HitblWidgetInstanceTextWitCollection coll = new HitblWidgetInstanceTextWitCollection();
            Query qry = new Query(HitblWidgetInstanceTextWit.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public HitblWidgetInstanceTextWitCollection FetchByID(object InsId)
        {
            HitblWidgetInstanceTextWitCollection coll = new HitblWidgetInstanceTextWitCollection().Where("INS_ID", InsId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public HitblWidgetInstanceTextWitCollection FetchByQuery(Query qry)
        {
            HitblWidgetInstanceTextWitCollection coll = new HitblWidgetInstanceTextWitCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object InsId)
        {
            return (HitblWidgetInstanceTextWit.Delete(InsId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object InsId)
        {
            return (HitblWidgetInstanceTextWit.Destroy(InsId) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(Guid InsId,string WitLangCode)
        {
            Query qry = new Query(HitblWidgetInstanceTextWit.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("InsId", InsId).AND("WitLangCode", WitLangCode);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(Guid InsId,string WitLangCode,string WitTitle)
	    {
		    HitblWidgetInstanceTextWit item = new HitblWidgetInstanceTextWit();
		    
            item.InsId = InsId;
            
            item.WitLangCode = WitLangCode;
            
            item.WitTitle = WitTitle;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(Guid InsId,string WitLangCode,string WitTitle)
	    {
		    HitblWidgetInstanceTextWit item = new HitblWidgetInstanceTextWit();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.InsId = InsId;
				
			item.WitLangCode = WitLangCode;
				
			item.WitTitle = WitTitle;
				
	        item.Save(UserName);
	    }
    }
}
