<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Wizards.ForumTopic" CodeBehind="ForumTopic.ascx.cs" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
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
        <telerik:RadEditor ID="TxtDesc" runat="server" Width="99%" Height="200px" ToolsFile="~/Configurations/RadEditorToolsFile1.config" Language="de-CH" EditModes="Design" StripFormattingOptions="AllExceptNewLines" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelModerated")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipModerated")%>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:CheckBox ID="CbModerated" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelTopicItemCreation") %>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipTopicItemCreation")%>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <telerik:RadComboBox ID="RcbRights" ZIndex="90000" runat="server" Width="100%">
            <Items>
                <telerik:RadComboBoxItem Value="1" Text="Alle Mitglieder" />
                <telerik:RadComboBoxItem Value="2" Text="Alle angemeldeten Benutzer" Selected="true" />
            </Items>
        </telerik:RadComboBox>
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
