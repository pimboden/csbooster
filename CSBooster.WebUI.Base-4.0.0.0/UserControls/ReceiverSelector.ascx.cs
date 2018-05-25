using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.WebUI.UserControls.Templates;
using _4screen.Utils.Web;
using SiteConfig = _4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class ReceiverSelector : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtUser.Attributes.Add("onkeyup", "ShowReceiverSuggestions(event, '" + btnUser.UniqueID + "')");
            txtUser.Attributes.Add("onkeydown", "AddEmailReceiver(event, '" + btnUser.UniqueID + "')");
        }

        protected void OnTextEnter(object sender, EventArgs e)
        {
            string userString = txtUser.Text;

            if (userString.Length > 0)
            {
                List<MessageReceiver> receivers = new List<MessageReceiver>();

                QuickParametersFriends parametersFriends = new QuickParametersFriends();
                parametersFriends.Udc = UserDataContext.GetUserDataContext();
                parametersFriends.CurrentUserID = UserProfile.Current.UserId;
                parametersFriends.OnlyNotBlocked = true;
                parametersFriends.Amount = 10000;
                parametersFriends.PageSize = 10000;
                parametersFriends.DisablePaging = true;
                parametersFriends.SortBy = QuickSort.Title;
                parametersFriends.Direction = QuickSortDirection.Asc;
                parametersFriends.ShowState = ObjectShowState.Published;
                List<DataObjectFriend> friends = DataObjects.Load<DataObjectFriend>(parametersFriends);
                receivers.AddRange(friends.FindAll(x => x.Nickname.ToLower().Contains(userString.ToLower()) ||
                                  (!string.IsNullOrEmpty(x.Vorname) && x.Vorname.ToLower().Contains(userString.ToLower())) ||
                                  (!string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains(userString.ToLower())))
                                  .Take(20).ToList().ConvertAll(x => new MessageReceiver() { User = x }));

                Dictionary<string, FriendType> friendTypes = new Dictionary<string, FriendType>();
                foreach (FriendType friendType in Enum.GetValues(typeof(FriendType)))
                {
                    if (friends.Exists(x => x.FriendType == friendType))
                    {
                        string text = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString(string.Format("TextFriendType{0}", (int)friendType));
                        friendTypes.Add(text, friendType);
                    }
                }
                receivers.AddRange(friendTypes.Where(x => x.Key.ToLower().Contains(userString.ToLower())).ToList().ConvertAll(x => new MessageReceiver() { FriendType = x.Value }));

                if (receivers.Count > 0)
                {
                    repSuggest.DataSource = receivers;
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
            MessageReceiver receiver = (MessageReceiver)e.Item.DataItem;

            HyperLink link = ((HyperLink)e.Item.FindControl("lnkSuggest"));

            if (receiver.User != null)
            {
                link.Attributes.Add("onClick", "SelectReceiver('" + receiver.User.UserID + "', '" + receiver.User.Nickname + "')");
                Control control = this.LoadControl("/UserControls/Templates/SmallOutputUser2.ascx");
                ((SmallOutputUser2)control).DataObjectUser = receiver.User;
                link.Controls.Add(control);
            }
            else if (receiver.FriendType.HasValue)
            {
                string text = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString(string.Format("TextFriendType{0}", (int)receiver.FriendType));
                link.Attributes.Add("onClick", "SelectReceiver('" + (int)receiver.FriendType + "', '" + text + "')");
                link.Controls.Add(new LiteralControl("<div class=\"receiverGroup\"></div><div class=\"receiverGroupName\">" + text + "</div>"));
            }

            link.ID = null;
        }
    }
}