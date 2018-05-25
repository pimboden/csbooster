<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.DetailsFolder" CodeBehind="DetailsFolder.ascx.cs" %>
<div>
    <div>
        <div>
            <div style="float: left; padding-top: 4px;">
                Sortieren:
            </div>
            <asp:LinkButton CssClass="inputButton" Style="float: left; margin-left: 5px;" ID="lbtnSortFolderSort" runat="server" OnClick="lbtnSortFolderSort_Click"><%=language.GetString("CommandFolderSort")%></asp:LinkButton>
            <asp:LinkButton CssClass="inputButton" Style="float: left; margin-left: 5px;" ID="lbtnSortAlphabetical" runat="server" OnClick="lbtnSortAlphabetical_Click"><%=language.GetString("CommandFolderAlphabetical")%></asp:LinkButton>
            <asp:LinkButton CssClass="inputButton" Style="float: left; margin-left: 5px;" ID="lbtnSortType" runat="server" OnClick="lbtnSortType_Click"><%=language.GetString("CommandFolderType")%></asp:LinkButton>
        </div>
        <div class="clearBoth" style="margin-bottom: 10px;">
        </div>
        <asp:Panel ID="pnlResults" runat="server">
        </asp:Panel>
        <asp:TextBox ID="txtSortType" runat="server" Style="visibility: hidden; display: none" Text="FolderSort" />
    </div>
    <asp:Literal ID="INFOLIT" runat="server" />
    <div>
        <asp:Literal ID="DETSUBTITLE" runat="server" />
    </div>
</div>
