<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StyleSettingsWidget.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.StyleSettingsWidget" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<%@ Register Src="~/UserControls/StyleSettings.ascx" TagName="StyleSettings" TagPrefix="csb" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script type="text/javascript">
    function OnSelectedTemplateChanged(sender, eventArgs) {
        if (eventArgs.get_item().get_text() != "") {
            __doPostBack("<%=RcbTemplates.UniqueID %>", '');
        }
    }

    function UpdateStyles(selector, style) {
        $find("<%=RcbTemplates.ClientID %>").findItemByValue("Custom").select();

        var styleSheet = document.styleSheets[document.styleSheets.length - 1];

        var removeList = new Array();
        var cssRules = null;
        if (styleSheet.cssRules != null)
            cssRules = styleSheet.cssRules;
        else
            cssRules = styleSheet.rules;

        for (var j = 0; j < cssRules.length; j++) {
            if (cssRules[j].selectorText == selector)
                removeList.push(j);
        }
        for (var i = 0; i < removeList.length; i++) {
            if (styleSheet.cssRules != null)
                styleSheet.deleteRule(removeList[i]);
            else
                styleSheet.removeRule(removeList[i]);
        }

        var styles = document.getElementById("<%=StylesId %>");
        var currentStyles = styles.value;
        var regEx = new RegExp(selector + ".*?{.*?}");
        currentStyles = currentStyles.replace(regEx, selector + " { " + style + " } ");
        styles.value = currentStyles;

        if (styleSheet.cssRules != null) {
            styleSheet.insertRule(selector + "{ " + style + " } ", styleSheet.cssRules.length - 1);
        }
        else {
            styleSheet.addRule(selector, style);
        }
    }
</script>
<asp:UpdatePanel ID="UpnlStyles" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Literal ID="LitStyles" runat="server" />
        <div style="float: left; width: 100%;">
            <div style="float: left; width: 58%;">
                <fieldset style="margin: 0px 0px 10px 0px;">
                    <legend>
                        <%= GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("TitleTemplates")%>
                    </legend>
                    <div class="inputBlock" style="margin-top: 7px;">
                        <div class="inputBlockLabel2">
                            <web:LabelControl ID="LblTemplateLoad" LanguageFile="UserControls.WebUI.Base" LabelKey="LabelLoadTemplate" TooltipKey="TooltipLoadTemplate" runat="server" />
                        </div>
                        <div class="inputBlockContent2">
                            <telerik:RadComboBox ID="RcbTemplates" OnSelectedIndexChanged="OnSelectedTemplateChange" OnClientSelectedIndexChanged="OnSelectedTemplateChanged" runat="server" Width="100" />
                        </div>
                    </div>
                    <div class="inputBlock" style="margin-bottom: 0px;">
                        <div class="inputBlockLabel2">
                            <web:LabelControl ID="LblTemplateSave" LanguageFile="UserControls.WebUI.Base" LabelKey="LabelSaveTemplate" TooltipKey="TooltipSaveTemplate" runat="server" />
                        </div>
                        <div class="inputBlockContent2">
                            <div style="float: left; margin-right: 5px;">
                                <telerik:RadComboBox ID="RcbTemplates2" AllowCustomText="true" runat="server" Width="100" />
                            </div>
                            <asp:LinkButton ID="LbtnSave" OnClick="OnTemplateSaveClick" runat="server" CssClass="inputButton"><%= GuiLanguage.GetGuiLanguage("Shared").GetString("CommandSave")%></asp:LinkButton>
                        </div>
                    </div>
                </fieldset>
                <fieldset>
                    <legend>
                        <%= GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("TitleSettings")%>
                    </legend>
                    <div style="height: 7px;">
                    </div>
                    <telerik:RadPanelBar runat="server" CollapseAnimation-Type="None" ExpandAnimation-Type="None" AllowCollapseAllItems="false" ExpandMode="SingleExpandedItem" ID="Rpb" Width="100%">
                        <Items>
                            <telerik:RadPanelItem Expanded="true">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Value="Header">
                                        <ItemTemplate>
                                            <csb:StyleSettings ID="SP" Type="Header" TargetClass=".widget .top" runat="server" />
                                        </ItemTemplate>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem>
                                <Items>
                                    <telerik:RadPanelItem runat="server" Value="Content">
                                        <ItemTemplate>
                                            <csb:StyleSettings ID="SP" Type="Content" TargetClass=".widget .cnt" LinkColorEnabled="true" runat="server" />
                                        </ItemTemplate>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem>
                                <Items>
                                    <telerik:RadPanelItem runat="server" Value="Footer">
                                        <ItemTemplate>
                                            <csb:StyleSettings ID="SP" Type="Footer" TargetClass=".widget .bottom" runat="server" />
                                        </ItemTemplate>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem>
                                <Items>
                                    <telerik:RadPanelItem runat="server" Value="CustomStyle">
                                        <ItemTemplate>
                                            <div style="margin-left: 5px;">
                                                <div style="margin-bottom: 5px;">
                                                    <web:LabelControl ID="LblEditStyle" LanguageFile="UserControls.WebUI.Base" LabelKey="LabelEditStyle" TooltipKey="TooltipEditStyle" runat="server" />
                                                </div>
                                                <div class="inputBlock">
                                                    <asp:TextBox ID="TxtStyle" runat="server" TextMode="MultiLine" Height="120" Width="97%" Style="font-family: Courier New; font-size: 11px;" />
                                                </div>
                                                <div class="inputBlockError">
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelBar>
                </fieldset>
            </div>
            <div style="float: right; width: 40%;">
                <fieldset>
                    <legend>
                        <%= GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("TitlePreview")%>
                    </legend>
                    <div>
                        <div class="widget">
                            <div class="top">
                                <%= GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("TextWidgetPreviewTitle")%>
                            </div>
                            <div class="cnt">
                                <%= GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("TextWidgetPreviewContent")%>
                            </div>
                            <div class="bottom">
                                <%= GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("TextWidgetPreviewFooter")%>
                            </div>
                        </div>
                        <div class="widget">
                            <div class="top">
                                <%= GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("TextWidgetPreviewTitle")%>
                            </div>
                            <div class="cnt">
                                <%= GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("TextWidgetPreviewContent")%>
                            </div>
                            <div class="bottom">
                                <%= GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("TextWidgetPreviewFooter")%>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>
        <div class="clearBoth">
        </div>
        <asp:Literal ID="LitStatus" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
