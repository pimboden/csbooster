<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Wizard.ascx.cs" Inherits="_4screen.CSB.Widget.Wizard" %>
<%@ Register Src="~/UserControls/Wizards/Wizard.ascx" TagName="Wizard" TagPrefix="csb" %>
<telerik:radformdecorator id="Rfd" runat="server" enableroundedcorners="false" decoratedcontrols="Fieldset,CheckBoxes,RadioButtons,Textarea" />
<csb:Wizard ID="Wiz" runat="server" />
<div class="clearBoth">
</div>
