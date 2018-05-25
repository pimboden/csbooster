<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteAdmin.Master" AutoEventWireup="true"
    CodeBehind="NavigationsEdit.aspx.cs" Inherits="_4screen.CSB.WebUI.Admin.NavigationsEdit" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Cnt1" runat="server">
<div class="topInfoPanel">
<div class="topInfoContent">
<asp:LinkButton CssClass="icon navnotsynchro" Text="Aktualisieren" id="lbtnG" runat="server" onclick="lbtnG_Click"><%=languageShared.GetString("CommandSave")%></asp:LinkButton>
</div>
</div>
    <asp:Panel ID="pnlEEditNav" runat="server"  style="clear: both;width:100%">
        <div style="float: left; width: 200px; height: 400px; overflow: scroll;">
            <telerik:RadTreeView ID="rtv1" runat="server" EnableDragAndDrop="True" EnableDragAndDropBetweenNodes="True"
                EnableEmbeddedBaseStylesheet="True" EnableEmbeddedScripts="True" OnNodeClick="rtv1_NodeClick"
                OnContextMenuItemClick="rtv1_ContextMenuItemClick" OnNodeDrop="rtv1_NodeDrop">
                <ContextMenus>
                    <telerik:RadTreeViewContextMenu ID="rtvCM" runat="server" CausesValidation="False">
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                        <Items>
                            <telerik:RadMenuItem runat="server" Text="Neuer Knoten" Value="NewNode">
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Neuer Unterknoten" Value="NewSubNode">
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Knoten Löschen" Value="DeleteNode">
                            </telerik:RadMenuItem>
                        </Items>
                    </telerik:RadTreeViewContextMenu>
                    <telerik:RadTreeViewContextMenu ID="rtvCMRoot" runat="server" CausesValidation="False">
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                        <Items>
                            <telerik:RadMenuItem runat="server" Text="Name ändern" Value="NewName">
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Neuer Knoten" Value="NewSubNode">
                            </telerik:RadMenuItem>
                        </Items>
                    </telerik:RadTreeViewContextMenu>
                </ContextMenus>
            </telerik:RadTreeView>
        </div>
        <asp:Panel ID="pnlNavNodeProp" runat="server" Visible="false" Style="float: left;
            width: 450px; padding-left: 10px">
            <table>
                <tr>
                    <td>
                        <%=language.GetString("LableNavigationLink")%>:
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtText" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <%=language.GetString("LableNavigationTooltip")%>:
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtTextTool" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <%=language.GetString("LableNavigationLink")%>:
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rcLinks" runat="server" Width="100%"
                            OnClientSelectedIndexChanged="ChangeLinkType">
                            <Items>
                                <telerik:RadComboBoxItem Text="URL" Value="URL" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUrl" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:HyperLink ID="lbtnT" runat="server" CssClass="CSB_button4"><%=language.GetString("CommandLanguage")%></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%=language.GetString("LableNavigationTarget")%>:
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rcTarget" runat="server" Width="100%">
                            <Items>
                                <telerik:RadComboBoxItem Text="Gleiches Fenster" Value="_self" />
                                <telerik:RadComboBoxItem Text="Neues Fenster" Value="_blank" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <%=language.GetString("LableNavigationVisibility")%>:
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rcRoles" runat="server" Width="100%" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnSaveNode" CssClass="CSB_button4" runat="server" OnClick="lbtnSaveNode_Click"><%=languageShared.GetString("CommandSave")%></asp:LinkButton>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlLinkPicker" runat="server">
                <asp:PlaceHolder runat="server" ID="phLinkPicker" />
            </asp:Panel>
            <asp:HiddenField ID="hidParentNodeId" runat="server" />
            <asp:HiddenField ID="hidCurrentNodeValue" runat="server" />
        </asp:Panel>
        <asp:Panel ID="pnlNaviNameEdit" runat="server" Visible="false" Style="float: left;
            width: 300px; padding-left: 10px">
            <table>
                <tr>
                    <td>
                        <%=language.GetString("LableNavigationName")%>:
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtNavName" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnSaveNavi" CssClass="CSB_button4" runat="server" OnClick="lbtnSaveNavi_Click"><%=languageShared.GetString("CommandSave")%></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div style="clear: both">
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hidCurrNavID" runat="server" Value="" />
</asp:Content>
