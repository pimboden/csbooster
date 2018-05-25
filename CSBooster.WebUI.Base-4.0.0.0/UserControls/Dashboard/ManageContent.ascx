<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageContent.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.ManageContent" %>
<%@ Register Src="~/UserControls/Dashboard/MyContent.ascx" TagName="MyContent" TagPrefix="csb" %>
<%@ Register Src="~/UserControls/Dashboard/MyContentSearch.ascx" TagName="MyContentSearch" TagPrefix="csb" %>
<div style="float: left; width: 100%;">
    <div style="float: left; width: 22%;">
        <csb:MyContentSearch ID="myContentSearch" runat="server" />
    </div>
    <div style="float: right; width: 76%;">
        <csb:MyContent ID="myContent" runat="server" />
    </div>
</div>
<div class="clearBoth">
</div>
