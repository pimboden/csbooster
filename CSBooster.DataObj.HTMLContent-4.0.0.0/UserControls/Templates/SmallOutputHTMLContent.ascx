<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SmallOutputHTMLContent.ascx.cs" Inherits="_4screen.CSB.DataObj.UserControls.Templates.SmallOutputHTMLContent" %>
<div class="objectOutput">
    <div class="objectOutputImage">
        <asp:HyperLink ID="LnkImg" runat="server">
            <asp:Image ID="Img1" runat="server" />
        </asp:HyperLink>
    </div>
    <asp:PlaceHolder ID="PhTitle" runat="server" EnableViewState="false">
        <div class="objectOutputTitle">
            <asp:HyperLink ID="LnkTitle" runat="server" />
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="PhDesc" runat="server" EnableViewState="false">
        <div class="objectOutputDesc">
            <asp:Literal ID="LitDesc" runat="server" />
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="PhAutor" runat="server" EnableViewState="false">
        <div class="objectOutputDesc">
            von
            <asp:HyperLink ID="LnkAutor" runat="server" />
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="PhCom" runat="server" EnableViewState="false">
        <div class="objectOutputDesc">
            <asp:Literal ID="litCom" runat="server" />
        </div>
    </asp:PlaceHolder>
</div>
