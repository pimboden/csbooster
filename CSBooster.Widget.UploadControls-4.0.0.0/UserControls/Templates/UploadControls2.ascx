<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.Widget.UserControls.Templates.UploadControls" CodeBehind="UploadControls.cs" %>
<div class="menu">
    <ul>
        <% if (!string.IsNullOrEmpty(SetUploadLinks()))
           { %>
        <%= SetUploadLinks() %>
        <% } %>
        <% if (!string.IsNullOrEmpty(SetCreatePageLink()))
           { %>
        <li class="page">
            <%= SetCreatePageLink() %>
        </li>
        <% } %>
        <% if (!string.IsNullOrEmpty(SetCreateGroupLink()))
           { %>
        <li class="group">
            <%= SetCreateGroupLink() %>
        </li>
        <% } %>
    </ul>
</div>
