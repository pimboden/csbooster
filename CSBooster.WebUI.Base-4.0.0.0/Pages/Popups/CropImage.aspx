<%@ Page Language="C#" MasterPageFile="~/MasterPages/Empty.master" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.Pages.Popups.CropImage" CodeBehind="CropImage.aspx.cs" %>

<%@ Register TagPrefix="Anders" Assembly="Anders.Web.Controls" Namespace="Anders.Web.Controls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Cnt1" runat="Server">
    <div id="popup">
        <div class="inputTitle">
            <%=language.GetString("TextCropTitle")%>
        </div>
        <div class="inputBlock">
            <Anders:ImageCropper ID="Crop" MaintainAspectRatio="false" CaptureKeys="false" CropEnabled="true" JpegQuality="90" AlternateText="Crop image" runat="server" />
        </div>
        <div class="inputBlock">
            <asp:LinkButton ID="LbtnCrop" CssClass="inputButton" OnClick="OnCropClick" runat="server"><%=languageShared.GetString("CommandCrop")%></asp:LinkButton>
            <asp:HyperLink ID="LbtnCancel" CssClass="inputButtonSecondary" OnClick="CloseWindow();" NavigateUrl="javascript:void(0)" runat="server"><%=languageShared.GetString("CommandCancel")%></asp:HyperLink>
        </div>
        <asp:Literal ID="LitScript" runat="server" />
    </div>
</asp:Content>
