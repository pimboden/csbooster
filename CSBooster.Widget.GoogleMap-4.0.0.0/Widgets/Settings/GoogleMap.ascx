<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GoogleMap.ascx.cs" Inherits="_4screen.CSB.Widget.Widgets.Settings.GoogleMap" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LabelUrl" LanguageFile="WidgetGoogleMap" LabelKey="LabelUrl" TooltipKey="TooltipUrl" runat="server" />
    </div>
    <div class="inputBlockContent">
        <asp:CheckBox ID="CbUrl" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LabelPreset" LanguageFile="WidgetGoogleMap" LabelKey="LabelPreset" TooltipKey="TooltipPreset" runat="server" />
    </div>
    <div class="inputBlockContent">
        <asp:DropDownList ID="DdlConfig" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LabelStyle" LanguageFile="WidgetGoogleMap" LabelKey="LabelStyle" TooltipKey="TooltipStyle" runat="server" />
    </div>
    <div class="inputBlockContent">
        <asp:RadioButtonList ID="RblStyle" runat="server" RepeatDirection="Vertical" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LabelNavigation" LanguageFile="WidgetGoogleMap" LabelKey="LabelNavigation" TooltipKey="TooltipNavigation" runat="server" />
    </div>
    <div class="inputBlockContent">
        <asp:RadioButtonList ID="RblNavi" runat="server" RepeatDirection="Vertical" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LabelLongLat" LanguageFile="WidgetGoogleMap" LabelKey="LabelLongLat" TooltipKey="TooltipLongLat" runat="server" />
    </div>
    <div class="inputBlockContent">
        <div style="float: left;">
            <asp:TextBox ID="TxtGeoLong" runat="server" />&nbsp;<asp:TextBox ID="TxtGeoLat" runat="server" />
        </div>
        <div style="float: left; margin-left: 5px;">
            <asp:HyperLink ID="LnkOpenMap" runat="server" CssClass="inputButton"><%=language.GetString("CommandShowMap") %></asp:HyperLink>
        </div>
    </div>
    <asp:HiddenField ID="HFZip" runat="server" />
    <asp:HiddenField ID="HFCity" runat="server" />
    <asp:HiddenField ID="HFStreet" runat="server" />
    <asp:HiddenField ID="HFCountry" runat="server" />
    <div class="inputBlockError">
        <asp:RegularExpressionValidator ID="RevLong" runat="server" CssClass="inputErrorTooltip" ControlToValidate="TxtGeoLong" Display="Dynamic" ValidationExpression="[0-9]*\.?[0-9]*"><%=language.GetString("ErrorMessageNotNumeric")%></asp:RegularExpressionValidator>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LabelZoom" LanguageFile="WidgetGoogleMap" LabelKey="LabelZoom" TooltipKey="TooltipZoom" runat="server" />
    </div>
    <div class="inputBlockContent">
        <div style="float: left; padding-top: 4px;">
            <%=language.GetString("LabelFar")%>
        </div>
        <div style="float: left; margin-left: 5px;">
            <telerik:radslider id="RsZoom" runat="server" maximumvalue="18" minimumvalue="0" smallchange="1" largechange="1" showdecreasehandle="false" showincreasehandle="false" dragtext="" />
        </div>
        <div style="float: left; margin-left: 5px; padding-top: 4px;">
            <%=language.GetString("LabelNear")%>
        </div>
    </div>
</div>
