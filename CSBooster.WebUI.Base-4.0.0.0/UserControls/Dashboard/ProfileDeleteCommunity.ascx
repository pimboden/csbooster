<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.ProfileDeleteCommunity" CodeBehind="ProfileDeleteCommunity.ascx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<fieldset>
    <legend>
        <%=language.GetString("TitleProfileDelete")%>
    </legend>
    <div class="inputBlock">
        <div class="inputBlockLabel">
            <%=language.GetString("LableInfoToFriend")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipInfoToFriend")%>">&nbsp;&nbsp;&nbsp;</a>
        </div>
        <div class="inputBlockContent">
            <asp:CheckBox ID="cbxInfo" runat="server" />
        </div>
    </div>
    <asp:Panel ID="PnlPassword" runat="server" CssClass="inputBlock">
        <div class="inputBlockLabel">
            <%=language.GetString("LablePasswort")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipProfileDelete")%>">&nbsp;&nbsp;&nbsp;</a>
        </div>
        <div class="inputBlockContent">
            <asp:TextBox ID="TxtPw" runat="server" TextMode="Password"></asp:TextBox>
        </div>
    </asp:Panel>
    <div class="inputBlock">
        <div class="inputBlockContent">
            <div>
                <asp:Label ID="lblMsg" class="errorText" runat="server" Visible="False" />
            </div>
            <asp:LinkButton ID="lbtnDelete" OnClientClick="RemoveNodeByID('SecXmlToken');" runat="server" CssClass="inputButton" OnClick="OnDeleteProfileClick"><%=language.GetString("CommandProfileDelete")%></asp:LinkButton>
        </div>
    </div>
</fieldset>
