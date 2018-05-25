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
    /// Controller class for hitbl_WidgetInstance_INS
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class HitblWidgetInstanceInController
    {
        // Preload our schema..
        HitblWidgetInstanceIn thisSchemaLoad = new HitblWidgetInstanceIn();
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
        public HitblWidgetInstanceInCollection FetchAll()
        {
            HitblWidgetInstanceInCollection coll = new HitblWidgetInstanceInCollection();
            Query qry = new Query(HitblWidgetInstanceIn.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public HitblWidgetInstanceInCollection FetchByID(object InsId)
        {
            HitblWidgetInstanceInCollection coll = new HitblWidgetInstanceInCollection().Where("INS_ID", InsId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public HitblWidgetInstanceInCollection FetchByQuery(Query qry)
        {
            HitblWidgetInstanceInCollection coll = new HitblWidgetInstanceInCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object InsId)
        {
            return (HitblWidgetInstanceIn.Delete(InsId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object InsId)
        {
            return (HitblWidgetInstanceIn.Destroy(InsId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(Guid InsId,Guid WdgId,Guid InsPagId,int InsColumnNo,int InsOrderNo,string InsXmlStateData,DateTime InsCreatedDate,DateTime InsLastUpdate,bool InsExpanded,Guid? WtpId,bool InsIsFixed,bool InsHideIfNoContent,string InsViewRoles,Guid? InsOutputTemplate,int? InsHeadingTag)
	    {
		    HitblWidgetInstanceIn item = new HitblWidgetInstanceIn();
		    
            item.InsId = InsId;
            
            item.WdgId = WdgId;
            
            item.InsPagId = InsPagId;
            
            item.InsColumnNo = InsColumnNo;
            
            item.InsOrderNo = InsOrderNo;
            
            item.InsXmlStateData = InsXmlStateData;
            
            item.InsCreatedDate = InsCreatedDate;
            
            item.InsLastUpdate = InsLastUpdate;
            
            item.InsExpanded = InsExpanded;
            
            item.WtpId = WtpId;
            
            item.InsIsFixed = InsIsFixed;
            
            item.InsHideIfNoContent = InsHideIfNoContent;
            
            item.InsViewRoles = InsViewRoles;
            
            item.InsOutputTemplate = InsOutputTemplate;
            
            item.InsHeadingTag = InsHeadingTag;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(Guid InsId,Guid WdgId,Guid InsPagId,int InsColumnNo,int InsOrderNo,string InsXmlStateData,DateTime InsCreatedDate,DateTime InsLastUpdate,bool InsExpanded,Guid? WtpId,bool InsIsFixed,bool InsHideIfNoContent,string InsViewRoles,Guid? InsOutputTemplate,int? InsHeadingTag)
	    {
		    HitblWidgetInstanceIn item = new HitblWidgetInstanceIn();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.InsId = InsId;
				
			item.WdgId = WdgId;
				
			item.InsPagId = InsPagId;
				
			item.InsColumnNo = InsColumnNo;
				
			item.InsOrderNo = InsOrderNo;
				
			item.InsXmlStateData = InsXmlStateData;
				
			item.InsCreatedDate = InsCreatedDate;
				
			item.InsLastUpdate = InsLastUpdate;
				
			item.InsExpanded = InsExpanded;
				
			item.WtpId = WtpId;
				
			item.InsIsFixed = InsIsFixed;
				
			item.InsHideIfNoContent = InsHideIfNoContent;
				
			item.InsViewRoles = InsViewRoles;
				
			item.InsOutputTemplate = InsOutputTemplate;
				
			item.InsHeadingTag = InsHeadingTag;
				
	        item.Save(UserName);
	    }
    }
}
