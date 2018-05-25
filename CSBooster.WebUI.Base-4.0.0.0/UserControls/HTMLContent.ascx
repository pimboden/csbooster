<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HTMLContent.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.HTMLContent" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:LinkButton ID="lbtnE" runat="server" OnClick="OnEditClick" ToolTip="Inhalt anpassen" CssClass="contentEditButton" />
<asp:Literal ID="litOutput" runat="server" />
<asp:Panel ID="pnlEditMode" runat="server" CssClass="contentEditor">
    <asp:PlaceHolder ID="phL" runat="server" />
    <div class="inputBlock">
        <telerik:RadEditor ID="REd" runat="server" EnableEmbeddedBaseStylesheet="true" Width="100%" Height="200px" ToolsFile="~/Configurations/RadEditorToolsFile1.config" Language="de-CH" EditModes="Design,Html" StripFormattingOptions="AllExceptNewLines" AutoResizeHeight="true" />
        <asp:HiddenField ID="txtLN" runat="server" />
    </div>
    <div class="inputBlock">
        <asp:LinkButton ID="lbtnS" runat="server" CssClass="inputButton2" OnClick="OnSaveClick"><%=languageShared.GetString("CommandSave")%></asp:LinkButton>
        <asp:LinkButton ID="lbtnF" runat="server" CssClass="inputButtonSecondary2" OnClick="OnCancelClick"><%=languageShared.GetString("CommandCancel")%></asp:LinkButton>
    </div>
</asp:Panel>
