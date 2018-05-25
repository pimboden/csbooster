// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Wizards
{
    public partial class Article : StepsASCX
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base");
        private DataObjectArticle article;

        protected void Page_Load(object sender, EventArgs e)
        {
            article = DataObject.Load<DataObjectArticle>(ObjectID, null, true);
            if (article.State == ObjectState.Added)
            {
                article.ObjectID = ObjectID;
                article.Title = GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUnnamed");
                article.CommunityID = CommunityID;
                article.ShowState = ObjectShowState.Draft;
                article.Insert(UserDataContext.GetUserDataContext());
                article.Title = string.Empty;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["TG"]))
                article.TagList = Server.UrlDecode(Request.QueryString["TG"]);
            if (!string.IsNullOrEmpty(Request.QueryString["OS"]))
                article.Status = (ObjectStatus)int.Parse(Request.QueryString["OS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["SS"]))
                article.ShowState = (ObjectShowState)int.Parse(Request.QueryString["SS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CR"]))
                article.Copyright = int.Parse(Request.QueryString["CR"]);
            if (!string.IsNullOrEmpty(Request.QueryString["GC"]))
            {
                string[] geoLatLong = Request.QueryString["GC"].Split(',');
                double geoLat, geoLong = double.MinValue;
                if (geoLatLong.Length == 2)
                {
                    if (double.TryParse(geoLatLong[0], out geoLat) && double.TryParse(geoLatLong[1], out geoLong))
                    {
                        article.Geo_Lat = geoLat;
                        article.Geo_Long = geoLong;
                    }
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ZP"]))
                article.Zip = Server.UrlDecode(Request.QueryString["ZP"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CI"]))
                article.City = Server.UrlDecode(Request.QueryString["CI"]);
            if (!string.IsNullOrEmpty(Request.QueryString["RE"]))
                article.Street = Server.UrlDecode(Request.QueryString["RE"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CO"]))
                article.CountryCode = Server.UrlDecode(Request.QueryString["CO"]);

            FillEditForm();
        }

        private void FillEditForm()
        {
            //this.TxtArticle.ContextMenus.FindByTagName("IMG").Enabled = true;

            this.TxtTitle.Text = article.Title;
            this.TxtDesc.Text = article.Description;
            this.TxtArticle.Content = article.ArticleText;
            this.HFTagWords.Value = article.TagList.Replace(Constants.TAG_DELIMITER, ',');
            this.HFStatus.Value = ((int)article.Status).ToString();
            this.HFShowState.Value = ((int)article.ShowState).ToString();
            this.HFCopyright.Value = article.Copyright.ToString();
            if (article.Geo_Lat != double.MinValue && article.Geo_Long != double.MinValue)
            {
                this.HFGeoLat.Value = article.Geo_Lat.ToString();
                this.HFGeoLong.Value = article.Geo_Long.ToString();
            }
            this.HFZip.Value = article.Zip;
            this.HFCity.Value = article.City;
            this.HFStreet.Value = article.Street;
            this.HFCountry.Value = article.CountryCode;
        }

        public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            try
            {
                article.Title = Common.Extensions.StripHTMLTags(this.TxtTitle.Text);
                article.Description = Common.Extensions.StripHTMLTags(this.TxtDesc.Text).CropString(20000);
                article.ArticleText = this.TxtArticle.Content;
                _4screen.CSB.DataAccess.Business.Utils.SetPictureRelationsFromContent(TxtArticle.Content, article, "ArticlePictures", true);
                //SetRelationsFromContent(this.TxtArticle.Content);
                article.TagList = Common.Extensions.StripHTMLTags(this.HFTagWords.Value);
                article.Status = (ObjectStatus)Enum.Parse(typeof(ObjectStatus), this.HFStatus.Value);
                article.ShowState = (ObjectShowState)Enum.Parse(typeof(ObjectShowState), this.HFShowState.Value);
                article.Copyright = int.Parse(this.HFCopyright.Value);
                double geoLat;
                if (double.TryParse(this.HFGeoLat.Value, out geoLat))
                    article.Geo_Lat = geoLat;
                double geoLong;
                if (double.TryParse(this.HFGeoLong.Value, out geoLong))
                    article.Geo_Long = geoLong;
                article.Zip = this.HFZip.Value;
                article.City = this.HFCity.Value;
                article.Street = this.HFStreet.Value;
                article.CountryCode = this.HFCountry.Value;

                article.Update(UserDataContext.GetUserDataContext());

                return true;
            }
            catch (Exception ex)
            {
                this.LitMsg.Text = string.Format("{0}: ", language.GetString("MessageSaveError")) + ex.Message;
                return false;
            }
        }

        /*private void SetRelationsFromContent(string content)
        {
            DataObject.RelDelete(new RelationParams() { ParentObjectID = article.ObjectID, ParentObjectType = Helper.GetObjectTypeNumericID("Article"), ChildObjectType = Helper.GetObjectTypeNumericID("Picture") });

            MatchCollection images = Regex.Matches(content, @"<img(.*?)>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            int insertNumber = 0;
            foreach (Match match in images)
            {
                Match idMatch = Regex.Match(match.Groups[1].Value, @"id=""Img_(.*?)""", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                Match pvMatch = Regex.Match(match.Groups[1].Value, @"pv=""(.*?)""", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                Match pvpMatch = Regex.Match(match.Groups[1].Value, @"pvp=""(.*?)""", RegexOptions.IgnoreCase | RegexOptions.Multiline);

                Guid objectId = idMatch.Groups[1].Value.ToGuid();
                PictureVersion pictureVersion = (PictureVersion)Enum.Parse(typeof(PictureVersion), pvMatch.Groups[1].Value);
                string popupVersion = pvpMatch.Groups[1].Value;
                string relationType = !string.IsNullOrEmpty(popupVersion) ? "PopupImage" : "Image";

                DataObject.RelInsert(new RelationParams()
                                         {
                                             ParentObjectID = article.ObjectID,
                                             ParentObjectType = Helper.GetObjectTypeNumericID("Article"),
                                             ChildObjectID = objectId,
                                             ChildObjectType = Helper.GetObjectTypeNumericID("Picture"),
                                             RelationType = relationType
                                         }, insertNumber++);
            }

            DataObjectList<DataObjectPicture> pictures = DataObjects.Load<DataObjectPicture>(new QuickParameters
                                                                                                 {
                                                                                                     Udc = UserDataContext.GetUserDataContext(),
                                                                                                     ShowState = null,
                                                                                                     RelationParams = new RelationParams
                                                                                                     {
                                                                                                         ParentObjectID = article.ObjectID.Value
                                                                                                     }
                                                                                                 }
                );
            if (pictures.Count > 0)
            {
                string mediaSource = string.Format(@"{0}\{1}\P\{{0}}\{2}.jpg", ConfigurationManager.AppSettings["ConverterRootPathMedia"], pictures[0].UserID, pictures[0].ObjectID);
                string mediaTarget = string.Format(@"{0}\{1}\P\{{0}}\{2}.jpg", ConfigurationManager.AppSettings["ConverterRootPathMedia"], article.UserID, article.ObjectID);

                foreach (var pictureFormat in pictures[0].PictureFormats)
                {
                    if (!string.IsNullOrEmpty(pictureFormat.Value))
                    {
                        article.SetImageType(pictureFormat.Key, (PictureFormat)Enum.Parse(typeof(PictureFormat), pictureFormat.Value));
                        File.Copy(string.Format(mediaSource, pictureFormat.Key), string.Format(mediaTarget, pictureFormat.Key), true);
                    }
                }

                article.Image = article.ObjectID.Value.ToString();
            }
        }*/
    }
}
