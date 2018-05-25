<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyContentWidgetBox.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.MyContentWidgetBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="CSBooster.CustomDragDrop" Namespace="CustomDragDrop" TagPrefix="cdd" %>
<asp:Panel ID="WT" CssClass="myContentWidgetBox" runat="server">
    <asp:Panel ID="WTH" CssClass="myContentWidgetBoxContent" runat="server">
        <asp:Panel CssClass="myContentWidgetBoxTitle" ID="PnlTitle" runat="server" />
        <div class="myContentWidgetBoxImage">
            <asp:Image ID="Img" runat="server" />
        </div>
    </asp:Panel>
</asp:Panel>
<cdd:CustomFloatingBehaviorExtender ID="WTFB" DragHandleID='WTH' TargetControlID='WT' runat="server" />
