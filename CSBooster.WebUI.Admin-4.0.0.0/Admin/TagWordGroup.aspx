<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/SiteAdmin.Master" CodeBehind="TagWordGroup.aspx.cs" Inherits="_4screen.CSB.WebUI.Admin.TagWordGroup" %>

<asp:Content ID="Cnt" ContentPlaceHolderID="Cnt1" runat="server">
    <asp:PlaceHolder ID="phOTOR" runat="server" />
    <div class="clearBoth">
    </div>
    <div style='float: right'>
        <asp:LinkButton ID="lbtnCancel" CssClass="CSB_button4" runat="server" OnClick="lbtnCancel_Click"><%=languageShared.GetString("CommandCancel")%></asp:LinkButton>
    </div>
    <div style='float: right'>
        <asp:LinkButton ID="lbtnSave" CssClass="CSB_button4" runat="server" OnClick="lbtnSave_Click"><%=languageShared.GetString("CommandSave")%></asp:LinkButton>
    </div>
    <div class="clearBoth">
    </div>
</asp:Content>
