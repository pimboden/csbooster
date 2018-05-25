<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchResults.ascx.cs" Inherits="_4screen.CSB.Widget.UserControls.Repeaters.SearchResults" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="csb" %>
<asp:Panel ID="PnlResults" runat="server" CssClass="CSB_search">
    <asp:UpdateProgress ID="UpProg" AssociatedUpdatePanelID="UpnlOverview" runat="server">
        <ProgressTemplate>
            <div class="CSB_loading">
                <%=languageShared.GetString("LabelUpdateProgress")%>.
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
            <div class="title">
                <asp:Literal ID="LitTitle" runat="server" />
            </div>
            <div class="results">
                <ul>
                    <asp:Repeater ID="RepRes" runat="server" OnItemDataBound="OnSearchItemDataBound">
                        <ItemTemplate>
                            <li>
                                <asp:PlaceHolder ID="Ph" runat="server" />
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <div class="CSB_clear">
            </div>
            <csb:Pager ID="Pag" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>
