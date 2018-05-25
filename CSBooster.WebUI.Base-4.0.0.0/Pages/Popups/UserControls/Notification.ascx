<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.Pages.Popups.UserControls.Notification" Codebehind="Notification.ascx.cs" %>
<div class="CSB_popup_content">
   <div class="CSB_alerts_scroll">
      <div class="CSB_alerts_admin">
         <asp:Panel ID="pnlLocal" runat="Server">
            <asp:Literal ID="lblLT" runat="server" />
         </asp:Panel>
         <asp:Panel ID="pnlGlobal" runat="Server">
            <asp:Literal ID="lblGT" runat="server" />
         </asp:Panel>
      </div>
   </div>
</div>
<div class="CSB_popup_buttons">
   <div style="float: right;">
      <asp:LinkButton ID="lbS" Style="float: left; margin-right: 5px;" CssClass="inputButton" OnClick="lbS_Click" runat="server"><%=languageShared.GetString("CommandSave")%></asp:LinkButton>
      <asp:LinkButton ID="lbC" Style="float: left;" CssClass="inputButton" OnClientClick="GetRadWindow().Close();" runat="server"><%=languageShared.GetString("CommandCancel")%></asp:LinkButton>
   </div>
</div>
