<%@ Page Language="C#" MasterPageFile="~/MasterPages/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="UserAdmin.aspx.cs" Inherits="_4screen.CSB.WebUI.Admin.UserAdmin" %>

<%@ Register Src="~/Admin/UserControls/UserOutput.ascx" TagName="UserOutput" TagPrefix="csb" %>
<asp:Content ID="Cnt" ContentPlaceHolderID="Cnt1" runat="server">
    <div class="CSB_admin_title">
        <%=language.GetString("TitleUser")%>
    </div>
    <div>
        <div style="float: left;">
            <%=language.GetString("LableUsername")%>
        </div>
        <div style="float: left; margin-left: 10px;">
            <asp:TextBox ID="txtSrchGnr" runat="server" />
        </div>
        <div style="float: left; margin-left: 10px;">
            <asp:CheckBox ID="CBLocked" runat="server" /><label for="<%=CBLocked.ClientID %>" ><%=language.GetString("LableUserLocked")%></label>
        </div>
        <div style="float: left; margin-left: 10px;">
            <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="CSB_admin_button" Text="Suchen" OnClick="lbtnSearch_Click"><%=languageShared.GetString("CommandSearch")%></asp:LinkButton>
        </div>
        <div class="clearBoth">
        </div>
    </div>
    <div style="height: 20px;">
    </div>
    <asp:Repeater ID="rptUser" runat="server" OnItemDataBound="rptUser_ItemDataBound">
        <HeaderTemplate>
            <table cellspacing="0" cellpadding="0" border="0" width="100%" class="CSB_admin_user_table">
                <tr>
                    <td class="CSB_admin_title2"><%=language.GetString("LableUsername")%></td>
                    <td class="CSB_admin_title2"><%=language.GetString("LableUserChangeEmail")%></td>
                    <td class="CSB_admin_title2"><%=language.GetString("LableUserSendPassword")%></td>
                    <td class="CSB_admin_title2" align="center"><%=language.GetString("LableUserLock")%></td>
                    <td class="CSB_admin_title2" align="center"><%=language.GetString("LableUserDel")%></td>
                    <td class="CSB_admin_title2"><%=language.GetString("LableUserSetRole")%></td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <asp:UpdatePanel ID="upPnl" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <csb:UserOutput ID="UOut" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
