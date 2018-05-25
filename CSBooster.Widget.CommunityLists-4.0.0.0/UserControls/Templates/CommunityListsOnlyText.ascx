<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.Widget.UserControls.Templates.CommunityLists" CodeBehind="CommunityLists.cs" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<div class="username">
    <a href="<%=CommunityDetailURL%>"><%=DataObjectCommunity.Title.CropString(20)%></a> von <a href="<%=UserDetailURL%>"><%=DataObjectCommunity.Nickname%></a>
</div>