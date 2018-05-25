<%@ Page Language="C#" MasterPageFile="~/MasterPages/Static.master" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.Pages.Static.ObjectNotFound" CodeBehind="ObjectNotFound.aspx.cs" %>

<%@ Import Namespace="_4screen.Utils.Web" %>
<asp:Content ID="CWZ1" ContentPlaceHolderID="WCZ1" runat="Server">
    <div class="staticContent">
        <h1 class="staticContentTitleRow">
            <asp:Literal ID="MSG" runat="server" />
        </h1>
        <div class="staticContentRow">
            <a href="javascript:history.back()">
                <%=GuiLanguage.GetGuiLanguage("Shared").GetString("CommandPrevious")%></a>
        </div>
    </div>
</asp:Content>
