<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.M.Admin.UserControls.Msgbox" CodeBehind="Msgbox.ascx.cs" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<%@ Register Src="~/M/Admin/UserControls/MessagePreview.ascx" TagName="MessagePreview" TagPrefix="uc1" %>
<asp:HiddenField ID="PBGrpID" runat="server" />
<asp:HiddenField ID="PBSearchOptions" runat="server" />
<table cellpadding="0" cellspacing="0" class="messages">
    <tr>
        <th>
            <%=GuiLanguage.GetGuiLanguage("WebUI.Mobile").GetString("LabelMessage") %>
        </th>
        <th>
            <%=GuiLanguage.GetGuiLanguage("WebUI.Mobile").GetString("LabelAction") %>
        </th>
    </tr>
    <asp:Repeater ID="msgbox" runat="server" OnItemDataBound="OnMsgboxItemDataBound">
        <ItemTemplate>
            <tr id="trMsg" runat="server">
                <td>
                    <asp:PlaceHolder ID="phMsg" runat="server" />
                </td>
                <td>
                    <asp:PlaceHolder ID="phAct" runat="server" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    <tr id="pnlNoItems" runat="server">
        <td colspan="2"><%=GuiLanguage.GetGuiLanguage("WebUI.Mobile").GetString("MessageNoMessages") %></td>
    </tr>
</table>
