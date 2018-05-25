<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.ObjectTagHandlerSimple" CodeBehind="ObjectTagHandlerSimple.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<fieldset>
    <legend>
        <%=GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("TitleTagging")%>
    </legend>
    <div id="Div1" class="inputBlock" runat="server">
        <div class="inputBlockLabel">
            <asp:Label ID="TAGLBL" runat="server" Text="Tags:" /><span class="inputNoHelp">&nbsp;&nbsp;&nbsp;</span>
        </div>
        <div class="inputBlockContent">
            <asp:TextBox ID="TxtTags" runat="Server" Width="99%" />
        </div>
    </div>
</fieldset>
