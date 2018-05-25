<%@ Page Language="C#" MasterPageFile="~/MasterPages/Empty.master" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.Pages.Popups.Message" CodeBehind="Message.aspx.cs" %>

<%@ Register Src="~/UserControls/Templates/SmallOutputUser2.ascx" TagName="SmallUserOutput" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Cnt1" runat="Server">
    <div id="popup" style="width: 450px;">
        <div style="float: right;">
            <div style="float: left; margin-right: 5px; line-height: 55px; font-weight: bold;">
                <asp:Literal ID="LitSenderReceiver" runat="server" />
            </div>
            <div style="float: left; text-align: center;">
                <asp:Literal ID="YMREC" runat="server" />
            </div>
        </div>
        <div class="inputTitle">
            <%=language.GetString("LableMessageSubject")%>
        </div>
        <div class="inputBlock">
            <asp:Literal ID="MSGSUB" runat="server" />
        </div>
        <div class="inputTitle">
            <%=language.GetString("LableMessage")%></div>
        <div class="inputBlock">
            <asp:Literal ID="MSGCNT" runat="server" />
        </div>
        <div class="inputBlock">
            <asp:HyperLink ID="REPLYLINK" CssClass="inputButton" Visible="false" runat="server"><%=languageShared.GetString("CommandReplay")%></asp:HyperLink>
            <asp:LinkButton ID="LbtnBlockUser" CssClass="inputButtonSecondary" OnClick="OnBlockUserClick" Visible="false" runat="server"><%=languageShared.GetString("CommandBlock")%></asp:LinkButton>
            <asp:LinkButton ID="CNCLBTN" CssClass="inputButtonSecondary" OnClientClick="GetRadWindow().Close();" runat="server"><%=languageShared.GetString("CommandClose")%></asp:LinkButton>
        </div>
        <asp:Literal ID="LitScript" runat="server" />
    </div>
</asp:Content>
