<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Wizards.PollQuestion" CodeBehind="PollQuestion.ascx.cs" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelPollTitle") %>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipPollTitle") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtQTitle" MaxLength="100" Width="99%" runat="server" />
        <div>
            <asp:RequiredFieldValidator ID="RFVTitle" CssClass="inputErrorTooltip" runat="server" ControlToValidate="TxtQTitle" Display="Dynamic"><%=language.GetString("MessagePollTitle") %></asp:RequiredFieldValidator>
        </div>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelPollQuestion")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipPollQuestion") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtQuestion" TextMode="MultiLine" MaxLength="500" Width="99%" Height="40" runat="server" />
        <div>
            <asp:RequiredFieldValidator ID="RFVQuestion" CssClass="inputErrorTooltip" runat="server" ControlToValidate="TxtQuestion" Display="Dynamic"><%=language.GetString("MessagePollQuestion") %></asp:RequiredFieldValidator>
        </div>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelPollStart")%>:<a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=language.GetStringForTooltip("TooltipPollStart") %>">&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div class="inputBlockContent">
        <telerik:RadDatePicker ID="RDPStartDate" runat="server">
            <DatePopupButton ImageUrl="~/Library/Skins/Custom/DatePicker/Custom.gif" HoverImageUrl="~/Library/Skins/Custom/DatePicker/Custom.gif" />
        </telerik:RadDatePicker>
        &nbsp;&nbsp;&nbsp;
        <telerik:RadDatePicker ID="RDPEndDate" runat="server">
            <DatePopupButton ImageUrl="~/Library/Skins/Custom/DatePicker/Custom.gif" HoverImageUrl="~/Library/Skins/Custom/DatePicker/Custom.gif" />
        </telerik:RadDatePicker>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelPollType")%>:
    </div>
    <div class="inputBlockContent">
        <asp:DropDownList ID="DdlType" runat="server" Width="99%" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelPollAnonymous")%>:
    </div>
    <div class="inputBlockContent">
        <asp:CheckBox ID="CbxAnonymous" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelPollShowResult")%>:
    </div>
    <div class="inputBlockContent">
        <asp:DropDownList ID="DdlShowResult" runat="server" Width="99%" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelPollLayout")%>:
    </div>
    <div class="inputBlockContent">
        <asp:DropDownList ID="DdlLayout" runat="server" Width="99%" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelPollAnswerCount")%>:
    </div>
    <div class="inputBlockContent">
        <asp:DropDownList ID="DdlAnswerCount" runat="server" Width="99%" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelPollAnswers")%>:
    </div>
    <div class="inputBlockContent">
        <div style="float: left;">
            <asp:ListBox ID="LstAnswers" runat="server" Width="400px" Rows="6" />
        </div>
        <div style="float: left; margin-left: 4px;">
            <asp:LinkButton ID="LbtDel" runat="server" CssClass="inputButton" OnClick="LbtDel_Click"><%=language.GetString("CommandPollDelAnswer") %></asp:LinkButton>
        </div>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        &nbsp;
    </div>
    <div class="inputBlockContent">
        <div style="float: left;">
            <asp:TextBox ID="TxtAnswer" runat="server" Width="260px" MaxLength="100" />
        </div>
        <div style="float: left; margin-left: 4px;">
            <asp:CheckBox ID="CbxRight" runat="server" /><%=language.GetString("LabelPollIsRight") %>
        </div>
        <div style="float: left; margin-left: 4px;">
            <asp:LinkButton ID="LbtAdd" runat="server" CssClass="inputButton" OnClick="LbtAdd_Click"><%=language.GetString("CommandPollAddAnswer") %></asp:LinkButton>
        </div>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelPollRight")%>:
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtRight" MaxLength="100" Width="99%" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelPollFalse")%>:
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtFalse" MaxLength="100" Width="99%" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <%=language.GetString("LabelPollPartially")%>:
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtPartially" MaxLength="100" Width="99%" runat="server" />
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
