<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.Widget.UserControls.Templates.UserLists" CodeBehind="UserLists.cs" %>
<%@ Import Namespace="_4screen.CSB.Common" %>
<div class="userOutput">
    <div class="userOutputColor1">
        <a href="<%=UserDetailURL%>"><img src="<%=UserSecondaryColorSmallURL%>" alt="<%=DataObjectUser.Nickname%>" /> </a>
    </div>
    <div class="userOutputImage">
        <a href="<%=UserDetailURL%>"><img src="<%=UserPictureURL(PictureVersion.XS)%>" alt="<%=DataObjectUser.Nickname%>" /> </a>
    </div>
    <div class="userOutputColor2">
        <a href="<%=UserDetailURL%>"><img src="<%=UserPrimaryColorSmallURL%>" alt="<%=DataObjectUser.Nickname%>" /> </a>
    </div>
</div>
