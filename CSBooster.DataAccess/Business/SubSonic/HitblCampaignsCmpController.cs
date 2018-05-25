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
    /// Controller class for hitbl_Campaigns_CMP
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class HitblCampaignsCmpController
    {
        // Preload our schema..
        HitblCampaignsCmp thisSchemaLoad = new HitblCampaignsCmp();
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
        public HitblCampaignsCmpCollection FetchAll()
        {
            HitblCampaignsCmpCollection coll = new HitblCampaignsCmpCollection();
            Query qry = new Query(HitblCampaignsCmp.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public HitblCampaignsCmpCollection FetchByID(object CtyId)
        {
            HitblCampaignsCmpCollection coll = new HitblCampaignsCmpCollection().Where("CTY_ID", CtyId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public HitblCampaignsCmpCollection FetchByQuery(Query qry)
        {
            HitblCampaignsCmpCollection coll = new HitblCampaignsCmpCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object CtyId)
        {
            return (HitblCampaignsCmp.Delete(CtyId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object CtyId)
        {
            return (HitblCampaignsCmp.Destroy(CtyId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(Guid CtyId,string CmpName,string CmpRedirectURL,string CmpStyleSheetPath,string CmpLogoPath)
	    {
		    HitblCampaignsCmp item = new HitblCampaignsCmp();
		    
            item.CtyId = CtyId;
            
            item.CmpName = CmpName;
            
            item.CmpRedirectURL = CmpRedirectURL;
            
            item.CmpStyleSheetPath = CmpStyleSheetPath;
            
            item.CmpLogoPath = CmpLogoPath;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(Guid CtyId,string CmpName,string CmpRedirectURL,string CmpStyleSheetPath,string CmpLogoPath)
	    {
		    HitblCampaignsCmp item = new HitblCampaignsCmp();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.CtyId = CtyId;
				
			item.CmpName = CmpName;
				
			item.CmpRedirectURL = CmpRedirectURL;
				
			item.CmpStyleSheetPath = CmpStyleSheetPath;
				
			item.CmpLogoPath = CmpLogoPath;
				
	        item.Save(UserName);
	    }
    }
}
