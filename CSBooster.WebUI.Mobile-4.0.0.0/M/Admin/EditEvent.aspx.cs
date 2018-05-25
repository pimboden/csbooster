using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.M.Admin
{
    public partial class EditEvent : System.Web.UI.Page
    {
        private bool objectExisting = true;
        private DataObjectEvent dataObjectEvent;
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Mobile");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DdlLocations.DataTextField = "Title";
            DdlLocations.DataValueField = "ObjectID";
            DdlLocations.DataSource = DataObjects.Load<DataObjectLocation>(new QuickParameters()
            {
                Udc = UserDataContext.GetUserDataContext(),
                SortBy = QuickSort.Title,
                Direction = QuickSortDirection.Asc,
                DisablePaging = true,
                IgnoreCache = true
            });
            DdlLocations.DataBind();

            foreach (string i in Enum.GetNames(typeof(EventType)))
            {
                string key = string.Format("Text{0}", i);
                string text = languageShared.GetString(key);
                if (!string.IsNullOrEmpty(text))
                {
                    ListItem item = new ListItem(text, i);
                    CblType.Items.Add(item);
                }
            }

            dataObjectEvent = DataObject.Load<DataObjectEvent>(Request.QueryString["OID"].ToNullableGuid());
            if (dataObjectEvent.State == ObjectState.Added)
            {
                LitTitle.Text = language.GetString("LabelAddEvent");
                lbtnSave.Text = languageShared.GetString("CommandCreate");
                objectExisting = false;
                dataObjectEvent.ObjectID = Guid.NewGuid();
                dataObjectEvent.CommunityID = UserProfile.Current.ProfileCommunityID;
                dataObjectEvent.ShowState = ObjectShowState.Published;
                dataObjectEvent.Status = ObjectStatus.Public;
            }
            else
            {
                LitTitle.Text = language.GetString("LabelEditEvent");
                lbtnSave.Text = languageShared.GetString("CommandSave");
                TxtTitle.Text = dataObjectEvent.Title;
                TxtDesc.Text = dataObjectEvent.Description;
                if (!string.IsNullOrEmpty(dataObjectEvent.Content))
                    TxtText.Text = dataObjectEvent.Content.Replace("<br/>", Environment.NewLine);
                TxtStartDate.Text = dataObjectEvent.StartDate.ToString("dd.MM.yyyy");
                TxtEndDate.Text = dataObjectEvent.EndDate.ToString("dd.MM.yyyy");
                TxtTime.Text = dataObjectEvent.Time;
                TxtAge.Text = dataObjectEvent.Age;
                TxtPrice.Text = dataObjectEvent.Price;

                DataObjectLocation currentLocation = DataObjects.Load<DataObjectLocation>(new QuickParameters()
                {
                    Udc = UserDataContext.GetUserDataContext(),
                    DisablePaging = true,
                    IgnoreCache = true,
                    RelationParams = new RelationParams()
                    {
                        ChildObjectID = dataObjectEvent.ObjectID
                    }
                }).SingleOrDefault();
                if (currentLocation != null)
                    DdlLocations.SelectedValue = currentLocation.ObjectID.ToString();

                foreach (ListItem item in CblType.Items)
                    item.Selected = dataObjectEvent.TagList.Contains(item.Value);
            }
        }

        private bool Save()
        {
            try
            {
                List<string> tags = new List<string>();
                foreach (ListItem item in CblType.Items)
                    if (item.Selected)
                        tags.Add(item.Value);
                DateTime? startDate = null;
                DateTime? endDate = null;
                try { startDate = DateTime.Parse(TxtStartDate.Text); }
                catch { }
                try { endDate = DateTime.Parse(TxtEndDate.Text); }
                catch { }

                if (string.IsNullOrEmpty(TxtTitle.Text.StripHTMLTags()))
                {
                    pnlStatus.Visible = true;
                    litStatus.Text = language.GetString("MessageTitleRequired");
                }
                else if (tags.Count == 0)
                {
                    pnlStatus.Visible = true;
                    litStatus.Text = language.GetString("MessageLocationTypeRequired");
                }
                else if (!startDate.HasValue)
                {
                    pnlStatus.Visible = true;
                    litStatus.Text = language.GetString("MessageEventDateRequired");
                }
                else if (endDate.HasValue && startDate.Value > endDate.Value)
                {
                    pnlStatus.Visible = true;
                    litStatus.Text = language.GetString("MessageStartDateAfterEndDate");
                }
                else
                {
                    dataObjectEvent.Title = Common.Extensions.StripHTMLTags(this.TxtTitle.Text);
                    dataObjectEvent.Description = Common.Extensions.StripHTMLTags(this.TxtDesc.Text).CropString(20000);
                    dataObjectEvent.Content =
                        TxtText.Text.StripHTMLTags().Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "<br/>");
                    dataObjectEvent.Copyright = 0;
                    dataObjectEvent.StartDate = startDate.Value;
                    dataObjectEvent.EndDate = endDate.HasValue ? endDate.Value : startDate.Value;
                    if (dataObjectEvent.StartDate == dataObjectEvent.EndDate)
                        dataObjectEvent.Featured = 1;
                    else
                        dataObjectEvent.Featured = 2;
                    dataObjectEvent.Time = Common.Extensions.StripHTMLTags(this.TxtTime.Text);
                    dataObjectEvent.Price = Common.Extensions.StripHTMLTags(this.TxtPrice.Text);
                    dataObjectEvent.Age = Common.Extensions.StripHTMLTags(this.TxtAge.Text);
                    dataObjectEvent.TagList = string.Join(",", tags.ToArray());
                    dataObjectEvent.Geo_Lat = double.MinValue;
                    dataObjectEvent.Geo_Long = double.MinValue;

                    if (objectExisting)
                        dataObjectEvent.Update(UserDataContext.GetUserDataContext());
                    else
                        dataObjectEvent.Insert(UserDataContext.GetUserDataContext());

                    if (DdlLocations.SelectedValue.IsGuid())
                    {
                        DataObjectLocation location =
                            DataObject.Load<DataObjectLocation>(DdlLocations.SelectedValue.ToGuid());
                        if (location.Geo_Lat != Double.MinValue && location.Geo_Long != Double.MinValue)
                        {
                            dataObjectEvent.Geo_Lat = location.Geo_Lat;
                            dataObjectEvent.Geo_Long = location.Geo_Long;
                        }
                        DataObject.RelDelete(new RelationParams()
                        {
                            Udc = UserDataContext.GetUserDataContext(),
                            ParentObjectType = location.ObjectType,
                            ChildObjectID = dataObjectEvent.ObjectID,
                            ChildObjectType = dataObjectEvent.ObjectType
                        });
                        DataObject.RelInsert(new RelationParams()
                        {
                            Udc = UserDataContext.GetUserDataContext(),
                            ParentObjectID = location.ObjectID,
                            ParentObjectType = location.ObjectType,
                            ChildObjectID = dataObjectEvent.ObjectID,
                            ChildObjectType = dataObjectEvent.ObjectType
                        }, 0);
                    }
                    dataObjectEvent.Update(UserDataContext.GetUserDataContext());

                    return true;
                }
            }
            catch (Exception e)
            {
                pnlStatus.Visible = true;
                litStatus.Text = "Event konnte nicht gespeichert werden: " + e.Message;
            }
            return false;
        }

        protected void OnSaveClick(object sender, EventArgs e)
        {
            if (Save())
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ReturnURL"]))
                    Response.Redirect(System.Text.ASCIIEncoding.ASCII.GetString(System.Convert.FromBase64String(Request.QueryString["ReturnURL"])));
                else
                    Response.Redirect(Helper.GetMobileDetailLink(dataObjectEvent.ObjectType, dataObjectEvent.ObjectID.ToString()));
            }
        }

        protected void OnAddLocationClick(object sender, EventArgs e)
        {
            if (Save())
                Response.Redirect("/M/Admin/EditLocation.aspx?ReturnUrl=" + System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(Request.RawUrl)));
        }
    }
}
