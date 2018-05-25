<%@ Page Language="C#" MasterPageFile="~/MasterPages/Empty.master" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.Pages.Popups.PictureUploadGallery" Title="Bild einfügen" CodeBehind="PictureUploadGallery.aspx.cs" %>

<%@ Import Namespace="_4screen.Utils.Web" %>
<%@ Register Src="~/UserControls/Dashboard/MyContent.ascx" TagName="MyContent" TagPrefix="csb" %>
<%@ Register Src="~/UserControls/Dashboard/MyContentSearch.ascx" TagName="MyContentSearch" TagPrefix="csb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Cnt1" runat="Server">
    <div id="popup" style="width: 800px;">
        <fieldset>
            <div class="inputBlock">
                <div class="inputBlockLabel">
                    <%=language.GetString("LablePicUploadTitle")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipPicUploadTitle")%>">&nbsp;&nbsp;&nbsp;</a>
                </div>
                <div class="inputBlockContent">
                    <asp:HyperLink ID="LnkImage" CssClass="inputButton" NavigateUrl="javascript:void(0)" runat="server"><%=languageShared.GetString("CommandUpload") %></asp:HyperLink>
                </div>
            </div>
            <div class="inputBlock">
                <div class="inputBlockLabel">
                    <%=language.GetString("LablePicUploadSize")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipPicUploadSize")%>">&nbsp;&nbsp;&nbsp;</a>
                </div>
                <div class="inputBlockContent">
                    <div style="float: left; margin-right: 10px;">
                        <asp:DropDownList ID="ddlPicV" runat="server">
                            <asp:ListItem Text="Extra klein" Value="XS" />
                            <asp:ListItem Text="Klein" Value="S" Selected="True" />
                            <asp:ListItem Text="Gross" Value="L" />
                            <asp:ListItem Text="Originalgrösse" Value="A" />
                        </asp:DropDownList>
                    </div>
                    <div style="float: left;">
                        <asp:DropDownList ID="ddlPicVPopup" runat="server">
                            <asp:ListItem Text="Kein Popup" Value="None" Selected="True" />
                            <asp:ListItem Text="Gross" Value="L" />
                            <asp:ListItem Text="Originalgrösse" Value="A" />
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </fieldset>
        <div class="inputBlock">
            <%=language.GetString("LablePicUploadSelect")%>
        </div>
        <div style="float: left; width: 100%;">
            <div style="float: left; width: 22%;">
                <csb:MyContentSearch ID="MyContentSearch" runat="server" />
            </div>
            <div style="float: right; width: 76%;">
                <csb:MyContent ID="MyContent" runat="server" />
            </div>

        </div>
        <asp:Literal ID="LitScript" runat="server" />
    </div>
</asp:Content>
