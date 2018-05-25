<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormAdvFields.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.FormAdvFields" %>
<%@ Register Assembly="CSBooster.WidgetControls" Namespace="_4screen.CSB.WidgetControls" TagPrefix="csb" %>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label1" LabelFile="WidgetForm" LabelKey="LabelText" ToolTipFile="WidgetForm" ToolTipKey="TooltipText" runat="server"></csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <asp:TextBox ID="txtTit" runat="server" Rows="2" MaxLength="500" Width="99%" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label3" LabelFile="WidgetForm" LabelKey="LabelOption" ToolTipFile="WidgetForm" ToolTipKey="TooltipOption" runat="server"></csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <asp:CheckBox ID="cbx1type" runat="server" /><label for="<%=cbx1type.ClientID %>" ><%=language.GetString("LabelMultiOption")%></label><br />
        <asp:CheckBox ID="cbx1typeMust" runat="server" /><label for="<%=cbx1typeMust.ClientID %>" ><%=language.GetString("LabelMustOption")%></label>
        <br />
        <asp:TextBox ID="txt1t0" runat="server" Width="60%" MaxLength="50"></asp:TextBox><br />
        <asp:TextBox ID="txt1t1" runat="server" Width="60%" MaxLength="50"></asp:TextBox><br />
        <asp:TextBox ID="txt1t2" runat="server" Width="60%" MaxLength="50"></asp:TextBox><br />
        <asp:TextBox ID="txt1t3" runat="server" Width="60%" MaxLength="50"></asp:TextBox><br />
        <asp:TextBox ID="txt1t4" runat="server" Width="60%" MaxLength="50"></asp:TextBox><br />
        <asp:TextBox ID="txt1t5" runat="server" Width="60%" MaxLength="50"></asp:TextBox><br />
        <asp:TextBox ID="txt1t6" runat="server" Width="60%" MaxLength="50"></asp:TextBox><br />
        <asp:TextBox ID="txt1t7" runat="server" Width="60%" MaxLength="50"></asp:TextBox><br />
        <asp:TextBox ID="txt1t8" runat="server" Width="60%" MaxLength="50"></asp:TextBox><br />
        <asp:TextBox ID="txt1t9" runat="server" Width="60%" MaxLength="50"></asp:TextBox>
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label4" LabelFile="WidgetForm" LabelKey="LabelFormField" ToolTipFile="WidgetForm" ToolTipKey="TooltipFormField" runat="server"></csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <asp:TextBox ID="TxtForm0" runat="server" Width="60%" MaxLength="50"></asp:TextBox><asp:CheckBox ID="CbxForm0Must" runat="server" Text="Pflichtfeld" /><asp:CheckBox ID="CbxForm0Multi" runat="server" Text="Multiline" /><br />
        <asp:TextBox ID="TxtForm1" runat="server" Width="60%" MaxLength="50"></asp:TextBox><asp:CheckBox ID="CbxForm1Must" runat="server" Text="Pflichtfeld" /><asp:CheckBox ID="CbxForm1Multi" runat="server" Text="Multiline" /><br />
        <asp:TextBox ID="TxtForm2" runat="server" Width="60%" MaxLength="50"></asp:TextBox><asp:CheckBox ID="CbxForm2Must" runat="server" Text="Pflichtfeld" /><asp:CheckBox ID="CbxForm2Multi" runat="server" Text="Multiline" /><br />
        <asp:TextBox ID="TxtForm3" runat="server" Width="60%" MaxLength="50"></asp:TextBox><asp:CheckBox ID="CbxForm3Must" runat="server" Text="Pflichtfeld" /><asp:CheckBox ID="CbxForm3Multi" runat="server" Text="Multiline" /><br />
        <asp:TextBox ID="TxtForm4" runat="server" Width="60%" MaxLength="50"></asp:TextBox><asp:CheckBox ID="CbxForm4Must" runat="server" Text="Pflichtfeld" /><asp:CheckBox ID="CbxForm4Multi" runat="server" Text="Multiline" /><br />
        <asp:TextBox ID="TxtForm5" runat="server" Width="60%" MaxLength="50"></asp:TextBox><asp:CheckBox ID="CbxForm5Must" runat="server" Text="Pflichtfeld" /><asp:CheckBox ID="CbxForm5Multi" runat="server" Text="Multiline" /><br />
        <asp:TextBox ID="TxtForm6" runat="server" Width="60%" MaxLength="50"></asp:TextBox><asp:CheckBox ID="CbxForm6Must" runat="server" Text="Pflichtfeld" /><asp:CheckBox ID="CbxForm6Multi" runat="server" Text="Multiline" /><br />
        <asp:TextBox ID="TxtForm7" runat="server" Width="60%" MaxLength="50"></asp:TextBox><asp:CheckBox ID="CbxForm7Must" runat="server" Text="Pflichtfeld" /><asp:CheckBox ID="CbxForm7Multi" runat="server" Text="Multiline" /><br />
        <asp:TextBox ID="TxtForm8" runat="server" Width="60%" MaxLength="50"></asp:TextBox><asp:CheckBox ID="CbxForm8Must" runat="server" Text="Pflichtfeld" /><asp:CheckBox ID="CbxForm8Multi" runat="server" Text="Multiline" /><br />
        <asp:TextBox ID="TxtForm9" runat="server" Width="60%" MaxLength="50"></asp:TextBox><asp:CheckBox ID="CbxForm9Must" runat="server" Text="Pflichtfeld" /><asp:CheckBox ID="CbxForm9Multi" runat="server" Text="Multiline" />
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
