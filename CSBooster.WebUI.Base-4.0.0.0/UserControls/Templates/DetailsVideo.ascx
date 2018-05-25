<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.DetailsVideo" CodeBehind="DetailsVideo.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="videoDetail">
    <div id="<%=VideoContentId %>">
        <a href="http://www.macromedia.com/go/getflashplayer">Flash Player</a>
        <%= language.GetString("LabelVideoFlashPlayer") %>

        <script type="text/javascript" src="/Library/Scripts/FlashPlayer/swfobject.js"></script>

        <script type="text/javascript">
            var so = new SWFObject('/Library/Scripts/FlashPlayer/player.swf', '<%=VideoPlayerId %>', '<%= FlashWidth %>', '<%= FlashHeight %>', '9', '#');
            so.addParam('allowfullscreen', 'true');
            so.addParam('allowscriptaccess', 'always');
            so.addParam('wmode', 'opaque');
            so.addVariable('abouttext', 'Flash player');
            so.addVariable('image', '<%= ThumbImgURL %>');
            so.addVariable('file', '<%= VideoURL %>');
            so.addVariable('skin', '<%= VideoSkin %>');
            so.addVariable('config', '<%= FlashVarsXml %>');
            so.write('<%=VideoContentId %>');
        </script>

    </div>
    <div class="videoCopyright">
        <asp:Literal ID="LitCopyright" runat="server" />
    </div>
    <asp:Panel ID="PnlDesc" runat="server" Visible="false" CssClass="videoDescription">
        <asp:Literal ID="LitDesc" runat="server" />
    </asp:Panel>
</div>
<telerik:RadToolTipManager ID="RTTM" runat="server" Width="300" Height="200" OnAjaxUpdate="OnAjaxUpdate" ShowCallout="False" Sticky="True">
</telerik:RadToolTipManager>
