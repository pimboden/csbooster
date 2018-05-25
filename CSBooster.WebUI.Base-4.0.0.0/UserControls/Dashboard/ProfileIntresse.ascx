<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.ProfileIntresse" CodeBehind="ProfileIntresse.ascx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<fieldset>
    <legend>
        <%=languageProfile.GetString("Information")%>
    </legend>
    <div class="inputBlock">
        <div class="inputBlockLabel">
            <%=languageProfile.GetString("LableIntresst")%>
        </div>
        <div class="inputBlockContent">
            <asp:TextBox ID="txtIntress1" runat="server" TabIndex="1" CssClass="" Style="margin-bottom: 4px;"></asp:TextBox> <asp:TextBox ID="txtIntress2" runat="server" TabIndex="2" CssClass="" Style="margin-bottom: 4px;"></asp:TextBox> <asp:TextBox ID="txtIntress3" runat="server" TabIndex="3" CssClass="" Style="margin-bottom: 4px;"></asp:TextBox> <asp:TextBox ID="txtIntress4" runat="server" TabIndex="4" CssClass="" Style="margin-bottom: 4px;"></asp:TextBox> <asp:TextBox ID="txtIntress5" runat="server" TabIndex="5" CssClass="" Style="margin-bottom: 4px;"></asp:TextBox> <asp:TextBox ID="txtIntress6" runat="server" TabIndex="6" CssClass="" Style="margin-bottom: 4px;"></asp:TextBox> <asp:TextBox ID="txtIntress7" runat="server" TabIndex="7" CssClass="" Style="margin-bottom: 4px;"></asp:TextBox> <asp:TextBox ID="txtIntress8" runat="server" TabIndex="8" CssClass="" Style="margin-bottom: 4px;"></asp:TextBox> <asp:TextBox ID="txtIntress9" runat="server" TabIndex="9" CssClass="" Style="margin-bottom: 4px;"></asp:TextBox> <asp:TextBox ID="txtIntress10" runat="server" TabIndex="10" CssClass="" Style="margin-bottom: 4px;"></asp:TextBox>
        </div>
    </div>
    <div class="inputBlock">
        <div class="inputBlockLabel">
            <%=languageProfile.GetString("LableShow")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=languageProfile.GetStringForTooltip("TooltipShow")%>')" onmouseout="tooltip.hide()">&nbsp;&nbsp;&nbsp;</a>
        </div>
        <div class="inputBlockContent">
            <asp:CheckBox ID="cbxGrp1" TabIndex="11" runat="server" Checked="True" />
        </div>
    </div>
</fieldset>
