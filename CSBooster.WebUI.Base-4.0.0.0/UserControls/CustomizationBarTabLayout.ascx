<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.CustomizationBarTabLayout" CodeBehind="CustomizationBarTabLayout.ascx.cs" %>
<div class="inputBlock">
    <%=language.GetString("TitleLayoutSelect")%>
</div>
<div class="inputBlock">
    <asp:RadioButtonList ID="rblLayouts" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnLayoutChangeClick" RepeatLayout="Table" RepeatDirection="Horizontal" />
</div>
<div class="inputBlock">
    <asp:LinkButton ID="LbtnClose" runat="server" CssClass="inputButtonSecondary" OnClick="OnCloseClick"><%=languageShared.GetString("CommandClose")%></asp:LinkButton>
</div>
