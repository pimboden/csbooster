<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="ObjectListsAdv.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.ObjectListsAdv" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
    <contenttemplate>
        <asp:Panel ID="PnlByUrl" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LabelByUrl" LanguageFile="WidgetRelatedObjectLists" LabelKey="LabelByUrl" TooltipKey="TooltipByUrl" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <asp:CheckBox ID="CbxByUrl" runat="server" />
            </div>
            <div class="inputBlockError">
            </div>
        </asp:Panel>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LabelFeatured" LanguageFile="WidgetRelatedObjectLists" LabelKey="LabelFeatured" TooltipKey="TooltipFeatured" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <telerik:RadComboBox ID="CbxFeatured" runat="server">
                    <Items>
                        <telerik:RadComboBoxItem Value="0" Selected= "true" />
                        <telerik:RadComboBoxItem Value="1" />
                        <telerik:RadComboBoxItem Value="2" />
                        <telerik:RadComboBoxItem Value="3" />
                    </Items> 
                </telerik:RadComboBox>
            </div>
            <div class="inputBlockError">
            </div>
        </div>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LabelDateConstraint" LanguageFile="WidgetRelatedObjectLists" LabelKey="LabelDateConstraint" TooltipKey="TooltipDateConstraint" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <telerik:RadComboBox ID="RcbDateConst" runat="server">
                    <Items>
                        <telerik:RadComboBoxItem Value="None" Selected= "true" />
                        <telerik:RadComboBoxItem Value="UntilYesterday" />
                        <telerik:RadComboBoxItem Value="UntilToday" />
                        <telerik:RadComboBoxItem Value="Today" />
                        <telerik:RadComboBoxItem Value="FromToday" />
                        <telerik:RadComboBoxItem Value="FromTomorrow" />
                    </Items> 
                </telerik:RadComboBox>
            </div>
            <div class="inputBlockError">
            </div>
        </div>
        <asp:Panel ID="PnlDataSource" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="Label5" LanguageFile="WidgetRelatedObjectLists" LabelKey="LabelDataSource" TooltipKey="TooltipDataSource" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <telerik:RadComboBox ID="CbxDataSource" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CbxDataSource_SelectedIndexChanged">
                </telerik:RadComboBox>
            </div>
            <div class="inputBlockError">
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlCommunity" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LabelCommunity" LanguageFile="WidgetRelatedObjectLists" LabelKey="LabelCommunity" TooltipKey="TooltipCommunity" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <div>
                    <asp:Literal ID="LitCommunity" runat="server"></asp:Literal>
                </div>
                <div>
                    <asp:TextBox ID="TxtCommunity" runat="server" Width="97%"></asp:TextBox>
                </div>
            </div>
            <div class="inputBlockError">
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlUser" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LabelUser" LanguageFile="WidgetRelatedObjectLists" LabelKey="LabelUser" TooltipKey="TooltipUser" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtUser" runat="server" Width="40%"></asp:TextBox>
            </div>
            <div class="inputBlockError">
            </div>
        </asp:Panel>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LabelTagList" LanguageFile="WidgetRelatedObjectLists" LabelKey="LabelTagList" TooltipKey="TooltipTagList" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <div>
                    <asp:TextBox ID="TxtTagList1" runat="server" Width="97%"></asp:TextBox>
                </div>
                <div>
                    <%=language.GetString("LabelTagAnd")%>
                </div>
                <div>
                    <asp:TextBox ID="TxtTagList2" runat="server" Width="97%"></asp:TextBox>
                </div>
                <div>
                    <%=language.GetString("LabelTagAnd")%>
                </div>
                <div>
                    <asp:TextBox ID="TxtTagList3" runat="server" Width="97%"></asp:TextBox>
                </div>
            </div>
            <div class="inputBlockError">
            </div>
        </div>
        <div class="inputBlock">
        </div>
    </contenttemplate>
</asp:UpdatePanel>
