<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.DetailsLocation" CodeBehind="DetailsLocation.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="locationDetail">
    <div class="locationDetailBlock">
        <div class="locationDetailInfo">
            <div class="locationDetailBlock">
                <div class="locationDetailLabel">
                    Location
                </div>
                <div class="locationDetailContent">
                    <asp:Literal ID="LitAddress" runat="server" />
                </div>
            </div>
            <div class="locationDetailBlock">
                <div class="locationDetailLabel">
                    Art
                </div>
                <div class="locationDetailContent">
                    <asp:Literal ID="LitType" runat="server" />
                </div>
            </div>
            <asp:Panel ID="TrWebsite" runat="server" CssClass="locationDetailBlock" Visible="false">
                <div class="locationDetailLabel">
                    Webseite
                </div>
                <div class="locationDetailContent">
                    <asp:HyperLink ID="LnkWebsite" runat="server" />
                </div>
            </asp:Panel>
            <asp:Panel ID="TrDesc" runat="server" CssClass="locationDetailBlock" Visible="false">
                <div class="locationDetailLabel">
                    Beschreibung
                </div>
                <div class="locationDetailContent">
                    <asp:Literal ID="LitDesc" runat="server" />
                </div>
            </asp:Panel>
        </div>
        <div class="locationDetailImage">
            <asp:Image ID="Img" runat="server" EnableViewState="false" />
        </div>
    </div>
    <div class="locationAddContent">
        <div class="locationAddContentIntro">
            <web:TextControl ID="TextLocationAddPicturesAndVideos" runat="server" LanguageFile="UserControls.Templates.WebUI.Base" TextKey="TextLocationAddPicturesAndVideos" AllowHtml="true" />
        </div>
        <asp:HyperLink ID="LnkRelCnt" runat="server" CssClass="inputButton">
            <web:TextControl ID="CommandAddPicturesAndVideos" runat="server" LanguageFile="UserControls.Templates.WebUI.Base" TextKey="CommandAddPicturesAndVideos" />
        </asp:HyperLink>
    </div>
</div>
<telerik:RadToolTipManager ID="RTTM" runat="server" Width="300" Height="200" OnAjaxUpdate="OnAjaxUpdate" ShowCallout="False" Sticky="True">
</telerik:RadToolTipManager>
