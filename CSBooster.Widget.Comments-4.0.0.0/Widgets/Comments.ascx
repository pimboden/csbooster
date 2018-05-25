<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Comments.ascx.cs" Inherits="_4screen.CSB.Widget.Comments" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="web" Namespace="_4screen.WebControls" Assembly="4screen.WebControls" %>
<div class="commentsForm">
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlInput" runat="server" Visible="false" CssClass="commentInputArea">
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
                        <web:LabelControl ID="LabelComment" LanguageFile="WidgetComments" LabelKey="LabelComment" TooltipKey="TooltipComment" runat="server" />
                    </div>
                    <div class="inputBlockContent">
                        <telerik:RadEditor ID="TxtCom" runat="server" Width="99%" Height="180px" ToolsFile="~/Configurations/RadEditorToolsFile1.config" Language="de-CH" EditModes="Design" StripFormattingOptions="All" />
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
                        <asp:LinkButton ID="lbtnSave" runat="server" CssClass="inputButton" OnClick="OnSaveClick">
                            <web:TextControl ID="CommandAdd" LanguageFile="Shared" TextKey="CommandAdd" runat="server" />
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="clearBoth">
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlInput2" runat="server" CssClass="commentToggleButton">
                <asp:LinkButton ID="lbtnAddComment" runat="server" ValidationGroup="Comments" CssClass="inputButton" OnClick="OnAddCommentClick">
                    <web:TextControl ID="showinput" LanguageFile="WidgetComments" TextKey="showinput" runat="server" />
                </asp:LinkButton>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<div class="comments">
    <asp:UpdatePanel ID="up2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:PlaceHolder ID="phResult" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
