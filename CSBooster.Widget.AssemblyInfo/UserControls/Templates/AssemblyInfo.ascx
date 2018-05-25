<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.Widget.UserControls.Templates.AssemblyInfo" CodeBehind="AssemblyInfo.cs" %>
<%@ Import Namespace="_4screen.CSB.Common" %>
<tr>
    <td><%=FileNameOnly%></td>
    <td><%=FileVersion%></td>    
    <td><%=ProductVersion%></td>
    <td><%=CreationTime.ToString("G")%></td>
    <td><%=DateModified.ToString("G")%></td>
</tr>