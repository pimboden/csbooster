<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Wizards.Audio" CodeBehind="Audio.ascx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelPicture") %>:<span class="inputNoHelp">&nbsp;&nbsp;&nbsp;</span>
    </div>
    <div class="inputBlockContent">
        <div class="inputBlockImage">
            <img id="Img" src="" runat="server" />
        </div>
        <div class="inputBlockImage">
            <asp:HyperLink ID="LnkImage" CssClass="inputButton" NavigateUrl="javascript:void(0)" runat="server"><%=languageShared.GetString("CommandUpload") %></asp:HyperLink>
        </div>
    </div>
</div>
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
        <%=language.GetString("LabelArtist") %>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipArtist") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtArtist" MaxLength="100" Width="99%" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelAlbum") %>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipAlbum") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtAlbum" MaxLength="100" Width="99%" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelGenre") %>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipGenre") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtGenre" MaxLength="100" Width="99%" runat="server" />
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
<asp:Panel ID="PnlTagwords" CssClass="hidden" runat="server">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelTagwords") %>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipTagwords") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtTagWords" Width="99%" runat="server" />
    </div>
</asp:Panel>
<asp:Panel ID="PnlGeo" CssClass="hidden" runat="server">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelGeotagging") %>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipGeotagging") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:HyperLink ID="LnkOpenMap" NavigateUrl="javascript:void(0)" CssClass="inputButton" Style="float: left;" Text="Karte anzeigen" runat="server" />
        <div style="float: left; margin-left: 10px;">
            <%=languageShared.GetString("LabelLongitude") %>: <asp:TextBox ID="TxtGeoLong" Width="140" runat="server" />
        </div>
        <div style="float: left; margin-left: 10px;">
            <%=languageShared.GetString("LabelLatitude") %>: <asp:TextBox ID="TxtGeoLat" Width="140" runat="server" />
        </div>
        <asp:HiddenField ID="HFZip" runat="server" />
        <asp:HiddenField ID="HFCity" runat="server" />
        <asp:HiddenField ID="HFStreet" runat="server" />
        <asp:HiddenField ID="HFCountry" runat="server" />
    </div>
</asp:Panel>
<asp:HiddenField ID="HFStatus" runat="server" />
<asp:HiddenField ID="HFShowState" runat="server" />
<asp:HiddenField ID="HFFriendType" runat="server" />
<asp:HiddenField ID="HFCopyright" runat="server" />
<div class="inputBlock">
    <div class="inputBlockContent errorText">
        <asp:Literal ID="LitMsg" runat="server" />
    </div>
</div>
