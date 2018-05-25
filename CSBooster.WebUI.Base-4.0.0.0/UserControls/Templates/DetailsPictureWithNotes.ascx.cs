// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using Telerik.Web.UI;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class DetailsPictureWithNotes : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        protected DataObject dataObject;
        private DataObjectPicture picture;
        private UserDataContext udc;

        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Templates.WebUI.Base");
        public Dictionary<string, object> Settings { get; set; }

        public DataObject DataObject
        {
            get { return dataObject; }
            set { dataObject = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(Page).Scripts.Add(new ScriptReference("/Library/Scripts/DragResize.js"));
            ScriptManager.GetCurrent(Page).Scripts.Add(new ScriptReference("/Library/Scripts/PhotoNotes.js"));
            ScriptManager.GetCurrent(Page).Scripts.Add(new ScriptReference("/Library/Scripts/ImageAnnotation.js"));
            ScriptManager.RegisterStartupScript(this, GetType(), "ImageAnnotation", "$telerik.$(function() { initImageAnnotation(); } );", true);

            picture = (DataObjectPicture)dataObject;
            udc = UserDataContext.GetUserDataContext();

            btdAdd.ToolTip = language.GetString("TooltipPictureNoteAdd").StripForScript();
            btnHide.ToolTip = language.GetString("TooltipPictureNoteHide").StripForScript();
            btnShow.ToolTip = language.GetString("TooltipPictureNoteShow").StripForScript();

            if (Request.IsAuthenticated)
                btdAdd.Visible = true;
            else
                btdAdd.Visible = false;

            // Disable tooltip managers by default -> tooltip managers without targets don't work
            this.RTTM.Visible = false;
            if (picture != null)
            {
                foreach (string tooltipId in _4screen.CSB.DataAccess.Business.AdWordHelper.GetCampaignObjectIds(picture.ObjectID.Value))
                {
                    this.RTTM.TargetControls.Add(tooltipId, true);
                    this.RTTM.Visible = true;
                }
            }

            if (Settings.ContainsKey("Width") && !string.IsNullOrEmpty(Settings["Width"].ToString()))
            {
                int width = 0;
                if (int.TryParse(Settings["Width"].ToString(), out width))
                {
                    if (picture.Width > width)
                        Img.Attributes.Add("max-width", width + "px");
                }
            }
            Img.ImageUrl = _4screen.CSB.Common.SiteConfig.MediaDomainName + picture.GetImage(PictureVersion.L);

            LoadPictureNotes();

            var copyrightConfig = Helper.LoadConfig("Copyrights.config", string.Format("{0}/Configurations/Copyrights.config", WebRootPath.Instance.ToString()));
            string copyrightText = (from copyright in copyrightConfig.Element("Copyrights").Elements("Copyright") where int.Parse(copyright.Attribute("Value").Value) == picture.Copyright select copyright.Attribute("Name").Value).Single();
            LitCopyright.Text = copyrightText;

            if (!string.IsNullOrEmpty(picture.DescriptionLinked))
            {
                PnlDesc.Visible = true;
                PnlDesc.ID = null;
                LitDesc.Text = picture.DescriptionLinked;
            }
        }

        private void LoadPictureNotes()
        {
            bool blnIsFirend = true;
            if (!Request.IsAuthenticated)
                blnIsFirend = false;

            StringBuilder sbFriend = new StringBuilder(100);
            StringBuilder sbFriendID = new StringBuilder(100);

            sbFriend.Append("var nnArray = new Array(");
            sbFriendID.Append("var uidArray = new Array(");

            bool blnFirst = true;
            DataObjectList<DataObjectFriend> lstFriends = DataObjects.Load<DataObjectFriend>(new QuickParametersFriends { CurrentUserID = udc.UserID, OnlyWithImage = false, OnlyNotBlocked = true, Udc = udc, SortBy = QuickSort.StartDate, IgnoreCache = true });
            foreach (DataObjectFriend friend in lstFriends)
            {
                if (!blnFirst)
                {
                    sbFriend.AppendLine(",");
                    sbFriendID.AppendLine(",");
                }

                sbFriend.AppendFormat("'{0}'", friend.Nickname.StripForScript());
                sbFriendID.AppendFormat("'{0}'", friend.ObjectID.Value.ToString());
                blnFirst = false;
            }

            sbFriend.Append(");");
            sbFriendID.Append(");");


            btnHide.Visible = (picture.Notes.Count > 0);
            btnShow.Visible = (picture.Notes.Count > 0);

            blnFirst = true;
            StringBuilder sbNote = new StringBuilder(30 + (100 * picture.Notes.Count));
            sbNote.Append("var notesArray = [");
            foreach (PhotoNote note in picture.Notes)
            {
                if (!note.Public && udc.UserID != picture.UserID.Value && udc.UserID != note.FromID)
                    continue;

                int intCanSave = 0;
                int intCanDelete = 0;
                int intCanFree = 0;

                if (udc.UserID == picture.UserID.Value)
                {
                    intCanDelete = 1;
                    intCanSave = 1;
                    intCanFree = 1;
                }
                else if (udc.UserID == note.FromID && !note.Public)
                {
                    intCanDelete = 1;
                    intCanSave = 1;
                }

                if (!blnFirst)
                    sbNote.AppendLine(",");

                sbNote.Append("{");
                sbNote.AppendFormat("'id':'{0}','rect':", note.ID);
                sbNote.Append("{");
                sbNote.AppendFormat("'left':{0},'top':{1},'width':{2},'height':{3}", note.Left, note.Top, note.Width, note.Height);
                sbNote.Append("}");
                sbNote.AppendFormat(",'title':'{0}','text':'{1}','whoisid':'{2}','whois':'{3}','fromid':'{4}','from':'{5}','state':'{6}','canSave':'{7}','canDel':'{8}','canFree':'{9}'", note.Title.StripForScript(), note.Text.StripForScript(), note.WhoIsID, note.WhoIs.StripForScript(), note.FromID, note.From.StripForScript(), note.Public ? "2" : "1", intCanSave, intCanDelete, intCanFree);
                sbNote.Append("}");
                blnFirst = false;
            }
            sbNote.AppendLine("];");

            StringBuilder sbAll = new StringBuilder(50 + sbNote.Length + sbFriend.Length + sbFriendID.Length);
            sbAll.AppendLine("<script type='text/javascript'> ");
            sbAll.AppendLine(sbFriend.ToString());
            sbAll.AppendLine(sbFriendID.ToString());

            sbAll.AppendLine("function initImageAnnotation(){");
            sbAll.AppendLine(sbNote.ToString());
            sbAll.AppendFormat("imageAnnotation = new ImageAnnotation('{0}','{1}', notesArray);", blnIsFirend ? "0" : "1", PnlPic.ClientID);
            sbAll.AppendLine("}");
            sbAll.AppendLine("</script>");

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "jsgo", sbAll.ToString());

            this.ram1.AjaxRequest += new RadAjaxControl.AjaxRequestDelegate(ram1_AjaxRequest);
        }

        protected void ram1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Argument))
            {
                string[] arrNote = e.Argument.Split('|');
                if (arrNote.Length > 0 && arrNote[0] == "S")
                { //SAVE
                    bool isNew = false;
                    PhotoNote item;
                    if (arrNote[1] == "-1")
                    {
                        item = picture.Notes.AddNode(udc, udc.UserID, udc.Nickname);
                        isNew = true;
                    }
                    else
                    {
                        item = picture.Notes.ItemByID(arrNote[1]);
                    }

                    if (item != null)
                    {
                        item.Text = arrNote[2];
                        item.WhoIsID = arrNote[3];
                        item.WhoIs = arrNote[4];
                        item.Left = int.Parse(arrNote[8]);
                        item.Top = int.Parse(arrNote[9]);
                        item.Width = int.Parse(arrNote[10]);
                        item.Height = int.Parse(arrNote[11]);
                        if (arrNote[7] == "2")
                            item.Public = true;
                        picture.Update(UserDataContext.GetUserDataContext());
                        if (isNew)
                        {
                            UserActivities.InsertAnotatedObject(udc, picture.ObjectID.Value);
                        }
                    }
                }
                else if (arrNote.Length > 0 && arrNote[0] == "D")
                {
                    picture.Notes.RemoveNode(arrNote[1]);
                    picture.Update(UserDataContext.GetUserDataContext());
                }

                btnHide.Visible = (picture.Notes.Count > 0);
                btnShow.Visible = (picture.Notes.Count > 0);

            }
        }

        protected void OnAjaxUpdate(object sender, Telerik.Web.UI.ToolTipUpdateEventArgs e)
        {
            string[] tooltipId = e.TargetControlID.Split(new char[] { '_' });
            if (tooltipId.Length == 4)
            {
                Literal literal = new Literal();
                literal.Text = _4screen.CSB.DataAccess.Business.AdWordHelper.GetCampaignContent(new Guid(tooltipId[0]), new Guid(tooltipId[1]), UserDataContext.GetUserDataContext(), tooltipId[2], "Popup");
                literal.Text = System.Text.RegularExpressions.Regex.Replace(literal.Text, @"(/Pages/Other/AdCampaignRedirecter.aspx\?CID=\w{8}-\w{4}-\w{4}-\w{4}-\w{12})", "$1&OID=" + tooltipId[1] + "&Word=" + tooltipId[2] + "&Type=PopupLink");
                e.UpdatePanel.ContentTemplateContainer.Controls.Add(literal);
            }
        }
    }
}