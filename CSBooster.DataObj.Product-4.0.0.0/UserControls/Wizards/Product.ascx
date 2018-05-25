<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.DataObj.UserControls.Wizards.Products" CodeBehind="Product.ascx.cs" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectProduct").GetString("LabelTitle")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectProduct").GetStringForTooltip("TooltipTitle") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtTitle" MaxLength="100" Width="99%" runat="server" />
        <div>
            <asp:RequiredFieldValidator ID="RFVTitle" CssClass="errorText" runat="server" ControlToValidate="TxtTitle" Display="Dynamic"><%=GuiLanguage.GetGuiLanguage("DataObjectProduct").GetString("MessageTitle") %></asp:RequiredFieldValidator>
        </div>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectProduct").GetString("LabelRef")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectProduct").GetStringForTooltip("TooltipRef") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="txtRef" MaxLength="100" Width="99%" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectProduct").GetString("LabelPrice1")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectProduct").GetStringForTooltip("TooltipPrice1") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="txtPrice1" MaxLength="100" Width="99%" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectProduct").GetString("LabelPrice2")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectProduct").GetStringForTooltip("TooltipPrice2") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="txtPrice2" MaxLength="100" Width="99%" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectProduct").GetString("LabelPorto")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectProduct").GetStringForTooltip("TooltipPorto") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtPorto" MaxLength="100" Width="99%" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectProduct").GetString("LabelStartDate")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectProduct").GetStringForTooltip("TooltipStartDate") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <telerik:RadDatePicker ID="RdpStart" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectProduct").GetString("LabelEndDate")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectProduct").GetStringForTooltip("TooltipEndDate") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <telerik:RadDatePicker ID="RdpEnd" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectProduct").GetString("LabelAbstract")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectProduct").GetStringForTooltip("TooltipAbstract") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtAbstract" TextMode="MultiLine" MaxLength="20000" Width="99%" Height="50" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectProduct").GetString("LabelDesc") %>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectProduct").GetStringForTooltip("TooltipDesc") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <telerik:RadEditor ID="TxtDesc" runat="server" Style="z-index: 10000;" Width="99%" Height="190px" ToolsFile="~/Configurations/RadEditorToolsFile1.config" Language="de-DE" EditModes="Design,Html" StripFormattingOptions="AllExceptNewLines" />
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
<div class="inputBlock">
    <div class="inputBlockContent errorText">
        <asp:Literal ID="LitMsg" runat="server" />
    </div>
</div>
