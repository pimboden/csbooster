<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.Login" CodeBehind="Login.ascx.cs" %>
<asp:Panel ID="PnlLogins" runat="server">
    <asp:Panel ID="PnlLogin" runat="server" Style="float: left; width: 100%;">
        <div class="inputTitle">
            <%=language.GetString("LableLogin")%>
        </div>
        <div class="inputBlock3">
            <asp:TextBox ID="txtLogin" CssClass="CSB_textbox" Width="98%" runat="server" />
        </div>
        <div class="inputTitle">
            <%=language.GetString("LablePassword")%>
        </div>
        <div class="inputBlock3">
            <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="CSB_textbox" Width="98%" runat="server" />
        </div>
        <div class="inputBlock3">
            <asp:PlaceHolder ID="phRemeberMe" runat="server" />
        </div>
        <asp:Panel ID="pnlStatus" runat="server" Visible="false" CssClass="inputBlock3 errorText">
            <asp:Literal runat="server" ID="litStatus" />
        </asp:Panel>
        <div class="inputBlock3">
            <asp:LinkButton ID="btnLogin" OnClientClick="RemoveNodeByID('SecXmlToken');" CssClass="inputButton" OnClick="LoginButtonClick" runat="server"><%=language.GetString("CommandLogin")%></asp:LinkButton>
        </div>
        <div class="inputBlock3">
            <a href="javascript:radWinOpen('/Pages/Popups/passwordrecover.aspx', '<%=language.GetString("CommandPasswordForget")%>', 430, 200, false, null);">
                <%=language.GetString("CommandPasswordForget")%>
            </a>
        </div>
    </asp:Panel>
</asp:Panel>
<div class="clearBoth">
</div>
