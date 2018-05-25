<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Step10.ascx.cs" Inherits="_4screen.CSB.Widget.TestWidgetSettings_Step10" %>

<script language="javascript">
   function setimage(objID)
   {
      var txtEle = document.getElementById('<%= txtImg.ClientID %>');
      
      if (txtEle.value.length > 0 && document.getElementById(txtEle.value) != null)
         document.getElementById(txtEle.value).style.border = "solid 2px silver";
      else if (txtEle.value.length == 0)
         document.getElementById('CSBNo').style.border = "solid 2px silver";
               
      txtEle.value = objID;
      document.getElementById(objID).style.border = "solid 2px yellow";
   }
</script>
<asp:Panel ID="settingsPanel" runat="server" Visible="true">
	<div class="CSB_wi_settings">
		<div class="item_header">
			Welches Bild möchtest Du zeigen?
		</div>
		<div class="item"  style="height: 220px; overflow:auto;">
		   <div>
		   <asp:Literal ID="LitImg" runat="server" EnableViewState="false"></asp:Literal>
		   </div>
		</div>
	</div>
	<div class="CSB_wi_settings" style="clear:both">
		<div class="item_header">
			Welche Zusatzinfo soll gezeigt werden?
		</div>
		<div class="item">
         <asp:CheckBox ID="cbxUsr" runat="server" Text="Autor" /><br />
			<asp:CheckBox ID="cbxTit" runat="server" Text="Bild-Titel" /><br />
         <asp:CheckBox ID="cbxDesc" runat="server" Text="Bild-Beschrieb" /><br />
         <asp:CheckBox ID="cbxRating" runat="server" Text="Bewertung erlauben" />
         <asp:TextBox ID="txtImg" runat="server" Text="" style="visibility:hidden;display:none"/>
		</div>
	</div>
   <div class="CSB_wi_settings" style="clear: both">
      <div class="item_header">
         Welche breit soll das Bild dargestellt werden?
      </div>
      <div class="item">
         <asp:DropDownList ID="ddlImgWidth" runat="server">
            <asp:ListItem Value="">Originalgrösse</asp:ListItem>
            <asp:ListItem Value="100">100 px</asp:ListItem>
            <asp:ListItem Value="150">150 px</asp:ListItem>
            <asp:ListItem Value="200">200 px</asp:ListItem>
            <asp:ListItem Value="250">250 px</asp:ListItem>
            <asp:ListItem Value="300" Selected="True">300 px</asp:ListItem>
            <asp:ListItem Value="360">360 px</asp:ListItem>
            <asp:ListItem Value="400">400 px</asp:ListItem>
            <asp:ListItem Value="450">450 px</asp:ListItem>
            <asp:ListItem Value="500">500 px</asp:ListItem>
            <asp:ListItem Value="550">550 px</asp:ListItem>
            <asp:ListItem Value="600">600 px</asp:ListItem>
            <asp:ListItem Value="650">650 px</asp:ListItem>
            <asp:ListItem Value="730">Maximalbreite</asp:ListItem>
         </asp:DropDownList>
      </div>
   </div>
</asp:Panel>
