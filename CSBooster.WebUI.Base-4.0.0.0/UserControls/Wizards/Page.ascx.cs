// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data;
using System.Linq;
using System.Transactions;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Wizards
{
    public partial class Page : StepsASCX
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        private UserDataContext udc = null;
        private DataObjectPage page = null;
        HitblCommunityCty community = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.OnInit(e);
            udc = UserDataContext.GetUserDataContext();
            page = DataObject.Load<DataObjectPage>(ObjectID, null, true);
            if (page.State == ObjectState.Added)
            {
                page = new DataObjectPage(udc);
                page.ParentObjectType = Helper.GetObjectTypeNumericID("Page");
                var roleRights = page.RoleRight.Keys.ToArray();
                foreach (var roleRight in roleRights)
                {
                    page.RoleRight[roleRight] = true;
                }
                page.ObjectID = ObjectID;
                page.CommunityID = ObjectID;
                page.Title = GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUnnamed");
                page.Status = ObjectStatus.Public;
                page.ShowState = ObjectShowState.InProgress;
                page.Insert(udc);
                page = DataObject.Load<DataObjectPage>(ObjectID, null, true);

                HitblCommunityCty objCommunity = new HitblCommunityCty();
                objCommunity.CtyId = page.ObjectID.Value;
                objCommunity.CtyInsertedDate = page.Inserted;
                objCommunity.CtyVirtualUrl = Guid.NewGuid().ToString();
                objCommunity.UsrIdInserted = page.UserID.Value;
                objCommunity.CtyIsProfile = false;
                objCommunity.CtyStatus = (int)CommunityStatus.Initializing;
                objCommunity.CtyLayout = _4screen.CSB.Common.CustomizationSection.CachedInstance.DefaultLayouts.Page;
                objCommunity.CtyTheme = Constants.DEFAULT_THEME;
                objCommunity.CtyUpdatedDate = page.Inserted;
                objCommunity.UsrIdUpdated = page.UserID;
                objCommunity.Save();
            }

            community = HitblCommunityCty.FetchByID(ObjectID.Value);
            if (community.CtyStatus != (int)CommunityStatus.Initializing)
            {
                txtCommName.ReadOnly = true;
            }
            else
            {
                page.Title = string.Empty;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["TG"]))
                page.TagList = Server.UrlDecode(Request.QueryString["TG"]);
            if (!string.IsNullOrEmpty(Request.QueryString["OS"]))
                page.Status = (ObjectStatus)int.Parse(Request.QueryString["OS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["SS"]))
                page.ShowState = (ObjectShowState)int.Parse(Request.QueryString["SS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CR"]))
                page.Copyright = int.Parse(Request.QueryString["CR"]);
            if (!string.IsNullOrEmpty(Request.QueryString["GC"]))
            {
                string[] geoLatLong = Request.QueryString["GC"].Split(',');
                double geoLat, geoLong = double.MinValue;
                if (geoLatLong.Length == 2)
                {
                    if (double.TryParse(geoLatLong[0], out geoLat) && double.TryParse(geoLatLong[1], out geoLong))
                    {
                        page.Geo_Lat = geoLat;
                        page.Geo_Long = geoLong;
                    }
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ZP"]))
                page.Zip = Server.UrlDecode(Request.QueryString["ZP"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CI"]))
                page.City = Server.UrlDecode(Request.QueryString["CI"]);
            if (!string.IsNullOrEmpty(Request.QueryString["RE"]))
                page.Street = Server.UrlDecode(Request.QueryString["RE"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CO"]))
                page.CountryCode = Server.UrlDecode(Request.QueryString["CO"]);

            InsertRoles();
            FillEditForm();
        }

        private void InsertRoles()
        {
            string selectedRoles = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["SR"]))
            {
                selectedRoles = Server.UrlDecode(Request.QueryString["SR"]);
            }
            if (!string.IsNullOrEmpty(selectedRoles))
            {
                string[] selRoles = selectedRoles.Split(Constants.TAG_DELIMITER);
                for (int i = 0; i < selRoles.Length; i++)
                {
                    page.RoleRight[selRoles[i]] = true;
                }
            }
        }

        private void FillEditForm()
        {
            litHostName.Text = _4screen.CSB.Common.SiteConfig.SiteURL + "/";
            this.Img.Src = string.Format("{0}/{1}", _4screen.CSB.Common.SiteConfig.MediaDomainName, page.GetImage(PictureVersion.S));
            this.LnkImage.Attributes.Add("onClick", string.Format("radWinOpen('/Pages/Popups/SinglePictureUpload.aspx?OID={0}&ParentRadWin={1}', '{2}', 400, 100, false, null, 'imageWin')", page.ObjectID, "wizardWin", language.GetString("TitlePictureUploadSingular").StripForScript()));

            txtCommName.Text = page.VirtualURL;
            RCBPageType.SelectedValue = ((int)page.PageType).ToString();
            RadEditor1.Content = page.Description;
            TxtTitle.Text = Server.HtmlDecode(page.Title);
            this.HFTagWords.Value = page.TagList.Replace(Constants.TAG_DELIMITER, ',');
            this.HFStatus.Value = ((int)page.Status).ToString();
            this.HFRoles.Value = Request.QueryString["SR"] ?? string.Empty;
            this.HFCopyright.Value = page.Copyright.ToString();
            if (page.Geo_Lat != double.MinValue && page.Geo_Long != double.MinValue)
            {
                this.HFGeoLat.Value = page.Geo_Lat.ToString();
                this.HFGeoLong.Value = page.Geo_Long.ToString();
            }
            this.HFZip.Value = page.Zip;
            this.HFCity.Value = page.City;
            this.HFStreet.Value = page.Street;
            this.HFCountry.Value = page.CountryCode;
        }

        public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            Page.Validate("WizardStep1");
            if (Page.IsValid)
            {
                page.Title = Common.Extensions.StripHTMLTags(this.TxtTitle.Text);
                page.VirtualURL = this.txtCommName.Text;
                page.PageType = (PageType)Enum.Parse(typeof(PageType), RCBPageType.SelectedValue);
                page.Description = this.RadEditor1.Content;
                page.TagList = Common.Extensions.StripHTMLTags(this.HFTagWords.Value);
                page.ShowState = ObjectShowState.Published;
                InsertRoles();
                page.Status = (ObjectStatus)Enum.Parse(typeof(ObjectStatus), this.HFStatus.Value);
                page.Copyright = int.Parse(this.HFCopyright.Value);
                double geoLat;
                if (double.TryParse(this.HFGeoLat.Value, out geoLat))
                    page.Geo_Lat = geoLat;
                double geoLong;
                if (double.TryParse(this.HFGeoLong.Value, out geoLong))
                    page.Geo_Long = geoLong;
                page.Zip = this.HFZip.Value;
                page.City = this.HFCity.Value;
                page.Street = this.HFStreet.Value;
                page.CountryCode = this.HFCountry.Value;
                community.CtyVirtualUrl = page.VirtualURL;

                using (TransactionScope scope = new TransactionScope())
                {
                    page.Update(UserDataContext.GetUserDataContext());

                    if (community.CtyStatus == (int)CommunityStatus.Initializing)
                    {
                        var thePage = PagesConfig.CreateNewPage(ObjectID.Value, "Page", "Start", "Home");
                        // TODO: Insert Default Widgets
                    }
                    community.CtyStatus = (int)CommunityStatus.Ready;
                    community.Save();
                    scope.Complete();
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        protected void txtCommName_TextChanged(object sender, EventArgs e)
        {
            IDataReader idr = null;
            string strCommName = txtCommName.Text;
            try
            {
                idr = HitblCommunityCty.FetchByParameter(HitblCommunityCty.Columns.CtyVirtualUrl, SubSonic.Comparison.Like, strCommName);
                if (idr.Read())
                {
                    lblNameExists.Visible = true;
                    txtCommName.Text = string.Empty;
                }
                else
                {
                    lblNameExists.Visible = false;
                }
            }
            catch
            {
            }
            finally
            {
                if (idr != null && !idr.IsClosed)
                    idr.Close();
            }

        }
    }
}