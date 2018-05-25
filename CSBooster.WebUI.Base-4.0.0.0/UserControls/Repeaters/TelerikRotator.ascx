<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TelerikRotator.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.Repeaters.TelerikRotator" %>
<telerik:RadRotator ID="Rr" runat="server" CssClass="rotator" RotatorType="AutomaticAdvance" ScrollDirection="Up" ScrollDuration="1000" Height="185" FrameDuration="5000" InitialItemIndex="1" OnItemDataBound="OnRotatorItemDataBound">
    <ItemTemplate>
        <asp:PlaceHolder ID="PhItem" runat="server" />
    </ItemTemplate>
</telerik:RadRotator>
