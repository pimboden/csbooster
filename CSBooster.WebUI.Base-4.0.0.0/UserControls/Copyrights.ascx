<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Copyrights" CodeBehind="Copyrights.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<fieldset id="Fs" runat="server" visible="false">
    <legend>
        <web:TextControl ID="TextTitleCopyright" runat="server" LanguageFile="UserControls.WebUI.Base" TextKey="TitleCopyright" />
    </legend>
    <div class="inputBlock">
        <div class="inputBlockLabel">
            <web:LabelControl ID="LabelCopyright" runat="server" LanguageFile="UserControls.WebUI.Base" LabelKey="LabelCopyright" TooltipKey="TooltipCopyright" />
        </div>
        <div class="inputBlockContent">
            <telerik:RadComboBox ID="DDCopyrights" DataTextField="Name" DataValueField="Value" runat="server" Width="100%" />
        </div>
    </div>
</fieldset>
