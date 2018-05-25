<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Wizards.Slideshow" CodeBehind="Slideshow.ascx.cs" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<%@ Register Src="~/UserControls/ObjectsToObjectRelator.ascx" TagName="ObjectsToObjectRelator" TagPrefix="csb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelTitle") %>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipTitle") %>">&nbsp;&nbsp;&nbsp;</a>
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
        <%=language.GetString("LabelDescription") %>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipDescription") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtDesc" TextMode="MultiLine" MaxLength="20000" Width="99%" Height="70" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelEffect") %>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipEffect") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <telerik:RadComboBox ID="DDEffect" runat="server" Width="100%">
            <Items>
                <telerik:RadComboBoxItem Value="None" Text="Kein Übergangseffekt" />
                <telerik:RadComboBoxItem Value="FadeFast" Text="&#220;berblendung schnell" />
                <telerik:RadComboBoxItem Value="FadeMedium" Text="&#220;berblendung mittel" />
                <telerik:RadComboBoxItem Value="FadeSlow" Text="&#220;berblendung langsam" />
                <telerik:RadComboBoxItem Value="ScrollRight" Text="Verschieben nach rechts" />
                <telerik:RadComboBoxItem Value="ScrollLeft" Text="Verschieben nach links" />
                <telerik:RadComboBoxItem Value="ScrollTop" Text="Verschieben nach oben" />
                <telerik:RadComboBoxItem Value="ScrollDown" Text="Verschieben nach unten" />
            </Items>
        </telerik:RadComboBox>
    </div>
</div>
<csb:ObjectsToObjectRelator ID="OTOR" runat="server" />
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
