<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.TabBar" CodeBehind="TabBar.ascx.cs" %>
<div id="pageTabs">
    <asp:UpdatePanel ID="upnl" runat="server" UpdateMode="conditional">
        <ContentTemplate>
            <ul id="tabList" runat="server">
            </ul>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="up" AssociatedUpdatePanelID="upnl" runat="server">
        <ProgressTemplate>
            <div class="updateProgress">
                <%=languageShared.GetString("LabelUpdateProgress")%>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
