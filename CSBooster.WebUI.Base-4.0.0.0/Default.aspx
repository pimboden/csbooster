<%@ Page Language="C#" Trace="false" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_4screen.CSB.WebUI.Default" %>

<%@ Import Namespace="_4screen.Utils.Web" %>
<asp:Content ID="cBar" ContentPlaceHolderID="cBar" runat="server">
    <asp:PlaceHolder ID="PhCB" runat="server" />
</asp:Content>
<asp:Content ID="Cnt" ContentPlaceHolderID="Cnt" runat="Server">
    <asp:PlaceHolder ID="Ph" runat="server" />
    <div id="contentBody">
        <asp:PlaceHolder ID="PhStyle" runat="server" />
        <asp:UpdatePanel ID="WCUP" runat="server" UpdateMode="Conditional" EnableViewState="false">
            <ContentTemplate>
                <asp:PlaceHolder ID="Ph2" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="WCUPPROG" AssociatedUpdatePanelID="WCUP" runat="server">
            <ProgressTemplate>
                <div class="updateProgress">
                    <%=GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUpdateProgress")%>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
