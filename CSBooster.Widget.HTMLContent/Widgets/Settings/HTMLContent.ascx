<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HTMLContent.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.HTMLContent" %>
<%@ Register Assembly="CSBooster.WidgetControls" Namespace="_4screen.CSB.WidgetControls" TagPrefix="csb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label1" LabelFile="WidgetHTMLContent" LabelKey="LabelText" ToolTipFile="WidgetHTMLContent" ToolTipKey="TooltipText" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <telerik:RadEditor ID="RadEditor1" AllowScripts="true" runat="server" Skin="Telerik" EnableEmbeddedBaseStylesheet="true" EnableEmbeddedSkins="true" Width="100%"
            Height="350px" ToolsFile="~/Configurations/RadEditorToolsFile1.config" Language="de-DE" EditModes="Design,Html" StripFormattingOptions="AllExceptNewLines">
            <Languages>
                <telerik:SpellCheckerLanguage Code="de-DE" Title="Deutsch" />
            </Languages>
        </telerik:RadEditor>
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
