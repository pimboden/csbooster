<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Wizards.Location" CodeBehind="Location.ascx.cs" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
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
        <asp:TextBox ID="TxtDesc" TextMode="MultiLine" MaxLength="20000" Width="99%" Height="80" runat="server" />
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
        <web:LabelControl ID="LabelLocationType" runat="server" CssClass="inputMandatory" LanguageFile="UserControls.Wizards.WebUI.Base" LabelKey="LabelLocationType" TooltipKey="TooltipLocationType" />
    </div>
    <div class="inputBlockContent">
        <asp:CheckBoxList ID="CblType" runat="server" Width="100%" CellPadding="0" CellSpacing="0" RepeatColumns="3" />
    </div>
    <div class="inputBlockError">
        <asp:CustomValidator ID="CvType" runat="server" ValidationGroup="Type" OnServerValidate="ValidateType" SetFocusOnError="true" CssClass="inputErrorTooltip" Display="Dynamic"><%=language.GetString("TooltipLocationTypeRequired")%></asp:CustomValidator>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LabelGeotagging" runat="server" LanguageFile="UserControls.Wizards.WebUI.Base" LabelKey="LabelGeotagging" TooltipKey="TooltipGeotagging" />
    </div>
    <div class="inputBlockContent">
        <asp:HyperLink ID="LnkOpenMap" NavigateUrl="javascript:void(0)" CssClass="inputButton" Style="float: left;" Text="Karte anzeigen" runat="server" />
        <div style="float: left; margin-left: 10px;">
            <web:TextControl ID="LabelLongitude" runat="server" LanguageFile="Shared" TextKey="LabelLongitude" />
            <asp:TextBox ID="TxtGeoLong" Width="140" runat="server" />
        </div>
        <div style="float: left; margin-left: 10px;">
            <web:TextControl ID="LabelLatitude" runat="server" LanguageFile="Shared" TextKey="LabelLatitude" />
            <asp:TextBox ID="TxtGeoLat" Width="140" runat="server" />
        </div>
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
