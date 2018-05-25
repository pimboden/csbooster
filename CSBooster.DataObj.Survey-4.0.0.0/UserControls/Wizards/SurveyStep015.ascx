<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SurveyStep015.ascx.cs" Inherits="_4screen.CSB.DataObj.UserControls.Wizards.SurveyStep015" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("LabelSurveyIsContest")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetStringForTooltip("TooltipSurveyIsContest") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:RadioButtonList ID="rblIsContest" runat="server"  RepeatDirection="Horizontal" RepeatColumns="2">
        <asp:ListItem Selected="True" Value="0" Text="Nein" />
        <asp:ListItem Value="1" Text="Ja" />
        </asp:RadioButtonList>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("LabelSurveyShowForm")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetStringForTooltip("TooltipSurveyShowForm") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:RadioButtonList ID="rblShowForm" runat="server"  RepeatDirection="Horizontal" RepeatColumns="2">
        <asp:ListItem Selected="True" Value="0" Text="Nein" />
        <asp:ListItem Value="1" Text="Ja" />
        </asp:RadioButtonList>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("LabelActiveFrom")%>:
    </div>
    <div class="inputBlockContent">
   <telerik:RadDateTimePicker runat="server" ID="rdtpActiveFrom" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("LabelActiveTo")%>:
    </div>
    <div class="inputBlockContent">
   <telerik:RadDateTimePicker runat="server" ID="rdtpActiveTo" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("LabelSendMailTo")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetStringForTooltip("TooltipSendMailTo") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
   <asp:TextBox ID="txtMT" runat="server" />
   <div>
   <asp:RegularExpressionValidator ID="revTxtMT" runat="server" 
           ControlToValidate="txtMT" CssClass="errorText" Display="Dynamic" 
           ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"> <%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("InvalidMailAddress")%></asp:RegularExpressionValidator>
   </div>
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
