<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.ProfileNotification" CodeBehind="ProfileNotification.ascx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock" style="margin-bottom: 10px;">
    <div class="inputBlockLabel">
        <%=language.GetString("LableAlertsCommon")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipAlertsCommon")%>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:PlaceHolder ID="PhGlobal" runat="server" />
    </div>
</div>
<div class="inputBlock" style="margin-bottom: 10px;">
    <div class="inputBlockLabel">
        <%=language.GetString("LableAlertsMyObjects")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipAlertsMyObjects")%>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:PlaceHolder ID="PhMyObjects" runat="server" />
    </div>
</div>
<div class="inputBlock" style="margin-bottom: 10px;">
    <div class="inputBlockLabel">
        <%=language.GetString("LableAlertsForeignObjects")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipAlertsForeignObjects")%>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:PlaceHolder ID="PhOtherObjects" runat="server" />
    </div>
</div>
