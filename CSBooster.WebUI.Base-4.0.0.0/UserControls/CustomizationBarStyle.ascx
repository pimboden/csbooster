<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.CustomizationBarStyle" CodeBehind="CustomizationBarStyle.ascx.cs" %>
<%@ Register Src="~/UserControls/CustomizationBarTabStyles.ascx" TagName="CustomizationBarTabStyles" TagPrefix="csb" %>
<%@ Register Src="~/UserControls/CustomizationBarTabLayout.ascx" TagName="CustomizationBarTabLayout" TagPrefix="csb" %>
<%@ Register Src="~/UserControls/CustomizationBarTabTheme.ascx" TagName="CustomizationBarTabTheme" TagPrefix="csb" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<table class="cBar" cellpadding="0" cellspacing="5">
    <tr>
        <td>
            <asp:UpdatePanel ID="upnl" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="cBarTabs">
                        <asp:HyperLink ID="LnkWidgetStyle" CssClass="cBarTabInactive" runat="server"><%=language.GetString("CommandStyle")%></asp:HyperLink>
                        <asp:HyperLink ID="LnkLayout" CssClass="cBarTabInactive" runat="server"><%=language.GetString("CommandLayout")%></asp:HyperLink>
                        <asp:HyperLink ID="LnkTheme" CssClass="cBarTabInactive" runat="server"><%=language.GetString("CommandTheme")%></asp:HyperLink>
                    </div>
                    <table class="cBarContent" cellpadding="0" cellspacing="5">
                        <tr>
                            <td>
                                <asp:MultiView ID="MVCustBar" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="VWidgetStyle" runat="server">
                                        <csb:CustomizationBarTabStyles ID="CSH" runat="server" />
                                    </asp:View>
                                    <asp:View ID="VLayout" runat="server">
                                        <csb:CustomizationBarTabLayout ID="LH" runat="server" />
                                    </asp:View>
                                    <asp:View ID="VTheme" runat="server">
                                        <csb:CustomizationBarTabTheme ID="TH" runat="server" />
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
