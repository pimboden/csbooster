<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Templates.DetailsPollQuestion" CodeBehind="DetailsPollQuestion.ascx.cs" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<div>
    <div>
        <div style="margin-bottom: 5px;">
            <%=language.GetString("LabelPollDuration")%>:
            <asp:Literal ID="LitVon" runat="server" />
            -
            <asp:Literal ID="LitBis" runat="server" />
        </div>
        <div style="margin-bottom: 5px;">
            <asp:Literal ID="DESCLIT" runat="server" />
        </div>
        <asp:Panel ID="PnlAnser" runat="server" Visible="true" EnableViewState="false">
            <div>
                <asp:CheckBoxList ID="CblAnswer" runat="server" RepeatColumns="0" EnableViewState="False" Visible="False" RepeatDirection="Vertical">
                </asp:CheckBoxList>
                <asp:RadioButtonList ID="RblAnswer" runat="server" EnableViewState="False" RepeatColumns="0" Visible="False" RepeatDirection="Vertical">
                </asp:RadioButtonList>
                <asp:LinkButton ID="LbtAnswer" CssClass="inputButton" runat="server" EnableViewState="false" OnClick="LbtAnswer_Click"><%=language.GetString("CommandPollVote")%></asp:LinkButton>
            </div>
        </asp:Panel>
        <div class="clearBoth">
        </div>
        <asp:Literal ID="LitResult" runat="server" EnableViewState="False"></asp:Literal>
        <asp:Panel ID="PnlResult" runat="server" Visible="true" EnableViewState="false">
            <telerik:RadChart ID="Chart" runat="server" Width="200px" Height="200px" TempImagesFolder="~/Log" DefaultType="Pie" SeriesOrientation="Horizontal">
                <PlotArea>
                    <XAxis>
                        <Appearance MajorTick-Visible="False">
                            <LabelAppearance Visible="False">
                            </LabelAppearance>
                        </Appearance>
                        <AxisLabel>
                            <Appearance RotationAngle="270">
                            </Appearance>
                        </AxisLabel>
                    </XAxis>
                    <YAxis AutoScale="False" MaxValue="100" MinValue="0" Step="10" MaxItemsCount="1">
                        <AxisLabel>
                            <Appearance RotationAngle="0">
                            </Appearance>
                        </AxisLabel>
                        <Items>
                            <telerik:ChartAxisItem>
                            </telerik:ChartAxisItem>
                            <telerik:ChartAxisItem Value="10">
                            </telerik:ChartAxisItem>
                            <telerik:ChartAxisItem Value="20">
                            </telerik:ChartAxisItem>
                            <telerik:ChartAxisItem Value="30">
                            </telerik:ChartAxisItem>
                            <telerik:ChartAxisItem Value="40">
                            </telerik:ChartAxisItem>
                            <telerik:ChartAxisItem Value="50">
                            </telerik:ChartAxisItem>
                            <telerik:ChartAxisItem Value="60">
                            </telerik:ChartAxisItem>
                            <telerik:ChartAxisItem Value="70">
                            </telerik:ChartAxisItem>
                            <telerik:ChartAxisItem Value="80">
                            </telerik:ChartAxisItem>
                            <telerik:ChartAxisItem Value="90">
                            </telerik:ChartAxisItem>
                            <telerik:ChartAxisItem Value="100">
                            </telerik:ChartAxisItem>
                        </Items>
                    </YAxis>
                    <YAxis2>
                        <AxisLabel>
                            <Appearance RotationAngle="0">
                            </Appearance>
                        </AxisLabel>
                    </YAxis2>
                    <Appearance Dimensions-Margins="3%, 3%, 10%, 5%">
                    </Appearance>
                </PlotArea>
                <Series>
                    <telerik:ChartSeries Name="ANSWER" Type="Pie">
                        <Appearance>
                            <LabelAppearance Position-AlignedPosition="Center">
                            </LabelAppearance>
                        </Appearance>
                    </telerik:ChartSeries>
                </Series>
                <Appearance>
                    <Border Visible="False" />
                </Appearance>
                <ChartTitle Visible="False">
                    <Appearance Visible="False">
                        <Border Visible="False" />
                    </Appearance>
                    <TextBlock Visible="False">
                    </TextBlock>
                </ChartTitle>
                <Legend Visible="False">
                    <Appearance Visible="False">
                    </Appearance>
                </Legend>
            </telerik:RadChart>
        </asp:Panel>
    </div>
    <asp:Literal ID="INFOLIT" runat="server" />
</div>
<telerik:RadToolTipManager ID="RTTM" runat="server" Width="300" Height="200" OnAjaxUpdate="OnAjaxUpdate" ShowCallout="False" Sticky="True">
</telerik:RadToolTipManager>
<telerik:RadToolTipManager ShowEvent="OnClick" runat="server" ID="RTTMIMG" EnableViewState="false" ShowCallout="false" Position="BottomRight" RelativeTo="Element" Animation="None" ShowDelay="0" Sticky="true" OnAjaxUpdate="OnAjaxUpdate">
</telerik:RadToolTipManager>
