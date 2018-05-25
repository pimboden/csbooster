<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HTMLContent.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.HTMLContent" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label1" LanguageFile="WidgetHTMLContent" LabelKey="LabelText" ToolTipKey="TooltipText" runat="server">
        </web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <telerik:RadEditor ID="RadEditor1" AllowScripts="true" runat="server" Skin="Telerik" EnableEmbeddedBaseStylesheet="true" EnableEmbeddedSkins="true" Width="100%"
            Height="350px" ToolsFile="~/Configurations/RadEditorToolsFile1.config" Language="de-DE" EditModes="Design,Html" StripFormattingOptions="AllExceptNewLines">
            <Languages>
                <telerik:SpellCheckerLanguage Code="de-DE" Title="Deutsch" />
            </Languages>
        </telerik:RadEditor>
    </div>
    <div class="inputBlockError">
    </div>
</div>
