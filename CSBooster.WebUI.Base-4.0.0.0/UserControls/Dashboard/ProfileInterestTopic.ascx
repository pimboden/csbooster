<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.ProfileInterestTopic" CodeBehind="ProfileInterestTopic.ascx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Repeater ID="rptTopics" runat="server" OnItemDataBound="rptTopics_OnItemDataBound">
    <ItemTemplate>
        <fieldset>
            <legend><asp:Label ID="lblTopic" runat="server" /> </legend>
            <div class="inputBlock">
                <div class="inputBlockLabel">
                    <%=languageProfile.GetString("LableSelect")%>
                </div>
                <div class="inputBlockContent">
                    <asp:CheckBoxList ID="cblSubTopic" runat="server" CssClass="dashboardSettingsTable" CellPadding="0" CellSpacing="0" RepeatColumns="2" RepeatDirection="Vertical" />
                </div>
            </div>
            <div class="inputBlock">
                <div class="inputBlockLabel">
                    <%=languageProfile.GetString("LableShow")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=languageProfile.GetStringForTooltip("TooltipShow")%>">&nbsp;&nbsp;&nbsp;</a>
                </div>
                <div class="inputBlockContent">
                    <asp:CheckBox ID="cbxGrp1" runat="server" />
                </div>
            </div>
        </fieldset>
    </ItemTemplate>
</asp:Repeater>
