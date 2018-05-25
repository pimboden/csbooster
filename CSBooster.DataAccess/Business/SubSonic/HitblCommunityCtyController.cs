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
    /// Controller class for hitbl_Community_CTY
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class HitblCommunityCtyController
    {
        // Preload our schema..
        HitblCommunityCty thisSchemaLoad = new HitblCommunityCty();
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
        public HitblCommunityCtyCollection FetchAll()
        {
            HitblCommunityCtyCollection coll = new HitblCommunityCtyCollection();
            Query qry = new Query(HitblCommunityCty.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public HitblCommunityCtyCollection FetchByID(object CtyId)
        {
            HitblCommunityCtyCollection coll = new HitblCommunityCtyCollection().Where("CTY_ID", CtyId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public HitblCommunityCtyCollection FetchByQuery(Query qry)
        {
            HitblCommunityCtyCollection coll = new HitblCommunityCtyCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object CtyId)
        {
            return (HitblCommunityCty.Delete(CtyId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object CtyId)
        {
            return (HitblCommunityCty.Destroy(CtyId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(Guid CtyId,string CtyVirtualUrl,DateTime? CtyStartDate,DateTime? CtyEndDate,int? CtyStatus,int? CtyPriority,DateTime? CtyLastBatchRunning,DateTime CtyInsertedDate,DateTime? CtyUpdatedDate,string CtyLayout,string CtyTheme,Guid? UsrIdInserted,Guid? UsrIdUpdated,string CtyBodyStyle,string CtyHeaderStyle,string CtyFooterStyle,bool CtyIsProfile)
	    {
		    HitblCommunityCty item = new HitblCommunityCty();
		    
            item.CtyId = CtyId;
            
            item.CtyVirtualUrl = CtyVirtualUrl;
            
            item.CtyStartDate = CtyStartDate;
            
            item.CtyEndDate = CtyEndDate;
            
            item.CtyStatus = CtyStatus;
            
            item.CtyPriority = CtyPriority;
            
            item.CtyLastBatchRunning = CtyLastBatchRunning;
            
            item.CtyInsertedDate = CtyInsertedDate;
            
            item.CtyUpdatedDate = CtyUpdatedDate;
            
            item.CtyLayout = CtyLayout;
            
            item.CtyTheme = CtyTheme;
            
            item.UsrIdInserted = UsrIdInserted;
            
            item.UsrIdUpdated = UsrIdUpdated;
            
            item.CtyBodyStyle = CtyBodyStyle;
            
            item.CtyHeaderStyle = CtyHeaderStyle;
            
            item.CtyFooterStyle = CtyFooterStyle;
            
            item.CtyIsProfile = CtyIsProfile;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(Guid CtyId,string CtyVirtualUrl,DateTime? CtyStartDate,DateTime? CtyEndDate,int? CtyStatus,int? CtyPriority,DateTime? CtyLastBatchRunning,DateTime CtyInsertedDate,DateTime? CtyUpdatedDate,string CtyLayout,string CtyTheme,Guid? UsrIdInserted,Guid? UsrIdUpdated,string CtyBodyStyle,string CtyHeaderStyle,string CtyFooterStyle,bool CtyIsProfile)
	    {
		    HitblCommunityCty item = new HitblCommunityCty();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.CtyId = CtyId;
				
			item.CtyVirtualUrl = CtyVirtualUrl;
				
			item.CtyStartDate = CtyStartDate;
				
			item.CtyEndDate = CtyEndDate;
				
			item.CtyStatus = CtyStatus;
				
			item.CtyPriority = CtyPriority;
				
			item.CtyLastBatchRunning = CtyLastBatchRunning;
				
			item.CtyInsertedDate = CtyInsertedDate;
				
			item.CtyUpdatedDate = CtyUpdatedDate;
				
			item.CtyLayout = CtyLayout;
				
			item.CtyTheme = CtyTheme;
				
			item.UsrIdInserted = UsrIdInserted;
				
			item.UsrIdUpdated = UsrIdUpdated;
				
			item.CtyBodyStyle = CtyBodyStyle;
				
			item.CtyHeaderStyle = CtyHeaderStyle;
				
			item.CtyFooterStyle = CtyFooterStyle;
				
			item.CtyIsProfile = CtyIsProfile;
				
	        item.Save(UserName);
	    }
    }
}
