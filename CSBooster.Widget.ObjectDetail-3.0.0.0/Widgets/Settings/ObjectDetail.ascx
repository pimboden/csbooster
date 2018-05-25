<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="ObjectDetail.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.ObjectDetail" %>
<%@ Register Assembly="CSBooster.WidgetControls" Namespace="_4screen.CSB.WidgetControls" TagPrefix="csb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
    <contenttemplate>
        <asp:Panel ID="PnlByUrl" runat="server" CssClass="CSB_input_block">
            <div class="CSB_input_label">
                <csb:LabelControl ID="Label1" LabelFile="WidgetObjectDetail" LabelKey="LabelByUrl" ToolTipFile="WidgetObjectDetail" ToolTipKey="TooltipByUrl" runat="server">
                </csb:LabelControl>
            </div>
            <div class="CSB_input_cnt">
                <asp:CheckBox ID="CbxByUrl" runat="server" OnCheckedChanged="CbxByUrl_CheckedChanged" AutoPostBack="True" />
            </div>
            <div class="CSB_error_cnt">
            </div>
        </asp:Panel>
        <asp:PlaceHolder ID="PhSearch" runat="server">
        
            <div class="CSB_input_block">
                <div class="CSB_input_label">
                    <csb:LabelControl ID="LabelControl2" LabelFile="WidgetObjectDetail" LabelKey="LabelSource" ToolTipFile="WidgetObjectDetail" ToolTipKey="TooltipSource" runat="server">
                    </csb:LabelControl>
                </div>
                <div class="CSB_input_cnt">
                    <telerik:RadComboBox ID="CbxOrderBy" runat="server">
                    </telerik:RadComboBox>
                </div>
                <div class="CSB_error_cnt">
                </div>
             </div>
        
            <asp:Panel ID="PnlSearch" runat="server" CssClass="CSB_input_block">
                <div class="CSB_input_label">
                    <csb:LabelControl ID="Label2" LabelFile="WidgetObjectDetail" LabelKey="LabelSearch" ToolTipFile="WidgetObjectDetail" ToolTipKey="TooltipSearch" runat="server">
                    </csb:LabelControl>
                </div>
                <div class="CSB_input_cnt">
                    <asp:UpdatePanel ID="up2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div style="float: left; margin-bottom: 5px; width:100%">
                                <div style="float: left; margin-left: 4px; width:25%">
                                    <asp:TextBox ID="TxtSearch" runat="server" Width="99%" />
                                </div>
                                <div style="float: left; margin-left: 4px;>
                                    <csb:LabelControl ID="LabelControl1" LabelFile="WidgetObjectDetail" LabelKey="LabelTagword" ToolTipFile="WidgetObjectDetail" ToolTipKey="TooltipTagword" runat="server">
                                    </csb:LabelControl>
                                </div>
                                <div style="float: left; margin-left: 4px; width:25%">
                                    <asp:TextBox ID="TxtTagword" runat="server" Width="100%" />
                                </div>
                                <div style="float: left; margin-left: 4px;">
                                    <asp:LinkButton ID="lbtnSR" CssClass="CSB-btn-150-l" runat="server" Text="Suchen" OnClick="lbtnSR_Click"><%=languageShared.GetString("CommandSearch")%></asp:LinkButton>
                                </div>
                            </div>
                            <div style="height: 300px; overflow : auto; float:left; width:100%">
                                <asp:Repeater ID="OBJOVW" runat="server" OnItemDataBound="OBJOVW_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:PlaceHolder ID='PhItem' runat="server" EnableViewState="false" />
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="CSB_clear">
                                </div>
                            </div>
                            <asp:HiddenField ID="TxtSelected" runat="server"></asp:HiddenField>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="CSB_error_cnt">
                </div>
            </asp:Panel>
        </asp:PlaceHolder>         
    </contenttemplate>
</asp:UpdatePanel>
