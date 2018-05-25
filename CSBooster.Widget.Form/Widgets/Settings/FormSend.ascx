<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormSend.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.FormSend" %>
<%@ Register Assembly="CSBooster.WidgetControls" Namespace="_4screen.CSB.WidgetControls" TagPrefix="csb" %>
<asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="CSB_input_block">
            <div class="CSB_input_label">
                <csb:LabelControl ID="Label1" LabelFile="WidgetForm" LabelKey="LabelSubject" ToolTipFile="WidgetForm" ToolTipKey="TooltipSubject" runat="server">
                </csb:LabelControl>
            </div>
            <div class="CSB_input_cnt">
                <asp:TextBox ID="TxtSubject" runat="server" MaxLength="100" Width="99%"></asp:TextBox>
            </div>
            <div class="CSB_error_cnt">
                <asp:RequiredFieldValidator ID="RFVSubject" runat="server" ErrorMessage="Pflichtfeld" ControlToValidate="TxtSubject" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="CSB_input_block">
            <div class="CSB_input_label">
                <csb:LabelControl ID="Label2" LabelFile="WidgetForm" LabelKey="LabelTextPre" ToolTipFile="WidgetForm" ToolTipKey="TooltipTextPre" runat="server">
                </csb:LabelControl>
            </div>
            <div class="CSB_input_cnt">
                <asp:TextBox ID="TxtVor" runat="server" Width="99%" MaxLength="500" TextMode="MultiLine" Rows="3"></asp:TextBox>
            </div>
            <div class="CSB_error_cnt">
            </div>
        </div>
        <div class="CSB_input_block">
            <div class="CSB_input_label">
                <csb:LabelControl ID="Label3" LabelFile="WidgetForm" LabelKey="LabelTextPost" ToolTipFile="WidgetForm" ToolTipKey="TooltipTextPost" runat="server">
                </csb:LabelControl>
            </div>
            <div class="CSB_input_cnt">
                <asp:TextBox ID="TxtNach" runat="server" Width="99%" MaxLength="500" TextMode="MultiLine" Rows="3"></asp:TextBox>
            </div>
            <div class="CSB_error_cnt">
            </div>
        </div>
        <div class="CSB_input_block">
            <div class="CSB_input_label">
                <csb:LabelControl ID="Label4" LabelFile="WidgetForm" LabelKey="LabelUserCopy" ToolTipFile="WidgetForm" ToolTipKey="TooltipUserCopy" runat="server">
                </csb:LabelControl>
            </div>
            <div class="CSB_input_cnt">
                <asp:DropDownList ID="Ddl_UserCopy" runat="server"></asp:DropDownList>
            </div>
            <div class="CSB_error_cnt">
            </div>
        </div>
        <div class="CSB_input_block">
            <div class="CSB_input_label">
                <csb:LabelControl ID="Label5" LabelFile="WidgetForm" LabelKey="LabelSendForm" ToolTipFile="WidgetForm" ToolTipKey="TooltipSendForm" runat="server">
                </csb:LabelControl>
            </div>
            <div class="CSB_input_cnt">
                <asp:RadioButtonList ID="RblSendAs" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="RblSendAs_SelectedIndexChanged">
                    <asp:ListItem Text="als Email" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="als interne Mitteilung" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
                <br />
                <asp:Literal ID="LitUser" runat="server"></asp:Literal>&nbsp;<asp:DropDownList ID="DdlUser" runat="server">
                </asp:DropDownList>
                <br />
                <asp:Literal ID="LitNickname" runat="server"></asp:Literal>&nbsp;<asp:TextBox ID="TxtNickname" runat="server" Width="20%" MaxLength="100"></asp:TextBox>
                <asp:Literal ID="LitOr" runat="server"></asp:Literal>
                <asp:Literal ID="LitEmail" runat="server"></asp:Literal><asp:TextBox ID="TxtEmail" runat="server" Width="20%" MaxLength="150"></asp:TextBox>
                <asp:Literal ID="LitMsg" runat="server" Visible="false"></asp:Literal>
            </div>
            <div class="CSB_error_cnt">
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
