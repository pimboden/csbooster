<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailsSurvey.ascx.cs" Inherits="_4screen.CSB.DataObj.UserControls.Templates.DetailsSurvey" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Panel ID="pnlOut" runat="server">
    <asp:Literal ID="litHeader" runat="server" />
    <telerik:RadMultiPage runat="server" ID="radMP">
    </telerik:RadMultiPage>
    <asp:PlaceHolder ID="pager" runat="server" />
    <asp:Panel ID="pnlFormular" runat="server" Visible="false">
        <div style="width: 350px">
            <div class="inputBlockLabel">
                Anrede:
            </div>
            <div class="inputBlockContent">
                <asp:DropDownList ID="Sex" runat="server" Width="99%">
                    <asp:ListItem Text="Frau" Value="1" />
                    <asp:ListItem Text="Herr" Value="0" />
                </asp:DropDownList>
            </div>
            <div class="clearBoth">
            </div>
            <div class="inputBlockLabel">
                Name<sup>*</sup>:
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="Name" runat="server" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RfvName" runat="server" ControlToValidate="Name" Display="Dynamic" Text="Pflichtfeld"></asp:RequiredFieldValidator>
            </div>
            <div class="clearBoth">
            </div>
            <div class="inputBlockLabel">
                Vorname<sup>*</sup>:
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="Vorname" runat="server" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RfvVorname" runat="server" ControlToValidate="Vorname" Display="Dynamic" Text="Pflichtfeld"></asp:RequiredFieldValidator>
            </div>
            <div class="clearBoth">
            </div>
            <div class="inputBlockLabel">
                Firma:
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="Firma" runat="server" Width="99%"></asp:TextBox>
            </div>
            <div class="clearBoth">
            </div>
            <div class="inputBlockLabel">
                Strasse<sup>*</sup>:
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="AddressStreet" runat="server" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RfvAddressStreet" runat="server" ControlToValidate="AddressStreet" Display="Dynamic" Text="Pflichtfeld"></asp:RequiredFieldValidator>
            </div>
            <div class="clearBoth">
            </div>
            <div class="inputBlockLabel">
                PLZ / Ort<sup>*</sup>:
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="AddressZip" MaxLength="10" runat="server" Width="20%"></asp:TextBox>&nbsp;&nbsp;<asp:TextBox ID="AddressCity" runat="server" Width="54%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RfvAddressZip" runat="server" ControlToValidate="AddressZip" Display="Dynamic" Text="Pflichtfeld"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RfvAddressCity" runat="server" ControlToValidate="AddressCity" Display="Dynamic" Text="Pflichtfeld"></asp:RequiredFieldValidator>
            </div>
            <div class="clearBoth">
            </div>
            <div class="inputBlockLabel">
                E-Mail<sup>*</sup>:
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="EMail" runat="server" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RfvEMail" runat="server" ControlToValidate="EMail" Display="Dynamic" Text="Pflichtfeld"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RevEMail" runat="server" ControlToValidate="EMail" Display="Dynamic" Text="Format ungültig"></asp:RegularExpressionValidator>
            </div>
            <div class="clearBoth">
            </div>
            <div class="inputBlockLabel">
                Telefon:
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="Phone" runat="server" Width="99%"></asp:TextBox>
            </div>
            <div class="clearBoth">
            </div>
            <div class="inputBlockLabel">
                Bemerkungen:
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="Comment" runat="server" MaxLength="500" TextMode="MultiLine" Rows="4" Width="99%"></asp:TextBox>
                <asp:HiddenField ID="hidQA" runat="server" />
                <asp:HiddenField ID="hidResult" runat="server" />
                <asp:HiddenField ID="hidSURID" runat="server" />
                <asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click" Text="Senden" />
            </div>
            <div class="clearBoth">
            </div>
        </div>
    </asp:Panel>
    <div class="clearBoth">
    </div>
    <asp:Literal ID="litFooter" runat="server" />
</asp:Panel>
<asp:HiddenField runat="server" ID="hdCurrPage" Value="0" />
<telerik:RadToolTipManager ID="RTTM" runat="server" Width="300" Height="200" OnAjaxUpdate="OnAjaxUpdate" ShowCallout="False" Position="MiddleRight" RelativeTo="Element" Sticky="True">
</telerik:RadToolTipManager>
