<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminTopMenu.ascx.cs" Inherits="_4screen.CSB.WebUI.Admin.UserControls.AdminTopMenu" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadMenu ID="nav" runat="server">
    <defaultgroupsettings flow="Horizontal" />
<CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
    <Items>
        <telerik:RadMenuItem runat="server" Text="Root RadMenuItem1">
            <Items>
                <telerik:RadMenuItem runat="server" Text="Child RadMenuItem 1">
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" Text="Root RadMenuItem2">
            <Items>
                <telerik:RadMenuItem runat="server" Text="Child RadMenuItem 1">
                    <Items>
                        <telerik:RadMenuItem runat="server" Text="Root RadMenuItem3">
                        </telerik:RadMenuItem>
                    </Items>
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Child RadMenuItem 2">
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" Text="Root RadMenuItem4">
            <Items>
                <telerik:RadMenuItem runat="server" Text="Child RadMenuItem 1">
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>
    </Items>
</telerik:RadMenu>