<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Step10.ascx.cs" Inherits="_4screen.CSB.Widget.KyteVideoSettings_Step10" %>
<asp:Panel ID="settingsPanel" runat="server">
   <div class="CSB_wi_settings">
      <div class="item_header">
         Kyte-Channel-Nummer:
      </div>
      <div class="item">
         <asp:TextBox ID="txtChannel" runat="server" />
      </div>
   </div>
   <div>
      <b>Wie finde ich die Channel-Nummer?</b><br />
      Die Kyte-Channel-Nummer findest Du in der jeweilgen URL auf www.kyte.tv.<br />
      <br />
      <b>Beispiel</b><br />
      http://www.kyte.tv/channels/browse.html?mode=VIEWERS#uri,channels/<b>459</b>.<br />
      <br />
      Du musst nun nur in diesem Beispiel die 459 in das oben stehende Eingabefeld kopieren.
   </div>
</asp:Panel>

