<%@ Page Language="C#" MasterPageFile="~/MasterPages/Empty.master" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.Pages.Popups.SimplePictureUploadGallery" Title="Bild einfügen" CodeBehind="SimplePictureUploadGallery.aspx.cs" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Cnt1" runat="Server">
    <div id="popup" style="width: 630px;">
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <%=language.GetString("LablePicUploadTitle")%>
            </div>
            <div class="inputBlockContent">
                <telerik:RadUpload ID="RadUpload" runat="server" AllowedFileExtensions=".gif,.jpeg,.jpg,.png" AllowedMimeTypes="image/gif,image/png,image/x-png,image/jpeg,image/pjpeg" ControlObjectsVisibility="None" Culture="de-CH" MaxFileInputsCount="1" useembeddedscripts="False" />
            </div>
        </div>
        <div class="inputBlock">
            <div class="inputBlockContent">
                <asp:LinkButton ID="LbtnUpload" CssClass="inputButton" runat="server"><%=language.GetString("CommandUpload")%></asp:LinkButton>
            </div>
        </div>
        <div class="inputBlock">
            <%=language.GetString("LablePicUploadSelectSimple")%>
        </div>
        <div class="inputBlock">
            <script type="text/javascript">
                function CloseAndSetPicture(pictureUrl) {
                    var oWnd = GetRadWindow();
                    var dialog1 = oWnd.get_windowManager().getWindowByName("WidgetSettings");
                    var contentWin = dialog1.get_contentFrame().contentWindow;
                    contentWin.<%=Callback %>(pictureUrl);
                    oWnd.close();
                }
            </script>
            <asp:DataList ID="DLObjects" runat="server" ItemStyle-CssClass="CSB_bg_tn" OnItemDataBound="OnObjectsItemDataBound" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="8" CellPadding="0" Width="100%" EnableViewState="False" ShowFooter="False" ShowHeader="False">
                <ItemTemplate>
                    <div style="position: relative; width: 70px; height: 70px; border: solid 1px #CCCCCC;">
                        <div style="position: absolute; max-width: 60; max-height: 60px; top: 5px; left: 5px;">
                            <asp:HyperLink ID="LnkImg" NavigateUrl="javascript:void(0)" runat="server" />
                        </div>
                        <div style="position: absolute; bottom: 2px; right: 2px;">
                            <asp:LinkButton ID="LbtnDelete" OnClick="OnDeleteClick" runat="server">
                                <img id="ImgDel" src="~/Library/Images/Layout/cmd_delete2.png" runat="server" />
                            </asp:LinkButton>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
        <asp:Literal ID="LitScript" runat="server" />
    </div>
</asp:Content>
