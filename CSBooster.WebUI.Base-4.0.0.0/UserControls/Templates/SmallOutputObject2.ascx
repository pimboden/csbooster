<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.SmallOutputObject2" CodeBehind="SmallOutputObject2.ascx.cs" %>
<table cellpadding="0" cellspacing="0" width="100%" class="CSB_ov_item2">
   <tr>
      <td class="left">
         <div class="img">
            <asp:HyperLink ID="LnkImg" runat="server">
               <asp:Image ID="Img1" runat="server" />
            </asp:HyperLink>
         </div>
      </td>
      <td class="right">
         <div class="desc">
            <asp:Literal ID="LitSDate" runat="server" />
         </div>
         <div class="desc">
            <asp:Literal ID="LitEDate" runat="server" />
         </div>
         <div>
            <div class="title">
               <asp:HyperLink ID="LnkTitle" runat="server" />
            </div>
            <div class="author">
               <asp:HyperLink ID="LnkAutor" runat="server" />
            </div>
            <div class="clearBoth">
            </div>
         </div>
         <div class="desc">
            <asp:Literal ID="LitDesc" runat="server" />
         </div>
    <div class="desc">
      <asp:Literal ID="litCom" runat="server" />
   </div>
     </td>
   </tr>
</table>
