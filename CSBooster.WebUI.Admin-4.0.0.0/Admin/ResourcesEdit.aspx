<%@ Page Language="C#" MasterPageFile="~/MasterPages/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="ResourcesEdit.aspx.cs" Inherits="_4screen.CSB.WebUI.Admin.ResourcesEdit" %>

<asp:Content ID="Cnt" ContentPlaceHolderID="Cnt1" runat="server">
    <div>
        <asp:Repeater ID="RepResources" runat="server" OnItemDataBound="RepResourcesItemDataBound">
            <HeaderTemplate>
                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                    <tr>
                        <td class="CSB_admin_title2">Name
                        <td class="CSB_admin_title2">Wert
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td width="10%" valign="top">
                        <asp:Literal ID="LitName" runat="server" />
                    </td>
                    <td width="90%">
                        <asp:PlaceHolder ID="PhText" runat="server" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div>
        <asp:LinkButton ID="LbtnSave" CssClass="CSB_admin_button" runat="server" Text="Speichern" OnClick="OnSaveClick" />
    </div>
    <asp:Panel ID="PnlMsg" runat="server" CssClass="CSB_admin_error" Visible="false">
        <asp:Label ID="LitMsg" runat="server" />
    </asp:Panel>
</asp:Content>
