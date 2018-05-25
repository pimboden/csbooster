<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.Widget.UserControls.Templates.UserLists" CodeBehind="UserLists.cs" %>
<%@ Import Namespace="_4screen.CSB.Common"%>
<div class="CSB_ov_object CSB_ov_2_c0">
    <div class="CSB_ov_user">
        <div class="img">
            <div class="border">
                <a href="<%=UserDetailURL%>">
                    <img style="border-width: 0px;" src="<%=UserSecondaryColorLargeURL%>" />
                </a>
            </div>
            <div class="border">
                <a href="<%=UserDetailURL%>">
                    <img class="usericon" style="border-width: 0px;" src="<%=UserPictureURL(PictureVersion.S)%>" />
                </a>
            </div>
            <div class="border">
                <a href="<%=UserDetailURL%>">
                    <img style="border-width: 0px;" src="<%=UserPrimaryColorLargeURL%>" />
                </a>
            </div>
        </div>
        <div class="username">
            <a href="<%=UserDetailURL%>"><%=DataObjectUser.Nickname%></a>
        </div>
        <div class="desc">
            Alter: <%=DataObjectUser.Age.ToString("#;-;-")%>
        </div>
        <div class="desc">
            Aus: <%=DataObjectUser.AddressCity%>
        </div>
    </div>
</div>
