<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.CustomizationBarTabStyle" CodeBehind="CustomizationBarTabStyle.ascx.cs" %>
<table class="cBarStyle" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td valign="top">
            <asp:RadioButton runat="server" ID="RbColor" Checked="true" /><asp:RadioButton runat="server" ID="RbImage" />
        </td>
    </tr>
    <tr>
        <td valign="top">
            <asp:Panel ID="PnlColor" runat="server">
                <asp:Panel ID="PnlColorBox" runat="server" />
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td><asp:TextBox ID="TxtColor" Columns="24" runat="server"></asp:TextBox></td>
                        <td>
                            <telerik:radcolorpicker id="rcp" palettemodes="WebPalette,HSV" runat="server" showicon="true" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="PnlImage" runat="server">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="40%">
                            <asp:Image ID="Image" runat="server" />
                            <asp:TextBox ID="TxtImage" runat="server" Style="display: none" /> </td>
                        <td width="60%">
                            <asp:HyperLink ID="btnAddImage" runat="server" CssClass="inputButton" Text="Bild auswählen" />
                            <div style="margin-top: 2px; margin-bottom: 2px;">
                                <%=language.GetString("LableStyleRepeat")%>:
                            </div>
                            <div style="line-height: 14px;">
                                <asp:CheckBox runat="server" ID="CbRepeatH" Text="" />
                                <asp:Image ID="cbHImg" ImageUrl="~/Library/Images/Layout/cmd_horizontal.png" runat="server" />
                                <span style="position: relative; top: -2px;">
                                    <%=language.GetString("LableStyleHori")%></span>
                            </div>
                            <div style="line-height: 14px;">
                                <asp:CheckBox runat="server" ID="CbRepeatV" Text="" />
                                <asp:Image ID="cbVImg" ImageUrl="~/Library/Images/Layout/cmd_vertical.png" runat="server" />
                                <span style="position: relative; top: -2px;">
                                    <%=language.GetString("LableStyleVerti")%></span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="PnlFooterHeight" runat="server" Visible="false">
                                <div>
                                    <%=language.GetString("LableStyleHeight")%>:&nbsp;<asp:TextBox ID="TxtFooterHeight" Columns="3" runat="server" /></div>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
