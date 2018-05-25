<%@ Page Language="C#" MasterPageFile="~/MasterPages/Empty.master" AutoEventWireup="True"
    CodeBehind="NavigationTranslate.aspx.cs" Inherits="_4screen.CSB.WebUI.Pages.Popups.NavigationTranslate" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Cnt1" runat="Server">
    <div style="width: 400px;">
        <table style="width: 400px;">
            <tr>
                <td style="width: 200px;">
                    <!--Current Language Data-->
                    <div class="CSB_wi_settings">
                        <div class="item_header">
                            Sprache
                        </div>
                        <div class="item">
                            <div>
                                <asp:Literal ID="litCurrLang" runat="server" />
                            </div>
                        </div>
                    </div>
                </td>
                <td style="width: 200px;">
                    <div class="CSB_wi_settings">
                        <div class="item_header">
                            Sprache
                        </div>
                        <div class="item">
                            <div>
                                <asp:DropDownList ID="ddlLangs" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLangs_SelectedIndexChanged" />
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="CSB_wi_settings">
                        <div class="item_header">
                            Link Text
                        </div>
                        <div class="item">
                            <div>
                                <asp:Literal ID="litText" runat="server" />
                            </div>
                        </div>
                    </div>
                </td>
                <td>
                    <div class="CSB_wi_settings">
                        <div class="item_header">
                            Link Text
                        </div>
                        <div class="item">
                            <div>
                                <asp:TextBox ID="txtText" runat="server" />
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="CSB_wi_settings">
                        <div class="item_header">
                            Tooltip
                        </div>
                        <div class="item">
                            <div>
                                <asp:Literal ID="litTooltip" runat="server" />
                            </div>
                        </div>
                    </div>
                </td>
                <td>
                    <div class="CSB_wi_settings">
                        <div class="item_header">
                            Tooltip
                        </div>
                        <div class="item">
                            <div>
                                <asp:TextBox ID="txtTooltip" runat="server" />
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="CSB_wi_settings">
                        <div class="item_header">
                            Link
                        </div>
                        <div class="item">
                            <div>
                                <asp:Literal ID="litLink" runat="server" />
                            </div>
                        </div>
                    </div>
    </td><td>
        <div class="CSB_wi_settings">
            <div class="item_header">
                Link
            </div>
            <div class="item">
                <div>
                    <asp:TextBox ID="txtLink" runat="server" />
                </div>
            </div>
        </div>
    </td>
    </tr>
    <tr>
        <td colspan="2">
            <div style="width: 400px">
                <div style="float: left;">
                    <asp:LinkButton ID="lbtnS" runat="server" Text="Speichern" CssClass="CSB_button4"
                        OnClick="lbtnS_Click" /></div>
                <div style="float: left;">
                    <asp:LinkButton ID="lbtnC" runat="server" Text="Fenster schliessen" CssClass="inputButton"
                        OnClientClick="GetRadWindow().Close();" /></div>
            </div>
        </td>
    </tr>
    </table> </div>
    <div style="clear: both">
    </div>
</asp:Content>
