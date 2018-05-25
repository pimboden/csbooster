<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.ObjectTagHandler" CodeBehind="ObjectTagHandler.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<fieldset>
    <legend>
        <%=language.GetString("TitleTagging")%>
    </legend>
    <asp:Panel ID="PnlTH" runat="server">
        <asp:Panel ID="MAINTAGPNL" CssClass="inputBlock" runat="server">
            <div class="inputBlockLabel">
                <asp:Label ID="MAINTAGLBL" runat="server"><%=language.GetString("LableTaggingCategory")%>:</asp:Label>
            </div>
            <div class="inputBlockContent">
                <telerik:RadComboBox ID="ddlMainCat" ZIndex="90000" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedGroupChanged" Width="100%" />
            </div>
        </asp:Panel>
        <asp:Panel ID="CATTAGPNL" CssClass="inputBlock" runat="server">
            <div class="inputBlockLabel">
                <asp:Label ID="CATTAGLBL" runat="server"><%=language.GetString("LableTaggingSubCategory")%>:</asp:Label>
            </div>
            <div class="inputBlockContent">
                <telerik:RadComboBox ID="ddlSubCat" ZIndex="90000" runat="server" Width="100%" />
            </div>
        </asp:Panel>
        <asp:Panel ID="TAGPNL" CssClass="inputBlock" runat="server">
            <div class="inputBlockLabel">
                <asp:Label ID="TAGLBL" runat="server"><%=language.GetString("LableTaggingFree")%>:</asp:Label>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtFreeTags" runat="Server" Width="99%" />
            </div>
        </asp:Panel>
    </asp:Panel>
</fieldset>
