<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserActivities.ascx.cs" Inherits="_4screen.CSB.Widget.UserActivities" %>
<%@ Register Assembly="CSBooster.WidgetControls" Namespace="_4screen.CSB.WidgetControls" TagPrefix="csb" %>
<asp:Panel ID="PnlInput" runat="server" Visible="true">
    <p>
        <%=language.GetString("LableAddText")%><br />
        <asp:TextBox ID="TxtInput" runat="server" Width="97%" MaxLength="256" /><br/>
        <asp:LinkButton ID="LbtnInput" Style="float: left; margin-right: 5px;" CssClass="CTY-btn-150" OnClick="LbtnInput_Click1" Text="Speichern" runat="server"><%=languageShared.GetString("CommandAdd")%></asp:LinkButton>
    </p>    
</asp:Panel>  
<asp:Panel ID="PnlCnt" runat="server">
</asp:Panel>
