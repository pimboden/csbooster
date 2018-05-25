<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SmallOutputUser.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.Templates.SmallOutputUser" %>
<div class="userOutput2">
    <div class="userOutput2Icon">
        <div class="userOutput2Color1">
            <asp:HyperLink ID="LnkImg1" runat="server">
                <asp:Image ID="Img1" runat="server" />
            </asp:HyperLink>
        </div>
        <div class="userOutput2Image">
            <asp:HyperLink ID="LnkImg2" runat="server">
                <asp:Image ID="Img2" CssClass="usericon" runat="server" />
            </asp:HyperLink>
        </div>
        <div class="userOutput2Color2">
            <asp:HyperLink ID="LnkImg3" runat="server">
                <asp:Image ID="Img3" runat="server" />
            </asp:HyperLink>
        </div>
    </div>
    <div class="userOutput2Info">
        <asp:HyperLink ID="LnkUser" runat="server" />
    </div>
    <div class="userOutput2Info">
        Alter:
        <asp:Literal ID="LitAge" runat="server" />
    </div>
    <div class="userOutput2Info">
        Aus:
        <asp:Literal ID="LitCity" runat="server" />
    </div>
</div>
