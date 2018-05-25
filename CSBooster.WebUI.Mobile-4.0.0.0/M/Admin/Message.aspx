<%@ Page Language="C#" MasterPageFile="~/MasterPages/MobileMaster.master" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.M.Admin.Message" CodeBehind="Message.aspx.cs" %>

<%@ Import Namespace="_4screen.Utils.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CpCnt" runat="Server">
    <ul class="itemList">
        <li>
            <asp:HyperLink ID="lnkBack" runat="server" CssClass="back">
                    <%=languageShared.GetString("CommandBack")%>
            </asp:HyperLink>
            <span class="title">
                <asp:Literal ID="litInfo" runat="server" />
            </span></li>
        <li class="detail dashboard">
            <div class="message">
                <div class="messageSubject">
                    <asp:Literal ID="litSubject" runat="server" />
                </div>
                <div class="messageBody">
                    <asp:Literal ID="litBody" runat="server" />
                </div>
            </div>
            <div>
                <asp:PlaceHolder ID="phReplyLink" runat="server" />
            </div>
        </li>
    </ul>
</asp:Content>
