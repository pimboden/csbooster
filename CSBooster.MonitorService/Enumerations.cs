//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.MonitorService
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.07.24  1.0.0.0  AW  Initial release
//  2007.11.20  1.0.0.0  AW  Enumerations added and changed
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
   public enum Regions
   {
      Westschweiz = 1,
      Nordwestschweiz = 2,
      Bern_Oberwallis = 3,
      Basel = 4,
      Aarau = 5,
      Innerschweiz_Tessin = 6,
      Graubünden = 7,
      Zürich = 8,
      Ostschweiz = 9
   }

   public enum ObjectType
   {
      /*None = 0,*/
      Community = 1,
      User = 2,
      Picture = 3,
      Video = 4,
      Tag = 5,
      Audio = 6,
      Article = 7,
      /*Blog = 8,
      BlogItem = 9,*/
      Forum = 10,
      ForumTopic = 11,
      ForumTopicItem = 12,
      SlideShow = 13,
      Folder = 14,
      Event = 15,
      ProfileCommunity = 19
   }

   public enum Gender
   {
      Männlich,
      Weiblich,
      Unbekannt
   }

   public enum AdWordFilterActions
   {
      Link,
      Popup
   }

   public enum BadWordFilterActions
   {
      None = 0,
      Censor = 1,
      Inform = 2,
      Lock = 4
   }
}
