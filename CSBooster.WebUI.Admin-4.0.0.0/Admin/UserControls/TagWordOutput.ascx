<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TagWordOutput.ascx.cs" Inherits="_4screen.CSB.WebUI.Admin_UserControls_TagWordOutput" %>
<tr>
    <td>
        <asp:Literal ID="LitTW" runat="server" />
    </td>
    <td>
    <asp:Literal ID="LitGR" runat="server" />
    </td>
    <td>
    <asp:Literal ID="LitSyn" runat="server" />
    </td>
    <td>
    <asp:Literal ID="LitSynF" runat="server" />
    </td>
    <td align="center">
        <asp:HyperLink ID="hlnkGR" runat="server" Text="" CssClass="CSB_admin_tag_group"/>
    </td>
    <td align="center">
        <asp:HyperLink ID="hlnkSyn" runat="server" Text="" CssClass="CSB_admin_tag_synonym"/>
    </td>
</tr>
<tr>
    <td colspan="5"><asp:Literal ID="LitMsg" runat="server" Visible="false" /></td>
</tr>
