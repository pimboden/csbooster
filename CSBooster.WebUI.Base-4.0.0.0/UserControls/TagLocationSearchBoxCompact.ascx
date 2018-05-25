<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.TagLocationSearchBoxCompact" CodeBehind="TagLocationSearchBoxCompact.ascx.cs" %>
<asp:Panel ID="PnlSimple" runat="server">
    <div style="float: left; width: 75%">
        <asp:TextBox ID="TxtSearch2" runat="server" Width="99%" />
    </div>
    <div style="float: left; margin-left: 5px; width: 24%">
        <asp:HyperLink ID="LnkFind2" CssClass="CSB_button4" runat="server" NavigateUrl="javascript:void(0);"><%=languageShared.GetString("CommandSearch")%></asp:HyperLink>
        <asp:LinkButton ID="LbtnFind2" runat="server" Text="" />
    </div>
    <div class="clearBoth">
    </div>
</asp:Panel>
<asp:Panel ID="PnlGeo" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td width="31%" height="20"><%=language.GetString("LableSearchSearch")%>:</td>
            <td width="31%"><%=language.GetString("LableSearchCity")%>:</td>
            <td width="16%"><%=language.GetString("LableSearchRegion")%>:</td>
            <td width="22%"><%=language.GetString("LableSearchCountry")%>:</td>
        </tr>
        <tr>
            <td><asp:TextBox ID="TxtSearch" runat="server" Width="95%" /></td>
            <td><asp:TextBox ID="TxtLoc" Width="95%" runat="server" /></td>
            <td>
                <asp:DropDownList ID="DDRadius" EnableViewState="true" Width="96%" runat="server">
                    <asp:ListItem Value="1" Text="<1 km" />
                    <asp:ListItem Value="5" Text="5 km" Selected="True" />
                    <asp:ListItem Value="10" Text="10 km" />
                    <asp:ListItem Value="25" Text="25 km" />
                    <asp:ListItem Value="50" Text="50 km" />
                    <asp:ListItem Value="100" Text="100 km" />
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="DDCountry" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td colspan="4" align="right" valign="bottom" height="26">
                <asp:HiddenField ID="HidCoords" runat="server" />
                <asp:LinkButton ID="LbtnFind" runat="server" Text="" />
                <div style="float: right;">
                    <asp:HyperLink ID="LnkReset" Style="margin-top: 5px;" CssClass="CSB_button4" runat="server"><%=languageShared.GetString("CommandReset")%></asp:HyperLink>
                </div>
                <div style="float: right; margin-right: 5px;">
                    <asp:HyperLink ID="LnkFind" Style="margin-top: 5px;" CssClass="CSB_button4" runat="server" NavigateUrl="javascript:void(0);"><%=languageShared.GetString("CommandSearch")%></asp:HyperLink>
                </div>
            </td>
        </tr>
    </table>
</asp:Panel>
