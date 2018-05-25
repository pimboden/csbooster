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
	#region Tables Struct
	public partial struct Tables
	{
		
		public static string AspnetMembership = @"aspnet_Membership";
        
		public static string HirelCommunityUserCur = @"hirel_Community_User_CUR";
        
		public static string HitblCampaignsCmp = @"hitbl_Campaigns_CMP";
        
		public static string HitblCommunityCty = @"hitbl_Community_CTY";
        
		public static string HitblCountryNameCou = @"hitbl_CountryName_COU";
        
		public static string HitblEventTypeEvt = @"hitbl_Event_Type_EVT";
        
		public static string HitblMainMan = @"hitbl_Main_MAN";
        
		public static string HitblPagePag = @"hitbl_Page_PAG";
        
		public static string HitblUserSettingsUst = @"hitbl_UserSettings_UST";
        
		public static string HitblWidgetInstanceIn = @"hitbl_WidgetInstance_INS";
        
		public static string HitblWidgetInstanceTextWit = @"hitbl_WidgetInstanceText_WIT";
        
		public static string HitblWidgetTemplatesWtp = @"hitbl_WidgetTemplates_WTP";
        
	}
	#endregion
    #region Schemas
    public partial class Schemas {
		
		public static TableSchema.Table AspnetMembership{
            get { return DataService.GetSchema("aspnet_Membership","SqlDataProvider"); }
		}
        
		public static TableSchema.Table HirelCommunityUserCur{
            get { return DataService.GetSchema("hirel_Community_User_CUR","SqlDataProvider"); }
		}
        
		public static TableSchema.Table HitblCampaignsCmp{
            get { return DataService.GetSchema("hitbl_Campaigns_CMP","SqlDataProvider"); }
		}
        
		public static TableSchema.Table HitblCommunityCty{
            get { return DataService.GetSchema("hitbl_Community_CTY","SqlDataProvider"); }
		}
        
		public static TableSchema.Table HitblCountryNameCou{
            get { return DataService.GetSchema("hitbl_CountryName_COU","SqlDataProvider"); }
		}
        
		public static TableSchema.Table HitblEventTypeEvt{
            get { return DataService.GetSchema("hitbl_Event_Type_EVT","SqlDataProvider"); }
		}
        
		public static TableSchema.Table HitblMainMan{
            get { return DataService.GetSchema("hitbl_Main_MAN","SqlDataProvider"); }
		}
        
		public static TableSchema.Table HitblPagePag{
            get { return DataService.GetSchema("hitbl_Page_PAG","SqlDataProvider"); }
		}
        
		public static TableSchema.Table HitblUserSettingsUst{
            get { return DataService.GetSchema("hitbl_UserSettings_UST","SqlDataProvider"); }
		}
        
		public static TableSchema.Table HitblWidgetInstanceIn{
            get { return DataService.GetSchema("hitbl_WidgetInstance_INS","SqlDataProvider"); }
		}
        
		public static TableSchema.Table HitblWidgetInstanceTextWit{
            get { return DataService.GetSchema("hitbl_WidgetInstanceText_WIT","SqlDataProvider"); }
		}
        
		public static TableSchema.Table HitblWidgetTemplatesWtp{
            get { return DataService.GetSchema("hitbl_WidgetTemplates_WTP","SqlDataProvider"); }
		}
        
	
    }
    #endregion
    #region View Struct
    public partial struct Views 
    {
		
		public static string VwAspnetMembershipUser = @"vw_aspnet_MembershipUsers";
        
    }
    #endregion
    
    #region Query Factories
	public static partial class DB
	{
        public static DataProvider _provider = DataService.Providers["SqlDataProvider"];
        static ISubSonicRepository _repository;
        public static ISubSonicRepository Repository {
            get {
                if (_repository == null)
                    return new SubSonicRepository(_provider);
                return _repository; 
            }
            set { _repository = value; }
        }
	
        public static Select SelectAllColumnsFrom<T>() where T : RecordBase<T>, new()
	    {
            return Repository.SelectAllColumnsFrom<T>();
            
	    }
	    public static Select Select()
	    {
            return Repository.Select();
	    }
	    
		public static Select Select(params string[] columns)
		{
            return Repository.Select(columns);
        }
	    
		public static Select Select(params Aggregate[] aggregates)
		{
            return Repository.Select(aggregates);
        }
   
	    public static Update Update<T>() where T : RecordBase<T>, new()
	    {
            return Repository.Update<T>();
	    }
     
	    
	    public static Insert Insert()
	    {
            return Repository.Insert();
	    }
	    
	    public static Delete Delete()
	    {
            
            return Repository.Delete();
	    }
	    
	    public static InlineQuery Query()
	    {
            
            return Repository.Query();
	    }
	    	    
	    
	}
    #endregion
    
}
#region Databases
public partial struct Databases 
{
	
	public static string SqlDataProvider = @"SqlDataProvider";
    
}
#endregion