<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GoogleMap.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.GoogleMap" %>
<asp:Literal ID="LitScript" runat="server" />
<div class="map">
    <asp:Panel ID="PnlNavi" runat="server" Style="float: left; width: 200px;" Visible="false">
        <telerik:RadTreeView ID="Rtv" runat="server" CheckBoxes="True" TriStateCheckBoxes="true" CheckChildNodes="true" OnClientLoad="OnMapNavigationLoad" OnClientNodeClicked="OnMapNavigationClick" OnClientNodeChecked="OnMapNavigationChange" />
    </asp:Panel>
    <asp:Panel ID="PnlMap" runat="server" Style="float: left;">
        <asp:Literal ID="LitMap" runat="server" />
    </asp:Panel>
</div>
<div class="clearBoth">
</div>