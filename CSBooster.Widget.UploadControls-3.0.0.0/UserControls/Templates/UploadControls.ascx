<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.Widget.UserControls.Templates.UploadControls" CodeBehind="UploadControls.cs" %>
<div class="CTY_upload-controls-msg"><%= UploadWhereMsg %></div>
<ul class="CTY_upload-controls">
    <% if (!string.IsNullOrEmpty(SetUploadLinks()))
       { %>
        <li class="title"><%= language.GetString("LabelContent")%></li>
        <%= SetUploadLinks() %>
    <% } %>
    <% if (!string.IsNullOrEmpty(SetCreatePageLink())) 
       { %>
        <li class="title"><%= language.GetString("LabelPage") %></li>
        <li><%= SetCreatePageLink() %></li>
    <% } %>
    <% if (!string.IsNullOrEmpty(SetCreateGroupLink())) 
       { %>
        <li class="title"><%= language.GetString("LabelCommunity") %></li>
        <li class="community"><%= SetCreateGroupLink() %></li>
    <% } %>
</ul>
