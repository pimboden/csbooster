<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataObjectLists.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.Repeaters.DataObjectLists" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<asp:Panel ID="PnlOverview" runat="server">
    <asp:Literal ID="LitParams" runat="server" />
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
            <asp:PlaceHolder ID="PhPagTop" runat="server" />
            <asp:Repeater ID="RepObj" runat="server" OnItemDataBound="OnOverviewItemDataBound">
                <ItemTemplate>
                    <asp:PlaceHolder ID="PhItem" runat="server" />
                </ItemTemplate>
            </asp:Repeater>
            <asp:Panel ID="PnlNoItems" CssClass="clearBoth" runat="server">
                <%=NotItemFound%>
            </asp:Panel>
            <asp:PlaceHolder ID="PhPagBot" runat="server" />
            <div class="clearBoth">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpProg" AssociatedUpdatePanelID="UpnlOverview" runat="server">
        <ProgressTemplate>
            <div class="updateProgress">
                <%=GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUpdateProgress")%>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Panel>
