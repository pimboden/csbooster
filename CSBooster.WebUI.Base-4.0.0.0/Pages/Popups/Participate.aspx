<%@ Page Language="C#" MasterPageFile="~/MasterPages/Empty.master" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.Pages.Popups.Participate" CodeBehind="Participate.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cnt1" runat="Server">
    <div id="popup" style="width: 450px;">
        <div class="inputBlock">
            <%=language.GetString("TitleBecomeAMember")%>
        </div>
        <div class="inputBlock">
            <asp:LinkButton ID="yesButton" CssClass="inputButton" OnClick="OnYesClick" runat="server"><%=languageShared.GetString("CommandBecomeAMember")%></asp:LinkButton>
            <asp:LinkButton ID="noButton" CssClass="inputButtonSecondary" OnClientClick="GetRadWindow().Close();" runat="server"><%=languageShared.GetString("CommandCancel")%></asp:LinkButton>
        </div>
    </div>
    <asp:Literal ID="LitScript" runat="server"></asp:Literal>
</asp:Content>
