<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.M.Admin.UserControls.MessagePreview" CodeBehind="MessagePreview.ascx.cs" %>
<asp:LinkButton ID="lnkOpen" runat="server">
    <div class="date">
        <asp:Literal ID="litDate" runat="server" />
    </div>
    <div class="user">
        <asp:Literal ID="litUser" runat="server" />
    </div>
    <div class="messagePreview">
        <asp:Literal ID="litMsg" runat="server" />
    </div>
</asp:LinkButton>
