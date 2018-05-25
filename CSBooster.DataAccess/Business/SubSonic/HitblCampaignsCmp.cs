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
	/// Strongly-typed collection for the HitblCampaignsCmp class.
	/// </summary>
    [Serializable]
	public partial class HitblCampaignsCmpCollection : ActiveList<HitblCampaignsCmp, HitblCampaignsCmpCollection>
	{	   
		public HitblCampaignsCmpCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>HitblCampaignsCmpCollection</returns>
		public HitblCampaignsCmpCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                HitblCampaignsCmp o = this[i];
                foreach (SubSonic.Where w in this.wheres)
                {
                    bool remove = false;
                    System.Reflection.PropertyInfo pi = o.GetType().GetProperty(w.ColumnName);
                    if (pi.CanRead)
                    {
                        object val = pi.GetValue(o, null);
                        switch (w.Comparison)
                        {
                            case SubSonic.Comparison.Equals:
                                if (!val.Equals(w.ParameterValue))
                                {
                                    remove = true;
                                }
                                break;
                        }
                    }
                    if (remove)
                    {
                        this.Remove(o);
                        break;
                    }
                }
            }
            return this;
        }
		
		
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the hitbl_Campaigns_CMP table.
	/// </summary>
	[Serializable]
	public partial class HitblCampaignsCmp : ActiveRecord<HitblCampaignsCmp>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public HitblCampaignsCmp()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public HitblCampaignsCmp(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public HitblCampaignsCmp(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public HitblCampaignsCmp(string columnName, object columnValue)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByParam(columnName,columnValue);
		}
		
		protected static void SetSQLProps() { GetTableSchema(); }
		
		#endregion
		
		#region Schema and Query Accessor	
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get
			{
				if (BaseSchema == null)
					SetSQLProps();
				return BaseSchema;
			}
		}
		
		private static void GetTableSchema() 
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("hitbl_Campaigns_CMP", TableType.Table, DataService.GetInstance("SqlDataProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarCtyId = new TableSchema.TableColumn(schema);
				colvarCtyId.ColumnName = "CTY_ID";
				colvarCtyId.DataType = DbType.Guid;
				colvarCtyId.MaxLength = 0;
				colvarCtyId.AutoIncrement = false;
				colvarCtyId.IsNullable = false;
				colvarCtyId.IsPrimaryKey = true;
				colvarCtyId.IsForeignKey = false;
				colvarCtyId.IsReadOnly = false;
				colvarCtyId.DefaultSetting = @"";
				colvarCtyId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCtyId);
				
				TableSchema.TableColumn colvarCmpName = new TableSchema.TableColumn(schema);
				colvarCmpName.ColumnName = "CMP_Name";
				colvarCmpName.DataType = DbType.String;
				colvarCmpName.MaxLength = 100;
				colvarCmpName.AutoIncrement = false;
				colvarCmpName.IsNullable = false;
				colvarCmpName.IsPrimaryKey = false;
				colvarCmpName.IsForeignKey = false;
				colvarCmpName.IsReadOnly = false;
				colvarCmpName.DefaultSetting = @"";
				colvarCmpName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCmpName);
				
				TableSchema.TableColumn colvarCmpRedirectURL = new TableSchema.TableColumn(schema);
				colvarCmpRedirectURL.ColumnName = "CMP_RedirectURL";
				colvarCmpRedirectURL.DataType = DbType.String;
				colvarCmpRedirectURL.MaxLength = 100;
				colvarCmpRedirectURL.AutoIncrement = false;
				colvarCmpRedirectURL.IsNullable = true;
				colvarCmpRedirectURL.IsPrimaryKey = false;
				colvarCmpRedirectURL.IsForeignKey = false;
				colvarCmpRedirectURL.IsReadOnly = false;
				colvarCmpRedirectURL.DefaultSetting = @"";
				colvarCmpRedirectURL.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCmpRedirectURL);
				
				TableSchema.TableColumn colvarCmpStyleSheetPath = new TableSchema.TableColumn(schema);
				colvarCmpStyleSheetPath.ColumnName = "CMP_StyleSheetPath";
				colvarCmpStyleSheetPath.DataType = DbType.String;
				colvarCmpStyleSheetPath.MaxLength = 100;
				colvarCmpStyleSheetPath.AutoIncrement = false;
				colvarCmpStyleSheetPath.IsNullable = true;
				colvarCmpStyleSheetPath.IsPrimaryKey = false;
				colvarCmpStyleSheetPath.IsForeignKey = false;
				colvarCmpStyleSheetPath.IsReadOnly = false;
				colvarCmpStyleSheetPath.DefaultSetting = @"";
				colvarCmpStyleSheetPath.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCmpStyleSheetPath);
				
				TableSchema.TableColumn colvarCmpLogoPath = new TableSchema.TableColumn(schema);
				colvarCmpLogoPath.ColumnName = "CMP_LogoPath";
				colvarCmpLogoPath.DataType = DbType.String;
				colvarCmpLogoPath.MaxLength = 100;
				colvarCmpLogoPath.AutoIncrement = false;
				colvarCmpLogoPath.IsNullable = true;
				colvarCmpLogoPath.IsPrimaryKey = false;
				colvarCmpLogoPath.IsForeignKey = false;
				colvarCmpLogoPath.IsReadOnly = false;
				colvarCmpLogoPath.DefaultSetting = @"";
				colvarCmpLogoPath.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCmpLogoPath);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["SqlDataProvider"].AddSchema("hitbl_Campaigns_CMP",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("CtyId")]
		[Bindable(true)]
		public Guid CtyId 
		{
			get { return GetColumnValue<Guid>(Columns.CtyId); }
			set { SetColumnValue(Columns.CtyId, value); }
		}
		  
		[XmlAttribute("CmpName")]
		[Bindable(true)]
		public string CmpName 
		{
			get { return GetColumnValue<string>(Columns.CmpName); }
			set { SetColumnValue(Columns.CmpName, value); }
		}
		  
		[XmlAttribute("CmpRedirectURL")]
		[Bindable(true)]
		public string CmpRedirectURL 
		{
			get { return GetColumnValue<string>(Columns.CmpRedirectURL); }
			set { SetColumnValue(Columns.CmpRedirectURL, value); }
		}
		  
		[XmlAttribute("CmpStyleSheetPath")]
		[Bindable(true)]
		public string CmpStyleSheetPath 
		{
			get { return GetColumnValue<string>(Columns.CmpStyleSheetPath); }
			set { SetColumnValue(Columns.CmpStyleSheetPath, value); }
		}
		  
		[XmlAttribute("CmpLogoPath")]
		[Bindable(true)]
		public string CmpLogoPath 
		{
			get { return GetColumnValue<string>(Columns.CmpLogoPath); }
			set { SetColumnValue(Columns.CmpLogoPath, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(Guid varCtyId,string varCmpName,string varCmpRedirectURL,string varCmpStyleSheetPath,string varCmpLogoPath)
		{
			HitblCampaignsCmp item = new HitblCampaignsCmp();
			
			item.CtyId = varCtyId;
			
			item.CmpName = varCmpName;
			
			item.CmpRedirectURL = varCmpRedirectURL;
			
			item.CmpStyleSheetPath = varCmpStyleSheetPath;
			
			item.CmpLogoPath = varCmpLogoPath;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(Guid varCtyId,string varCmpName,string varCmpRedirectURL,string varCmpStyleSheetPath,string varCmpLogoPath)
		{
			HitblCampaignsCmp item = new HitblCampaignsCmp();
			
				item.CtyId = varCtyId;
			
				item.CmpName = varCmpName;
			
				item.CmpRedirectURL = varCmpRedirectURL;
			
				item.CmpStyleSheetPath = varCmpStyleSheetPath;
			
				item.CmpLogoPath = varCmpLogoPath;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn CtyIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn CmpNameColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn CmpRedirectURLColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn CmpStyleSheetPathColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn CmpLogoPathColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string CtyId = @"CTY_ID";
			 public static string CmpName = @"CMP_Name";
			 public static string CmpRedirectURL = @"CMP_RedirectURL";
			 public static string CmpStyleSheetPath = @"CMP_StyleSheetPath";
			 public static string CmpLogoPath = @"CMP_LogoPath";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
