<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.DetailsForum" CodeBehind="DetailsForum.ascx.cs" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<%@ Register Src="~/UserControls/Templates/SmallOutputUser2.ascx" TagName="SmallUserOutput" TagPrefix="csb" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="csb" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="forum">
    <div class="forumDesc">
        <asp:Literal ID="LitDesc" runat="server" />
    </div>
    <div class="forumDesc">
        <asp:HyperLink ID="LnkAddTopic" NavigateUrl="javascript:void(0)" CssClass="inputButton" runat="server"><%=language.GetString("CommandForumTopicAdd")%></asp:HyperLink>
    </div>
    <asp:UpdatePanel ID="UpPnlForum" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="PBPageNum" runat="server" />
            <csb:Pager ID="FTPAGTOP" runat="server" Visible="false" />
            <table cellpadding="0" cellspacing="0" width="100%" class="forumContent">
                <tr id="TrH" runat="server">
                    <th width="45%" id="TdTopicH" align="left" runat="server">
                        <%=language.GetString("TitleForumTopic")%>
                    </th>
                    <th width="20%" id="TdStarterH" align="center" runat="server">
                        <%=language.GetString("TitleForumCreateBy")%>
                    </th>
                    <th width="15%" id="TdInfoH" align="center" runat="server">
                        <%=language.GetString("TitleForumEntry")%>
                    </th>
                    <th width="20%" id="TdLastPosterH" align="center" runat="server">
                        <%=language.GetString("TitleForumLastEntry")%>
                    </th>
                </tr>
                <asp:Repeater ID="RepForum" runat="server" EnableViewState="False" OnItemDataBound="OnForumItemDataBound">
                    <ItemTemplate>
                        <tr id="TrC" runat="server">
                            <td id="TdTopicC" runat="server" valign="top" style="overflow: hidden;">
                                <asp:HyperLink ID="LnkTopicLink" runat="server">
                                    <h2 class="forumTopicTitle">
                                        <asp:Literal ID="LitTopicTitle" runat="server" />
                                    </h2>
                                    <div class="forumTopicDesc">
                                        <asp:Literal ID="LitTopicDesc" runat="server" />
                                    </div>
                                    <asp:HyperLink ID="LnkOpenLatestPost" runat="server" CssClass="inputButton" Visible="false">
                                        <web:TextControl ID="CommandOpenLatestPost" runat="server" LanguageFile="Shared" TextKey="CommandOpenLatestPost" />
                                    </asp:HyperLink>
                                </asp:HyperLink>
                            </td>
                            <td id="TdStarterC" align="center" runat="server">
                                <div class="forumDate">
                                    <asp:Literal ID="LitStarterInfo" runat="server" />
                                </div>
                                <asp:PlaceHolder ID="PhStarter" runat="server" />
                            </td>
                            <td id="TdInfoC" align="center" runat="server">
                                <div style="margin-bottom: 2px;">
                                    <asp:Literal ID="LitNumberPosts" runat="server" />
                                </div>
                                <div style="margin-bottom: 2px;">
                                    <asp:Literal ID="LitNumberViews" runat="server" />
                                </div>
                                <div style="white-space: nowrap;">
                                    <asp:PlaceHolder ID="PhRating" runat="server" />
                                </div>
                            </td>
                            <td id="TdLastPosterC" align="center" runat="server">
                                <div class="forumDate">
                                    <asp:Literal ID="ListLastPosterInfo" runat="server" />
                                </div>
                                <asp:PlaceHolder ID="PhLastPoster" runat="server" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <csb:Pager ID="FTPAGBOT" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UPPROG" AssociatedUpdatePanelID="UpPnlForum" runat="server">
        <ProgressTemplate>
            <div class="updateProgress">
                <%=languageShared.GetString("LabelUpdateProgress")%>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
