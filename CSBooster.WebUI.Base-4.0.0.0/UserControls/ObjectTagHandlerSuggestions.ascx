<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.ObjectTagHandlerSuggestions" CodeBehind="ObjectTagHandlerSuggestions.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<fieldset>
    <legend>
        <web:TextControl ID="TextTitleTagging" runat="server" LanguageFile="UserControls.WebUI.Base" TextKey="TitleTagging" />
    </legend>
    <div class="inputBlock">
        <div class="inputBlockLabel">
            <web:LabelControl ID="LabelTagSuggestions" runat="server" LanguageFile="UserControls.WebUI.Base" LabelKey="LabelTagSuggestions" TooltipKey="TooltipTagSuggestions" />
        </div>
        <div class="inputBlockContent">
            <div id="tagHintsList">
                <asp:Literal ID="litTags" runat="server" />
                <div id="tagHintsTextBox">
                    <asp:TextBox ID="txtTag" runat="server" />
                </div>
            </div>
            <div id="tagHints">
                <asp:UpdatePanel ID="upnl" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="btnTag" runat="server" OnClick="OnTextEnter" CssClass="hidden" />
                        <asp:Panel ID="pnlSuggest" runat="server" CssClass="tagHints" Visible="false">
                            <asp:Repeater ID="repSuggest" runat="server" OnItemDataBound="OnSuggestionItemDataBound">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkSuggest" runat="server" NavigateUrl="javascript:void(0)" />
                                </ItemTemplate>
                            </asp:Repeater>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</fieldset>
