<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleObjectSelection.ascx.cs" Inherits="_4screen.CSB.WebUI.Admin.UserControls.SingleObjectSelection" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="csb" %>
<%@ Register Assembly="obout_ImageZoom_NET" Namespace="OboutInc.ImageZoom" TagPrefix="obout" %>
<asp:Panel ID="pnlSelection" runat="server" Style="display: block;">
    <div style="border-bottom: solid 1px #CCCCCC; margin-bottom: 5px; padding-bottom: 5px;">
    </div>
    <div class="SucheD1">
        <%=language.GetString("TitleSearch")%>:
    </div>
    <div class="SucheD2">
        <asp:TextBox ID="txtSR" Text="" runat="server" /></div>
    <div class="SucheD3">
        <asp:LinkButton ID="lbtnSR" CssClass="CSB_button4" runat="server" Text="Suchen" OnClick="lbtnSR_Click"><%=languageShared.GetString("CommandSearch")%></asp:LinkButton>
    </div>
    <div class="clearBoth">
    </div>
    <div style="height: 10px">
    </div>
    <csb:Pager ID="PAGTOP" ItemNamePlural="Inhalte" ItemNameSingular="Inhalt" PageSize="30" runat="server" />
    <div class="clearBoth">
    </div>
    <asp:Repeater ID="RepObj" runat="server" OnItemDataBound="OnRepObjItemDataBound">
        <ItemTemplate>
            <asp:Panel ID="pnlItem" runat="server" class="CSB_admin_cnt_item CSB_admin_cnt_item_row">
                <div class="col0">
                    <asp:Literal ID="litItemPicker" runat="server" />
                </div>
                <div class="col1">
                    <div class="img">
                        <obout:ImageZoom ID="IZ1" runat="server" CssClass="imgExtrasmall" />
                    </div>
                </div>
                <div class="col2">
                    <div class="title">
                        <asp:HyperLink ID="LnkDet2" runat="server" />
                    </div>
                </div>
                <div class="col3">
                    <div class="author" title="Ersteller">
                        <asp:HyperLink ID="LnkAuthor" runat="server" />
                    </div>
                </div>
                <div class="col4">
                    <asp:Panel ID="PnlLoc" runat="server" ToolTip="Speicherort">
                        <asp:HyperLink ID="LnkCty" runat="server" />
                    </asp:Panel>
                </div>
                <div class="col5">
                    <div class="linkcnt">
                        <asp:HyperLink ID="lnkMyCnt" runat="server" />
                    </div>
                </div>
                <div class="clearBoth">
                </div>
            </asp:Panel>
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>
<asp:Label ID="lblNoData" runat="server" Visible="false" />
<asp:HiddenField ID="htObjType" runat="server" />
<asp:HiddenField ID="htUserId" runat="server" />
<asp:HiddenField ID="htSort" runat="server" />
<asp:HiddenField ID="htSortDirection" runat="server" />
<asp:HiddenField ID="htAccesType" runat="server" />
<asp:HiddenField ID="htSearchWord" runat="server" />
<asp:HiddenField ID="htCurrentPage" runat="server" />
<asp:HiddenField ID="htNumberItems" runat="server" />
