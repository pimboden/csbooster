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
	/// Strongly-typed collection for the HitblWidgetInstanceIn class.
	/// </summary>
    [Serializable]
	public partial class HitblWidgetInstanceInCollection : ActiveList<HitblWidgetInstanceIn, HitblWidgetInstanceInCollection>
	{	   
		public HitblWidgetInstanceInCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>HitblWidgetInstanceInCollection</returns>
		public HitblWidgetInstanceInCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                HitblWidgetInstanceIn o = this[i];
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
	/// This is an ActiveRecord class which wraps the hitbl_WidgetInstance_INS table.
	/// </summary>
	[Serializable]
	public partial class HitblWidgetInstanceIn : ActiveRecord<HitblWidgetInstanceIn>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public HitblWidgetInstanceIn()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public HitblWidgetInstanceIn(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public HitblWidgetInstanceIn(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public HitblWidgetInstanceIn(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("hitbl_WidgetInstance_INS", TableType.Table, DataService.GetInstance("SqlDataProvider"));
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
				colvarInsId.IsForeignKey = false;
				colvarInsId.IsReadOnly = false;
				colvarInsId.DefaultSetting = @"";
				colvarInsId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInsId);
				
				TableSchema.TableColumn colvarWdgId = new TableSchema.TableColumn(schema);
				colvarWdgId.ColumnName = "WDG_ID";
				colvarWdgId.DataType = DbType.Guid;
				colvarWdgId.MaxLength = 0;
				colvarWdgId.AutoIncrement = false;
				colvarWdgId.IsNullable = false;
				colvarWdgId.IsPrimaryKey = false;
				colvarWdgId.IsForeignKey = false;
				colvarWdgId.IsReadOnly = false;
				colvarWdgId.DefaultSetting = @"";
				colvarWdgId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWdgId);
				
				TableSchema.TableColumn colvarInsPagId = new TableSchema.TableColumn(schema);
				colvarInsPagId.ColumnName = "INS_PAG_ID";
				colvarInsPagId.DataType = DbType.Guid;
				colvarInsPagId.MaxLength = 0;
				colvarInsPagId.AutoIncrement = false;
				colvarInsPagId.IsNullable = false;
				colvarInsPagId.IsPrimaryKey = false;
				colvarInsPagId.IsForeignKey = true;
				colvarInsPagId.IsReadOnly = false;
				colvarInsPagId.DefaultSetting = @"";
				
					colvarInsPagId.ForeignKeyTableName = "hitbl_Page_PAG";
				schema.Columns.Add(colvarInsPagId);
				
				TableSchema.TableColumn colvarInsColumnNo = new TableSchema.TableColumn(schema);
				colvarInsColumnNo.ColumnName = "INS_ColumnNo";
				colvarInsColumnNo.DataType = DbType.Int32;
				colvarInsColumnNo.MaxLength = 0;
				colvarInsColumnNo.AutoIncrement = false;
				colvarInsColumnNo.IsNullable = false;
				colvarInsColumnNo.IsPrimaryKey = false;
				colvarInsColumnNo.IsForeignKey = false;
				colvarInsColumnNo.IsReadOnly = false;
				colvarInsColumnNo.DefaultSetting = @"";
				colvarInsColumnNo.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInsColumnNo);
				
				TableSchema.TableColumn colvarInsOrderNo = new TableSchema.TableColumn(schema);
				colvarInsOrderNo.ColumnName = "INS_OrderNo";
				colvarInsOrderNo.DataType = DbType.Int32;
				colvarInsOrderNo.MaxLength = 0;
				colvarInsOrderNo.AutoIncrement = false;
				colvarInsOrderNo.IsNullable = false;
				colvarInsOrderNo.IsPrimaryKey = false;
				colvarInsOrderNo.IsForeignKey = false;
				colvarInsOrderNo.IsReadOnly = false;
				colvarInsOrderNo.DefaultSetting = @"";
				colvarInsOrderNo.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInsOrderNo);
				
				TableSchema.TableColumn colvarInsXmlStateData = new TableSchema.TableColumn(schema);
				colvarInsXmlStateData.ColumnName = "INS_XmlStateData";
				colvarInsXmlStateData.DataType = DbType.String;
				colvarInsXmlStateData.MaxLength = -1;
				colvarInsXmlStateData.AutoIncrement = false;
				colvarInsXmlStateData.IsNullable = true;
				colvarInsXmlStateData.IsPrimaryKey = false;
				colvarInsXmlStateData.IsForeignKey = false;
				colvarInsXmlStateData.IsReadOnly = false;
				colvarInsXmlStateData.DefaultSetting = @"";
				colvarInsXmlStateData.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInsXmlStateData);
				
				TableSchema.TableColumn colvarInsCreatedDate = new TableSchema.TableColumn(schema);
				colvarInsCreatedDate.ColumnName = "INS_CreatedDate";
				colvarInsCreatedDate.DataType = DbType.DateTime;
				colvarInsCreatedDate.MaxLength = 0;
				colvarInsCreatedDate.AutoIncrement = false;
				colvarInsCreatedDate.IsNullable = false;
				colvarInsCreatedDate.IsPrimaryKey = false;
				colvarInsCreatedDate.IsForeignKey = false;
				colvarInsCreatedDate.IsReadOnly = false;
				colvarInsCreatedDate.DefaultSetting = @"";
				colvarInsCreatedDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInsCreatedDate);
				
				TableSchema.TableColumn colvarInsLastUpdate = new TableSchema.TableColumn(schema);
				colvarInsLastUpdate.ColumnName = "INS_LastUpdate";
				colvarInsLastUpdate.DataType = DbType.DateTime;
				colvarInsLastUpdate.MaxLength = 0;
				colvarInsLastUpdate.AutoIncrement = false;
				colvarInsLastUpdate.IsNullable = false;
				colvarInsLastUpdate.IsPrimaryKey = false;
				colvarInsLastUpdate.IsForeignKey = false;
				colvarInsLastUpdate.IsReadOnly = false;
				colvarInsLastUpdate.DefaultSetting = @"";
				colvarInsLastUpdate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInsLastUpdate);
				
				TableSchema.TableColumn colvarInsExpanded = new TableSchema.TableColumn(schema);
				colvarInsExpanded.ColumnName = "INS_Expanded";
				colvarInsExpanded.DataType = DbType.Boolean;
				colvarInsExpanded.MaxLength = 0;
				colvarInsExpanded.AutoIncrement = false;
				colvarInsExpanded.IsNullable = false;
				colvarInsExpanded.IsPrimaryKey = false;
				colvarInsExpanded.IsForeignKey = false;
				colvarInsExpanded.IsReadOnly = false;
				colvarInsExpanded.DefaultSetting = @"";
				colvarInsExpanded.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInsExpanded);
				
				TableSchema.TableColumn colvarWtpId = new TableSchema.TableColumn(schema);
				colvarWtpId.ColumnName = "WTP_ID";
				colvarWtpId.DataType = DbType.Guid;
				colvarWtpId.MaxLength = 0;
				colvarWtpId.AutoIncrement = false;
				colvarWtpId.IsNullable = true;
				colvarWtpId.IsPrimaryKey = false;
				colvarWtpId.IsForeignKey = false;
				colvarWtpId.IsReadOnly = false;
				colvarWtpId.DefaultSetting = @"";
				colvarWtpId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWtpId);
				
				TableSchema.TableColumn colvarInsIsFixed = new TableSchema.TableColumn(schema);
				colvarInsIsFixed.ColumnName = "INS_IsFixed";
				colvarInsIsFixed.DataType = DbType.Boolean;
				colvarInsIsFixed.MaxLength = 0;
				colvarInsIsFixed.AutoIncrement = false;
				colvarInsIsFixed.IsNullable = false;
				colvarInsIsFixed.IsPrimaryKey = false;
				colvarInsIsFixed.IsForeignKey = false;
				colvarInsIsFixed.IsReadOnly = false;
				
						colvarInsIsFixed.DefaultSetting = @"((0))";
				colvarInsIsFixed.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInsIsFixed);
				
				TableSchema.TableColumn colvarInsHideIfNoContent = new TableSchema.TableColumn(schema);
				colvarInsHideIfNoContent.ColumnName = "INS_HideIfNoContent";
				colvarInsHideIfNoContent.DataType = DbType.Boolean;
				colvarInsHideIfNoContent.MaxLength = 0;
				colvarInsHideIfNoContent.AutoIncrement = false;
				colvarInsHideIfNoContent.IsNullable = false;
				colvarInsHideIfNoContent.IsPrimaryKey = false;
				colvarInsHideIfNoContent.IsForeignKey = false;
				colvarInsHideIfNoContent.IsReadOnly = false;
				
						colvarInsHideIfNoContent.DefaultSetting = @"((0))";
				colvarInsHideIfNoContent.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInsHideIfNoContent);
				
				TableSchema.TableColumn colvarInsViewRoles = new TableSchema.TableColumn(schema);
				colvarInsViewRoles.ColumnName = "INS_ViewRoles";
				colvarInsViewRoles.DataType = DbType.String;
				colvarInsViewRoles.MaxLength = -1;
				colvarInsViewRoles.AutoIncrement = false;
				colvarInsViewRoles.IsNullable = true;
				colvarInsViewRoles.IsPrimaryKey = false;
				colvarInsViewRoles.IsForeignKey = false;
				colvarInsViewRoles.IsReadOnly = false;
				colvarInsViewRoles.DefaultSetting = @"";
				colvarInsViewRoles.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInsViewRoles);
				
				TableSchema.TableColumn colvarInsOutputTemplate = new TableSchema.TableColumn(schema);
				colvarInsOutputTemplate.ColumnName = "INS_OutputTemplate";
				colvarInsOutputTemplate.DataType = DbType.Guid;
				colvarInsOutputTemplate.MaxLength = 0;
				colvarInsOutputTemplate.AutoIncrement = false;
				colvarInsOutputTemplate.IsNullable = true;
				colvarInsOutputTemplate.IsPrimaryKey = false;
				colvarInsOutputTemplate.IsForeignKey = false;
				colvarInsOutputTemplate.IsReadOnly = false;
				colvarInsOutputTemplate.DefaultSetting = @"";
				colvarInsOutputTemplate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInsOutputTemplate);
				
				TableSchema.TableColumn colvarInsHeadingTag = new TableSchema.TableColumn(schema);
				colvarInsHeadingTag.ColumnName = "INS_HeadingTag";
				colvarInsHeadingTag.DataType = DbType.Int32;
				colvarInsHeadingTag.MaxLength = 0;
				colvarInsHeadingTag.AutoIncrement = false;
				colvarInsHeadingTag.IsNullable = true;
				colvarInsHeadingTag.IsPrimaryKey = false;
				colvarInsHeadingTag.IsForeignKey = false;
				colvarInsHeadingTag.IsReadOnly = false;
				
						colvarInsHeadingTag.DefaultSetting = @"((3))";
				colvarInsHeadingTag.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInsHeadingTag);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["SqlDataProvider"].AddSchema("hitbl_WidgetInstance_INS",schema);
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
		  
		[XmlAttribute("WdgId")]
		[Bindable(true)]
		public Guid WdgId 
		{
			get { return GetColumnValue<Guid>(Columns.WdgId); }
			set { SetColumnValue(Columns.WdgId, value); }
		}
		  
		[XmlAttribute("InsPagId")]
		[Bindable(true)]
		public Guid InsPagId 
		{
			get { return GetColumnValue<Guid>(Columns.InsPagId); }
			set { SetColumnValue(Columns.InsPagId, value); }
		}
		  
		[XmlAttribute("InsColumnNo")]
		[Bindable(true)]
		public int InsColumnNo 
		{
			get { return GetColumnValue<int>(Columns.InsColumnNo); }
			set { SetColumnValue(Columns.InsColumnNo, value); }
		}
		  
		[XmlAttribute("InsOrderNo")]
		[Bindable(true)]
		public int InsOrderNo 
		{
			get { return GetColumnValue<int>(Columns.InsOrderNo); }
			set { SetColumnValue(Columns.InsOrderNo, value); }
		}
		  
		[XmlAttribute("InsXmlStateData")]
		[Bindable(true)]
		public string InsXmlStateData 
		{
			get { return GetColumnValue<string>(Columns.InsXmlStateData); }
			set { SetColumnValue(Columns.InsXmlStateData, value); }
		}
		  
		[XmlAttribute("InsCreatedDate")]
		[Bindable(true)]
		public DateTime InsCreatedDate 
		{
			get { return GetColumnValue<DateTime>(Columns.InsCreatedDate); }
			set { SetColumnValue(Columns.InsCreatedDate, value); }
		}
		  
		[XmlAttribute("InsLastUpdate")]
		[Bindable(true)]
		public DateTime InsLastUpdate 
		{
			get { return GetColumnValue<DateTime>(Columns.InsLastUpdate); }
			set { SetColumnValue(Columns.InsLastUpdate, value); }
		}
		  
		[XmlAttribute("InsExpanded")]
		[Bindable(true)]
		public bool InsExpanded 
		{
			get { return GetColumnValue<bool>(Columns.InsExpanded); }
			set { SetColumnValue(Columns.InsExpanded, value); }
		}
		  
		[XmlAttribute("WtpId")]
		[Bindable(true)]
		public Guid? WtpId 
		{
			get { return GetColumnValue<Guid?>(Columns.WtpId); }
			set { SetColumnValue(Columns.WtpId, value); }
		}
		  
		[XmlAttribute("InsIsFixed")]
		[Bindable(true)]
		public bool InsIsFixed 
		{
			get { return GetColumnValue<bool>(Columns.InsIsFixed); }
			set { SetColumnValue(Columns.InsIsFixed, value); }
		}
		  
		[XmlAttribute("InsHideIfNoContent")]
		[Bindable(true)]
		public bool InsHideIfNoContent 
		{
			get { return GetColumnValue<bool>(Columns.InsHideIfNoContent); }
			set { SetColumnValue(Columns.InsHideIfNoContent, value); }
		}
		  
		[XmlAttribute("InsViewRoles")]
		[Bindable(true)]
		public string InsViewRoles 
		{
			get { return GetColumnValue<string>(Columns.InsViewRoles); }
			set { SetColumnValue(Columns.InsViewRoles, value); }
		}
		  
		[XmlAttribute("InsOutputTemplate")]
		[Bindable(true)]
		public Guid? InsOutputTemplate 
		{
			get { return GetColumnValue<Guid?>(Columns.InsOutputTemplate); }
			set { SetColumnValue(Columns.InsOutputTemplate, value); }
		}
		  
		[XmlAttribute("InsHeadingTag")]
		[Bindable(true)]
		public int? InsHeadingTag 
		{
			get { return GetColumnValue<int?>(Columns.InsHeadingTag); }
			set { SetColumnValue(Columns.InsHeadingTag, value); }
		}
		
		#endregion
		
		
		#region PrimaryKey Methods		
		
        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);
            
            SetPKValues();
        }
        
		
		public _4screen.CSB.DataAccess.Business.HitblWidgetInstanceTextWitCollection HitblWidgetInstanceTextWitRecords()
		{
			return new _4screen.CSB.DataAccess.Business.HitblWidgetInstanceTextWitCollection().Where(HitblWidgetInstanceTextWit.Columns.InsId, InsId).Load();
		}
		#endregion
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a HitblPagePag ActiveRecord object related to this HitblWidgetInstanceIn
		/// 
		/// </summary>
		public _4screen.CSB.DataAccess.Business.HitblPagePag HitblPagePag
		{
			get { return _4screen.CSB.DataAccess.Business.HitblPagePag.FetchByID(this.InsPagId); }
			set { SetColumnValue("INS_PAG_ID", value.PagId); }
		}
		
		
		#endregion
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(Guid varInsId,Guid varWdgId,Guid varInsPagId,int varInsColumnNo,int varInsOrderNo,string varInsXmlStateData,DateTime varInsCreatedDate,DateTime varInsLastUpdate,bool varInsExpanded,Guid? varWtpId,bool varInsIsFixed,bool varInsHideIfNoContent,string varInsViewRoles,Guid? varInsOutputTemplate,int? varInsHeadingTag)
		{
			HitblWidgetInstanceIn item = new HitblWidgetInstanceIn();
			
			item.InsId = varInsId;
			
			item.WdgId = varWdgId;
			
			item.InsPagId = varInsPagId;
			
			item.InsColumnNo = varInsColumnNo;
			
			item.InsOrderNo = varInsOrderNo;
			
			item.InsXmlStateData = varInsXmlStateData;
			
			item.InsCreatedDate = varInsCreatedDate;
			
			item.InsLastUpdate = varInsLastUpdate;
			
			item.InsExpanded = varInsExpanded;
			
			item.WtpId = varWtpId;
			
			item.InsIsFixed = varInsIsFixed;
			
			item.InsHideIfNoContent = varInsHideIfNoContent;
			
			item.InsViewRoles = varInsViewRoles;
			
			item.InsOutputTemplate = varInsOutputTemplate;
			
			item.InsHeadingTag = varInsHeadingTag;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(Guid varInsId,Guid varWdgId,Guid varInsPagId,int varInsColumnNo,int varInsOrderNo,string varInsXmlStateData,DateTime varInsCreatedDate,DateTime varInsLastUpdate,bool varInsExpanded,Guid? varWtpId,bool varInsIsFixed,bool varInsHideIfNoContent,string varInsViewRoles,Guid? varInsOutputTemplate,int? varInsHeadingTag)
		{
			HitblWidgetInstanceIn item = new HitblWidgetInstanceIn();
			
				item.InsId = varInsId;
			
				item.WdgId = varWdgId;
			
				item.InsPagId = varInsPagId;
			
				item.InsColumnNo = varInsColumnNo;
			
				item.InsOrderNo = varInsOrderNo;
			
				item.InsXmlStateData = varInsXmlStateData;
			
				item.InsCreatedDate = varInsCreatedDate;
			
				item.InsLastUpdate = varInsLastUpdate;
			
				item.InsExpanded = varInsExpanded;
			
				item.WtpId = varWtpId;
			
				item.InsIsFixed = varInsIsFixed;
			
				item.InsHideIfNoContent = varInsHideIfNoContent;
			
				item.InsViewRoles = varInsViewRoles;
			
				item.InsOutputTemplate = varInsOutputTemplate;
			
				item.InsHeadingTag = varInsHeadingTag;
			
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
        
        
        
        public static TableSchema.TableColumn WdgIdColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn InsPagIdColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn InsColumnNoColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn InsOrderNoColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn InsXmlStateDataColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn InsCreatedDateColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn InsLastUpdateColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn InsExpandedColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn WtpIdColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn InsIsFixedColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn InsHideIfNoContentColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn InsViewRolesColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn InsOutputTemplateColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn InsHeadingTagColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string InsId = @"INS_ID";
			 public static string WdgId = @"WDG_ID";
			 public static string InsPagId = @"INS_PAG_ID";
			 public static string InsColumnNo = @"INS_ColumnNo";
			 public static string InsOrderNo = @"INS_OrderNo";
			 public static string InsXmlStateData = @"INS_XmlStateData";
			 public static string InsCreatedDate = @"INS_CreatedDate";
			 public static string InsLastUpdate = @"INS_LastUpdate";
			 public static string InsExpanded = @"INS_Expanded";
			 public static string WtpId = @"WTP_ID";
			 public static string InsIsFixed = @"INS_IsFixed";
			 public static string InsHideIfNoContent = @"INS_HideIfNoContent";
			 public static string InsViewRoles = @"INS_ViewRoles";
			 public static string InsOutputTemplate = @"INS_OutputTemplate";
			 public static string InsHeadingTag = @"INS_HeadingTag";
						
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
