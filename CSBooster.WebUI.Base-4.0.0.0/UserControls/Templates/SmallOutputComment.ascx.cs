using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.WebUI.UserControls.Dashboard;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class SmallOutputComment : System.Web.UI.UserControl, IDataObjectWorker
    {
        public DataObject DataObject { get; set; }

        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetComments");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        protected DataObjectComment doComment;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            doComment = this.DataObject as DataObjectComment;
            Control ctrl = this.LoadControl("/UserControls/Templates/SmallOutputUser2.ascx");
            SmallOutputUser2 userOutput = ctrl as SmallOutputUser2;

            SetUserOutput(userOutput, doComment.UserID.Value, doComment.Nickname);

            phUserOutput.Controls.Add(userOutput);

            if (doComment.CommentStatus == 0)
            {
                CommentPreview commentPreview = (CommentPreview)LoadControl("/UserControls/Dashboard/CommentPreview.ascx");
                commentPreview.Comment = doComment;
                commentPreview.StripHtml = false;
                commentPreview.Type = "Comment";
                phCommentOuput.Controls.Add(commentPreview);
                LbtnDel.Visible = false;
                if (UserDataContext.GetUserDataContext().IsAdmin && doComment.CommentStatus != 1)
                {
                    LbtnDel.Visible = true;
                    LbtnDel.ID = "del_" + doComment.ObjectID.ToString().Replace("-", "");
                    LbtnDel.CommandArgument = doComment.ObjectID.ToString();
                }
            }
            else
            {
                phCommentOuput.Controls.Add(new LiteralControl(languageShared.GetString("MessageCommentDeletedByAdmin")));
                LbtnDel.Visible = false;
            }

        }

        private void SetUserOutput(SmallOutputUser2 userOutput, Guid userId, string userName)
        {
            if (userId != Constants.ANONYMOUS_USERID.ToGuid())
            {
                userOutput.DataObjectUser = DataObject.Load<DataObjectUser>(userId);
                userOutput.LinkActive = true;
            }
            else
            {
                userOutput.UserName = userName;
                userOutput.UserDetailURL = string.Empty;
                userOutput.UserPictureURL = _4screen.CSB.Common.SiteConfig.MediaDomainName + Helper.GetObjectType("User").DefaultImageURL;
                userOutput.PrimaryColor = Helper.GetDefaultUserPrimaryColor();
                userOutput.SecondaryColor = Helper.GetDefaultUserSecondaryColor();
            }
        }

        protected void OnDeleteClick(object sender, EventArgs e)
        {
            Guid commentId = ((LinkButton)sender).CommandArgument.ToGuid();
            DataObjectComment comment = DataObject.Load<DataObjectComment>(commentId);
            if (comment.State == ObjectState.Saved)
            {
                comment.CommentStatus = 1;
                comment.Update(UserDataContext.GetUserDataContext());
            }
        }
    }
}