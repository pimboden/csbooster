<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.M.Admin.UserControls.Friends" CodeBehind="Friends.ascx.cs" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<asp:HiddenField ID="PBPageNum" runat="server" />
<asp:HiddenField ID="PBSearchOptions" runat="server" />
<div class="friends">
    <asp:Repeater ID="repFriends" runat="server" OnItemDataBound="OnRepFriendsItemDataBound">
        <ItemTemplate>
            <div class="friend">
                <div class="user">
                    <asp:PlaceHolder ID="UD" runat="server" />
                </div>
                <div class="actions">
                    <asp:PlaceHolder ID="ACT" runat="server" />
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
<div class="clearBoth">
</div>
<asp:Panel ID="pnlNoItems" runat="server">
    <%=GuiLanguage.GetGuiLanguage("WebUI.Mobile").GetString("MessageNoFriends") %>
</asp:Panel>
