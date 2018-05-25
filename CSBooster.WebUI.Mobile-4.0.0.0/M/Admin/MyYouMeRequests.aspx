<%@ Page Language="C#" MasterPageFile="~/MasterPages/MobileMaster.master" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.M.Admin.MyYouMeRequests" CodeBehind="MyYouMeRequests.aspx.cs" %>

<%@ Register Src="~/M/Admin/UserControls/Requests.ascx" TagName="Requests" TagPrefix="csb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CpCnt" runat="Server">
    <ul class="itemList">
        <li><a class="back" href="/M/Admin/Dashboard.aspx"><%=language.GetString("TitleDashboard")%></a><span class="title"><%=language.GetString("TitleFriendshipRequests")%></span></li>
        <li class="detail dashboard">
            <csb:Requests ID="receivedRequests" RequestType="YouMe" runat="server" />
        </li>
    </ul>
</asp:Content>
