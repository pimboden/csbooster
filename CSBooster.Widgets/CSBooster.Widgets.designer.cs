﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4927
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _4screen.CSB.Widget
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
    using System.Configuration;
	
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="CSBooster.V20")]
	public partial class CSBooster_WidgetDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void Inserthitbl_WidgetInstance_IN(hitbl_WidgetInstance_IN instance);
    partial void Updatehitbl_WidgetInstance_IN(hitbl_WidgetInstance_IN instance);
    partial void Deletehitbl_WidgetInstance_IN(hitbl_WidgetInstance_IN instance);
    partial void Inserthitbl_WidgetInstanceText_WIT(hitbl_WidgetInstanceText_WIT instance);
    partial void Updatehitbl_WidgetInstanceText_WIT(hitbl_WidgetInstanceText_WIT instance);
    partial void Deletehitbl_WidgetInstanceText_WIT(hitbl_WidgetInstanceText_WIT instance);
    #endregion
		
		public CSBooster_WidgetDataContext() : 
            base(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public CSBooster_WidgetDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CSBooster_WidgetDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CSBooster_WidgetDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CSBooster_WidgetDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<hitbl_WidgetInstance_IN> hitbl_WidgetInstance_INs
		{
			get
			{
				return this.GetTable<hitbl_WidgetInstance_IN>();
			}
		}
		
		public System.Data.Linq.Table<hitbl_WidgetInstanceText_WIT> hitbl_WidgetInstanceText_WITs
		{
			get
			{
				return this.GetTable<hitbl_WidgetInstanceText_WIT>();
			}
		}
		
		[Function(Name="dbo.hisp_Widget_LoadInstanceData")]
		public ISingleResult<hisp_Widget_LoadInstanceDataResult> hisp_Widget_LoadInstanceData([Parameter(Name="INS_ID", DbType="UniqueIdentifier")] System.Nullable<System.Guid> iNS_ID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iNS_ID);
			return ((ISingleResult<hisp_Widget_LoadInstanceDataResult>)(result.ReturnValue));
		}
		
		[Function(Name="dbo.hisp_Community_IsUserMember")]
		public ISingleResult<hisp_Community_IsUserMemberResult> hisp_Community_IsUserMember([Parameter(Name="CommunityId", DbType="UniqueIdentifier")] System.Nullable<System.Guid> communityId, [Parameter(Name="UserId", DbType="UniqueIdentifier")] System.Nullable<System.Guid> userId)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), communityId, userId);
			return ((ISingleResult<hisp_Community_IsUserMemberResult>)(result.ReturnValue));
		}
		
		[Function(Name="dbo.hisp_Widget_SaveInstanceData")]
		public int hisp_Widget_SaveInstanceData([Parameter(Name="INS_ID", DbType="UniqueIdentifier")] System.Nullable<System.Guid> iNS_ID, [Parameter(Name="INS_XmlStateData", DbType="NVarChar(MAX)")] string iNS_XmlStateData)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iNS_ID, iNS_XmlStateData);
			return ((int)(result.ReturnValue));
		}
		
		[Function(Name="dbo.hisp_Widget_GetDetailWidgetPages")]
		public ISingleResult<hisp_Widget_GetDetailWidgetPagesResult> hisp_Widget_GetDetailWidgetPages([Parameter(Name="WidgetId", DbType="UniqueIdentifier")] System.Nullable<System.Guid> widgetId, [Parameter(Name="XmlStateData", DbType="NVarChar(255)")] string xmlStateData)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), widgetId, xmlStateData);
			return ((ISingleResult<hisp_Widget_GetDetailWidgetPagesResult>)(result.ReturnValue));
		}
	}
	
	[Table(Name="dbo.hitbl_WidgetInstance_INS")]
	public partial class hitbl_WidgetInstance_IN : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _INS_ID;
		
		private System.Guid _WDG_ID;
		
		private System.Guid _INS_PAG_ID;
		
		private int _INS_ColumnNo;
		
		private int _INS_OrderNo;
		
		private string _INS_XmlStateData;
		
		private System.DateTime _INS_CreatedDate;
		
		private System.DateTime _INS_LastUpdate;
		
		private bool _INS_Expanded;
		
		private System.Nullable<System.Guid> _WTP_ID;
		
		private bool _INS_IsFixed;
		
		private bool _INS_HideIfNoContent;
		
		private string _INS_ViewRoles;
		
		private EntitySet<hitbl_WidgetInstanceText_WIT> _hitbl_WidgetInstanceText_WITs;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnINS_IDChanging(System.Guid value);
    partial void OnINS_IDChanged();
    partial void OnWDG_IDChanging(System.Guid value);
    partial void OnWDG_IDChanged();
    partial void OnINS_PAG_IDChanging(System.Guid value);
    partial void OnINS_PAG_IDChanged();
    partial void OnINS_ColumnNoChanging(int value);
    partial void OnINS_ColumnNoChanged();
    partial void OnINS_OrderNoChanging(int value);
    partial void OnINS_OrderNoChanged();
    partial void OnINS_XmlStateDataChanging(string value);
    partial void OnINS_XmlStateDataChanged();
    partial void OnINS_CreatedDateChanging(System.DateTime value);
    partial void OnINS_CreatedDateChanged();
    partial void OnINS_LastUpdateChanging(System.DateTime value);
    partial void OnINS_LastUpdateChanged();
    partial void OnINS_ExpandedChanging(bool value);
    partial void OnINS_ExpandedChanged();
    partial void OnWTP_IDChanging(System.Nullable<System.Guid> value);
    partial void OnWTP_IDChanged();
    partial void OnINS_IsFixedChanging(bool value);
    partial void OnINS_IsFixedChanged();
    partial void OnINS_HideIfNoContentChanging(bool value);
    partial void OnINS_HideIfNoContentChanged();
    partial void OnINS_ViewRolesChanging(string value);
    partial void OnINS_ViewRolesChanged();
    #endregion
		
		public hitbl_WidgetInstance_IN()
		{
			this._hitbl_WidgetInstanceText_WITs = new EntitySet<hitbl_WidgetInstanceText_WIT>(new Action<hitbl_WidgetInstanceText_WIT>(this.attach_hitbl_WidgetInstanceText_WITs), new Action<hitbl_WidgetInstanceText_WIT>(this.detach_hitbl_WidgetInstanceText_WITs));
			OnCreated();
		}
		
		[Column(Storage="_INS_ID", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid INS_ID
		{
			get
			{
				return this._INS_ID;
			}
			set
			{
				if ((this._INS_ID != value))
				{
					this.OnINS_IDChanging(value);
					this.SendPropertyChanging();
					this._INS_ID = value;
					this.SendPropertyChanged("INS_ID");
					this.OnINS_IDChanged();
				}
			}
		}
		
		[Column(Storage="_WDG_ID", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid WDG_ID
		{
			get
			{
				return this._WDG_ID;
			}
			set
			{
				if ((this._WDG_ID != value))
				{
					this.OnWDG_IDChanging(value);
					this.SendPropertyChanging();
					this._WDG_ID = value;
					this.SendPropertyChanged("WDG_ID");
					this.OnWDG_IDChanged();
				}
			}
		}
		
		[Column(Storage="_INS_PAG_ID", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid INS_PAG_ID
		{
			get
			{
				return this._INS_PAG_ID;
			}
			set
			{
				if ((this._INS_PAG_ID != value))
				{
					this.OnINS_PAG_IDChanging(value);
					this.SendPropertyChanging();
					this._INS_PAG_ID = value;
					this.SendPropertyChanged("INS_PAG_ID");
					this.OnINS_PAG_IDChanged();
				}
			}
		}
		
		[Column(Storage="_INS_ColumnNo", DbType="Int NOT NULL")]
		public int INS_ColumnNo
		{
			get
			{
				return this._INS_ColumnNo;
			}
			set
			{
				if ((this._INS_ColumnNo != value))
				{
					this.OnINS_ColumnNoChanging(value);
					this.SendPropertyChanging();
					this._INS_ColumnNo = value;
					this.SendPropertyChanged("INS_ColumnNo");
					this.OnINS_ColumnNoChanged();
				}
			}
		}
		
		[Column(Storage="_INS_OrderNo", DbType="Int NOT NULL")]
		public int INS_OrderNo
		{
			get
			{
				return this._INS_OrderNo;
			}
			set
			{
				if ((this._INS_OrderNo != value))
				{
					this.OnINS_OrderNoChanging(value);
					this.SendPropertyChanging();
					this._INS_OrderNo = value;
					this.SendPropertyChanged("INS_OrderNo");
					this.OnINS_OrderNoChanged();
				}
			}
		}
		
		[Column(Storage="_INS_XmlStateData", DbType="NVarChar(MAX)")]
		public string INS_XmlStateData
		{
			get
			{
				return this._INS_XmlStateData;
			}
			set
			{
				if ((this._INS_XmlStateData != value))
				{
					this.OnINS_XmlStateDataChanging(value);
					this.SendPropertyChanging();
					this._INS_XmlStateData = value;
					this.SendPropertyChanged("INS_XmlStateData");
					this.OnINS_XmlStateDataChanged();
				}
			}
		}
		
		[Column(Storage="_INS_CreatedDate", DbType="DateTime NOT NULL")]
		public System.DateTime INS_CreatedDate
		{
			get
			{
				return this._INS_CreatedDate;
			}
			set
			{
				if ((this._INS_CreatedDate != value))
				{
					this.OnINS_CreatedDateChanging(value);
					this.SendPropertyChanging();
					this._INS_CreatedDate = value;
					this.SendPropertyChanged("INS_CreatedDate");
					this.OnINS_CreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_INS_LastUpdate", DbType="DateTime NOT NULL")]
		public System.DateTime INS_LastUpdate
		{
			get
			{
				return this._INS_LastUpdate;
			}
			set
			{
				if ((this._INS_LastUpdate != value))
				{
					this.OnINS_LastUpdateChanging(value);
					this.SendPropertyChanging();
					this._INS_LastUpdate = value;
					this.SendPropertyChanged("INS_LastUpdate");
					this.OnINS_LastUpdateChanged();
				}
			}
		}
		
		[Column(Storage="_INS_Expanded", DbType="Bit NOT NULL")]
		public bool INS_Expanded
		{
			get
			{
				return this._INS_Expanded;
			}
			set
			{
				if ((this._INS_Expanded != value))
				{
					this.OnINS_ExpandedChanging(value);
					this.SendPropertyChanging();
					this._INS_Expanded = value;
					this.SendPropertyChanged("INS_Expanded");
					this.OnINS_ExpandedChanged();
				}
			}
		}
		
		[Column(Storage="_WTP_ID", DbType="UniqueIdentifier")]
		public System.Nullable<System.Guid> WTP_ID
		{
			get
			{
				return this._WTP_ID;
			}
			set
			{
				if ((this._WTP_ID != value))
				{
					this.OnWTP_IDChanging(value);
					this.SendPropertyChanging();
					this._WTP_ID = value;
					this.SendPropertyChanged("WTP_ID");
					this.OnWTP_IDChanged();
				}
			}
		}
		
		[Column(Storage="_INS_IsFixed", DbType="Bit NOT NULL")]
		public bool INS_IsFixed
		{
			get
			{
				return this._INS_IsFixed;
			}
			set
			{
				if ((this._INS_IsFixed != value))
				{
					this.OnINS_IsFixedChanging(value);
					this.SendPropertyChanging();
					this._INS_IsFixed = value;
					this.SendPropertyChanged("INS_IsFixed");
					this.OnINS_IsFixedChanged();
				}
			}
		}
		
		[Column(Storage="_INS_HideIfNoContent", DbType="Bit NOT NULL")]
		public bool INS_HideIfNoContent
		{
			get
			{
				return this._INS_HideIfNoContent;
			}
			set
			{
				if ((this._INS_HideIfNoContent != value))
				{
					this.OnINS_HideIfNoContentChanging(value);
					this.SendPropertyChanging();
					this._INS_HideIfNoContent = value;
					this.SendPropertyChanged("INS_HideIfNoContent");
					this.OnINS_HideIfNoContentChanged();
				}
			}
		}
		
		[Column(Storage="_INS_ViewRoles", DbType="NVarChar(MAX)")]
		public string INS_ViewRoles
		{
			get
			{
				return this._INS_ViewRoles;
			}
			set
			{
				if ((this._INS_ViewRoles != value))
				{
					this.OnINS_ViewRolesChanging(value);
					this.SendPropertyChanging();
					this._INS_ViewRoles = value;
					this.SendPropertyChanged("INS_ViewRoles");
					this.OnINS_ViewRolesChanged();
				}
			}
		}
		
		[Association(Name="hitbl_WidgetInstance_IN_hitbl_WidgetInstanceText_WIT", Storage="_hitbl_WidgetInstanceText_WITs", ThisKey="INS_ID", OtherKey="INS_ID")]
		public EntitySet<hitbl_WidgetInstanceText_WIT> hitbl_WidgetInstanceText_WITs
		{
			get
			{
				return this._hitbl_WidgetInstanceText_WITs;
			}
			set
			{
				this._hitbl_WidgetInstanceText_WITs.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_hitbl_WidgetInstanceText_WITs(hitbl_WidgetInstanceText_WIT entity)
		{
			this.SendPropertyChanging();
			entity.hitbl_WidgetInstance_IN = this;
		}
		
		private void detach_hitbl_WidgetInstanceText_WITs(hitbl_WidgetInstanceText_WIT entity)
		{
			this.SendPropertyChanging();
			entity.hitbl_WidgetInstance_IN = null;
		}
	}
	
	[Table(Name="dbo.hitbl_WidgetInstanceText_WIT")]
	public partial class hitbl_WidgetInstanceText_WIT : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _INS_ID;
		
		private string _WIT_LangCode;
		
		private string _WIT_Title;
		
		private EntityRef<hitbl_WidgetInstance_IN> _hitbl_WidgetInstance_IN;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnINS_IDChanging(System.Guid value);
    partial void OnINS_IDChanged();
    partial void OnWIT_LangCodeChanging(string value);
    partial void OnWIT_LangCodeChanged();
    partial void OnWIT_TitleChanging(string value);
    partial void OnWIT_TitleChanged();
    #endregion
		
		public hitbl_WidgetInstanceText_WIT()
		{
			this._hitbl_WidgetInstance_IN = default(EntityRef<hitbl_WidgetInstance_IN>);
			OnCreated();
		}
		
		[Column(Storage="_INS_ID", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid INS_ID
		{
			get
			{
				return this._INS_ID;
			}
			set
			{
				if ((this._INS_ID != value))
				{
					if (this._hitbl_WidgetInstance_IN.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnINS_IDChanging(value);
					this.SendPropertyChanging();
					this._INS_ID = value;
					this.SendPropertyChanged("INS_ID");
					this.OnINS_IDChanged();
				}
			}
		}
		
		[Column(Storage="_WIT_LangCode", DbType="VarChar(10) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string WIT_LangCode
		{
			get
			{
				return this._WIT_LangCode;
			}
			set
			{
				if ((this._WIT_LangCode != value))
				{
					this.OnWIT_LangCodeChanging(value);
					this.SendPropertyChanging();
					this._WIT_LangCode = value;
					this.SendPropertyChanged("WIT_LangCode");
					this.OnWIT_LangCodeChanged();
				}
			}
		}
		
		[Column(Storage="_WIT_Title", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string WIT_Title
		{
			get
			{
				return this._WIT_Title;
			}
			set
			{
				if ((this._WIT_Title != value))
				{
					this.OnWIT_TitleChanging(value);
					this.SendPropertyChanging();
					this._WIT_Title = value;
					this.SendPropertyChanged("WIT_Title");
					this.OnWIT_TitleChanged();
				}
			}
		}
		
		[Association(Name="hitbl_WidgetInstance_IN_hitbl_WidgetInstanceText_WIT", Storage="_hitbl_WidgetInstance_IN", ThisKey="INS_ID", OtherKey="INS_ID", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public hitbl_WidgetInstance_IN hitbl_WidgetInstance_IN
		{
			get
			{
				return this._hitbl_WidgetInstance_IN.Entity;
			}
			set
			{
				hitbl_WidgetInstance_IN previousValue = this._hitbl_WidgetInstance_IN.Entity;
				if (((previousValue != value) 
							|| (this._hitbl_WidgetInstance_IN.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._hitbl_WidgetInstance_IN.Entity = null;
						previousValue.hitbl_WidgetInstanceText_WITs.Remove(this);
					}
					this._hitbl_WidgetInstance_IN.Entity = value;
					if ((value != null))
					{
						value.hitbl_WidgetInstanceText_WITs.Add(this);
						this._INS_ID = value.INS_ID;
					}
					else
					{
						this._INS_ID = default(System.Guid);
					}
					this.SendPropertyChanged("hitbl_WidgetInstance_IN");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	public partial class hisp_Widget_LoadInstanceDataResult
	{
		
		private string _INS_XmlStateData;
		
		public hisp_Widget_LoadInstanceDataResult()
		{
		}
		
		[Column(Storage="_INS_XmlStateData", DbType="NVarChar(MAX)")]
		public string INS_XmlStateData
		{
			get
			{
				return this._INS_XmlStateData;
			}
			set
			{
				if ((this._INS_XmlStateData != value))
				{
					this._INS_XmlStateData = value;
				}
			}
		}
	}
	
	public partial class hisp_Community_IsUserMemberResult
	{
		
		private System.Guid _CTY_ID;
		
		private System.Guid _USR_ID;
		
		private bool _CUR_IsOwner;
		
		private System.Nullable<int> _CUR_Status;
		
		private System.Nullable<System.DateTime> _CUR_InsertedDate;
		
		private System.Nullable<System.Guid> _USR_ID_InvitedBy;
		
		public hisp_Community_IsUserMemberResult()
		{
		}
		
		[Column(Storage="_CTY_ID", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid CTY_ID
		{
			get
			{
				return this._CTY_ID;
			}
			set
			{
				if ((this._CTY_ID != value))
				{
					this._CTY_ID = value;
				}
			}
		}
		
		[Column(Storage="_USR_ID", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid USR_ID
		{
			get
			{
				return this._USR_ID;
			}
			set
			{
				if ((this._USR_ID != value))
				{
					this._USR_ID = value;
				}
			}
		}
		
		[Column(Storage="_CUR_IsOwner", DbType="Bit NOT NULL")]
		public bool CUR_IsOwner
		{
			get
			{
				return this._CUR_IsOwner;
			}
			set
			{
				if ((this._CUR_IsOwner != value))
				{
					this._CUR_IsOwner = value;
				}
			}
		}
		
		[Column(Storage="_CUR_Status", DbType="Int")]
		public System.Nullable<int> CUR_Status
		{
			get
			{
				return this._CUR_Status;
			}
			set
			{
				if ((this._CUR_Status != value))
				{
					this._CUR_Status = value;
				}
			}
		}
		
		[Column(Storage="_CUR_InsertedDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> CUR_InsertedDate
		{
			get
			{
				return this._CUR_InsertedDate;
			}
			set
			{
				if ((this._CUR_InsertedDate != value))
				{
					this._CUR_InsertedDate = value;
				}
			}
		}
		
		[Column(Storage="_USR_ID_InvitedBy", DbType="UniqueIdentifier")]
		public System.Nullable<System.Guid> USR_ID_InvitedBy
		{
			get
			{
				return this._USR_ID_InvitedBy;
			}
			set
			{
				if ((this._USR_ID_InvitedBy != value))
				{
					this._USR_ID_InvitedBy = value;
				}
			}
		}
	}
	
	public partial class hisp_Widget_GetDetailWidgetPagesResult
	{
		
		private string _OBJ_Title;
		
		private string _VirtualURL;
		
		public hisp_Widget_GetDetailWidgetPagesResult()
		{
		}
		
		[Column(Storage="_OBJ_Title", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string OBJ_Title
		{
			get
			{
				return this._OBJ_Title;
			}
			set
			{
				if ((this._OBJ_Title != value))
				{
					this._OBJ_Title = value;
				}
			}
		}
		
		[Column(Storage="_VirtualURL", DbType="NVarChar(100)")]
		public string VirtualURL
		{
			get
			{
				return this._VirtualURL;
			}
			set
			{
				if ((this._VirtualURL != value))
				{
					this._VirtualURL = value;
				}
			}
		}
	}
}
#pragma warning restore 1591