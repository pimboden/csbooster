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
	/// Strongly-typed collection for the HitblPagePag class.
	/// </summary>
    [Serializable]
	public partial class HitblPagePagCollection : ActiveList<HitblPagePag, HitblPagePagCollection>
	{	   
		public HitblPagePagCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>HitblPagePagCollection</returns>
		public HitblPagePagCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                HitblPagePag o = this[i];
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
	/// This is an ActiveRecord class which wraps the hitbl_Page_PAG table.
	/// </summary>
	[Serializable]
	public partial class HitblPagePag : ActiveRecord<HitblPagePag>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public HitblPagePag()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public HitblPagePag(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public HitblPagePag(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public HitblPagePag(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("hitbl_Page_PAG", TableType.Table, DataService.GetInstance("SqlDataProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarPagId = new TableSchema.TableColumn(schema);
				colvarPagId.ColumnName = "PAG_ID";
				colvarPagId.DataType = DbType.Guid;
				colvarPagId.MaxLength = 0;
				colvarPagId.AutoIncrement = false;
				colvarPagId.IsNullable = false;
				colvarPagId.IsPrimaryKey = true;
				colvarPagId.IsForeignKey = false;
				colvarPagId.IsReadOnly = false;
				colvarPagId.DefaultSetting = @"";
				colvarPagId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPagId);
				
				TableSchema.TableColumn colvarPagTitle = new TableSchema.TableColumn(schema);
				colvarPagTitle.ColumnName = "PAG_Title";
				colvarPagTitle.DataType = DbType.String;
				colvarPagTitle.MaxLength = 255;
				colvarPagTitle.AutoIncrement = false;
				colvarPagTitle.IsNullable = false;
				colvarPagTitle.IsPrimaryKey = false;
				colvarPagTitle.IsForeignKey = false;
				colvarPagTitle.IsReadOnly = false;
				colvarPagTitle.DefaultSetting = @"";
				colvarPagTitle.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPagTitle);
				
				TableSchema.TableColumn colvarPagCreatedDate = new TableSchema.TableColumn(schema);
				colvarPagCreatedDate.ColumnName = "PAG_CreatedDate";
				colvarPagCreatedDate.DataType = DbType.DateTime;
				colvarPagCreatedDate.MaxLength = 0;
				colvarPagCreatedDate.AutoIncrement = false;
				colvarPagCreatedDate.IsNullable = false;
				colvarPagCreatedDate.IsPrimaryKey = false;
				colvarPagCreatedDate.IsForeignKey = false;
				colvarPagCreatedDate.IsReadOnly = false;
				colvarPagCreatedDate.DefaultSetting = @"";
				colvarPagCreatedDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPagCreatedDate);
				
				TableSchema.TableColumn colvarPagLastUpdate = new TableSchema.TableColumn(schema);
				colvarPagLastUpdate.ColumnName = "PAG_LastUpdate";
				colvarPagLastUpdate.DataType = DbType.DateTime;
				colvarPagLastUpdate.MaxLength = 0;
				colvarPagLastUpdate.AutoIncrement = false;
				colvarPagLastUpdate.IsNullable = false;
				colvarPagLastUpdate.IsPrimaryKey = false;
				colvarPagLastUpdate.IsForeignKey = false;
				colvarPagLastUpdate.IsReadOnly = false;
				colvarPagLastUpdate.DefaultSetting = @"";
				colvarPagLastUpdate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPagLastUpdate);
				
				TableSchema.TableColumn colvarCtyId = new TableSchema.TableColumn(schema);
				colvarCtyId.ColumnName = "CTY_ID";
				colvarCtyId.DataType = DbType.Guid;
				colvarCtyId.MaxLength = 0;
				colvarCtyId.AutoIncrement = false;
				colvarCtyId.IsNullable = false;
				colvarCtyId.IsPrimaryKey = false;
				colvarCtyId.IsForeignKey = true;
				colvarCtyId.IsReadOnly = false;
				colvarCtyId.DefaultSetting = @"";
				
					colvarCtyId.ForeignKeyTableName = "hitbl_Community_CTY";
				schema.Columns.Add(colvarCtyId);
				
				TableSchema.TableColumn colvarPagOrderNr = new TableSchema.TableColumn(schema);
				colvarPagOrderNr.ColumnName = "PAG_OrderNr";
				colvarPagOrderNr.DataType = DbType.Int32;
				colvarPagOrderNr.MaxLength = 0;
				colvarPagOrderNr.AutoIncrement = false;
				colvarPagOrderNr.IsNullable = false;
				colvarPagOrderNr.IsPrimaryKey = false;
				colvarPagOrderNr.IsForeignKey = false;
				colvarPagOrderNr.IsReadOnly = false;
				colvarPagOrderNr.DefaultSetting = @"";
				colvarPagOrderNr.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPagOrderNr);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["SqlDataProvider"].AddSchema("hitbl_Page_PAG",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("PagId")]
		[Bindable(true)]
		public Guid PagId 
		{
			get { return GetColumnValue<Guid>(Columns.PagId); }
			set { SetColumnValue(Columns.PagId, value); }
		}
		  
		[XmlAttribute("PagTitle")]
		[Bindable(true)]
		public string PagTitle 
		{
			get { return GetColumnValue<string>(Columns.PagTitle); }
			set { SetColumnValue(Columns.PagTitle, value); }
		}
		  
		[XmlAttribute("PagCreatedDate")]
		[Bindable(true)]
		public DateTime PagCreatedDate 
		{
			get { return GetColumnValue<DateTime>(Columns.PagCreatedDate); }
			set { SetColumnValue(Columns.PagCreatedDate, value); }
		}
		  
		[XmlAttribute("PagLastUpdate")]
		[Bindable(true)]
		public DateTime PagLastUpdate 
		{
			get { return GetColumnValue<DateTime>(Columns.PagLastUpdate); }
			set { SetColumnValue(Columns.PagLastUpdate, value); }
		}
		  
		[XmlAttribute("CtyId")]
		[Bindable(true)]
		public Guid CtyId 
		{
			get { return GetColumnValue<Guid>(Columns.CtyId); }
			set { SetColumnValue(Columns.CtyId, value); }
		}
		  
		[XmlAttribute("PagOrderNr")]
		[Bindable(true)]
		public int PagOrderNr 
		{
			get { return GetColumnValue<int>(Columns.PagOrderNr); }
			set { SetColumnValue(Columns.PagOrderNr, value); }
		}
		
		#endregion
		
		
		#region PrimaryKey Methods		
		
        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);
            
            SetPKValues();
        }
        
		
		public _4screen.CSB.DataAccess.Business.HitblWidgetInstanceInCollection HitblWidgetInstanceIns()
		{
			return new _4screen.CSB.DataAccess.Business.HitblWidgetInstanceInCollection().Where(HitblWidgetInstanceIn.Columns.InsPagId, PagId).Load();
		}
		#endregion
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a HitblCommunityCty ActiveRecord object related to this HitblPagePag
		/// 
		/// </summary>
		public _4screen.CSB.DataAccess.Business.HitblCommunityCty HitblCommunityCty
		{
			get { return _4screen.CSB.DataAccess.Business.HitblCommunityCty.FetchByID(this.CtyId); }
			set { SetColumnValue("CTY_ID", value.CtyId); }
		}
		
		
		#endregion
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(Guid varPagId,string varPagTitle,DateTime varPagCreatedDate,DateTime varPagLastUpdate,Guid varCtyId,int varPagOrderNr)
		{
			HitblPagePag item = new HitblPagePag();
			
			item.PagId = varPagId;
			
			item.PagTitle = varPagTitle;
			
			item.PagCreatedDate = varPagCreatedDate;
			
			item.PagLastUpdate = varPagLastUpdate;
			
			item.CtyId = varCtyId;
			
			item.PagOrderNr = varPagOrderNr;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(Guid varPagId,string varPagTitle,DateTime varPagCreatedDate,DateTime varPagLastUpdate,Guid varCtyId,int varPagOrderNr)
		{
			HitblPagePag item = new HitblPagePag();
			
				item.PagId = varPagId;
			
				item.PagTitle = varPagTitle;
			
				item.PagCreatedDate = varPagCreatedDate;
			
				item.PagLastUpdate = varPagLastUpdate;
			
				item.CtyId = varCtyId;
			
				item.PagOrderNr = varPagOrderNr;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn PagIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn PagTitleColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn PagCreatedDateColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn PagLastUpdateColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn CtyIdColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn PagOrderNrColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string PagId = @"PAG_ID";
			 public static string PagTitle = @"PAG_Title";
			 public static string PagCreatedDate = @"PAG_CreatedDate";
			 public static string PagLastUpdate = @"PAG_LastUpdate";
			 public static string CtyId = @"CTY_ID";
			 public static string PagOrderNr = @"PAG_OrderNr";
						
		}
		#endregion
		
		#region Update PK Collections
		
        public void SetPKValues()
        {
}
        #endregion
    
        #region Deep Save
		
        public void DeepSave()
        {
            Save();
            
}
        #endregion
	}
}
