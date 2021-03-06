﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _4screen.CSB.Extensions.Data
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
	
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="CSBooster")]
	public partial class CSBooster_ExtensionsDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public CSBooster_ExtensionsDataContext() : 
				base(global::_4screen.CSB.Extensions.Properties.Settings.Default.CSBoosterConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public CSBooster_ExtensionsDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CSBooster_ExtensionsDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CSBooster_ExtensionsDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CSBooster_ExtensionsDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[Function(Name="dbo.hisp_WebServicesLog_Insert")]
		public int hisp_WebServicesLog_Insert(
					[Parameter(Name="WSE_IP", DbType="VarChar(15)")] string wSE_IP, 
					[Parameter(Name="USR_ID", DbType="UniqueIdentifier")] System.Nullable<System.Guid> uSR_ID, 
					[Parameter(Name="PAR_ID", DbType="UniqueIdentifier")] System.Nullable<System.Guid> pAR_ID, 
					[Parameter(Name="OBJ_ID", DbType="UniqueIdentifier")] System.Nullable<System.Guid> oBJ_ID, 
					[Parameter(Name="WSE_StartDate", DbType="DateTime")] System.Nullable<System.DateTime> wSE_StartDate, 
					[Parameter(Name="WSE_EndDate", DbType="DateTime")] System.Nullable<System.DateTime> wSE_EndDate, 
					[Parameter(Name="WSE_ServiceType", DbType="VarChar(20)")] string wSE_ServiceType, 
					[Parameter(Name="WSE_SerivceName", DbType="VarChar(50)")] string wSE_SerivceName, 
					[Parameter(Name="WSE_Method", DbType="VarChar(20)")] string wSE_Method, 
					[Parameter(Name="WSE_Parameters", DbType="VarChar(200)")] string wSE_Parameters, 
					[Parameter(Name="WSE_Result", DbType="VarChar(20)")] string wSE_Result, 
					[Parameter(Name="WSE_GeoService", DbType="VarChar(20)")] string wSE_GeoService, 
					[Parameter(Name="WSE_FilesToDownload", DbType="Int")] System.Nullable<int> wSE_FilesToDownload, 
					[Parameter(Name="WSE_FilesDownloaded", DbType="Int")] System.Nullable<int> wSE_FilesDownloaded, 
					[Parameter(Name="WSE_Message", DbType="VarChar(500)")] string wSE_Message, 
					[Parameter(Name="WSE_ExtendedMessage", DbType="VarChar(MAX)")] string wSE_ExtendedMessage)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), wSE_IP, uSR_ID, pAR_ID, oBJ_ID, wSE_StartDate, wSE_EndDate, wSE_ServiceType, wSE_SerivceName, wSE_Method, wSE_Parameters, wSE_Result, wSE_GeoService, wSE_FilesToDownload, wSE_FilesDownloaded, wSE_Message, wSE_ExtendedMessage);
			return ((int)(result.ReturnValue));
		}
	}
}
#pragma warning restore 1591
