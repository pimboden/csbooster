<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WidgetSettings.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.WidgetSettings" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LblHeadingTag" LanguageFile="UserControls.WebUI.Base" LabelKey="LabelWidgetHeadingTag" TooltipKey="TooltipWidgetHeadingTag" runat="server" />
    </div>
    <div class="inputBlockContent">
        <asp:RadioButtonList ID="RblHeadingTag" runat="server">
            <asp:ListItem Text="" Value="" Selected="True" />
            <asp:ListItem Text="H1" Value="1" />
            <asp:ListItem Text="H2" Value="2" />
            <asp:ListItem Text="H3" Value="3" />
        </asp:RadioButtonList>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LblVisRoles" LanguageFile="UserControls.WebUI.Base" LabelKey="LabelWidgetVisibilityRoles" TooltipKey="TooltipWidgetVisibilityRoles" runat="server" />
    </div>
    <div class="inputBlockContent">
        <telerik:RadComboBox ID="RcbVisRoles" runat="server" Width="99%" 
            CausesValidation="False" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LblFixed" LanguageFile="UserControls.WebUI.Base" LabelKey="LabelWidgetFixed" TooltipKey="TooltipWidgetFixed" runat="server" />
    </div>
    <div class="inputBlockContent">
        <asp:CheckBox ID="CbFixed" runat="server" />
    </div>
</div>
<div class="clearBoth">
</div>
