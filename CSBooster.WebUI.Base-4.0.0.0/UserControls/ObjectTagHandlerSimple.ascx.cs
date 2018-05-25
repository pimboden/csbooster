using System;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class ObjectTagHandlerSimple : System.Web.UI.UserControl, ITagHandler
    {
        private string currentTags = string.Empty;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            TxtTags.Text = currentTags;
        }

        public void SetTags(string tags)
        {
            currentTags = tags.ToLower().Trim(Constants.TAG_DELIMITER).Replace(Constants.TAG_DELIMITER, ',');
        }

        public string GetTags()
        {
            char[] separators = new char[] { ',', ';', '|', '¦' };
            string[] tagList = TxtTags.Text.ToLower().Trim(separators).Split(separators);

            string tags = string.Empty;
            for (int i = 0; i < tagList.Length; i++)
            {
                tagList[i] = tagList[i].Trim();
                tags += tagList[i];
                if (i < tagList.Length - 1)
                    tags += Constants.TAG_DELIMITER.ToString();
            }
            return tags;
        }
    }
}