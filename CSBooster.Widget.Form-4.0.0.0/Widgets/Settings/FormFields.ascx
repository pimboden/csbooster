<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormFields.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.FormFields" %>

<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label1" LanguageFile="WidgetForm" LabelKey="LabelText" ToolTipKey="TooltipText" runat="server">
        </web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="txtTit" runat="server" Rows="4" MaxLength="500" Width="99%" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div class="inputBlockError">
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label2" LanguageFile="WidgetForm" LabelKey="LabelTextbox" ToolTipKey="TooltipTextbox" runat="server">
        </web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <asp:CheckBox ID="CBText" runat="server" /><label for="<%=CBText.ClientID %>" ><%=language.GetString("LabelShowTextbox")%></label> &nbsp;&nbsp;<asp:CheckBox ID="CBTextMust" runat="server" /><label for="<%=CBTextMust.ClientID %>" ><%=language.GetString("LabelMustTextbox")%></label>
    </div>
    <div class="inputBlockError">
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label3" LanguageFile="WidgetForm" LabelKey="LabelOption" ToolTipKey="LabelMultiOption" runat="server">
        </web:LabelControl>
    </div>
    <div class="inputBlockContent">
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
    <div class="inputBlockError">
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label4" LanguageFile="WidgetForm" LabelKey="LabelFormField" ToolTipKey="TooltipFormField" runat="server">
        </web:LabelControl>
    </div>
    <div class="inputBlockContent">
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
    <div class="inputBlockError">
    </div>
</div>
