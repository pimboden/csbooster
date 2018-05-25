<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SurveyStep03.ascx.cs" Inherits="_4screen.CSB.DataObj.UserControls.Wizards.SurveyStep03" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="CSB_survey_Pages"><div class="tabs">
<telerik:RadTabStrip ID="radTab" runat="server" SelectedIndex="0" OnTabClick="radTab_TabClick"  Orientation ="VerticalLeft"
    ScrollButtonsPosition="Middle" ScrollChildren="true" PerTabScrolling="true" MultiPageID="radMultiPage">
    <Tabs>
    </Tabs>
</telerik:RadTabStrip>
</div><div class="page">
<telerik:RadMultiPage ID="radMultiPage" runat="server" SelectedIndex="0">
</telerik:RadMultiPage> 
</div>
</div>
<div class="clearBoth"></div>


<div class="inputBlock">
    <div class="inputBlockContent errorText">
        <asp:Literal ID="litMsg" runat="server" />
    </div>
</div>


<asp:HiddenField ID="hfCT" runat="server"  Value="0"/>
<asp:HiddenField ID="HFTagWords" runat="server" />
<asp:HiddenField ID="HFGeoLong" runat="server" />
<asp:HiddenField ID="HFGeoLat" runat="server" />
<asp:HiddenField ID="HFZip" runat="server" />
<asp:HiddenField ID="HFCity" runat="server" />
<asp:HiddenField ID="HFRegion" runat="server" />
<asp:HiddenField ID="HFCountry" runat="server" />
<asp:HiddenField ID="HFStatus" runat="server" />
<asp:HiddenField ID="HFShowState" runat="server" />
<asp:HiddenField ID="HFCopyright" runat="server" />
