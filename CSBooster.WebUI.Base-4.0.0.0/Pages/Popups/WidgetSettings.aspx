<%@ Page Language="C#" Trace="false" MasterPageFile="~/MasterPages/Empty.master" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.Pages.Popups.WidgetSettings" CodeBehind="WidgetSettings.aspx.cs" %>

<%@ Import Namespace="_4screen.Utils.Web" %>
<%@ Register Src="~/UserControls/WidgetSettings.ascx" TagName="WidgetSettings" TagPrefix="csb" %>
<%@ Register Src="~/UserControls/StyleSettingsWidget.ascx" TagName="StyleSettingsWidget" TagPrefix="csb" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Cnt" ContentPlaceHolderID="Cnt1" runat="Server" EnableViewState="false">
    <telerik:RadFormDecorator ID="Rfd" runat="server" EnableRoundedCorners="false" DecoratedControls="all" />
    <div id="widgetSettings">
        <telerik:RadTabStrip ID="TabStrip" runat="server" MultiPageID="MultiPage" ShowBaseLine="true" ScrollChildren="true" EnableViewState="false" OnClientTabSelected="function () { GetRadWindow().autoSize(); }" />
        <div class="widgetSettingsContent">
            <telerik:RadMultiPage RenderSelectedPageOnly="false" ID="MultiPage" runat="server" EnableViewState="false" />
            <asp:Literal ID="LitResult" runat="server" />
            <div class="inputBlock2">
                <div class="inputBlockContent">
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="OnSaveClick" CssClass="inputButton"><%= GuiLanguage.GetGuiLanguage("Shared").GetString("CommandSave")%></asp:LinkButton>
                    <a class="inputButtonSecondary" href="javascript:CloseWindow();">
                        <%= GuiLanguage.GetGuiLanguage("Shared").GetString("CommandCancel")%>
                    </a>
                </div>
            </div>
            <div class="clearBoth">
            </div>
        </div>
    </div>
</asp:Content>
