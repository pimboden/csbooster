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
    /// Controller class for hitbl_UserSettings_UST
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class HitblUserSettingsUstController
    {
        // Preload our schema..
        HitblUserSettingsUst thisSchemaLoad = new HitblUserSettingsUst();
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
        public HitblUserSettingsUstCollection FetchAll()
        {
            HitblUserSettingsUstCollection coll = new HitblUserSettingsUstCollection();
            Query qry = new Query(HitblUserSettingsUst.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public HitblUserSettingsUstCollection FetchByID(object UsrId)
        {
            HitblUserSettingsUstCollection coll = new HitblUserSettingsUstCollection().Where("USR_ID", UsrId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public HitblUserSettingsUstCollection FetchByQuery(Query qry)
        {
            HitblUserSettingsUstCollection coll = new HitblUserSettingsUstCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object UsrId)
        {
            return (HitblUserSettingsUst.Delete(UsrId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object UsrId)
        {
            return (HitblUserSettingsUst.Destroy(UsrId) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(Guid UsrId,Guid UsrCurrentCommunityId)
        {
            Query qry = new Query(HitblUserSettingsUst.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("UsrId", UsrId).AND("UsrCurrentCommunityId", UsrCurrentCommunityId);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(Guid UsrId,Guid UsrCurrentCommunityId,Guid UsrCurrentPageId,string UsrCurrentLang)
	    {
		    HitblUserSettingsUst item = new HitblUserSettingsUst();
		    
            item.UsrId = UsrId;
            
            item.UsrCurrentCommunityId = UsrCurrentCommunityId;
            
            item.UsrCurrentPageId = UsrCurrentPageId;
            
            item.UsrCurrentLang = UsrCurrentLang;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(Guid UsrId,Guid UsrCurrentCommunityId,Guid UsrCurrentPageId,string UsrCurrentLang)
	    {
		    HitblUserSettingsUst item = new HitblUserSettingsUst();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.UsrId = UsrId;
				
			item.UsrCurrentCommunityId = UsrCurrentCommunityId;
				
			item.UsrCurrentPageId = UsrCurrentPageId;
				
			item.UsrCurrentLang = UsrCurrentLang;
				
	        item.Save(UserName);
	    }
    }
}
