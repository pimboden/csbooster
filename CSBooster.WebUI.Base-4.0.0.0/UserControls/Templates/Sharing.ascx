<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Sharing.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.Templates.Sharing" %>
<div class="socialSharing">
    <asp:Panel ID="PnlILike" runat="server" CssClass="socialSharingBlock" Visible="false">
        <asp:Literal ID="LitILike" runat="server" />
    </asp:Panel>
    <asp:Panel ID="PnlShareBtns" runat="server" CssClass="socialSharingBlock socialSharingButtons" Visible="false">
        <div style="margin-bottom: 5px;">
            <web:TextControl ID="TextSharingIntro" runat="server" LanguageFile="UserControls.Templates.WebUI.Base" TextKey="TextSharingIntro" />
        </div>
        <ul>
            <asp:Literal ID="LitSharing" runat="server" />
        </ul>
    </asp:Panel>
    <asp:Panel CssClass="socialSharingBlock socialSharingEmbed" ID="PnlEmbed" runat="server">
        <web:LabelControl ID="lBLUrl" LanguageFile="Shared" LabelKey="LabelPermaLink" TooltipKey="TooltipPermaLink" runat="server" />
        <input style="width: 97%;" type="textbox" onclick="this.select()" value="<%= permaLink %>">
        <asp:PlaceHolder ID="PhEmbed" runat="server" />
    </asp:Panel>
    <asp:Panel CssClass="socialSharingBlock" ID="PnlShareUrl" runat="server">
        <a href="javascript:radWinOpen('/Pages/Popups/MessageSend.aspx?MsgType=rec&URL=<%= sendUrl %>', '<%= popupTitle %>', 450, 450, true);" class="inputButton">
            <web:TextControl ID="CommandRecommend" runat="server" LanguageFile="Shared" TextKey="CommandRecommend" />
        </a>
    </asp:Panel>
</div>
<div class="clearBoth">
</div>
