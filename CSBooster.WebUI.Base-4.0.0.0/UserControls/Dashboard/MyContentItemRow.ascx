<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyContentItemRow.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.MyContentItemRow" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<div class="myContentRow">
    <div class="myContentRow1">
        <asp:HyperLink ID="LnkDet1" runat="server">
            <asp:Image ID="Img" runat="server" />
        </asp:HyperLink>
    </div>
    <div class="myContentRow2">
        <asp:HyperLink ID="LnkDet2" runat="server" />
    </div>
    <div class="myContentRow3" title='<%=GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base").GetString("TooltipCreator")%>'>
        <asp:HyperLink ID="LnkAuthor" runat="server" />
    </div>
    <div class="myContentRow4">
        <asp:Panel ID="PnlLoc" runat="server">
            <asp:HyperLink ID="LnkCty" runat="server" />
        </asp:Panel>
    </div>
    <div class="myContentRow5">
        <asp:PlaceHolder ID="PhInfo" runat="server" />
    </div>
    <div class="myContentRow6">
        <asp:PlaceHolder ID="PhFunc" runat="server" />
    </div>
</div>
