<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.Widget.UserControls.Repeaters.CacheInfo" CodeBehind="CacheInfo.ascx.cs" %>
<asp:Repeater ID="YMRP" runat="server" EnableViewState="False" OnItemDataBound="YMRP_ItemDataBound">
    <HeaderTemplate>
        <table cellspacing="5px">
            <tr>
                <th>
                    <web:labelcontrol id="Key" languagefile="WidgetCacheInfo" labelkey="Key" tooltipkey="TooltipKey" runat="server" />
                </th>
                <th>
                    <web:labelcontrol id="Size" languagefile="WidgetCacheInfo" labelkey="Size" tooltipkey="TooltipSize" runat="server" />
                </th>
                <th>&nbsp;</th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <asp:PlaceHolder ID="Ph" runat="server"></asp:PlaceHolder>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
