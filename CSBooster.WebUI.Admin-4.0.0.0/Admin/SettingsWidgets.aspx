<%@ Page Language="C#" MasterPageFile="~/MasterPages/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="SettingsWidgets.aspx.cs" Inherits="_4screen.CSB.WebUI.Admin.SettingsWidgets" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<asp:Content ID="Cnt" ContentPlaceHolderID="Cnt1" runat="server">
    <div>
        <asp:Repeater ID="RepWidgets" runat="server" OnItemDataBound="RepWidgetsItemDataBound">
            <HeaderTemplate>
                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                    <tr>
                        <td class="CSB_admin_title2"><%=language.GetString("LableWidgetName")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipWidgetName")%>">&nbsp;&nbsp;&nbsp;</a></td>
                        <td class="CSB_admin_title2"><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipWidgetGroup")%>">&nbsp;&nbsp;&nbsp;</a></td>
                        <td class="CSB_admin_title2"><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipWidgetSort")%>">&nbsp;&nbsp;&nbsp;</a></td>
                        <td class="CSB_admin_title2"><%=language.GetString("LableWidgetRoles")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipWidgetRoles")%>">&nbsp;&nbsp;&nbsp;</a></td>
                        <td class="CSB_admin_title2"><%=language.GetString("LableWidgetCommunities")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipWidgetCommunities")%>">&nbsp;&nbsp;&nbsp;</a></td>
                        <td class="CSB_admin_title2"><%=language.GetString("LableWidgetPageType")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipWidgetPageType")%>">&nbsp;&nbsp;&nbsp;</a></td>
                        <td class="CSB_admin_title2"><%=language.GetString("LableWidgetSettings")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipWidgetSettings")%>">&nbsp;&nbsp;&nbsp;</a></td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr id="widgetRow" runat="server" class="CSB_admin_widget_row">
                    <td>
                        <asp:Literal ID="LitName" runat="server" />
                    </td>
                    <td><asp:TextBox ID="TxtGroupId" Width="20" runat="server" /></td>
                    <td><asp:TextBox ID="TxtOrderKey" Width="20" runat="server" /></td>
                    <td><asp:TextBox ID="TxtRoles" Width="100" runat="server" /></td>
                    <td><asp:TextBox ID="TxtCommunities" Width="100" runat="server" /></td>
                    <td><asp:TextBox ID="TxtPageTypes" Width="100" runat="server" /></td>
                    <td><asp:TextBox ID="TxtSettings" runat="server" /></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div>
        <asp:LinkButton ID="LbtnSave" CssClass="CSB_admin_button" runat="server" OnClick="OnSaveClick"><%=GuiLanguage.GetGuiLanguage("Shared").GetString("CommandSave")%></asp:LinkButton>
    </div>
    <asp:Panel ID="PnlMsg" runat="server" CssClass="CSB_admin_error" Visible="false">
        <asp:Label ID="LitMsg" runat="server" />
    </asp:Panel>
</asp:Content>
