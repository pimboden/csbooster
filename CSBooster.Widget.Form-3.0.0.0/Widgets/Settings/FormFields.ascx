﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormFields.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.FormFields" %>
<%@ Register Assembly="CSBooster.WidgetControls" Namespace="_4screen.CSB.WidgetControls" TagPrefix="csb" %>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label1" LabelFile="WidgetForm" LabelKey="LabelText" ToolTipFile="WidgetForm" ToolTipKey="TooltipText" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <asp:TextBox ID="txtTit" runat="server" Rows="4" MaxLength="500" Width="99%" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label2" LabelFile="WidgetForm" LabelKey="LabelTextbox" ToolTipFile="WidgetForm" ToolTipKey="TooltipTextbox" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <asp:CheckBox ID="CBText" runat="server" /><label for="<%=CBText.ClientID %>" ><%=language.GetString("LabelShowTextbox")%></label> &nbsp;&nbsp;<asp:CheckBox ID="CBTextMust" runat="server" /><label for="<%=CBTextMust.ClientID %>" ><%=language.GetString("LabelMustTextbox")%></label>
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label3" LabelFile="WidgetForm" LabelKey="LabelOption" ToolTipFile="WidgetForm" ToolTipKey="LabelMultiOption" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <asp:CheckBox ID="cbx1type" runat="server" /><label for="<%=cbx1type.ClientID %>" ><%=language.GetString("LabelMultiOption")%></label>&nbsp;&nbsp;<asp:CheckBox ID="cbx1typeMust" runat="server" /><label for="<%=cbx1typeMust.ClientID %>" ><%=language.GetString("LabelMustOption")%></label>
        <br />
        <asp:TextBox ID="txt1t0" runat="server" Width="80%" MaxLength="50"></asp:TextBox><br />
        <asp:TextBox ID="txt1t1" runat="server" Width="80%" MaxLength="50"></asp:TextBox><br />
        <asp:TextBox ID="txt1t2" runat="server" Width="80%" MaxLength="50"></asp:TextBox><br />
        <asp:TextBox ID="txt1t3" runat="server" Width="80%" MaxLength="50"></asp:TextBox><br />
        <asp:TextBox ID="txt1t4" runat="server" Width="80%" MaxLength="50"></asp:TextBox><br />
        <asp:TextBox ID="txt1t5" runat="server" Width="80%" MaxLength="50"></asp:TextBox><br />
        <asp:TextBox ID="txt1t6" runat="server" Width="80%" MaxLength="50"></asp:TextBox><br />
        <asp:TextBox ID="txt1t7" runat="server" Width="80%" MaxLength="50"></asp:TextBox><br />
        <asp:TextBox ID="txt1t8" runat="server" Width="80%" MaxLength="50"></asp:TextBox><br />
        <asp:TextBox ID="txt1t9" runat="server" Width="80%" MaxLength="50"></asp:TextBox>
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label4" LabelFile="WidgetForm" LabelKey="LabelFormField" ToolTipFile="WidgetForm" ToolTipKey="TooltipFormField" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <asp:TextBox ID="TxtForm0" runat="server" Width="80%" MaxLength="50"></asp:TextBox>&nbsp;&nbsp;<asp:CheckBox ID="CbxForm0Must" runat="server" /><label for="<%=CbxForm0Must.ClientID %>" ><%=language.GetString("LabelFormFieldMust")%></label><br />
        <asp:TextBox ID="TxtForm1" runat="server" Width="80%" MaxLength="50"></asp:TextBox>&nbsp;&nbsp;<asp:CheckBox ID="CbxForm1Must" runat="server" /><label for="<%=CbxForm1Must.ClientID %>" ><%=language.GetString("LabelFormFieldMust")%></label><br />
        <asp:TextBox ID="TxtForm2" runat="server" Width="80%" MaxLength="50"></asp:TextBox>&nbsp;&nbsp;<asp:CheckBox ID="CbxForm2Must" runat="server" /><label for="<%=CbxForm2Must.ClientID %>" ><%=language.GetString("LabelFormFieldMust")%></label><br />
        <asp:TextBox ID="TxtForm3" runat="server" Width="80%" MaxLength="50"></asp:TextBox>&nbsp;&nbsp;<asp:CheckBox ID="CbxForm3Must" runat="server" /><label for="<%=CbxForm3Must.ClientID %>" ><%=language.GetString("LabelFormFieldMust")%></label><br />
        <asp:TextBox ID="TxtForm4" runat="server" Width="80%" MaxLength="50"></asp:TextBox>&nbsp;&nbsp;<asp:CheckBox ID="CbxForm4Must" runat="server" /><label for="<%=CbxForm4Must.ClientID %>" ><%=language.GetString("LabelFormFieldMust")%></label><br />
        <asp:TextBox ID="TxtForm5" runat="server" Width="80%" MaxLength="50"></asp:TextBox>&nbsp;&nbsp;<asp:CheckBox ID="CbxForm5Must" runat="server" /><label for="<%=CbxForm5Must.ClientID %>" ><%=language.GetString("LabelFormFieldMust")%></label><br />
        <asp:TextBox ID="TxtForm6" runat="server" Width="80%" MaxLength="50"></asp:TextBox>&nbsp;&nbsp;<asp:CheckBox ID="CbxForm6Must" runat="server" /><label for="<%=CbxForm6Must.ClientID %>" ><%=language.GetString("LabelFormFieldMust")%></label><br />
        <asp:TextBox ID="TxtForm7" runat="server" Width="80%" MaxLength="50"></asp:TextBox>&nbsp;&nbsp;<asp:CheckBox ID="CbxForm7Must" runat="server" /><label for="<%=CbxForm7Must.ClientID %>" ><%=language.GetString("LabelFormFieldMust")%></label><br />
        <asp:TextBox ID="TxtForm8" runat="server" Width="80%" MaxLength="50"></asp:TextBox>&nbsp;&nbsp;<asp:CheckBox ID="CbxForm8Must" runat="server" /><label for="<%=CbxForm8Must.ClientID %>" ><%=language.GetString("LabelFormFieldMust")%></label><br />
        <asp:TextBox ID="TxtForm9" runat="server" Width="80%" MaxLength="50"></asp:TextBox>&nbsp;&nbsp;<asp:CheckBox ID="CbxForm9Must" runat="server" /><label for="<%=CbxForm9Must.ClientID %>" ><%=language.GetString("LabelFormFieldMust")%></label>
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
