<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.Widget.UserControls.Templates.UserLists" CodeBehind="UserLists.cs" %>
<%@ Import Namespace="_4screen.CSB.Common" %>
<div class="userOutput2">
    <div class="userOutput2Icon">
        <div class="userOutput2Color1">
            <a href="<%=UserDetailURL%>"><img src="<%=UserSecondaryColorLargeURL%>" /> </a>
        </div>
        <div class="userOutput2Image">
            <a href="<%=UserDetailURL%>"><img src="<%=UserPictureURL(PictureVersion.S)%>" /> </a>
        </div>
        <div class="userOutput2Color2">
            <a href="<%=UserDetailURL%>"><img src="<%=UserPrimaryColorLargeURL%>" /> </a>
        </div>
    </div>
    <div class="userOutput2Info">
        <a href="<%=UserDetailURL%>">
            <%=DataObjectUser.Nickname%></a>
    </div>
    <div class="userOutput2Info">
        Alter:
        <%=DataObjectUser.Age.ToString("#;-;-")%>
    </div>
    <div class="userOutput2Info">
        Aus:
        <%=DataObjectUser.AddressCity%>
    </div>
</div>
