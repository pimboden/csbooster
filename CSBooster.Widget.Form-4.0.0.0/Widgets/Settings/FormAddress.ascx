<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormAddress.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.FormAddress" %>

<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label1" LanguageFile="WidgetForm" LabelKey="LabelAddressData" ToolTipKey="TooltipAddressData" runat="server">
        </web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <div>
            <asp:CheckBox ID="CbxAdressShow" runat="server" />
        </div>
        <div>
            <asp:CheckBox ID="CbxAdressCommentShow" runat="server" /><label for="<%=CbxAdressCommentShow.ClientID %>" ><%=language.GetString("LabelAddressComment")%></label>
        </div>
        <div style="padding-top: 10px;">
            <asp:DropDownList runat="server" ID="DdlAdrSave" Width="300"></asp:DropDownList>
        </div>
    </div>
    <div class="inputBlockError">
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label2" LanguageFile="WidgetForm" LabelKey="LabelMustAuth" ToolTipKey="TooltipMustAuth" runat="server">
        </web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <asp:CheckBox ID="CbxMustAuth" runat="server" />
    </div>
    <div class="inputBlockError">
    </div>
</div>
