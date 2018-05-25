<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.CustomizationBarTabWidgets" CodeBehind="CustomizationBarTabWidgets.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="CSBooster.CustomDragDrop" Namespace="CustomDragDrop" TagPrefix="cdd" %>
<div class="inputBlock">
    <%=language.GetString("LableWidgetTitle")%>
</div>
<div class="inputBlock">
    <asp:CheckBoxList ID="CblWidgets" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnWidgetFilterChanged" RepeatLayout="Flow" RepeatDirection="Horizontal" CssClass="cBarWidgetFilter" />
</div>
<div class="inputBlock">
    <div class="cBarWidgetList">
        <asp:Repeater ID="WG" OnItemDataBound="OnWidgetItemDataBound" runat="server">
            <ItemTemplate>
                <asp:Panel ID="WT" CssClass="cBarWidget" runat="server">
                    <asp:Panel ID="WTH" CssClass="cBarWidgetDragHandle" runat="server">
                        <div class="cBarWidgetTitle">
                            <asp:Literal ID="LitTitle" runat="server" />
                        </div>
                        <div class="cBarWidgetDescription">
                            <asp:Literal ID="LitDesc" runat="server" />
                        </div>
                    </asp:Panel>
                </asp:Panel>
                <cdd:CustomFloatingBehaviorExtender ID="WTFB" DragHandleID='WTH' TargetControlID='WT' runat="server" />
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
<div class="inputBlock">
    <asp:LinkButton ID="LbtnClose" runat="server" CssClass="inputButtonSecondary" OnClick="OnCloseClick"><%=languageShared.GetString("CommandClose")%></asp:LinkButton>
</div>
