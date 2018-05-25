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
	/// Strongly-typed collection for the HirelCommunityUserCur class.
	/// </summary>
    [Serializable]
	public partial class HirelCommunityUserCurCollection : ActiveList<HirelCommunityUserCur, HirelCommunityUserCurCollection>
	{	   
		public HirelCommunityUserCurCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>HirelCommunityUserCurCollection</returns>
		public HirelCommunityUserCurCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                HirelCommunityUserCur o = this[i];
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
	/// This is an ActiveRecord class which wraps the hirel_Community_User_CUR table.
	/// </summary>
	[Serializable]
	public partial class HirelCommunityUserCur : ActiveRecord<HirelCommunityUserCur>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public HirelCommunityUserCur()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public HirelCommunityUserCur(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public HirelCommunityUserCur(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public HirelCommunityUserCur(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("hirel_Community_User_CUR", TableType.Table, DataService.GetInstance("SqlDataProvider"));
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
				colvarCtyId.IsForeignKey = true;
				colvarCtyId.IsReadOnly = false;
				colvarCtyId.DefaultSetting = @"";
				
					colvarCtyId.ForeignKeyTableName = "hitbl_Community_CTY";
				schema.Columns.Add(colvarCtyId);
				
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
				
				TableSchema.TableColumn colvarCurIsOwner = new TableSchema.TableColumn(schema);
				colvarCurIsOwner.ColumnName = "CUR_IsOwner";
				colvarCurIsOwner.DataType = DbType.Boolean;
				colvarCurIsOwner.MaxLength = 0;
				colvarCurIsOwner.AutoIncrement = false;
				colvarCurIsOwner.IsNullable = false;
				colvarCurIsOwner.IsPrimaryKey = false;
				colvarCurIsOwner.IsForeignKey = false;
				colvarCurIsOwner.IsReadOnly = false;
				colvarCurIsOwner.DefaultSetting = @"";
				colvarCurIsOwner.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCurIsOwner);
				
				TableSchema.TableColumn colvarCurStatus = new TableSchema.TableColumn(schema);
				colvarCurStatus.ColumnName = "CUR_Status";
				colvarCurStatus.DataType = DbType.Int32;
				colvarCurStatus.MaxLength = 0;
				colvarCurStatus.AutoIncrement = false;
				colvarCurStatus.IsNullable = true;
				colvarCurStatus.IsPrimaryKey = false;
				colvarCurStatus.IsForeignKey = false;
				colvarCurStatus.IsReadOnly = false;
				colvarCurStatus.DefaultSetting = @"";
				colvarCurStatus.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCurStatus);
				
				TableSchema.TableColumn colvarCurInsertedDate = new TableSchema.TableColumn(schema);
				colvarCurInsertedDate.ColumnName = "CUR_InsertedDate";
				colvarCurInsertedDate.DataType = DbType.DateTime;
				colvarCurInsertedDate.MaxLength = 0;
				colvarCurInsertedDate.AutoIncrement = false;
				colvarCurInsertedDate.IsNullable = true;
				colvarCurInsertedDate.IsPrimaryKey = false;
				colvarCurInsertedDate.IsForeignKey = false;
				colvarCurInsertedDate.IsReadOnly = false;
				colvarCurInsertedDate.DefaultSetting = @"";
				colvarCurInsertedDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCurInsertedDate);
				
				TableSchema.TableColumn colvarUsrIdInvitedBy = new TableSchema.TableColumn(schema);
				colvarUsrIdInvitedBy.ColumnName = "USR_ID_InvitedBy";
				colvarUsrIdInvitedBy.DataType = DbType.Guid;
				colvarUsrIdInvitedBy.MaxLength = 0;
				colvarUsrIdInvitedBy.AutoIncrement = false;
				colvarUsrIdInvitedBy.IsNullable = true;
				colvarUsrIdInvitedBy.IsPrimaryKey = false;
				colvarUsrIdInvitedBy.IsForeignKey = false;
				colvarUsrIdInvitedBy.IsReadOnly = false;
				colvarUsrIdInvitedBy.DefaultSetting = @"";
				colvarUsrIdInvitedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUsrIdInvitedBy);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["SqlDataProvider"].AddSchema("hirel_Community_User_CUR",schema);
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
		  
		[XmlAttribute("UsrId")]
		[Bindable(true)]
		public Guid UsrId 
		{
			get { return GetColumnValue<Guid>(Columns.UsrId); }
			set { SetColumnValue(Columns.UsrId, value); }
		}
		  
		[XmlAttribute("CurIsOwner")]
		[Bindable(true)]
		public bool CurIsOwner 
		{
			get { return GetColumnValue<bool>(Columns.CurIsOwner); }
			set { SetColumnValue(Columns.CurIsOwner, value); }
		}
		  
		[XmlAttribute("CurStatus")]
		[Bindable(true)]
		public int? CurStatus 
		{
			get { return GetColumnValue<int?>(Columns.CurStatus); }
			set { SetColumnValue(Columns.CurStatus, value); }
		}
		  
		[XmlAttribute("CurInsertedDate")]
		[Bindable(true)]
		public DateTime? CurInsertedDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.CurInsertedDate); }
			set { SetColumnValue(Columns.CurInsertedDate, value); }
		}
		  
		[XmlAttribute("UsrIdInvitedBy")]
		[Bindable(true)]
		public Guid? UsrIdInvitedBy 
		{
			get { return GetColumnValue<Guid?>(Columns.UsrIdInvitedBy); }
			set { SetColumnValue(Columns.UsrIdInvitedBy, value); }
		}
		
		#endregion
		
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a HitblCommunityCty ActiveRecord object related to this HirelCommunityUserCur
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
		public static void Insert(Guid varCtyId,Guid varUsrId,bool varCurIsOwner,int? varCurStatus,DateTime? varCurInsertedDate,Guid? varUsrIdInvitedBy)
		{
			HirelCommunityUserCur item = new HirelCommunityUserCur();
			
			item.CtyId = varCtyId;
			
			item.UsrId = varUsrId;
			
			item.CurIsOwner = varCurIsOwner;
			
			item.CurStatus = varCurStatus;
			
			item.CurInsertedDate = varCurInsertedDate;
			
			item.UsrIdInvitedBy = varUsrIdInvitedBy;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(Guid varCtyId,Guid varUsrId,bool varCurIsOwner,int? varCurStatus,DateTime? varCurInsertedDate,Guid? varUsrIdInvitedBy)
		{
			HirelCommunityUserCur item = new HirelCommunityUserCur();
			
				item.CtyId = varCtyId;
			
				item.UsrId = varUsrId;
			
				item.CurIsOwner = varCurIsOwner;
			
				item.CurStatus = varCurStatus;
			
				item.CurInsertedDate = varCurInsertedDate;
			
				item.UsrIdInvitedBy = varUsrIdInvitedBy;
			
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
        
        
        
        public static TableSchema.TableColumn UsrIdColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn CurIsOwnerColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn CurStatusColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn CurInsertedDateColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn UsrIdInvitedByColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string CtyId = @"CTY_ID";
			 public static string UsrId = @"USR_ID";
			 public static string CurIsOwner = @"CUR_IsOwner";
			 public static string CurStatus = @"CUR_Status";
			 public static string CurInsertedDate = @"CUR_InsertedDate";
			 public static string UsrIdInvitedBy = @"USR_ID_InvitedBy";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
