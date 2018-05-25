// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Wizards
{
    public partial class Community : StepsASCX
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        private UserDataContext udc = null;
        private DataObjectCommunity currDatatObjectCommunity = null;
        HitblCommunityCty currComm = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.OnInit(e);
            
            udc = UserDataContext.GetUserDataContext();
            currDatatObjectCommunity = DataObject.Load<DataObjectCommunity>(ObjectID, null, true);

            if (currDatatObjectCommunity.State == ObjectState.Added)
            {
                currDatatObjectCommunity = new DataObjectCommunity(udc);
                currDatatObjectCommunity.ObjectID = ObjectID;
                currDatatObjectCommunity.CommunityID = ObjectID;
                currDatatObjectCommunity.Title = GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUnnamed");
                currDatatObjectCommunity.Status = ObjectStatus.Public;
                currDatatObjectCommunity.ShowState = ObjectShowState.InProgress;
                if (CommunityID.HasValue)
                {
                    //It's a Group (SubCommunity)
                    currDatatObjectCommunity.ParentObjectID = CommunityID;
                    currDatatObjectCommunity.ParentObjectType = Helper.GetObjectTypeNumericID("Community");
                }
                currDatatObjectCommunity.Insert(udc);
                currDatatObjectCommunity = DataObject.Load<DataObjectCommunity>(ObjectID, null, true);

                HitblCommunityCty objCommunity = new HitblCommunityCty();
                objCommunity.CtyId = currDatatObjectCommunity.ObjectID.Value;
                objCommunity.CtyInsertedDate = currDatatObjectCommunity.Inserted;
                objCommunity.CtyVirtualUrl = Guid.NewGuid().ToString();
                objCommunity.UsrIdInserted = currDatatObjectCommunity.UserID;
                objCommunity.CtyIsProfile = false;
                objCommunity.CtyStatus = (int)CommunityStatus.Initializing;
                objCommunity.CtyLayout = _4screen.CSB.Common.CustomizationSection.CachedInstance.DefaultLayouts.Community;
                objCommunity.CtyTheme = Constants.DEFAULT_THEME;
                objCommunity.CtyUpdatedDate = currDatatObjectCommunity.Inserted;
                objCommunity.UsrIdUpdated = currDatatObjectCommunity.UserID;
                objCommunity.Save();
            }

            currComm = HitblCommunityCty.FetchByID(ObjectID);
            if (currComm.CtyStatus != (int)CommunityStatus.Initializing)
            {
                //its an edit inserted!
                txtCommName.ReadOnly = true;
            }
            else
            {
                currDatatObjectCommunity.Title = string.Empty;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["TG"]))
                currDatatObjectCommunity.TagList = Server.UrlDecode(Request.QueryString["TG"]);
            if (!string.IsNullOrEmpty(Request.QueryString["OS"]))
                currDatatObjectCommunity.Status = (ObjectStatus)int.Parse(Request.QueryString["OS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["SS"]))
                currDatatObjectCommunity.ShowState = (ObjectShowState)int.Parse(Request.QueryString["SS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CR"]))
                currDatatObjectCommunity.Copyright = int.Parse(Request.QueryString["CR"]);
            if (!string.IsNullOrEmpty(Request.QueryString["GC"]))
            {
                string[] geoLatLong = Request.QueryString["GC"].Split(',');
                double geoLat, geoLong = double.MinValue;
                if (geoLatLong.Length == 2)
                {
                    if (double.TryParse(geoLatLong[0], out geoLat) && double.TryParse(geoLatLong[1], out geoLong))
                    {
                        currDatatObjectCommunity.Geo_Lat = geoLat;
                        currDatatObjectCommunity.Geo_Long = geoLong;
                    }
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ZP"]))
                currDatatObjectCommunity.Zip = Server.UrlDecode(Request.QueryString["ZP"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CI"]))
                currDatatObjectCommunity.City = Server.UrlDecode(Request.QueryString["CI"]);
            if (!string.IsNullOrEmpty(Request.QueryString["RE"]))
                currDatatObjectCommunity.Street = Server.UrlDecode(Request.QueryString["RE"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CO"]))
                currDatatObjectCommunity.CountryCode = Server.UrlDecode(Request.QueryString["CO"]);

            FillEditForm();
        }

        private void FillEditForm()
        {
            litHostName.Text = _4screen.CSB.Common.SiteConfig.SiteURL + "/";
            this.Img.Src = string.Format("{0}/{1}", _4screen.CSB.Common.SiteConfig.MediaDomainName, currDatatObjectCommunity.GetImage(PictureVersion.S));
            this.LnkImage.Attributes.Add("onClick", string.Format("radWinOpen('/Pages/Popups/SinglePictureUpload.aspx?OID={0}&ParentRadWin={1}', 'Bild hochladen', 400, 100, false, null, 'imageWin')", currDatatObjectCommunity.ObjectID, "wizardWin"));

            txtCommName.Text = currDatatObjectCommunity.VirtualURL;
            RadEditor1.Content = currDatatObjectCommunity.Description;
            TxtTitle.Text = Server.HtmlDecode(currDatatObjectCommunity.Title);
            this.HFTagWords.Value = currDatatObjectCommunity.TagList.Replace(Constants.TAG_DELIMITER, ',');
            this.HFStatus.Value = ((int)currDatatObjectCommunity.Status).ToString();
            this.HFManaged.Value = Convert.ToInt32(currDatatObjectCommunity.Managed).ToString();
            this.HFCopyright.Value = currDatatObjectCommunity.Copyright.ToString();
            if (currDatatObjectCommunity.Geo_Lat != double.MinValue && currDatatObjectCommunity.Geo_Long != double.MinValue)
            {
                this.HFGeoLat.Value = currDatatObjectCommunity.Geo_Lat.ToString();
                this.HFGeoLong.Value = currDatatObjectCommunity.Geo_Long.ToString();
            }
            this.HFZip.Value = currDatatObjectCommunity.Zip;
            this.HFCity.Value = currDatatObjectCommunity.City;
            this.HFStreet.Value = currDatatObjectCommunity.Street;
            this.HFCountry.Value = currDatatObjectCommunity.CountryCode;
        }

        public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            if (Page.IsValid)
            {
                currDatatObjectCommunity.Title = Common.Extensions.StripHTMLTags(this.TxtTitle.Text);
                currDatatObjectCommunity.VirtualURL = this.txtCommName.Text;
                currDatatObjectCommunity.Description = this.RadEditor1.Content;
                currDatatObjectCommunity.TagList = Common.Extensions.StripHTMLTags(this.HFTagWords.Value);
                currDatatObjectCommunity.ShowState = ObjectShowState.Published;
                currDatatObjectCommunity.Managed = Convert.ToBoolean(Convert.ToInt32((string)this.HFManaged.Value));
                currDatatObjectCommunity.Status = (ObjectStatus)Enum.Parse(typeof(ObjectStatus), this.HFStatus.Value);
                currDatatObjectCommunity.Copyright = int.Parse(this.HFCopyright.Value);
                double geoLat;
                if (double.TryParse(this.HFGeoLat.Value, out geoLat))
                    currDatatObjectCommunity.Geo_Lat = geoLat;
                double geoLong;
                if (double.TryParse(this.HFGeoLong.Value, out geoLong))
                    currDatatObjectCommunity.Geo_Long = geoLong;
                currDatatObjectCommunity.Zip = this.HFZip.Value;
                currDatatObjectCommunity.City = this.HFCity.Value;
                currDatatObjectCommunity.Street = this.HFStreet.Value;
                currDatatObjectCommunity.CountryCode = this.HFCountry.Value;
                currComm.CtyVirtualUrl = currDatatObjectCommunity.VirtualURL;

                using (TransactionScope scope = new TransactionScope())
                {
                    currDatatObjectCommunity.Update(UserDataContext.GetUserDataContext());

                    if (currComm.CtyStatus == (int)CommunityStatus.Initializing)
                    {
                        var privatePage = PagesConfig.CreateNewPage(ObjectID.Value, "Community", "Private", "Dashboard");
                        var thePage = PagesConfig.CreateNewPage(ObjectID.Value, "Community", "Start", "Home");
                        HirelCommunityUserCur.Insert(currComm.CtyId, udc.UserID, true, 0, DateTime.Now, Guid.Empty);
                        SPs.HispDataObjectAddMemberCount(currComm.CtyId, 1).Execute();
                        try
                        {
                            if (udc.UserID != Constants.ADMIN_USERID.ToGuid())
                            {
                                HirelCommunityUserCur.Insert(currComm.CtyId, new Guid(Constants.ADMIN_USERID), true, 0, DateTime.Now, Guid.Empty);
                                SPs.HispDataObjectAddMemberCount(currComm.CtyId, 1).Execute();
                            }
                        }
                        catch
                        {
                        }
                    }
                    currComm.CtyStatus = (int)CommunityStatus.Ready;
                    currComm.Save();
                    _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent("COMMUNITY_UPLOAD", udc, ObjectID.Value.ToString());
                    scope.Complete();
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        protected void ValidatePage(object sender, EventArgs e)
        {
            Page.Validate();
        }

        protected void ValidateCommunityName(object sender, ServerValidateEventArgs e)
        {
            if (this.AccessMode == Common.AccessMode.Insert)
            {
                IDataReader idr = null;
                try
                {
                    idr = HitblCommunityCty.FetchByParameter(HitblCommunityCty.Columns.CtyVirtualUrl, SubSonic.Comparison.Like, e.Value);
                    if (idr.Read())
                    {
                        e.IsValid = false;
                        ScriptManager.RegisterStartupScript(UpnlCommunity, UpnlCommunity.GetType(), "SetFocus", String.Format("document.getElementById('{0}_text').focus();", this.txtCommName.ClientID), true);
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
            else
            {
                e.IsValid = true;
            }
        }
    }
}
