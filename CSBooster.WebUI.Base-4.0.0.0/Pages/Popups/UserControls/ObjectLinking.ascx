<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.Pages.Popups.UserControls.ObjectLinking" Codebehind="ObjectLinking.ascx.cs" %>
<%@ Import Namespace="_4screen.CSB.Common"%>
<%@ Import Namespace="_4screen.Utils.Web" %>
<div class="CSB_popup_content">
   <div style="padding-bottom: 5px;">
      <asp:Label ID="COMTIT" runat="server" EnableViewState="false"><%= GuiLanguage.GetGuiLanguage(Helper.GetObjectType(1).LocalizationBaseFileName).GetString(Helper.GetObjectType(1).NamePluralKey)%>:</asp:Label>
   </div>
   <div class="CSB_objlink_scroll">
      <table cellpadding="5" cellspacing="0" border="0" width="100%">
         <asp:Repeater ID="CTYS" runat="server" OnItemDataBound="CTYS_ItemDataBound">
            <ItemTemplate>
               <tr>
                  <td><asp:Literal ID="CTYSEL" runat="server" /></td>
                  <td style="white-space: nowrap"><asp:Literal ID="CTYTITLE" runat="server" /></td>
               </tr>
            </ItemTemplate>
         </asp:Repeater>
      </table>
   </div>
</div>
<div class="CSB_popup_buttons">
   <div style="float: right;">
      <asp:LinkButton ID="lbtnS" Style="float: left; margin-right: 5px;" CssClass="inputButton" OnClick="OnCopyObject" Text="Verlinken" runat="server"><%=GuiLanguage.GetGuiLanguage("Shared").GetString("CommandLink")%></asp:LinkButton> />
      <asp:LinkButton ID="lbtnC" Style="float: left;" CssClass="inputButton" OnClientClick="GetRadWindow().Close();" Text="Abbrechen" runat="server"><%=GuiLanguage.GetGuiLanguage("Shared").GetString("CommandCancel")%></asp:LinkButton> />
   </div>
</div>
