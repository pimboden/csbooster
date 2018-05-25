<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.DataObj.UserControls.Repeaters.ProductOverview" CodeBehind="ProductOverview.ascx.cs" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Panel ID="PnlOverview" runat="server">
    <asp:UpdateProgress ID="UpProg" AssociatedUpdatePanelID="UpnlOverview" runat="server">
        <ProgressTemplate>
            <div class="updateProgress">
                Es wird geladen...
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpnlOverview" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="PBPageNum" runat="server" />
            <asp:HiddenField ID="PBSortAttr" runat="server" />
            <asp:HiddenField ID="PBObjectType" runat="server" />
            <asp:HiddenField ID="PBAscCtyID" runat="server" />
            <asp:HiddenField ID="PBAscUserID" runat="server" />
            <asp:HiddenField ID="PBTagWord" runat="server" />
            <asp:HiddenField ID="PBSearchParam" runat="server" />
            <asp:HiddenField ID="PBUserSearchParam" runat="server" />
            <asp:HiddenField ID="PBGeoCoordsLat" runat="server" />
            <asp:HiddenField ID="PBGeoCoordsLong" runat="server" />
            <asp:HiddenField ID="PBGeoRadius" runat="server" />
            <asp:PlaceHolder ID="PhPagTop" runat="server" EnableViewState="false" />
            <asp:PlaceHolder ID="PhNavi" runat="server" />
            <asp:DataList ID="OBJOVW" runat="server" OnItemDataBound="OnOverviewItemDataBound" RepeatDirection="Horizontal" RepeatLayout="Flow" CellPadding="0" Width="100%" EnableViewState="False" ShowFooter="False" ShowHeader="False">
                <ItemTemplate>
                    <asp:PlaceHolder ID='PhItem' runat="server" EnableViewState="false" />
                </ItemTemplate>
            </asp:DataList>
            <div class="clearBoth">
            </div>
            <div>
                <asp:Panel ID="NOITEMPH" Style="padding-top: 20px; padding-bottom: 20px;" runat="server">
                    Keine
                    <%=Title %>
                    gefunden
                </asp:Panel>
            </div>
            <asp:PlaceHolder ID='PhPagBot' runat="server" EnableViewState="false" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>
