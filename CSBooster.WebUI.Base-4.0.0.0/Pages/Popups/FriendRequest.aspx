<%@ Page Language="C#" MasterPageFile="~/MasterPages/Empty.master" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.Pages.Popups.FriendRequest" CodeBehind="FriendRequest.aspx.cs" %>

<%@ Register Src="~/UserControls/Templates/SmallOutputUser2.ascx" TagName="SmallUserOutput" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Cnt1" runat="Server">
    <div id="popup" style="width: 500px;">
        <div style="float: right;">
            <div style="float: left; margin-right: 5px; line-height: 55px; font-weight: bold;">
                <%=language.GetString("LableRequestReceiver")%>
            </div>
            <div style="float: left; text-align: center;">
                <asp:Literal ID="YMREC" runat="server" />
            </div>
        </div>
        <div class="inputTitle">
            <%=language.GetString("LableRequestText")%>
        </div>
        <div class="inputBlock3" style="overflow: scroll; height: 100px; border: solid 1px #CCCCCC;">
            <asp:Literal ID="MSGCNT" runat="server" />
        </div>
        <asp:Panel ID="YMTYPEPAN" runat="server" Style="float: left; width: 100%;">
            <div class="inputTitle">
                <%=language.GetString("LableRequestFriendship")%>
            </div>
            <div class="inputBlock3">
                <asp:DropDownList CssClass="CSB_form_element" ID="YMTYPESEL" runat="server" />
            </div>
            <div class="inputBlock3">
                <asp:CheckBox ID="YMBIRTH" runat="server" Checked="True" />
                <%=language.GetString("LableRequestBirthdayInfo")%>
            </div>
        </asp:Panel>
        <asp:Panel ID="YMBLOCKPAN" runat="server" Style="float: left; width: 100%;">
            <div class="inputBlock3">
                <asp:CheckBox ID="YMBLOCKCB" runat="server" />
                <%=language.GetString("LableRequestBlockMore")%>
            </div>
        </asp:Panel>
        <div class="inputTitle">
            <asp:Literal ID="MSGREPLYTITLE" runat="server" />
        </div>
        <div class="inputBlock3">
            <telerik:RadEditor CssClass="CSB_form_element" ID="MSGREPLYCNT" runat="server" Width="99%" Height="170px" ToolsFile="~/Configurations/RadEditorToolsFile1.config" Language="de-CH" EditModes="Design" StripFormattingOptions="All" />
        </div>
        <div class="inputBlock">
            <asp:LinkButton ID="SENDBTN" CssClass="inputButton" OnClick="OnSendMsgClick" runat="server"><%=languageShared.GetString("CommandSend")%></asp:LinkButton>
            <asp:LinkButton ID="CLOSEBTN" CssClass="inputButtonSecondary" OnClientClick="GetRadWindow().Close();" runat="server"><%=languageShared.GetString("CommandCancel")%></asp:LinkButton>
        </div>
    </div>
    <asp:Literal ID="litScript" runat="server" />
</asp:Content>
