<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailsForum.ascx.cs" Inherits="_4screen.CSB.WebUI.M.UserControls.Templates.DetailsForum" %>
<%@ Register Src="/M/UserControls/Pager.ascx" TagName="Pager" TagPrefix="csb" %>
<li><a class="back" href="/mpdefault.aspx">
    <%=language.GetString("TitleHome")%></a> <span class="title"><%=language.GetString("TitleForum")%></span>
    <asp:HyperLink ID="lnkCreate" runat="server" CssClass="create"><%=language.GetString("CommandAdd")%></asp:HyperLink></li>
<li class="detail forum">
    <div class="forumDesc">
        <asp:Literal ID="LitDesc" runat="server" />
    </div>
</li>
<asp:Repeater ID="RepForum" runat="server" EnableViewState="False" OnItemDataBound="OnForumItemDataBound">
    <ItemTemplate>
        <li class="forum">
            <asp:HyperLink ID="LnkTopicLink" runat="server" CssClass="goto">
                <h2 class="forumTopicTitle">
                    <asp:Literal ID="LitTopicTitle" runat="server" />
                </h2>
                <div class="forumTopicDesc">
                    <asp:Literal ID="LitTopicDesc" runat="server" />
                </div>
                <div class="forumTopicDesc2">
                    <%=language.GetString("LabelEntries")%>&nbsp;<asp:Literal ID="LitNumberPosts" runat="server" />&nbsp;/&nbsp;<%=language.GetString("LabelVisits")%>&nbsp;<asp:Literal ID="LitNumberViews" runat="server" />
                </div>
            </asp:HyperLink>
        </li>
    </ItemTemplate>
</asp:Repeater>
<csb:Pager ID="pager" runat="server" />
