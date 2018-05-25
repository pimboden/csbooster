<%@ Page Language="C#" MasterPageFile="~/MasterPages/Empty.master" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.Pages.Popups.CommunityMemberOwner" CodeBehind="CommunityMemberOwner.aspx.cs" %>

<%@ Import Namespace="_4screen.Utils.Web" %>
<asp:Content ID="cph1" ContentPlaceHolderID="Cnt1" runat="Server">
    <div id="popup" style="width: 400px;">
        <div class="inputBlock" style="overflow: scroll; height: 300px; border: solid 1px #CCCCCC;">
            <table cellpadding="0" cellspacing="5" border="0" width="100%">
                <asp:Repeater ID="rep" runat="server" EnableViewState="False" OnItemDataBound="rep_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td width="35%">
                                <asp:Literal ID="liNam" runat="server" />
                            </td>
                            <td width="20%">
                                <asp:PlaceHolder ID="phOwn" runat="server" />
                            </td>
                            <td width="45%" align="right">
                                <asp:PlaceHolder ID="phMem" runat="server" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <asp:Literal ID="litMsg" runat="server" Visible="false"></asp:Literal>
        <div class="inputBlock">
            <asp:LinkButton ID="btnSave" CssClass="inputButton" OnClick="OnSaveMembersClick" runat="server"><%=GuiLanguage.GetGuiLanguage("Shared").GetString("CommandSave")%></asp:LinkButton>
            <asp:LinkButton ID="btnCan" CssClass="inputButtonSecondary" OnClientClick="GetRadWindow().Close();" runat="server"><%=GuiLanguage.GetGuiLanguage("Shared").GetString("CommandClose")%></asp:LinkButton>
        </div>
    </div>
</asp:Content>
