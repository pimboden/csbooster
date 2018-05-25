<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Wizards.Wizard" CodeBehind="Wizard.ascx.cs" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div id="wizard">
    <asp:Panel ID="PnlWiz" runat="server">
        <asp:HiddenField ID="HfLTab" runat="server" />
        <telerik:RadTabStrip AutoPostBack="true" ShowBaseLine="true" ID="TabStrip" runat="server" MultiPageID="MultiPage" SelectedIndex="0" ScrollChildren="true" EnableViewState="false" />
        <telerik:RadMultiPage RenderSelectedPageOnly="true" ID="MultiPage" runat="server" SelectedIndex="0" CssClass="wizardContent" EnableViewState="false" />
    </asp:Panel>
</div>
