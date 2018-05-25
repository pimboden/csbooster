<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.SmallOutputLocation2" CodeBehind="SmallOutputLocation2.ascx.cs" %>
<div class="locationOutput locationOutput2">
    <div class="locationOutputTitle">
        <asp:HyperLink ID="LnkTitle" runat="server" />
    </div>
    <asp:Panel ID="PnlImage" runat="server" CssClass="locationOutputInfo" Visible="false">
        <asp:HyperLink ID="LnkImg" runat="server" Visible="true">
            <asp:Image ID="Img" runat="server" />
        </asp:HyperLink>
    </asp:Panel>
    <div class="locationOutputInfo">
        <asp:Literal ID="LitType" runat="server" />
    </div>
</div>
