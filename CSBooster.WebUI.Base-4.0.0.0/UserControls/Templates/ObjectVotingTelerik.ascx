<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ObjectVotingTelerik.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.Templates.ObjectVotingTelerik" %>
<asp:UpdatePanel ID="UpnlRr" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <telerik:RadRating ID="Rr" runat="server" SelectionMode="Continuous" Precision="Item" Orientation="Horizontal" />
    </ContentTemplate>
</asp:UpdatePanel>
