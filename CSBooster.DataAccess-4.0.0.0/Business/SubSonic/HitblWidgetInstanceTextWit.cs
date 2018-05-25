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
	/// Strongly-typed collection for the HitblWidgetInstanceTextWit class.
	/// </summary>
    [Serializable]
	public partial class HitblWidgetInstanceTextWitCollection : ActiveList<HitblWidgetInstanceTextWit, HitblWidgetInstanceTextWitCollection>
	{	   
		public HitblWidgetInstanceTextWitCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>HitblWidgetInstanceTextWitCollection</returns>
		public HitblWidgetInstanceTextWitCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                HitblWidgetInstanceTextWit o = this[i];
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
	/// This is an ActiveRecord class which wraps the hitbl_WidgetInstanceText_WIT table.
	/// </summary>
	[Serializable]
	public partial class HitblWidgetInstanceTextWit : ActiveRecord<HitblWidgetInstanceTextWit>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public HitblWidgetInstanceTextWit()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public HitblWidgetInstanceTextWit(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public HitblWidgetInstanceTextWit(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public HitblWidgetInstanceTextWit(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("hitbl_WidgetInstanceText_WIT", TableType.Table, DataService.GetInstance("SqlDataProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarInsId = new TableSchema.TableColumn(schema);
				colvarInsId.ColumnName = "INS_ID";
				colvarInsId.DataType = DbType.Guid;
				colvarInsId.MaxLength = 0;
				colvarInsId.AutoIncrement = false;
				colvarInsId.IsNullable = false;
				colvarInsId.IsPrimaryKey = true;
				colvarInsId.IsForeignKey = true;
				colvarInsId.IsReadOnly = false;
				colvarInsId.DefaultSetting = @"";
				
					colvarInsId.ForeignKeyTableName = "hitbl_WidgetInstance_INS";
				schema.Columns.Add(colvarInsId);
				
				TableSchema.TableColumn colvarWitLangCode = new TableSchema.TableColumn(schema);
				colvarWitLangCode.ColumnName = "WIT_LangCode";
				colvarWitLangCode.DataType = DbType.AnsiString;
				colvarWitLangCode.MaxLength = 10;
				colvarWitLangCode.AutoIncrement = false;
				colvarWitLangCode.IsNullable = false;
				colvarWitLangCode.IsPrimaryKey = true;
				colvarWitLangCode.IsForeignKey = false;
				colvarWitLangCode.IsReadOnly = false;
				colvarWitLangCode.DefaultSetting = @"";
				colvarWitLangCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWitLangCode);
				
				TableSchema.TableColumn colvarWitTitle = new TableSchema.TableColumn(schema);
				colvarWitTitle.ColumnName = "WIT_Title";
				colvarWitTitle.DataType = DbType.String;
				colvarWitTitle.MaxLength = 255;
				colvarWitTitle.AutoIncrement = false;
				colvarWitTitle.IsNullable = false;
				colvarWitTitle.IsPrimaryKey = false;
				colvarWitTitle.IsForeignKey = false;
				colvarWitTitle.IsReadOnly = false;
				colvarWitTitle.DefaultSetting = @"";
				colvarWitTitle.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWitTitle);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["SqlDataProvider"].AddSchema("hitbl_WidgetInstanceText_WIT",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("InsId")]
		[Bindable(true)]
		public Guid InsId 
		{
			get { return GetColumnValue<Guid>(Columns.InsId); }
			set { SetColumnValue(Columns.InsId, value); }
		}
		  
		[XmlAttribute("WitLangCode")]
		[Bindable(true)]
		public string WitLangCode 
		{
			get { return GetColumnValue<string>(Columns.WitLangCode); }
			set { SetColumnValue(Columns.WitLangCode, value); }
		}
		  
		[XmlAttribute("WitTitle")]
		[Bindable(true)]
		public string WitTitle 
		{
			get { return GetColumnValue<string>(Columns.WitTitle); }
			set { SetColumnValue(Columns.WitTitle, value); }
		}
		
		#endregion
		
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a HitblWidgetInstanceIn ActiveRecord object related to this HitblWidgetInstanceTextWit
		/// 
		/// </summary>
		public _4screen.CSB.DataAccess.Business.HitblWidgetInstanceIn HitblWidgetInstanceIn
		{
			get { return _4screen.CSB.DataAccess.Business.HitblWidgetInstanceIn.FetchByID(this.InsId); }
			set { SetColumnValue("INS_ID", value.InsId); }
		}
		
		
		#endregion
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(Guid varInsId,string varWitLangCode,string varWitTitle)
		{
			HitblWidgetInstanceTextWit item = new HitblWidgetInstanceTextWit();
			
			item.InsId = varInsId;
			
			item.WitLangCode = varWitLangCode;
			
			item.WitTitle = varWitTitle;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(Guid varInsId,string varWitLangCode,string varWitTitle)
		{
			HitblWidgetInstanceTextWit item = new HitblWidgetInstanceTextWit();
			
				item.InsId = varInsId;
			
				item.WitLangCode = varWitLangCode;
			
				item.WitTitle = varWitTitle;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn InsIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn WitLangCodeColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn WitTitleColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string InsId = @"INS_ID";
			 public static string WitLangCode = @"WIT_LangCode";
			 public static string WitTitle = @"WIT_Title";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
