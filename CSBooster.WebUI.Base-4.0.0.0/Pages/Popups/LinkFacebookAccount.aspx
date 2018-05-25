<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Empty.Master" CodeBehind="LinkFacebookAccount.aspx.cs" Inherits="_4screen.CSB.WebUI.Pages.Popups.LinkFacebookAccount" %>

<asp:Content ID="Cnt" runat="server" ContentPlaceHolderID="Cnt1">
    <div style="width: 440px; float: left; padding: 20px; background-color: #FFFFFF;">
        <div class="inputBlock">
            <div>
                <web:TextControl ID="TextLinkFacebookAccount" runat="server" AllowHtml="true" LanguageFile="Pages.Popups.WebUI.Base" TextKey="TextLinkFacebookAccount" />
            </div>
        </div>
        <asp:UpdatePanel ID="UpnlLogin" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="inputBlock3">
                    <div class="inputBlockLabel">
                        <web:LabelControl ID="LabelUsername" LanguageFile="Pages.Popups.WebUI.Base" LabelKey="LabelUsername" TooltipKey="TooltipUsername" runat="server" />
                    </div>
                    <div class="inputBlockContent">
                        <asp:TextBox ID="TxtLogin" runat="server" OnTextChanged="ValidateLogin" AutoPostBack="True" MaxLength="16" Width="99%" />
                    </div>
                    <div class="inputBlockError">
                        <asp:RequiredFieldValidator ID="RfvLogin" runat="server" ValidationGroup="Login" ControlToValidate="TxtLogin" Display="Dynamic" EnableViewState="False" SetFocusOnError="true" CssClass="inputErrorTooltip"><%=language.GetString("MessageUsernameNeeded")%></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RevLogin" runat="server" ValidationGroup="Login" ValidationExpression="^[0-9A-Za-z_-]{3,32}$" ControlToValidate="TxtLogin" Display="Dynamic" EnableViewState="False" SetFocusOnError="true" CssClass="inputErrorTooltip"><%=language.GetString("MessageUsernameWrongChar")%></asp:RegularExpressionValidator>
                        <asp:CustomValidator ID="CvLogin" runat="server" ValidationGroup="Login" ControlToValidate="TxtLogin" OnServerValidate="ValidateLogin" SetFocusOnError="true" CssClass="inputErrorTooltip" Display="Dynamic"><%=language.GetString("MessageUsernameExists")%></asp:CustomValidator>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="inputBlock">
            <div class="inputBlockContent">
                <asp:LinkButton ID="LbtnCreate" runat="server" CssClass="inputButton" OnClick="OnCreateClick">
                    <web:TextControl ID="CommandCreateProfile" runat="server" LanguageFile="Shared" TextKey="CommandCreateProfile" />
                </asp:LinkButton>
                <a href="javascript:void(0)" onclick="closeRadWindow()" class="inputButtonSecondary">
                    <web:TextControl ID="CommandCancel" runat="server" LanguageFile="Shared" TextKey="CommandCancel" />
                </a>
            </div>
        </div>
    </div>
</asp:Content>
