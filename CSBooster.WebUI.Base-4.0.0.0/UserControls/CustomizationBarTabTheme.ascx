<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.CustomizationBarTabTheme" CodeBehind="CustomizationBarTabTheme.ascx.cs" %>
<div class="inputBlock">
    <%=language.GetString("TitleThemeSelect")%>
</div>
<div class="inputBlock">
    <asp:RadioButtonList ID="rblThemes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnLayoutChangeClick" RepeatLayout="Table" RepeatDirection="Horizontal" />
</div>
<div class="inputBlock">
    <asp:LinkButton ID="LbtnSave" runat="server" CssClass="inputButtonSecondary" OnClick="OnCloseClick"><%=languageShared.GetString("CommandClose")%></asp:LinkButton>
</div>
