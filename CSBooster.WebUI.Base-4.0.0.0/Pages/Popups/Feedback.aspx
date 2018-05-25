<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Empty.Master" CodeBehind="Feedback.aspx.cs" Inherits="_4screen.CSB.WebUI.Pages.Popups.Feedback" %>

<asp:Content ID="Cnt" runat="server" ContentPlaceHolderID="Cnt1">
    <div id="popup" style="width: 500px;">
        <div class="inputBlock">
            <div>
                <web:TextControl ID="TextSendFeedback" runat="server" AllowHtml="true" LanguageFile="Pages.Popups.WebUI.Base" TextKey="TextSendFeedback" />
            </div>
        </div>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LabelEmail" runat="server" LanguageFile="Shared" LabelKey="LabelEmail" TooltipKey="TooltipEmail" />
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtEmail" runat="server" Width="100%" />
            </div>
            <div class="inputBlockError">
                <asp:RequiredFieldValidator ID="RfvEmail" runat="server" ValidationGroup="Email" ControlToValidate="TxtEmail" Display="Dynamic" SetFocusOnError="true" CssClass="inputErrorTooltip">
                    <web:TextControl ID="TextMessageValidEmail" runat="server" LanguageFile="Pages.Popups.WebUI.Base" TextKey="MessageValidEmail" />
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RevEmail" runat="server" ValidationGroup="Email" ControlToValidate="TxtEmail" Display="Dynamic" SetFocusOnError="true" CssClass="inputErrorTooltip">
                    <web:TextControl ID="TextMessageValidEmail2" runat="server" LanguageFile="Pages.Popups.WebUI.Base" TextKey="MessageValidEmail" />
                </asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LabelName" runat="server" LanguageFile="Pages.Popups.WebUI.Base" LabelKey="LabelName" TooltipKey="TooltipName" />
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtName" runat="server" Width="100%" />
            </div>
        </div>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LabelFeedback" runat="server" LanguageFile="Pages.Popups.WebUI.Base" LabelKey="LabelFeedback" TooltipKey="TooltipFeedback" />
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtFeedback" TextMode="MultiLine" runat="server" Width="100%" Height="200" />
                <label class="hidden" for="codeBox">
                    <web:TextControl ID="TextDontFillBelow" runat="server" LanguageFile="Pages.Popups.WebUI.Base" TextKey="TextDontFillBelow" />
                </label>
                <input type="text" class="hidden" id="codeBox" name="codeBox">
            </div>
            <div class="inputBlockError">
                <asp:RequiredFieldValidator ID="RfvFeedback" runat="server" ValidationGroup="Feedback" ControlToValidate="TxtFeedback" Display="Dynamic" SetFocusOnError="true" CssClass="inputErrorTooltip">
                    <web:TextControl ID="TextMessageFeedbackRequired" runat="server" LanguageFile="Pages.Popups.WebUI.Base" TextKey="MessageFeedbackRequired" />
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LabelFeedbackAttachment" runat="server" LanguageFile="Pages.Popups.WebUI.Base" LabelKey="LabelFeedbackAttachment" TooltipKey="TooltipFeedbackAttachment" />
            </div>
            <div class="inputBlockContent">
                <asp:FileUpload ID="FuAttachment" runat="server" Width="98%" />
            </div>
        </div>
        <asp:Panel ID="PnlStatus" runat="server" CssClass="inputBlock" Visible="false">
            <div class="inputBlockContent errorText">
                <asp:Literal ID="LitStatus" runat="server" />
            </div>
        </asp:Panel>
        <div class="inputBlock">
            <div class="inputBlockContent">
                <asp:LinkButton ID="LbtnSend" runat="server" CssClass="inputButton" OnClick="OnSendClick">
                    <web:TextControl ID="CommandSend" runat="server" LanguageFile="Shared" TextKey="CommandSend" />
                </asp:LinkButton>
                <a href="javascript:void(0)" onclick="CloseWindow()" class="inputButtonSecondary">
                    <web:TextControl ID="CommandCancel" runat="server" LanguageFile="Shared" TextKey="CommandCancel" />
                </a>
            </div>
        </div>
    </div>
</asp:Content>
