<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.DetailsAudio" CodeBehind="DetailsAudio.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div>
    <div>
        <div id="audcnt" class="CTY_audio_player"><a href="http://www.macromedia.com/go/getflashplayer">Flash Player</a> <%= language.GetString("LabelVideoFlashPlayer") %>
            <script type="text/javascript" src="/Library/Scripts/FlashPlayer/swfobject.js"></script>        
            <script type="text/javascript">
                var so = new SWFObject('/Library/Scripts/FlashPlayer/player.swf', 'ply', '<%= FlashWidth %>', '<%= FlashHeight %>', '9', '#');
                so.addParam('allowfullscreen','true');
                so.addParam('allowscriptaccess','always');
                so.addParam('wmode','opaque');
                so.addVariable('abouttext', 'Flash player');
                //   so.addVariable('abouttext', 'sieme.net');
                //   so.addVariable('aboutlink', 'http://www.sieme.net');
                so.addVariable('file', '<%= AudioURL %>');
                so.addVariable('skin', '<%= VideoSkin %>');
                so.addVariable('config', '<%= FlashVarsXml %>');
                so.write('audcnt');
            </script>    
        </div>
    </div>
    <div class="desc">
        <img src="<%= ThumbImgURL%>" /> <%= DescLinked %>
    </div>
</div>
<telerik:RadToolTipManager ID="RTTM" runat="server" Width="300" Height="200" OnAjaxUpdate="OnAjaxUpdate" ShowCallout="False" Sticky="True">
</telerik:RadToolTipManager>
