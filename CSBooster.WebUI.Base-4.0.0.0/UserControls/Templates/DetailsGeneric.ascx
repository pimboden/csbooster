<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.DetailsGeneric" CodeBehind="DetailsGeneric.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div>
    <asp:Literal ID="LitContent" runat="server" />
    <asp:Literal ID="INFOLIT" runat="server" />
</div>
<telerik:RadToolTipManager ID="RTTM" runat="server" Width="300" Height="200" OnAjaxUpdate="OnAjaxUpdate" ShowCallout="False" Sticky="True">
</telerik:RadToolTipManager>
