<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.WidgetContainer" CodeBehind="WidgetContainer.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Panel ID="W" CssClass="widget" runat="server">
    <asp:Panel ID="pnlOwner" runat="server" CssClass="widgetHeader">
        <asp:Panel ID="WidgetHeader" runat="server" EnableViewState="false" CssClass="widgetHeaderMove" />
        <asp:HyperLink ID="WEdt" runat="server" CssClass="widgetHeaderEdit" />
        <asp:LinkButton ID="WCl" runat="Server" CssClass="widgetHeaderRemove" OnClick="OnDeleteClick" />
    </asp:Panel>
    <div class="widget_<%=ClassPrefix%>">
        <div class="top">
            <asp:Literal ID="LitTitle" runat="server" />
        </div>
        <asp:Panel ID="WBP" runat="Server" CssClass="cnt">
        </asp:Panel>
        <div class="bottom">
        </div>
    </div>
    <telerik:RadWindow ID="Rw" runat="server" VisibleOnPageLoad="true" AutoSize="true" DestroyOnClose="true" Modal="true" VisibleTitlebar="true" VisibleStatusbar="false" IconUrl="~/Library/Images/Layout/favicon.png" Behaviors="Resize, Close, Move" InitialBehaviors="None" Visible="false" />
</asp:Panel>
