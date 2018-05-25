using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.DataAccess.Data;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class ObjectTagHandlerSelector : System.Web.UI.UserControl, ITagHandler
    {
        private string currentTags = string.Empty;

        class MainTagInfo
        {
            public string Title { get; set; }
            public int SynonymCount { get; set; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            TxtTags.Attributes.Add("readonly", "readonly");

            TxtTags.Text = currentTags;

            List<string> tags = new List<string>();
            foreach (string tag in currentTags.Split(';'))
            {
                tags.Add(tag.ToLower());
            }

            Dictionary<MainTagInfo, List<string>> mainTagAndSynonyms = (Dictionary<MainTagInfo, List<string>>)HttpRuntime.Cache["MainTagsAndSynonyms"];
            if (mainTagAndSynonyms == null)
            {
                mainTagAndSynonyms = new Dictionary<MainTagInfo, List<string>>();
                //Get all words that have Synonym
                QuickParameters quickParSynonyms = new QuickParameters();
                quickParSynonyms.Udc = UserDataContext.GetUserDataContext();
                quickParSynonyms.ObjectType = 5;
                quickParSynonyms.IgnoreCache = true;
                quickParSynonyms.PageSize = 9999999;
                quickParSynonyms.DisablePaging = true;
                quickParSynonyms.SortBy = QuickSort.Title;
                quickParSynonyms.Direction = QuickSortDirection.Asc;
                quickParSynonyms.RelationParams = new RelationParams { ExcludeSystemObjects = false, RelationType = "Synonym", ParentObjectType = 5, GroupSort = QuickSort.Title, GroupSortDirection = QuickSortDirection.Asc };
                DataObjectList<DataObjectTag> tagWithSynonyms = DataObjects.Load<DataObjectTag>(quickParSynonyms);
                GroupByInfoComparer comp = new GroupByInfoComparer();
                IEnumerable<IGrouping<GroupByInfo, DataObjectTag>> outerSquence = tagWithSynonyms.GroupBy(x => x.GroupByInfo, comp);
                foreach (var keyGroupSequence in outerSquence)
                {
                    List<string> tagSynonymList = new List<string>();
                    tagSynonymList.Add(string.Format("{0}", keyGroupSequence.Key.Title));
                    foreach (DataObjectTag DataObjectTag in keyGroupSequence)
                    {
                        tagSynonymList.Add(DataObjectTag.Title);
                    }
                    mainTagAndSynonyms.Add(new MainTagInfo { SynonymCount = keyGroupSequence.Count(), Title = keyGroupSequence.Key.Title }, tagSynonymList);
                }
                CSBooster_DataContext cdc = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
                var tagList = cdc.hisp_DataObjectTag_GetAllButSynonyms();
                foreach (var tag in tagList)
                {
                    List<string> tagSynonymList = new List<string>();
                    tagSynonymList.Add(tag.OBJ_Title);
                    mainTagAndSynonyms.Add(new MainTagInfo { SynonymCount = 0, Title = tag.OBJ_Title }, tagSynonymList);
                }
                HttpRuntime.Cache.Insert("MainTagsAndSynonyms", mainTagAndSynonyms, null, DateTime.Now.Add(new TimeSpan(1, 0, 0)), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }
            var orderedTags = mainTagAndSynonyms.OrderBy(x => x.Key.Title);
            foreach (var mainTagAndSynonym in orderedTags)
            {
                var tagItem = new ListItem();
                tagItem.Text = mainTagAndSynonym.Key.Title;
                if (mainTagAndSynonym.Key.SynonymCount > 0)
                    tagItem.Text = string.Format("{0} (+{1})", tagItem.Text, mainTagAndSynonym.Key.SynonymCount);
                if (tags.Contains(mainTagAndSynonym.Key.Title.ToLower()))
                {
                    tagItem.Selected = true;
                    tagItem.Attributes.Add("class", "CSB_cb_selected");
                }
                else
                {
                    tagItem.Attributes.Add("class", "CSB_cb_unselected");
                }
                tagItem.Attributes.Add("onClick", string.Format("UpdateTagsTextbox(this, '{0}', '{1}')", this.TxtTags.ClientID, string.Join(";", mainTagAndSynonym.Value.ToArray()).Replace("'", @"\'")));
                CblTags.Items.Add(tagItem);
            }
        }

        public void SetTags(string tags)
        {
            currentTags = tags.ToLower().Trim(Constants.TAG_DELIMITER).Replace(Constants.TAG_DELIMITER, ';');
        }

        public string GetTags()
        {
            string[] tagList = TxtTags.Text.Split(new char[] { ';' });

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