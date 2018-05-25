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
	/// Strongly-typed collection for the AspnetMembership class.
	/// </summary>
    [Serializable]
	public partial class AspnetMembershipCollection : ActiveList<AspnetMembership, AspnetMembershipCollection>
	{	   
		public AspnetMembershipCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>AspnetMembershipCollection</returns>
		public AspnetMembershipCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                AspnetMembership o = this[i];
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
	/// This is an ActiveRecord class which wraps the aspnet_Membership table.
	/// </summary>
	[Serializable]
	public partial class AspnetMembership : ActiveRecord<AspnetMembership>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public AspnetMembership()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public AspnetMembership(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public AspnetMembership(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public AspnetMembership(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("aspnet_Membership", TableType.Table, DataService.GetInstance("SqlDataProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarApplicationId = new TableSchema.TableColumn(schema);
				colvarApplicationId.ColumnName = "ApplicationId";
				colvarApplicationId.DataType = DbType.Guid;
				colvarApplicationId.MaxLength = 0;
				colvarApplicationId.AutoIncrement = false;
				colvarApplicationId.IsNullable = false;
				colvarApplicationId.IsPrimaryKey = false;
				colvarApplicationId.IsForeignKey = true;
				colvarApplicationId.IsReadOnly = false;
				colvarApplicationId.DefaultSetting = @"";
				
					colvarApplicationId.ForeignKeyTableName = "aspnet_Applications";
				schema.Columns.Add(colvarApplicationId);
				
				TableSchema.TableColumn colvarUserId = new TableSchema.TableColumn(schema);
				colvarUserId.ColumnName = "UserId";
				colvarUserId.DataType = DbType.Guid;
				colvarUserId.MaxLength = 0;
				colvarUserId.AutoIncrement = false;
				colvarUserId.IsNullable = false;
				colvarUserId.IsPrimaryKey = true;
				colvarUserId.IsForeignKey = true;
				colvarUserId.IsReadOnly = false;
				colvarUserId.DefaultSetting = @"";
				
					colvarUserId.ForeignKeyTableName = "aspnet_Users";
				schema.Columns.Add(colvarUserId);
				
				TableSchema.TableColumn colvarPassword = new TableSchema.TableColumn(schema);
				colvarPassword.ColumnName = "Password";
				colvarPassword.DataType = DbType.String;
				colvarPassword.MaxLength = 128;
				colvarPassword.AutoIncrement = false;
				colvarPassword.IsNullable = false;
				colvarPassword.IsPrimaryKey = false;
				colvarPassword.IsForeignKey = false;
				colvarPassword.IsReadOnly = false;
				colvarPassword.DefaultSetting = @"";
				colvarPassword.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPassword);
				
				TableSchema.TableColumn colvarPasswordFormat = new TableSchema.TableColumn(schema);
				colvarPasswordFormat.ColumnName = "PasswordFormat";
				colvarPasswordFormat.DataType = DbType.Int32;
				colvarPasswordFormat.MaxLength = 0;
				colvarPasswordFormat.AutoIncrement = false;
				colvarPasswordFormat.IsNullable = false;
				colvarPasswordFormat.IsPrimaryKey = false;
				colvarPasswordFormat.IsForeignKey = false;
				colvarPasswordFormat.IsReadOnly = false;
				
						colvarPasswordFormat.DefaultSetting = @"((0))";
				colvarPasswordFormat.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPasswordFormat);
				
				TableSchema.TableColumn colvarPasswordSalt = new TableSchema.TableColumn(schema);
				colvarPasswordSalt.ColumnName = "PasswordSalt";
				colvarPasswordSalt.DataType = DbType.String;
				colvarPasswordSalt.MaxLength = 128;
				colvarPasswordSalt.AutoIncrement = false;
				colvarPasswordSalt.IsNullable = false;
				colvarPasswordSalt.IsPrimaryKey = false;
				colvarPasswordSalt.IsForeignKey = false;
				colvarPasswordSalt.IsReadOnly = false;
				colvarPasswordSalt.DefaultSetting = @"";
				colvarPasswordSalt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPasswordSalt);
				
				TableSchema.TableColumn colvarMobilePIN = new TableSchema.TableColumn(schema);
				colvarMobilePIN.ColumnName = "MobilePIN";
				colvarMobilePIN.DataType = DbType.String;
				colvarMobilePIN.MaxLength = 16;
				colvarMobilePIN.AutoIncrement = false;
				colvarMobilePIN.IsNullable = true;
				colvarMobilePIN.IsPrimaryKey = false;
				colvarMobilePIN.IsForeignKey = false;
				colvarMobilePIN.IsReadOnly = false;
				colvarMobilePIN.DefaultSetting = @"";
				colvarMobilePIN.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMobilePIN);
				
				TableSchema.TableColumn colvarEmail = new TableSchema.TableColumn(schema);
				colvarEmail.ColumnName = "Email";
				colvarEmail.DataType = DbType.String;
				colvarEmail.MaxLength = 256;
				colvarEmail.AutoIncrement = false;
				colvarEmail.IsNullable = true;
				colvarEmail.IsPrimaryKey = false;
				colvarEmail.IsForeignKey = false;
				colvarEmail.IsReadOnly = false;
				colvarEmail.DefaultSetting = @"";
				colvarEmail.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEmail);
				
				TableSchema.TableColumn colvarLoweredEmail = new TableSchema.TableColumn(schema);
				colvarLoweredEmail.ColumnName = "LoweredEmail";
				colvarLoweredEmail.DataType = DbType.String;
				colvarLoweredEmail.MaxLength = 256;
				colvarLoweredEmail.AutoIncrement = false;
				colvarLoweredEmail.IsNullable = true;
				colvarLoweredEmail.IsPrimaryKey = false;
				colvarLoweredEmail.IsForeignKey = false;
				colvarLoweredEmail.IsReadOnly = false;
				colvarLoweredEmail.DefaultSetting = @"";
				colvarLoweredEmail.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLoweredEmail);
				
				TableSchema.TableColumn colvarPasswordQuestion = new TableSchema.TableColumn(schema);
				colvarPasswordQuestion.ColumnName = "PasswordQuestion";
				colvarPasswordQuestion.DataType = DbType.String;
				colvarPasswordQuestion.MaxLength = 256;
				colvarPasswordQuestion.AutoIncrement = false;
				colvarPasswordQuestion.IsNullable = true;
				colvarPasswordQuestion.IsPrimaryKey = false;
				colvarPasswordQuestion.IsForeignKey = false;
				colvarPasswordQuestion.IsReadOnly = false;
				colvarPasswordQuestion.DefaultSetting = @"";
				colvarPasswordQuestion.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPasswordQuestion);
				
				TableSchema.TableColumn colvarPasswordAnswer = new TableSchema.TableColumn(schema);
				colvarPasswordAnswer.ColumnName = "PasswordAnswer";
				colvarPasswordAnswer.DataType = DbType.String;
				colvarPasswordAnswer.MaxLength = 128;
				colvarPasswordAnswer.AutoIncrement = false;
				colvarPasswordAnswer.IsNullable = true;
				colvarPasswordAnswer.IsPrimaryKey = false;
				colvarPasswordAnswer.IsForeignKey = false;
				colvarPasswordAnswer.IsReadOnly = false;
				colvarPasswordAnswer.DefaultSetting = @"";
				colvarPasswordAnswer.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPasswordAnswer);
				
				TableSchema.TableColumn colvarIsApproved = new TableSchema.TableColumn(schema);
				colvarIsApproved.ColumnName = "IsApproved";
				colvarIsApproved.DataType = DbType.Boolean;
				colvarIsApproved.MaxLength = 0;
				colvarIsApproved.AutoIncrement = false;
				colvarIsApproved.IsNullable = false;
				colvarIsApproved.IsPrimaryKey = false;
				colvarIsApproved.IsForeignKey = false;
				colvarIsApproved.IsReadOnly = false;
				colvarIsApproved.DefaultSetting = @"";
				colvarIsApproved.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsApproved);
				
				TableSchema.TableColumn colvarIsLockedOut = new TableSchema.TableColumn(schema);
				colvarIsLockedOut.ColumnName = "IsLockedOut";
				colvarIsLockedOut.DataType = DbType.Boolean;
				colvarIsLockedOut.MaxLength = 0;
				colvarIsLockedOut.AutoIncrement = false;
				colvarIsLockedOut.IsNullable = false;
				colvarIsLockedOut.IsPrimaryKey = false;
				colvarIsLockedOut.IsForeignKey = false;
				colvarIsLockedOut.IsReadOnly = false;
				colvarIsLockedOut.DefaultSetting = @"";
				colvarIsLockedOut.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsLockedOut);
				
				TableSchema.TableColumn colvarCreateDate = new TableSchema.TableColumn(schema);
				colvarCreateDate.ColumnName = "CreateDate";
				colvarCreateDate.DataType = DbType.DateTime;
				colvarCreateDate.MaxLength = 0;
				colvarCreateDate.AutoIncrement = false;
				colvarCreateDate.IsNullable = false;
				colvarCreateDate.IsPrimaryKey = false;
				colvarCreateDate.IsForeignKey = false;
				colvarCreateDate.IsReadOnly = false;
				colvarCreateDate.DefaultSetting = @"";
				colvarCreateDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreateDate);
				
				TableSchema.TableColumn colvarLastLoginDate = new TableSchema.TableColumn(schema);
				colvarLastLoginDate.ColumnName = "LastLoginDate";
				colvarLastLoginDate.DataType = DbType.DateTime;
				colvarLastLoginDate.MaxLength = 0;
				colvarLastLoginDate.AutoIncrement = false;
				colvarLastLoginDate.IsNullable = false;
				colvarLastLoginDate.IsPrimaryKey = false;
				colvarLastLoginDate.IsForeignKey = false;
				colvarLastLoginDate.IsReadOnly = false;
				colvarLastLoginDate.DefaultSetting = @"";
				colvarLastLoginDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastLoginDate);
				
				TableSchema.TableColumn colvarLastPasswordChangedDate = new TableSchema.TableColumn(schema);
				colvarLastPasswordChangedDate.ColumnName = "LastPasswordChangedDate";
				colvarLastPasswordChangedDate.DataType = DbType.DateTime;
				colvarLastPasswordChangedDate.MaxLength = 0;
				colvarLastPasswordChangedDate.AutoIncrement = false;
				colvarLastPasswordChangedDate.IsNullable = false;
				colvarLastPasswordChangedDate.IsPrimaryKey = false;
				colvarLastPasswordChangedDate.IsForeignKey = false;
				colvarLastPasswordChangedDate.IsReadOnly = false;
				colvarLastPasswordChangedDate.DefaultSetting = @"";
				colvarLastPasswordChangedDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastPasswordChangedDate);
				
				TableSchema.TableColumn colvarLastLockoutDate = new TableSchema.TableColumn(schema);
				colvarLastLockoutDate.ColumnName = "LastLockoutDate";
				colvarLastLockoutDate.DataType = DbType.DateTime;
				colvarLastLockoutDate.MaxLength = 0;
				colvarLastLockoutDate.AutoIncrement = false;
				colvarLastLockoutDate.IsNullable = false;
				colvarLastLockoutDate.IsPrimaryKey = false;
				colvarLastLockoutDate.IsForeignKey = false;
				colvarLastLockoutDate.IsReadOnly = false;
				colvarLastLockoutDate.DefaultSetting = @"";
				colvarLastLockoutDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastLockoutDate);
				
				TableSchema.TableColumn colvarFailedPasswordAttemptCount = new TableSchema.TableColumn(schema);
				colvarFailedPasswordAttemptCount.ColumnName = "FailedPasswordAttemptCount";
				colvarFailedPasswordAttemptCount.DataType = DbType.Int32;
				colvarFailedPasswordAttemptCount.MaxLength = 0;
				colvarFailedPasswordAttemptCount.AutoIncrement = false;
				colvarFailedPasswordAttemptCount.IsNullable = false;
				colvarFailedPasswordAttemptCount.IsPrimaryKey = false;
				colvarFailedPasswordAttemptCount.IsForeignKey = false;
				colvarFailedPasswordAttemptCount.IsReadOnly = false;
				colvarFailedPasswordAttemptCount.DefaultSetting = @"";
				colvarFailedPasswordAttemptCount.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFailedPasswordAttemptCount);
				
				TableSchema.TableColumn colvarFailedPasswordAttemptWindowStart = new TableSchema.TableColumn(schema);
				colvarFailedPasswordAttemptWindowStart.ColumnName = "FailedPasswordAttemptWindowStart";
				colvarFailedPasswordAttemptWindowStart.DataType = DbType.DateTime;
				colvarFailedPasswordAttemptWindowStart.MaxLength = 0;
				colvarFailedPasswordAttemptWindowStart.AutoIncrement = false;
				colvarFailedPasswordAttemptWindowStart.IsNullable = false;
				colvarFailedPasswordAttemptWindowStart.IsPrimaryKey = false;
				colvarFailedPasswordAttemptWindowStart.IsForeignKey = false;
				colvarFailedPasswordAttemptWindowStart.IsReadOnly = false;
				colvarFailedPasswordAttemptWindowStart.DefaultSetting = @"";
				colvarFailedPasswordAttemptWindowStart.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFailedPasswordAttemptWindowStart);
				
				TableSchema.TableColumn colvarFailedPasswordAnswerAttemptCount = new TableSchema.TableColumn(schema);
				colvarFailedPasswordAnswerAttemptCount.ColumnName = "FailedPasswordAnswerAttemptCount";
				colvarFailedPasswordAnswerAttemptCount.DataType = DbType.Int32;
				colvarFailedPasswordAnswerAttemptCount.MaxLength = 0;
				colvarFailedPasswordAnswerAttemptCount.AutoIncrement = false;
				colvarFailedPasswordAnswerAttemptCount.IsNullable = false;
				colvarFailedPasswordAnswerAttemptCount.IsPrimaryKey = false;
				colvarFailedPasswordAnswerAttemptCount.IsForeignKey = false;
				colvarFailedPasswordAnswerAttemptCount.IsReadOnly = false;
				colvarFailedPasswordAnswerAttemptCount.DefaultSetting = @"";
				colvarFailedPasswordAnswerAttemptCount.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFailedPasswordAnswerAttemptCount);
				
				TableSchema.TableColumn colvarFailedPasswordAnswerAttemptWindowStart = new TableSchema.TableColumn(schema);
				colvarFailedPasswordAnswerAttemptWindowStart.ColumnName = "FailedPasswordAnswerAttemptWindowStart";
				colvarFailedPasswordAnswerAttemptWindowStart.DataType = DbType.DateTime;
				colvarFailedPasswordAnswerAttemptWindowStart.MaxLength = 0;
				colvarFailedPasswordAnswerAttemptWindowStart.AutoIncrement = false;
				colvarFailedPasswordAnswerAttemptWindowStart.IsNullable = false;
				colvarFailedPasswordAnswerAttemptWindowStart.IsPrimaryKey = false;
				colvarFailedPasswordAnswerAttemptWindowStart.IsForeignKey = false;
				colvarFailedPasswordAnswerAttemptWindowStart.IsReadOnly = false;
				colvarFailedPasswordAnswerAttemptWindowStart.DefaultSetting = @"";
				colvarFailedPasswordAnswerAttemptWindowStart.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFailedPasswordAnswerAttemptWindowStart);
				
				TableSchema.TableColumn colvarComment = new TableSchema.TableColumn(schema);
				colvarComment.ColumnName = "Comment";
				colvarComment.DataType = DbType.String;
				colvarComment.MaxLength = 1073741823;
				colvarComment.AutoIncrement = false;
				colvarComment.IsNullable = true;
				colvarComment.IsPrimaryKey = false;
				colvarComment.IsForeignKey = false;
				colvarComment.IsReadOnly = false;
				colvarComment.DefaultSetting = @"";
				colvarComment.ForeignKeyTableName = "";
				schema.Columns.Add(colvarComment);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["SqlDataProvider"].AddSchema("aspnet_Membership",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("ApplicationId")]
		[Bindable(true)]
		public Guid ApplicationId 
		{
			get { return GetColumnValue<Guid>(Columns.ApplicationId); }
			set { SetColumnValue(Columns.ApplicationId, value); }
		}
		  
		[XmlAttribute("UserId")]
		[Bindable(true)]
		public Guid UserId 
		{
			get { return GetColumnValue<Guid>(Columns.UserId); }
			set { SetColumnValue(Columns.UserId, value); }
		}
		  
		[XmlAttribute("Password")]
		[Bindable(true)]
		public string Password 
		{
			get { return GetColumnValue<string>(Columns.Password); }
			set { SetColumnValue(Columns.Password, value); }
		}
		  
		[XmlAttribute("PasswordFormat")]
		[Bindable(true)]
		public int PasswordFormat 
		{
			get { return GetColumnValue<int>(Columns.PasswordFormat); }
			set { SetColumnValue(Columns.PasswordFormat, value); }
		}
		  
		[XmlAttribute("PasswordSalt")]
		[Bindable(true)]
		public string PasswordSalt 
		{
			get { return GetColumnValue<string>(Columns.PasswordSalt); }
			set { SetColumnValue(Columns.PasswordSalt, value); }
		}
		  
		[XmlAttribute("MobilePIN")]
		[Bindable(true)]
		public string MobilePIN 
		{
			get { return GetColumnValue<string>(Columns.MobilePIN); }
			set { SetColumnValue(Columns.MobilePIN, value); }
		}
		  
		[XmlAttribute("Email")]
		[Bindable(true)]
		public string Email 
		{
			get { return GetColumnValue<string>(Columns.Email); }
			set { SetColumnValue(Columns.Email, value); }
		}
		  
		[XmlAttribute("LoweredEmail")]
		[Bindable(true)]
		public string LoweredEmail 
		{
			get { return GetColumnValue<string>(Columns.LoweredEmail); }
			set { SetColumnValue(Columns.LoweredEmail, value); }
		}
		  
		[XmlAttribute("PasswordQuestion")]
		[Bindable(true)]
		public string PasswordQuestion 
		{
			get { return GetColumnValue<string>(Columns.PasswordQuestion); }
			set { SetColumnValue(Columns.PasswordQuestion, value); }
		}
		  
		[XmlAttribute("PasswordAnswer")]
		[Bindable(true)]
		public string PasswordAnswer 
		{
			get { return GetColumnValue<string>(Columns.PasswordAnswer); }
			set { SetColumnValue(Columns.PasswordAnswer, value); }
		}
		  
		[XmlAttribute("IsApproved")]
		[Bindable(true)]
		public bool IsApproved 
		{
			get { return GetColumnValue<bool>(Columns.IsApproved); }
			set { SetColumnValue(Columns.IsApproved, value); }
		}
		  
		[XmlAttribute("IsLockedOut")]
		[Bindable(true)]
		public bool IsLockedOut 
		{
			get { return GetColumnValue<bool>(Columns.IsLockedOut); }
			set { SetColumnValue(Columns.IsLockedOut, value); }
		}
		  
		[XmlAttribute("CreateDate")]
		[Bindable(true)]
		public DateTime CreateDate 
		{
			get { return GetColumnValue<DateTime>(Columns.CreateDate); }
			set { SetColumnValue(Columns.CreateDate, value); }
		}
		  
		[XmlAttribute("LastLoginDate")]
		[Bindable(true)]
		public DateTime LastLoginDate 
		{
			get { return GetColumnValue<DateTime>(Columns.LastLoginDate); }
			set { SetColumnValue(Columns.LastLoginDate, value); }
		}
		  
		[XmlAttribute("LastPasswordChangedDate")]
		[Bindable(true)]
		public DateTime LastPasswordChangedDate 
		{
			get { return GetColumnValue<DateTime>(Columns.LastPasswordChangedDate); }
			set { SetColumnValue(Columns.LastPasswordChangedDate, value); }
		}
		  
		[XmlAttribute("LastLockoutDate")]
		[Bindable(true)]
		public DateTime LastLockoutDate 
		{
			get { return GetColumnValue<DateTime>(Columns.LastLockoutDate); }
			set { SetColumnValue(Columns.LastLockoutDate, value); }
		}
		  
		[XmlAttribute("FailedPasswordAttemptCount")]
		[Bindable(true)]
		public int FailedPasswordAttemptCount 
		{
			get { return GetColumnValue<int>(Columns.FailedPasswordAttemptCount); }
			set { SetColumnValue(Columns.FailedPasswordAttemptCount, value); }
		}
		  
		[XmlAttribute("FailedPasswordAttemptWindowStart")]
		[Bindable(true)]
		public DateTime FailedPasswordAttemptWindowStart 
		{
			get { return GetColumnValue<DateTime>(Columns.FailedPasswordAttemptWindowStart); }
			set { SetColumnValue(Columns.FailedPasswordAttemptWindowStart, value); }
		}
		  
		[XmlAttribute("FailedPasswordAnswerAttemptCount")]
		[Bindable(true)]
		public int FailedPasswordAnswerAttemptCount 
		{
			get { return GetColumnValue<int>(Columns.FailedPasswordAnswerAttemptCount); }
			set { SetColumnValue(Columns.FailedPasswordAnswerAttemptCount, value); }
		}
		  
		[XmlAttribute("FailedPasswordAnswerAttemptWindowStart")]
		[Bindable(true)]
		public DateTime FailedPasswordAnswerAttemptWindowStart 
		{
			get { return GetColumnValue<DateTime>(Columns.FailedPasswordAnswerAttemptWindowStart); }
			set { SetColumnValue(Columns.FailedPasswordAnswerAttemptWindowStart, value); }
		}
		  
		[XmlAttribute("Comment")]
		[Bindable(true)]
		public string Comment 
		{
			get { return GetColumnValue<string>(Columns.Comment); }
			set { SetColumnValue(Columns.Comment, value); }
		}
		
		#endregion
		
		
		#region PrimaryKey Methods		
		
        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);
            
            SetPKValues();
        }
        
		
		public _4screen.CSB.DataAccess.Business.HitblWidgetTemplatesWtpCollection HitblWidgetTemplatesWtpRecords()
		{
			return new _4screen.CSB.DataAccess.Business.HitblWidgetTemplatesWtpCollection().Where(HitblWidgetTemplatesWtp.Columns.UserID, UserId).Load();
		}
		#endregion
		
			
		
		#region ForeignKey Properties
		
		#endregion
		
		
		
		#region Many To Many Helpers
		
		#endregion
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(Guid varApplicationId,Guid varUserId,string varPassword,int varPasswordFormat,string varPasswordSalt,string varMobilePIN,string varEmail,string varLoweredEmail,string varPasswordQuestion,string varPasswordAnswer,bool varIsApproved,bool varIsLockedOut,DateTime varCreateDate,DateTime varLastLoginDate,DateTime varLastPasswordChangedDate,DateTime varLastLockoutDate,int varFailedPasswordAttemptCount,DateTime varFailedPasswordAttemptWindowStart,int varFailedPasswordAnswerAttemptCount,DateTime varFailedPasswordAnswerAttemptWindowStart,string varComment)
		{
			AspnetMembership item = new AspnetMembership();
			
			item.ApplicationId = varApplicationId;
			
			item.UserId = varUserId;
			
			item.Password = varPassword;
			
			item.PasswordFormat = varPasswordFormat;
			
			item.PasswordSalt = varPasswordSalt;
			
			item.MobilePIN = varMobilePIN;
			
			item.Email = varEmail;
			
			item.LoweredEmail = varLoweredEmail;
			
			item.PasswordQuestion = varPasswordQuestion;
			
			item.PasswordAnswer = varPasswordAnswer;
			
			item.IsApproved = varIsApproved;
			
			item.IsLockedOut = varIsLockedOut;
			
			item.CreateDate = varCreateDate;
			
			item.LastLoginDate = varLastLoginDate;
			
			item.LastPasswordChangedDate = varLastPasswordChangedDate;
			
			item.LastLockoutDate = varLastLockoutDate;
			
			item.FailedPasswordAttemptCount = varFailedPasswordAttemptCount;
			
			item.FailedPasswordAttemptWindowStart = varFailedPasswordAttemptWindowStart;
			
			item.FailedPasswordAnswerAttemptCount = varFailedPasswordAnswerAttemptCount;
			
			item.FailedPasswordAnswerAttemptWindowStart = varFailedPasswordAnswerAttemptWindowStart;
			
			item.Comment = varComment;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(Guid varApplicationId,Guid varUserId,string varPassword,int varPasswordFormat,string varPasswordSalt,string varMobilePIN,string varEmail,string varLoweredEmail,string varPasswordQuestion,string varPasswordAnswer,bool varIsApproved,bool varIsLockedOut,DateTime varCreateDate,DateTime varLastLoginDate,DateTime varLastPasswordChangedDate,DateTime varLastLockoutDate,int varFailedPasswordAttemptCount,DateTime varFailedPasswordAttemptWindowStart,int varFailedPasswordAnswerAttemptCount,DateTime varFailedPasswordAnswerAttemptWindowStart,string varComment)
		{
			AspnetMembership item = new AspnetMembership();
			
				item.ApplicationId = varApplicationId;
			
				item.UserId = varUserId;
			
				item.Password = varPassword;
			
				item.PasswordFormat = varPasswordFormat;
			
				item.PasswordSalt = varPasswordSalt;
			
				item.MobilePIN = varMobilePIN;
			
				item.Email = varEmail;
			
				item.LoweredEmail = varLoweredEmail;
			
				item.PasswordQuestion = varPasswordQuestion;
			
				item.PasswordAnswer = varPasswordAnswer;
			
				item.IsApproved = varIsApproved;
			
				item.IsLockedOut = varIsLockedOut;
			
				item.CreateDate = varCreateDate;
			
				item.LastLoginDate = varLastLoginDate;
			
				item.LastPasswordChangedDate = varLastPasswordChangedDate;
			
				item.LastLockoutDate = varLastLockoutDate;
			
				item.FailedPasswordAttemptCount = varFailedPasswordAttemptCount;
			
				item.FailedPasswordAttemptWindowStart = varFailedPasswordAttemptWindowStart;
			
				item.FailedPasswordAnswerAttemptCount = varFailedPasswordAnswerAttemptCount;
			
				item.FailedPasswordAnswerAttemptWindowStart = varFailedPasswordAnswerAttemptWindowStart;
			
				item.Comment = varComment;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn ApplicationIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn UserIdColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn PasswordColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn PasswordFormatColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn PasswordSaltColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn MobilePINColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn EmailColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn LoweredEmailColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn PasswordQuestionColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn PasswordAnswerColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn IsApprovedColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn IsLockedOutColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn CreateDateColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn LastLoginDateColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn LastPasswordChangedDateColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn LastLockoutDateColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn FailedPasswordAttemptCountColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn FailedPasswordAttemptWindowStartColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn FailedPasswordAnswerAttemptCountColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn FailedPasswordAnswerAttemptWindowStartColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn CommentColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string ApplicationId = @"ApplicationId";
			 public static string UserId = @"UserId";
			 public static string Password = @"Password";
			 public static string PasswordFormat = @"PasswordFormat";
			 public static string PasswordSalt = @"PasswordSalt";
			 public static string MobilePIN = @"MobilePIN";
			 public static string Email = @"Email";
			 public static string LoweredEmail = @"LoweredEmail";
			 public static string PasswordQuestion = @"PasswordQuestion";
			 public static string PasswordAnswer = @"PasswordAnswer";
			 public static string IsApproved = @"IsApproved";
			 public static string IsLockedOut = @"IsLockedOut";
			 public static string CreateDate = @"CreateDate";
			 public static string LastLoginDate = @"LastLoginDate";
			 public static string LastPasswordChangedDate = @"LastPasswordChangedDate";
			 public static string LastLockoutDate = @"LastLockoutDate";
			 public static string FailedPasswordAttemptCount = @"FailedPasswordAttemptCount";
			 public static string FailedPasswordAttemptWindowStart = @"FailedPasswordAttemptWindowStart";
			 public static string FailedPasswordAnswerAttemptCount = @"FailedPasswordAnswerAttemptCount";
			 public static string FailedPasswordAnswerAttemptWindowStart = @"FailedPasswordAnswerAttemptWindowStart";
			 public static string Comment = @"Comment";
						
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
