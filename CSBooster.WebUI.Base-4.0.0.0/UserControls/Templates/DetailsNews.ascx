<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.DetailsNews" CodeBehind="DetailsNews.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div>
    <div style="margin-bottom: 10px;">
        <asp:Literal ID="LitDate" runat="server" />
    </div>
    <div style="font-weight: bold; margin-bottom: 10px;">
        <asp:Literal ID="LitDescription" runat="server" />
    </div>
    <div style="margin-bottom: 10px;">
        <asp:Literal ID="LitContent" runat="server" />
    </div>
    <div style="margin-bottom: 10px;">
        <div>
            <%=language.GetString("LableNewsDetailLink")%>
        </div>
        <asp:Literal ID="LitLinks" runat="server" />
    </div>
    <div style="margin-bottom: 10px;">
        <asp:PlaceHolder ID="PhNewsImgs" runat="server" />
    </div>
    <div class="clearBoth">
    </div>
    <asp:Literal ID="INFOLIT" runat="server" />
</div>
<telerik:RadToolTipManager ID="RTTM" runat="server" Width="300" Height="200" OnAjaxUpdate="OnAjaxUpdate" ShowCallout="False" Sticky="True">
</telerik:RadToolTipManager>
<telerik:RadToolTipManager ShowEvent="OnClick" runat="server" ID="RTTMIMG" EnableViewState="false" ShowCallout="false" Position="BottomRight" RelativeTo="Element" Animation="None" ShowDelay="0" Sticky="true" OnAjaxUpdate="OnAjaxUpdate">
</telerik:RadToolTipManager>
