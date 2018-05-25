<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.M.UserControls.Repeaters.OverviewEvent" CodeBehind="OverviewEvent.ascx.cs" %>
<%@ Register Src="/M/UserControls/Pager.ascx" TagName="Pager" TagPrefix="csb" %>
<%@ Register Src="/M/UserControls/Search.ascx" TagName="Search" TagPrefix="csb" %>

<li><a class="back" href="/mpdefault.aspx">
    <%=language.GetString("TitleHome")%></a><span class="title"><asp:Literal ID="LitTitle" runat="server" /></span>
    <asp:HyperLink ID="lnkCreate" runat="server" CssClass="create" Visible="false"><%=language.GetString("CommandAdd")%></asp:HyperLink></li>
<li><csb:Search ID="Search" runat="server" /></li>
<asp:Repeater ID="repObj" runat="server" OnItemDataBound="OnOverviewItemDataBound" EnableViewState="false">
    <ItemTemplate>
        <li>
            <asp:PlaceHolder ID="PhItem" runat="server" />
        </li>
    </ItemTemplate>
</asp:Repeater>
<csb:Pager ID="pager" runat="server" />
<li>
    <asp:HyperLink ID="lnkToday1" runat="server" CssClass="goto2" Visible="false"><%=language.GetString("TitleEventToday") %></asp:HyperLink>
    <asp:HyperLink ID="lnkToday2" runat="server" CssClass="goto2" Visible="false"><%=language.GetString("TitleEventTodayMultiDay") %></asp:HyperLink>
    <asp:HyperLink ID="lnkFromTomorrow" runat="server" CssClass="goto2" Visible="false"><%=language.GetString("TitleEventFromTomorrow") %></asp:HyperLink>
    <asp:HyperLink ID="lnkUntilYesterday" runat="server" CssClass="goto2" Visible="false"><%=language.GetString("TitleEventUntilYesterday") %></asp:HyperLink>
</li>
