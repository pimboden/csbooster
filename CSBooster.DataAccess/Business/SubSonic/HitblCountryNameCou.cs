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
	/// Strongly-typed collection for the HitblCountryNameCou class.
	/// </summary>
    [Serializable]
	public partial class HitblCountryNameCouCollection : ActiveList<HitblCountryNameCou, HitblCountryNameCouCollection>
	{	   
		public HitblCountryNameCouCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>HitblCountryNameCouCollection</returns>
		public HitblCountryNameCouCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                HitblCountryNameCou o = this[i];
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
	/// This is an ActiveRecord class which wraps the hitbl_CountryName_COU table.
	/// </summary>
	[Serializable]
	public partial class HitblCountryNameCou : ActiveRecord<HitblCountryNameCou>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public HitblCountryNameCou()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public HitblCountryNameCou(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public HitblCountryNameCou(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public HitblCountryNameCou(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("hitbl_CountryName_COU", TableType.Table, DataService.GetInstance("SqlDataProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarCountryCode = new TableSchema.TableColumn(schema);
				colvarCountryCode.ColumnName = "CountryCode";
				colvarCountryCode.DataType = DbType.AnsiString;
				colvarCountryCode.MaxLength = 2;
				colvarCountryCode.AutoIncrement = false;
				colvarCountryCode.IsNullable = false;
				colvarCountryCode.IsPrimaryKey = true;
				colvarCountryCode.IsForeignKey = false;
				colvarCountryCode.IsReadOnly = false;
				colvarCountryCode.DefaultSetting = @"";
				colvarCountryCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCountryCode);
				
				TableSchema.TableColumn colvarLangCode = new TableSchema.TableColumn(schema);
				colvarLangCode.ColumnName = "LangCode";
				colvarLangCode.DataType = DbType.String;
				colvarLangCode.MaxLength = 10;
				colvarLangCode.AutoIncrement = false;
				colvarLangCode.IsNullable = false;
				colvarLangCode.IsPrimaryKey = true;
				colvarLangCode.IsForeignKey = false;
				colvarLangCode.IsReadOnly = false;
				colvarLangCode.DefaultSetting = @"";
				colvarLangCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLangCode);
				
				TableSchema.TableColumn colvarCountryName = new TableSchema.TableColumn(schema);
				colvarCountryName.ColumnName = "CountryName";
				colvarCountryName.DataType = DbType.String;
				colvarCountryName.MaxLength = 150;
				colvarCountryName.AutoIncrement = false;
				colvarCountryName.IsNullable = true;
				colvarCountryName.IsPrimaryKey = false;
				colvarCountryName.IsForeignKey = false;
				colvarCountryName.IsReadOnly = false;
				colvarCountryName.DefaultSetting = @"";
				colvarCountryName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCountryName);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["SqlDataProvider"].AddSchema("hitbl_CountryName_COU",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("CountryCode")]
		[Bindable(true)]
		public string CountryCode 
		{
			get { return GetColumnValue<string>(Columns.CountryCode); }
			set { SetColumnValue(Columns.CountryCode, value); }
		}
		  
		[XmlAttribute("LangCode")]
		[Bindable(true)]
		public string LangCode 
		{
			get { return GetColumnValue<string>(Columns.LangCode); }
			set { SetColumnValue(Columns.LangCode, value); }
		}
		  
		[XmlAttribute("CountryName")]
		[Bindable(true)]
		public string CountryName 
		{
			get { return GetColumnValue<string>(Columns.CountryName); }
			set { SetColumnValue(Columns.CountryName, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varCountryCode,string varLangCode,string varCountryName)
		{
			HitblCountryNameCou item = new HitblCountryNameCou();
			
			item.CountryCode = varCountryCode;
			
			item.LangCode = varLangCode;
			
			item.CountryName = varCountryName;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(string varCountryCode,string varLangCode,string varCountryName)
		{
			HitblCountryNameCou item = new HitblCountryNameCou();
			
				item.CountryCode = varCountryCode;
			
				item.LangCode = varLangCode;
			
				item.CountryName = varCountryName;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn CountryCodeColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn LangCodeColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn CountryNameColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string CountryCode = @"CountryCode";
			 public static string LangCode = @"LangCode";
			 public static string CountryName = @"CountryName";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
