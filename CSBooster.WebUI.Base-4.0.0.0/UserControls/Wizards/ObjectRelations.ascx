<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Wizards.ObjectRelations" CodeBehind="ObjectRelations.ascx.cs" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<%@ Register Src="~/UserControls/ObjectsToObjectRelator.ascx" TagName="ObjectsToObjectRelator" TagPrefix="csb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LabelUploadObjects" runat="server" LanguageFile="UserControls.Wizards.WebUI.Base" />
    </div>
    <div class="inputBlockContent">
        <asp:HyperLink ID="LnkUpload" CssClass="inputButton" NavigateUrl="javascript:void(0)" runat="server">
            <web:TextControl ID="CommandUpload" runat="server" LanguageFile="Shared" TextKey="CommandUpload" />
        </asp:HyperLink>
    </div>
</div>
<csb:ObjectsToObjectRelator ID="OTOR" runat="server" />
<div class="inputBlock">
    <div class="inputBlockContent errorText">
        <asp:Literal ID="LitMsg" runat="server" />
    </div>
</div>
