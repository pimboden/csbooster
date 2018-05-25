<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.M.Admin.UserControls.Requests" CodeBehind="Requests.ascx.cs" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<asp:HiddenField ID="PBPageNum" runat="server" />
<div class="requests">
    <asp:Repeater ID="repRequests" runat="server" OnItemDataBound="OnRepRequestsItemDataBound">
        <ItemTemplate>
            <div class="request">
                <div class="date">
                    <asp:PlaceHolder ID="FT" runat="server" />
                </div>
                <div class="user">
                    <asp:PlaceHolder ID="UD" runat="server" />
                </div>
                <div class="actions">
                    <asp:PlaceHolder ID="FPAN" runat="server" />
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
<div class="clearBoth">
</div>
<asp:Panel ID="pnlNoItems" runat="server">
    <%=GuiLanguage.GetGuiLanguage("WebUI.Mobile").GetString("MessageNoRequests") %>
</asp:Panel>
