<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.DataObj.UserControls.Templates.SmallOutputProduct" CodeBehind="SmallOutputProduct.ascx.cs" %>
<div class="CSB_ov_product">
    <div style="float: left; width: <%=WidthColumn%>">
        <div style="float: left; width: <%=WidthImg%>">
            <div class="img">
                <asp:HyperLink ID="LnkImg" runat="server">
                    <asp:Image ID="Img1" runat="server" />
                </asp:HyperLink>
            </div>
        </div>
        <div style="float: right; width: <%=WidthCnt%>">
            <asp:PlaceHolder ID="PhTitle" runat="server" EnableViewState="false">
                <div class="title2">
                    <asp:HyperLink ID="LnkTitle" runat="server" />
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="PhDesc" runat="server" EnableViewState="false">
                <div class="desc">
                    <asp:Literal ID="LitDesc" runat="server" />
                </div>
                <asp:Literal ID="litAbonSavings" runat="server" />
            </asp:PlaceHolder>
            <div class="title">
                <asp:HyperLink ID="LnkMoreInfo" runat="server" Text="Mehr Informationen" NavigateUrl="/" />
            </div>
        </div>
    </div>
    <div style="float: left; width: 100%; text-align: right; padding-top: 10px">
        <asp:PlaceHolder ID="PhPrice1" runat="server" EnableViewState="false">
            <div class="desc">
                <asp:Literal ID="LitPrice1" runat="server" />
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="PhPrice2" runat="server" EnableViewState="false">
            <div class="desc">
                <asp:Literal ID="LitPrice2" runat="server" />
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phOrder" runat="server" EnableViewState="false">
            <div class="desc" style="padding-top: 5px;">
                Bemerkungen zur Bestellung:
            </div>
            <div>
                <asp:TextBox ID="TxtCom" runat="server" TextMode="MultiLine" MaxLength="256" Rows="3" Columns="30" />
            </div>
            <div class="desc" style="float: right; padding-top: 5px;">
                <asp:LinkButton ID="LbtnAdd" runat="server" OnClick="OnBasketAddClick" CssClass="CSB_basket" />
            </div>
        </asp:PlaceHolder>
    </div>
</div>
