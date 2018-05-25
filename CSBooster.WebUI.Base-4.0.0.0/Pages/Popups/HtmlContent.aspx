<%@ Page Language="C#" MasterPageFile="~/MasterPages/Empty.master" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.Pages.Popups.HtmlContent" CodeBehind="HtmlContent.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cnt1" runat="Server">
    <div style="width: 640px; float: left; padding: 10px; background-color: #FFFFFF;">
        <div style="max-height: 500px; overflow: auto; margin-bottom: 10px;">
            <asp:Literal ID="LitContent" runat="server" />
        </div>
        <div>
            <a href="javascript:CloseWindow()" class="inputButton">
                <web:TextControl ID="CommandClose" runat="server" LanguageFile="Shared" TextKey="CommandClose" />
            </a>
        </div>
    </div>
</asp:Content>
