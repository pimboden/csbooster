<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="RelatedObjectListsAdv.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.RelatedObjectListsAdv" %>
<%@ Register Assembly="CSBooster.WidgetControls" Namespace="_4screen.CSB.WidgetControls" TagPrefix="csb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="PnlByUrl" runat="server" CssClass="CSB_input_block">
            <div class="CSB_input_label">
                <csb:LabelControl ID="Label4" LabelFile="WidgetRelatedObjectLists" LabelKey="LabelByUrl" ToolTipFile="WidgetRelatedObjectLists" ToolTipKey="TooltipByUrl" runat="server">
                </csb:LabelControl>
            </div>
            <div class="CSB_input_cnt">
                <asp:CheckBox ID="CbxByUrl" runat="server" AutoPostBack="true" />
            </div>
            <div class="CSB_error_cnt">
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlParentOID" runat="server" CssClass="CSB_input_block">
            <div class="CSB_input_label">
                <csb:LabelControl ID="Lbl1" LabelFile="WidgetRelatedObjectLists" LabelKey="LabelParentOID" ToolTipFile="WidgetRelatedObjectLists" ToolTipKey="TooltipParentOID" runat="server">
                </csb:LabelControl>
            </div>
            <div class="CSB_input_cnt">
                <div>
                    <asp:TextBox ID="TxtParentOID" runat="server" Width="97%"></asp:TextBox>
                </div>
            </div>
            <div class="CSB_error_cnt">
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlFeatured" runat="server" CssClass="CSB_input_block">
            <div class="CSB_input_label">
                <csb:LabelControl ID="LabelControl4" LabelFile="WidgetRelatedObjectLists" LabelKey="LabelFeatured" ToolTipFile="WidgetRelatedObjectLists" ToolTipKey="TooltipFeatured" runat="server">
                </csb:LabelControl>
            </div>
            <div class="CSB_input_cnt">
                <telerik:RadComboBox ID="CbxFeatured" runat="server">
                    <Items>
                        <telerik:RadComboBoxItem Text="all" Value="0" Selected= "true" />  
                        <telerik:RadComboBoxItem Text="high" Value="1" Selected= "true" />  
                        <telerik:RadComboBoxItem Text="medium" Value="2" Selected= "true" />  
                        <telerik:RadComboBoxItem Text="low" Value="3" Selected= "true" />  
                    </Items> 
                </telerik:RadComboBox>
            </div>
            <div class="CSB_error_cnt">
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlDataSource" runat="server" CssClass="CSB_input_block">
            <div class="CSB_input_label">
                <csb:LabelControl ID="Label5" LabelFile="WidgetRelatedObjectLists" LabelKey="LabelDataSource" ToolTipFile="WidgetRelatedObjectLists" ToolTipKey="TooltipDataSource" runat="server">
                </csb:LabelControl>
            </div>
            <div class="CSB_input_cnt">
                <telerik:RadComboBox ID="CbxDataSource" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CbxDataSource_SelectedIndexChanged">
                </telerik:RadComboBox>
            </div>
            <div class="CSB_error_cnt">
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlCommunity" runat="server" CssClass="CSB_input_block">
            <div class="CSB_input_label">
                <csb:LabelControl ID="LabelControl1" LabelFile="WidgetRelatedObjectLists" LabelKey="LabelCommunity" ToolTipFile="WidgetRelatedObjectLists" ToolTipKey="TooltipCommunity" runat="server">
                </csb:LabelControl>
            </div>
            <div class="CSB_input_cnt">
                <div>
                    <asp:Literal ID="LitCommunity" runat="server"></asp:Literal>
                </div>
                <div>
                    <asp:TextBox ID="TxtCommunity" runat="server" Width="97%"></asp:TextBox>
                </div>
            </div>
            <div class="CSB_error_cnt">
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlUser" runat="server" CssClass="CSB_input_block">
            <div class="CSB_input_label">
                <csb:LabelControl ID="LabelControl2" LabelFile="WidgetRelatedObjectLists" LabelKey="LabelUser" ToolTipFile="WidgetRelatedObjectLists" ToolTipKey="TooltipUser" runat="server">
                </csb:LabelControl>
            </div>
            <div class="CSB_input_cnt">
                <asp:TextBox ID="TxtUser" runat="server" Width="40%"></asp:TextBox>
            </div>
            <div class="CSB_error_cnt">
            </div>
        </asp:Panel>
        
        <div class="CSB_input_block">
            <div class="CSB_input_label">
                <csb:LabelControl ID="LabelControl3" LabelFile="WidgetRelatedObjectLists" LabelKey="LabelTagList" ToolTipFile="WidgetRelatedObjectLists" ToolTipKey="TooltipTagList" runat="server">
                </csb:LabelControl>
            </div>
            <div class="CSB_input_cnt">
                <div>
                    <asp:TextBox ID="TxtTagList1" runat="server" Width="97%"></asp:TextBox>
                </div>
                <div>
                    <%=language.GetString("LabelTagOr")%>
                </div>
                <div>
                    <asp:TextBox ID="TxtTagList2" runat="server" Width="97%"></asp:TextBox>
                </div>
                <div>
                    <%=language.GetString("LabelTagOr")%>
                </div>
                <div>
                    <asp:TextBox ID="TxtTagList3" runat="server" Width="97%"></asp:TextBox>
                </div>
            </div>
            <div class="CSB_error_cnt">
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>