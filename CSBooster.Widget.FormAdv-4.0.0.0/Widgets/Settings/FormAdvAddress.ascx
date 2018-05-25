<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormAdvAddress.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.FormAdvAddress" %>

<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=languageProfile.GetString("LableEmail")%>
    </div>
    <div class="inputBlockContent">
        </asp:TextBox><asp:CheckBox ID="CbxForm0Must" runat="server" Text="Pflichtfeld" /><asp:CheckBox ID="CbxForm0Multi" runat="server" Text="Multiline" /><br />
    </div>
    <div class="inputBlockError">
    </div>

        <%=languageProfile.GetString("LableEmail")%><asp:CheckBox ID="CbxForm1Must" runat="server" Text="Pflichtfeld" /><asp:CheckBox ID="CbxForm1Multi" runat="server" Text="Multiline" /><br />
        <%=languageProfile.GetString("LableEmail")%><asp:CheckBox ID="CbxForm2Must" runat="server" Text="Pflichtfeld" /><asp:CheckBox ID="CbxForm2Multi" runat="server" Text="Multiline" /><br />
        <%=languageProfile.GetString("LableEmail")%><asp:CheckBox ID="CbxForm3Must" runat="server" Text="Pflichtfeld" /><asp:CheckBox ID="CbxForm3Multi" runat="server" Text="Multiline" /><br />
        <%=languageProfile.GetString("LableEmail")%><asp:CheckBox ID="CbxForm4Must" runat="server" Text="Pflichtfeld" /><asp:CheckBox ID="CbxForm4Multi" runat="server" Text="Multiline" /><br />
        <asp:TextBox ID="TxtForm5" runat="server" Width="60%" MaxLength="50"></asp:TextBox><asp:CheckBox ID="CbxForm5Must" runat="server" Text="Pflichtfeld" /><asp:CheckBox ID="CbxForm5Multi" runat="server" Text="Multiline" /><br />
        <asp:TextBox ID="TxtForm6" runat="server" Width="60%" MaxLength="50"></asp:TextBox><asp:CheckBox ID="CbxForm6Must" runat="server" Text="Pflichtfeld" /><asp:CheckBox ID="CbxForm6Multi" runat="server" Text="Multiline" /><br />
        <asp:TextBox ID="TxtForm7" runat="server" Width="60%" MaxLength="50"></asp:TextBox><asp:CheckBox ID="CbxForm7Must" runat="server" Text="Pflichtfeld" /><asp:CheckBox ID="CbxForm7Multi" runat="server" Text="Multiline" /><br />
        <asp:TextBox ID="TxtForm8" runat="server" Width="60%" MaxLength="50"></asp:TextBox><asp:CheckBox ID="CbxForm8Must" runat="server" Text="Pflichtfeld" /><asp:CheckBox ID="CbxForm8Multi" runat="server" Text="Multiline" /><br />
        <asp:TextBox ID="TxtForm9" runat="server" Width="60%" MaxLength="50"></asp:TextBox><asp:CheckBox ID="CbxForm9Must" runat="server" Text="Pflichtfeld" /><asp:CheckBox ID="CbxForm9Multi" runat="server" Text="Multiline" />
    </div>
    <div class="inputBlockError">
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label5" LanguageFile="WidgetForm" LabelKey="LabelText" ToolTipKey="TooltipText" runat="server"></web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <asp:CheckBox ID="CbxAdressShow" runat="server" Text="Adressfelder anzeigen" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="DdlAdressSave" runat="server">
            <asp:ListItem Text="Adressdaten immer speichern" Value="0" />
            <asp:ListItem Text="Fragen ob Adressdaten speichern" Value="1" />
            <asp:ListItem Text="Adressdaten nicht speichern" Value="2" />
        </asp:DropDownList>
        <br />
        <asp:CheckBox ID="CbxAdressCommentShow" runat="server" Text="Anzeigen Kommentarfeld zur Adresse" />
        <br />
        <asp:CheckBox ID="CbxMustAuth" runat="server" Text="muss der Benutzer angenmeldet sein" />
    </div>
    <div class="inputBlockError">
    </div>
</div>
