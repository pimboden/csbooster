<%@ Page Language="C#" MasterPageFile="~/MasterPages/MobileMaster.master" AutoEventWireup="true" CodeBehind="EditLocation.aspx.cs" Inherits="_4screen.CSB.WebUI.M.Admin.EditLocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CpCnt" runat="Server">
    <ul class="itemList">
        <li><a class="back" href="javascript:history.back()"><%=languageShared.GetString("CommandCancel")%></a> <span class="title">
            <asp:Literal ID="LitTitle" runat="server" /></span> </li>
        <li class="detail">
            <div class="inputLabel inputMandatory">
                <%=language.GetString("LabelLocationTitle")%>
            </div>
            <div class="inputContent">
                <asp:TextBox ID="TxtTitle" runat="server" />
            </div>
            <div class="inputLabel">
                <%=language.GetString("LabelLocationDescription")%>
            </div>
            <div class="inputContent">
                <asp:TextBox ID="TxtDesc" TextMode="MultiLine" Height="50" runat="server" />
            </div>
            <div class="inputLabel">
                <%=language.GetString("LabelLocationStreet")%>
            </div>
            <div class="inputContent">
                <asp:TextBox ID="TxtStreet" runat="server" />
            </div>
            <div class="inputLabel">
                <%=language.GetString("LabelLocationZipCode")%>
            </div>
            <div class="inputContent">
                <asp:TextBox ID="TxtZipCode" runat="server" />
            </div>
            <div class="inputLabel">
                <%=language.GetString("LabelLocationCity")%>
            </div>
            <div class="inputContent">
                <asp:TextBox ID="TxtCity" runat="server" />
            </div>
            <div class="inputLabel inputMandatory">
                <%=language.GetString("LabelLocationType")%>
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
