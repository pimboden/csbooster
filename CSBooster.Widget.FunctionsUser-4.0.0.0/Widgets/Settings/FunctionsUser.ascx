<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="FunctionsUser.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.FunctionsUser" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label1" LanguageFile="WidgetFunctionsUser" LabelKey="LabelVisibility" TooltipKey="TooltipVisibility" runat="server">
        </web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <div id="DivMessage" runat="server">
            <asp:CheckBox ID="CbxMessage" runat="server" /><label for="<%=CbxMessage.ClientID %>"><%=language.GetString("LabelMessage")%></label>
        </div>
        <div id="DivFriends" runat="server">
            <asp:CheckBox ID="CbxFriends" runat="server" /><label for="<%=CbxFriends.ClientID %>"><%=language.GetString("LabelFriends")%></label>
        </div>
        <div id="DivMembership" runat="server">
            <asp:CheckBox ID="CbxMembership" runat="server" /><label for="<%=CbxMembership.ClientID %>"><%=language.GetString("LabelMembership")%></label>
        </div>
        <div id="DivNotifications" runat="server">
            <asp:CheckBox ID="CbxNotifications" runat="server" /><label for="<%=CbxNotifications.ClientID %>"><%=language.GetString("LabelNotifications")%></label>
        </div>
        <div id="DivMap" runat="server">
            <asp:CheckBox ID="CbxMap" runat="server" /><label for="<%=CbxMap.ClientID %>"><%=language.GetString("LabeMap")%></label>
        </div>
    </div>
    <div class="inputBlockError">
    </div>
</div>
