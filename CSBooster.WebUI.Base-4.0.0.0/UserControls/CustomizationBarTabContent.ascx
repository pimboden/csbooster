<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomizationBarTabContent.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.CustomizationBarTabContent" %>
<%@ Register Src="~/UserControls/Dashboard/MyContent.ascx" TagName="MyContent" TagPrefix="csb" %>
<%@ Register Src="~/UserControls/Dashboard/MyContentSearch.ascx" TagName="MyContentSearch" TagPrefix="csb" %>
<div class="inputBlock">
    <div style="float: left; width: 18%;">
        <csb:MyContentSearch ID="MyContentSearch" runat="server" />
    </div>
    <div style="float: right; width: 80%;">
        <csb:MyContent ID="MyContent" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <asp:LinkButton ID="LbtnClose" runat="server" CssClass="inputButtonSecondary" OnClick="OnCloseClick"><%=languageShared.GetString("CommandClose")%></asp:LinkButton>
</div>
