<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Wizards.Tagging" CodeBehind="Tagging.ascx.cs" %>
<%@ Register Src="~/UserControls/ObjectViewHandler.ascx" TagName="ObjectViewHandler" TagPrefix="csb" %>
<%@ Register Src="~/UserControls/Copyrights.ascx" TagName="Copyrights" TagPrefix="csb" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:PlaceHolder ID="PhView" runat="server" />
<asp:Panel ID="PnlCtyGroups" runat="server" Visible="false">
    <fieldset>
        <legend>
            <web:TextControl ID="TextTitleCtyGroups" runat="server" LanguageFile="UserControls.Wizards.WebUI.Base" TextKey="TitleCtyGroups" />
        </legend>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LabelCtyGroups" runat="server" LanguageFile="UserControls.Wizards.WebUI.Base" LabelKey="LabelCtyGroups" TooltipKey="TooltipCtyGroups" />
            </div>
            <div class="inputBlockContent">
                <telerik:RadComboBox ID="RCBCtyGroups" ZIndex="90000" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem Value="0" />
                        <telerik:RadComboBoxItem Value="1" />
                    </Items>
                </telerik:RadComboBox>
            </div>
        </div>
    </fieldset>
</asp:Panel>
<asp:Panel ID="PnlCtyUpload" runat="server" Visible="false">
    <fieldset>
        <legend>
            <web:TextControl ID="TextTitleCtyUpload" runat="server" LanguageFile="UserControls.Wizards.WebUI.Base" TextKey="TitleCtyUpload" />
        </legend>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LabelCtyUpload" runat="server" LanguageFile="UserControls.Wizards.WebUI.Base" LabelKey="LabelCtyUpload" TooltipKey="TooltipCtyUpload" />
            </div>
            <div class="inputBlockContent">
                <telerik:RadComboBox ID="RCBCtyUpload" ZIndex="90000" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem Value="0" />
                        <telerik:RadComboBoxItem Value="1" />
                    </Items>
                </telerik:RadComboBox>
            </div>
        </div>
    </fieldset>
</asp:Panel>
<csb:Copyrights ID="Copyright" runat="server" />
<asp:PlaceHolder ID="PhTagging" runat="server" />
<asp:Panel ID="PnlGeoTagging" runat="server">
    <fieldset>
        <legend>
            <web:TextControl ID="TextTitleGeotagging" runat="server" LanguageFile="UserControls.Wizards.WebUI.Base" TextKey="TitleGeotagging" />
        </legend>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LabelGeotagging" runat="server" LanguageFile="UserControls.Wizards.WebUI.Base" LabelKey="LabelGeotagging" TooltipKey="TooltipGeotagging" />
            </div>
            <div class="inputBlockContent">
                <asp:HyperLink ID="LnkOpenMap" NavigateUrl="javascript:void(0)" CssClass="inputButton" Style="float: left;" runat="server">
                    <web:TextControl ID="TextCommandOpenPlaceOnMap" runat="server" LanguageFile="Shared" TextKey="CommandOpenPlaceOnMap" />
                </asp:HyperLink>
                <div style="float: left; margin-left: 10px; width: 200px;">
                    <%=languageShared.GetString("LabelLongitude")%>
                    <asp:TextBox ID="TxtGeoLong" Width="140" runat="server" />
                </div>
                <div style="float: left; margin-left: 10px; width: 200px;">
                    <%=languageShared.GetString("LabelLatitude")%>
                    <asp:TextBox ID="TxtGeoLat" Width="140" runat="server" />
                </div>
                <asp:HiddenField ID="HFZip" runat="server" />
                <asp:HiddenField ID="HFCity" runat="server" />
                <asp:HiddenField ID="HFStreet" runat="server" />
                <asp:HiddenField ID="HFCountry" runat="server" />
            </div>
        </div>
    </fieldset>
</asp:Panel>
