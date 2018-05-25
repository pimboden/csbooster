<%@ Page Language="C#" MasterPageFile="~/MasterPages/MobileMaster.master" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.M.HtmlContent" CodeBehind="HtmlContent.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CpCnt" runat="Server">
    <ul class="itemList">
        <li><a href="javascript:history.back()" class="back">
            <%=language.GetString("CommandBack") %></a> </li>
        <li class="detail htmlContent">
            <div class="title">
                <asp:Literal ID="LitTitle" runat="server" />
            </div>
            <div>
                <asp:Literal ID="LitContent" runat="server" />
            </div>
        </li>
    </ul>
</asp:Content>
