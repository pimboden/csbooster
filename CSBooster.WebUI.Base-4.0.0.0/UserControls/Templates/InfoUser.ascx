<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InfoUser.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.Templates.InfoUser" %>
<fieldset>
    <legend>
        <web:TextControl ID="TitleUserInfo" runat="server" LanguageFile="ProfileData" TextKey="TitleUserInfo" />
    </legend>
    <div class="inputBlock">
        <div class="inputBlockLabel3">
            <web:TextControl ID="LablePicture" runat="server" LanguageFile="ProfileData" TextKey="LablePicture" />
        </div>
        <div class="inputBlockContent3">
            <asp:PlaceHolder ID="PhImage" runat="server" />
        </div>
    </div>
    <asp:Panel ID="PnlName" runat="server" CssClass="inputBlock" Visible="false">
        <div class="inputBlockLabel3">
            <web:TextControl ID="TitleNames" runat="server" LanguageFile="ProfileData" TextKey="TitleNames" />
        </div>
        <div class="inputBlockContent3">
            <asp:Literal ID="LitName" runat="server" />
        </div>
    </asp:Panel>
    <asp:Panel ID="PnlGender" runat="server" CssClass="inputBlock" Visible="false">
        <div class="inputBlockLabel3">
            <web:TextControl ID="Sex" runat="server" LanguageFile="ProfileData" TextKey="Sex" />
        </div>
        <div class="inputBlockContent3">
            <asp:Literal ID="LitGender" runat="server" />
        </div>
    </asp:Panel>
    <asp:Panel ID="PnlBirthday" runat="server" CssClass="inputBlock" Visible="false">
        <div class="inputBlockLabel3">
            <web:TextControl ID="LabelAge" runat="server" LanguageFile="ProfileData" TextKey="LabelAge" />
        </div>
        <div class="inputBlockContent3">
            <asp:Literal ID="LitAge" runat="server" />
        </div>
    </asp:Panel>
</fieldset>
<fieldset id="FsWeb" runat="server" visible="false">
    <legend>
        <web:TextControl ID="TitleWeb" runat="server" LanguageFile="ProfileData" TextKey="TitleWeb" />
    </legend>
    <asp:Panel ID="PnlHomepage" runat="server" CssClass="inputBlock" Visible="false">
        <div class="inputBlockLabel3">
            <web:TextControl ID="LableHomepage" runat="server" LanguageFile="ProfileData" TextKey="LableHomepage" />
        </div>
        <div class="inputBlockContent3">
            <asp:HyperLink ID="LnkHomepage" runat="server" Target="_blank" />
        </div>
    </asp:Panel>
    <asp:Panel ID="PnlBlog" runat="server" CssClass="inputBlock" Visible="false">
        <div class="inputBlockLabel3">
            <web:TextControl ID="LableBlog" runat="server" LanguageFile="ProfileData" TextKey="LableBlog" />
        </div>
        <div class="inputBlockContent3">
            <asp:HyperLink ID="LnkBlog" runat="server" Target="_blank" />
        </div>
    </asp:Panel>
</fieldset>
