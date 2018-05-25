<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.ProfileChangePassword" CodeBehind="ProfileChangePassword.ascx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<fieldset>
    <legend>
        <%=language.GetString("TitlePassword")%>
    </legend>
    <div class="inputBlock">
        <div class="inputBlockLabel">
            <%=language.GetString("TitlePassword")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("CommandPasswordChange")%>">&nbsp;&nbsp;&nbsp;</a>
        </div>
        <div class="inputBlockContent">
            <asp:HyperLink ID="hplChange" CssClass="inputButton" runat="server"><%=language.GetString("CommandPasswordChange")%></asp:HyperLink>
        </div>
    </div>
</fieldset>
