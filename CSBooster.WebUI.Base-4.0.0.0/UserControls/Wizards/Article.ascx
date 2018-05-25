<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Wizards.Article" CodeBehind="Article.ascx.cs" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelTitle") %>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipTitle") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtTitle" MaxLength="100" Width="99%" runat="server" />
    </div>
    <div class="inputBlockError">
        <asp:RequiredFieldValidator ID="RFVTitle" CssClass="inputErrorTooltip" runat="server" ControlToValidate="TxtTitle" Display="Dynamic"><%=language.GetString("MessageTitle") %></asp:RequiredFieldValidator>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelDescription") %>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipDescription") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtDesc" TextMode="MultiLine" MaxLength="20000" Width="99%" Height="80" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelArticle") %>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipArticle") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <telerik:RadEditor ID="TxtArticle" runat="server" Width="99%" Height="240px" ToolsFile="~/Configurations/RadEditorToolsFile2.config" Language="de-CH" EditModes="Design" StripFormattingOptions="AllExceptNewLines" />

        <script type="text/javascript">
            Telerik.Web.UI.Editor.CommandList["buttonInsertImage"] = function(commandName, editor, args) {
                var myCallbackFunction = function InsertCustomImageSmall(sender, argument) {
                    if (argument) {
                        var imageInfo = argument.toString().split(',');
                        if (imageInfo.length == 4) {
                            var styleclass = 'CSB_img_org'
                            if (imageInfo[3] != 'None')
                                styleclass = 'CSB_img_popup'
                            var image = '<img pv="' + imageInfo[2] + '" pvp="' + imageInfo[3] + '" id="Img_' + imageInfo[0] + '" class="' + styleclass + '" alt="" src="' + imageInfo[1].replace('/S/', '/' + imageInfo[2] + '/') + '" />';
                            editor.pasteHtml(image);
                        }
                    }
                };
                radWinOpen("/Pages/Popups/PictureUploadGallery.aspx?CN=<%=CommunityID %>&OID=<%=ObjectID %>&PV=S", "Bild einfügen", 400, 100, false, myCallbackFunction, "galleryWin");
            };
        </script>

    </div>
</div>
<asp:HiddenField ID="HFTagWords" runat="server" />
<asp:HiddenField ID="HFGeoLong" runat="server" />
<asp:HiddenField ID="HFGeoLat" runat="server" />
<asp:HiddenField ID="HFZip" runat="server" />
<asp:HiddenField ID="HFCity" runat="server" />
<asp:HiddenField ID="HFStreet" runat="server" />
<asp:HiddenField ID="HFCountry" runat="server" />
<asp:HiddenField ID="HFStatus" runat="server" />
<asp:HiddenField ID="HFShowState" runat="server" />
<asp:HiddenField ID="HFCopyright" runat="server" />
<div class="inputBlock">
    <div class="inputBlockContent errorText">
        <asp:Literal ID="LitMsg" runat="server" />
    </div>
</div>
