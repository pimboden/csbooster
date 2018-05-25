<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WidgetTitleSettings.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.WidgetTitleSettings" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%= GuiLanguage.GetGuiLanguage("Shared").GetString("LabelWidgetTitle")%><a href="javascript:void(0)" class="inputNoHelp">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtTitle" runat="server" Width="99%" />
    </div>
    <div class="inputBlockError">
    </div>
</div>
