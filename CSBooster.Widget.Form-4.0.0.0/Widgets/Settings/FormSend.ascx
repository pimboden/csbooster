<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormSend.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.FormSend" %>

<asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="Label1" LanguageFile="WidgetForm" LabelKey="LabelSubject" ToolTipKey="TooltipSubject" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtSubject" runat="server" MaxLength="100" Width="99%"></asp:TextBox>
            </div>
            <div class="inputBlockError">
                <asp:RequiredFieldValidator ID="RFVSubject" runat="server" ErrorMessage="Pflichtfeld" ControlToValidate="TxtSubject" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="Label2" LanguageFile="WidgetForm" LabelKey="LabelTextPre" ToolTipKey="TooltipTextPre" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtVor" runat="server" Width="99%" MaxLength="500" TextMode="MultiLine" Rows="3"></asp:TextBox>
            </div>
            <div class="inputBlockError">
            </div>
        </div>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="Label3" LanguageFile="WidgetForm" LabelKey="LabelTextPost" ToolTipKey="TooltipTextPost" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtNach" runat="server" Width="99%" MaxLength="500" TextMode="MultiLine" Rows="3"></asp:TextBox>
            </div>
            <div class="inputBlockError">
            </div>
        </div>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="Label4" LanguageFile="WidgetForm" LabelKey="LabelUserCopy" ToolTipKey="TooltipUserCopy" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <asp:DropDownList ID="Ddl_UserCopy" runat="server"></asp:DropDownList>
            </div>
            <div class="inputBlockError">
            </div>
        </div>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="Label5" LanguageFile="WidgetForm" LabelKey="LabelSendForm" ToolTipKey="TooltipSendForm" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
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
            <div class="inputBlockError">
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
