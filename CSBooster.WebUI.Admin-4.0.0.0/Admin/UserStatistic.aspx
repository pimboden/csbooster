<%@ Page Language="C#" MasterPageFile="~/MasterPages/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="UserStatistic.aspx.cs" Inherits="_4screen.CSB.WebUI.Admin.UserStatistic" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Cnt" ContentPlaceHolderID="Cnt1" runat="server">
    <div>
        <div style="float: left;">
            <%=language.GetString("LableUsername")%>
        </div>
        <div style="float: left; margin-left: 5px;">
            <asp:TextBox ID="txtUser" runat="server" />
        </div>
        <div style="float: left; margin-left: 10px;">
            <%=language.GetString("LableDateVon")%>
        </div>
        <div style="float: left; margin-left: 5px;">
            <telerik:RadDatePicker ID="RadDateFrom" runat="server" Width="95px" />
        </div>
        <div style="float: left; margin-left: 10px;">
            <%=language.GetString("LableDateBis")%>
        </div>
        <div style="float: left; margin-left: 5px;">
            <telerik:RadDatePicker ID="RadDateTo" runat="server" Width="95px" />
        </div>
        <div style="float: left; margin-left: 10px;">
            <asp:DropDownList ID="ddlType" runat="server">
                <asp:ListItem Text="Alle" Value="1" Selected="True"></asp:ListItem> 
                <asp:ListItem Text="Nur mit aktivitäten" Value="2"></asp:ListItem> 
                <asp:ListItem Text="Aktivitätenliste" Value="3"></asp:ListItem> 
            </asp:DropDownList>
        </div>
        
        <div style="float: left; margin-left: 15px;">
            <asp:LinkButton ID="LbtnExport" runat="server" CssClass="CSB_admin_button" OnClick="OnUserStatisticClick">Statistik herunterladen</asp:LinkButton>
        </div>
        <div class="clearBoth">
        </div>
    </div>
</asp:Content>