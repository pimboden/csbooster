// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class ObjectVoting : System.Web.UI.UserControl, IObjectVoting
    {
        protected DataObject dataObject;
        private UserDataContext udc = null;
        private bool showInfo;
        private GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");

        private UserDataContext Udc
        {
            get
            {
                if (udc == null)
                    udc = UserDataContext.GetUserDataContext();

                return udc;
            }
        }

        public DataObject DataObject
        {
            get { return dataObject; }
            set { dataObject = value; }
        }

        public bool ShowInfo
        {
            get { return showInfo; }
            set { showInfo = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (dataObject != null)
                DrawRating();
        }

        protected void DrawRating()
        {
            this.objVp.Controls.Clear();

            bool blnCanRat = false;
            bool blnOwner = (dataObject.UserID.Value == UserProfile.Current.UserId);
            if (!UserProfile.Current.IsAnonymous && !blnOwner)
            {
                blnCanRat = DataObject.IsRatingPossible(Udc, dataObject.ObjectID);
            }

            RatingConfig rConfig = DataAccessConfiguration.GetRatingConfig(RatingType.Standard);
            int ratingStep = (int)((float)(rConfig.MaxPoint - rConfig.MinPoint) / 6.0f + 0.5f);

            int l = 0;
            for (int i = 1; i <= 6; i++)
            {
                string ratingPicture = "rating_i";
                if (dataObject.RatedCount > 0)
                {
                    if (dataObject.RatedAverage > i)
                        ratingPicture = string.Format("rating_{0}", i);
                    else if (dataObject.RatedAverage == i)
                        ratingPicture = string.Format("rating_{0}", i);
                    else if (dataObject.RatedAverage > l)
                        ratingPicture = string.Format("rating_{0}.5", l);
                }
                l++;

                LinkButton ratingButton = new LinkButton();
                ratingButton = new LinkButton();
                ratingButton.ID = string.Concat("Lbtn", i);

                System.Web.UI.WebControls.Image image = new System.Web.UI.WebControls.Image() { ImageUrl = string.Format("/Library/Images/Layout/{0}.png", ratingPicture) };
                image.ID = string.Concat("RAT", i);
                ratingButton.Controls.Add(image);
                if (blnCanRat)
                {
                    ratingButton.ToolTip = string.Format(language.GetString("ToottipRatingValue"), i);
                    ratingButton.Click += new EventHandler(OnRatingClick);
                    ratingButton.CommandArgument = string.Format("{0}_{1}", dataObject.ObjectID, (ratingStep * i));
                    image.Attributes.Add("ssrc", image.ImageUrl);
                    image.Attributes.Add("nsrc", string.Format("/Library/Images/Layout/rating_{0}.png", i));
                    image.Attributes.Add("onMouseover", string.Format("ChangeRatingImg('{0}', {1});", this.ClientID, i));
                    image.Attributes.Add("onMouseout", string.Format("RestoreRatingImg('{0}', {1});", this.ClientID, i));
                }
                else
                {
                    ratingButton.Enabled = false;
                    if (blnOwner)
                        ratingButton.ToolTip = language.GetString("ToottipRatingMyOwn");
                    else if (UserProfile.Current.IsAnonymous)
                        ratingButton.ToolTip = language.GetString("ToottipRatingIsAnonymous");
                    else
                        ratingButton.ToolTip = language.GetString("ToottipRatingAllreadyVoted");
                }
                this.objVp.Controls.Add(ratingButton);
            }

            if (showInfo)
            {
                Literal literal = new Literal();
                if (dataObject.RatedCount == 0)
                    literal.Text = string.Format("<div style=\"padding-top:4px;\">{0}</div>", language.GetString("LabelRatingNotRated"));
                else if (dataObject.RatedCount == 1)
                    literal.Text = string.Format("<div style=\"padding-top:4px;\">1 {0}</div>", language.GetString("LabelRatingRatedSingular"));
                else
                    literal.Text = string.Format("<div style=\"padding-top:4px;\">{0} {1}</div>", dataObject.RatedCount, language.GetString("LabelRatingRatedPlural"));
                this.objVp.Controls.Add(literal);
            }
        }

        protected void OnRatingClick(object sender, EventArgs e)
        {
            string[] ratingInfo = ((LinkButton)sender).CommandArgument.Split(new char[] { '_' });
            if (ratingInfo.Length == 2)
            {
                int rating = int.Parse(ratingInfo[1]);

                DataObject.AddRating(Udc, ratingInfo[0].ToNullableGuid(), dataObject.ObjectType, rating);

                _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent(string.Format("{0}_RATE", Helper.GetObjectType(dataObject.ObjectType).Id.ToUpper()), Udc, ratingInfo[0]);
            }
            DrawRating();

            List<string> pageNames = new List<string>();
            pageNames.Add("RatingUpload");
            List<Dialog> dialogs = DialogEngine.GetDialogByPageName(pageNames, UserProfile.Current.UserId);
            if (dialogs.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Dialog dialog in dialogs)
                {
                    sb.AppendFormat("<div><b>{0}</b><br/>{1}</div>", dialog.Title, dialog.Content);
                    sb.AppendFormat("<div style=\"margin-top:10px;margin-bottom:10px;height:1px;background-color:#CCCCCC;\"></div>");
                }
                string content = Regex.Replace(sb.ToString(), "<(.*?)>", "&lt;$1&gt;"); // Ugly, but safari needs it
                ScriptManager.RegisterStartupScript((Control)this.objVp, this.objVp.GetType(), "DialogWin", "SetPopupWindow('" + this.ClientID + string.Format("', 700, 0, 200, '{0}', '", language.GetString("TitleRating").StripForScript()) + content + "', true);", true);
            }
        }
    }
}