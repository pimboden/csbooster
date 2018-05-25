using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class ObjectVotingTelerik : System.Web.UI.UserControl, IObjectVoting
    {
        private GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");
        private RatingConfig ratingConfig;

        public DataObject DataObject { get; set; }
        public bool ShowInfo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (DataObject != null)
            {
                ratingConfig = DataAccessConfiguration.GetRatingConfig(RatingType.Standard);
                for (int i = ratingConfig.MinPoint; i <= ratingConfig.MaxPoint; i++)
                    Rr.Items.Add(new RadRatingItem() { Value = i, ToolTip = string.Format(language.GetString("ToottipRatingValue"), i) });
                Rr.Value = DataObject.RatedAverage;
                DrawRating();
            }
        }

        private void DrawRating()
        {
            bool isOwner = (DataObject.UserID.Value == UserProfile.Current.UserId);
            if (DataObject.IsRatingPossible(UserDataContext.GetUserDataContext(), DataObject.ObjectID) && !isOwner && (!UserProfile.Current.IsAnonymous || ratingConfig.AllowAnonymous))
            {
                Rr.AutoPostBack = true;
                Rr.Rate += new EventHandler(OnRatingClick);
            }
            else
            {
                Rr.ReadOnly = true;
                if (isOwner)
                    Rr.ToolTip = language.GetString("ToottipRatingMyOwn");
                else if (UserProfile.Current.IsAnonymous && !ratingConfig.AllowAnonymous)
                    Rr.ToolTip = language.GetString("ToottipRatingIsAnonymous");
                else
                    Rr.ToolTip = language.GetString("ToottipRatingAllreadyVoted");
            }
        }

        public void OnRatingClick(object sender, EventArgs e)
        {
            DataObject.AddRating(UserDataContext.GetUserDataContext(), DataObject.ObjectID, DataObject.ObjectType, (int)Math.Round(Rr.Value));
            Extensions.Business.IncentivePointsManager.AddIncentivePointEvent(string.Format("{0}_RATE", Helper.GetObjectType(DataObject.ObjectType).Id.ToUpper()), UserDataContext.GetUserDataContext(), DataObject.ObjectID.ToString());
            DrawRating();
        }
    }
}