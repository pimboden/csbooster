<%@ Page Language="C#" MasterPageFile="~/MasterPages/Static.master" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.Pages.Static.DoS" CodeBehind="DoS.aspx.cs" %>

<%@ Import Namespace="_4screen.Utils.Web" %>
<asp:Content ID="CWZ1" ContentPlaceHolderID="WCZ1" runat="Server">
    <div class="staticContent">
        <h1 class="staticContentTitleRow">
            <%=GuiLanguage.GetGuiLanguage("Pages.Static.WebUI.Base").GetString("MessageDoS")%>
        </h1>
    </div>
</asp:Content>
