<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Wizards.PinboardSearch" CodeBehind="PinboardSearch.ascx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelTitle") %>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipTitle") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtTitle" MaxLength="100" Width="99%" runat="server" />
        <div>
            <asp:RequiredFieldValidator ID="RFVTitle" CssClass="inputErrorTooltip" runat="server" ControlToValidate="TxtTitle" Display="Dynamic"><%=language.GetString("MessageTitle") %></asp:RequiredFieldValidator>
        </div>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelDescription") %>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipDescription") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtDesc" TextMode="MultiLine" MaxLength="20000" Width="99%" Height="80" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelPrice") %>:
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtPrice" Width="99%" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelExpirationDate") %>:
    </div>
    <div class="inputBlockContent">
        <telerik:RadDatePicker ID="RDPExpDate" runat="server">
            <DatePopupButton ImageUrl="~/Library/Skins/Custom/DatePicker/Custom.gif" HoverImageUrl="~/Library/Skins/Custom/DatePicker/Custom.gif" />
        </telerik:RadDatePicker>
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
