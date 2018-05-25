<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SurveyTestResults.ascx.cs"
    Inherits="_4screen.CSB.DataObj.UserControls.Wizards.SurveyTestResults" %>
<div class='CSB_Survey_TestResultItem'>
    <div class='col1'>
        <asp:TextBox ID="txtT" runat="server" TextMode="MultiLine" Width="200px" Rows="2" /></div>
    <div class='col2'>
        <asp:TextBox ID="txtPS" runat="server" Width="50px" />
        <asp:RequiredFieldValidator runat="server" ID="rfvPS" ControlToValidate="txtPS" ErrorMessage="&lt;br/&gt;Punkte sind zwingend"
            Display="Dynamic" />
        <asp:RangeValidator runat="server" ID="rngPS" ControlToValidate="txtPS" ErrorMessage="&lt;br/&gt;Numerische werte"
            MaximumValue="1" MinimumValue="-1" Type="Integer" Display="Dynamic" />
    </div>
    <div class='col3'>
        <asp:TextBox ID="txtPE" runat="server" Width="50px" />
        <asp:RequiredFieldValidator runat="server" ID="rfvPE" ControlToValidate="txtPE" ErrorMessage="&lt;br/&gt;Punkte sind zwingend"
            Display="Dynamic" />
        <asp:RangeValidator runat="server" ID="rngPE" ControlToValidate="txtPE" ErrorMessage="&lt;br/&gt;Numerische werte"
            MaximumValue="1" MinimumValue="-1" Type="Integer" Display="Dynamic" />
    </div>
    <div class='col4'>
        <div class="func">
            <asp:LinkButton CssClass="icon ok" runat="server" ID="lbOK" OnClick="lbOK_Click" /><asp:LinkButton
                CssClass="icon delete" runat="server" ID="lbDel" OnClick="lbDel_Click" /></div>
    </div>
</div>
<div class="clearBoth"></div>
