using System;
using System.Collections.Generic;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Notification.Business;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class Notifications : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Notification); 

            Pages.Popups.UserControls.Notification notification = this.LoadControl("/Pages/Popups/UserControls/Notification.ascx") as Pages.Popups.UserControls.Notification;
            notification.ID = "NOT";

            if (!string.IsNullOrEmpty(Request.QueryString["OID"]))
            {
                notification.ObjectID = Request.QueryString["OID"].ToGuid();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["UI"]))
            {
                notification.ObjectID = Request.QueryString["UI"].ToGuid();
                notification.UserID = Request.QueryString["UI"].ToGuid();
            }
            string paramCtyId = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["CN"]))
            {
                paramCtyId = Request.QueryString["CN"];

                if (!paramCtyId.IsGuid())
                    notification.ObjectID = notification.CommunityID = DataObjectCommunity.GetCommunityIDByVirtualURL(paramCtyId);
                else
                    notification.ObjectID = notification.CommunityID = paramCtyId.ToGuid();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["OT"]))
            {
                notification.ObjectType = Helper.GetObjectTypeNumericID(Request.QueryString["OT"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["OTS"]))
            {
                string[] objectTypesArray = Request.QueryString["OTS"].Split(',');
                int[] objectTypes = new int[objectTypesArray.Length];
                for (int i = 0; i < objectTypesArray.Length; i++)
                {
                    objectTypes[i] = Helper.GetObjectTypeNumericID(objectTypesArray[i]);
                }
                notification.ObjectTypes = objectTypes;
            }

            List<TagWord> tagWords = new List<TagWord>();
            if (!string.IsNullOrEmpty(Request.QueryString["TGL1"]))
            {
                string[] tagWordIDs = QuickParameters.GetDelimitedTagIds(Request.QueryString["TGL1"], ',').Split('|');
                foreach (string tagWordID in tagWordIDs)
                {
                    tagWords.Add(new TagWord() { TagID = tagWordID, GroupID = 1 });
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["TGL2"]))
            {
                string[] tagWordIDs = QuickParameters.GetDelimitedTagIds(Request.QueryString["TGL2"], ',').Split('|');
                foreach (string tagWordID in tagWordIDs)
                {
                    tagWords.Add(new TagWord() { TagID = tagWordID, GroupID = 2 });
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["TGL3"]))
            {
                string[] tagWordIDs = QuickParameters.GetDelimitedTagIds(Request.QueryString["TGL3"], ',').Split('|');
                foreach (string tagWordID in tagWordIDs)
                {
                    tagWords.Add(new TagWord() { TagID = tagWordID, GroupID = 3 });
                }
            }
            notification.TagWords = tagWords;

            ph.Controls.Add(notification);
            base.OnInit(e);
        }
    }
}