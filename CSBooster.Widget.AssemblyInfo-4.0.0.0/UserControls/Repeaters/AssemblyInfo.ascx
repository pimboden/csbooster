<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.Widget.UserControls.Repeaters.AssemblyInfo" CodeBehind="AssemblyInfo.ascx.cs" %>
<asp:Repeater ID="YMRP" runat="server" EnableViewState="False" OnItemDataBound="YMRP_ItemDataBound">
    <HeaderTemplate>
        <table cellspacing="5px">
            <tr>
                <th>
                    <web:labelcontrol id="FileName" languagefile="WidgetAssemblyInfo" labelkey="FileName" tooltipkey="TooltipFileName" runat="server" />
                </th>
                <th>
                    <web:labelcontrol id="FileVersion" languagefile="WidgetAssemblyInfo" labelkey="FileVersion" tooltipkey="TooltipFileVersion" runat="server" />
                </th>
                <th>
                    <web:labelcontrol id="ProductVersion" languagefile="WidgetAssemblyInfo" labelkey="ProductVersion" tooltipkey="TooltipProductVersion" runat="server" />
                </th>
                <th>
                    <web:labelcontrol id="Creation" languagefile="WidgetAssemblyInfo" labelkey="Creation" tooltipkey="TooltipCreation" runat="server" />
                </th>
                <th>
                    <web:labelcontrol id="Modified" languagefile="WidgetAssemblyInfo" labelkey="Modified" tooltipkey="TooltipModified" runat="server" />
                </th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <asp:PlaceHolder ID="Ph" runat="server"></asp:PlaceHolder>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
