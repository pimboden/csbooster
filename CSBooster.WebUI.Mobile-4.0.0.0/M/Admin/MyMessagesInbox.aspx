<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MobileMaster.master" Inherits="_4screen.CSB.WebUI.M.Admin.MyMessagesInbox" CodeBehind="MyMessagesInbox.aspx.cs" %>

<%@ Register Src="~/M/Admin/UserControls/Msgbox.ascx" TagName="Msgbox" TagPrefix="csb" %>
<asp:Content ID="CNT" ContentPlaceHolderID="CpCnt" runat="Server">
    <ul class="itemList">
        <li><a class="back" href="/M/Admin/Dashboard.aspx"><%=language.GetString("TitleDashboard")%></a><span class="title"><%=language.GetString("TitleMessageInbox")%></span></li>
        <li class="detail dashboard">
            <csb:Msgbox ID="inbox" MsgboxType="Inbox" runat="server" />
        </li>
    </ul>
</asp:Content>
