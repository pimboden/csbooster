<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailsHTMLContent.ascx.cs" Inherits="_4screen.CSB.DataObj.UserControls.Templates.DetailsHTMLContent" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Literal ID="litDesc" runat="server" />
<telerik:RadToolTipManager ID="RTTM" runat="server" Width="300" Height="200" OnAjaxUpdate="OnAjaxUpdate"
    ShowCallout="False" Sticky="True">
</telerik:RadToolTipManager>
<telerik:RadToolTipManager ShowEvent="OnClick" runat="server" ID="RTTMIMG" EnableViewState="false"
    ShowCallout="false" Position="Center" RelativeTo="Element" Animation="None" ShowDelay="0"
    Sticky="true" OnAjaxUpdate="OnAjaxUpdate">
</telerik:RadToolTipManager>
