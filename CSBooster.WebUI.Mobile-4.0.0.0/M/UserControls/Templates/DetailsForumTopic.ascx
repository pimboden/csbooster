<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailsForumTopic.ascx.cs" Inherits="_4screen.CSB.WebUI.M.UserControls.Templates.DetailsForumTopic" %>
<%@ Register Src="/M/UserControls/Pager.ascx" TagName="Pager" TagPrefix="csb" %>
<li>
    <asp:HyperLink ID="lnkBack" runat="server" CssClass="back" />
    <span class="title">
        <%=language.GetString("TitleForumTopic")%>
    </span>
    <asp:HyperLink ID="lnkCreate" runat="server" CssClass="create"><%=language.GetString("CommandAdd")%></asp:HyperLink>
</li>
<li class="detail forum">
    <div class="forumTitle">
        <asp:Literal ID="LitTitle" runat="server" />
    </div>
    <div class="forumDesc">
        <asp:Literal ID="LitDesc" runat="server" />
    </div>
    <table cellpadding="4" cellspacing="0" border="0" width="100%" class="forumContent">
        <asp:Repeater ID="RepForumTopic" runat="server" EnableViewState="False" OnItemDataBound="OnForumTopicItemDataBound">
            <ItemTemplate>
                <tr>
                    <td rowspan="2" align="center" valign="top" width="75">
                        <asp:PlaceHolder ID="PhPoster" runat="server" />
                    </td>
                    <td valign="top" class="forumPost">
                        <div class="forumInfo">
                            <asp:Literal ID="LitPosterInfo" runat="server" />
                        </div>
                        <div>
                            <asp:PlaceHolder ID="PhContent" runat="server" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="bottom" class="forumFunctions">
                        <asp:PlaceHolder ID="PhFunc" runat="server" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</li>
<csb:Pager ID="pager" runat="server" />
