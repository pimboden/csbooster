<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.Registration" CodeBehind="Registration.ascx.cs" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<asp:Panel ID="PnlReg" runat="server">
    <asp:UpdatePanel ID="UpnlEmail" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="inputBlock3">
                <div class="inputBlockLabel">
                    <web:LabelControl ID="LabelEmailAddress" LanguageFile="Pages.Popups.WebUI.Base" LabelKey="LabelEmailAddress" TooltipKey="TooltipEmailAddress" runat="server" />
                </div>
                <div class="inputBlockContent">
                    <asp:TextBox ID="TxtEmail" runat="server" OnTextChanged="ValidateEmail" AutoPostBack="True" MaxLength="50" Width="99%" />
                </div>
                <div class="inputBlockError">
                    <asp:RequiredFieldValidator ID="RfvEmail" runat="server" ValidationGroup="Email" ControlToValidate="TxtEmail" Display="Dynamic" EnableViewState="False" SetFocusOnError="true" CssClass="inputErrorTooltip"><%= language.GetString("MessageValidEmail")%></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RevEmail" runat="server" ValidationGroup="Email" ControlToValidate="TxtEmail" Display="Dynamic" EnableViewState="False" SetFocusOnError="true" CssClass="inputErrorTooltip"><%=language.GetString("MessageValidEmail")%></asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="CvEmail" runat="server" ValidationGroup="Email" ControlToValidate="TxtEmail" OnServerValidate="ValidateEmail" SetFocusOnError="true" CssClass="inputErrorTooltip" Display="Dynamic"><%=language.GetString("TooltipEmailExists")%></asp:CustomValidator>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
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
    <div class="inputBlock3">
        <div class="inputBlockLabel">
            <web:LabelControl ID="LabelPassword" LanguageFile="Pages.Popups.WebUI.Base" LabelKey="LabelPassword" TooltipKey="TooltipPassword" runat="server" />
        </div>
        <div class="inputBlockContent">
            <asp:TextBox ID="TxtPW" runat="server" TextMode="Password" Width="99%" />
        </div>
        <div class="inputBlockError">
            <asp:RequiredFieldValidator ID="RfvPW" runat="server" ValidationGroup="Password" ControlToValidate="TxtPW" SetFocusOnError="true" CssClass="inputErrorTooltip"><%=language.GetString("MessagePwdNeeded")%></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="inputBlock3">
        <div class="inputBlockLabel">
            <web:LabelControl ID="LabelAGB" LanguageFile="Pages.Popups.WebUI.Base" LabelKey="LabelAGB" TooltipKey="TooltipAcceptAGB" runat="server" />
        </div>
        <div class="inputBlockContent">
            <asp:CheckBox ID="CbxAGB" runat="server" /> <a href="javascript:radWinOpen('/Pages/Popups/HtmlContent.aspx?OID=6f1cf79f-1f2b-4aab-889e-00114fefd1d1', '<%=_4screen.Utils.Web.GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base").GetString("TitleTermsAndConditions") %>', 640, 400, false, null, 'htmlContent')">
                <web:TextControl ID="CommandOpenTermsAndConditions" runat="server" LanguageFile="Pages.Popups.WebUI.Base" TextKey="CommandOpenTermsAndConditions" />
            </a>
            <label class="hidden" for="codeBox">
                <web:TextControl ID="TextDontFillBelow" runat="server" LanguageFile="Pages.Popups.WebUI.Base" TextKey="TextDontFillBelow" />
            </label>
            <input type="text" class="hidden" id="codeBox" name="codeBox">
        </div>
        <div class="inputBlockError">
            <asp:CustomValidator ID="CvAGB" runat="server" ValidationGroup="AGB" OnServerValidate="ValidateAGB" SetFocusOnError="true" CssClass="inputErrorTooltip" Display="Dynamic"><%=language.GetString("MessageAcceptAGB")%></asp:CustomValidator>
        </div>
    </div>
    <asp:Panel ID="PnlError" runat="server" CssClass="inputBlock" Visible="false">
        <div class="inputBlockLabel">
            &nbsp;
        </div>
        <div class="inputBlockContent errorText">
            <asp:Literal ID="LitError" runat="server" EnableViewState="False" />
        </div>
    </asp:Panel>
    <div class="inputBlock">
        <div class="inputBlockLabel">
            &nbsp;
        </div>
        <div class="inputBlockContent">
            <asp:LinkButton ID="LbtnSave" runat="server" CssClass="inputButton" OnClick="OnSaveClick">
                <web:TextControl ID="CommandRegister" runat="server" LanguageFile="Pages.Popups.WebUI.Base" TextKey="CommandRegister" />
            </asp:LinkButton>
        </div>
    </div>
</asp:Panel>
<asp:Panel ID="PnlMsg" runat="server" Visible="false">
    <web:TextControl ID="MessageClosedUser" runat="server" LanguageFile="Sahred" TextKey="MessageClosedUser" />
</asp:Panel>
<asp:Panel ID="PnlRegistered" runat="server" Visible="false">
    <div style="margin-bottom: 10px;">
        <web:TextControl ID="MessageRegistrationSucceded" runat="server" LanguageFile="Pages.Popups.WebUI.Base" TextKey="MessageRegistrationSucceded" />
    </div>
</asp:Panel>
<div class="clearBoth">
</div>
