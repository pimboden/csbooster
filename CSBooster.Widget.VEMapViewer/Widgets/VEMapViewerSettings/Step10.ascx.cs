//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		17.08.2007 / PI
//                         Inherits StepsASCX
//                         Step with Basic Info
//  Updated:   
//******************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using Telerik.Web.UI;

namespace _4screen.CSB.Widget
{
    public partial class VEMapViewerSettings_Step10 : StepsASCX
    {
        private Guid InstanceID;
        private Control ctrlSourceAndTagSelector = null;
        private ISourceAndTagSelector SourceAndTagSelector = null;
        private Control ctrlRoleVisibilityAndFixation = null;
        private IRoleVisibilityAndFixation RoleVisibilityAndFixation = null;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ctrlSourceAndTagSelector = LoadControl("~/WidgetControls/SourceAndTagSelector.ascx");
            SourceAndTagSelector = (ISourceAndTagSelector)ctrlSourceAndTagSelector;
            SourceAndTagSelector.CommunityID = CommunityID.Value;
            InstanceID = ObjectID.Value;
            string strXml = LoadInstanceData(InstanceID);

            XmlDocument xmlDom = new XmlDocument();
            if (!string.IsNullOrEmpty(strXml))
            {
                xmlDom.LoadXml(strXml);
            }

            if (rcbOT.Items.Count == 0)
            {
                rcbOT.AllowCustomText = true;
                rcbOT.Text = string.Empty;
                RadComboBoxItem item = new RadComboBoxItem("Alle", string.Empty);
                CheckBox cbx = new CheckBox();
                cbx.ID = "CheckBox";
                cbx.Text = "Alle";
                cbx.Attributes.Add("onClick", "setComboboxText('" + rcbOT.ClientID + "');stopPropagation(event);");
                item.Controls.Add(cbx);
                rcbOT.Items.Add(item);

                List<SiteObjectType> completeObjectTypesList = Helper.GetObjectTypes();
                var allObjectTypes = from types in completeObjectTypesList.Where(x => x.IsGeoTaggable)
                                     select new
                                                {
                                                    type = types.Id,
                                                    Singular = GuiLanguage.GetGuiLanguage("SiteObjects").GetString(types.NameSingularKey),
                                                    Plural = GuiLanguage.GetGuiLanguage("SiteObjects").GetString(types.NamePluralKey)
                                                };
                string[] objTypes = { };
                string oTTemp = XmlHelper.GetElementValue(xmlDom.DocumentElement, "objTypes", string.Empty).ToLower();
                if (oTTemp.Length > 0)
                {
                    objTypes = oTTemp.Split(',');
                }
                else
                {
                    cbx.Checked = true;
                    rcbOT.Text = "Alle";
                }
                foreach (var obj in allObjectTypes)
                {
                    item = new RadComboBoxItem(obj.Plural, obj.type);
                    cbx = new CheckBox();
                    cbx.ID = "CheckBox";
                    cbx.Text = obj.Plural;
                    if (objTypes.Contains(obj.type.ToLower()))
                    {
                        cbx.Checked = true;
                        rcbOT.Text += cbx.Text + ",";
                    }

                    cbx.Attributes.Add("onClick", "setComboboxText('" + rcbOT.ClientID + "');stopPropagation(event);");
                    item.Controls.Add(cbx);
                    rcbOT.Items.Add(item);
                }
                rcbOT.Text = rcbOT.Text.Trim(',');
            }


            rcbOverWriteByURL.SelectedIndex = rcbOverWriteByURL.Items.IndexOf(rcbOverWriteByURL.Items.FindItemByValue(XmlHelper.GetElementValue(xmlDom.DocumentElement, "rcbOverWriteByURL", "0")));
            int layoutType = XmlHelper.GetElementValue(xmlDom.DocumentElement, "layoutType", (int)VEMAPVWLayoutType.MultiLayerByObjectType);

            if (layoutType == (int)VEMAPVWLayoutType.MultiLayerByObjectType)
            {
                rcbLT.SelectedIndex = 2;
                rcbST.SelectedIndex = 0;
            }
            else if (layoutType == (int)VEMAPVWLayoutType.MultiLayerByTag)
            {
                rcbLT.SelectedIndex = 2;
                rcbST.SelectedIndex = 1;
            }
            else if (layoutType == (int)VEMAPVWLayoutType.SingleLayerByObjectType)
            {
                rcbLT.SelectedIndex = 1;
                rcbST.SelectedIndex = 0;
            }
            else if (layoutType == (int)VEMAPVWLayoutType.SingleLayerdByTag)
            {
                rcbLT.SelectedIndex = 1;
                rcbST.SelectedIndex = 1;
            }
            else if (layoutType == (int)VEMAPVWLayoutType.SimpleByObjectType)
            {
                rcbLT.SelectedIndex = 0;
                rcbST.SelectedIndex = 0;
            }
            else if (layoutType == (int)VEMAPVWLayoutType.SimpleByTag)
            {
                rcbLT.SelectedIndex = 0;
                rcbST.SelectedIndex = 1;
            }
            SourceAndTagSelector.TagList1 = XmlHelper.GetElementValue(xmlDom.DocumentElement, "tagWords", string.Empty).ToLower();
            SourceAndTagSelector.TagList2 = XmlHelper.GetElementValue(xmlDom.DocumentElement, "tagWords2", string.Empty).ToLower();
            SourceAndTagSelector.TagList3 = XmlHelper.GetElementValue(xmlDom.DocumentElement, "tagWords3", string.Empty).ToLower();
            SourceAndTagSelector.DataSourceIDs = XmlHelper.GetElementValue(xmlDom.DocumentElement, "ctyID", CommunityID.ToString());
            SourceAndTagSelector.DataSourceSelection = XmlHelper.GetElementValue(xmlDom.DocumentElement, "rcbDS", Convert.ToString((int)WidgetDataSource.SingleCommunity));
            SourceAndTagSelector.UserID = XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtUI", SiteContext.UserProfile.UserId.ToString()).ToNullableGuid();

