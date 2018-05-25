<%@ Page Language="C#" MasterPageFile="~/MasterPages/MobileMaster.master" AutoEventWireup="true" CodeBehind="EditEvent.aspx.cs" Inherits="_4screen.CSB.WebUI.M.Admin.EditEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CpCnt" runat="Server">
    <ul class="itemList">
        <li><a class="back" href="javascript:history.back()"><%=languageShared.GetString("CommandCancel")%></a> <span class="title">
            <asp:Literal ID="LitTitle" runat="server" /></span> </li>
        <li class="detail">
            <div class="inputLabel inputMandatory">
                <%=language.GetString("LabelEventTitle")%>
            </div>
            <div class="inputContent">
                <asp:TextBox ID="TxtTitle" runat="server" />
            </div>
            <div class="inputLabel">
                <%=language.GetString("LabelEventDescription")%>
            </div>
            <div class="inputContent">
                <asp:TextBox ID="TxtDesc" TextMode="MultiLine" Height="50" runat="server" />
            </div>
            <div class="inputLabel">
                <%=language.GetString("LabelEventText")%>
            </div>
            <div class="inputContent">
                <asp:TextBox ID="TxtText" TextMode="MultiLine" Height="100" runat="server" />
            </div>
            <div class="inputLabel inputMandatory">
                <%=language.GetString("LabelEventDate")%>
            </div>
            <div class="inputContent">
                <asp:TextBox ID="TxtStartDate" runat="server" Style="width: 100px;" /> <asp:TextBox ID="TxtEndDate" runat="server" Style="width: 100px;" />
            </div>
            <div class="inputLabel">
                <%=language.GetString("LabelEventTime")%>
            </div>
            <div class="inputContent">
                <asp:TextBox ID="TxtTime" runat="server" />
            </div>
            <div class="inputLabel">
                <%=language.GetString("LabelEventAge")%>
            </div>
            <div class="inputContent">
                <asp:TextBox ID="TxtAge" runat="server" />
            </div>
            <div class="inputLabel">
                <%=language.GetString("LabelEventPrice")%>
            </div>
            <div class="inputContent">
                <asp:TextBox ID="TxtPrice" runat="server" />
            </div>
            <div class="inputLabel">
                <%=language.GetString("LabelEventChooseLocation")%>
            </div>
            <div class="inputContent">
                <asp:DropDownList ID="DdlLocations" runat="server" />
            </div>
            <div class="inputLabel">
                <%=language.GetString("LabelEventAddLocation")%>
            </div>
            <div class="inputContent">
                <asp:LinkButton ID="LbtnAddLocation" runat="server" CssClass="button" OnClick="OnAddLocationClick">
                <%=language.GetString("LabelAddLocation")%>
                </asp:LinkButton>
            </div>
            <div class="inputLabel inputMandatory">
                <%=language.GetString("LabelEventType")%>
            </div>
            <div class="inputContent">
                <asp:CheckBoxList ID="CblType" runat="server" Width="100%" CellPadding="0" CellSpacing="0" RepeatColumns="2" />
            </div>
            <asp:Panel ID="pnlStatus" runat="server" CssClass="inputContent error">
                <asp:Literal ID="litStatus" runat="server" />
            </asp:Panel>
            <div>
                <asp:LinkButton ID="lbtnSave" CssClass="button" OnClick="OnSaveClick" runat="server" />
            </div>
        </li>
    </ul>
</asp:Content>
