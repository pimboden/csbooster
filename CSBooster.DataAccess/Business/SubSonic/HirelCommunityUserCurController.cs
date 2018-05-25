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
    /// Controller class for hirel_Community_User_CUR
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class HirelCommunityUserCurController
    {
        // Preload our schema..
        HirelCommunityUserCur thisSchemaLoad = new HirelCommunityUserCur();
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
        public HirelCommunityUserCurCollection FetchAll()
        {
            HirelCommunityUserCurCollection coll = new HirelCommunityUserCurCollection();
            Query qry = new Query(HirelCommunityUserCur.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public HirelCommunityUserCurCollection FetchByID(object CtyId)
        {
            HirelCommunityUserCurCollection coll = new HirelCommunityUserCurCollection().Where("CTY_ID", CtyId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public HirelCommunityUserCurCollection FetchByQuery(Query qry)
        {
            HirelCommunityUserCurCollection coll = new HirelCommunityUserCurCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object CtyId)
        {
            return (HirelCommunityUserCur.Delete(CtyId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object CtyId)
        {
            return (HirelCommunityUserCur.Destroy(CtyId) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(Guid CtyId,Guid UsrId)
        {
            Query qry = new Query(HirelCommunityUserCur.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("CtyId", CtyId).AND("UsrId", UsrId);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(Guid CtyId,Guid UsrId,bool CurIsOwner,int? CurStatus,DateTime? CurInsertedDate,Guid? UsrIdInvitedBy)
	    {
		    HirelCommunityUserCur item = new HirelCommunityUserCur();
		    
            item.CtyId = CtyId;
            
            item.UsrId = UsrId;
            
            item.CurIsOwner = CurIsOwner;
            
            item.CurStatus = CurStatus;
            
            item.CurInsertedDate = CurInsertedDate;
            
            item.UsrIdInvitedBy = UsrIdInvitedBy;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(Guid CtyId,Guid UsrId,bool CurIsOwner,int? CurStatus,DateTime? CurInsertedDate,Guid? UsrIdInvitedBy)
	    {
		    HirelCommunityUserCur item = new HirelCommunityUserCur();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.CtyId = CtyId;
				
			item.UsrId = UsrId;
				
			item.CurIsOwner = CurIsOwner;
				
			item.CurStatus = CurStatus;
				
			item.CurInsertedDate = CurInsertedDate;
				
			item.UsrIdInvitedBy = UsrIdInvitedBy;
				
	        item.Save(UserName);
	    }
    }
}
