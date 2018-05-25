<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CntEdit.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.CntEdit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="PnlEditTrig" runat="server" CssClass="contentEditButton" Visible="false" />
<telerik:RadToolTip ID="RTT" TargetControlID="PnlEditTrig" ShowEvent="OnClick" HideEvent="LeaveToolTip" RenderInPageRoot="true" ShowCallout="false" HideDelay="500" ShowDelay="0" RelativeTo="Element" Position="BottomRight" runat="server" Visible="false">
    <div class="contentEdit">
        <ul>
            <asp:PlaceHolder ID="PhFunc2" runat="server" />
        </ul>
    </div>
</telerik:RadToolTip>
