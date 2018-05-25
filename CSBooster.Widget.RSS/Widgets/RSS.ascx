<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RSS.ascx.cs" Inherits="_4screen.CSB.Widget.RSS" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:UpdatePanel runat="server" ID="UpnlRss" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:MultiView ID="MV" runat="server" ActiveViewIndex="0">
            <asp:View runat="server" ID="V1">
                <asp:Image runat="server" ID="IMGPRGRS" ImageAlign="Middle" />
                <asp:Timer ID="TimLoad" runat="server" Interval="500" OnTick="OnTimerLoadTick" />
            </asp:View>
            <asp:View runat="server" ID="V2">
                <asp:Repeater ID="RepFeed" runat="server" OnItemDataBound="OnRepFeedItemBound">
                    <ItemTemplate>
                        <div class="CTY_feed_item">
                            <div class="date">
                                <asp:Literal ID="LitDate" runat="server" />
                            </div>
                            <div class="title">
                                <asp:HyperLink ID="FeedLink" runat="server" CssClass="link" />
                            </div>
                            <asp:Panel ID="PnlDesc" CssClass="desc" runat="server" Visible="false" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Literal ID="LitMsg" runat="server" />
                <asp:Timer ID="TimReload" runat="server" Interval="500" OnTick="OnTimerReloadTick" />
            </asp:View>
        </asp:MultiView>
    </ContentTemplate>
</asp:UpdatePanel>
