<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.Widget.UserControls.Repeaters.Comments" CodeBehind="Comments.ascx.cs" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="csb" %>
<table width="100%" cellpadding="0" cellspacing="0">
    <asp:Repeater ID="repComments" runat="server" EnableViewState="False" OnItemDataBound="OnRepCommentsItemDataBound">
        <ItemTemplate>
            <asp:PlaceHolder ID="Ph" runat="server" />
        </ItemTemplate>
    </asp:Repeater>
</table>
<csb:Pager ID="pager" runat="server" />
