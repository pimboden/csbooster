<%@ Page Language="C#" MasterPageFile="~/MasterPages/Static.master" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.Pages.Static.AccessDenied" CodeBehind="AccessDenied.aspx.cs" %>

<asp:Content ID="CWZ1" ContentPlaceHolderID="WCZ1" runat="Server">
    <div class="staticContent">
        <h1 class="staticContentTitleRow">
            <%=language.GetString("MessageAccessDenied")%>
        </h1>
        <div class="staticContentRow">
            <a href="javascript:history.back()">
                <%=languageShared.GetString("CommandPrevious")%></a>
            <%=languageShared.GetString("TextOr")%>
            <a href="<%=_4screen.CSB.Common.Constants.Links["LINK_TO_CONTACT_PAGE"] %>">
                <%=language.GetString("TextGoToContact")%></a>
        </div>
    </div>
</asp:Content>
