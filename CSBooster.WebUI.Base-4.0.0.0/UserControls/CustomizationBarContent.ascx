<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.CustomizationBarContent" CodeBehind="CustomizationBarContent.ascx.cs" %>
<%@ Register Src="~/UserControls/CustomizationBarTabWidgets.ascx" TagName="CustomizationBarTabWidgets" TagPrefix="csb" %>
<%@ Register Src="~/UserControls/CustomizationBarTabContent.ascx" TagName="CustomizationBarTabContent" TagPrefix="csb" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<table class="cBar" cellpadding="0" cellspacing="5">
    <tr>
        <td>
            <asp:UpdatePanel ID="upnl" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="cBarTabs">
                        <asp:HyperLink ID="LnkWidgetSelect" CssClass="cBarTabInactive" runat="server"><%=language.GetString("CommandWidgets")%></asp:HyperLink>
                        <asp:HyperLink ID="LnkContent" CssClass="cBarTabInactive" runat="server"><%=language.GetString("CommandContent")%></asp:HyperLink>
                    </div>
                    <table class="cBarContent" cellpadding="0" cellspacing="5">
                        <tr>
                            <td>
                                <asp:MultiView ID="MVCustBar" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="VWidgetSelect" runat="server">
                                        <csb:CustomizationBarTabWidgets ID="WT" runat="server" />
                                    </asp:View>
                                    <asp:View ID="VContent" runat="server">
                                        <csb:CustomizationBarTabContent ID="CH" runat="server" />
                                    </asp:View>
                                </asp:MultiView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="up" AssociatedUpdatePanelID="upnl" runat="server">
                <ProgressTemplate>
                    <div class="updateProgress">
                        <%=GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUpdateProgress")%>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </td>
    </tr>
</table>
