<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.DetailsDocument" CodeBehind="DetailsDocument.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div>
    <div>
        <asp:Literal ID="DESCLIT" runat="server" />
    </div>
    <div>
        <asp:Literal ID="LitDownload" runat="server" />
    </div>
</div>
<telerik:RadToolTipManager ID="RTTM" runat="server" Width="300" Height="200" OnAjaxUpdate="OnAjaxUpdate" ShowCallout="False" Sticky="True">
</telerik:RadToolTipManager>
