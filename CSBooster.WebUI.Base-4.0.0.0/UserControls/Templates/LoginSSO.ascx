<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.LoginSSO" CodeBehind="LoginSSO.ascx.cs" %>
<asp:Panel ID="PnlLogins" runat="server">
    <asp:Panel ID="PnlFacebook" runat="server" Style="float: left; width: 100%;">
        <div class="inputTitle">
            <%=language.GetString("LabelFacebookConnect")%>
        </div>

        <script type="text/javascript" src="http://connect.facebook.net/de_DE/all.js"></script>

        <asp:Literal ID="litLogin" runat="server" />
        <div id="fb-root">
        </div>

        <script type="text/javascript">
            FB.init({ appId: '<%=System.Configuration.ConfigurationManager.AppSettings["FacebookApplicationId"] %>', status: true, cookie: true, xfbml: true });
            FB.Event.subscribe('auth.login', function(response) { window.location.reload(); });
        </script>

    </asp:Panel>
    <asp:Panel ID="PnlOpenID" runat="server" Style="float: left; width: 100%;">
        <hr />
        <div class="inputTitle">
            <span>
                <asp:Image ID="ImgOpenID" ImageUrl="~/Library/Images/Layout/icon_openid.png" Style="margin-right: 2px; position: relative; top: 4px;" runat="server" />
                <%=language.GetString("LableOpenID")%>
            </span>
            <asp:HyperLink ID="LnkOpenID" NavigateUrl="javascript:void(0)" Visible="false" runat="server"><%=language.GetString("CommandWathIsIt")%></asp:HyperLink>
        </div>
        <div class="inputBlock3">
            <asp:TextBox ID="TxtOpenID" CssClass="CSB_textbox" Width="98%" runat="server" />
        </div>
        <asp:Literal runat="server" ID="LitOpenIDMsg" EnableViewState="false" Visible="true"></asp:Literal>
        <div class="inputBlock3">
            <asp:LinkButton ID="LbtnOpenIDLogin" OnClientClick="RemoveNodeByID('SecXmlToken');" CssClass="inputButton" OnClick="OpenIDButtonClick" runat="server"><%=language.GetString("CommandOpenIDLogin")%></asp:LinkButton>
        </div>
    </asp:Panel>
    <asp:Panel ID="PnlInfoCard" runat="server" Style="float: left; width: 100%;">

        <script type="text/javascript" src="/Library/Scripts/InformationCards.js"></script>

        <hr />
        <div class="inputTitle">
            <span>
                <asp:Image ID="ImgInformationCard" ImageUrl="~/Library/Images/Layout/icon_cardspace.png" runat="server" />
                Info Card </span>
            <asp:HyperLink ID="LnkInformationCard" NavigateUrl="javascript:void(0)" runat="server"><%=language.GetString("CommandWathIsIt")%></asp:HyperLink>
        </div>
        <asp:Literal runat="server" ID="LitInformationCardMsg" EnableViewState="false" Visible="true"></asp:Literal>
        <div class="inputBlock3" id="InformationCardMsg">
            <%=language.GetString("CommandWathIsIt")%>
        </div>

        <script type="text/javascript">
            (function() {
                if (InformationCard.AreCardsSupported()) {
                    document.write('\x0a<OBJECT type=\x22application/x-informationCard\x22\x0a id=\x22SecXmlToken\x22\x0a name=\x22SecXmlToken\x22\x0a        class=\x22\x22>\x0a  <PARAM Name=\x22tokenType\x22\x0a         Value=\x22urn:oasis:names:tc:SAML:1.0:assertion\x22>\x0a  <PARAM Name=\x22issuer\x22\x0a         Value=\x22http://schemas.xmlsoap.org/ws/2005/05/identity/issuer/self\x22>\x0a  <PARAM Name=\x22requiredClaims\x22\x0a         Value=\x22http://schemas.xmlsoap.org/ws/2005/05/identity/claims/privatepersonalidentifier http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender\x22>\x0a  </OBJECT>\x0a');
                    document.getElementById('InformationCardMsg').className = "hidden";
                }
                else {
                }
            })();
        </script>

        <asp:Button ID="BtnInformationCard" OnClick="IdentitySelectorIconClick" runat="server" CssClass="hidden" />
    </asp:Panel>
</asp:Panel>
<div class="clearBoth">
</div>
