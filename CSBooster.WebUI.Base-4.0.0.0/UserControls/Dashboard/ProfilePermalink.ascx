<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.ProfilePermalink" CodeBehind="ProfilePermalink.ascx.cs" %>
<fieldset>
    <legend>
        <%=language.GetString("TitlePermalink")%>
    </legend>
    <div class="inputBlock">
        <div class="inputBlockLabel">
            <%=language.GetString("LablePermalink")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipPermalink")%>">&nbsp;&nbsp;&nbsp;</a>
        </div>
        <div class="inputBlockContent">
            <asp:Label ID="LblProfUrl" runat="server" EnableViewState="false" />
        </div>
    </div>
    <div class="inputBlock">
        <div class="inputBlockContent">
            <asp:HyperLink ID="hplMail" CssClass="inputButton" runat="server"><%=language.GetString("CommandPermalinkSend")%></asp:HyperLink>
        </div>
    </div>
</fieldset>
