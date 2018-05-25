<%@ Page Language="C#" MasterPageFile="~/MasterPages/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="UserCreate.aspx.cs" Inherits="_4screen.CSB.WebUI.Admin.UserCreate" %>

<%@ Register Src="~/Admin/UserControls/UserOutput.ascx" TagName="UserOutput" TagPrefix="csb" %>
<asp:Content ID="Cnt" ContentPlaceHolderID="Cnt1" runat="server">
    <div class="inputBlock">
        <div class="inputBlockLabel">
            Benutzername
        </div>
        <div class="inputBlockContent">
            <asp:TextBox ID="TxtUsername" runat="server" Width="200" />
        </div>
    </div>
    <div class="inputBlock">
        <div class="inputBlockLabel">
            Passwort
        </div>
        <div class="inputBlockContent">
            <asp:TextBox ID="TxtPassword" runat="server" Width="200" />
        </div>
    </div>
    <div class="inputBlock">
        <div class="inputBlockLabel">
            Email-Adresse
        </div>
        <div class="inputBlockContent">
            <asp:TextBox ID="TxtEmail" runat="server" Width="200" />
        </div>
    </div>
    <div class="inputBlock">
        <div class="inputBlockLabel">
            Vorname
        </div>
        <div class="inputBlockContent">
            <asp:TextBox ID="TxtFirstname" runat="server" Width="200" />
        </div>
    </div>
    <div class="inputBlock">
        <div class="inputBlockLabel">
            Nachname
        </div>
        <div class="inputBlockContent">
            <asp:TextBox ID="TxtLastname" runat="server" Width="200" />
        </div>
    </div>
    <asp:Panel ID="PnlStatus" runat="server" CssClass="inputBlock" Visible="false">
        <div class="inputBlockContent">
            <asp:Literal ID="LitStatus" runat="server" />
        </div>
    </asp:Panel>
    <div>
        <div class="inputBlockLabel">
        </div>
        <div class="inputBlockContent">
            <asp:LinkButton ID="lbtnCreate" runat="server" CssClass="CSB_admin_button" Text="Benutzer erstellen" CommandArgument="CreateOnly" OnClick="OnCreateClick" />
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="CSB_admin_button" Text="Benutzer erstellen & anmelden" CommandArgument="CreateAndLogin" OnClick="OnCreateClick" />
        </div>
    </div>
</asp:Content>
