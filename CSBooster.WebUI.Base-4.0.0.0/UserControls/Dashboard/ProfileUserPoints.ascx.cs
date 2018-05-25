// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.WebUI.Code;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class ProfileUserPoints : ProfileQuestionsControl
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");

        protected void Page_Load(object sender, EventArgs e)
        {
            IDataReader idr = SPs.HispIncentivePointsGetPointsWithColors(UserProfile.Current.UserId).GetReader();
            if (idr.Read())
            {
                lblGreenPoints.Text = idr["GreenPoints"] != DBNull.Value ? idr["GreenPoints"].ToString() : "0";
                lblRedPoints.Text = idr["RedPoints"] != DBNull.Value ? idr["RedPoints"].ToString() : "0";
                lblTotalPoints.Text = idr["TotalPoints"] != DBNull.Value ? idr["TotalPoints"].ToString() : "0";
            }
        }
    }
}