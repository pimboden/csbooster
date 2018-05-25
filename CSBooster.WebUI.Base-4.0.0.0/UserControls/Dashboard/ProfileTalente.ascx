<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.ProfileTalente" CodeBehind="ProfileTalente.ascx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="PnlJobTal" runat="server">
    <fieldset>
        <legend>
            <%=languageProfile.GetString("Information")%>
        </legend>
        <asp:Panel ID="PnlJob" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("LableBeruf")%>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="txtBeruf" runat="server" TabIndex="301" CssClass=""></asp:TextBox>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlSlo" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("LableMotto")%>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="txtMotto" runat="server" TabIndex="301" CssClass=""></asp:TextBox>
            </div>
        </asp:Panel>
        <asp:Repeater ID="RepTalents" runat="server" OnItemDataBound="OnRepTalentsBinding">
            <ItemTemplate>
                <div class="inputBlock">
                    <div class="inputBlockLabel">
                        <asp:Literal ID="LitTalent" runat="server" />
                    </div>
                    <div class="inputBlockContent">
                        <asp:TextBox ID="TxtTalent" runat="server" CssClass=""></asp:TextBox>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("LableShow")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=languageProfile.GetStringForTooltip("TooltipShow")%>">&nbsp;&nbsp;&nbsp;</a>
            </div>
            <div class="inputBlockContent">
                <asp:CheckBox ID="cbxGrp1" TabIndex="399" runat="server" />
            </div>
        </div>
    </fieldset>
</asp:Panel>
