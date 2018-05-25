<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.Widget.UserControls.Templates.UploadControls" CodeBehind="UploadControls.cs" %>
<div class="menu">
    <div class="menuTitle">
        <%= UploadWhereMsg %>
    </div>
    <ul>
        <% if (!string.IsNullOrEmpty(SetUploadLinks()))
           { %>
        <li class="menuTitle">
            <%= language.GetString("LabelContent")%>
        </li>
        <%= SetUploadLinks() %>
        <% } %>
        <% if (!string.IsNullOrEmpty(SetCreatePageLink()))
           { %>
        <li class="menuTitle">
            <%= language.GetString("LabelPage") %>
        </li>
        <li class="page">
            <%= SetCreatePageLink() %>
        </li>
        <% } %>
        <% if (!string.IsNullOrEmpty(SetCreateGroupLink()))
           { %>
        <li class="menuTitle">
            <%= language.GetString("LabelCommunity") %>
        </li>
        <li class="group">
            <%= SetCreateGroupLink() %>
        </li>
        <% } %>
    </ul>
</div>
