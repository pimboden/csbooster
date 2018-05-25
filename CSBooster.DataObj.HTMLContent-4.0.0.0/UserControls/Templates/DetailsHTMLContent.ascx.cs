// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.DataObj.Business;
using _4screen.CSB.Widget;

namespace _4screen.CSB.DataObj.UserControls.Templates
{
    public partial class DetailsHTMLContent : System.Web.UI.UserControl, ISettings, IDataObjectWorker
    {
        private DataObjectHTMLContent dataObjectHTMLContent;
        public Dictionary<string, object> Settings { get; set; }
        public DataAccess.Business.DataObject DataObject { get; set; }
        private Dictionary<Guid, PictureVersion> articlePictures;
        private bool isUserAdminOrOwner = false;

        protected override void OnInit(EventArgs e)
        {
            try
            {
                if (DataObject is Business.DataObjectHTMLContent)
                    dataObjectHTMLContent = (Business.DataObjectHTMLContent)DataObject;
                else
                    dataObjectHTMLContent = DataAccess.Business.DataObject.Load<Business.DataObjectHTMLContent>(DataObject.ObjectID, null, false);

                //Check the Container of the Detail
                if (Settings.ContainsKey("ParentObjectType"))
                {
                    int ParentObjectType = (int)Settings["ParentObjectType"];
                    if (ParentObjectType == Helper.GetObjectTypeNumericID("Page"))
                    {
                        isUserAdminOrOwner = UserDataContext.GetUserDataContext().IsAdmin;
                    }
                    else
                    {
                        Community comm = new Community((Guid)Settings["ParentCommunityID"]);
                        isUserAdminOrOwner = comm.IsUserOwner || UserDataContext.GetUserDataContext().IsAdmin;
                    }
                }

                if (dataObjectHTMLContent != null && dataObjectHTMLContent.State != ObjectState.Added)
                {
                    PrintOutput();
                }
            }
            catch
            {

            }
        }


        private void PrintOutput()
        {

            this.RTTM.Visible = false;
            this.RTTMIMG.Visible = false;

            DateTime dt = DateTime.Now;
            bool cntVisible = true;
            if (dt < dataObjectHTMLContent.StartDate || dt > dataObjectHTMLContent.EndDate || dataObjectHTMLContent.ShowState != ObjectShowState.Published)
            {
                cntVisible = false;
            }
            if (cntVisible || isUserAdminOrOwner)
            {
                foreach (string tooltipId in _4screen.CSB.DataAccess.Business.AdWordHelper.GetCampaignObjectIds(dataObjectHTMLContent.ObjectID.Value))
                {
                    this.RTTM.TargetControls.Add(tooltipId, true);
                    this.RTTM.Visible = true;
                }
                if (isUserAdminOrOwner)
                {
                    //Admin will see a special div
                    if (!cntVisible)
                        litDesc.Text = string.Format("<div class='CSB_CNT_public_invisible'>{0}</div>", dataObjectHTMLContent.DescriptionLinked);
                    else
                        litDesc.Text = string.Format("<div class='CSB_CNT_public_OK'>{0}</div>", dataObjectHTMLContent.DescriptionLinked);
                }
                else
                {
                    litDesc.Text = string.Format("{0}", dataObjectHTMLContent.DescriptionLinked);
                }

                LoadPictures();
            }
            Settings.Add("HasContent", cntVisible);
        }

        private void LoadPictures()
        {
            string idPrefix;
            articlePictures = _4screen.CSB.DataAccess.Business.Utils.GetPicturesFromContent(dataObjectHTMLContent.DescriptionLinked, out idPrefix);
            foreach (var pictureId in articlePictures.Keys)
            {
                string imageId = string.Format("Img_{0}", pictureId);
                this.RTTMIMG.TargetControls.Add(imageId, true);
                this.RTTMIMG.Visible = true;
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
            else if (e.TargetControlID.IndexOf("Img") > -1)
            {
                try
                {
                    Guid pictureId = e.TargetControlID.Substring(4).ToGuid();
                    DataObject picture = DataAccess.Business.DataObject.Load<DataObjectPicture>(pictureId);
                    Literal literal = new Literal();
                    literal.Text = string.Format("<div><div><img src=\"{0}{1}\"></div><div>{2}</div></div>", SiteConfig.MediaDomainName, picture.GetImage(articlePictures[pictureId]), picture.Title);
                    e.UpdatePanel.ContentTemplateContainer.Controls.Add(literal);
                }
                catch { }
            }
        }
    }
}