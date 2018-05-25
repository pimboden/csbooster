//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#2.0.0.0		04.02.2009 / AW
//******************************************************************************
using System;
using System.Xml;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;
using System.Web.UI.WebControls;
using System.Text;

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
                dataObject = DataObject.LoadByReflection(objectId.Value, Helper.GetObjectTypeNumericID(objectType));
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
            if (_Host.OutputTemplate != null)
            {
                if (!string.IsNullOrEmpty(_Host.OutputTemplate.OutputTemplateControl))
                    template = _Host.OutputTemplate.OutputTemplateControl;
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
                    setting.Settings.Add("Width", _Host.ColumnWidth);
                    setting.Settings.Add("ParentPageType", _Host.ParentPageType);
                    setting.Settings.Add("ParentObjectType", _Host.ParentObjectType);
                    setting.Settings.Add("ParentCommunityID", _Host.ParentCommunityID);
                }

                PhDet.Controls.Add(control);
                if (dataObject.ObjectType == 3) //Picture
                {
                    ((IWidgetPageMaster)this.Page.Master).BodyTag.Attributes.Add("onload", "initImageAnnotation();");
                }

                //// Set widget title
                Control widgetControl = WidgetHelper.GetWidgetHost(this, 0, 5);
                ((Literal)widgetControl.FindControl("LitTitle")).Text = ((Literal)widgetControl.FindControl("LitTitle")).Text.Replace("##OBJ_TITLE##", dataObject.Title);

                if (_Host.ParentPageType == PageType.Detail)
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

                    // TODO: HTML content will not be rendered to the breadcrumb, because the breadcrumb would contain a lot of 'static' html content links
                    // same issue occurs of course, if there are multiple other object detail widget on ONE page
                    if (dataObject.objectType != 1020)
                        ((IWidgetPageMaster)this.Page.Master).BreadCrumb.RenderDetailPageBreadCrumbs(dataObject);

                    ((IWidgetPageMaster)this.Page.Master).MetaKeywords.Content = taglist.ToString().Trim();
                    ((IWidgetPageMaster)this.Page.Master).MetaDescription.Content = dataObject.Description.StripHTMLTags();
                    ((IWidgetPageMaster)this.Page.Master).MetaModifiedDate.Content = System.Xml.XmlConvert.ToString(dataObject.Updated);
                    ((IWidgetPageMaster)this.Page.Master).MetaModifiedDate.Visible = true;
                    ((IWidgetPageMaster)this.Page.Master).MetaModifiedDate.ID = null;
                }

                dataObject.AddViewed();

                if (UserProfile.Current.UserId != dataObject.UserID.Value)
                {
                    _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent(dataObject.ObjectType.ToString().ToUpper() + "_VIEWED", UserDataContext.GetUserDataContext(), dataObject.ObjectID.Value.ToString());
                }

                if (setting.Settings.ContainsKey("HasContent"))
                    return (bool)setting.Settings["HasContent"];
                else
                    return true;
            }
            catch (Exception ex)
            {
                PhDet.Controls.Add(new LiteralControl(string.Format(GuiLanguage.GetGuiLanguage("UserControls.WebUI").GetString("MessageNotLoad"), Helper.GetObjectName(objectType, true))));
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

            if (this._Host.ParentObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
            {
                qp.Udc = UserDataContext.GetUserDataContext(Constants.ANONYMOUS_USERID);
                qp.CommunityID = null;
                qp.Communities = null;
                qp.UserID = UserDataContext.GetUserDataContext().UserID;
            }
            else if (this._Host.ParentObjectType == Helper.GetObjectTypeNumericID("Page"))
            {
                qp.Udc = UserDataContext.GetUserDataContext(Constants.ANONYMOUS_USERID);
                qp.CommunityID = null;
                qp.Communities = null;
                qp.UserID = null;
            }
            else if (this._Host.ParentObjectType == Helper.GetObjectTypeNumericID("Community"))
            {
                qp.Udc = UserDataContext.GetUserDataContext();
                qp.CommunityID = this._Host.ParentCommunityID;
            }

            DataObjectList<DataObject> list = DataObjects.LoadByReflection(qp);
            if (list.Count > 0)
                return list[0];
            else
                return new DataObject();

        }
    }
}
