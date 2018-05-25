<%@ Page Language="C#" MasterPageFile="~/MasterPages/Empty.master" AutoEventWireup="True" Async="true" Inherits="_4screen.CSB.WebUI.Pages.Popups.SinglePictureUpload" CodeBehind="SinglePictureUpload.aspx.cs" %>

<%@ Import Namespace="_4screen.Utils.Web" %>
<%@ Register Src="~/UserControls/Wizards/Tagging.ascx" TagName="Tagging" TagPrefix="csb" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="Anders" Assembly="Anders.Web.Controls" Namespace="Anders.Web.Controls" %>
<asp:Content ID="Cnt" ContentPlaceHolderID="Cnt1" runat="server">
    <div id="popup">
        <asp:Panel ID="pnlUpload" runat="server" Style="width: 450px; min-height: 130px;">
            <asp:Panel ID="pnlStandart" runat="server">
                <div class="inputBlock">
                    <asp:Label ID="lblHintPictureFormat" runat="server"><%=language.GetString("LabelUploadFormats")%></asp:Label>
                </div>
                <div class="inputBlock">
                    <div class="inputBlockLabel">
                        <asp:Label ID="lblPictureUpload" runat="server"><%=language.GetString("LabelUploadTitle")%></asp:Label>
                    </div>
                    <div class="inputBlockContent">
                        <telerik:RadUpload ID="RadUpload1" runat="server" AllowedFileExtensions=".gif,.jpeg,.jpg,.png" Width="280" AllowedMimeTypes="image/gif,image/png,image/x-png,image/jpeg,image/pjpeg" ControlObjectsVisibility="None" Culture="de-CH" MaxFileInputsCount="1" UseEmbeddedScripts="False" />
                    </div>
                </div>
                <div class="inputBlock">
                    <div class="inputBlockContent">
                        <asp:LinkButton ID="lbtnUpload" CssClass="inputButton" runat="server" OnClientClick="return ShowUploadingPanel();"><%=languageShared.GetString("CommandUpload")%></asp:LinkButton>
                        <asp:LinkButton ID="lbtnCancel0" CssClass="inputButtonSecondary" OnClick="OnCancelClick" runat="server"><%=languageShared.GetString("CommandCancel")%></asp:LinkButton>
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlUploading" Style="visibility: hidden; display: none">
            <telerik:RadProgressManager ID="rpm" runat="server" />
            <telerik:RadProgressArea ID="rpa" runat="server" />
            <asp:Button ID="buttonSubmit" runat="server" OnClick="buttonSubmit_Click" CausesValidation="false" Style="visibility: hidden; display: none" />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlCroping" Visible="false" Style="width: 600px;">
            <asp:Panel ID="PnlTitle" Visible="false" runat="server">
                <div class="inputBlock">
                    <div class="inputBlockLabel">
                        <%=language.GetString("LabelTitle") %>
                    </div>
                    <div class="inputBlockContent">
                        <asp:TextBox ID="TxtTitle" Width="100%" runat="server" />
                    </div>
                </div>
                <div style="clear: both;">
                </div>
                <csb:Tagging ID="Tagging" runat="server" />
            </asp:Panel>
            <div class="inputBlock">
                <%=language.GetString("TextCropTitle")%>
            </div>
            <div class="inputBlock">
                <Anders:ImageCropper ID="Crop" MaintainAspectRatio="false" CaptureKeys="false" CropEnabled="true" JpegQuality="90" AlternateText="Crop image" runat="server" />
            </div>
            <div class="inputBlock">
                <asp:LinkButton ID="lbtnCrop" CssClass="inputButton" OnClick="OnSaveClick" runat="server"><%=languageShared.GetString("CommandSave")%></asp:LinkButton>
                <asp:LinkButton ID="lbtnCancel" CssClass="inputButtonSecondary" OnClick="OnCancelClick" runat="server"><%=languageShared.GetString("CommandCancel")%></asp:LinkButton>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlUploadFinish" Visible="false">
            <table cellpadding="0" cellspacing="10" border="0" width="100%">
                <tr>
                    <td><span id="txtsmbold" style="text-align: left;"><asp:Label ID="lblFinish" runat="server" Text="Das Hochladen wurde erfolgreich beendet."></asp:Label>
                        <asp:Repeater ID="repeaterInvalidResults" runat="server" Visible="False">
                            <HeaderTemplate>
                                <%=language.GetString("MessageUploadInvalidFile")%>:<br />
                                <br />
                            </HeaderTemplate>
                            <ItemTemplate>
                                Name:
                                <%#DataBinder.Eval(Container.DataItem, "FileName")%>
                                <br />
                                Grösse:
                                <%#DataBinder.Eval(Container.DataItem, "ContentLength").ToString() + " bytes"%>
                                <br />
                                Mime-typ:
                                <%#DataBinder.Eval(Container.DataItem, "ContentType").ToString()%>
                                <br />
                                <br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </span></td>
                </tr>
            </table>
            <div style="float: left; margin-bottom: 10px;">
                <asp:LinkButton ID="lbtnClose" CssClass="inputButton" OnClientClick="CloseWindow();" runat="server"><%=languageShared.GetString("CommandClose")%></asp:LinkButton>
            </div>
        </asp:Panel>
        <asp:Literal ID="litScript" runat="server" />
    </div>
</asp:Content>
