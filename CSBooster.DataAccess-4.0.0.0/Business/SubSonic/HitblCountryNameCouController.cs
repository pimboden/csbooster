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
    /// Controller class for hitbl_CountryName_COU
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class HitblCountryNameCouController
    {
        // Preload our schema..
        HitblCountryNameCou thisSchemaLoad = new HitblCountryNameCou();
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
        public HitblCountryNameCouCollection FetchAll()
        {
            HitblCountryNameCouCollection coll = new HitblCountryNameCouCollection();
            Query qry = new Query(HitblCountryNameCou.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public HitblCountryNameCouCollection FetchByID(object CountryCode)
        {
            HitblCountryNameCouCollection coll = new HitblCountryNameCouCollection().Where("CountryCode", CountryCode).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public HitblCountryNameCouCollection FetchByQuery(Query qry)
        {
            HitblCountryNameCouCollection coll = new HitblCountryNameCouCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object CountryCode)
        {
            return (HitblCountryNameCou.Delete(CountryCode) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object CountryCode)
        {
            return (HitblCountryNameCou.Destroy(CountryCode) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(string CountryCode,string LangCode)
        {
            Query qry = new Query(HitblCountryNameCou.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("CountryCode", CountryCode).AND("LangCode", LangCode);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string CountryCode,string LangCode,string CountryName)
	    {
		    HitblCountryNameCou item = new HitblCountryNameCou();
		    
            item.CountryCode = CountryCode;
            
            item.LangCode = LangCode;
            
            item.CountryName = CountryName;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string CountryCode,string LangCode,string CountryName)
	    {
		    HitblCountryNameCou item = new HitblCountryNameCou();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.CountryCode = CountryCode;
				
			item.LangCode = LangCode;
				
			item.CountryName = CountryName;
				
	        item.Save(UserName);
	    }
    }
}
