<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SurveyPageInfo.ascx.cs" Inherits="_4screen.CSB.DataObj.UserControls.Wizards.SurveyPageInfo" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadTabStrip ID="radTab" runat="server" SelectedIndex="0"  ScrollButtonsPosition="Middle" OnTabClick="radTab_TabClick" 
ScrollChildren="true" PerTabScrolling="true" MultiPageID="radMP">
    <Tabs>
    <telerik:RadTab runat="server" Text="Info" Value="Info" />
    <telerik:RadTab runat="server" Text="Fragen" Value="Info" />
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="radMP" runat="server" SelectedIndex="0">
<telerik:RadPageView ID="pvInfo" runat="server">
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("LabelSurveyPageInfoTitle")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetStringForTooltip("TooltipSurveyPageInfoTitle") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="txtTitle" MaxLength="100" Width="99%" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("LabelSurveyPageInfoDescription")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetStringForTooltip("TooltipSurveyPageInfoDescription")%>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <telerik:RadEditor ID="txtDesc" runat="server" Style="z-index: 10000;" Width="95%" Height="100px" ToolsFile="~/Configurations/RadEditorToolsFile1.config" Language="de-DE" EditModes="Design,Html" StripFormattingOptions="AllExceptNewLines" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
       &nbsp;
    </div>
    <div class="inputBlockContent"><div style='float:left'>
<asp:LinkButton ID="lbtnS" OnClick="lbtnS_Click" runat="server" CssClass = "inputButton" style="float:left"><%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("ButtonSave")%></asp:LinkButton>
<asp:LinkButton ID="lbtnD" OnClick="lbtnD_Click" runat="server" CssClass = "inputButton" style="float:left"><%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("DeletePage")%></asp:LinkButton>
    </div>
    <div class="clearBoth"> </div>
    </div>
    </div>
</telerik:RadPageView>
<telerik:RadPageView ID="pvQuestions" runat="server">
</telerik:RadPageView>
</telerik:RadMultiPage> 
<asp:HiddenField ID="hfCT" runat="server"  Value="0"/>