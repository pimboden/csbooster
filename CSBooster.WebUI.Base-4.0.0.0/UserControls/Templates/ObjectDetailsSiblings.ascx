<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.ObjectDetailsSiblings" CodeBehind="ObjectDetailsSiblings.ascx.cs" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="csb" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<div>
    <asp:UpdatePanel ID="SIBUP" runat="server" UpdateMode="Conditional" EnableViewState="false">
        <ContentTemplate>
            <asp:HiddenField ID="PBPageNum" runat="server" />
            <asp:Panel ID="SIBPAN" CssClass="CSB_det_scroll" runat="server">
                <asp:DataList ID="DLObjects" runat="server" OnItemDataBound="OnObjectsItemDataBound" RepeatDirection="Horizontal" RepeatLayout="Table" CellPadding="0" Width="100%" EnableViewState="False" ShowFooter="False" ShowHeader="False">
                    <ItemTemplate>
                        <asp:PlaceHolder ID='PhItem' runat="server" EnableViewState="false" />
                    </ItemTemplate>
                </asp:DataList>
                <div class="clearBoth">
                </div>
                <csb:Pager ID="Pager" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UPPROG" AssociatedUpdatePanelID="SIBUP" runat="server">
        <ProgressTemplate>
            <div class="updateProgress">
                <%=GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUpdateProgress")%>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
