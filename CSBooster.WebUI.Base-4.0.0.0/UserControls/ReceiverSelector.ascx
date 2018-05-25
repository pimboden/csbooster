<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReceiverSelector.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.ReceiverSelector" %>
<div class="inputBlock">
    <div id="receiverList">
        <div id="receiverTextBox">
            <asp:TextBox ID="txtUser" runat="server" />
        </div>
    </div>
    <div id="receiverSuggestions">
        <asp:UpdatePanel ID="upnl" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnUser" runat="server" OnClick="OnTextEnter" CssClass="hidden" />
                <asp:Panel ID="pnlSuggest" runat="server" CssClass="receiverSuggestion" Visible="false">
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
