<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.ObjectTagHandlerSelector" CodeBehind="ObjectTagHandlerSelector.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<fieldset>
    <legend>
        <%=GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("TitleTagging")%>
    </legend>
    <div class="inputBlock">
        <div class="inputBlockLabel">
            <asp:Label ID="TAGLBL" runat="server" Text="Tags:" />
        </div>
        <div class="inputBlockContent">
            <div class="CSB_tag_handler1">
                <asp:TextBox ID="TxtTags" runat="server" CssClass="CSB_tag_handler2" />
            </div>
            <div class="CSB_tag_handler3">
                <asp:CheckBoxList ID="CblTags" RepeatColumns="1" RepeatDirection="Horizontal" CellPadding="1" CellSpacing="0" Width="100%" runat="server" />
            </div>
        </div>
    </div>
</fieldset>
