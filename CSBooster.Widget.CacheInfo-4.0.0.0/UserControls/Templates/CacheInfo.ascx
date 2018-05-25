<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.Widget.UserControls.Templates.CacheInfo" CodeBehind="CacheInfo.cs" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<tr>
    <td><%=Key%></td>
    <td><%=Size.ToString("n0")%></td>
    <td><asp:LinkButton ID="lbtnRemove" runat="server" OnClick="OnRemoveClick"><%=languageShared.GetString("CommandRemove") %></asp:LinkButton></td>
</tr>