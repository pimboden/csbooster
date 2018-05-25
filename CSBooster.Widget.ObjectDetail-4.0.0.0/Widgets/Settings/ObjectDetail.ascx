<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="ObjectDetail.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.ObjectDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="PnlByUrl" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="Label1" LanguageFile="WidgetObjectDetail" LabelKey="LabelByUrl" TooltipKey="TooltipByUrl" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <asp:CheckBox ID="CbxByUrl" runat="server" OnCheckedChanged="CbxByUrl_CheckedChanged" AutoPostBack="True" />
            </div>
            <div class="inputBlockError">
            </div>
        </asp:Panel>
        <asp:PlaceHolder ID="PhSearch" runat="server">
            <div class="inputBlock">
                <div class="inputBlockLabel">
                    <web:LabelControl ID="LabelControl2" LanguageFile="WidgetObjectDetail" LabelKey="LabelSource" TooltipKey="TooltipSource" runat="server">
                    </web:LabelControl>
                </div>
                <div class="inputBlockContent">
                    <telerik:RadComboBox ID="CbxOrderBy" runat="server">
                    </telerik:RadComboBox>
                </div>
                <div class="inputBlockError">
                </div>
            </div>
            <asp:Panel ID="PnlSearch" runat="server" CssClass="inputBlock">
                <div class="inputBlockLabel">
                    <web:LabelControl ID="Label2" LanguageFile="WidgetObjectDetail" LabelKey="LabelSearch" TooltipKey="TooltipSearch" runat="server">
                    </web:LabelControl>
                </div>
                <div class="inputBlockContent">
                    <asp:UpdatePanel ID="up2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div style="float: left; margin-bottom: 5px; width: 100%">
                                <div style="float: left; margin-left: 4px; width: 25%">
                                    <asp:TextBox ID="TxtSearch" runat="server" Width="99%" />
                                </div>
                                <div style="float: left; margin-left: 4px;">
                                    <web:LabelControl ID="LabelControl1" LanguageFile="WidgetObjectDetail" LabelKey="LabelTagword" TooltipKey="TooltipTagword" runat="server">
                                    </web:LabelControl>
                                </div>
                                <div style="float: left; margin-left: 4px; width: 25%">
                                    <asp:TextBox ID="TxtTagword" runat="server" Width="100%" />
                                </div>
                                <div style="float: left; margin-left: 4px;">
                                    <asp:LinkButton ID="lbtnSR" CssClass="inputButton" runat="server" Text="Suchen" OnClick="lbtnSR_Click"><%=languageShared.GetString("CommandSearch")%></asp:LinkButton>
                                </div>
                            </div>
                            <div style="height: 300px; overflow: auto; float: left; width: 100%">
                                <asp:Repeater ID="OBJOVW" runat="server" OnItemDataBound="OBJOVW_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:PlaceHolder ID='PhItem' runat="server" EnableViewState="false" />
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="clearBoth">
                                </div>
                            </div>
                            <asp:HiddenField ID="TxtSelected" runat="server"></asp:HiddenField>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="inputBlockError">
                </div>
            </asp:Panel>
        </asp:PlaceHolder>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LabelShowPager" LanguageFile="WidgetObjectDetail" LabelKey="LabelShowPager" TooltipKey="TooltipShowPager" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <asp:CheckBox ID="CbShowPager" runat="server" />
            </div>
            <div class="inputBlockError">
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
