<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="Step10.ascx.cs" Inherits="_4screen.CSB.Widget.VEMapViewerSettings_Step10" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="settingsPanel" runat="server" Visible="true">
	<div class="CSB_wi_settings">
		<div class="item_header">
			Layout Typ?
		</div>
		<div class="item">
			<telerik:radcombobox id="rcbLT" runat="server" Skin="Custom" EnableEmbeddedSkins="false" width="450px"><Items><telerik:RadComboBoxItem Text="Ohne Navigation"  Value="0" /><telerik:RadComboBoxItem Text="Navigation ohne Überlappung" Value="1" /><telerik:RadComboBoxItem Text="Navigation mit Überlappung" Value="2" /></Items></telerik:radcombobox>
		</div>
	</div>
	<div class="CSB_wi_settings">
		<div class="item_header">
			Navigation und Icon aus?
		</div>
		<div class="item">
			<telerik:radcombobox id="rcbST" runat="server" Skin="Custom" EnableEmbeddedSkins="false" width="450px"><Items><telerik:RadComboBoxItem Text="Objekte"  Value="0" Selected="true" /><telerik:RadComboBoxItem Text="Tagwörter" Value="1" /></Items><CollapseAnimation 
				Duration="200" Type="OutQuint" /></telerik:radcombobox>
		</div>
	</div>
	<div class="CSB_wi_settings">
		<div class="item_header">
			Welche Objektypen anzeigen?
		</div>
		<div class="item">
			<telerik:radcombobox id="rcbOT" runat="server" Skin="Custom" EnableEmbeddedSkins="false" width="450px"></telerik:radcombobox>
		</div>
	</div>
</asp:Panel>
<asp:PlaceHolder ID="phSTS" runat="server" />
<div class="CSB_wi_settings">
	<div class="item_header">
		Widget mit URL steuern<br />
	</div>
	<div class="item">
		<telerik:radcombobox id="rcbOverWriteByURL" runat="server" Skin="Custom" EnableEmbeddedSkins="false" width="450px"><Items><telerik:RadComboBoxItem Text="Ja"  Value="1" />
				<telerik:RadComboBoxItem Text="Nein" Value="0" Selected="true" />
				</Items></telerik:radcombobox>
	</div>
    <asp:PlaceHolder ID="phRF" runat="server"></asp:PlaceHolder>
</div>
