<%@ Page Language="C#" MasterPageFile="~/MasterPages/Empty.master" AutoEventWireup="true" CodeBehind="AddLocation.aspx.cs" Inherits="_4screen.CSB.WebUI.Pages.Popups.AddLocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cnt1" runat="Server">
    <div id="popup" style="width: 700px;">
        <asp:PlaceHolder ID="PhLocation" runat="server" />
        <div class="inputBlock">
            <div class="inputBlockContent">
                <asp:LinkButton ID="LbtnSave" CssClass="inputButton" OnClick="OnSaveClick" runat="server"><%=languageShared.GetString("CommandAdd")%></asp:LinkButton>
                <asp:LinkButton ID="LbtnCancel" CssClass="inputButtonSecondary" CausesValidation="false" OnClick="OnCancelClick" runat="server"><%=languageShared.GetString("CommandCancel") %></asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
