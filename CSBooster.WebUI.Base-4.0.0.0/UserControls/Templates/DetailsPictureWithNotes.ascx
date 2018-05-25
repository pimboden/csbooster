<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.DetailsPictureWithNotes" CodeBehind="DetailsPictureWithNotes.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script type="text/javascript">
    function addNote() {
        if (imageAnnotation.isReadonly()) {
            alert('Notes set is read only, note can\'t be added')
        } else {
            imageAnnotation.addNote()
        }
    }

    function hideAllNotes() {
        imageAnnotation.hideAllNotes();
    }

    function showAllNotes() {
        imageAnnotation.showAllNotes();
    }

    function generateRDF() {
    }

    function InitiateAsyncRequest(argument) {
        var ajaxManager = <%= ram1.ClientID %>;
        ajaxManager.ajaxRequest(argument);
    }
</script>

<div class="pictureDetail">
    <asp:Panel ID="PnlPic" CssClass="fn-container" runat="server">
        <asp:Image ID="Img" runat="server" EnableViewState="false" />
        <telerik:RadAjaxManager ID="ram1" runat="server" useembeddedscripts="False" />
    </asp:Panel>
    <div class="pictureCopyright">
        <asp:Literal ID="LitCopyright" runat="server" />
    </div>
    <div class="pictureNotes">
        <asp:HyperLink ID="btnShow" runat="server" OnClick="showAllNotes();return false" CssClass="showImageNotesButton" />
        <asp:HyperLink ID="btnHide" runat="server" OnClick="hideAllNotes();return false" CssClass="hideImageNotesButton" />
        <asp:HyperLink ID="btdAdd" runat="server" OnClick="addNote();return false" CssClass="addImageNoteButton" Visible="false" />
    </div>
    <asp:Panel ID="PnlDesc" runat="server" Visible="false" CssClass="pictureDescription">
        <asp:Literal ID="LitDesc" runat="server" />
    </asp:Panel>
</div>
<telerik:RadToolTipManager ID="RTTM" runat="server" Width="300" Height="200" OnAjaxUpdate="OnAjaxUpdate" ShowCallout="False" Sticky="True" />
