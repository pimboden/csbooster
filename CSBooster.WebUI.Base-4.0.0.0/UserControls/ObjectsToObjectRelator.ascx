<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.ObjectsToObjectRelator" CodeBehind="ObjectsToObjectRelator.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/UserControls/Dashboard/MyContent.ascx" TagName="MyContent" TagPrefix="csb" %>
<%@ Register Src="~/UserControls/Dashboard/MyContentSearch.ascx" TagName="MyContentSearch" TagPrefix="csb" %>
<script type="text/javascript">
    $(function() { initializeObjectRelator(); });
</script>
<div class="inputBlock">
    <div class="myContentSearchRelator">
        <div class="myContentSearchRelatorLabel">
            <web:LabelControl ID="LabelChooseObjects" runat="server" LanguageFile="UserControls.WebUI.Base" LabelKey="LabelChooseObjects" TooltipKey="TooltipChooseObjects" />
        </div>
        <div class="myContentSearchRelatorBox">
            <csb:MyContentSearch ID="MyContentSearch" runat="server" />
        </div>
    </div>
    <div class="myContentRelator objectRelator">
        <csb:MyContent ID="MyContent" runat="server" />
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockContent" style="text-align: center;">
        <span class="myContentRelatorArrow"></span>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LabelRelatedObjects" runat="server" LanguageFile="UserControls.WebUI.Base" LabelKey="LabelRelatedObjects" TooltipKey="TooltipRelatedObjects" />
    </div>
    <div class="inputBlockContent">
        <div class="objectRelator">
            <ul id="dropContainer">
                <asp:PlaceHolder ID="phRelObj" runat="server" />
            </ul>
        </div>
    </div>
</div>
