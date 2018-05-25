<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Pager" CodeBehind="Pager.ascx.cs" %>
<asp:Panel ID="Pag" runat="server">
    <table border="0" width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left">
                <asp:Literal ID="LitText" runat="server" />
            </td>
            <td align="right" id="Ctrl" runat="server">
                <asp:HyperLink ID="LnkFirst" runat="server" Visible="true">
                    <asp:Image ID="ImgFirst" ImageUrl="/Library/Images/Layout/cmd_first.png" runat="server" />
                </asp:HyperLink>
                <asp:HyperLink ID="LnkPrev" runat="server" Visible="true">
                    <asp:Image ID="ImgPrev" ImageUrl="/Library/Images/Layout/cmd_first.png" runat="server" />
                </asp:HyperLink>
                <asp:PlaceHolder ID="LnkGoTo" runat="server" Visible="true" />
                <asp:HyperLink ID="LnkNext" runat="server" Visible="true">
                    <asp:Image ID="ImgNext" ImageUrl="/Library/Images/Layout/cmd_first.png" runat="server" />
                </asp:HyperLink>
                <asp:HyperLink ID="LnkLast" runat="server" Visible="true">
                    <asp:Image ID="ImgLast" ImageUrl="/Library/Images/Layout/cmd_first.png" runat="server" />
                </asp:HyperLink>
                <asp:LinkButton ID="GoToFirst" OnClick="OnChangePageClick" runat="server">
                    <asp:Image ID="First" ImageUrl="/Library/Images/Layout/cmd_first.png" runat="server" />
                </asp:LinkButton>
                <asp:LinkButton ID="GoToPrev" OnClick="OnChangePageClick" runat="server">
                    <asp:Image ID="Prev" ImageUrl="/Library/Images/Layout/cmd_prev.png" runat="server" />
                </asp:LinkButton>
                <asp:PlaceHolder ID="GoTo" runat="server" />
                <asp:LinkButton ID="GoToNext" OnClick="OnChangePageClick" runat="server">
                    <asp:Image ID="Next" ImageUrl="/Library/Images/Layout/cmd_next.png" runat="server" />
                </asp:LinkButton>
                <asp:LinkButton ID="GoToLast" OnClick="OnChangePageClick" runat="server">
                    <asp:Image ID="Last" ImageUrl="/Library/Images/Layout/cmd_last.png" runat="server" />
                </asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Panel>
