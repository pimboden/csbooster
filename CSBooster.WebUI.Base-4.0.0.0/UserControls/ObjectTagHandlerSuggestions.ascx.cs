using System;
using System.Text;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class ObjectTagHandlerSuggestions : System.Web.UI.UserControl, ITagHandler
    {
        public void SetTags(string tags)
        {
            string[] tagList = tags.ToLower().Split(Constants.TAG_DELIMITER);
            StringBuilder sb = new StringBuilder();
            foreach (string tag in tagList)
            {
                if (!string.IsNullOrEmpty(tag))
                    sb.Append("<div class=\"tagHint\"><span>" + tag + "</span><input type=\"hidden\" value=\"" + tag + "\" name=\"tagHint\"><a onclick=\"RemoveTag(this)\" href=\"javascript:void(0)\"></a></div>");
            }
            litTags.Text = sb.ToString();
        }

        public string GetTags()
        {
            if (Request.Form.GetValues("tagHint") != null)
                return string.Join(Constants.TAG_DELIMITER.ToString(), Request.Form.GetValues("tagHint"));
            else
                return string.Empty;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtTag.Attributes.Add("onkeyup", "ShowTagSuggestions(event, '" + btnTag.UniqueID + "')");
            txtTag.Attributes.Add("onkeydown", "AddTag(event, '" + btnTag.UniqueID + "')");
        }

        protected void OnTextEnter(object sender, EventArgs e)
        {
            string tag = txtTag.Text;

            if (tag.Length > 0)
            {
                QuickParameters quickParameters = new QuickParameters();
                quickParameters.Udc = UserDataContext.GetUserDataContext();
                quickParameters.Amount = 50;
                quickParameters.PageSize = 50;
                quickParameters.DisablePaging = true;
                quickParameters.Title = tag;
                quickParameters.SortBy = QuickSort.Title;
                quickParameters.Direction = QuickSortDirection.Asc;
                quickParameters.ShowState = ObjectShowState.Published;
                quickParameters.ObjectStatus = ObjectStatus.Public;
                var tags = DataObjects.Load<DataObjectTag>(quickParameters);

                if (tags.Count > 0)
                {
                    repSuggest.DataSource = tags;
                    repSuggest.DataBind();
                    pnlSuggest.Visible = true;
                }
                else
                {
                    pnlSuggest.Visible = false;
                }
            }
            else
            {
                pnlSuggest.Visible = false;
            }
        }

        protected void OnSuggestionItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataObjectTag tag = (DataObjectTag)e.Item.DataItem;
            HyperLink link = ((HyperLink)e.Item.FindControl("lnkSuggest"));
            link.Attributes.Add("onClick", "SelectTag('" + tag.Title + "', '" + tag.Title + "')");
            link.Text = tag.Title;
            link.ID = null;
        }
    }
}