<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserOutput.ascx.cs" Inherits="_4screen.CSB.WebUI.Admin_UserControls_UserOutput" %>
<tr>
    <td>
        <asp:HyperLink ID="LnkUser" runat="server" />
    </td>
    <td>
        <div style="float: left;">
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtEmail" ID="rfvMail" runat="server" ValidationGroup="email" Display="Dynamic"><%=language.GetString("MessagePasswordMust")%></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtEmail" ID="revNail" runat="server" ValidationGroup="email" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"><%=language.GetString("MessagePasswordWrong")%></asp:RegularExpressionValidator>
        </div>
        <div style="float: left;margin-left:5px;">
            <asp:LinkButton ID="emailOk" runat="server" CssClass="CSB_admin_button" OnClick="OnEmailSaveClick" CausesValidation="true" ValidationGroup="email"><%=languageShared.GetString("CommandOK")%></asp:LinkButton>
        </div>
    </td>
    <td>
        <asp:LinkButton ID="LbtnPassword" runat="server" CssClass="CSB_admin_button" Text="Neues Passwort senden" ToolTip='<%=language.GetString("TooltipNewPassword")%>' OnClick="OnPasswortResetClick" CausesValidation="False"><%=language.GetString("CommandNewPassword")%></asp:LinkButton>
    </td>
    <td align="center">
        <asp:LinkButton ID="LbtnLock" runat="server" Text="" OnClick="OnLockUserClick" CausesValidation="False" />
    </td>
    <td align="center">
        <asp:LinkButton ID="LbtnDelete" runat="server" Text="" CssClass="CSB_admin_user_delete" OnClick="OnDeleteUserClick" CausesValidation="False" />
    </td>
    <td>
        <asp:DropDownList ID="DDLRoles" runat="server" AutoPostBack="True" CausesValidation="false" OnSelectedIndexChanged="OnSelectedRoleChanged" />
    </td>
</tr>
<tr>
    <td colspan="5"><asp:Literal ID="LitMsg" runat="server" Visible="false" /></td>
</tr>
