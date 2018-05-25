<%@ Page Language="C#" MasterPageFile="~/MasterPages/MobileMaster.master" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.M.Admin.MessageSend" CodeBehind="MessageSend.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CpCnt" runat="Server">
    <ul class="itemList">
        <li>
            <asp:HyperLink ID="lnkBack" runat="server" CssClass="back">
                    <%=languageShared.GetString("CommandBack")%>
            </asp:HyperLink>
            <span class="title">
                <%=language.GetString("TitleNewMessage")%>
            </span></li>
        <li class="detail dashboard">
            <div class="inputLabel">
                <%=language.GetString("LabelReceiver")%>
            </div>
            <div class="inputContent">
                <asp:Literal ID="litReceiver" runat="server" />
                <asp:DropDownList ID="ddReceiver" runat="server" />
            </div>
            <div class="inputLabel">
                <%=language.GetString("LabelMessageSubject")%>
            </div>
            <div class="inputContent">
                <asp:TextBox ID="txtSubject" TextMode="SingleLine" runat="server" />
            </div>
            <div class="inputLabel">
                <%=language.GetString("LabelMessageBody")%>
            </div>
            <div class="inputContent">
                <asp:TextBox ID="txtBody" TextMode="MultiLine" Rows="3" runat="server" />
            </div>
            <asp:Panel ID="pnlStatus" runat="server" CssClass="inputContent error">
                <asp:Literal ID="litStatus" runat="server" />
            </asp:Panel>
            <div>
                <asp:LinkButton ID="lbtnReceivers" runat="server" CssClass="button" OnClick="OnSendClick">
                <%=languageShared.GetString("CommandSend")%>
                </asp:LinkButton>
            </div>
        </li>
    </ul>
</asp:Content>
