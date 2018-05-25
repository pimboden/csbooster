//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#2.0.0.0		04.02.2009 / AW
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Linq;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public partial class SearchResults : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(settingsXml);

            var searchableObjectTypes = SiteObjectSection.CachedInstance.SiteObjects.LINQEnumarable.Where(x => x.IsSearchable);

            Dictionary<int, int> numberItems = new Dictionary<int, int>();

            foreach (var objectType in searchableObjectTypes)
            {
                QuickParameters quickParameters = new QuickParameters();
                quickParameters.FromNameValueCollection(Request.QueryString);
                quickParameters.Udc = UserDataContext.GetUserDataContext();
                quickParameters.ObjectType = objectType.NumericId;
                quickParameters.PageSize = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "NumberResults", 5);
                quickParameters.Amount = objectType.DefaultLoadAmount;
                quickParameters.SortBy = QuickSort.Title;
                quickParameters.Direction = QuickSortDirection.Asc;
                quickParameters.ShowState = ObjectShowState.Published;
                quickParameters.DateQueryMethode = QuickDateQueryMethode.BetweenStartOpenEndOpen;
                quickParameters.FromStartDate = DateTime.Now;
                quickParameters.ToEndDate = DateTime.Now;

                Control control = LoadControl("~/UserControls/Repeaters/SearchResults.ascx");
                control.ID = "SRB" + objectType.NumericId;


                IRepeater overview = control as IRepeater;
                if (overview != null)
                {
                    overview.QuickParameters = quickParameters;
                    overview.OutputTemplate = ""; // template;
                    overview.TopPagerVisible = false;
                    overview.BottomPagerVisible = true;
                }

                Ph.Controls.Add(control);
                if (((IBrowsable)control).GetNumberItems() > 0)
                    numberItems.Add(objectType.NumericId, ((IBrowsable)control).GetNumberItems());
            }

            DlRes.DataSource = numberItems;
            DlRes.DataBind();

            if (numberItems.Count == 0)
                Ph.Controls.Add(new LiteralControl(GuiLanguage.GetGuiLanguage("WidgetSearchResults").GetString("MessageNothingFound")));

            return true;
        }

        protected void OnResultItemDataBound(object sender, DataListItemEventArgs e)
        {
            KeyValuePair<int, int> numberItems = (KeyValuePair<int, int>)e.Item.DataItem;
            Literal gotoLink = (Literal)e.Item.FindControl("LitGoTo");

            gotoLink.Text = string.Format("<a href=\"#Results{0}\">{1} ({2})</a>", numberItems.Key, Helper.GetObjectName(numberItems.Key, false), numberItems.Value);
        }
    }
}
