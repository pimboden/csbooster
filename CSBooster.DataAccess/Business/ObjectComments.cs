//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		26.03.2007 / PI
//  Updated:   
//******************************************************************************

using System;
using System.Collections.Generic;
using System.Configuration;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public static class ObjectComments
    {
        private static string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;

        public static List<ObjectComment> GetCommentsReceived(Guid userId, DateTime? createdFrom, DateTime? createdTo, string text, Guid? objectId, string username, string generalSearchParam, int? pageNum, int? pageSize, string sortAttr, string sortDir, out int numberItems)
        {
            List<ObjectComment> commentList = new List<ObjectComment>();

            int? refNumberItems = null;
            CSBooster_DataContext cdc = new CSBooster_DataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);
            var commentResults = cdc.hisp_Comments_GetCommentsReceived(userId, createdFrom, createdTo, "%" + text + "%", objectId, "%" + username + "%", "%" + generalSearchParam + "%", pageNum, pageSize, sortAttr, sortDir, ref refNumberItems);
            foreach (CommentResult commentResult in commentResults)
            {
                ObjectComment comment = new ObjectComment();
                ObjectComment.FillComment(comment, commentResult);
                commentList.Add(comment);
            }

            numberItems = refNumberItems.Value;

            return commentList;
        }

        public static List<ObjectComment> GetCommentsPosted(Guid userId, DateTime? createdFrom, DateTime? createdTo, string text, Guid? objectId, string username, string generalSearchParam, int? pageNum, int? pageSize, string sortAttr, string sortDir, out int numberItems)
        {
            List<ObjectComment> commentList = new List<ObjectComment>();

            int? refNumberItems = null;
            CSBooster_DataContext cdc = new CSBooster_DataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);
            var commentResults = cdc.hisp_Comments_GetCommentsPosted(userId, createdFrom, createdTo, "%" + text + "%", objectId, "%" + username + "%", "%" + generalSearchParam + "%", pageNum, pageSize, sortAttr, sortDir, ref refNumberItems);
            foreach (CommentResult commentResult in commentResults)
            {
                ObjectComment comment = new ObjectComment();
                ObjectComment.FillComment(comment, commentResult);
                commentList.Add(comment);
            }

            numberItems = refNumberItems.Value;

            return commentList;
        }

        public static List<ObjectComment> GetComments(Guid? userId, DateTime? createdFrom, DateTime? createdTo, string text, Guid? objectId, string username, string generalSearchParam, int? pageNum, int? pageSize, string sortAttr, string sortDir, out int numberItems)
        {
            List<ObjectComment> commentList = new List<ObjectComment>();

            int? refNumberItems = null;
            CSBooster_DataContext cdc = new CSBooster_DataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);
            var commentResults = cdc.hisp_Comments_GetComments(userId, createdFrom, createdTo, "%" + text + "%", objectId, "%" + username + "%", "%" + generalSearchParam + "%", pageNum, pageSize, sortAttr, sortDir, ref refNumberItems);
            foreach (CommentResult commentResult in commentResults)
            {
                ObjectComment comment = new ObjectComment();
                ObjectComment.FillComment(comment, commentResult);
                commentList.Add(comment);
            }

            numberItems = refNumberItems.Value;

            return commentList;
        }
    }
}