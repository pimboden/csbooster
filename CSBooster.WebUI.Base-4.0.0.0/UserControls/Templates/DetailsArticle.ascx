<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.DetailsArticle" CodeBehind="DetailsArticle.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div>
    <div>
        <asp:Literal ID="DETSUBTITLE" runat="server" />
    </div>
    <div>
        <asp:Literal ID="ARTTXTLIT" runat="server" />
    </div>
    <div>
        <asp:Literal ID="ARTIMGLIT" runat="server" />
    </div>
    <asp:Literal ID="INFOLIT" runat="server" />
</div>
<div class="clearBoth">
</div>
<telerik:RadToolTipManager ID="RTTM" runat="server" Width="300" Height="200" OnAjaxUpdate="OnAjaxUpdate" ShowCallout="False" Sticky="True">
</telerik:RadToolTipManager>
<telerik:RadToolTipManager ShowEvent="OnClick" runat="server" ID="RTTMIMG" EnableViewState="false" ShowCallout="false" Position="Center" RelativeTo="Element" Animation="None" ShowDelay="0" Sticky="true" OnAjaxUpdate="OnAjaxUpdate">
</telerik:RadToolTipManager>
