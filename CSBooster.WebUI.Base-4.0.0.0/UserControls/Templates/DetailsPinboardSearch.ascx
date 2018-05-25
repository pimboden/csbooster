<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.DetailsPinboardSearch" CodeBehind="DetailsPinboardSearch.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div>
    <div>
        <div style="margin-bottom: 10px;">
            <asp:Literal ID="DESCLIT" runat="server" />
        </div>
        <div style="margin-bottom: 10px;">
            <asp:Literal ID="PRICELIT" runat="server" />
        </div>
        <div>
            <asp:HyperLink CssClass="inputButton" ID="CTCTBTN" runat="server"><%=language.GetString("CommandPinboardContact")%></asp:HyperLink>
        </div>
    </div>
    <asp:Literal ID="INFOLIT" runat="server" />
</div>
<telerik:RadToolTipManager ID="RTTM" runat="server" Width="300" Height="200" OnAjaxUpdate="OnAjaxUpdate" ShowCallout="False" Sticky="True">
</telerik:RadToolTipManager>
