<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="Rating.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.Rating" %>

<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label2" LanguageFile="WidgetRating" LabelKey="LabelShowInfo" TooltipKey="TooltipShowInfo" runat="server"></web:LabelControl>
    </div>
    <div class="inputBlockContent">
            <asp:CheckBox ID="CbxShowInfo" runat="server" />
    </div>
    <div class="inputBlockError">
    </div>
</div>