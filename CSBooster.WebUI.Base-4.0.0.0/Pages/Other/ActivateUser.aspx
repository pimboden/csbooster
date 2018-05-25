<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Static.master" Inherits="_4screen.CSB.WebUI.Pages.Other.ActivateUser" CodeBehind="ActivateUser.aspx.cs" %>

<asp:Content ID="CWZ1" ContentPlaceHolderID="WCZ1" runat="Server">
    <div class="staticContent">
        <asp:Panel ID="pnlError" runat="server" class="staticContentTitleRow" Visible="False">
            <%=language.GetString("MessageNotActivated")%>
        </asp:Panel>
        <asp:Panel ID="pnlActivate" runat="server" Visible="False">
            <div class="staticContentTitleRow">
                <%=language.GetString("MessageActivated")%>
            </div>
            <div class="staticContentRow">
                <div style="float: left; line-height: 22px; margin-right: 5px;">
                    <%=language.GetString("LabelActivatCode")%>
                </div>
                <div style="float: left; margin-right: 5px;">
                    <asp:TextBox ID="TxtActivateCode" Height="18px" runat="server" Width="240px" />
                </div>
                <asp:LinkButton CssClass="inputButton" Style="float: left; margin-right: 5px;" ID="BtnActivate" runat="server" OnClick="BtnActivate_Click"><%=language.GetString("CommandActivat")%></asp:LinkButton>
                <div class="clearBoth">
                    <asp:Label ID="lblInfo" runat="server" />
                </div>
            </div>
            <asp:Literal ID="litscript" runat="server"></asp:Literal>
        </asp:Panel>
    </div>
</asp:Content>
