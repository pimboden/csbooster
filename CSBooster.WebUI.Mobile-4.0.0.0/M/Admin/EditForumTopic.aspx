<%@ Page Language="C#" MasterPageFile="~/MasterPages/MobileMaster.master" AutoEventWireup="true" CodeBehind="EditForumTopic.aspx.cs" Inherits="_4screen.CSB.WebUI.M.Admin.EditForumTopic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CpCnt" runat="Server">
    <ul class="itemList">
        <li><a class="back" href="javascript:history.back()"><%=languageShared.GetString("CommandCancel")%></a> <span class="title">
            <asp:Literal ID="LitTitle" runat="server" /></span> </li>
        <li class="detail">
            <div class="inputLabel inputMandatory">
                <%=language.GetString("LabelForumTopicTitle")%>
            </div>
            <div class="inputContent">
                <asp:TextBox ID="TxtTitle" runat="server" />
            </div>
            <div class="inputLabel">
                <%=language.GetString("LabelForumTopicDescription")%>
            </div>
            <div class="inputContent">
                <asp:TextBox ID="TxtDesc" TextMode="MultiLine" Height="150" runat="server" />
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
