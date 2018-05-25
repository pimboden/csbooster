<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.SmallOutputVideo" CodeBehind="SmallOutputVideo.ascx.cs" %>
<div class="videoOutput">
    <div class="videoOutputImage">
        <asp:HyperLink ID="LnkImg" runat="server" Visible="true">
            <asp:Image ID="Img" runat="server" />
        </asp:HyperLink>
    </div>
    <div class="videoOutputInfo">
        <div class="videoOutputTitle">
            <asp:HyperLink ID="LnkTitle" runat="server" />
        </div>
        <div class="videoOutputAuthor">
            von <asp:HyperLink ID="LnkOwner" runat="server" />
        </div>
    </div>
</div>
