<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Wizards.Forum" CodeBehind="Forum.ascx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelPicture") %>:
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
        <%=language.GetString("LabelDescription") %>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipDescription") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <telerik:RadEditor ID="TxtDesc" runat="server" Width="99%" Height="200px" ToolsFile="~/Configurations/RadEditorToolsFile1.config" Language="de-CH" EditModes="Design" StripFormattingOptions="AllExceptNewLines" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelForumTopicCreation") %>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipForumTopicCreation")%>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <telerik:RadComboBox ID="RcbRights" ZIndex="90000" runat="server" Width="100%">
            <Items>
                <telerik:RadComboBoxItem Value="0" Text="Ersteller / Besitzer" />
                <telerik:RadComboBoxItem Value="2" Text="Alle Mitglieder" />
            </Items>
        </telerik:RadComboBox>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelForumUserVisibliity") %>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipForumUserVisibliity")%>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:CheckBoxList ID="CblUserVisibliity" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Value="0" Text="Owner"></asp:ListItem> 
            <asp:ListItem Value="1" Text="Owner"></asp:ListItem> 
            <asp:ListItem Value="2" Text="Owner"></asp:ListItem> 
        </asp:CheckBoxList> 
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelForumCharied")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipForumCharied") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:CheckBox ID="CbxCharied" runat="server" /> 
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
