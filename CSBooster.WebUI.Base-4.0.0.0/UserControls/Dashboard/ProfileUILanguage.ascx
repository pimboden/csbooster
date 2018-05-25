<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.ProfileUILanguage" CodeBehind="ProfileUILanguage.ascx.cs" %>
<fieldset>
    <legend>
        <%=language.GetString("TitleLanguage")%>
    </legend>
    <div class="inputBlock">
        <div class="inputBlockLabel">
            <%=language.GetString("LableLanguage")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipLanguage")%>">&nbsp;&nbsp;&nbsp;</a>
        </div>
        <div class="inputBlockContent">
            <asp:DropDownList ID="DdlLang" runat="server" />
        </div>
    </div>
    <div class="inputBlock">
        <div class="inputBlockContent">
            <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="RemoveNodeByID('SecXmlToken');" CssClass="inputButton" Text="Speichern" OnClick="OnSaveClick"><%=language.GetString("CommandSave")%></asp:LinkButton>
        </div>
    </div>
</fieldset>
