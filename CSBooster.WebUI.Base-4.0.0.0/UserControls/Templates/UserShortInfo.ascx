<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.UserShortInfo" Codebehind="UserShortInfo.ascx.cs" %>
<%@ Register Src="~/UserControls/Templates/SmallOutputUser2.ascx" TagName="SmallUserOutput" TagPrefix="csb" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<table>
   <tr>
      <td valign="top">
         <csb:SmallUserOutput ID="SUA" runat="server" />
      </td>
      <td valign="top">
         <div style="margin-left: 10px;">
            <asp:Literal ID="LitInfo" runat="server" />
            <asp:Literal ID="LitObjects" runat="server" />
         </div>
      </td>
   </tr>
</table>
<div style="margin-top: 10px;">
   <asp:Literal ID="LitInfo2" runat="server" />
</div>
