<%@ Page Language="C#" MasterPageFile="~/MasterPages/Empty.master" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.Pages.Popups.MessageSend" CodeBehind="MessageSend.aspx.cs" %>

<%@ Register Src="~/UserControls/Templates/SmallOutputUser2.ascx" TagName="SmallUserOutput" TagPrefix="uc1" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Cnt1" runat="Server">
    <div id="popup" style="width: 550px;">
        <asp:PlaceHolder ID="receiverPlaceHolder" runat="server" />
        <asp:Panel ID="reportPanel" runat="server" CssClass="inputBlock3" Visible="false">
            <telerik:RadComboBox ID="rcbReport" Width="98%" runat="server" />
        </asp:Panel>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LableMessageSubject" runat="server" LanguageFile="Pages.Popups.WebUI.Base" LabelKey="LableMessageSubject" TooltipKey="TooltipMessageSubject" />
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="txtSubject" TextMode="SingleLine" Width="98%" runat="server" />
            </div>
        </div>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LableMessage" runat="server" LanguageFile="Pages.Popups.WebUI.Base" LabelKey="LableMessage" TooltipKey="TooltipMessage" />
            </div>
            <div class="inputBlockContent">
                <telerik:RadEditor ID="txtBody" runat="server" Width="98%" Height="240px" ToolsFile="~/Configurations/RadEditorToolsFile1.config" Language="de-CH" EditModes="Design" StripFormattingOptions="All" />
            </div>
        </div>
        <div class="inputBlock">
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlError" runat="server" CssClass="inputBlock" Visible="false">
                        <div class="inputBlockContent errorText">
                            <asp:Literal ID="errorMsg" runat="server" />
                        </div>
                    </asp:Panel>
                    <asp:Literal ID="litScript" runat="server" />
                    <div class="inputBlock2">
                        <div class="inputBlockContent">
                            <asp:LinkButton ID="sendButton" CssClass="inputButton" OnClick="OnSendMessageClick" runat="server"><%=languageShared.GetString("CommandSend")%></asp:LinkButton>
                            <a class="inputButtonSecondary" href="javascript:CloseWindow();">
                                <%= languageShared.GetString("CommandCancel")%></a>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
