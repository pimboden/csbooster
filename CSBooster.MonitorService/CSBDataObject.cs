//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.MonitorService
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.11.06  1.0.0.4  AW  Initial release
//*****************************************************************************************

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace _4screen.CSB.MonitorService
{
   public class CSBDataObject
   {
      private Guid objectId;
      private Guid communityId;
      private Guid userId;
      private ObjectType objectType;
      private string title;
      private string nickname;
      private bool featured;

      public Guid ObjectId
      {
         get { return objectId; }
         set { objectId = value; }
      }

      public Guid CommunityId
      {
         get { return communityId; }
         set { communityId = value; }
      }

      public Guid UserId
      {
         get { return userId; }
         set { userId = value; }
      }

      public ObjectType ObjectType
      {
         get { return objectType; }
         set { objectType = value; }
      }

      public string Title
      {
         get { return title; }
         set { title = value; }
      }

      public string Nickname
      {
         get { return nickname; }
         set { nickname = value; }
      }

      public bool Featured
      {
         get { return featured; }
         set { featured = value; }
      }

      public CSBDataObject()
      {
      }
   }
}
