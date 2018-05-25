<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Comments.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.Comments" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
    <web:labelcontrol id="Label1" languagefile="WidgetComments" labelkey="LabelAllowAnonymous" tooltipkey="TooltipAllowAnonymous" runat="server" />
    </div>
    <div class="inputBlockContent">
        <div>
            <asp:CheckBox ID="CbxAllowAnonymous" runat="server" /><label for="<%=CbxAllowAnonymous.ClientID %>" ><%=language.GetString("LabelAllowAnonymousComments")%></label>
        </div>
    </div>
    <div class="inputBlockError">
    </div>
</div>
