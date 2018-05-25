<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.ProfileCommunication" CodeBehind="ProfileCommunication.ascx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="PnlEma" runat="server">
    <fieldset>
        <legend>
            <%=languageProfile.GetString("TitleEmail")%>
        </legend>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("TitleEmail")%>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtEmail" runat="server" TabIndex="200" CssClass="" />
                <asp:Literal ID="LitEmailMsg" runat="server" />
            </div>
        </div>
    </fieldset>
</asp:Panel>
<asp:Panel ID="PnlPho" runat="server">
    <fieldset>
        <legend>
            <%=languageProfile.GetString("TitlePhone")%>
        </legend>
        <asp:Panel ID="PnlMob" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("LableMobile")%>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="txtMobile" runat="server" TabIndex="201" CssClass="" />
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlLan" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("LablePhone")%>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="txtPhone" runat="server" TabIndex="202" CssClass="" />
            </div>
        </asp:Panel>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("LableShow")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=languageProfile.GetStringForTooltip("TooltipShow")%>">&nbsp;&nbsp;&nbsp;</a>
            </div>
            <div class="inputBlockContent">
                <asp:CheckBox ID="cbxGrp1" TabIndex="203" runat="server" />
            </div>
        </div>
    </fieldset>
</asp:Panel>
<asp:Panel ID="PnlCom" runat="server">
    <fieldset>
        <legend>
            <%=languageProfile.GetString("TitleMessenger")%>
        </legend>
        <asp:Panel ID="PnlMSN" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("LableMSN")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=languageProfile.GetStringForTooltip("TooltipMSN")%>">&nbsp;&nbsp;&nbsp;</a>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="txtMSN" runat="server" TabIndex="204" CssClass="" />
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlYah" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("LableYahoo")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=languageProfile.GetStringForTooltip("TooltipYahoo")%>">&nbsp;&nbsp;&nbsp;</a>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="txtYahoo" runat="server" TabIndex="205" CssClass="" />
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlSky" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("LableSkype")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=languageProfile.GetStringForTooltip("TooltipSkype")%>">&nbsp;&nbsp;&nbsp;</a>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="txtSkype" runat="server" TabIndex="206" CssClass="" />
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlICQ" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("LableICQ")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=languageProfile.GetStringForTooltip("TooltipICQ")%>">&nbsp;&nbsp;&nbsp;</a>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="txtICQ" runat="server" TabIndex="207" CssClass="" />
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlAIM" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("LableAIM")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=languageProfile.GetStringForTooltip("TooltipAIM")%>">&nbsp;&nbsp;&nbsp;</a>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="txtAIM" runat="server" TabIndex="208" CssClass="" />
            </div>
        </asp:Panel>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("LableShow")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=languageProfile.GetStringForTooltip("TooltipShow")%>">&nbsp;&nbsp;&nbsp;</a>
            </div>
            <div class="inputBlockContent">
                <asp:CheckBox ID="cbxGrp2" TabIndex="209" runat="server" />
            </div>
        </div>
    </fieldset>
</asp:Panel>
<asp:Panel ID="PnlWeb" runat="server">
    <fieldset>
        <legend>
            <%=languageProfile.GetString("TitleWeb")%>
        </legend>
        <asp:Panel ID="PnlHP" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("LableHomepage")%>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="txtHomepage" runat="server" TabIndex="210" CssClass="" />
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlBlo" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("LableBlog")%>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="txtBlog" runat="server" TabIndex="211" CssClass="" />
            </div>
        </asp:Panel>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("LableShow")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=languageProfile.GetStringForTooltip("TooltipShow")%>">&nbsp;&nbsp;&nbsp;</a>
            </div>
            <div class="inputBlockContent">
                <asp:CheckBox ID="cbxGrp3" TabIndex="212" runat="server" />
            </div>
        </div>
    </fieldset>
</asp:Panel>
