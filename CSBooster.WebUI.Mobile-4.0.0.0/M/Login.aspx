<%@ Page Language="C#" MasterPageFile="~/MasterPages/MobileMaster.master" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.M.Login" CodeBehind="Login.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CpCnt" runat="Server">
    <ul class="itemList">
        <li>
            <div class="title">
                <%=language.GetString("TitleLogin") %>
            </div>
            <div class="inputLabel">
                <%=language.GetString("LabelLogin") %>
            </div>
            <div class="inputContent">
                <asp:TextBox ID="txtLogin" runat="server" />
            </div>
            <div class="inputLabel">
                <%=language.GetString("LabelPassword") %>
            </div>
            <div class="inputContent">
                <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" />
            </div>
            <asp:Panel ID="pnlStatus" runat="server" Visible="false" CssClass="inputContent error">
                <asp:Literal runat="server" ID="litStatus" />
            </asp:Panel>
            <div>
                <asp:Button ID="btnLogin" CssClass="button" OnClick="OnLoginClick" runat="server" />
            </div>
        </li>
    </ul>
</asp:Content>
