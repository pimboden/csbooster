<%@ Page Language="C#" MasterPageFile="~/MasterPages/MobileMaster.master" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.M.Admin.MyFriends" CodeBehind="MyFriends.aspx.cs" %>

<%@ Register Src="~/M/Admin/UserControls/Friends.ascx" TagName="Friends" TagPrefix="csb" %>
<asp:Content ID="CNT2" ContentPlaceHolderID="CpCnt" runat="Server">
    <ul class="itemList">
        <li><a class="back" href="/M/Admin/Dashboard.aspx"><%=language.GetString("TitleDashboard")%></a><span class="title"><%=language.GetString("TitleFriends")%></span></li>
        <li class="detail dashboard">
            <csb:Friends ID="myFriends" FriendsType="AllFriends" runat="server" />
        </li>
    </ul>
</asp:Content>
