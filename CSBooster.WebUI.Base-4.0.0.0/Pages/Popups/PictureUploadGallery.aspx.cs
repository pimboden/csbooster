using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class PictureUploadGallery : System.Web.UI.Page
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base");
        private Guid? communityId = null;
        private Guid? objectId = null;
        private PictureVersion returnPictureVersion = PictureVersion.S;

        protected void Page_Load(object sender, EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.WizardSpezial);

            foreach (ListItem item in this.ddlPicV.Items)
            {
                item.Text = language.GetString(string.Format("TextPicUploadSize{0}", item.Value));
            }

            foreach (ListItem item in this.ddlPicVPopup.Items)
            {
                item.Text = language.GetString(string.Format("TextPicUploadPopup{0}", item.Value));
            }


            string paramCtyId = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["CN"]))
            {
                paramCtyId = Request.QueryString["CN"];
                if (!paramCtyId.IsGuid())
                    communityId = DataObjectCommunity.GetCommunityIDByVirtualURL(paramCtyId);
                else
                    communityId = paramCtyId.ToGuid();
            }

            if (!string.IsNullOrEmpty(Request.QueryString["OID"]))
            {
                objectId = Request.QueryString["OID"].ToGuid();
                if (!communityId.HasValue)
                {
                    DataObject dataObject = DataObject.Load<DataObject>(objectId, null, false);
                    communityId = dataObject.CommunityID.Value;
                }
            }

            if (!string.IsNullOrEmpty(Request.QueryString["PV"]))
            {
                returnPictureVersion = (PictureVersion)Enum.Parse(typeof(PictureVersion), Request.QueryString["PV"]);
            }

            this.LnkImage.Attributes.Add("onClick", string.Format("radWinOpen('/Pages/Popups/SinglePictureUpload.aspx?OID={0}&TemplateID={1}&ParentRadWin={2}', '{3}', 400, 100, false, null, 'imageWin')", Guid.NewGuid().ToString(), objectId, "galleryWin", language.GetString("TitlePicUpload").StripForScript()));

            LoadPictures();
        }

        private void LoadPictures()
        {
            MyContent.ObjectType = Helper.GetObjectType("Picture").NumericId;
            MyContent.PageSize = 24;
            MyContent.Sort = CustomizationSection.CachedInstance.MyContent.DefaultSortOrder;
            MyContent.SortDirection = MyContent.Sort == QuickSort.Title ? QuickSortDirection.Asc : QuickSortDirection.Desc;
            MyContent.MyContentMode = MyContentMode.Selection;
            MyContent.Settings = new Dictionary<string, object>();
            MyContent.Settings.Add("SelectionCallbackPictureVersion", ddlPicV.ClientID);
            MyContent.Settings.Add("SelectionCallbackPopupVersion", ddlPicVPopup.ClientID);
            MyContent.Settings.Add("SelectionCallbackReturnPictureVersion", returnPictureVersion);

            MyContentSearch.MyContentMode = MyContentMode.Selection;
            MyContentSearch.MyContent = MyContent;
        }

        protected void OnObjectsItemDataBound(object sender, DataListItemEventArgs e)
        {
            DataObjectPicture picture = (DataObjectPicture)e.Item.DataItem;
            Image image = (Image)e.Item.FindControl("Img");
            image.ImageUrl = _4screen.CSB.Common.SiteConfig.MediaDomainName + picture.GetImage(PictureVersion.XS);
            image.Attributes.Add("OnClick", string.Format("CloseWindow('{0},{1}{2},'+document.getElementById('{3}').value+','+document.getElementById('{4}').value)", picture.ObjectID.Value, _4screen.CSB.Common.SiteConfig.MediaDomainName, picture.GetImage(returnPictureVersion), ddlPicV.ClientID, ddlPicVPopup.ClientID));
        }
    }
}