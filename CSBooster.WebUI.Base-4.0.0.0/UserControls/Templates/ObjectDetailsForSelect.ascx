<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ObjectDetailsForSelect.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.Templates.ObjectDetailsForSelect" %>
<div class="objectOutput">
    <div class="objectOutputImage">
        <asp:Image ID="Img1" runat="server" />
    </div>
    <div class="objectOutputTitle">
        <asp:Literal ID="LitTile" runat="server" />
    </div>
    <div class="objectOutputDesc">
        <asp:Literal ID="LitDesc" runat="server"  Visible="false"/>
    </div>
</div>
