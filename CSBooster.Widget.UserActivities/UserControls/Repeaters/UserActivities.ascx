<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.Widget.UserControls.Repeaters.UserActivities" Codebehind="UserActivities.ascx.cs" %>
<ul class="CTY_user-act">
    <asp:Repeater ID="YMRP" runat="server" EnableViewState="False" OnItemDataBound="YMRP_ItemDataBound">
     <ItemTemplate>
        <asp:PlaceHolder ID="Ph" runat="server"></asp:PlaceHolder> 
     </ItemTemplate>
    </asp:Repeater>
</ul>
