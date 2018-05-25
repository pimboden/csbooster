<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.DetailsEvent" CodeBehind="DetailsEvent.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="eventDetail">
    <div class="eventDetailBlock">
        <div class="eventDetailInfo">
            <div class="eventDetailBlock">
                <div class="eventDetailLabel">
                    Wo
                </div>
                <div class="eventDetailContent">
                    <asp:HyperLink ID="LnkLocation" runat="server" />
                    <br />
                    <asp:Literal ID="LitAddress" runat="server" />
                </div>
            </div>
            <div class="eventDetailBlock">
                <div class="eventDetailLabel">
                    Wann
                </div>
                <div class="eventDetailContent">
                    <asp:Literal ID="LitWhen" runat="server" />
                </div>
            </div>
            <div class="eventDetailBlock">
                <div class="eventDetailLabel">
                    Art
                </div>
                <div class="eventDetailContent">
                    <asp:Literal ID="LitType" runat="server" />
                </div>
            </div>
            <asp:Panel ID="TrWebsite" runat="server" CssClass="eventDetailBlock" Visible="false">
                <div class="eventDetailLabel">
                    Webseite
                </div>
                <div class="eventDetailContent">
                    <asp:HyperLink ID="LnkWebsite" runat="server" />
                </div>
            </asp:Panel>
            <asp:Panel ID="TrAge" runat="server" CssClass="eventDetailBlock" Visible="false">
                <div class="eventDetailLabel">
                    Alter
                </div>
                <div class="eventDetailContent">
                    <asp:Literal ID="LitAge" runat="server" />
                </div>
            </asp:Panel>
            <asp:Panel ID="TrPrice" runat="server" CssClass="eventDetailBlock" Visible="false">
                <div class="eventDetailLabel">
                    Preis
                </div>
                <div class="eventDetailContent">
                    <asp:Literal ID="LitPrice" runat="server" />
                </div>
            </asp:Panel>
            <asp:Panel ID="TrDesc" runat="server" CssClass="eventDetailBlock" Visible="false">
                <div class="eventDetailLabel">
                    Beschreibung
                </div>
                <div class="eventDetailContent">
                    <asp:Literal ID="LitDesc" runat="server" />
                </div>
            </asp:Panel>
            <asp:Panel ID="TrEvent" runat="server" CssClass="eventDetailBlock" Visible="false">
                <div class="eventDetailLabel">
                    Event
                </div>
                <div class="eventDetailContent">
                    <asp:Literal ID="LitEvent" runat="server" />
                </div>
            </asp:Panel>
        </div>
        <div class="eventDetailImage">
            <asp:Image ID="Img" runat="server" EnableViewState="false" />
        </div>
    </div>
    <div class="eventAddContent">
        <div class="eventAddContentIntro">
            <web:TextControl ID="TextEventAddPicturesAndVideos" runat="server" LanguageFile="UserControls.Templates.WebUI.Base" TextKey="TextEventAddPicturesAndVideos" AllowHtml="true" />
        </div>
        <asp:HyperLink ID="LnkRelCnt" runat="server" CssClass="inputButton">
            <web:TextControl ID="CommandAddPicturesAndVideos" runat="server" LanguageFile="UserControls.Templates.WebUI.Base" TextKey="CommandAddPicturesAndVideos" />
        </asp:HyperLink>
    </div>
</div>
<telerik:RadToolTipManager ID="RTTM" runat="server" Width="300" Height="200" OnAjaxUpdate="OnAjaxUpdate" ShowCallout="False" Sticky="True">
</telerik:RadToolTipManager>
