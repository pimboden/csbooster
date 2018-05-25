<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SmallOutputComment.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.Templates.SmallOutputComment" %>
<tr>
    <td class="commentUser">
        <asp:PlaceHolder ID="phUserOutput" runat="server" />
    </td>
    <td class="commentContent">
        <asp:PlaceHolder ID="phCommentOuput" runat="server" />
    </td>
    <td>
        <asp:LinkButton ID="LbtnDel" OnClick="OnDeleteClick" CssClass="commentDeleteButton" runat="server" Visible="false" />
    </td>
</tr>
