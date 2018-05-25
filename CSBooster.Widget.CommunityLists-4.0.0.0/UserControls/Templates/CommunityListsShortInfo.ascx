<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.Widget.UserControls.Templates.CommunityLists" CodeBehind="CommunityLists.cs" %>
<%@ Import Namespace="_4screen.CSB.Common" %>
<div class="CSB_ov_object CSB_ov_1_c0">
    <div class="CSB_ov_community">
        <div class="left">
            <div class="img">
                <a href="<%=CommunityDetailURL%>"><img style="border-width: 0px;" src="<%=CommunityPictureURL(PictureVersion.S)%>" /> </a>
            </div>
        </div>
        <div class="right">
            <div class="title">
                <a href="<%=CommunityDetailURL%>">
                    <%=DataObjectCommunity.Title.CropString(20)%></a>
            </div>
            <div class="desc2">
                <%=DataObjectCommunity.Description.StripHTMLTags().CropString(40)%>
            </div>
            <div class="desc">
                <%=DataObjectCommunity.MemberCount.ToString("0")%>
                Members |
                <%=DataObjectCommunity.ViewCount.ToString("0")%>
                Views
            </div>
            <div class="desc">
                <%=CommunityObjectInfo()%>
            </div>
        </div>
    </div>
</div>
