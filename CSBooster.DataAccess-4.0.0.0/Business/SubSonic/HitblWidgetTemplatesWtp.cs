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
	/// Strongly-typed collection for the HitblWidgetTemplatesWtp class.
	/// </summary>
    [Serializable]
	public partial class HitblWidgetTemplatesWtpCollection : ActiveList<HitblWidgetTemplatesWtp, HitblWidgetTemplatesWtpCollection>
	{	   
		public HitblWidgetTemplatesWtpCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>HitblWidgetTemplatesWtpCollection</returns>
		public HitblWidgetTemplatesWtpCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                HitblWidgetTemplatesWtp o = this[i];
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
	/// This is an ActiveRecord class which wraps the hitbl_WidgetTemplates_WTP table.
	/// </summary>
	[Serializable]
	public partial class HitblWidgetTemplatesWtp : ActiveRecord<HitblWidgetTemplatesWtp>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public HitblWidgetTemplatesWtp()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public HitblWidgetTemplatesWtp(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public HitblWidgetTemplatesWtp(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public HitblWidgetTemplatesWtp(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("hitbl_WidgetTemplates_WTP", TableType.Table, DataService.GetInstance("SqlDataProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarWtpId = new TableSchema.TableColumn(schema);
				colvarWtpId.ColumnName = "WTP_ID";
				colvarWtpId.DataType = DbType.Guid;
				colvarWtpId.MaxLength = 0;
				colvarWtpId.AutoIncrement = false;
				colvarWtpId.IsNullable = false;
				colvarWtpId.IsPrimaryKey = true;
				colvarWtpId.IsForeignKey = false;
				colvarWtpId.IsReadOnly = false;
				colvarWtpId.DefaultSetting = @"";
				colvarWtpId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWtpId);
				
				TableSchema.TableColumn colvarUserID = new TableSchema.TableColumn(schema);
				colvarUserID.ColumnName = "UserId";
				colvarUserID.DataType = DbType.Guid;
				colvarUserID.MaxLength = 0;
				colvarUserID.AutoIncrement = false;
				colvarUserID.IsNullable = true;
				colvarUserID.IsPrimaryKey = false;
				colvarUserID.IsForeignKey = true;
				colvarUserID.IsReadOnly = false;
				colvarUserID.DefaultSetting = @"";
				
					colvarUserID.ForeignKeyTableName = "aspnet_Membership";
				schema.Columns.Add(colvarUserID);
				
				TableSchema.TableColumn colvarWtpName = new TableSchema.TableColumn(schema);
				colvarWtpName.ColumnName = "WTP_Name";
				colvarWtpName.DataType = DbType.String;
				colvarWtpName.MaxLength = 255;
				colvarWtpName.AutoIncrement = false;
				colvarWtpName.IsNullable = false;
				colvarWtpName.IsPrimaryKey = false;
				colvarWtpName.IsForeignKey = false;
				colvarWtpName.IsReadOnly = false;
				colvarWtpName.DefaultSetting = @"";
				colvarWtpName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWtpName);
				
				TableSchema.TableColumn colvarWtpTemplate = new TableSchema.TableColumn(schema);
				colvarWtpTemplate.ColumnName = "WTP_Template";
				colvarWtpTemplate.DataType = DbType.String;
				colvarWtpTemplate.MaxLength = -1;
				colvarWtpTemplate.AutoIncrement = false;
				colvarWtpTemplate.IsNullable = false;
				colvarWtpTemplate.IsPrimaryKey = false;
				colvarWtpTemplate.IsForeignKey = false;
				colvarWtpTemplate.IsReadOnly = false;
				colvarWtpTemplate.DefaultSetting = @"";
				colvarWtpTemplate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWtpTemplate);
				
				TableSchema.TableColumn colvarWtpXMLTemplate = new TableSchema.TableColumn(schema);
				colvarWtpXMLTemplate.ColumnName = "WTP_XMLTemplate";
				colvarWtpXMLTemplate.DataType = DbType.String;
				colvarWtpXMLTemplate.MaxLength = -1;
				colvarWtpXMLTemplate.AutoIncrement = false;
				colvarWtpXMLTemplate.IsNullable = false;
				colvarWtpXMLTemplate.IsPrimaryKey = false;
				colvarWtpXMLTemplate.IsForeignKey = false;
				colvarWtpXMLTemplate.IsReadOnly = false;
				colvarWtpXMLTemplate.DefaultSetting = @"";
				colvarWtpXMLTemplate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWtpXMLTemplate);
				
				TableSchema.TableColumn colvarWtpExplicitInserted = new TableSchema.TableColumn(schema);
				colvarWtpExplicitInserted.ColumnName = "WTP_ExplicitInserted";
				colvarWtpExplicitInserted.DataType = DbType.Boolean;
				colvarWtpExplicitInserted.MaxLength = 0;
				colvarWtpExplicitInserted.AutoIncrement = false;
				colvarWtpExplicitInserted.IsNullable = false;
				colvarWtpExplicitInserted.IsPrimaryKey = false;
				colvarWtpExplicitInserted.IsForeignKey = false;
				colvarWtpExplicitInserted.IsReadOnly = false;
				colvarWtpExplicitInserted.DefaultSetting = @"";
				colvarWtpExplicitInserted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWtpExplicitInserted);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["SqlDataProvider"].AddSchema("hitbl_WidgetTemplates_WTP",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("WtpId")]
		[Bindable(true)]
		public Guid WtpId 
		{
			get { return GetColumnValue<Guid>(Columns.WtpId); }
			set { SetColumnValue(Columns.WtpId, value); }
		}
		  
		[XmlAttribute("UserId")]
		[Bindable(true)]
		public Guid? UserID 
		{
			get { return GetColumnValue<Guid?>(Columns.UserID); }
			set { SetColumnValue(Columns.UserID, value); }
		}
		  
		[XmlAttribute("WtpName")]
		[Bindable(true)]
		public string WtpName 
		{
			get { return GetColumnValue<string>(Columns.WtpName); }
			set { SetColumnValue(Columns.WtpName, value); }
		}
		  
		[XmlAttribute("WtpTemplate")]
		[Bindable(true)]
		public string WtpTemplate 
		{
			get { return GetColumnValue<string>(Columns.WtpTemplate); }
			set { SetColumnValue(Columns.WtpTemplate, value); }
		}
		  
		[XmlAttribute("WtpXMLTemplate")]
		[Bindable(true)]
		public string WtpXMLTemplate 
		{
			get { return GetColumnValue<string>(Columns.WtpXMLTemplate); }
			set { SetColumnValue(Columns.WtpXMLTemplate, value); }
		}
		  
		[XmlAttribute("WtpExplicitInserted")]
		[Bindable(true)]
		public bool WtpExplicitInserted 
		{
			get { return GetColumnValue<bool>(Columns.WtpExplicitInserted); }
			set { SetColumnValue(Columns.WtpExplicitInserted, value); }
		}
		
		#endregion
		
		
		#region PrimaryKey Methods		
		
        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);
            
            SetPKValues();
        }
        
		
		#endregion
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a AspnetMembership ActiveRecord object related to this HitblWidgetTemplatesWtp
		/// 
		/// </summary>
		public _4screen.CSB.DataAccess.Business.AspnetMembership AspnetMembership
		{
			get { return _4screen.CSB.DataAccess.Business.AspnetMembership.FetchByID(this.UserID); }
			set { SetColumnValue("UserId", value.UserId); }
		}
		
		
		#endregion
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(Guid varWtpId,Guid? varUserID,string varWtpName,string varWtpTemplate,string varWtpXMLTemplate,bool varWtpExplicitInserted)
		{
			HitblWidgetTemplatesWtp item = new HitblWidgetTemplatesWtp();
			
			item.WtpId = varWtpId;
			
			item.UserID = varUserID;
			
			item.WtpName = varWtpName;
			
			item.WtpTemplate = varWtpTemplate;
			
			item.WtpXMLTemplate = varWtpXMLTemplate;
			
			item.WtpExplicitInserted = varWtpExplicitInserted;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(Guid varWtpId,Guid? varUserID,string varWtpName,string varWtpTemplate,string varWtpXMLTemplate,bool varWtpExplicitInserted)
		{
			HitblWidgetTemplatesWtp item = new HitblWidgetTemplatesWtp();
			
				item.WtpId = varWtpId;
			
				item.UserID = varUserID;
			
				item.WtpName = varWtpName;
			
				item.WtpTemplate = varWtpTemplate;
			
				item.WtpXMLTemplate = varWtpXMLTemplate;
			
				item.WtpExplicitInserted = varWtpExplicitInserted;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn WtpIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn UserIDColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn WtpNameColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn WtpTemplateColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn WtpXMLTemplateColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn WtpExplicitInsertedColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string WtpId = @"WTP_ID";
			 public static string UserID = @"UserId";
			 public static string WtpName = @"WTP_Name";
			 public static string WtpTemplate = @"WTP_Template";
			 public static string WtpXMLTemplate = @"WTP_XMLTemplate";
			 public static string WtpExplicitInserted = @"WTP_ExplicitInserted";
						
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
