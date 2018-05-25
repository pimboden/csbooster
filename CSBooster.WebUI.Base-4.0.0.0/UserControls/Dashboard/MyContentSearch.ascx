<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.MyContentSearch" CodeBehind="MyContentSearch.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:UpdatePanel ID="upnl" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="inputBlock">
            <asp:TextBox ID="TxtSearch" runat="server" Width="97%" onkeypress="return DisableEnterKey(event)" />
        </div>
        <asp:Panel ID="PnlType" runat="server" CssClass="inputBlock">
            <div class="inputTitle">
                <%=language.GetString("LableObjectType")%>
            </div>
            <div class="inputBlock">
                <telerik:RadComboBox ID="DdObjType" ZIndex="90000" runat="server" Width="100%" AllowCustomText="false" AutoPostBack="True" OnSelectedIndexChanged="OnDropDownChange" />
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlCommunities" runat="server" CssClass="inputBlock">
            <div class="inputTitle">
                <%=language.GetString("LableContentType")%>
            </div>
            <div class="inputBlock">
                <telerik:RadComboBox ID="DdMyCont" ZIndex="90000" runat="server" Width="100%" DropDownWidth="200px" AutoPostBack="True" OnSelectedIndexChanged="OnDropDownChange">
                    <Items>
                        <telerik:RadComboBoxItem Value="true" />
                        <telerik:RadComboBoxItem Value="false" />
                    </Items>
                </telerik:RadComboBox>
            </div>
            <div class="inputTitle">
                <%=language.GetString("LableInCommunities")%>
            </div>
            <div class="inputBlock">
                <telerik:RadComboBox ID="DdComms" ZIndex="90000" runat="server" Width="100%" DropDownWidth="200px" />
            </div>
        </asp:Panel>
        <div class="inputBlock">
            <div class="inputTitle">
                <%=language.GetString("LabelTags")%>
            </div>
            <telerik:RadListBox ID="RlbTags" runat="server" OnItemDataBound="OnTagItemDataBound" CheckBoxes="true" Height="100" Width="100%" />
        </div>
        <div>
            <asp:LinkButton ID="LbtnSearch" runat="server" CssClass="inputButton" Style="margin-bottom: 4px;" OnClick="OnFindClick"><%=languageShared.GetString("CommandSearch")%></asp:LinkButton>
            <asp:LinkButton ID="LbtnReset" runat="server" CssClass="inputButtonSecondary" OnClick="OnResetClick"><%=languageShared.GetString("CommandReset")%></asp:LinkButton>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="up" AssociatedUpdatePanelID="upnl" runat="server">
    <ProgressTemplate>
        <div class="updateProgress">
            <%=languageShared.GetString("LabelUpdateProgress")%>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
