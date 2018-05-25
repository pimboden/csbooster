<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HTMLContent_Step010.ascx.cs" Inherits="_4screen.CSB.DataObj.UserControls.Wizards.HTMLContent_Step010" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectHTMLContent").GetString("LabelTitle")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectHTMLContent").GetStringForTooltip("TooltipTitle") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtTitle" MaxLength="100" Width="99%" runat="server" />
    </div>
    <div class="inputBlockError">
        <asp:RequiredFieldValidator ID="RFVTitle" CssClass="inputErrorTooltip" runat="server" ControlToValidate="TxtTitle" Display="Dynamic"><%=GuiLanguage.GetGuiLanguage("DataObjectHTMLContent").GetString("MessageTitle") %></asp:RequiredFieldValidator>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectHTMLContent").GetString("LabelDateTimeFrom")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectHTMLContent").GetStringForTooltip("TooltipDateTimeFrom") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <telerik:RadDateTimePicker ID="RDPStartDate" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectHTMLContent").GetString("LabelDateTimeTo") %>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectHTMLContent").GetStringForTooltip("TooltipDateTimeTo") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <telerik:RadDateTimePicker ID="RDPEndDate" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=GuiLanguage.GetGuiLanguage("DataObjectHTMLContent").GetString("LabelContent") %>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=GuiLanguage.GetGuiLanguage("DataObjectHTMLContent").GetStringForTooltip("TooltipLabelContent") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <telerik:RadEditor ID="TxtEditor" runat="server" Style="z-index: 10000;" Width="99%" Height="260px" ToolsFile="~/Configurations/RadEditorToolsFile2.config" Language="de-DE" EditModes="Design,Html" StripFormattingOptions="AllExceptNewLines" ContentFilters="None" />

        <script type="text/javascript">
            Telerik.Web.UI.Editor.CommandList["buttonInsertImage"] = function(commandName, editor, args) {
                var myCallbackFunction = function InsertCustomImageSmall(sender, argument) {
                    if (argument) {
                        var imageInfo = argument.toString().split(',');
                        if (imageInfo.length == 5) {
                            var pictureFormats = imageInfo[4].toString().split(':');
                            var pictureFormat = "jpg";
                            switch (imageInfo[2]) {
                                case "XS":
                                    var pictureFormat = pictureFormats[0];
                                    break;
                                case "S":
                                    var pictureFormat = pictureFormats[1];
                                    break;
                                case "L":
                                    var pictureFormat = pictureFormats[2];
                                    break;
                                case "A":
                                    var pictureFormat = pictureFormats[3];
                                    break;
                            }
                            var imageUrl = imageInfo[1].replace('/S/', '/' + imageInfo[2] + '/');
                            imageUrl = imageUrl.replace(/\..{3}$/, '.' + pictureFormat);

                            var styleclass = 'CSB_img_org'
                            if (imageInfo[3] != 'None')
                                styleclass = 'CSB_img_popup'
                            var image = '<img pv="' + imageInfo[2] + '" pvp="' + imageInfo[3] + '" id="Img_' + imageInfo[0] + '" class="' + styleclass + '" alt="" src="' + imageUrl + '" />';
                            editor.pasteHtml(image);
                        }
                    }
                };
                radWinOpen("/Pages/Popups/PictureUploadGallery.aspx?CN=<%=CommunityID %>&OID=<%=ObjectID %>&PV=S", "Bild einfügen", 400, 100, false, myCallbackFunction, "galleryWin");
            };
        </script>

    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockContent errorText">
        <asp:Literal ID="LitMsg" runat="server" />
    </div>
</div>
<asp:HiddenField ID="HFTagWords" runat="server" />
<asp:HiddenField ID="HFGeoLong" runat="server" />
<asp:HiddenField ID="HFGeoLat" runat="server" />
<asp:HiddenField ID="HFZip" runat="server" />
<asp:HiddenField ID="HFCity" runat="server" />
<asp:HiddenField ID="HFRegion" runat="server" />
<asp:HiddenField ID="HFCountry" runat="server" />
<asp:HiddenField ID="HFStatus" runat="server" />
<asp:HiddenField ID="HFShowState" runat="server" />
<asp:HiddenField ID="HFCopyright" runat="server" />
