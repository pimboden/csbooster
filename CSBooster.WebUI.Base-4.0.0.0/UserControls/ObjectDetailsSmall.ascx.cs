//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		30.08.2007 / TS
//  Updated:   #1.3.0.0    01.02.2008 / AW
//                         - New layout
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class ObjectDetailsSmall : System.Web.UI.UserControl
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");
        protected DataObject dataObject;

        public DataObject DataObject
        {
            get { return dataObject; }
            set { dataObject = value; }
        }

        public string GetContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}: {1} {2}<br/>", language.GetString("LableDetailCreateDate"), dataObject.Inserted.ToShortDateString(), dataObject.Inserted.ToShortTimeString());
            sb.AppendFormat("{0}: {1} {2}<br/>", language.GetString("LableDetailStartDate"), dataObject.StartDate.ToShortDateString(), dataObject.StartDate.ToShortTimeString());
            sb.AppendFormat("{0}: {1} {2}<br/><br/>", language.GetString("LableDetailEndDate"), dataObject.EndDate.ToShortDateString(), dataObject.EndDate.ToShortTimeString());

            if (dataObject.ViewCount == 0)
                sb.AppendFormat("{0}<br/>", language.GetString("LableDetailNoVisits"));
            else if (dataObject.ViewCount == 1)
                sb.AppendFormat("1 {0}<br/>", language.GetString("LableDetailVisit"));
            else
                sb.AppendFormat("{0} {1}<br/>", dataObject.ViewCount, language.GetString("LableDetailVisits"));

            if (dataObject.RatedCount == 0)
                sb.AppendFormat("{0}<br/>", language.GetString("LableDetailNoRatings"));
            else if (dataObject.RatedCount == 1)
                sb.AppendFormat("1 {0}<br/>", language.GetString("LableDetailRating"));
            else
                sb.AppendFormat("{0} {1}<br/>", dataObject.RatedCount, language.GetString("LableDetailRatings"));

            if (dataObject.CommentedCount == 0)
                sb.AppendFormat("{0}<br/>", language.GetString("LableDetailNoComments"));
            else if (dataObject.CommentedCount == 1)
                sb.AppendFormat("1 {0}<br/>", language.GetString("LableDetailComment"));
            else
                sb.AppendFormat("{0} {1}<br/>", dataObject.CommentedCount, language.GetString("LableDetailComments"));


            /*if (dataObject.NumCopies == 0)
               sb.AppendFormat("<a>Keine Verlinkungen</a><br/>");
            else if (dataObject.NumCopies == 1)
               sb.AppendFormat("<a>1 Verlinkung</a><br/>");
            else
               sb.AppendFormat("<a>{0} Verlinkungen</a><br/>", dataObject.NumCopies);*/

            sb.AppendFormat("<br/>");

            string tags = dataObject.TagList;
            string[] tagArray = tags.Split(Constants.TAG_DELIMITER);
            List<string> tagList = new List<string>();
            foreach (string tag in tagArray)
            {
                tag.TrimStart(new char[] { ' ' });
                tag.TrimEnd(new char[] { ' ' });
                if (!tagList.Contains(tag) && !string.IsNullOrEmpty(tag))
                    tagList.Add(tag);
            }

            sb.AppendFormat("<b>{0}</b><br/>", language.GetString("LableDetailTags"));
            sb.Append("<div style=\"width:200px;\">");
            for (int i = 0; i < tagList.Count; i++)
            {
                sb.AppendFormat("{0}", tagList[i]);
                if (i != tagList.Count - 1)
                    sb.Append(", ");
            }
            if (tagList.Count == 0)
            {
                sb.Append(language.GetString("LableDetailNone"));
            }
            sb.Append("</div>");

            //return Server.HtmlEncode(sb.ToString());
            return sb.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.LitCnt.Text = this.GetContent();
        }
    }
}