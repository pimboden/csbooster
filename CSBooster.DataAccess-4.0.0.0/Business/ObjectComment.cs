// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Linq;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Data;

namespace _4screen.CSB.DataAccess.Business
{
    public class ObjectComment
    {
        public Guid ComId { get; set; }
        public Guid ObjId { get; set; }
        public Guid UsrId { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public CommentStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int ObjType { get; set; }

        public ObjectComment()
        {
            ComId = Guid.NewGuid();
            ObjId = Guid.Empty;
            UsrId = Guid.Empty;
            Text = string.Empty;
            Status = CommentStatus.None;
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            ObjType = 0;
        }

        public static ObjectComment Load(Guid comId)
        {
            ObjectComment comment = new ObjectComment();
            try
            {
                CSBooster_DataContext cdc = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
                CommentResult result = cdc.hisp_Comments_GetComment(comId).ElementAtOrDefault(0);

                if (result != null)
                {
                    FillComment(comment, result);
                }
            }
            catch
            {
            }
            return comment;
        }

        public static void Delete(Guid comId)
        {
            CSBooster_DataContext cdc = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            cdc.hisp_Comments_SetCommentStatus(comId, (int?) CommentStatus.Deleted);
        }

        internal static void FillComment(ObjectComment comment, CommentResult result)
        {
            comment.ComId = result.COM_ID;
            comment.ObjId = result.OBJ_ID;
            comment.UsrId = result.USR_ID;
            comment.Username = result.UserName;
            comment.Text = result.COM_Text;
            comment.Status = (CommentStatus) result.COM_Status;
            comment.CreateDate = result.COM_InsertedDate;
            comment.UpdateDate = result.COM_UpdatedDate;
            comment.ObjType = result.OBJ_Type;
        }
    }
}