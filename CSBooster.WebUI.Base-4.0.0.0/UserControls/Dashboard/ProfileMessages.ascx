<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.ProfileMessages" CodeBehind="ProfileMessages.ascx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<fieldset>
    <legend>
        <%=language.GetString("LableMessagePlural")%>
    </legend>
    <div class="inputBlock">
        <div class="inputBlockLabel">
            <%=language.GetString("LableMessagePlural")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipMessageFrom")%>">&nbsp;&nbsp;&nbsp;</a>
        </div>
        <div class="inputBlockContent">
            <asp:RadioButtonList ID="rblMsgFrom" runat="server">
                <asp:ListItem Selected="True" Value="All">von Usern</asp:ListItem>
                <asp:ListItem Value="Friends">von meinen Freunden</asp:ListItem>
                <asp:ListItem Value="Nobody">von niemanden</asp:ListItem>
            </asp:RadioButtonList>
        </div>
    </div>
    <div class="inputBlock">
        <div class="inputBlockLabel">
            <%=language.GetString("LableMessageByNew")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipMessageByNew")%>">&nbsp;&nbsp;&nbsp;</a>
        </div>
        <div class="inputBlockContent">
            <asp:RadioButtonList ID="rblEmail" runat="server">
                <asp:ListItem Selected="True" Value="Yes">Ja</asp:ListItem>
                <asp:ListItem Value="No">Nein</asp:ListItem>
            </asp:RadioButtonList>
        </div>
    </div>
    <div class="inputBlock">
        <div class="inputBlockContent">
            <asp:LinkButton ID="btnAds" runat="server" OnClientClick="RemoveNodeByID('SecXmlToken');" CssClass="inputButton" OnClick="OnSaveClick"><%=language.GetString("CommandSave")%></asp:LinkButton>
        </div>
    </div>
</fieldset>
