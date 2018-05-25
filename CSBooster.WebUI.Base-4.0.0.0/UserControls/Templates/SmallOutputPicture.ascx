<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.SmallOutputPicture" CodeBehind="SmallOutputPicture.ascx.cs" %>
<div class="pictureOutput">
    <div class="pictureOutputImage">
        <asp:HyperLink ID="LnkImg" runat="server" Visible="true">
            <asp:Image ID="Img" runat="server"/>
        </asp:HyperLink>
    </div>
    <div class="pictureOutputInfo">
        <div class="pictureOutputTitle">
            <asp:HyperLink ID="LnkTitle" runat="server" />
        </div>
        <div class="pictureOutputAuthor">
            von <asp:HyperLink ID="LnkOwner" runat="server" />
        </div>
    </div>
</div>
