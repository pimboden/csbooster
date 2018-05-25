<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.ProfileShowAds" CodeBehind="ProfileShowAds.ascx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<fieldset>
    <legend>
        <%=language.GetString("TitleAds")%>
    </legend>
    <div class="inputBlock">
        <div class="inputBlockLabel">
            <%=language.GetString("LableAds")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipAds")%>">&nbsp;&nbsp;&nbsp;</a>
        </div>
        <div class="inputBlockContent">
            <asp:CheckBox ID="SHOWADSCB" runat="server" />
        </div>
    </div>
    <div class="inputBlock">
        <div class="inputBlockContent">
            <asp:LinkButton ID="btnAds" runat="server" OnClientClick="RemoveNodeByID('SecXmlToken');" CssClass="inputButton" OnClick="OnSaveClick"><%=language.GetString("CommandSave")%></asp:LinkButton>
        </div>
    </div>
</fieldset>
