<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.CustomizationBarTabStyles" CodeBehind="CustomizationBarTabStyles.ascx.cs" %>
<%@ Register Src="~/UserControls/CustomizationBarTabStyle.ascx" TagName="CustomizationBarTabStyle" TagPrefix="csb" %>
<div class="inputBlock">
    <%=language.GetString("TitleStyle")%>
</div>
<div class="inputBlock">
    <table class="cBarStyles" cellpadding="0" cellspacing="0">
        <tr>
            <th>
                <%=language.GetString("LableStyleHeader")%>
            </th>
            <th>
                <%=language.GetString("LableStyleBody")%>
            </th>
            <th>
                <%=language.GetString("LableStyleFooter")%>
            </th>
        </tr>
        <tr>
            <td valign="top">
                <csb:CustomizationBarTabStyle ID="SHHeader" PreviewElementId="header" Section="Header" runat="server" />
            </td>
            <td valign="top">
                <csb:CustomizationBarTabStyle ID="SHBody" PreviewElementId="content" Section="Body" runat="server" />
            </td>
            <td valign="top">
                <csb:CustomizationBarTabStyle ID="SHFooter" PreviewElementId="footer" Section="Footer" runat="server" />
            </td>
        </tr>
    </table>
</div>
<asp:Panel ID="pnlStatus" runat="server" Visible="false">
    <asp:Panel ID="litStatus" runat="server" />
</asp:Panel>
<div class="inputBlock">
    <asp:LinkButton ID="LbtnSave" runat="server" CssClass="inputButton" OnClick="OnSaveClick"><%=languageShared.GetString("CommandSave")%></asp:LinkButton>
    <asp:LinkButton ID="LbtnClose" runat="server" CssClass="inputButtonSecondary" OnClick="OnCloseClick"><%=languageShared.GetString("CommandClose")%></asp:LinkButton>
</div>
