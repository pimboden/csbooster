<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.DetailsSlideShow" CodeBehind="DetailsSlideShow.ascx.cs" %>
<%@ Register Assembly="obout_Show_Net" Namespace="OboutInc.Show" TagPrefix="obshow" %>
<div>
    <div id="slideshow">
        <asp:Literal ID="LitSLShowObj" runat="server" EnableViewState="false" />
    </div>
    <asp:Literal ID="LitSLShowImg" runat="server" EnableViewState="false" />
    <obshow:Show ID="Show1" runat="server">
        <Changer Type="Both" ArrowType="Side1" Height="40" Width="200" HorizontalAlign="Left" />
    </obshow:Show>
    <asp:Literal ID="litCtrl" runat="server" />
    <asp:Literal ID="INFOLIT" runat="server" />
    <div class="desc">
        <asp:Literal ID="DETSUBTITLE" runat="server" />
    </div>
</div>
