<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Wizards.Video" CodeBehind="Video.ascx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="PnlImg" CssClass="inputBlock" Visible="false" runat="server">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LabelPicture" runat="server" LanguageFile="UserControls.Wizards.WebUI.Base" LabelKey="LabelPicture" TooltipKey="TooltipPicture" />
    </div>
    <div class="inputBlockContent">
        <div class="inputBlockImage">
            <img id="Img" src="" runat="server" />
        </div>
    </div>
</asp:Panel>
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
<asp:Panel ID="PnlPreview" CssClass="inputBlock" Visible="false" runat="server">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LabelVideoPreview" runat="server" LanguageFile="UserControls.Wizards.WebUI.Base" LabelKey="LabelVideoPreview" TooltipKey="TooltipVideoPreview" />
    </div>
    <div class="inputBlockContent">
        <telerik:RadNumericTextBox ID="RntbPreview" Width="121" runat="server" MinValue="1" MaxValue="3600" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" />
    </div>
</asp:Panel>
<asp:Panel ID="PnlTagwords" CssClass="hidden" runat="server">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LabelTagwords" runat="server" LanguageFile="UserControls.Wizards.WebUI.Base" LabelKey="LabelTagwords" TooltipKey="TooltipTagwords" />
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtTagWords" Width="99%" runat="server" />
    </div>
</asp:Panel>
<asp:Panel ID="PnlGeo" CssClass="hidden" runat="server">
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
        <asp:HiddenField ID="HFZip" runat="server" />
        <asp:HiddenField ID="HFCity" runat="server" />
        <asp:HiddenField ID="HFStreet" runat="server" />
        <asp:HiddenField ID="HFCountry" runat="server" />
    </div>
</asp:Panel>
<asp:HiddenField ID="HFStatus" runat="server" />
<asp:HiddenField ID="HFShowState" runat="server" />
<asp:HiddenField ID="HFFriendType" runat="server" />
<asp:HiddenField ID="HFCopyright" runat="server" />
<div class="inputBlock">
    <div class="inputBlockContent errorText">
        <asp:Literal ID="LitMsg" runat="server" />
    </div>
</div>
