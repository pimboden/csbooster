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
	/// Strongly-typed collection for the HitblMainMan class.
	/// </summary>
    [Serializable]
	public partial class HitblMainManCollection : ActiveList<HitblMainMan, HitblMainManCollection>
	{	   
		public HitblMainManCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>HitblMainManCollection</returns>
		public HitblMainManCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                HitblMainMan o = this[i];
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
	/// This is an ActiveRecord class which wraps the hitbl_Main_MAN table.
	/// </summary>
	[Serializable]
	public partial class HitblMainMan : ActiveRecord<HitblMainMan>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public HitblMainMan()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public HitblMainMan(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public HitblMainMan(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public HitblMainMan(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("hitbl_Main_MAN", TableType.Table, DataService.GetInstance("SqlDataProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarManId = new TableSchema.TableColumn(schema);
				colvarManId.ColumnName = "MAN_ID";
				colvarManId.DataType = DbType.Int32;
				colvarManId.MaxLength = 0;
				colvarManId.AutoIncrement = true;
				colvarManId.IsNullable = false;
				colvarManId.IsPrimaryKey = true;
				colvarManId.IsForeignKey = false;
				colvarManId.IsReadOnly = false;
				colvarManId.DefaultSetting = @"";
				colvarManId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarManId);
				
				TableSchema.TableColumn colvarManLevel = new TableSchema.TableColumn(schema);
				colvarManLevel.ColumnName = "MAN_Level";
				colvarManLevel.DataType = DbType.Int32;
				colvarManLevel.MaxLength = 0;
				colvarManLevel.AutoIncrement = false;
				colvarManLevel.IsNullable = true;
				colvarManLevel.IsPrimaryKey = false;
				colvarManLevel.IsForeignKey = false;
				colvarManLevel.IsReadOnly = false;
				colvarManLevel.DefaultSetting = @"";
				colvarManLevel.ForeignKeyTableName = "";
				schema.Columns.Add(colvarManLevel);
				
				TableSchema.TableColumn colvarManManId = new TableSchema.TableColumn(schema);
				colvarManManId.ColumnName = "MAN_MAN_ID";
				colvarManManId.DataType = DbType.Int32;
				colvarManManId.MaxLength = 0;
				colvarManManId.AutoIncrement = false;
				colvarManManId.IsNullable = true;
				colvarManManId.IsPrimaryKey = false;
				colvarManManId.IsForeignKey = true;
				colvarManManId.IsReadOnly = false;
				colvarManManId.DefaultSetting = @"";
				
					colvarManManId.ForeignKeyTableName = "hitbl_Main_MAN";
				schema.Columns.Add(colvarManManId);
				
				TableSchema.TableColumn colvarManTitle = new TableSchema.TableColumn(schema);
				colvarManTitle.ColumnName = "MAN_Title";
				colvarManTitle.DataType = DbType.String;
				colvarManTitle.MaxLength = 50;
				colvarManTitle.AutoIncrement = false;
				colvarManTitle.IsNullable = false;
				colvarManTitle.IsPrimaryKey = false;
				colvarManTitle.IsForeignKey = false;
				colvarManTitle.IsReadOnly = false;
				colvarManTitle.DefaultSetting = @"";
				colvarManTitle.ForeignKeyTableName = "";
				schema.Columns.Add(colvarManTitle);
				
				TableSchema.TableColumn colvarManStatus = new TableSchema.TableColumn(schema);
				colvarManStatus.ColumnName = "MAN_Status";
				colvarManStatus.DataType = DbType.Int32;
				colvarManStatus.MaxLength = 0;
				colvarManStatus.AutoIncrement = false;
				colvarManStatus.IsNullable = true;
				colvarManStatus.IsPrimaryKey = false;
				colvarManStatus.IsForeignKey = false;
				colvarManStatus.IsReadOnly = false;
				colvarManStatus.DefaultSetting = @"";
				colvarManStatus.ForeignKeyTableName = "";
				schema.Columns.Add(colvarManStatus);
				
				TableSchema.TableColumn colvarManOrder = new TableSchema.TableColumn(schema);
				colvarManOrder.ColumnName = "MAN_Order";
				colvarManOrder.DataType = DbType.Int32;
				colvarManOrder.MaxLength = 0;
				colvarManOrder.AutoIncrement = false;
				colvarManOrder.IsNullable = false;
				colvarManOrder.IsPrimaryKey = false;
				colvarManOrder.IsForeignKey = false;
				colvarManOrder.IsReadOnly = false;
				
						colvarManOrder.DefaultSetting = @"((1))";
				colvarManOrder.ForeignKeyTableName = "";
				schema.Columns.Add(colvarManOrder);
				
				TableSchema.TableColumn colvarTgwId = new TableSchema.TableColumn(schema);
				colvarTgwId.ColumnName = "TGW_ID";
				colvarTgwId.DataType = DbType.Guid;
				colvarTgwId.MaxLength = 0;
				colvarTgwId.AutoIncrement = false;
				colvarTgwId.IsNullable = false;
				colvarTgwId.IsPrimaryKey = false;
				colvarTgwId.IsForeignKey = false;
				colvarTgwId.IsReadOnly = false;
				
						colvarTgwId.DefaultSetting = @"(newid())";
				colvarTgwId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTgwId);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["SqlDataProvider"].AddSchema("hitbl_Main_MAN",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("ManId")]
		[Bindable(true)]
		public int ManId 
		{
			get { return GetColumnValue<int>(Columns.ManId); }
			set { SetColumnValue(Columns.ManId, value); }
		}
		  
		[XmlAttribute("ManLevel")]
		[Bindable(true)]
		public int? ManLevel 
		{
			get { return GetColumnValue<int?>(Columns.ManLevel); }
			set { SetColumnValue(Columns.ManLevel, value); }
		}
		  
		[XmlAttribute("ManManId")]
		[Bindable(true)]
		public int? ManManId 
		{
			get { return GetColumnValue<int?>(Columns.ManManId); }
			set { SetColumnValue(Columns.ManManId, value); }
		}
		  
		[XmlAttribute("ManTitle")]
		[Bindable(true)]
		public string ManTitle 
		{
			get { return GetColumnValue<string>(Columns.ManTitle); }
			set { SetColumnValue(Columns.ManTitle, value); }
		}
		  
		[XmlAttribute("ManStatus")]
		[Bindable(true)]
		public int? ManStatus 
		{
			get { return GetColumnValue<int?>(Columns.ManStatus); }
			set { SetColumnValue(Columns.ManStatus, value); }
		}
		  
		[XmlAttribute("ManOrder")]
		[Bindable(true)]
		public int ManOrder 
		{
			get { return GetColumnValue<int>(Columns.ManOrder); }
			set { SetColumnValue(Columns.ManOrder, value); }
		}
		  
		[XmlAttribute("TgwId")]
		[Bindable(true)]
		public Guid TgwId 
		{
			get { return GetColumnValue<Guid>(Columns.TgwId); }
			set { SetColumnValue(Columns.TgwId, value); }
		}
		
		#endregion
		
		
		#region PrimaryKey Methods		
		
        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);
            
            SetPKValues();
        }
        
		
		public _4screen.CSB.DataAccess.Business.HitblMainManCollection ChildHitblMainManRecords()
		{
			return new _4screen.CSB.DataAccess.Business.HitblMainManCollection().Where(HitblMainMan.Columns.ManManId, ManId).Load();
		}
		#endregion
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a HitblMainMan ActiveRecord object related to this HitblMainMan
		/// 
		/// </summary>
		public _4screen.CSB.DataAccess.Business.HitblMainMan ParentHitblMainMan
		{
			get { return _4screen.CSB.DataAccess.Business.HitblMainMan.FetchByID(this.ManManId); }
			set { SetColumnValue("MAN_MAN_ID", value.ManId); }
		}
		
		
		#endregion
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int? varManLevel,int? varManManId,string varManTitle,int? varManStatus,int varManOrder,Guid varTgwId)
		{
			HitblMainMan item = new HitblMainMan();
			
			item.ManLevel = varManLevel;
			
			item.ManManId = varManManId;
			
			item.ManTitle = varManTitle;
			
			item.ManStatus = varManStatus;
			
			item.ManOrder = varManOrder;
			
			item.TgwId = varTgwId;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varManId,int? varManLevel,int? varManManId,string varManTitle,int? varManStatus,int varManOrder,Guid varTgwId)
		{
			HitblMainMan item = new HitblMainMan();
			
				item.ManId = varManId;
			
				item.ManLevel = varManLevel;
			
				item.ManManId = varManManId;
			
				item.ManTitle = varManTitle;
			
				item.ManStatus = varManStatus;
			
				item.ManOrder = varManOrder;
			
				item.TgwId = varTgwId;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn ManIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn ManLevelColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn ManManIdColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn ManTitleColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn ManStatusColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn ManOrderColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn TgwIdColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string ManId = @"MAN_ID";
			 public static string ManLevel = @"MAN_Level";
			 public static string ManManId = @"MAN_MAN_ID";
			 public static string ManTitle = @"MAN_Title";
			 public static string ManStatus = @"MAN_Status";
			 public static string ManOrder = @"MAN_Order";
			 public static string TgwId = @"TGW_ID";
						
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
