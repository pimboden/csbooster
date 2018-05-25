<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Statistics.ascx.cs" Inherits="_4screen.CSB.Widget.Statistics" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="OpenFlash" Assembly="OpenFlashChartLib" Namespace="OpenFlashChartLib.Controls" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<script type="text/javascript" src="/Library/Scripts/FlashPlayer/swfobject2.js"></script>

<script type="text/javascript">
    var params =
        {
            flashvars: "data-file=<%=DefaultUrlEncoded %>",
            wmode: "transparent"
        };
    var flashvars = false;
    var attributes = false;
    swfobject.embedSWF("/Library/Scripts/FlashPlayer/open-flash-chart.swf", "<%=myChart.ClientID %>", "<%=ChartWidth %>", "<%=ChartHeight %>", "9.0.0", false, flashvars, params, attributes);
</script>

<div style="margin: 3px;">
    <asp:Panel ID="myChart" runat="server">
    </asp:Panel>
    <asp:Panel ID="myChartErr" runat="server">
    </asp:Panel>
    <div>
        <div style="float: left;">
            <a href="#" onclick="setFromToDate(1, '<%=dateFrom.ClientID %>', '<%=dateTo.ClientID %>'); return false;"><%=languageDataAccess.GetString("EnumStatisticDataRangeThisMonth")%></a>&nbsp;&nbsp;
            <a href="#" onclick="setFromToDate(2, '<%=dateFrom.ClientID %>', '<%=dateTo.ClientID %>'); return false;"><%=languageDataAccess.GetString("EnumStatisticDataRangeLastMonth")%></a>&nbsp;&nbsp; 
            <a href="#" onclick="setFromToDate(3, '<%=dateFrom.ClientID %>', '<%=dateTo.ClientID %>'); return false;"><%=languageDataAccess.GetString("EnumStatisticDataRangeLastTwoMonth")%></a>&nbsp;&nbsp; 
        </div>    
        <div style="float: left;">
            <telerik:RadDatePicker ID="dateFrom" runat="server" Width="100">
            </telerik:RadDatePicker>
            <%=GuiLanguage.GetGuiLanguage("Shared").GetString("TextTo")%>
            <telerik:RadDatePicker ID="dateTo" runat="server" Width="100">
            </telerik:RadDatePicker>
        </div>
        <div class="clearBoth">
        </div>
    </div>
</div>
&nbsp;
<asp:Literal ID="litJS" runat="server">
</asp:Literal> 





