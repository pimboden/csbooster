<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.SmallOutputForumTopic" CodeBehind="SmallOutputForumTopic.ascx.cs" %>
<div class="forumTopicOutput">
    <div class="forumTopicOutputTitle">
        <asp:HyperLink ID="LnkTitle" runat="server" />
    </div>
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" CssClass="forumTopicOutputTitle2">
        Noch keine Beitr&auml;ge
    </asp:Panel>
    <asp:Panel ID="pnlInfo2" runat="server" Visible="false">
        <div class="forumTopicOutputDate">
            <asp:Literal ID="LitDesc" runat="server" />
        </div>
        <div class="forumTopicOutputDesc">
            <asp:Literal ID="LitDesc2" runat="server" />
        </div>
    </asp:Panel>
</div>
