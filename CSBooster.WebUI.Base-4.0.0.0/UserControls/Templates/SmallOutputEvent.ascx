<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.SmallOutputEvent" CodeBehind="SmallOutputEvent.ascx.cs" %>
<div class="eventOutput">
    <div class="eventOutputImage">
        <asp:HyperLink ID="LnkImg" runat="server" Visible="true">
            <asp:Image ID="Img" runat="server" />
        </asp:HyperLink>
    </div>
    <div class="eventOutputInfo">
        <div class="eventOutputDate">
            <asp:Literal ID="LitDate" runat="server" />
        </div>
        <div class="eventOutputTitle">
            <asp:HyperLink ID="LnkTitle" runat="server" />
        </div>
        <div class="eventOutputDesc">
            <asp:Literal ID="LitDesc" runat="server" />
        </div>
        <div class="eventOutputDesc">
            <asp:Literal ID="LitType" runat="server" />
        </div>
        <div class="eventOutputDesc">
            <asp:Literal ID="LitPrice" runat="server" />
        </div>
    </div>
</div>
