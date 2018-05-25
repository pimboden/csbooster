<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.ProfileInfoCard" Codebehind="ProfileInfoCard.ascx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script type="text/javascript" src="/Library/Scripts/InformationCards.js"></script>

<div class="CSB_input_title CSB_title1">
   <%=language.GetString("TitleInfoCard")%>
</div>
<div class="inputBlock">
   <div class="inputBlockLabel">
      <%=language.GetString("LableInfoCard")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipInfoCard")%>">&nbsp;&nbsp;&nbsp;</a>
   </div>
   <div class="inputBlockContent">
      <div style="float: left;">
         <asp:Literal ID="LitInfoCardCur" runat="server" />
      </div>
      <div style="float: right;">
         <asp:LinkButton ID="LbtnInfoCardRemove" OnClientClick="RemoveNodeByID('SecXmlToken');" runat="server" CssClass="inputButton" OnClick="LbtnInfoCardRemoveClick"><%=language.GetString("CommandInfoCardRemove")%></asp:LinkButton>
      </div>
   </div>
</div>
<div class="inputBlock">
   <div class="inputBlockLabel">
      <%=language.GetString("LableInfoCardNew")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipInfoCardNew")%>">&nbsp;&nbsp;&nbsp;</a>
   </div>
   <div class="inputBlockContent">

      <script type="text/javascript">
         (function()
         {
            if (InformationCard.AreCardsSupported())
            {
     	         document.write('\x0a<OBJECT type=\x22application/x-informationCard\x22\x0a id=\x22SecXmlToken\x22\x0a name=\x22SecXmlToken\x22\x0a        class=\x22\x22>\x0a  <PARAM Name=\x22tokenType\x22\x0a         Value=\x22urn:oasis:names:tc:SAML:1.0:assertion\x22>\x0a  <PARAM Name=\x22issuer\x22\x0a         Value=\x22http://schemas.xmlsoap.org/ws/2005/05/identity/issuer/self\x22>\x0a  <PARAM Name=\x22requiredClaims\x22\x0a         Value=\x22http://schemas.xmlsoap.org/ws/2005/05/identity/claims/privatepersonalidentifier http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender\x22>\x0a  </OBJECT>\x0a');
     	         document.getElementById('InformationCardMsg').className = "hidden";
            }
            else
            {
            }
         })();
      </script>

      <div id="InformationCardMsg" style="float: left;">
         <%=language.GetString("MessageNoInfoCard")%>
      </div>
      <div style="float: left;">
         <asp:Literal ID="LitInfoCardMsg" runat="server" />
      </div>
      <div style="float: right;">
         <asp:LinkButton ID="LbtnInfoCardAdd" runat="server" CssClass="inputButton" OnClick="LbtnInfoCardAddClick"><%=language.GetString("CommandInfoCardConfirm")%></asp:LinkButton>
      </div>
   </div>
</div>
