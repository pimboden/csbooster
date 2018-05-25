<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SurveyHistory.ascx.cs" Inherits="_4screen.CSB.DataObj.UserControls.Dashboard.SurveyHistory" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:SqlDataSource ID="sqlDS" runat="server" ConnectionString="Data Source=4SAPPL02\SQL2005;Initial Catalog=CSBooster.V20;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [OBJ_ID], [TestDate], [TestTitle], [TotalTestResult], [TestResultText], [Light] FROM [hitbl_Survey_User_Result_SUR] WHERE ([USR_ID] = @USR_ID AND [IsContest] = 0) ORDER BY [TestDate] DESC, [TestTitle]">
    <SelectParameters>
        <asp:ControlParameter ControlID="hidUserId" Name="USR_ID" PropertyName="Value" Type="String" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:HiddenField runat="server" ID="hidUserId" Value="" />
<telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" AllowSorting="True" DataSourceID="sqlDS" GridLines="None">
    <HeaderContextMenu EnableTheming="True">
        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
    </HeaderContextMenu>
    <MasterTableView AutoGenerateColumns="False" DataSourceID="sqlDS">
        <NoRecordsTemplate>
            Keine Test gefunden
        </NoRecordsTemplate>
        <RowIndicatorColumn>
            <HeaderStyle Width="20px"></HeaderStyle>
        </RowIndicatorColumn>
        <ExpandCollapseColumn>
            <HeaderStyle Width="20px"></HeaderStyle>
        </ExpandCollapseColumn>
        <Columns>
            <telerik:GridBoundColumn DataField="TestDate" DataType="System.DateTime" HeaderText="Datum" SortExpression="TestDate" UniqueName="TestDate" DataFormatString="{0:dd.MM.yy}">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="TestTitle" HeaderText="Titel" SortExpression="TestTitle" UniqueName="TestTitle">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="TotalTestResult" DataType="System.Double" HeaderText="Punkte" SortExpression="TotalTestResult" DataFormatString="{0:F1}" UniqueName="TotalTestResult">
                <ItemStyle HorizontalAlign="Center" />
            </telerik:GridBoundColumn>
            <telerik:GridTemplateColumn DataField="TestResultText" HeaderText="Text" UniqueName="TestResultText">
                <ItemTemplate>
                    <div style="width: 220px; height: 50px; overflow: auto">
                        <%# Eval("TestResultText")%></div>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn DataField="Light" HeaderText="" UniqueName="Light">
                <ItemTemplate>
                    <asp:Image ImageUrl='<%# GetImage(Eval("Light"))%>' runat="server" /></ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn DataField="OBJ_ID" HeaderText="" UniqueName="OBJ_ID">
                <ItemTemplate>
                    <%# GetLink(Eval("OBJ_ID"))%></ItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
    <FilterMenu EnableTheming="True">
        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
    </FilterMenu>
</telerik:RadGrid>
