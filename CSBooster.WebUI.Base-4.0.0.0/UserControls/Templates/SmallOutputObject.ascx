<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.SmallOutputObject" CodeBehind="SmallOutputObject.ascx.cs" %>
<div class="objectOutput">
    <div class="objectOutputImage">
        <asp:HyperLink ID="LnkImg" runat="server" Visible="true">
            <asp:Image ID="Img1" runat="server" />
        </asp:HyperLink>
    </div>
    <asp:PlaceHolder ID="PhTitle" runat="server" EnableViewState="false" Visible="true">
        <div class="objectOutputTitle">
            <asp:HyperLink ID="LnkTitle" runat="server" />
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="PhDesc" runat="server" EnableViewState="false" Visible="false">
        <div class="objectOutputDesc">
            <asp:Literal ID="LitDesc" runat="server" />
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="PhAutor" runat="server" EnableViewState="false" Visible="false">
        <div class="objectOutputDesc">
            von
            <asp:HyperLink ID="LnkAutor" runat="server" />
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="PhCom" runat="server" EnableViewState="false" Visible="false">
        <div class="objectOutputDesc">
            <asp:Literal ID="litCom" runat="server" />
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="PhSDate" runat="server" EnableViewState="false" Visible="false">
        <div class="objectOutputDesc">
            <asp:Literal ID="LitSDate" runat="server" />
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="PhEDate" runat="server" EnableViewState="false" Visible="false">
        <div class="objectOutputDesc">
            <asp:Literal ID="LitEDate" runat="server" />
        </div>
    </asp:PlaceHolder>
</div>
