<%@ Page Language="C#" MasterPageFile="~/MasterPages/Empty.master" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.Pages.Popups.ForumTopicItemComment" CodeBehind="ForumTopicItemComment.aspx.cs" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Cnt1" runat="Server">
    <div id="popup" style="width: 540px;">
        <asp:Panel ID="PnlInputAnonymous" runat="server" Visible="true">
            <div class="inputBlock">
                <div class="inputBlockLabel">
                    <web:LabelControl ID="LabelName" LanguageFile="WidgetComments" LabelKey="LabelName" TooltipKey="TooltipName" runat="server" />
                </div>
                <div class="inputBlockContent">
                    <div>
                        <asp:TextBox ID="TxtName" runat="server" Width="200" MaxLength="32" />
                    </div>
                </div>
                <div class="inputBlockError">
                    <asp:RequiredFieldValidator ID="RfvName" runat="server" ValidationGroup="Comments" ControlToValidate="TxtName" Display="Dynamic" EnableViewState="False" SetFocusOnError="true" CssClass="inputErrorTooltip" />
                    <asp:RegularExpressionValidator ID="RevName" runat="server" ValidationGroup="Comments" ControlToValidate="TxtName" Display="Dynamic" EnableViewState="False" SetFocusOnError="true" CssClass="inputErrorTooltip" />
                </div>
            </div>
            <div class="inputBlock3">
                <div class="inputBlockLabel">
                    <web:LabelControl ID="LabelEmail" LanguageFile="WidgetComments" LabelKey="LabelEmail" TooltipKey="TooltipEmail" runat="server" />
                </div>
                <div class="inputBlockContent">
                    <div>
                        <asp:TextBox ID="TxtEmail" runat="server" Width="200" MaxLength="50" />
                    </div>
                </div>
                <div class="inputBlockError">
                    <asp:RequiredFieldValidator ID="RfvEmail" runat="server" ValidationGroup="Comments" ControlToValidate="TxtEmail" Display="Dynamic" EnableViewState="False" SetFocusOnError="true" CssClass="inputErrorTooltip" EnableClientScript="true" />
                    <asp:RegularExpressionValidator ID="RevEmail" runat="server" ValidationGroup="Comments" ControlToValidate="TxtEmail" Display="Dynamic" EnableViewState="False" SetFocusOnError="true" EnableClientScript="true" CssClass="inputErrorTooltip" />
                </div>
            </div>
        </asp:Panel>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <asp:Label ID="COMTIT" runat="server" EnableViewState="false"><%=language.GetString("LableComment")%></asp:Label>
            </div>
            <div class="inputBlockContent">
                <telerik:RadEditor ID="TxtCom" runat="server" Width="99%" Height="300px" ToolsFile="~/Configurations/RadEditorToolsFile1.config" Language="de-CH" EditModes="Design" StripFormattingOptions="All" />
                <label class="hidden" for="codeBox">
                    <web:TextControl ID="TextDontFillBelow" runat="server" LanguageFile="Pages.Popups.WebUI.Base" TextKey="TextDontFillBelow" />
                </label>
                <input type="text" class="hidden" id="codeBox" name="codeBox">
            </div>
            <div class="inputBlockError">
                <asp:RequiredFieldValidator ID="RevCom" runat="server" ValidationGroup="Comments" ControlToValidate="TxtCom" Display="Dynamic" EnableViewState="False" SetFocusOnError="true" CssClass="inputErrorTooltip" EnableClientScript="true" />
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
        <div class="inputBlock2">
            <div class="inputBlockLabel">
            </div>
            <div class="inputBlockContent">
                <asp:LinkButton ID="OKBTN" CssClass="inputButton" OnClick="OnSaveClick" Text="Speichern" runat="server"><%=languageShared.GetString("CommandSave")%></asp:LinkButton>
                <a class="inputButtonSecondary" href="javascript:CloseWindow();">
                    <%= languageShared.GetString("CommandCancel")%></a>
            </div>
        </div>
    </div>
</asp:Content>
