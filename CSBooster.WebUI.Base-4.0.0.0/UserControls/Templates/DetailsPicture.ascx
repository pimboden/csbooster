<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.DetailsPicture" CodeBehind="DetailsPicture.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="pictureDetail">
    <div>
        <asp:Image ID="Img" runat="server" EnableViewState="false" />
    </div>
    <div class="pictureCopyright">
        <asp:Literal ID="LitCopyright" runat="server" />
    </div>
    <asp:Panel ID="PnlDesc" runat="server" Visible="false" CssClass="pictureDescription">
        <asp:Literal ID="LitDesc" runat="server" />
    </asp:Panel>
</div>
<telerik:RadToolTipManager ID="RTTM" runat="server" Width="300" Height="200" OnAjaxUpdate="OnAjaxUpdate" ShowCallout="False" Sticky="True" />
