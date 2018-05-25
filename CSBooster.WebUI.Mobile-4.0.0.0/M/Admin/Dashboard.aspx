<%@ Page Language="C#" MasterPageFile="~/MasterPages/MobileMaster.master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="_4screen.CSB.WebUI.M.Admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CpCnt" runat="Server">
    <ul class="itemList">
        <li><a class="back" href="/mpdefault.aspx"><%=language.GetString("TitleHome")%></a><span class="title"><%=language.GetString("TitleDashboard")%></span></li>
        <li><a href="/M/Admin/MyMessagesInbox.aspx" class="goto2"><%=language.GetString("TitleMessageInbox")%></a> <a href="/M/Admin/MyMessagesOutbox.aspx" class="goto2"><%=language.GetString("TitleMessageOutbox")%></a> <a href="/M/Admin/MessageSend.aspx" class="goto2"><%=language.GetString("CommandNewMessage")%></a></li>
        <li><a href="/M/Admin/MyFriends.aspx" class="goto2"><%=language.GetString("TitleFriends")%></a> <a href="/M/Admin/MyYouMeRequests.aspx" class="goto2"><%=language.GetString("TitleFriendshipRequests")%></a></li>
        <li><a href="/Pages/Other/Logout.ashx?SM=Mobile" class="goto2"><%=language.GetString("CommandLogout")%></a></li>
    </ul>
</asp:Content>
