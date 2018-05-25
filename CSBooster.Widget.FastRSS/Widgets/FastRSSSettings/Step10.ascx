<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Step10.ascx.cs" Inherits="_4screen.CSB.Widget.FastRSSSettings_Step10" %>
<asp:Panel ID="settingsPanel" runat="server" Visible="true">
	<div class="CSB_wi_settings">
		<div class="item_header">
			URL
		</div>
		<div class="item">
			<div>
				<asp:TextBox ID="txtURL" runat="server" />
			</div>
		</div>
	</div>
	<div class="CSB_wi_settings" style="clear: both">
		<div class="item_header">
			Anzahl Feeds sollen angezeigt werden?
		</div>
		<div class="item">
			<asp:DropDownList ID="ddlFC" runat="server">
				<asp:ListItem Text="1 Feed" Value="1" />
				<asp:ListItem Text="2 Feeds" Value="2" />
				<asp:ListItem Text="3 Feeds" Value="3" />
				<asp:ListItem Text="4 Feeds" Value="4" />
				<asp:ListItem Text="5 Feeds" Value="5" />
				<asp:ListItem Text="6 Feeds" Value="6" />
				<asp:ListItem Text="7 Feeds" Value="7" />
				<asp:ListItem Text="8 Feeds" Value="8" />
				<asp:ListItem Text="9 Feeds" Value="9" />
				<asp:ListItem Text="Alle Feeds" Value="1000" />
			</asp:DropDownList>
		</div>
	</div>
	<div class="CSB_wi_settings" style="clear: both">
		<div class="item_header">
			Welche Zusatzinfo soll gezeigt werden?
		</div>
		<div class="item">
			<asp:CheckBox ID="cbxDesc" runat="server" Text="Abstract anzeigen" />
		</div>
	</div>
    <asp:PlaceHolder ID="phRF" runat="server"></asp:PlaceHolder>
</asp:Panel>
