<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailsProduct.ascx.cs" Inherits="_4screen.CSB.DataObj.UserControls.Templates.DetailsProduct" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div id="printArea">
    <div style="float: left; width: <%=WidthColumn%>; margin-bottom: 10px;">
        <div style="float: left; width: <%=WidthCnt%>;">
            <asp:Literal ID="LitContent" runat="server" />
        </div>
        <div style="float: right; width: <%=WidthImg%>; text-align: right;">
            <asp:PlaceHolder ID="ProdImg1" runat="server" />
            <div style="float: left; width: 100%; text-align: right; padding-top: 10px;">
                <div style="font-weight: bold; margin-bottom: 4px;">
                    <asp:Literal ID="litSM" runat="server" />
                </div>
                <div style="margin-bottom: 4px;">
                    Normalpreis CHF
                    <asp:Literal ID="litP1" runat="server" />
                </div>
                <asp:Panel ID="PnlSpecPrice" runat="server" Visible="false" Style="margin-bottom: 4px;">
                    Spezialpreis CHF
                    <asp:Literal ID="litP2" runat="server" />
                </asp:Panel>
                <div style="padding-top: 5px; margin-bottom: 4px;">
                    Bemerkungen zur Bestellung:
                </div>
                <div>
                    <asp:TextBox ID="TxtCom" runat="server" TextMode="MultiLine" MaxLength="256" Rows="3" Columns="30" />
                </div>
                <div style="float: right; padding-top: 5px; margin-bottom: 4px;">
                    <asp:LinkButton ID="LbtnAdd" runat="server" OnClick="OnBasketAddClick" CssClass="CSB_basket" />
                </div>
            </div>
        </div>
    </div>
    <div class="clearBoth">
    </div>
    <div>
        <div style="margin-bottom: 10px;">
            <asp:PlaceHolder ID="PhProdImgs" runat="server" />
        </div>
        <div class="clearBoth">
        </div>
        <asp:Literal ID="INFOLIT" runat="server" />
    </div>
</div>
<telerik:RadToolTipManager ID="RTTM" runat="server" Width="300" Height="200" OnAjaxUpdate="OnAjaxUpdate" ShowCallout="False" Position="MiddleRight" RelativeTo="Element" Sticky="True">
</telerik:RadToolTipManager>
<telerik:RadToolTipManager ShowEvent="OnClick" runat="server" ID="RTTMIMG" EnableViewState="false" ShowCallout="false" Position="BottomRight" RelativeTo="Element" Animation="None" ShowDelay="0" Sticky="true" OnAjaxUpdate="OnAjaxUpdate">
</telerik:RadToolTipManager>
