<%@ Page Language="C#" MasterPageFile="~/MasterPages/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="ResourcesCompile.aspx.cs" Inherits="_4screen.CSB.WebUI.Admin.ResourcesCompile" %>

<asp:Content ID="Cnt" ContentPlaceHolderID="Cnt1" runat="server">
    <div class="CSB_admin_title">
        Resourcen
    </div>
    <div>
        <asp:PlaceHolder ID="PhResxFiles" runat="server" />
    </div>
    <div class="CSB_admin_sep2">
    </div>
    <div>
        <asp:LinkButton ID="LbtnCompile" CssClass="CSB_admin_button" runat="server" Text="Kompilieren" OnClick="OnCompileClick" />
    </div>
    <div style="margin-top: 5px; margin-bottom: 5px;">
        <asp:TextBox ID="TxtOutput" CssClass="CSB_admin_monotype" TextMode="MultiLine" Width="100%" Rows="10" runat="server" />
    </div>
    <asp:Panel ID="PnlMsg" runat="server">
        <asp:Label ID="LitMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="PnlService" runat="server" Visible="false">
        <div class="CSB_admin_sep">
        </div>
        <div>
            <asp:Literal ID="LitServiceStatus" runat="server" />
        </div>
        <div style="margin-top: 5px; margin-bottom: 5px;">
            <asp:LinkButton ID="LbtnServiceStop" Style="float: left;" CssClass="CSB_admin_button" runat="server" Text="Service stoppen und aktualisieren" OnClick="OnStopServiceClick" />
            <asp:LinkButton ID="LbtnServiceStart" Style="float: left; margin-left: 10px;" CssClass="CSB_admin_button" runat="server" Text="Service starten" OnClick="OnStartServiceClick" />
            <div class="clearBoth">
            </div>
        </div>
        <asp:Panel ID="PnlMsg2" runat="server">
            <asp:Label ID="LitMsg2" runat="server" />
        </asp:Panel>
    </asp:Panel>
</asp:Content>