            phSTS.Controls.Add(ctrlSourceAndTagSelector);
            ctrlRoleVisibilityAndFixation = LoadControl("~/WidgetControls/RoleVisibilityAndFixation.ascx");
            RoleVisibilityAndFixation = (IRoleVisibilityAndFixation)ctrlRoleVisibilityAndFixation;
            ctrlRoleVisibilityAndFixation.ID = "RVAF";
            RoleVisibilityAndFixation.InstanceID = InstanceID;
            phRF.Controls.Add(ctrlRoleVisibilityAndFixation);
        }

        public override bool SaveStep(int NextStep)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                base.SaveStep(NextStep);
                try
                {
                    RoleVisibilityAndFixation.Save();
                    XmlDocument xmlDom = new XmlDocument();
                    InstanceID = ObjectID.Value;
                    string strXml = LoadInstanceData(InstanceID);
                    if (string.IsNullOrEmpty(strXml))
                    {
                        xmlDom.DocumentElement.SetAttribute("version", "2.0");
                        XmlHelper.CreateRoot(xmlDom, "root");
                    }
                    else
                    {
                        xmlDom.LoadXml(strXml);
                    }

                    SourceAndTagSelector.GetProperties();


                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "txtUI", SourceAndTagSelector.UserID.HasValue? SourceAndTagSelector.UserID.Value.ToString():string.Empty);
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "ctyID", SourceAndTagSelector.DataSourceIDs);
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "rcbDS", SourceAndTagSelector.DataSourceSelection);
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "rcbOverWriteByURL", rcbOverWriteByURL.SelectedValue);
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "tagWords", SourceAndTagSelector.TagList1);
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "tagWords2", SourceAndTagSelector.TagList2);
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "tagWords3", SourceAndTagSelector.TagList3);

                    if (rcbLT.SelectedIndex == 0 && rcbST.SelectedIndex == 0)
                    {
                        XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "layoutType", (int)VEMAPVWLayoutType.SimpleByObjectType);
                    }
                    else if (rcbLT.SelectedIndex == 0 && rcbST.SelectedIndex == 1)
                    {
                        XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "layoutType", (int)VEMAPVWLayoutType.SimpleByTag);
                    }
                    else if (rcbLT.SelectedIndex == 1 && rcbST.SelectedIndex == 0)
                    {
                        XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "layoutType", (int)VEMAPVWLayoutType.SingleLayerByObjectType);
                    }
                    else if (rcbLT.SelectedIndex == 1 && rcbST.SelectedIndex == 1)
                    {
                        XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "layoutType", (int)VEMAPVWLayoutType.SingleLayerdByTag);
                    }
                    else if (rcbLT.SelectedIndex == 2 && rcbST.SelectedIndex == 0)
                    {
                        XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "layoutType", (int)VEMAPVWLayoutType.MultiLayerByObjectType);
                    }
                    else if (rcbLT.SelectedIndex == 2 && rcbST.SelectedIndex == 1)
                    {
                        XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "layoutType", (int)VEMAPVWLayoutType.MultiLayerByTag);
                    }


                    string selectedObjTypes = string.Empty;
                    for (int i = 0; i < rcbOT.Items.Count; i++)
                    {
                        string checkboxId = rcbOT.UniqueID + "$i" + i + "$Checkbox";
                        if (Request.Form[checkboxId] != null)
                        {
                            if (i > 0)
                            {
                                selectedObjTypes += rcbOT.Items[i].Value + ",";
                            }
                            else if (rcbOT.Text.ToLower().IndexOf(",alle") > -1 || rcbOT.Text.ToLower().IndexOf("alle,") > -1 || rcbOT.Text.Trim().ToLower() == "alle")
                            {
                                //All Selected --> Return Empty, which means all
                                selectedObjTypes = string.Empty;
                                break;
                            }
                        }
                    }

                    selectedObjTypes = selectedObjTypes.Trim(',');
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "objTypes", selectedObjTypes);

                    return SaveInstanceData(InstanceID, xmlDom.OuterXml);
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}