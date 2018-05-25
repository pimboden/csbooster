<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Wizards.Community" CodeBehind="Community.ascx.cs" %>
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
<asp:UpdatePanel ID="UpnlCommunity" runat="server">
    <ContentTemplate>
        <div class="inputBlock3">
            <div class="inputBlockLabel">
                <%=language.GetString("LabelCommunityPermalink")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipCommunityPermalink")%>">&nbsp;&nbsp;&nbsp;</a>
            </div>
            <div class="inputBlockContent">
                <div style="float: left;">
                    [<asp:Literal ID="litHostName" runat="server" />
                </div>
                <div style="float: left;">
                    <asp:TextBox ID="txtCommName" runat="server" OnTextChanged="ValidatePage" AutoPostBack="True" MaxLength="50" Width="250px" />
                </div>
                <div style="float: left;">
                    ]
                </div>
            </div>
            <div class="inputBlockError">
                <asp:RequiredFieldValidator ID="rfvCommName" runat="server" ControlToValidate="txtCommName" CssClass="inputErrorTooltip" Display="Dynamic" EnableViewState="False"><%=language.GetString("ErrorMessageRequired") %></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revCommName" runat="server" ValidationExpression="[^~\^+;&quot;'&amp;?%&lt;&gt;()<\\/{}\s\x7f-\xff]+$" CssClass="inputErrorTooltip" ControlToValidate="txtCommName" Display="Dynamic" EnableViewState="False"><%=language.GetString("ErrorMessageInvalidCharacters")%></asp:RegularExpressionValidator>
                <asp:CustomValidator ID="cbCommName" runat="server" ControlToValidate="txtCommName" OnServerValidate="ValidateCommunityName" SetFocusOnError="true" CssClass="inputErrorTooltip" Display="Dynamic">Name ist bereits vergeben</asp:CustomValidator>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" EnableViewState="False">
    <ProgressTemplate>
        <div class="updateProgress">
            <%=languageShared.GetString("LabelUpdateProgress")%>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
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
        <telerik:RadEditor ID="RadEditor1" runat="server" Width="99%" Height="240px" ToolsFile="~/Configurations/RadEditorToolsFile1.config" Language="de-CH" EditModes="Design" StripFormattingOptions="AllExceptNewLines" />
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
<asp:HiddenField ID="HFManaged" runat="server" />
<asp:HiddenField ID="HFCopyright" runat="server" />
