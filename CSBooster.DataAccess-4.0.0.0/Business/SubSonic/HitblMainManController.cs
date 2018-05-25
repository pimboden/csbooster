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
    /// Controller class for hitbl_Main_MAN
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class HitblMainManController
    {
        // Preload our schema..
        HitblMainMan thisSchemaLoad = new HitblMainMan();
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
        public HitblMainManCollection FetchAll()
        {
            HitblMainManCollection coll = new HitblMainManCollection();
            Query qry = new Query(HitblMainMan.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public HitblMainManCollection FetchByID(object ManId)
        {
            HitblMainManCollection coll = new HitblMainManCollection().Where("MAN_ID", ManId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public HitblMainManCollection FetchByQuery(Query qry)
        {
            HitblMainManCollection coll = new HitblMainManCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object ManId)
        {
            return (HitblMainMan.Delete(ManId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object ManId)
        {
            return (HitblMainMan.Destroy(ManId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int? ManLevel,int? ManManId,string ManTitle,int? ManStatus,int ManOrder,Guid TgwId)
	    {
		    HitblMainMan item = new HitblMainMan();
		    
            item.ManLevel = ManLevel;
            
            item.ManManId = ManManId;
            
            item.ManTitle = ManTitle;
            
            item.ManStatus = ManStatus;
            
            item.ManOrder = ManOrder;
            
            item.TgwId = TgwId;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int ManId,int? ManLevel,int? ManManId,string ManTitle,int? ManStatus,int ManOrder,Guid TgwId)
	    {
		    HitblMainMan item = new HitblMainMan();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.ManId = ManId;
				
			item.ManLevel = ManLevel;
				
			item.ManManId = ManManId;
				
			item.ManTitle = ManTitle;
				
			item.ManStatus = ManStatus;
				
			item.ManOrder = ManOrder;
				
			item.TgwId = TgwId;
				
	        item.Save(UserName);
	    }
    }
}
