<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OutputTemplates.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.OutputTemplates" %>

<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LblTemplates" LanguageFile="UserControls.WebUI.Base" LabelKey="LabelWidgetOutputTemplates" TooltipKey="TooltipWidgetOutputTemplates" runat="server" />
    </div>
    <div class="inputBlockContent">
        <asp:HiddenField ID="HfTemplate" runat="server" />
        <div class="CSB_wi_preview_area">
            <asp:Repeater ID="RepTemplates" runat="server" OnItemDataBound="OnRepTemplatesDataBound">
                <ItemTemplate>
                    <div class="CSB_wi_preview_element">
                        <asp:PlaceHolder ID="PhTemplate" runat="server" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>
<div class="clearBoth">
</div>