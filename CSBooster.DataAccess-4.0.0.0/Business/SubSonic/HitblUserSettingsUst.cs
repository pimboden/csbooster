// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.ComponentModel;
using System.Data;
using System.Xml.Serialization;
using SubSonic;

namespace _4screen.CSB.DataAccess.Business
{
	/// <summary>
	/// Strongly-typed collection for the HitblUserSettingsUst class.
	/// </summary>
    [Serializable]
	public partial class HitblUserSettingsUstCollection : ActiveList<HitblUserSettingsUst, HitblUserSettingsUstCollection>
	{	   
		public HitblUserSettingsUstCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>HitblUserSettingsUstCollection</returns>
		public HitblUserSettingsUstCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                HitblUserSettingsUst o = this[i];
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
	/// This is an ActiveRecord class which wraps the hitbl_UserSettings_UST table.
	/// </summary>
	[Serializable]
	public partial class HitblUserSettingsUst : ActiveRecord<HitblUserSettingsUst>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public HitblUserSettingsUst()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public HitblUserSettingsUst(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public HitblUserSettingsUst(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public HitblUserSettingsUst(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("hitbl_UserSettings_UST", TableType.Table, DataService.GetInstance("SqlDataProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarUsrId = new TableSchema.TableColumn(schema);
				colvarUsrId.ColumnName = "USR_ID";
				colvarUsrId.DataType = DbType.Guid;
				colvarUsrId.MaxLength = 0;
				colvarUsrId.AutoIncrement = false;
				colvarUsrId.IsNullable = false;
				colvarUsrId.IsPrimaryKey = true;
				colvarUsrId.IsForeignKey = false;
				colvarUsrId.IsReadOnly = false;
				colvarUsrId.DefaultSetting = @"";
				colvarUsrId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUsrId);
				
				TableSchema.TableColumn colvarUsrCurrentCommunityId = new TableSchema.TableColumn(schema);
				colvarUsrCurrentCommunityId.ColumnName = "USR_CurrentCommunityId";
				colvarUsrCurrentCommunityId.DataType = DbType.Guid;
				colvarUsrCurrentCommunityId.MaxLength = 0;
				colvarUsrCurrentCommunityId.AutoIncrement = false;
				colvarUsrCurrentCommunityId.IsNullable = false;
				colvarUsrCurrentCommunityId.IsPrimaryKey = true;
				colvarUsrCurrentCommunityId.IsForeignKey = false;
				colvarUsrCurrentCommunityId.IsReadOnly = false;
				colvarUsrCurrentCommunityId.DefaultSetting = @"";
				colvarUsrCurrentCommunityId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUsrCurrentCommunityId);
				
				TableSchema.TableColumn colvarUsrCurrentPageId = new TableSchema.TableColumn(schema);
				colvarUsrCurrentPageId.ColumnName = "USR_CurrentPageId";
				colvarUsrCurrentPageId.DataType = DbType.Guid;
				colvarUsrCurrentPageId.MaxLength = 0;
				colvarUsrCurrentPageId.AutoIncrement = false;
				colvarUsrCurrentPageId.IsNullable = false;
				colvarUsrCurrentPageId.IsPrimaryKey = false;
				colvarUsrCurrentPageId.IsForeignKey = false;
				colvarUsrCurrentPageId.IsReadOnly = false;
				colvarUsrCurrentPageId.DefaultSetting = @"";
				colvarUsrCurrentPageId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUsrCurrentPageId);
				
				TableSchema.TableColumn colvarUsrCurrentLang = new TableSchema.TableColumn(schema);
				colvarUsrCurrentLang.ColumnName = "USR_CurrentLang";
				colvarUsrCurrentLang.DataType = DbType.AnsiString;
				colvarUsrCurrentLang.MaxLength = 50;
				colvarUsrCurrentLang.AutoIncrement = false;
				colvarUsrCurrentLang.IsNullable = false;
				colvarUsrCurrentLang.IsPrimaryKey = false;
				colvarUsrCurrentLang.IsForeignKey = false;
				colvarUsrCurrentLang.IsReadOnly = false;
				colvarUsrCurrentLang.DefaultSetting = @"";
				colvarUsrCurrentLang.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUsrCurrentLang);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["SqlDataProvider"].AddSchema("hitbl_UserSettings_UST",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("UsrId")]
		[Bindable(true)]
		public Guid UsrId 
		{
			get { return GetColumnValue<Guid>(Columns.UsrId); }
			set { SetColumnValue(Columns.UsrId, value); }
		}
		  
		[XmlAttribute("UsrCurrentCommunityId")]
		[Bindable(true)]
		public Guid UsrCurrentCommunityId 
		{
			get { return GetColumnValue<Guid>(Columns.UsrCurrentCommunityId); }
			set { SetColumnValue(Columns.UsrCurrentCommunityId, value); }
		}
		  
		[XmlAttribute("UsrCurrentPageId")]
		[Bindable(true)]
		public Guid UsrCurrentPageId 
		{
			get { return GetColumnValue<Guid>(Columns.UsrCurrentPageId); }
			set { SetColumnValue(Columns.UsrCurrentPageId, value); }
		}
		  
		[XmlAttribute("UsrCurrentLang")]
		[Bindable(true)]
		public string UsrCurrentLang 
		{
			get { return GetColumnValue<string>(Columns.UsrCurrentLang); }
			set { SetColumnValue(Columns.UsrCurrentLang, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(Guid varUsrId,Guid varUsrCurrentCommunityId,Guid varUsrCurrentPageId,string varUsrCurrentLang)
		{
			HitblUserSettingsUst item = new HitblUserSettingsUst();
			
			item.UsrId = varUsrId;
			
			item.UsrCurrentCommunityId = varUsrCurrentCommunityId;
			
			item.UsrCurrentPageId = varUsrCurrentPageId;
			
			item.UsrCurrentLang = varUsrCurrentLang;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(Guid varUsrId,Guid varUsrCurrentCommunityId,Guid varUsrCurrentPageId,string varUsrCurrentLang)
		{
			HitblUserSettingsUst item = new HitblUserSettingsUst();
			
				item.UsrId = varUsrId;
			
				item.UsrCurrentCommunityId = varUsrCurrentCommunityId;
			
				item.UsrCurrentPageId = varUsrCurrentPageId;
			
				item.UsrCurrentLang = varUsrCurrentLang;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn UsrIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn UsrCurrentCommunityIdColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn UsrCurrentPageIdColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn UsrCurrentLangColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string UsrId = @"USR_ID";
			 public static string UsrCurrentCommunityId = @"USR_CurrentCommunityId";
			 public static string UsrCurrentPageId = @"USR_CurrentPageId";
			 public static string UsrCurrentLang = @"USR_CurrentLang";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
