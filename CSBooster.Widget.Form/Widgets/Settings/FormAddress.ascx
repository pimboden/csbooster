<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormAddress.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.FormAddress" %>
<%@ Register Assembly="CSBooster.WidgetControls" Namespace="_4screen.CSB.WidgetControls" TagPrefix="csb" %>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label1" LabelFile="WidgetForm" LabelKey="LabelAddressData" ToolTipFile="WidgetForm" ToolTipKey="TooltipAddressData" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <div>
            <asp:CheckBox ID="CbxAdressShow" runat="server" />
        </div>
        <div>
            <asp:CheckBox ID="CbxAdressCommentShow" runat="server" /><label for="<%=CbxAdressCommentShow.ClientID %>" ><%=language.GetString("LabelAddressComment")%></label>
        </div>
        <div style="padding-top: 10px;">
            <asp:DropDownList runat="server" ID="DdlAdrSave" Width="300"></asp:DropDownList>
        </div>
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label2" LabelFile="WidgetForm" LabelKey="LabelMustAuth" ToolTipFile="WidgetForm" ToolTipKey="TooltipMustAuth" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <asp:CheckBox ID="CbxMustAuth" runat="server" />
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
