<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SmallOutputCommunity.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.Templates.SmallOutputCommunity" %>
<div class="CSB_ov_community">
   <div class="left">
      <div class="img">
         <asp:HyperLink ID="LnkImg" runat="server">
            <asp:Image ID="Img" runat="server" />
         </asp:HyperLink>
      </div>
   </div>
   <div class="right">
      <div class="title">
         <asp:HyperLink ID="LnkTitle" runat="server" />
      </div>
      <div class="desc2">
         <asp:Literal ID="LitDesc" runat="server" />
      </div>
      <div class="desc">
         <asp:Literal ID="LitMembers" runat="server" />
         Members |
         <asp:Literal ID="LitViews" runat="server" />
         Views
      </div>
   </div>
</div>
