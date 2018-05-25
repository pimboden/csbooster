<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SurveyStep02.ascx.cs" Inherits="_4screen.CSB.DataObj.UserControls.Wizards.SurveyStep02" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("LabelSurveyRedPoint")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetStringForTooltip("TooltipSurveyRedPoint") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="txtR" runat="server" Width="50px" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("LabelSurveyYellowPoint")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetStringForTooltip("TooltipSurveyYellowPoint") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="txtY" runat="server" Width="50px" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("LabelSurveyGreenPoint")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetStringForTooltip("TooltipSurveyGreenPoint") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="txtG" runat="server" Width="50px" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("LabelSurveyTestResults")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetStringForTooltip("TooltipSurveyTestResults") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:PlaceHolder ID="phTextResults" runat="server" />
        <asp:LinkButton ID="lbtnNT" runat="server" OnClick="lbtnNT_OnClick"><%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("LabelSurveyAddTestResults")%></asp:LinkButton>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockContent errorText">
        <asp:Literal ID="litMsg" runat="server" />
    </div>
</div>
<asp:HiddenField ID="HFTagWords" runat="server" />
<asp:HiddenField ID="HFGeoLong" runat="server" />
<asp:HiddenField ID="HFGeoLat" runat="server" />
<asp:HiddenField ID="HFZip" runat="server" />
<asp:HiddenField ID="HFCity" runat="server" />
<asp:HiddenField ID="HFRegion" runat="server" />
<asp:HiddenField ID="HFCountry" runat="server" />
<asp:HiddenField ID="HFStatus" runat="server" />
<asp:HiddenField ID="HFShowState" runat="server" />
<asp:HiddenField ID="HFCopyright" runat="server" />
