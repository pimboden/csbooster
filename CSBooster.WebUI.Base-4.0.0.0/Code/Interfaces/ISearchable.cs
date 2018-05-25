//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		28.09.2007 / PI
//  Updated:   
//******************************************************************************
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using _4screen.CSB.Common;

/// <summary>
/// Summary description for ISearchable
/// </summary>
namespace _4screen.CSB.WebUI
{
   public interface ISearchable
   {
      int ObjType
      {
         get;
         set;
      }

      QuickSort Sort
      {
         get;
         set;
      }

      QuickSortDirection SortDirection
      {
         get;
         set;
      }

      DataAccessType AccesType
      {
         get;
         set;
      }

       Nullable<ObjectShowState> ShowState
      {
         get;
         set;
      }

      Nullable<DateTime> DateFrom
      {
         get;
         set;
      }

      Nullable<DateTime> DateTo
      {
         get;
         set;
      }

     void DoSearch();
  }
}