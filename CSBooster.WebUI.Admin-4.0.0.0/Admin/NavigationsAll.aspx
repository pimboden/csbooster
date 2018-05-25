<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteAdmin.Master" AutoEventWireup="true"
    CodeBehind="NavigationsAll.aspx.cs" Inherits="_4screen.CSB.WebUI.Admin.NavigationsAll" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="csb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Cnt1" runat="server">
    <csb:Pager ID="PAGTOP" ItemNamePlural="Inhalte" ItemNameSingular="Inhalt" PageSize="30"
        runat="server" />
    <div class="clearBoth">
    </div>
    <asp:Repeater ID="RepNav" runat="server" OnItemDataBound="OnRepNavItemDataBound">
        <ItemTemplate>
            <asp:Panel ID="pnlItem" runat="server" class="CSB_admin_nav_item CSB_admin_nav_item_row">
                <div class="col1Nav">
                    <div class="title">
                        <asp:Literal id="litName" runat="server" />
                    </div>
                </div>
                <div class="col2Nav">
                    <div class="func">
                        <asp:PlaceHolder ID="PhFunc" runat="server" />
                    </div>
                </div>
            </asp:Panel>
        </ItemTemplate>
    </asp:Repeater>
    <asp:HiddenField ID="htCurrentPage" runat="server" />
    <asp:HiddenField ID="htNumberItems" runat="server" />
</asp:Content>
