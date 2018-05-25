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
	/// Strongly-typed collection for the HitblCommunityCty class.
	/// </summary>
    [Serializable]
	public partial class HitblCommunityCtyCollection : ActiveList<HitblCommunityCty, HitblCommunityCtyCollection>
	{	   
		public HitblCommunityCtyCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>HitblCommunityCtyCollection</returns>
		public HitblCommunityCtyCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                HitblCommunityCty o = this[i];
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
	/// This is an ActiveRecord class which wraps the hitbl_Community_CTY table.
	/// </summary>
	[Serializable]
	public partial class HitblCommunityCty : ActiveRecord<HitblCommunityCty>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public HitblCommunityCty()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public HitblCommunityCty(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public HitblCommunityCty(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public HitblCommunityCty(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("hitbl_Community_CTY", TableType.Table, DataService.GetInstance("SqlDataProvider"));
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
				
						colvarCtyId.DefaultSetting = @"(newid())";
				colvarCtyId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCtyId);
				
				TableSchema.TableColumn colvarCtyVirtualUrl = new TableSchema.TableColumn(schema);
				colvarCtyVirtualUrl.ColumnName = "CTY_VirtualUrl";
				colvarCtyVirtualUrl.DataType = DbType.String;
				colvarCtyVirtualUrl.MaxLength = 100;
				colvarCtyVirtualUrl.AutoIncrement = false;
				colvarCtyVirtualUrl.IsNullable = false;
				colvarCtyVirtualUrl.IsPrimaryKey = false;
				colvarCtyVirtualUrl.IsForeignKey = false;
				colvarCtyVirtualUrl.IsReadOnly = false;
				colvarCtyVirtualUrl.DefaultSetting = @"";
				colvarCtyVirtualUrl.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCtyVirtualUrl);
				
				TableSchema.TableColumn colvarCtyStartDate = new TableSchema.TableColumn(schema);
				colvarCtyStartDate.ColumnName = "CTY_StartDate";
				colvarCtyStartDate.DataType = DbType.DateTime;
				colvarCtyStartDate.MaxLength = 0;
				colvarCtyStartDate.AutoIncrement = false;
				colvarCtyStartDate.IsNullable = true;
				colvarCtyStartDate.IsPrimaryKey = false;
				colvarCtyStartDate.IsForeignKey = false;
				colvarCtyStartDate.IsReadOnly = false;
				colvarCtyStartDate.DefaultSetting = @"";
				colvarCtyStartDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCtyStartDate);
				
				TableSchema.TableColumn colvarCtyEndDate = new TableSchema.TableColumn(schema);
				colvarCtyEndDate.ColumnName = "CTY_EndDate";
				colvarCtyEndDate.DataType = DbType.DateTime;
				colvarCtyEndDate.MaxLength = 0;
				colvarCtyEndDate.AutoIncrement = false;
				colvarCtyEndDate.IsNullable = true;
				colvarCtyEndDate.IsPrimaryKey = false;
				colvarCtyEndDate.IsForeignKey = false;
				colvarCtyEndDate.IsReadOnly = false;
				colvarCtyEndDate.DefaultSetting = @"";
				colvarCtyEndDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCtyEndDate);
				
				TableSchema.TableColumn colvarCtyStatus = new TableSchema.TableColumn(schema);
				colvarCtyStatus.ColumnName = "CTY_Status";
				colvarCtyStatus.DataType = DbType.Int32;
				colvarCtyStatus.MaxLength = 0;
				colvarCtyStatus.AutoIncrement = false;
				colvarCtyStatus.IsNullable = true;
				colvarCtyStatus.IsPrimaryKey = false;
				colvarCtyStatus.IsForeignKey = false;
				colvarCtyStatus.IsReadOnly = false;
				colvarCtyStatus.DefaultSetting = @"";
				colvarCtyStatus.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCtyStatus);
				
				TableSchema.TableColumn colvarCtyPriority = new TableSchema.TableColumn(schema);
				colvarCtyPriority.ColumnName = "CTY_Priority";
				colvarCtyPriority.DataType = DbType.Int32;
				colvarCtyPriority.MaxLength = 0;
				colvarCtyPriority.AutoIncrement = false;
				colvarCtyPriority.IsNullable = true;
				colvarCtyPriority.IsPrimaryKey = false;
				colvarCtyPriority.IsForeignKey = false;
				colvarCtyPriority.IsReadOnly = false;
				colvarCtyPriority.DefaultSetting = @"";
				colvarCtyPriority.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCtyPriority);
				
				TableSchema.TableColumn colvarCtyLastBatchRunning = new TableSchema.TableColumn(schema);
				colvarCtyLastBatchRunning.ColumnName = "CTY_LastBatchRunning";
				colvarCtyLastBatchRunning.DataType = DbType.DateTime;
				colvarCtyLastBatchRunning.MaxLength = 0;
				colvarCtyLastBatchRunning.AutoIncrement = false;
				colvarCtyLastBatchRunning.IsNullable = true;
				colvarCtyLastBatchRunning.IsPrimaryKey = false;
				colvarCtyLastBatchRunning.IsForeignKey = false;
				colvarCtyLastBatchRunning.IsReadOnly = false;
				colvarCtyLastBatchRunning.DefaultSetting = @"";
				colvarCtyLastBatchRunning.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCtyLastBatchRunning);
				
				TableSchema.TableColumn colvarCtyInsertedDate = new TableSchema.TableColumn(schema);
				colvarCtyInsertedDate.ColumnName = "CTY_InsertedDate";
				colvarCtyInsertedDate.DataType = DbType.DateTime;
				colvarCtyInsertedDate.MaxLength = 0;
				colvarCtyInsertedDate.AutoIncrement = false;
				colvarCtyInsertedDate.IsNullable = false;
				colvarCtyInsertedDate.IsPrimaryKey = false;
				colvarCtyInsertedDate.IsForeignKey = false;
				colvarCtyInsertedDate.IsReadOnly = false;
				
						colvarCtyInsertedDate.DefaultSetting = @"(getdate())";
				colvarCtyInsertedDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCtyInsertedDate);
				
				TableSchema.TableColumn colvarCtyUpdatedDate = new TableSchema.TableColumn(schema);
				colvarCtyUpdatedDate.ColumnName = "CTY_UpdatedDate";
				colvarCtyUpdatedDate.DataType = DbType.DateTime;
				colvarCtyUpdatedDate.MaxLength = 0;
				colvarCtyUpdatedDate.AutoIncrement = false;
				colvarCtyUpdatedDate.IsNullable = true;
				colvarCtyUpdatedDate.IsPrimaryKey = false;
				colvarCtyUpdatedDate.IsForeignKey = false;
				colvarCtyUpdatedDate.IsReadOnly = false;
				colvarCtyUpdatedDate.DefaultSetting = @"";
				colvarCtyUpdatedDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCtyUpdatedDate);
				
				TableSchema.TableColumn colvarCtyLayout = new TableSchema.TableColumn(schema);
				colvarCtyLayout.ColumnName = "CTY_Layout";
				colvarCtyLayout.DataType = DbType.String;
				colvarCtyLayout.MaxLength = 50;
				colvarCtyLayout.AutoIncrement = false;
				colvarCtyLayout.IsNullable = true;
				colvarCtyLayout.IsPrimaryKey = false;
				colvarCtyLayout.IsForeignKey = false;
				colvarCtyLayout.IsReadOnly = false;
				colvarCtyLayout.DefaultSetting = @"";
				colvarCtyLayout.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCtyLayout);
				
				TableSchema.TableColumn colvarCtyTheme = new TableSchema.TableColumn(schema);
				colvarCtyTheme.ColumnName = "CTY_Theme";
				colvarCtyTheme.DataType = DbType.String;
				colvarCtyTheme.MaxLength = 50;
				colvarCtyTheme.AutoIncrement = false;
				colvarCtyTheme.IsNullable = true;
				colvarCtyTheme.IsPrimaryKey = false;
				colvarCtyTheme.IsForeignKey = false;
				colvarCtyTheme.IsReadOnly = false;
				colvarCtyTheme.DefaultSetting = @"";
				colvarCtyTheme.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCtyTheme);
				
				TableSchema.TableColumn colvarUsrIdInserted = new TableSchema.TableColumn(schema);
				colvarUsrIdInserted.ColumnName = "USR_ID_Inserted";
				colvarUsrIdInserted.DataType = DbType.Guid;
				colvarUsrIdInserted.MaxLength = 0;
				colvarUsrIdInserted.AutoIncrement = false;
				colvarUsrIdInserted.IsNullable = true;
				colvarUsrIdInserted.IsPrimaryKey = false;
				colvarUsrIdInserted.IsForeignKey = false;
				colvarUsrIdInserted.IsReadOnly = false;
				colvarUsrIdInserted.DefaultSetting = @"";
				colvarUsrIdInserted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUsrIdInserted);
				
				TableSchema.TableColumn colvarUsrIdUpdated = new TableSchema.TableColumn(schema);
				colvarUsrIdUpdated.ColumnName = "USR_ID_Updated";
				colvarUsrIdUpdated.DataType = DbType.Guid;
				colvarUsrIdUpdated.MaxLength = 0;
				colvarUsrIdUpdated.AutoIncrement = false;
				colvarUsrIdUpdated.IsNullable = true;
				colvarUsrIdUpdated.IsPrimaryKey = false;
				colvarUsrIdUpdated.IsForeignKey = false;
				colvarUsrIdUpdated.IsReadOnly = false;
				colvarUsrIdUpdated.DefaultSetting = @"";
				colvarUsrIdUpdated.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUsrIdUpdated);
				
				TableSchema.TableColumn colvarCtyBodyStyle = new TableSchema.TableColumn(schema);
				colvarCtyBodyStyle.ColumnName = "CTY_BodyStyle";
				colvarCtyBodyStyle.DataType = DbType.String;
				colvarCtyBodyStyle.MaxLength = 512;
				colvarCtyBodyStyle.AutoIncrement = false;
				colvarCtyBodyStyle.IsNullable = true;
				colvarCtyBodyStyle.IsPrimaryKey = false;
				colvarCtyBodyStyle.IsForeignKey = false;
				colvarCtyBodyStyle.IsReadOnly = false;
				colvarCtyBodyStyle.DefaultSetting = @"";
				colvarCtyBodyStyle.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCtyBodyStyle);
				
				TableSchema.TableColumn colvarCtyHeaderStyle = new TableSchema.TableColumn(schema);
				colvarCtyHeaderStyle.ColumnName = "CTY_HeaderStyle";
				colvarCtyHeaderStyle.DataType = DbType.String;
				colvarCtyHeaderStyle.MaxLength = 512;
				colvarCtyHeaderStyle.AutoIncrement = false;
				colvarCtyHeaderStyle.IsNullable = true;
				colvarCtyHeaderStyle.IsPrimaryKey = false;
				colvarCtyHeaderStyle.IsForeignKey = false;
				colvarCtyHeaderStyle.IsReadOnly = false;
				colvarCtyHeaderStyle.DefaultSetting = @"";
				colvarCtyHeaderStyle.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCtyHeaderStyle);
				
				TableSchema.TableColumn colvarCtyFooterStyle = new TableSchema.TableColumn(schema);
				colvarCtyFooterStyle.ColumnName = "CTY_FooterStyle";
				colvarCtyFooterStyle.DataType = DbType.String;
				colvarCtyFooterStyle.MaxLength = 512;
				colvarCtyFooterStyle.AutoIncrement = false;
				colvarCtyFooterStyle.IsNullable = true;
				colvarCtyFooterStyle.IsPrimaryKey = false;
				colvarCtyFooterStyle.IsForeignKey = false;
				colvarCtyFooterStyle.IsReadOnly = false;
				colvarCtyFooterStyle.DefaultSetting = @"";
				colvarCtyFooterStyle.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCtyFooterStyle);
				
				TableSchema.TableColumn colvarCtyIsProfile = new TableSchema.TableColumn(schema);
				colvarCtyIsProfile.ColumnName = "CTY_IsProfile";
				colvarCtyIsProfile.DataType = DbType.Boolean;
				colvarCtyIsProfile.MaxLength = 0;
				colvarCtyIsProfile.AutoIncrement = false;
				colvarCtyIsProfile.IsNullable = false;
				colvarCtyIsProfile.IsPrimaryKey = false;
				colvarCtyIsProfile.IsForeignKey = false;
				colvarCtyIsProfile.IsReadOnly = false;
				
						colvarCtyIsProfile.DefaultSetting = @"((0))";
				colvarCtyIsProfile.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCtyIsProfile);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["SqlDataProvider"].AddSchema("hitbl_Community_CTY",schema);
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
		  
		[XmlAttribute("CtyVirtualUrl")]
		[Bindable(true)]
		public string CtyVirtualUrl 
		{
			get { return GetColumnValue<string>(Columns.CtyVirtualUrl); }
			set { SetColumnValue(Columns.CtyVirtualUrl, value); }
		}
		  
		[XmlAttribute("CtyStartDate")]
		[Bindable(true)]
		public DateTime? CtyStartDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.CtyStartDate); }
			set { SetColumnValue(Columns.CtyStartDate, value); }
		}
		  
		[XmlAttribute("CtyEndDate")]
		[Bindable(true)]
		public DateTime? CtyEndDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.CtyEndDate); }
			set { SetColumnValue(Columns.CtyEndDate, value); }
		}
		  
		[XmlAttribute("CtyStatus")]
		[Bindable(true)]
		public int? CtyStatus 
		{
			get { return GetColumnValue<int?>(Columns.CtyStatus); }
			set { SetColumnValue(Columns.CtyStatus, value); }
		}
		  
		[XmlAttribute("CtyPriority")]
		[Bindable(true)]
		public int? CtyPriority 
		{
			get { return GetColumnValue<int?>(Columns.CtyPriority); }
			set { SetColumnValue(Columns.CtyPriority, value); }
		}
		  
		[XmlAttribute("CtyLastBatchRunning")]
		[Bindable(true)]
		public DateTime? CtyLastBatchRunning 
		{
			get { return GetColumnValue<DateTime?>(Columns.CtyLastBatchRunning); }
			set { SetColumnValue(Columns.CtyLastBatchRunning, value); }
		}
		  
		[XmlAttribute("CtyInsertedDate")]
		[Bindable(true)]
		public DateTime CtyInsertedDate 
		{
			get { return GetColumnValue<DateTime>(Columns.CtyInsertedDate); }
			set { SetColumnValue(Columns.CtyInsertedDate, value); }
		}
		  
		[XmlAttribute("CtyUpdatedDate")]
		[Bindable(true)]
		public DateTime? CtyUpdatedDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.CtyUpdatedDate); }
			set { SetColumnValue(Columns.CtyUpdatedDate, value); }
		}
		  
		[XmlAttribute("CtyLayout")]
		[Bindable(true)]
		public string CtyLayout 
		{
			get { return GetColumnValue<string>(Columns.CtyLayout); }
			set { SetColumnValue(Columns.CtyLayout, value); }
		}
		  
		[XmlAttribute("CtyTheme")]
		[Bindable(true)]
		public string CtyTheme 
		{
			get { return GetColumnValue<string>(Columns.CtyTheme); }
			set { SetColumnValue(Columns.CtyTheme, value); }
		}
		  
		[XmlAttribute("UsrIdInserted")]
		[Bindable(true)]
		public Guid? UsrIdInserted 
		{
			get { return GetColumnValue<Guid?>(Columns.UsrIdInserted); }
			set { SetColumnValue(Columns.UsrIdInserted, value); }
		}
		  
		[XmlAttribute("UsrIdUpdated")]
		[Bindable(true)]
		public Guid? UsrIdUpdated 
		{
			get { return GetColumnValue<Guid?>(Columns.UsrIdUpdated); }
			set { SetColumnValue(Columns.UsrIdUpdated, value); }
		}
		  
		[XmlAttribute("CtyBodyStyle")]
		[Bindable(true)]
		public string CtyBodyStyle 
		{
			get { return GetColumnValue<string>(Columns.CtyBodyStyle); }
			set { SetColumnValue(Columns.CtyBodyStyle, value); }
		}
		  
		[XmlAttribute("CtyHeaderStyle")]
		[Bindable(true)]
		public string CtyHeaderStyle 
		{
			get { return GetColumnValue<string>(Columns.CtyHeaderStyle); }
			set { SetColumnValue(Columns.CtyHeaderStyle, value); }
		}
		  
		[XmlAttribute("CtyFooterStyle")]
		[Bindable(true)]
		public string CtyFooterStyle 
		{
			get { return GetColumnValue<string>(Columns.CtyFooterStyle); }
			set { SetColumnValue(Columns.CtyFooterStyle, value); }
		}
		  
		[XmlAttribute("CtyIsProfile")]
		[Bindable(true)]
		public bool CtyIsProfile 
		{
			get { return GetColumnValue<bool>(Columns.CtyIsProfile); }
			set { SetColumnValue(Columns.CtyIsProfile, value); }
		}
		
		#endregion
		
		
		#region PrimaryKey Methods		
		
        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);
            
            SetPKValues();
        }
        
		
		public _4screen.CSB.DataAccess.Business.HirelCommunityUserCurCollection HirelCommunityUserCurRecords()
		{
			return new _4screen.CSB.DataAccess.Business.HirelCommunityUserCurCollection().Where(HirelCommunityUserCur.Columns.CtyId, CtyId).Load();
		}
		public _4screen.CSB.DataAccess.Business.HitblPagePagCollection HitblPagePagRecords()
		{
			return new _4screen.CSB.DataAccess.Business.HitblPagePagCollection().Where(HitblPagePag.Columns.CtyId, CtyId).Load();
		}
		#endregion
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(Guid varCtyId,string varCtyVirtualUrl,DateTime? varCtyStartDate,DateTime? varCtyEndDate,int? varCtyStatus,int? varCtyPriority,DateTime? varCtyLastBatchRunning,DateTime varCtyInsertedDate,DateTime? varCtyUpdatedDate,string varCtyLayout,string varCtyTheme,Guid? varUsrIdInserted,Guid? varUsrIdUpdated,string varCtyBodyStyle,string varCtyHeaderStyle,string varCtyFooterStyle,bool varCtyIsProfile)
		{
			HitblCommunityCty item = new HitblCommunityCty();
			
			item.CtyId = varCtyId;
			
			item.CtyVirtualUrl = varCtyVirtualUrl;
			
			item.CtyStartDate = varCtyStartDate;
			
			item.CtyEndDate = varCtyEndDate;
			
			item.CtyStatus = varCtyStatus;
			
			item.CtyPriority = varCtyPriority;
			
			item.CtyLastBatchRunning = varCtyLastBatchRunning;
			
			item.CtyInsertedDate = varCtyInsertedDate;
			
			item.CtyUpdatedDate = varCtyUpdatedDate;
			
			item.CtyLayout = varCtyLayout;
			
			item.CtyTheme = varCtyTheme;
			
			item.UsrIdInserted = varUsrIdInserted;
			
			item.UsrIdUpdated = varUsrIdUpdated;
			
			item.CtyBodyStyle = varCtyBodyStyle;
			
			item.CtyHeaderStyle = varCtyHeaderStyle;
			
			item.CtyFooterStyle = varCtyFooterStyle;
			
			item.CtyIsProfile = varCtyIsProfile;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(Guid varCtyId,string varCtyVirtualUrl,DateTime? varCtyStartDate,DateTime? varCtyEndDate,int? varCtyStatus,int? varCtyPriority,DateTime? varCtyLastBatchRunning,DateTime varCtyInsertedDate,DateTime? varCtyUpdatedDate,string varCtyLayout,string varCtyTheme,Guid? varUsrIdInserted,Guid? varUsrIdUpdated,string varCtyBodyStyle,string varCtyHeaderStyle,string varCtyFooterStyle,bool varCtyIsProfile)
		{
			HitblCommunityCty item = new HitblCommunityCty();
			
				item.CtyId = varCtyId;
			
				item.CtyVirtualUrl = varCtyVirtualUrl;
			
				item.CtyStartDate = varCtyStartDate;
			
				item.CtyEndDate = varCtyEndDate;
			
				item.CtyStatus = varCtyStatus;
			
				item.CtyPriority = varCtyPriority;
			
				item.CtyLastBatchRunning = varCtyLastBatchRunning;
			
				item.CtyInsertedDate = varCtyInsertedDate;
			
				item.CtyUpdatedDate = varCtyUpdatedDate;
			
				item.CtyLayout = varCtyLayout;
			
				item.CtyTheme = varCtyTheme;
			
				item.UsrIdInserted = varUsrIdInserted;
			
				item.UsrIdUpdated = varUsrIdUpdated;
			
				item.CtyBodyStyle = varCtyBodyStyle;
			
				item.CtyHeaderStyle = varCtyHeaderStyle;
			
				item.CtyFooterStyle = varCtyFooterStyle;
			
				item.CtyIsProfile = varCtyIsProfile;
			
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
        
        
        
        public static TableSchema.TableColumn CtyVirtualUrlColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn CtyStartDateColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn CtyEndDateColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn CtyStatusColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn CtyPriorityColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn CtyLastBatchRunningColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn CtyInsertedDateColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn CtyUpdatedDateColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn CtyLayoutColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn CtyThemeColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn UsrIdInsertedColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn UsrIdUpdatedColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn CtyBodyStyleColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn CtyHeaderStyleColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn CtyFooterStyleColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn CtyIsProfileColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string CtyId = @"CTY_ID";
			 public static string CtyVirtualUrl = @"CTY_VirtualUrl";
			 public static string CtyStartDate = @"CTY_StartDate";
			 public static string CtyEndDate = @"CTY_EndDate";
			 public static string CtyStatus = @"CTY_Status";
			 public static string CtyPriority = @"CTY_Priority";
			 public static string CtyLastBatchRunning = @"CTY_LastBatchRunning";
			 public static string CtyInsertedDate = @"CTY_InsertedDate";
			 public static string CtyUpdatedDate = @"CTY_UpdatedDate";
			 public static string CtyLayout = @"CTY_Layout";
			 public static string CtyTheme = @"CTY_Theme";
			 public static string UsrIdInserted = @"USR_ID_Inserted";
			 public static string UsrIdUpdated = @"USR_ID_Updated";
			 public static string CtyBodyStyle = @"CTY_BodyStyle";
			 public static string CtyHeaderStyle = @"CTY_HeaderStyle";
			 public static string CtyFooterStyle = @"CTY_FooterStyle";
			 public static string CtyIsProfile = @"CTY_IsProfile";
						
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
