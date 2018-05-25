<%@ Page Language="C#" MasterPageFile="~/MasterPages/Empty.master" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.Pages.Popups.PasswordRecover" CodeBehind="PasswordRecover.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cnt1" runat="Server">
    <div id="popup" style="width: 400px;">
        <asp:Panel ID="PnlRecover" runat="server">
            <div class="inputBlock">
                <%=language.GetString("TitlePasswordRecover") %>
            </div>
            <div class="inputBlock">
                <div class="inputBlockLabel">
                    <web:LabelControl ID="LabelEmail" runat="server" LanguageFile="Shared" LabelKey="LabelEmail" TooltipKey="TooltipEmail" />
                </div>
                <div class="inputBlockContent">
                    <asp:TextBox ID="txtEMail" runat="server" Width="220"></asp:TextBox>
                </div>
                <div class="inputBlockError">
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" CssClass="inputErrorTooltip" ControlToValidate="txtEMail" Display="Dynamic" EnableViewState="False" ValidationGroup="PwdRecoverry"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" CssClass="inputErrorTooltip" ControlToValidate="txtEMail" Display="Dynamic" EnableViewState="False" ValidationGroup="PwdRecoverry"></asp:RegularExpressionValidator>
                </div>
            </div>
            <asp:Panel ID="PnlError" runat="server" CssClass="inputBlock" Visible="false">
                <div class="inputBlockContent errorText">
                    <asp:Literal ID="LitError" runat="server" />
                </div>
            </asp:Panel>
            <div class="inputBlock">
                <div class="inputBlockContent">
                    <asp:LinkButton ID="btnSend" CssClass="inputButton" OnClick="OnSendClick" runat="server">
                        <%=languageShared.GetString("CommandSend")%>
                    </asp:LinkButton>
                    <a class="inputButtonSecondary" href="javascript:CloseWindow();">
                        <%= languageShared.GetString("CommandCancel")%></a>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlSent" runat="server" Visible="false">
            <div class="inputBlock">
                <%= language.GetString("MessageAccountInfoSent")%>
            </div>
            <div class="inputBlock">
                <a class="inputButton" href="javascript:CloseWindow();">
                    <%= languageShared.GetString("CommandClose")%></a>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
