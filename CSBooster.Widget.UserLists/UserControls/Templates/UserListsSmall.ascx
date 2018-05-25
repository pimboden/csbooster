<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.Widget.UserControls.Templates.UserLists" CodeBehind="UserLists.cs" %>
<%@ Import Namespace="_4screen.CSB.Common" %>
<div class="CSB_ov_user2">
    <div class="scolor">
        <a class="scolor" href="<%=UserDetailURL%>">
            <img src="<%=UserSecondaryColorSmallURL%>" alt="<%=DataObjectUser.Nickname%>" />
        </a>
    </div>
    <div class="userclip">
        <a class="scolor" href="<%=UserDetailURL%>">
            <img class="userimg" src="<%=UserPictureURL(PictureVersion.XS)%>" alt="<%=DataObjectUser.Nickname%>"/>
        </a>
    </div>
    <div class="pcolor">
        <a class="scolor" href="<%=UserDetailURL%>">
            <img src="<%=UserPrimaryColorSmallURL%>" alt="<%=DataObjectUser.Nickname%>" />
        </a>
    </div>
</div>
