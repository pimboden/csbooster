<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.Widget.UserControls.Repeaters.UserActivities" CodeBehind="UserActivities.ascx.cs" %>
<ul>
    <asp:Repeater ID="YMRP" runat="server" EnableViewState="False" OnItemDataBound="YMRP_ItemDataBound">
        <ItemTemplate>
            <asp:PlaceHolder ID="Ph" runat="server" />
        </ItemTemplate>
    </asp:Repeater>
</ul>
