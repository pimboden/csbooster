<%@ Page Language="C#" MasterPageFile="~/MasterPages/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="WordFilterAdmin.aspx.cs" Inherits="_4screen.CSB.WebUI.Admin.WordFilterAdmin" %>
<%@ Register Src="~/Admin/UserControls/WordFilter.ascx" TagName="WordFilter" TagPrefix="csb" %>
<asp:Content ID="Cnt" ContentPlaceHolderID="Cnt1" runat="server">
    <div class="CSB_admin_title">
        <%=language.GetString("TitleFilter")%>
    </div>
    <table cellspacing="0" cellpadding="0" border="0" width="100%" class="CSB_admin_user_table">
        <tr>
            <td class="CSB_admin_title2"><%=language.GetString("LableFilterWord")%></td>
            <td class="CSB_admin_title2"><%=language.GetString("LableFilterFullWord")%></td>
            <td class="CSB_admin_title2"><%=language.GetString("LableFilterAction")%></td>
            <td class="CSB_admin_title2"></td>
            <td class="CSB_admin_title2"></td>
        </tr>
        <asp:Repeater ID="RepFilterWords" runat="server" OnItemDataBound="RepFilterWordsItemDataBound">
            <ItemTemplate>
                <asp:UpdatePanel ID="upPnl" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <csb:WordFilter ID="WF" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td class="CSB_admin_title2"><asp:TextBox ID="TxtWord" runat="server" /></td>
            <td class="CSB_admin_title2"><asp:CheckBox ID="CBExact" runat="server" /></td>
            <td class="CSB_admin_title2">
                <asp:DropDownList ID="DDLAction" runat="server">
                    <asp:ListItem Text="Email" Value="Inform" />
                </asp:DropDownList>
            </td>
            <td class="CSB_admin_title2" colspan="2">
                <asp:LinkButton ID="LbtnAdd" runat="server" CssClass="CSB_admin_button" OnClick="OnAddClick"><%=languageShared.GetString("CommandAdd")%></asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Content>
