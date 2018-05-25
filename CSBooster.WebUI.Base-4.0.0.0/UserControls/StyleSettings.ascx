<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StyleSettings.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.StyleSettings" %>
<%@ Import Namespace="_4screen.Utils.Web" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script type="text/javascript">
    function Set<%=Type %>Background(pictureUrl) {
        if(pictureUrl != null)
        {
            var imageLarge = pictureUrl.replace(/BG\/S/i, "BG/L");
            document.getElementById("<%=TxtBackground.ClientID %>").value = imageLarge;
            document.getElementById("<%=CbBackground.ClientID %>").checked = true;
            On<%=Type %>Change(null, null);
        }
    }

    function On<%=Type %>Change(sender, eventArgs) {
        var textColor = $find("<%=RcpText.ClientID %>").get_selectedColor();
        if($find("<%=RcpLink.ClientID %>") != null)
            var linkColor = $find("<%=RcpLink.ClientID %>").get_selectedColor();
        var backgroundColor = $find("<%=RcpBackground.ClientID %>").get_selectedColor();
        var backgroundImageActive = document.getElementById("<%=CbBackground.ClientID %>").checked;
        var backgroundImage = document.getElementById("<%=TxtBackground.ClientID %>").value;
        var verticalRepetition = document.getElementById("<%=CbVertical.ClientID %>").checked;
        var hortizontalRepetition = document.getElementById("<%=CbHorizontal.ClientID %>").checked;
        var borderColor = $find("<%=RcpBorder.ClientID %>").get_selectedColor();
        var borderType = $find("<%=RcbBorder.ClientID %>").get_value();
        var borderWidth = $find("<%=RntbBorder.ClientID %>").get_value();
        
        var mainRule = "";
        var subRule = "";
        if(textColor != null)
        {
            mainRule += "color:"+textColor+"; ";
        }
        if(linkColor != null)
        {
            subRule += "color:"+linkColor+"; ";
        }
        if(backgroundColor != null)
        {
            mainRule += "background-color:"+backgroundColor+"; ";
        }
        else
        {
            mainRule += "background-color:Transparent; ";
        }
        if(backgroundImageActive)
        {
            if(backgroundImage != "")
                mainRule += "background-image:url("+backgroundImage+"); ";
                
            if(verticalRepetition && hortizontalRepetition)
                mainRule += "background-repeat:repeat; ";
            else if(verticalRepetition)
                mainRule += "background-repeat:repeat-y; ";
            else if(hortizontalRepetition)
                mainRule += "background-repeat:repeat-x; ";
            else
                mainRule += "background-repeat:no-repeat; ";
        }
        else
        {
            mainRule += "background-image:none; ";
        }
        if(borderColor != null)
        {
            mainRule += "border:"+borderType+" "+borderWidth+"px "+borderColor+"; ";
        }
        else
        {
            mainRule += "border:none; ";
        }
        
        UpdateStyles("<%=TargetClass %>", mainRule);
        if(subRule != "")
            UpdateStyles("<%=TargetClass %> a", subRule);
    }
</script>

<div style="height: 5px;">
</div>
<div class="inputBlock">
    <div class="inputBlockLabel2">
        <web:LabelControl ID="LblText" LanguageFile="UserControls.WebUI.Base" LabelKey="LabelTextColor" TooltipKey="TooltipTextColor" runat="server" />
    </div>
    <div class="inputBlockContent2">
        <telerik:RadColorPicker ID="RcpText" PaletteModes="WebPalette,HSV" runat="server" ShowIcon="true" />
    </div>
</div>
<asp:Panel ID="PnlLinkColor" runat="server" CssClass="inputBlock">
    <div class="inputBlockLabel2">
        <web:LabelControl ID="LblLimk" LanguageFile="UserControls.WebUI.Base" LabelKey="LabelLinkColor" TooltipKey="TooltipLinkColor" runat="server" />
    </div>
    <div class="inputBlockContent2">
        <telerik:RadColorPicker ID="RcpLink" PaletteModes="WebPalette,HSV" runat="server" ShowIcon="true" />
    </div>
</asp:Panel>
<div class="inputBlock">
    <div class="inputBlockLabel2">
        <web:LabelControl ID="LblBackground" LanguageFile="UserControls.WebUI.Base" LabelKey="LabelBackgroundColor" TooltipKey="TooltipBackgroundColor" runat="server" />
    </div>
    <div class="inputBlockContent2">
        <telerik:RadColorPicker ID="RcpBackground" PaletteModes="WebPalette,HSV" runat="server" ShowIcon="true" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel2">
        <web:LabelControl ID="LblBGImage" LanguageFile="UserControls.WebUI.Base" LabelKey="LabelBackgroundImage" TooltipKey="TooltipBackgroundImage" runat="server" />
    </div>
    <div class="inputBlockContent2">
        <div style="float: left;">
            <asp:CheckBox ID="CbBackground" runat="server" />
        </div>
        <div style="float: left; margin-left: 2px;">
            <asp:TextBox ID="TxtBackground" Width="150" runat="server" />
        </div>
        <asp:HyperLink ID="LnkBackground" CssClass="imageUploadButton" Style="float: left; margin-left: 4px; margin-top: 2px;" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel2">
        <web:LabelControl ID="LblRepetition" LanguageFile="UserControls.WebUI.Base" LabelKey="LabelRepetition" TooltipKey="TooltipRepetition" runat="server" />
    </div>
    <div class="inputBlockContent2">
        <div style="float: left;">
            <asp:CheckBox ID="CbHorizontal" runat="server" /><asp:Label ID="LblHorizontal" runat="server" AssociatedControlID="CbHorizontal"><img src="/Library/Images/Layout/cmd_horizontal.png" style="margin-right:2px;"/><%= GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("LabelHorizontal")%></asp:Label>
        </div>
        <div style="float: left;">
            <asp:CheckBox ID="CbVertical" runat="server" /><asp:Label ID="Label1" runat="server" AssociatedControlID="CbVertical"><img src="/Library/Images/Layout/cmd_vertical.png" style="margin-right:2px;"/><%= GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("LabelVertical")%></asp:Label>
        </div>
    </div>
</div>
<div class="inputBlock3">
    <div class="inputBlockLabel2">
        <web:LabelControl ID="LblBorder" LanguageFile="UserControls.WebUI.Base" LabelKey="LabelBorder" TooltipKey="TooltipBorder" runat="server" />
    </div>
    <div class="inputBlockContent2">
        <div style="float: left;">
            <telerik:RadColorPicker ID="RcpBorder" PaletteModes="WebPalette,HSV" runat="server" ShowIcon="true" />
        </div>
        <div style="float: left; margin-left: 5px;">
            <telerik:RadComboBox ID="RcbBorder" runat="server" Width="100">
                <Items>
                    <telerik:RadComboBoxItem Value="solid" />
                    <telerik:RadComboBoxItem Value="dashed" />
                </Items>
            </telerik:RadComboBox>
        </div>
        <div style="float: left; margin-left: 5px;">
            <telerik:RadNumericTextBox ID="RntbBorder" Width="50" runat="server" Value="1" MinValue="1" MaxValue="10" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" NumberFormat-PositivePattern="n px" />
        </div>
    </div>
</div>
