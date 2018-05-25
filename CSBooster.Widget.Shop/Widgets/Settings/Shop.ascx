<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="Shop.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.Shop" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        Warenkorbanzeige
    </div>
    <div class="CSB_input_cnt">
        <telerik:RadComboBox ID="rcbBasketType" runat="server" Skin="Custom" EnableEmbeddedSkins="false" AutoPostBack="true" Width="100%">
            <Items>
                <telerik:RadComboBoxItem Text="Klein" Value="Small" Selected="true" />
                <telerik:RadComboBoxItem Text="Komplett" Value="Complete" />
            </Items>
        </telerik:RadComboBox>
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
