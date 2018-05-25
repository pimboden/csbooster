<%@ Page Language="C#" MasterPageFile="~/MasterPages/MobileMaster.master" AutoEventWireup="true" CodeBehind="EditForumTopicItem.aspx.cs" Inherits="_4screen.CSB.WebUI.M.Admin.EditForumTopicItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CpCnt" runat="Server">
    <ul class="itemList">
        <li><a class="back" href="javascript:history.back()">
            <%=languageShared.GetString("CommandCancel")%></a> <span class="title">
                <asp:Literal ID="LitTitle" runat="server" /></span> </li>
        <li class="detail">
            <asp:Panel ID="PnlInputAnonymous" runat="server" Visible="false">
                <div class="inputLabel inputMandatory">
                    <%=language.GetString("LabelName")%>
                </div>
                <div class="inputContent">
                    <asp:TextBox ID="TxtName" runat="server" MaxLength="32" />
                </div>
                <div class="inputLabel inputMandatory">
                    <%=language.GetString("LabelEmail")%>
                </div>
                <div class="inputContent">
                    <asp:TextBox ID="TxtEmail" runat="server" MaxLength="50" />
                </div>
            </asp:Panel>
            <div class="inputLabel inputMandatory">
                <%=language.GetString("LabelForumTopicItemText")%>
            </div>
            <div class="inputContent">
                <asp:TextBox ID="TxtDesc" TextMode="MultiLine" Height="150" runat="server" />
                <label class="hidden" for="codeBox">
                    <%=language.GetString("TextDontFillBelow")%>
                </label>
                <input type="text" class="hidden" id="codeBox" name="codeBox" />
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
