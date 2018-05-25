<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Step15.ascx.cs" Inherits="_4screen.CSB.Widget.VEMapViewerSettings_Step15" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="settingsPanel" runat="server" Visible="true">
   <div class="CSB_wi_settings">
      <div class="item_header">
         Sytlesheet URL?
      </div>
      <div class="item">
         <asp:TextBox ID="txtECSS" runat="server" />
      </div>
   </div>
   <div class="CSB_wi_settings">
      <div class="item_header">
         Bilder URL ?
      </div>
      <div class="item">
         <asp:TextBox ID="txtIP" runat="server" />
      </div>
   </div>
   <div class="CSB_wi_settings">
      <div class="item_header">
         Bilder Prefix ?
      </div>
      <div class="item">
         <asp:TextBox ID="txtPFX" runat="server" />
      </div>
   </div>
   <div class="CSB_wi_settings">
      <div class="item_header">
         Bilder Typ ?
      </div>
      <div class="item">
         <telerik:RadComboBox ID="rcbPT" Runat="server" Skin="Custom" enableembeddedskins="false">
            <items>
					<telerik:RadComboBoxItem Text="gif"  Value="gif" />
					<telerik:RadComboBoxItem Text="jpg" Value="jpg" />
					<telerik:RadComboBoxItem Text="png" Value="png"  Selected="true"/>
			   </items>
            <collapseanimation duration="200" type="OutQuint" />
         </telerik:RadComboBox>
      </div>
   </div>
   <div class="CSB_wi_settings">
      <div class="item_header">
         Anzahl Objekte ?
      </div>
      <div class="item">
         <telerik:RadComboBox ID="rcbAnz" Runat="server" Skin="Custom" enableembeddedskins="false">
            <items>
            <telerik:RadComboBoxItem Value="10000" Selected="True" Text="Alle Objekte" />
            <telerik:RadComboBoxItem Value="10" Text="10 Objekte" />
            <telerik:RadComboBoxItem  Value="20" Text="20 Objekte" />
            <telerik:RadComboBoxItem Value="50" Text="50 Objekte" />
            <telerik:RadComboBoxItem Value="100" Text="100 Objekte" />
            <telerik:RadComboBoxItem Value="200" Text="200 Objekte" />
            <telerik:RadComboBoxItem Value="400" Text="400 Objekte" />
            </items>
         </telerik:RadComboBox>
      </div>
   </div>
   <div class="CSB_wi_settings">
      <div class="item_header">
         Start Positionierung der Karte ?
      </div>
      <div class="item">
         <table>
            <tr>
               <td style="white-space: nowrap">
                  <asp:Literal ID="litLat" runat="server" Text="Latitude" /></td>
               <td><asp:TextBox ID="txtLat" runat="server" /></td>
            </tr>
            <tr>
               <td style="white-space: nowrap">
                  <asp:Literal ID="litLong" runat="server" Text="Longitude" /></td>
               <td><asp:TextBox ID="txtLong" runat="server" /></td>
            </tr>
            <tr>
               <td style="white-space: nowrap">
                  <asp:Literal ID="litZoom" runat="server" Text="ZoomLevel (1-19)" /></td>
               <td><asp:TextBox ID="txtZoom" runat="server" /></td>
            </tr>
            <tr>
               <td style="white-space: nowrap">
                  <asp:Literal ID="litMS" runat="server" Text="Karte Styl" /></td>
               <td>
                  <telerik:RadComboBox ID="rcbMS" Runat="server" Skin="Custom" enableembeddedskins="false">
                  <items>
                  <telerik:RadComboBoxItem Text="Strasse"  Value="1" />
                  <telerik:RadComboBoxItem Text="Luft" Value="2" />
                  <telerik:RadComboBoxItem Text="Hybrid" Value="3"  Selected="true"/>
                  </items>
                  <collapseanimation duration="200" type="OutQuint" />
                  </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
               <td style="white-space: nowrap">
                  <asp:Literal ID="litMM" runat="server" Text="Karte Modus" /></td>
               <td>
                  <telerik:RadComboBox ID="rcbMM" Runat="server" Skin="Custom" enableembeddedskins="false">
                  <items>
                  <telerik:RadComboBoxItem Text="2D"  Value="3" />
                  <telerik:RadComboBoxItem Text="3D" Value="2" />
                  <telerik:RadComboBoxItem Text="Beide" Value="1"  Selected="true"/>
                  </items>
                  <collapseanimation duration="200" type="OutQuint" />
                  </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
               <td style="white-space: nowrap">
                  <asp:Literal ID="litNC" runat="server" Text="Karte Modus" /></td>
               <td>
                  <telerik:RadComboBox ID="rcbNC" Runat="server" Skin="Custom" enableembeddedskins="false">
                  <items>
                  <telerik:RadComboBoxItem Text="Gross"  Value="1"  Selected="true"/>
                  <telerik:RadComboBoxItem Text="Mittel" Value="2" />
                  <telerik:RadComboBoxItem Text="Klein" Value="3" />
                  <telerik:RadComboBoxItem Text="Keine Navigation" Value="4" />
                  </items>
                  <collapseanimation duration="200" type="OutQuint" />
                  </telerik:RadComboBox></td>
            </tr>
            <tr>
               <td style="white-space: nowrap">
                  <asp:Literal ID="Literal1" runat="server" Text="Karte Breite" /></td>
               <td><asp:TextBox ID="txtMW" runat="server" Text="400px" /> </td>
            </tr>
            <tr>
               <td style="white-space: nowrap">
                  <asp:Literal ID="Literal2" runat="server" Text="Karte Höhe" /></td>
               <td><asp:TextBox ID="txtMH" runat="server" Text="400px" /> </td>
            </tr>
            <tr>
               <td style="white-space: nowrap">
                  <asp:Literal ID="Literal3" runat="server" Text="Link Area Breite" /></td>
               <td><asp:TextBox ID="txtLPW" runat="server" Text="400px" /></td>
            </tr>
         </table>
      </div>
   </div>
</asp:Panel>
