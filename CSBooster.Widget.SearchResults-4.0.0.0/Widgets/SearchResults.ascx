<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchResults.ascx.cs" Inherits="_4screen.CSB.Widget.SearchResults" %>
<div class="searchResultsHeader">
    <asp:DataList ID="DlRes" runat="server" OnItemDataBound="OnResultItemDataBound" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" RepeatColumns="4" Width="100%">
        <ItemTemplate>
            <asp:Literal ID="LitGoTo" runat="server" />
        </ItemTemplate>
    </asp:DataList>
</div>
<div>
    <asp:PlaceHolder ID="Ph" runat="server" />
    <div class="clearBoth">
    </div>
</div>
