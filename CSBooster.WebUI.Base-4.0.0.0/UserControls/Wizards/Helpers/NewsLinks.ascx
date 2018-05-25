<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsLinks.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.Wizards.Helpers.NewsLinks" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div style="float: left; width: 100%; margin-bottom: 4px;">
    <div style="float: left; width: 8%;">
        <%=GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base").GetString("LabelNewsLinksTitle") %>:
    </div>
    <div style="float: left; width: 40%;">
        <asp:TextBox ID="TxtTitle" Width="96%" runat="server" />
    </div>
    <div style="float: left; width: 8%;">
        <%=GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base").GetString("LabelNewsLinksURL") %>:
    </div>
    <div style="float: left; width: 40%;">
        <asp:TextBox ID="TxtURL" Width="96%" runat="server" />
    </div>
    <div style="float: left; width: 4%;">
        <div style="float: right;">
            <asp:PlaceHolder ID="PhRemove" runat="server" />
        </div>
    </div>
</div>
