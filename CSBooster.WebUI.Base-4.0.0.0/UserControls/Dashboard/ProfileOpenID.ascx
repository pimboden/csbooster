<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.ProfileOpenID" CodeBehind="ProfileOpenID.ascx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<fieldset>
    <legend>
        <%=language.GetString("TitleOpenId")%>
    </legend>
    <div class="inputBlock">
        <div class="inputBlockLabel">
            <%=language.GetString("LableOpenId")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipOpenId")%>">&nbsp;&nbsp;&nbsp;</a>
        </div>
        <div class="inputBlockContent">
            <asp:Literal ID="LitOpenIDCur" runat="server" />
        </div>
    </div>
    <div class="inputBlock">
        <div class="inputBlockLabel">
            <%=language.GetString("LableOpenIdNew")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipOpenIdNew")%>">&nbsp;&nbsp;&nbsp;</a>
        </div>
        <div class="inputBlockContent">
            <asp:TextBox ID="TxtOpenIDNew" runat="server" Width="300px"></asp:TextBox>
        </div>
    </div>
    <div class="inputBlock">
        <div class="inputBlockContent">
            <div>
                <asp:Literal ID="LitOpenIDMsg" runat="server" />
            </div>
            <asp:LinkButton ID="LbtnOpenIDRemove" OnClientClick="RemoveNodeByID('SecXmlToken');" runat="server" CssClass="inputButton" OnClick="LbtnOpenIDRemoveClick"><%=language.GetString("CommandOpenIdRemove")%></asp:LinkButton>
            <asp:LinkButton ID="LbtnOpenIDAdd" OnClientClick="RemoveNodeByID('SecXmlToken');" runat="server" CssClass="inputButton" OnClick="LbtnOpenIDAddClick"><%=language.GetString("CommandOpenIdConfirm")%></asp:LinkButton>
        </div>
    </div>
</fieldset>
