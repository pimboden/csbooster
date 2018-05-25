// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Data;
using _4screen.Utils.Web;
using Telerik.Web.UI;
using SiteConfig=_4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.Admin
{
    public partial class NavigationsEdit : Page
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Admin");

        private Guid? navId;
        private string returnUrl;
        UserControls.SingleObjectSelection soslLinkPicker;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.SiteAdmin);

            RegisterScripts();
            if (!string.IsNullOrEmpty(Request.QueryString["NavID"]))
            {
                navId = Request.QueryString["NavID"].ToGuid();
            }
            if (!string.IsNullOrEmpty(Request.Form[hidCurrNavID.UniqueID]))
            {
                navId = Request.Form[hidCurrNavID.UniqueID].ToGuid();
            }
            returnUrl = Request.QueryString["Src"];
            LoadNaviTree(navId);
            if (!string.IsNullOrEmpty(Request.Form[rcLinks.ClientID + "_ClientState"]))
            {
                string itemValue = GetRadComboboxValue(rcLinks);


                if (!string.IsNullOrEmpty(itemValue) && itemValue.StartsWith("OT_"))
                {
                    RcLinkSingleObjectType(itemValue.Substring(3));
                }
                ShowNodeEdit(Request.Form[hidCurrentNodeValue.UniqueID], false);

            }
        }

        private void RegisterScripts()
        {
            string js = string.Format(@"
                <script type=""text/javascript"">

        function ChangeLinkType(sender, eventArgs) {{
            var item = eventArgs.get_item();
            if (item.get_value() == ""URL"") {{
                document.getElementById('{0}').style.visibility = 'visible';
                document.getElementById('{0}').style.display = 'inline';
                sender.set_text(""Geben Sie den Link an:"");
                document.getElementById('{1}').style.visibility = 'hidden';
                document.getElementById('{1}').style.display = 'none';

            }}
            else if (item.get_value().indexOf('OT_') != -1) {{
                document.getElementById('{0}').style.visibility = 'visible';
                document.getElementById('{0}').style.display = 'inline';
                __doPostBack('{2}', '')
            }}
            else {{
                document.getElementById('{0}').style.visibility = 'hidden';
                document.getElementById('{0}').style.display = 'none';
                document.getElementById('{3}').value = item.get_text();
                document.getElementById('{1}').style.visibility = 'hidden';
                document.getElementById('{1}').style.display = 'none';

            }}

        }}

    </script>", txtUrl.ClientID, pnlLinkPicker.ClientID, rcLinks.ClientID, txtText.ClientID);
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "navJS" + ClientID, js, false);

        }

        private string GetRadComboboxValue(RadComboBox rd)
        {
            string retValue = string.Empty;
            if (!string.IsNullOrEmpty(Request.Form[rd.ClientID + "_ClientState"]))
            {

                System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
                RadComboBoxItem item;
                try
                {
                    item = ser.Deserialize<RadComboBoxItem>(Request.Form[rd.ClientID + "_ClientState"]);
                    retValue = item.Value;
                }
                catch(Exception)
                {

                }
            }
            else
            {
                retValue = rd.SelectedValue;
            }
            return retValue;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //((MasterPages_SiteAdmin)this.Master).SetNavigationItem("NavigationsEdit");

        }

        private void LoadNaviTree(Guid? navTreeId)
        {
            //Load The structure of the Navigation
            CSBooster_DataContext csb = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            XDocument xNavigation = new XDocument();
            if (navTreeId.HasValue)
            {
                hidCurrNavID.Value = navTreeId.ToString();
                var navStructureRecord = csb.hisp_Navigation_GetNavigationStructureXML(navTreeId).SingleOrDefault();
                var navLanguageRecord = csb.hisp_Navigation_GetNavigationLanguageXML(navTreeId, CultureHandler.GetCurrentNeutralLanguageCode()).SingleOrDefault();
                if (navStructureRecord != null)
                {
                    XElement xmlStruct = XElement.Parse(navStructureRecord.NST_XMLStruct);

                    XElement xmlLanguage = navLanguageRecord != null ? XElement.Parse(navLanguageRecord.NAV_XMLNames) : XElement.Parse(@"<Tree/>");
                    xNavigation = new XDocument();
                    XElement xRoot = new XElement("Tree");
                    XElement xCurrentParrent = new XElement("Node", new XAttribute("Text", csb.hisp_Navigation_GetNavigationKeyName(navTreeId).SingleOrDefault().NST_KeyName), new XAttribute("Value", navTreeId.ToString()));

                    foreach (var xnode in xmlStruct.Elements("Node"))
                    {
                        XElement xnewChild = DataAccess.Business.Navigation.ReplaceData(xnode, xmlLanguage, true);
                        if (xnewChild != null)
                        {
                            xCurrentParrent.Add(xnewChild);
                        }
                    }

                    xRoot.Add(xCurrentParrent);
                    xNavigation.Add(xRoot);
                    if (navStructureRecord.NST_IsDirty)
                    {
                        lbtnG.Text = language.GetString("TooltipNotSynchronized");
                        lbtnG.CssClass = "icon navnotsynchro";
                    }
                    else
                    {
                        lbtnG.Text = language.GetString("TooltipSynchronized");
                        lbtnG.CssClass = "icon navsynchro";
                    }
                }
            }
            else
            {
                navTreeId = Guid.NewGuid();
                hidCurrNavID.Value = navTreeId.ToString();
                xNavigation = XDocument.Parse(string.Format(@"<Tree><Node Text=""Neue Navigation"" Value=""{0}""/></Tree>", hidCurrNavID.Value));
                //Create a New Navigation
                XDocument xStruct = new XDocument(
                    new XElement("Tree"));
                XDocument xLang = new XDocument(
                   new XElement("Tree"));
                csb.hisp_Navigation_SaveNavigationStructure(navTreeId, "Neue Navigation", xStruct.ToString());
                lbtnG.Text = language.GetString("TooltipNotSynchronized");
                lbtnG.CssClass = "icon navnotsynchro";

                csb.hisp_Navigation_SaveNavigationLanguage(navTreeId, CultureHandler.GetCurrentNeutralLanguageCode(), xLang.ToString(), string.Empty);
                lbtnG.Text = language.GetString("TooltipSynchronized");
                lbtnG.CssClass = "icon navnotsynchro";
            }
            rtv1.LoadXmlString(xNavigation.ToString());
            rtv1.Nodes[0].EnableContextMenu = true;
            rtv1.Nodes[0].ContextMenuID = rtvCMRoot.ID;
        }

        protected void rtv1_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                ShowNaviNameEdit();
            }
            else
            {
                ShowNodeEdit(e.Node.Value, true);
                hidCurrentNodeValue.Value = e.Node.Value;
            }
        }

        protected void rtv1_ContextMenuItemClick(object sender, RadTreeViewContextMenuEventArgs e)
        {
            if (hidCurrNavID.Value == string.Empty)
                hidCurrNavID.Value = Request.Form[hidCurrNavID.UniqueID];
            if (e.MenuItem != null)
            {

                if (e.MenuItem.Value == "NewNode")
                {
                    string newNodeValue = Guid.NewGuid().ToString();
                    RadTreeNode rtN = new RadTreeNode("NewFolder", newNodeValue);
                    e.Node.ParentNode.Nodes.Add(rtN);
                    AddNavNode("NewFolder", newNodeValue, e.Node.ParentNode.Value);
                    ShowNodeEdit(newNodeValue, true);
                    rtN.Selected = true;
                    rtN.ExpandParentNodes();
                    hidCurrentNodeValue.Value = newNodeValue;

                }
                else if (e.MenuItem.Value == "NewSubNode")
                {
                    string newNodeValue = Guid.NewGuid().ToString();
                    RadTreeNode rtN = new RadTreeNode("NewFolder", newNodeValue);
                    e.Node.Nodes.Add(rtN);
                    rtN.Selected = true;
                    rtN.ExpandParentNodes();
                    AddNavNode("NewFolder", newNodeValue, e.Node.Value);
                    ShowNodeEdit(newNodeValue, true);
                    hidCurrentNodeValue.Value = newNodeValue;
                }
                else if (e.MenuItem.Value == "DeleteNode")
                {
                    RemoveNavNode(e.Node.Value);
                    hidCurrentNodeValue.Value = e.Node.ParentNode.Value;
                    e.Node.ParentNode.Selected = true;
                    e.Node.Remove();

                }
                else if (e.MenuItem.Value == "NewName")
                {
                    ShowNaviNameEdit();
                }
            }

        }

        private void ShowNodeEdit(string nodeValue, bool updateValues)
        {
            pnlNavNodeProp.Visible = true;
            pnlNaviNameEdit.Visible = false;
            Guid currentNavId = new Guid(hidCurrNavID.Value);
            CSBooster_DataContext csb = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            XElement xmlStructNode = null;
            XElement xmlLanguageNode = null;
            var navStructureRecord = csb.hisp_Navigation_GetNavigationStructureXML(currentNavId).SingleOrDefault();
            var navLanguageRecord = csb.hisp_Navigation_GetNavigationLanguageXML(currentNavId, CultureHandler.GetCurrentNeutralLanguageCode()).SingleOrDefault();
            if (navStructureRecord != null)
            {
                XElement xmlStruct = XElement.Parse(navStructureRecord.NST_XMLStruct);
                xmlStructNode = (from xmlStructNodes in xmlStruct.Descendants("Node").Where(x => x.Attribute("Value").Value == nodeValue)
                                 select xmlStructNodes).SingleOrDefault();

                if (navLanguageRecord != null)
                {
                    XElement xmlLanguage = XElement.Parse(navLanguageRecord.NAV_XMLNames);
                    xmlLanguageNode = (from xmlLanguageNodes in xmlLanguage.Descendants("Node").Where(x => x.Attribute("Value").Value == nodeValue)
                                       select xmlLanguageNodes).SingleOrDefault() ?? XElement.Parse(string.Format(@"<Node Text='' Value='{0}'/>", nodeValue));
                }
                else
                {
                    xmlLanguageNode = XElement.Parse(string.Format(@"<Node Text='' Value='{0}'/>", nodeValue));
                }
            }
            LoadRolesCombobox(xmlStructNode);
            txtText.Text = xmlLanguageNode.Attribute("Text") != null ? xmlLanguageNode.Attribute("Text").Value : string.Empty;
            txtTextTool.Text = xmlLanguageNode.Attribute("Tooltip") != null ? xmlLanguageNode.Attribute("Tooltip").Value : string.Empty;
            string target = xmlStructNode.Attribute("Target") != null ? xmlStructNode.Attribute("Target").Value : "_self";
            rcTarget.SelectedIndex = rcTarget.Items.FindItemIndexByValue(target);
            LoadPredefinedUrls();
            if (updateValues)
            {
                if ((xmlStructNode.Attribute("PredefinedUrl") == null || xmlStructNode.Attribute("PredefinedUrl").Value == "") &&
                    (xmlStructNode.Attribute("ObjectUrl") == null || string.IsNullOrEmpty(xmlStructNode.Attribute("ObjectUrl").Value)))
                {
                    //Neither Predeifined nor link to an object
                    rcLinks.SelectedIndex = 0;
                    if (xmlLanguageNode.Attribute("NavigateUrl") != null && !string.IsNullOrEmpty(xmlLanguageNode.Attribute("NavigateUrl").Value))
                        txtUrl.Text = xmlLanguageNode.Attribute("NavigateUrl").Value;
                    else
                        txtUrl.Text = string.Empty;
                    txtUrl.Visible = true;
                    pnlLinkPicker.Controls.Clear();
                }
                else if (xmlStructNode.Attribute("ObjectUrl") != null && !string.IsNullOrEmpty(xmlStructNode.Attribute("ObjectUrl").Value))
                {
                    //link to an object
                    rcLinks.SelectedIndex = rcLinks.Items.FindItemIndexByValue("OT_" + xmlStructNode.Attribute("ObjectUrl").Value);
                    if (xmlLanguageNode.Attribute("NavigateUrl") != null && !string.IsNullOrEmpty(xmlLanguageNode.Attribute("NavigateUrl").Value))
                        txtUrl.Text = xmlLanguageNode.Attribute("NavigateUrl").Value;
                    else
                        txtUrl.Text = string.Empty;

                    txtUrl.Visible = true;
                    RcLinkSingleObjectType(xmlStructNode.Attribute("ObjectUrl").Value);
                }
                else
                {
                    pnlLinkPicker.Controls.Clear();
                    //Predeifined
                    try
                    {
                        try
                        {
                            txtUrl.Attributes.Remove("style");
                        }
                        catch
                        {
                        }
                        txtUrl.Attributes.Add("style", "display:none;visibility:hidden;");
                        txtUrl.Text = string.Empty;

                    }
                    catch
                    {
                        rcLinks.SelectedIndex = 0;
                        txtUrl.Text = string.Empty;
                        txtUrl.Visible = true;

                    }
                }
            }
            lbtnT.NavigateUrl = string.Format("javascript:radWinOpen('/Pages/Popups/NavigationTranslate.aspx?NAV={0}&NID={1}&Target={2}', 'Navigation', 550, 400, true)", currentNavId, nodeValue, "NodeTrans"); 

            lbtnT.Visible = SiteConfig.NeutralLanguages.Count > 1;
        }

        private void LoadRolesCombobox(XElement xmlStructNode)
        {
            try
            {
                string[] allRoles = string.Format("{0},Anonymous", string.Join(",", Roles.GetAllRoles())).Split(',');
                string roleVisibility = xmlStructNode.Attribute("RolesVisibility") != null ? xmlStructNode.Attribute("RolesVisibility").Value : string.Empty;


                //Gett al parent nodes and get the one that has the most restriction... only show roles of that parent
                var restrictedAncestor = (from allAncestor in xmlStructNode.Ancestors("Node").Where(x => x.Attribute("RolesVisibility") != null && x.Attribute("RolesVisibility").Value != string.Empty)
                                          select allAncestor).FirstOrDefault();
                if (restrictedAncestor != null)
                {
                    allRoles = restrictedAncestor.Attribute("RolesVisibility").Value.Split(Constants.TAG_DELIMITER);
                    if (roleVisibility == string.Empty)
                    {
                        roleVisibility = restrictedAncestor.Attribute("RolesVisibility").Value;
                    }
                    else
                    {
                        //only the roles of the most restrictive parent are allowed to be in this node... remove any other role

                        string[] currentRoles = roleVisibility.Split(Constants.TAG_DELIMITER);
                        List<string> allowedNodeRoles = new List<string>();
                        foreach (string currentRole in currentRoles)
                        {
                            if (allRoles.Contains(currentRole))
                            {
                                allowedNodeRoles.Add(currentRole);
                            }
                        }
                        roleVisibility = string.Join(Constants.TAG_DELIMITER.ToString(), allowedNodeRoles.ToArray());
                    }
                }


                rcRoles.Items.Clear();
                rcRoles.ZIndex = 99999;
                rcRoles.Height = Unit.Pixel(120);
                rcRoles.AllowCustomText = true;
                rcRoles.Text = string.Empty;
                rcRoles.Attributes.Add("OnChange", "setComboboxText('" + rcRoles.ClientID + "');stopPropagation(event);");


                foreach (string key in allRoles)
                {
                    RadComboBoxItem rcItem = new RadComboBoxItem(key, key);
                    CheckBox checkbox = new CheckBox
                                            {
                                                ID = "CheckBox",
                                                Text = key
                                            };
                    checkbox.Attributes.Add("onClick", "setComboboxText('" + rcRoles.ClientID + "');stopPropagation(event);");
                    rcItem.Controls.Add(checkbox);
                    checkbox.Checked = string.IsNullOrEmpty(roleVisibility) || roleVisibility.ToLower().Contains(key.ToLower() + Constants.TAG_DELIMITER) || roleVisibility.ToLower().EndsWith(key.ToLower());
                    if (checkbox.Checked)
                    {
                        rcRoles.Text += rcItem.Text + ",";
                    }
                    rcRoles.Items.Add(rcItem);
                }

                rcRoles.Text = rcRoles.Text.TrimEnd(',');
            }
            catch
            {

            }
        }

        private void LoadPredefinedUrls()
        {
            var allPredNavLink = from predNl in Constants.Links.LINQEnumarable.Where(x => x.PredefinedNaviLink)
                                 select predNl;

            int i = 0;
            foreach (var xPredefinedNavi in allPredNavLink)
            {
                string defaultText = string.Format("Predefined Link {0}", i);
                string localizationBaseFileName = "Navigation";
                if (!string.IsNullOrEmpty(xPredefinedNavi.LocalizationBaseFileName))
                {
                    localizationBaseFileName = xPredefinedNavi.LocalizationBaseFileName;
                }

                rcLinks.Items.Add(new RadComboBoxItem(GuiLanguage.GetGuiLanguage(localizationBaseFileName).GetString(xPredefinedNavi.UrlTextKey) ?? defaultText, xPredefinedNavi.Key));
                i++;
            }
            List<SiteObjectType> lst = Helper.GetActiveUserContentObjectTypes(false);
            var allObjectTypes = from allTypes in lst
                                 select new { ID = allTypes.Id, NummericIs = allTypes.NumericId, MenuTitle = Helper.GetObjectName(allTypes.NumericId, true) };
            foreach (var objectType in allObjectTypes)
            {
                rcLinks.Items.Add(new RadComboBoxItem(objectType.MenuTitle, string.Format("OT_{0}", objectType.ID)));
            }

        }

        private void ShowNaviNameEdit()
        {
            pnlNavNodeProp.Visible = false;
            pnlNaviNameEdit.Visible = true;
            txtNavName.Text = rtv1.Nodes[0].Text;
        }

        protected void rtv1_NodeDrop(object sender, RadTreeNodeDragDropEventArgs e)
        {
            RadTreeNode sourceNode = e.SourceDragNode;
            RadTreeNode destNode = e.DestDragNode;
            RadTreeViewDropPosition dropPosition = e.DropPosition;
            if (sourceNode.TreeView.SelectedNodes.Count <= 1)
            {
                PerformDragAndDrop(dropPosition, sourceNode, destNode);
            }
            else if (sourceNode.TreeView.SelectedNodes.Count > 1)
            {
                foreach (RadTreeNode node in sourceNode.TreeView.SelectedNodes)
                {
                    PerformDragAndDrop(dropPosition, node, destNode);
                }
            }
            ShowNodeEdit(e.SourceDragNode.Value, true);
            hidCurrentNodeValue.Value = e.SourceDragNode.Value;

        }

        private void AddNavNode(string nodeText, string nodeValue, string parentNodeValue)
        {
            Guid currentNavId = new Guid(hidCurrNavID.Value);
            CSBooster_DataContext csb = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            hidParentNodeId.Value = parentNodeValue;
            //Savte the Current Node in the struct XML 
            var navStructureRecord = csb.hisp_Navigation_GetNavigationStructureXML(currentNavId).SingleOrDefault();
            if (navStructureRecord != null)
            {
                XElement xmlStruct = XElement.Parse(navStructureRecord.NST_XMLStruct);
                var parentNode = (from theNodes in xmlStruct.Descendants("Node").Where(x => x.Attribute("Value").Value == parentNodeValue)
                                  select theNodes).SingleOrDefault();
                if (parentNode != null)
                {

                    parentNode.Add(new XElement("Node", new XAttribute("Text", nodeText), new XAttribute("Value", nodeValue)));
                }
                else
                {
                    //The Parent Node is the Root... add it to the Tree
                    xmlStruct.Add(new XElement("Node", new XAttribute("Text", nodeText), new XAttribute("Value", nodeValue)));
                }
                csb.hisp_Navigation_SaveNavigationStructure(currentNavId, null, xmlStruct.ToString());
                lbtnG.Text = language.GetString("TooltipNotSynchronized");
                lbtnG.CssClass = "icon navnotsynchro";


                var navLanguageRecord = csb.hisp_Navigation_GetNavigationLanguageXML(currentNavId, CultureHandler.GetCurrentNeutralLanguageCode()).SingleOrDefault();
                if (navLanguageRecord != null)
                {
                    XElement xmlLanguage = XElement.Parse(navLanguageRecord.NAV_XMLNames);
                    var langNode = (from theLangNodes in xmlLanguage.Descendants("Node").Where(x => x.Attribute("Value").Value == nodeValue)
                                    select theLangNodes).SingleOrDefault();
                    if (langNode == null)
                    {
                        xmlLanguage.Add(new XElement(new XElement("Node", new XAttribute("Text", nodeText), new XAttribute("Value", nodeValue))));
                    }
                    else
                    {
                        //Should never happen...
                        langNode.Attribute("Text").Value = nodeText;
                    }
                    csb.hisp_Navigation_SaveNavigationLanguage(currentNavId, CultureHandler.GetCurrentNeutralLanguageCode(), xmlLanguage.ToString(), string.Empty);
                }
                else
                {
                    csb.hisp_Navigation_SaveNavigationLanguage(currentNavId, CultureHandler.GetCurrentNeutralLanguageCode(), "<Tree/>", string.Empty);
                }
            }
        }

        private void RemoveNavNode(string nodeValue)
        {
            Guid currentNavId = new Guid(hidCurrNavID.Value);
            CSBooster_DataContext csb = new CSBooster_DataContext(Helper.GetSiemeConnectionString());

            var navStructureRecord = csb.hisp_Navigation_GetNavigationStructureXML(currentNavId).SingleOrDefault();
            if (navStructureRecord != null)
            {
                XElement xmlStruct = XElement.Parse(navStructureRecord.NST_XMLStruct);
                var nodeToRemove = (from theNodes in xmlStruct.Descendants("Node").Where(x => x.Attribute("Value").Value == nodeValue)
                                    select theNodes).SingleOrDefault();
                if (nodeToRemove != null)
                {
                    nodeToRemove.Remove();
                }
                csb.hisp_Navigation_SaveNavigationStructure(currentNavId, null, xmlStruct.ToString());
                lbtnG.Text = language.GetString("TooltipNotSynchronized");
                lbtnG.CssClass = "icon navnotsynchro";

                foreach (string langKey in SiteConfig.NeutralLanguages.Keys)
                {
                    var navLanguageRecord = csb.hisp_Navigation_GetNavigationLanguageXML(currentNavId, langKey).SingleOrDefault();
                    if (navLanguageRecord != null)
                    {
                        XElement xmlLanguage = XElement.Parse(navLanguageRecord.NAV_XMLNames);
                        var langNodeToRemove = (from theLangNodes in xmlLanguage.Descendants("Node").Where(x => x.Attribute("Value").Value == nodeValue)
                                                select theLangNodes).SingleOrDefault();

                        if (langNodeToRemove != null)
                        {
                            langNodeToRemove.Remove();
                        }
                        csb.hisp_Navigation_SaveNavigationLanguage(currentNavId, langKey, xmlLanguage.ToString(), string.Empty);
                    }
                }
            }
        }

        private void PerformDragAndDrop(RadTreeViewDropPosition dropPosition, RadTreeNode sourceNode, RadTreeNode destNode)
        {
            if (sourceNode.Equals(destNode) || sourceNode.IsAncestorOf(destNode))
            {
                return;
            }
            sourceNode.Owner.Nodes.Remove(sourceNode);
            Guid currentNavId = new Guid(hidCurrNavID.Value);
            CSBooster_DataContext csb = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            var navStructureRecord = csb.hisp_Navigation_GetNavigationStructureXML(currentNavId).SingleOrDefault();
            XElement xMoveNode = null;
            XElement structDestNode = null;
            XElement xmlStruct = null;
            if (navStructureRecord != null)
            {
                xmlStruct = XElement.Parse(navStructureRecord.NST_XMLStruct);
                var nodeToRemove = (from theNodes in xmlStruct.Descendants("Node").Where(x => x.Attribute("Value").Value == sourceNode.Value)
                                    select theNodes).SingleOrDefault();
                structDestNode = (from theNodes in xmlStruct.Descendants("Node").Where(x => x.Attribute("Value").Value == destNode.Value)
                                  select theNodes).SingleOrDefault();
                if (nodeToRemove != null)
                {
                    xMoveNode = XElement.Parse(nodeToRemove.ToString());
                    nodeToRemove.Remove();

                }
            }
            if (xMoveNode != null)
            {

                switch (dropPosition)
                {
                    case RadTreeViewDropPosition.Over:
                        // child
                        if (!sourceNode.IsAncestorOf(destNode))
                        {
                            destNode.Nodes.Add(sourceNode);
                            if (structDestNode != null)
                            {
                                structDestNode.Add(xMoveNode);
                            }
                            else
                            {
                                //droped to the root
                                xmlStruct.Add(xMoveNode);
                            }
                        }
                        break;

                    case RadTreeViewDropPosition.Above:
                        // sibling - above                    
                        destNode.InsertBefore(sourceNode);
                        structDestNode.AddBeforeSelf(xMoveNode);
                        break;

                    case RadTreeViewDropPosition.Below:
                        // sibling - below
                        destNode.InsertAfter(sourceNode);
                        structDestNode.AddAfterSelf(xMoveNode);
                        break;
                }
                sourceNode.ExpandParentNodes();
                csb.hisp_Navigation_SaveNavigationStructure(currentNavId, null, xmlStruct.ToString());
                lbtnG.Text = language.GetString("TooltipNotSynchronized");
                lbtnG.CssClass = "icon navnotsynchro";

            }
        }

        private object GetRoleVisibility()
        {

            string strRoleVisibility = string.Empty;
            for (int i = 0; i < rcRoles.Items.Count; i++)
            {
                string checkboxId = rcRoles.UniqueID + "$i" + i + "$Checkbox";
                if (Request.Form[checkboxId] != null)
                {
                    strRoleVisibility += rcRoles.Items[i].Text + Constants.TAG_DELIMITER;
                }
            }
            if (strRoleVisibility.Length > 0)
                strRoleVisibility = strRoleVisibility.TrimEnd(Constants.TAG_DELIMITER);

            return strRoleVisibility;
        }

        protected void RcLinkSingleObjectType(string objectTypeName)
        {
            try
            {
                txtUrl.Attributes.Remove("style");
            }
            catch
            {
            }
            Control ctrlLinkPicker = LoadControl("/Admin/UserControls/SingleObjectSelection.ascx");
            soslLinkPicker = ctrlLinkPicker as UserControls.SingleObjectSelection;


            phLinkPicker.Controls.Clear();

            phLinkPicker.Controls.Add(soslLinkPicker);
            soslLinkPicker.CurrentSelected = txtUrl.Text;
            soslLinkPicker.UrlTextBoxId = txtUrl.ClientID;
            soslLinkPicker.ObjType = Helper.GetObjectTypeNumericID(objectTypeName);
            soslLinkPicker.StartSearch();
        }
        protected void lbtnSaveNode_Click(object sender, EventArgs e)
        {
            Guid currentNavId = new Guid(hidCurrNavID.Value);
            string nodeValue = Request.Form[hidCurrentNodeValue.UniqueID];
            CSBooster_DataContext csb = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            XElement xmlLanguageNode;
            var navStructureRecord = csb.hisp_Navigation_GetNavigationStructureXML(currentNavId).SingleOrDefault();
            var navLanguageRecord = csb.hisp_Navigation_GetNavigationLanguageXML(currentNavId, CultureHandler.GetCurrentNeutralLanguageCode()).SingleOrDefault();
            if (navStructureRecord != null)
            {
                XElement xmlStruct = XElement.Parse(navStructureRecord.NST_XMLStruct);
                XElement xmlLanguage;
                XElement xmlStructNode = (from xmlStructNodes in xmlStruct.Descendants("Node").Where(x => x.Attribute("Value").Value == nodeValue)
                                          select xmlStructNodes).SingleOrDefault();

                if (navLanguageRecord != null)
                {
                    xmlLanguage = XElement.Parse(navLanguageRecord.NAV_XMLNames);
                    xmlLanguageNode = (from xmlLanguageNodes in xmlLanguage.Descendants("Node").Where(x => x.Attribute("Value").Value == nodeValue)
                                       select xmlLanguageNodes).SingleOrDefault();
                    if (xmlLanguageNode == null)
                    {
                        xmlLanguageNode = XElement.Parse(string.Format(@"<Node Text='' Value='{0}'/>", nodeValue));
                        xmlLanguage.Add(xmlLanguageNode);
                    }

                }
                else
                {
                    xmlLanguage = XElement.Parse(string.Format(@"<Tree><Node Text='' Value='{0}'/></Tree>", nodeValue));
                    xmlLanguageNode = xmlLanguage.Nodes().OfType<XElement>().FirstOrDefault();
                }
                if (rcRoles.Items.Count == 0)
                {
                    LoadRolesCombobox(xmlStructNode);
                }
                if (rcLinks.Items.Count == 1)
                {
                    LoadPredefinedUrls();
                }
                xmlStructNode.SetAttributeValue("Text", Request.Form[txtText.UniqueID]);
                xmlStructNode.SetAttributeValue("Tooltip", Request.Form[txtTextTool.UniqueID]);
                xmlStructNode.SetAttributeValue("Value", nodeValue);
                xmlStructNode.SetAttributeValue("RolesVisibility", GetRoleVisibility());
                //Attention RadCombobox Returns the text and not the Value of the selected Combo
                xmlStructNode.SetAttributeValue("Target", rcTarget.FindItemByText(Request.Form[rcTarget.UniqueID]).Value);

                xmlLanguageNode.SetAttributeValue("Text", Request.Form[txtText.UniqueID]);
                xmlLanguageNode.SetAttributeValue("Tooltip", Request.Form[txtTextTool.UniqueID]);
                xmlLanguageNode.SetAttributeValue("Value", nodeValue);

                string rcLinksSelectedVal = GetRadComboboxValue(rcLinks);

                if (rcLinksSelectedVal == "URL")
                {
                    string url = Request.Form[txtUrl.UniqueID];
                    xmlLanguageNode.SetAttributeValue("NavigateUrl", url);
                    xmlStructNode.SetAttributeValue("ObjectUrl", string.Empty);
                    xmlStructNode.SetAttributeValue("PredefinedUrl", string.Empty);
                }
                else if (rcLinksSelectedVal.StartsWith("OT_"))
                {
                    string url = Request.Form[txtUrl.UniqueID];
                    xmlLanguageNode.SetAttributeValue("NavigateUrl", url);
                    xmlStructNode.SetAttributeValue("ObjectUrl", rcLinksSelectedVal.Substring(3));
                    xmlStructNode.SetAttributeValue("PredefinedUrl", string.Empty);
                }
                else
                {
                    var xPredefinedNavies = Constants.Links[rcLinksSelectedVal];
                    if (xPredefinedNavies != null)
                    {
                        string url = xPredefinedNavies.Url;
                        xmlLanguageNode.SetAttributeValue("NavigateUrl", url);
                        string localizationBaseFileName = "Navigation";
                        if (!string.IsNullOrEmpty(xPredefinedNavies.LocalizationBaseFileName))
                        {
                            localizationBaseFileName = xPredefinedNavies.LocalizationBaseFileName;
                        }

                        xmlLanguageNode.SetAttributeValue("Text", GuiLanguage.GetGuiLanguage(localizationBaseFileName).GetString(xPredefinedNavies.UrlTextKey) ?? Request.Form[txtText.UniqueID]);

                    }
                    else
                    {
                        xmlLanguageNode.SetAttributeValue("NavigateUrl", null);
                    }
                    xmlStructNode.SetAttributeValue("PredefinedUrl", rcLinksSelectedVal);
                    xmlStructNode.SetAttributeValue("ObjectUrl", string.Empty);
                }
                csb.hisp_Navigation_SaveNavigationStructure(currentNavId, null, xmlStruct.ToString());
                lbtnG.Text = language.GetString("TooltipNotSynchronized");
                lbtnG.CssClass = "icon navnotsynchro";

                csb.hisp_Navigation_SaveNavigationLanguage(currentNavId, CultureHandler.GetCurrentNeutralLanguageCode(), xmlLanguage.ToString(), string.Empty);
                rtv1.SelectedNode.Text = xmlLanguageNode.Attribute("Text").Value;
                ShowNodeEdit(Request.Form[hidCurrentNodeValue.UniqueID], true);
            }
        }

        protected void lbtnSaveNavi_Click(object sender, EventArgs e)
        {

            CSBooster_DataContext csb = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            Guid currentNavId = new Guid(hidCurrNavID.Value);
            string navName = Request.Form[txtNavName.UniqueID];
            if (!string.IsNullOrEmpty(navName))
            {
                navName = navName.StripHTMLTags().ToValidFilenameForWeb().Replace(" ", "").Trim().CropString(50);
                if (navName.Length > 0)
                {
                    csb.hisp_Navigation_SaveNavigationKeyName(currentNavId, navName);
                    rtv1.Nodes[0].Text = navName;
                    txtNavName.Text = navName;
                }
            }
        }

        protected void lbtnG_Click(object sender, EventArgs e)
        {

            Guid currentNavId = new Guid(hidCurrNavID.Value);
            string[] allRoles = string.Format("{0},Anonymous", string.Join(",", Roles.GetAllRoles())).Split(',');

            foreach (string langCode in SiteConfig.NeutralLanguages.Keys)
            {
                foreach (string role in allRoles)
                {
                    DataAccess.Business.Navigation.GeneratePreCacheNavigation(currentNavId, langCode, role);
                    foreach (int currNavT in Enum.GetValues(typeof(DataAccess.Business.Navigation.NavigationType)))
                    {
                        Cache.Remove(string.Format("{0}_{1}_{2}_{3}", currentNavId, langCode.ToLower(), role.ToLower(), currNavT));
                    }
                }
            }

            Response.Redirect(string.Format("/Admin/NavigationsEdit.aspx?NavID={0}&Src={1}", currentNavId, returnUrl));
        }

    }
}
