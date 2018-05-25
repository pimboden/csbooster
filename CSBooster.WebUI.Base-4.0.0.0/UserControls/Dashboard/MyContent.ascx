<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.MyContent" CodeBehind="MyContent.ascx.cs" %>
<%@ Register Src="~/Pages/Popups/UserControls/ObjectLinking.ascx" TagName="ObjectLinking" TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/ObjectDetailsSmall.ascx" TagName="ObjectDetailsSmall" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="csb" %>
<script type="text/javascript">
    function RefreshMyContent() {
        __doPostBack('<%=upnl.UniqueID %>', '');
    }
</script>
<asp:UpdatePanel ID="upnl" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="PnlTemplate" runat="server">
            <asp:LinkButton ID="LbtnStyle1" runat="server" OnClick="OnSelectStyleClick" CommandArgument="Table"><%=language.GetString("CommandListView")%></asp:LinkButton>
            &nbsp;|&nbsp;
            <asp:LinkButton ID="LbtnStyle2" runat="server" OnClick="OnSelectStyleClick" CommandArgument="Alternative"><%=language.GetString("CommandAlternateView")%></asp:LinkButton>
        </asp:Panel>
        <csb:Pager ID="pager1" PageSize="30" runat="server" />
        <div class="myContent">
            <asp:Literal ID="LitListStart" runat="server" />
            <asp:Repeater ID="contentRepeater" runat="server" OnItemDataBound="OnContentItemDataBound">
                <ItemTemplate>
                    <asp:PlaceHolder ID="PhItem" runat="server" />
                </ItemTemplate>
            </asp:Repeater>
            <asp:Literal ID="LitListEnd" runat="server" />
        </div>
        <asp:HiddenField ID="htObjType" runat="server" />
        <asp:HiddenField ID="htMyCntOnly" runat="server" />
        <asp:HiddenField ID="htCommunityId" runat="server" />
        <asp:HiddenField ID="htTagId" runat="server" />
        <asp:HiddenField ID="htDataRange" runat="server" />
        <asp:HiddenField ID="htSort" runat="server" />
        <asp:HiddenField ID="htSortDirection" runat="server" />
        <asp:HiddenField ID="htAccesType" runat="server" />
        <asp:HiddenField ID="htShowState" runat="server" />
        <asp:HiddenField ID="htDateFrom" runat="server" />
        <asp:HiddenField ID="htDateTo" runat="server" />
        <asp:HiddenField ID="htSearchWord" runat="server" />
        <asp:HiddenField ID="htCurrentPage" runat="server" />
        <asp:HiddenField ID="htNumberItems" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="up" AssociatedUpdatePanelID="upnl" runat="server">
    <ProgressTemplate>
        <div class="updateProgress">
            <%=languageShared.GetString("LabelUpdateProgress")%>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
