<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="FunctionsSort.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.FunctionsSort" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label1" LanguageFile="WidgetFunctionsSort" LabelKey="LabelSort" TooltipKey="TooltipSort" runat="server">
        </web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <div>
            <asp:CheckBox ID="CbxByTitle" runat="server" /><label for="<%=CbxByTitle.ClientID %>"><%=language.GetString("CommandByTitle")%></label>
        </div>
        <div>
            <asp:CheckBox ID="CbxByActivity" runat="server" /><label for="<%=CbxByActivity.ClientID %>"><%=language.GetString("CommandByActivity")%></label>
        </div>
        <div>
            <asp:CheckBox ID="CbxByMember" runat="server" /><label for="<%=CbxByMember.ClientID %>"><%=language.GetString("CommandByMember")%></label>
        </div>
        <div>
            <asp:CheckBox ID="CbxByDate" runat="server" /><label for="<%=CbxByDate.ClientID %>"><%=language.GetString("CommandByDate")%></label>
        </div>
        <div>
            <asp:CheckBox ID="CbxByVisits" runat="server" /><label for="<%=CbxByVisits.ClientID %>"><%=language.GetString("CommandByVisits")%></label>
        </div>
        <div>
            <asp:CheckBox ID="CbxByRatings" runat="server" /><label for="<%=CbxByRatings.ClientID %>"><%=language.GetString("CommandByRatings")%></label>
        </div>
        <div>
            <asp:CheckBox ID="CbxByRatingConsolidated" runat="server" /><label for="<%=CbxByRatingConsolidated.ClientID %>"><%=language.GetString("CommandByRatingConsolidated")%></label>
        </div>
        <div>
            <asp:CheckBox ID="CbxByComments" runat="server" /><label for="<%=CbxByComments.ClientID %>"><%=language.GetString("CommandByComments")%></label>
        </div>
        <div>
            <asp:CheckBox ID="CbxByLinks" runat="server" /><label for="<%=CbxByLinks.ClientID %>"><%=language.GetString("CommandByLinks")%></label>
        </div>
        <div>
            <asp:CheckBox ID="CbxNoSort" runat="server" /><label for="<%=CbxNoSort.ClientID %>"><%=language.GetString("CommandSetDefault")%></label>
        </div>
    </div>
    <div class="inputBlockError">
    </div>
</div>
