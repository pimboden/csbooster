<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.M.UserControls.Templates.SmallOutputEvent" CodeBehind="SmallOutputEvent.ascx.cs" %>
<asp:HyperLink ID="lnkDetail" runat="server" CssClass="goto">
    <div class="outputObject">
        <div class="image">
            <asp:Literal ID="litImg" runat="server" />
        </div>
        <div class="date">
            <asp:Literal ID="litDate" runat="server" />
        </div>
        <div class="title">
            <asp:Literal ID="litTitle" runat="server" />
        </div>
        <div class="text">
            <asp:Literal ID="litDesc" runat="server" />
        </div>
    </div>
</asp:HyperLink>
