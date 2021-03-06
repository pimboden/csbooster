﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormAdvSend.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.FormAdvSend" %>
<asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                Betreff
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtSubject" runat="server" MaxLength="100" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RFVSubject" runat="server" ErrorMessage="Pflichtfeld" ControlToValidate="TxtSubject" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            <div class="inputBlockError">
            </div>
        </div>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                Text vor Inhalt
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtVor" runat="server" Width="99%" MaxLength="500" TextMode="MultiLine" Rows="3"></asp:TextBox>
            </div>
            <div class="inputBlockError">
            </div>
        </div>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                Text nach Inhalt
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtNach" runat="server" Width="99%" MaxLength="500" TextMode="MultiLine" Rows="3"></asp:TextBox>
            </div>
            <div class="inputBlockError">
            </div>
        </div>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                Kopie an Benutzer
            </div>
            <div class="inputBlockContent">
                <asp:DropDownList ID="DdlUserCopy" runat="server">
                    <asp:ListItem Text="Nein" Value="0" />
                    <asp:ListItem Text="als Email" Value="1" />
                    <asp:ListItem Text="als interne Mitteilung" Value="2" />
                </asp:DropDownList>
            </div>
            <div class="inputBlockError">
            </div>
        </div>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                Formular senden an
            </div>
            <div class="inputBlockContent">
                <asp:RadioButtonList ID="RblSendAs" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="RblSendAs_SelectedIndexChanged">
                    <asp:ListItem Text="als Email" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="als interne Mitteilung" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
                <br />
                <asp:Literal ID="LitUser" runat="server">User: </asp:Literal><asp:DropDownList ID="DdlUser" runat="server">
                </asp:DropDownList>
                <br />
                <asp:Literal ID="LitNickname" runat="server">Nickname: </asp:Literal><asp:TextBox ID="TxtNickname" runat="server" Width="20%" MaxLength="100"></asp:TextBox>
                <asp:Literal ID="LitOr" runat="server"> und / oder </asp:Literal>
                <asp:Literal ID="LitEmail" runat="server">E-Mail: </asp:Literal><asp:TextBox ID="TxtEmail" runat="server" Width="20%" MaxLength="150"></asp:TextBox>
                <asp:Literal ID="LitMsg" runat="server" Visible="false">Nickname ist ungültig!</asp:Literal>
            </div>
            <div class="inputBlockError">
            </div>
        </div>
</ContentTemplate>
</asp:UpdatePanel>
