<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyContentItemRow.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.MyContentItemRow" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<div class="myContentItem">
    <div class="myContentItemTitle">
        <asp:HyperLink ID="LnkDet2" runat="server" />
    </div>
    <div class="myContentItemMain">
        <div class="myContentItemImage">
            <asp:HyperLink ID="LnkDet1" runat="server">
                <asp:Image ID="Img" runat="server" />
            </asp:HyperLink>
        </div>
        <div class="myContentItemInfo">
            <div class="myContentItemInfo1" title='<%=GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base").GetString("TooltipCreator")%>'>
                <asp:HyperLink ID="LnkAuthor" runat="server" />
            </div>
            <div class="myContentItemInfo2">
                <asp:Panel ID="PnlLoc" runat="server" ToolTip='<%=GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base").GetString("TooltipSavePlace")%>'>
                    <asp:HyperLink ID="LnkCty" runat="server" />
                </asp:Panel>
            </div>
            <div class="myContentItemInfo3">
                <asp:PlaceHolder ID="PhInfo" runat="server" />
            </div>
            <div class="myContentItemInfo4">
                <asp:PlaceHolder ID="PhFunc" runat="server" />
            </div>
        </div>
    </div>
</div>
