<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.EmbedCode" CodeBehind="EmbedCode.ascx.cs" %>

<web:LabelControl ID="LabelControl1" LanguageFile="Shared" LabelKey="LabelEmbed" TooltipKey="TooltipEmbed" runat="server" />
<input class="CSB_textbox" style="width: 97%" type="textbox" onclick="this.select()" value="<%= EmbedingCode %>">