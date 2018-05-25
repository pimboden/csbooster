<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserLists.ascx.cs" Inherits="_4screen.CSB.Widget.UserControls.Repeaters.UserLists" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Panel ID="PnlOverview" runat="server">
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
            <asp:Repeater ID="OBJOVW" runat="server" OnItemDataBound="OnOverviewItemDataBound">
                <ItemTemplate>
                    <asp:PlaceHolder ID="PhItem" runat="server" EnableViewState="false" />
                </ItemTemplate>
            </asp:Repeater>
            <div class="clearBoth">
            </div>
            <div>
                <asp:Panel ID="NOITEMPH" runat="server">
                    <%=NotItemFound%>
                </asp:Panel>
            </div>
            <asp:PlaceHolder ID='PhPagBot' runat="server" EnableViewState="false" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpProg" AssociatedUpdatePanelID="UpnlOverview" runat="server">
        <ProgressTemplate>
            <div class="updateProgress">
                <%=languageShared.GetString("LabelUpdateProgress")%>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Panel>
