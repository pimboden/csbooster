<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SurveyStep01.ascx.cs" Inherits="_4screen.CSB.DataObj.UserControls.Wizards.SurveyStep01" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("LabelSurveyInfoTitle")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetStringForTooltip("TooltipSurveyInfoTitle") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="txtTitle" MaxLength="100" Width="99%" runat="server" />
    </div>
    <div>
        <asp:RequiredFieldValidator ID="rfvTitle" CssClass="errorText" runat="server" ControlToValidate="txtTitle" Display="Dynamic"><%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("RFVTitleErrorMessage")%></asp:RequiredFieldValidator>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("LabelSurveyHeaderText")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetStringForTooltip("TooltipSurveyHeaderText") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <telerik:RadEditor ID="txtHeader" runat="server" Style="z-index: 10000;" Width="99%" Height="180px" ToolsFile="~/Configurations/RadEditorToolsFile1.config" Language="de-DE" EditModes="Design,Html" StripFormattingOptions="AllExceptNewLines" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("LabelSurveyFooterText")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetStringForTooltip("TooltipSurveyFooterText") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <telerik:RadEditor ID="txtFooter" runat="server" Style="z-index: 10000;" Width="99%" Height="180px" ToolsFile="~/Configurations/RadEditorToolsFile1.config" Language="de-DE" EditModes="Design,Html" StripFormattingOptions="AllExceptNewLines" />
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
