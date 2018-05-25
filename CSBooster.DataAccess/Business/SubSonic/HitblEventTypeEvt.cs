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
	/// Strongly-typed collection for the HitblEventTypeEvt class.
	/// </summary>
    [Serializable]
	public partial class HitblEventTypeEvtCollection : ActiveList<HitblEventTypeEvt, HitblEventTypeEvtCollection>
	{	   
		public HitblEventTypeEvtCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>HitblEventTypeEvtCollection</returns>
		public HitblEventTypeEvtCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                HitblEventTypeEvt o = this[i];
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
	/// This is an ActiveRecord class which wraps the hitbl_Event_Type_EVT table.
	/// </summary>
	[Serializable]
	public partial class HitblEventTypeEvt : ActiveRecord<HitblEventTypeEvt>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public HitblEventTypeEvt()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public HitblEventTypeEvt(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public HitblEventTypeEvt(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public HitblEventTypeEvt(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("hitbl_Event_Type_EVT", TableType.Table, DataService.GetInstance("SqlDataProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarEvtId = new TableSchema.TableColumn(schema);
				colvarEvtId.ColumnName = "EVT_ID";
				colvarEvtId.DataType = DbType.Int32;
				colvarEvtId.MaxLength = 0;
				colvarEvtId.AutoIncrement = false;
				colvarEvtId.IsNullable = false;
				colvarEvtId.IsPrimaryKey = true;
				colvarEvtId.IsForeignKey = false;
				colvarEvtId.IsReadOnly = false;
				colvarEvtId.DefaultSetting = @"";
				colvarEvtId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEvtId);
				
				TableSchema.TableColumn colvarEvtName = new TableSchema.TableColumn(schema);
				colvarEvtName.ColumnName = "EVT_Name";
				colvarEvtName.DataType = DbType.String;
				colvarEvtName.MaxLength = 200;
				colvarEvtName.AutoIncrement = false;
				colvarEvtName.IsNullable = false;
				colvarEvtName.IsPrimaryKey = false;
				colvarEvtName.IsForeignKey = false;
				colvarEvtName.IsReadOnly = false;
				colvarEvtName.DefaultSetting = @"";
				colvarEvtName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEvtName);
				
				TableSchema.TableColumn colvarEvtSortOrder = new TableSchema.TableColumn(schema);
				colvarEvtSortOrder.ColumnName = "EVT_SortOrder";
				colvarEvtSortOrder.DataType = DbType.Int32;
				colvarEvtSortOrder.MaxLength = 0;
				colvarEvtSortOrder.AutoIncrement = false;
				colvarEvtSortOrder.IsNullable = false;
				colvarEvtSortOrder.IsPrimaryKey = false;
				colvarEvtSortOrder.IsForeignKey = false;
				colvarEvtSortOrder.IsReadOnly = false;
				colvarEvtSortOrder.DefaultSetting = @"";
				colvarEvtSortOrder.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEvtSortOrder);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["SqlDataProvider"].AddSchema("hitbl_Event_Type_EVT",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("EvtId")]
		[Bindable(true)]
		public int EvtId 
		{
			get { return GetColumnValue<int>(Columns.EvtId); }
			set { SetColumnValue(Columns.EvtId, value); }
		}
		  
		[XmlAttribute("EvtName")]
		[Bindable(true)]
		public string EvtName 
		{
			get { return GetColumnValue<string>(Columns.EvtName); }
			set { SetColumnValue(Columns.EvtName, value); }
		}
		  
		[XmlAttribute("EvtSortOrder")]
		[Bindable(true)]
		public int EvtSortOrder 
		{
			get { return GetColumnValue<int>(Columns.EvtSortOrder); }
			set { SetColumnValue(Columns.EvtSortOrder, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int varEvtId,string varEvtName,int varEvtSortOrder)
		{
			HitblEventTypeEvt item = new HitblEventTypeEvt();
			
			item.EvtId = varEvtId;
			
			item.EvtName = varEvtName;
			
			item.EvtSortOrder = varEvtSortOrder;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varEvtId,string varEvtName,int varEvtSortOrder)
		{
			HitblEventTypeEvt item = new HitblEventTypeEvt();
			
				item.EvtId = varEvtId;
			
				item.EvtName = varEvtName;
			
				item.EvtSortOrder = varEvtSortOrder;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn EvtIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn EvtNameColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn EvtSortOrderColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string EvtId = @"EVT_ID";
			 public static string EvtName = @"EVT_Name";
			 public static string EvtSortOrder = @"EVT_SortOrder";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
