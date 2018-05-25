<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineUser.ascx.cs" Inherits="_4screen.CSB.Widget.OnlineUser" %>
            <asp:Repeater ID="RepUsersOnline" OnItemDataBound="OnRepUsersOnlineBound" runat="server">
                <ItemTemplate>
                    <div class="CSB_home_online2">
                        <asp:HyperLink ID="LnkUser" runat="server" /><br />
                        <%= languageWidget.GetString("LabelMemberSince")%>
                        <asp:Literal runat="server" ID="MemberDate"></asp:Literal>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
