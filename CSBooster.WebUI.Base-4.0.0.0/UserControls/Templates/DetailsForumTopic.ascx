<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.DetailsForumTopic" CodeBehind="DetailsForumTopic.ascx.cs" %>
<%@ Register Src="~/UserControls/Templates/SmallOutputUser2.ascx" TagName="SmallUserOutput" TagPrefix="csb" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="csb" %>
<div class="forum">
    <div class="forumDesc">
        <asp:Literal ID="LitDesc" runat="server" />
    </div>
    <div class="forumDesc">
        <asp:HyperLink ID="LnkAdd" NavigateUrl="javascript:void(0)" CssClass="inputButton" runat="server"><%=language.GetString("CommandForumPostAdd")%></asp:HyperLink>
    </div>
    <asp:UpdatePanel ID="UpPnlForumTopic" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="PBPageNum" runat="server" />
            <csb:Pager ID="FTPAGTOP" runat="server" Visible="false" />
            <table cellpadding="4" cellspacing="0" border="0" width="100%" class="forumContent">
                <asp:Repeater ID="RepForumTopic" runat="server" EnableViewState="False" OnItemDataBound="OnForumTopicItemDataBound">
                    <ItemTemplate>
                        <tr id="Tr1" runat="server">
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
                        <tr id="Tr2" runat="server">
                            <td align="right" valign="bottom" class="forumFunctions">
                                <asp:PlaceHolder ID="PhFunc" runat="server" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <csb:Pager ID="FTPAGBOT" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UPPROG" AssociatedUpdatePanelID="UpPnlForumTopic" runat="server">
        <ProgressTemplate>
            <div class="updateProgress">
                <%=languageShared.GetString("LabelUpdateProgress")%>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
