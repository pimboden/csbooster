<%@ Page Language="C#" MasterPageFile="~/MasterPages/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="TagWords.aspx.cs" Inherits="_4screen.CSB.WebUI.Admin.TagWords" %>
<%@ Register Src="~/Admin/UserControls/TagWordOutput.ascx" TagName="TagWordOutput" TagPrefix="csb" %>
<asp:Content ID="Cnt" ContentPlaceHolderID="Cnt1" runat="server">
    <div class="CSB_admin_title">
        <%=language.GetString("TitleTagword")%>
    </div>
    <div>
        <div style="float: left;">
            <asp:Literal ID="litSrchLabel" runat="server" Text="Tagwort" />
        </div>
        <div style="float: left; margin-left: 10px;">
            <asp:TextBox ID="txtSrchGnr" runat="server" />
        </div>
        <div style="float: left; margin-left: 10px;">
            <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="CSB_admin_button" OnClick="lbtnSearch_Click"><%=languageShared.GetString("CommandSearch")%></asp:LinkButton>
        </div>
        <div style="float: left; margin-left: 10px;">
            <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="CSB_admin_button" OnClick="lbtnAdd_Click"><%=languageShared.GetString("CommandAdd")%></asp:LinkButton>
        </div>
        <div class="clearBoth">
        </div>
    </div>
    <div style="height: 20px;">
    </div>
    <asp:Repeater ID="rptTag" runat="server" OnItemDataBound="rptTag_ItemDataBound">
        <HeaderTemplate>
            <table cellspacing="0" cellpadding="0" border="0" width="100%" class="CSB_admin_user_table">
                <tr>
                    <td class="CSB_admin_title2"><%=language.GetString("LableTagword")%></td>
                    <td class="CSB_admin_title2"><%=language.GetString("LableTagwordGroupTag")%></td>
                    <td class="CSB_admin_title2"><%=language.GetString("LableTagwordSynonym")%></td>
                    <td class="CSB_admin_title2"><%=language.GetString("LableTagwordSynonymOf")%></td>
                    <td class="CSB_admin_title2" align="center"><%=language.GetString("LableTagwordGroup")%></td>
                    <td class="CSB_admin_title2" align="center"><%=language.GetString("LableTagwordAdd")%></td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <asp:UpdatePanel ID="upPnl" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <csb:TagWordOutput ID="TOut" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
