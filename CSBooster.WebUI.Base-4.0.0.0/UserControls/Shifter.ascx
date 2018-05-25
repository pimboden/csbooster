<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Shifter" Codebehind="Shifter.ascx.cs" %>
<asp:Panel ID="PAGER" runat="server">
  <table border="0" width="100%" cellpadding="0" cellspacing="0">
    <tr>
      <td align="left">
        <asp:Label ID="PAGTEXT" runat="server" />
      </td>
      <td align="right" valign="middle">
        <asp:ImageButton ID="PAGGOFIRST" CssClass="CSB_msg_pag_btn" ImageUrl="~/Library/Images/Layout/cmd_first.png" OnClick="OnShiftClick" runat="server" />
        <asp:ImageButton ID="PAGGOPREV" CssClass="CSB_msg_pag_btn" ImageUrl="~/Library/Images/Layout/cmd_prev.png" OnClick="OnShiftClick" runat="server" />
        <asp:ImageButton ID="PAGGONEXT" CssClass="CSB_msg_pag_btn" ImageUrl="~/Library/Images/Layout/cmd_next.png" OnClick="OnShiftClick" runat="server" />
        <asp:ImageButton ID="PAGGOLAST" CssClass="CSB_msg_pag_btn" ImageUrl="~/Library/Images/Layout/cmd_last.png" OnClick="OnShiftClick" runat="server" />
      </td>
    </tr>
  </table>
</asp:Panel>
