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
    /// Controller class for hitbl_WidgetTemplates_WTP
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class HitblWidgetTemplatesWtpController
    {
        // Preload our schema..
        HitblWidgetTemplatesWtp thisSchemaLoad = new HitblWidgetTemplatesWtp();
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
        public HitblWidgetTemplatesWtpCollection FetchAll()
        {
            HitblWidgetTemplatesWtpCollection coll = new HitblWidgetTemplatesWtpCollection();
            Query qry = new Query(HitblWidgetTemplatesWtp.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public HitblWidgetTemplatesWtpCollection FetchByID(object WtpId)
        {
            HitblWidgetTemplatesWtpCollection coll = new HitblWidgetTemplatesWtpCollection().Where("WTP_ID", WtpId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public HitblWidgetTemplatesWtpCollection FetchByQuery(Query qry)
        {
            HitblWidgetTemplatesWtpCollection coll = new HitblWidgetTemplatesWtpCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object WtpId)
        {
            return (HitblWidgetTemplatesWtp.Delete(WtpId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object WtpId)
        {
            return (HitblWidgetTemplatesWtp.Destroy(WtpId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(Guid WtpId,Guid? UserID,string WtpName,string WtpTemplate,string WtpXMLTemplate,bool WtpExplicitInserted)
	    {
		    HitblWidgetTemplatesWtp item = new HitblWidgetTemplatesWtp();
		    
            item.WtpId = WtpId;
            
            item.UserID = UserID;
            
            item.WtpName = WtpName;
            
            item.WtpTemplate = WtpTemplate;
            
            item.WtpXMLTemplate = WtpXMLTemplate;
            
            item.WtpExplicitInserted = WtpExplicitInserted;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(Guid WtpId,Guid? UserID,string WtpName,string WtpTemplate,string WtpXMLTemplate,bool WtpExplicitInserted)
	    {
		    HitblWidgetTemplatesWtp item = new HitblWidgetTemplatesWtp();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.WtpId = WtpId;
				
			item.UserID = UserID;
				
			item.WtpName = WtpName;
				
			item.WtpTemplate = WtpTemplate;
				
			item.WtpXMLTemplate = WtpXMLTemplate;
				
			item.WtpExplicitInserted = WtpExplicitInserted;
				
	        item.Save(UserName);
	    }
    }
}
