<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Wizards.Event" CodeBehind="Event.ascx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LabelPicture" runat="server" LanguageFile="UserControls.Wizards.WebUI.Base" LabelKey="LabelPicture" TooltipKey="TooltipPicture" />
    </div>
    <div class="inputBlockContent">
        <div class="inputBlockImage">
            <img id="Img" src="" runat="server" />
        </div>
        <div class="inputBlockImage">
            <asp:HyperLink ID="LnkImage" CssClass="inputButton" NavigateUrl="javascript:void(0)" runat="server"><%=languageShared.GetString("CommandUpload") %></asp:HyperLink>
        </div>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LabelTitle" runat="server" CssClass="inputMandatory" LanguageFile="UserControls.Wizards.WebUI.Base" LabelKey="LabelTitle" TooltipKey="TooltipTitle" />
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtTitle" MaxLength="100" Width="99%" runat="server" />
    </div>
    <div class="inputBlockError">
        <asp:RequiredFieldValidator ID="RFVTitle" CssClass="inputErrorTooltip" runat="server" ControlToValidate="TxtTitle" Display="Dynamic"><%=language.GetString("MessageTitle") %></asp:RequiredFieldValidator>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LabelDescription" runat="server" LanguageFile="UserControls.Wizards.WebUI.Base" LabelKey="LabelDescription" TooltipKey="TooltipDescription" />
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtDesc" TextMode="MultiLine" MaxLength="20000" Width="99%" Height="60" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LabelEvent" runat="server" LanguageFile="UserControls.Wizards.WebUI.Base" LabelKey="LabelEvent" TooltipKey="TooltipEvent" />
    </div>
    <div class="inputBlockContent">
        <telerik:RadEditor ID="TxtEvent" runat="server" Width="99%" Height="200px" ToolsFile="~/Configurations/RadEditorToolsFile1.config" Language="de-CH" EditModes="Design" StripFormattingOptions="AllExceptNewLines" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LabelWebsite" runat="server" LanguageFile="UserControls.Wizards.WebUI.Base" LabelKey="LabelWebsite" TooltipKey="TooltipWebsite" />
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtWebsite" Width="99%" runat="server" />
    </div>
    <div class="inputBlockError">
        <asp:RegularExpressionValidator ID="RevWebsite" runat="server" ControlToValidate="TxtWebsite" Display="Dynamic" EnableViewState="False" SetFocusOnError="true" CssClass="inputErrorTooltip"><%=language.GetString("MessageValidUrl")%></asp:RegularExpressionValidator>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LabelEventDateTime" CssClass="inputMandatory" runat="server" LanguageFile="UserControls.Wizards.WebUI.Base" LabelKey="LabelEventDateTime" TooltipKey="TooltipEventDateTime" />
    </div>
    <div class="inputBlockContent">
        <telerik:RadDatePicker ID="RDPFromDate" runat="server" />
        <telerik:RadDatePicker ID="RDPToDate" runat="server" />
        <asp:TextBox ID="TxtTime" runat="server" />
    </div>
    <div class="inputBlockError">
        <asp:RequiredFieldValidator ID="RfvFromDate" CssClass="inputErrorTooltip" runat="server" ControlToValidate="RDPFromDate" Display="Dynamic"><%=language.GetString("TooltipDateRequired") %></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RfvToDate" CssClass="inputErrorTooltip" runat="server" ControlToValidate="RDPToDate" Display="Dynamic"><%=language.GetString("TooltipDateRequired") %></asp:RequiredFieldValidator>
        <asp:CustomValidator ID="CvDate" runat="server" ValidationGroup="Date" OnServerValidate="ValidateDate" SetFocusOnError="true" CssClass="inputErrorTooltip" Display="Dynamic"><%=language.GetString("TooltipEndDateBeforeStartDate") %></asp:CustomValidator>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LabelEventAgePrice" runat="server" LanguageFile="UserControls.Wizards.WebUI.Base" LabelKey="LabelEventAgePrice" TooltipKey="TooltipEventAgePrice" />
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtAge" runat="server" /> <asp:TextBox ID="TxtPrice" runat="server" />
    </div>
</div>

<script type="text/javascript">
    function OnLocationAdded(sender, eventArgs) {
        __doPostBack('<%=UpnlLoc.UniqueID %>', '');
    }
</script>

<asp:UpdatePanel ID="UpnlLoc" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LabelEventSelect" runat="server" LanguageFile="UserControls.Wizards.WebUI.Base" LabelKey="LabelEventSelect" TooltipKey="TooltipEventSelect" />
            </div>
            <div class="inputBlockContent">
                <telerik:RadComboBox ID="DDLocations" runat="server" Width="100%" ExpandDirection="Up" MaxHeight="150" AllowCustomText="true" Filter="Contains" />
            </div>
        </div>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LabelEventAdd" runat="server" LanguageFile="UserControls.Wizards.WebUI.Base" LabelKey="LabelEventAdd" TooltipKey="TooltipEventAdd" />
            </div>
            <div class="inputBlockContent">
                <asp:LinkButton ID="LbtnAddLoc" runat="server" CssClass="inputButton"><%=language.GetString("CommandEventAdd") %></asp:LinkButton>
            </div>
        </div>
        <div class="inputBlock">
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LabelEventType" runat="server" CssClass="inputMandatory" LanguageFile="UserControls.Wizards.WebUI.Base" LabelKey="LabelEventType" TooltipKey="TooltipEventType" />
    </div>
    <div class="inputBlockContent">
        <asp:CheckBoxList ID="CblType" runat="server" Width="100%" CellPadding="0" CellSpacing="0" RepeatColumns="3" />
    </div>
    <div class="inputBlockError">
        <asp:CustomValidator ID="CvType" runat="server" ValidationGroup="Type" OnServerValidate="ValidateType" SetFocusOnError="true" CssClass="inputErrorTooltip" Display="Dynamic"><%=language.GetString("TooltipEventTypeRequired")%></asp:CustomValidator>
    </div>
</div>
<asp:HiddenField ID="HFTagWords" runat="server" />
<asp:HiddenField ID="HFGeoLong" runat="server" />
<asp:HiddenField ID="HFGeoLat" runat="server" />
<asp:HiddenField ID="HFZip" runat="server" />
<asp:HiddenField ID="HFCity" runat="server" />
<asp:HiddenField ID="HFStreet" runat="server" />
<asp:HiddenField ID="HFCountry" runat="server" />
<asp:HiddenField ID="HFStatus" runat="server" />
<asp:HiddenField ID="HFShowState" runat="server" />
<asp:HiddenField ID="HFCopyright" runat="server" />
<div class="inputBlock">
    <div class="inputBlockContent errorText">
        <asp:Literal ID="LitMsg" runat="server" />
    </div>
</div>
