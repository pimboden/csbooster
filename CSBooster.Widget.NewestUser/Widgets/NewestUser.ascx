<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewestUser.ascx.cs" Inherits="_4screen.CSB.Widget.NewestUser" %>
<div>
    <asp:Repeater ID="RepNewUsers" OnItemDataBound="OnRepNewUsersBound" runat="server">
        <ItemTemplate>
            <div class="CSB_home_newuser">
                <asp:PlaceHolder ID="phSUO" runat="server" />
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
<div class="CSB_clear">
</div>
<div class="NewestUsers_AllUsers">
    <asp:HyperLink ID="allUsers" runat="server"></asp:HyperLink>
</div>
