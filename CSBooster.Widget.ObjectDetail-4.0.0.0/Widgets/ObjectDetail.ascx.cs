// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget
{
    public partial class ObjectDetail : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(settingsXml);
            bool showEdit = true;

            bool byUrl = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ByUrl", false);
            string objectType = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ObjectType", "0");
            Guid? objectId = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ObjectID", string.Empty).ToNullableGuid();
            int source = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Source", 8);
            bool showPager = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowPager", false);

            if (objectType == "0")
            {
                byUrl = true;
                if (string.IsNullOrEmpty(Request.QueryString["OT"]) || string.IsNullOrEmpty(Request.QueryString["OID"]))
                    return false;
                else
                {
                    objectType = Request.QueryString["OT"];
                    objectId = Request.QueryString["OID"].ToGuid();
                }
            }

            DataObject dataObject = null;

            if (byUrl)
            {
                if (!objectId.HasValue && string.IsNullOrEmpty(Request.QueryString["OID"]))
                    return false;

                if (!string.IsNullOrEmpty(Request.QueryString["OT"]) && Helper.GetObjectTypeNumericID(Request.QueryString["OT"]) != Helper.GetObjectTypeNumericID(objectType))
                    return false;

                objectId = Request.QueryString["OID"].ToGuid();
                dataObject = DataObject.LoadByReflection(objectId.Value, Helper.GetObjectTypeNumericID(objectType));
            }
            else if (source > -1)
            {
                dataObject = LoadBySource(Helper.GetObjectTypeNumericID(objectType), source);
                showEdit = false;
            }
            else if (objectId.HasValue)
            {
                dataObject = DataObject.LoadByReflection(objectId.Value, Helper.GetObjectTypeNumericID(objectType), null);
            }
            else
            {
                return false;
            }

            if (dataObject == null)
            {
                return false;
            }

            if (dataObject.State == ObjectState.Added)
            {
                return false;
            }
            else if (dataObject.State == ObjectState.Deleted)
            {
                this.PhDet.Controls.Add(new LiteralControl(GuiLanguage.GetGuiLanguage("WidgetObjectDetail").GetString("MessageDeleted")));
                return true;
            }

            if (showEdit)
            {
                Control controlEP = LoadControl("~/UserControls/CntEdit.ascx");
                ISettings ISettEP = controlEP as ISettings;
                ISettEP.Settings = new System.Collections.Generic.Dictionary<string, object>();
                ISettEP.Settings.Add("ObjectID", dataObject.ObjectID);
                PhEP.Controls.Add(controlEP);
            }

            string template = "ObjectDetail.ascx";
            if (WidgetHost.OutputTemplate != null)
            {
                if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.OutputTemplateControl))
                    template = WidgetHost.OutputTemplate.OutputTemplateControl;
            }

            try
            {
                Control control = LoadControl("~/UserControls/Templates/" + template);

                IDataObjectWorker dataObjectWorker = control as IDataObjectWorker;
                dataObjectWorker.DataObject = dataObject;

                ISettings setting = control as ISettings;
                if (setting != null)
                {
                    setting.Settings = new System.Collections.Generic.Dictionary<string, object>();
                    setting.Settings.Add("ObjectType", Helper.GetObjectTypeNumericID(objectType));
                    setting.Settings.Add("ObjectId", dataObject.ObjectID);
                    setting.Settings.Add("Width", WidgetHost.ColumnWidth - WidgetHost.ContentPadding);
                    setting.Settings.Add("ParentPageType", WidgetHost.ParentPageType);
                    setting.Settings.Add("ParentObjectType", WidgetHost.ParentObjectType);
                    setting.Settings.Add("ParentCommunityID", WidgetHost.ParentCommunityID);
                }

                PhDet.Controls.Add(control);

                if (showPager)
                {
                    Control pager = LoadControl("/UserControls/DetailPager.ascx");
                    ((IDataObjectWorker)pager).DataObject = dataObject;
                    PhPager.Controls.Add(pager);
                }

                // Set widget title
                Control widgetControl = WidgetHelper.GetWidgetHost(this, 0, 5);
                ((Literal)widgetControl.FindControl("LitTitle")).Text = ((Literal)widgetControl.FindControl("LitTitle")).Text.Replace("##OBJ_TITLE##", dataObject.Title).Replace("##OBJ_INSERT_DATE##", dataObject.Inserted.ToShortDateString() + " " + dataObject.Inserted.ToShortTimeString()).Replace("##OBJ_NICKNAME##", dataObject.Nickname);

                if (WidgetHost.ParentPageType == PageType.Detail)
                {
                    StringBuilder taglist = new StringBuilder();
                    foreach (string tag in dataObject.TagList.Split(Constants.TAG_DELIMITER))
                    {
                        if (tag.StartsWith("*"))
                            continue;

                        if (taglist.Length > 0)
                            taglist.Append(", ");

                        taglist.Append(tag);
                    }

                    if (WidgetHost.WidgetInstance.INS_HeadingTag.HasValue && WidgetHost.WidgetInstance.INS_HeadingTag.Value == 1)
                    {
                        ((IWidgetPageMaster)this.Page.Master).BreadCrumb.RenderDetailPageBreadCrumbs(dataObject);

                        ((IWidgetPageMaster)this.Page.Master).MetaKeywords.Content = Server.HtmlEncode(taglist.ToString().Trim());
                        ((IWidgetPageMaster)this.Page.Master).MetaDescription.Content = Server.HtmlEncode(dataObject.Description.StripHTMLTags());
                        ((IWidgetPageMaster)this.Page.Master).MetaModifiedDate.Content = System.Xml.XmlConvert.ToString(dataObject.Updated);
                        ((IWidgetPageMaster)this.Page.Master).MetaModifiedDate.Visible = true;

                        ((IWidgetPageMaster)this.Page.Master).MetaOgTitle.Content = Server.HtmlEncode(dataObject.Title.StripHTMLTags());
                        ((IWidgetPageMaster)this.Page.Master).MetaOgTitle.Visible = true;
                        ((IWidgetPageMaster)this.Page.Master).MetaOgUrl.Content = _4screen.CSB.Common.SiteConfig.SiteURL + Helper.GetDetailLink(dataObject.ObjectType, dataObject.ObjectID.ToString());
                        ((IWidgetPageMaster)this.Page.Master).MetaOgUrl.Visible = true;
                        if (!string.IsNullOrEmpty(dataObject.GetImage(PictureVersion.S, false)))
                        {
                            ((IWidgetPageMaster)this.Page.Master).MetaOgImage.Content = _4screen.CSB.Common.SiteConfig.MediaDomainName + dataObject.GetImage(PictureVersion.S, false);
                            ((IWidgetPageMaster)this.Page.Master).MetaOgImage.Visible = true;
                        }

                        if (dataObject.Geo_Lat != Double.MinValue && dataObject.Geo_Long != Double.MinValue)
                        {
                            ((IWidgetPageMaster)this.Page.Master).MetaOgLatitude.Content = dataObject.Geo_Lat.ToString();
                            ((IWidgetPageMaster)this.Page.Master).MetaOgLatitude.Visible = true;
                            ((IWidgetPageMaster)this.Page.Master).MetaOgLongitude.Content = dataObject.Geo_Long.ToString();
                            ((IWidgetPageMaster)this.Page.Master).MetaOgLongitude.Visible = true;
                        }

                        if (!string.IsNullOrEmpty(dataObject.Street) && !string.IsNullOrEmpty(dataObject.City) && !string.IsNullOrEmpty(dataObject.CountryCode))
                        {
                            ((IWidgetPageMaster)this.Page.Master).MetaOgStreet.Content = dataObject.Street;
                            ((IWidgetPageMaster)this.Page.Master).MetaOgStreet.Visible = true;
                            ((IWidgetPageMaster)this.Page.Master).MetaOgCity.Content = dataObject.City;
                            ((IWidgetPageMaster)this.Page.Master).MetaOgCity.Visible = true;
                            ((IWidgetPageMaster)this.Page.Master).MetaOgCountryCode.Content = dataObject.CountryCode;
                            ((IWidgetPageMaster)this.Page.Master).MetaOgCountryCode.Visible = true;
                            if (!string.IsNullOrEmpty(dataObject.Zip))
                            {
                                ((IWidgetPageMaster)this.Page.Master).MetaOgZipCode.Content = dataObject.Zip;
                                ((IWidgetPageMaster)this.Page.Master).MetaOgZipCode.Visible = true;
                            }
                        }
                    }
                }

                bool hasContent = true;
                if (setting.Settings.ContainsKey("HasContent"))
                    hasContent = (bool)setting.Settings["HasContent"];

                if (hasContent)
                {
                    dataObject.AddViewed(UserDataContext.GetUserDataContext());
                    if (UserProfile.Current.UserId != dataObject.UserID.Value)
                    {
                        _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent(dataObject.ObjectType.ToString().ToUpper() + "_VIEWED", UserDataContext.GetUserDataContext(), dataObject.ObjectID.Value.ToString());
                    }
                }
                return hasContent;
            }
            catch (Exception ex)
            {
                PhDet.Controls.Add(new LiteralControl(string.Format(GuiLanguage.GetGuiLanguage("Shared").GetString("MessageNotLoad"), Helper.GetObjectName(objectType, true))));
            }

            return true;
        }

        private DataObject LoadBySource(int objectType, int source)
        {
            QuickParameters qp = new QuickParameters();
            qp.Direction = QuickSortDirection.Desc;
            qp.SortBy = (QuickSort)source;
            qp.IgnoreCache = (qp.SortBy == QuickSort.Random);
            qp.ObjectType = Helper.GetObjectTypeNumericID(objectType);
            qp.ShowState = ObjectShowState.Published;
            qp.DisablePaging = true;
            qp.Amount = 1;

            if (this.WidgetHost.ParentObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
            {
                qp.Udc = UserDataContext.GetUserDataContext(Constants.ANONYMOUS_USERID);
                qp.CommunityID = null;
                qp.Communities = null;
                qp.UserID = UserDataContext.GetUserDataContext().UserID;
            }
            else if (this.WidgetHost.ParentObjectType == Helper.GetObjectTypeNumericID("Page"))
            {
                qp.Udc = UserDataContext.GetUserDataContext(Constants.ANONYMOUS_USERID);
                qp.CommunityID = null;
                qp.Communities = null;
                qp.UserID = null;
            }
            else if (this.WidgetHost.ParentObjectType == Helper.GetObjectTypeNumericID("Community"))
            {
                qp.Udc = UserDataContext.GetUserDataContext();
                qp.CommunityID = this.WidgetHost.ParentCommunityID;
            }

            DataObjectList<DataObject> list = DataObjects.LoadByReflection(qp);
            if (list.Count > 0)
                return list[0];
            else
                return new DataObject();

        }
    }
}
