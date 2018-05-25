<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WordFilter.ascx.cs" Inherits="_4screen.CSB.WebUI.Admin.UserControls.WordFilter" %>
<tr>
    <td><asp:TextBox ID="TxtWord" runat="server" /> </td>
    <td><asp:CheckBox ID="CBExact" runat="server" /> </td>
    <td>
        <asp:DropDownList ID="DDLAction" runat="server">
            <asp:ListItem Text="Email" Value="Inform" />
        </asp:DropDownList>
    </td>
    <td>
        <asp:LinkButton ID="LbtnSave" CssClass="CSB_admin_button" runat="server"><%=languageShared.GetString("CommandSave")%></asp:LinkButton>
    </td>
    <td>
        <asp:LinkButton ID="LbtnDelete" CssClass="CSB_admin_user_delete" runat="server" />
    </td>
</tr>
