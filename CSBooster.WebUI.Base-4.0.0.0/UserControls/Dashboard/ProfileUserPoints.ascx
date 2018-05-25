<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.ProfileUserPoints" CodeBehind="ProfileUserPoints.ascx.cs" %>
<fieldset>
    <legend>
        <%=language.GetString("TitlePoints")%>
    </legend>
    <div class="inputBlock">
        <div class="inputBlockLabel">
            <%=language.GetString("LablePoints")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipPoints")%>">&nbsp;&nbsp;&nbsp;</a>
        </div>
        <div class="inputBlockContent">
            <asp:Label ID="lblTotalPoints" runat="server" Text="0" />
        </div>
    </div>
    <div class="inputBlock">
        <div class="inputBlockLabel">
            <%=language.GetString("LablePointsGreen")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipPointsGreen")%>">&nbsp;&nbsp;&nbsp;</a>
        </div>
        <div class="inputBlockContent">
            <asp:Label ID="lblGreenPoints" runat="server" Text="0" />
        </div>
    </div>
    <div class="inputBlock">
        <div class="inputBlockLabel">
            <%=language.GetString("LablePointsRed")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipPointsRed")%>">&nbsp;&nbsp;&nbsp;</a>
        </div>
        <div class="inputBlockContent">
            <asp:Label ID="lblRedPoints" runat="server" Text="0" />
        </div>
    </div>
</fieldset>
