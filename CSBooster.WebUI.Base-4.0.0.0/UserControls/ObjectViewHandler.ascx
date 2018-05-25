<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.ObjectViewHandler" CodeBehind="ObjectViewHandler.ascx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnlView" runat="server">
    <fieldset>
        <legend>
            <web:TextControl ID="TextTitleVisibility" runat="server" LanguageFile="UserControls.WebUI.Base" TextKey="TitleVisibility" />
        </legend>
        <asp:Panel ID="TRVisibility" CssClass="inputBlock" runat="server" Visible="false">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LableVisibility" LanguageFile="UserControls.WebUI.Base" LabelKey="LableVisibility" TooltipKey="TooltipVisibility" runat="server" />
            </div>
            <div class="inputBlockContent">
                <div>
                    <asp:RadioButton ID="RbtPublic" runat="server" GroupName="Status" />
                </div>
                <div>
                    <asp:RadioButton ID="RbtPrivat" runat="server" GroupName="Status" />
                    <div style="padding-left: 22px;">
                        <asp:CheckBoxList ID="CbxFriends" runat="server" RepeatLayout="Flow" RepeatDirection="Vertical" />
                    </div>
                </div>
                <div>
                    <asp:RadioButton ID="RbtUnlisted" runat="server" GroupName="Status" />
                </div>
            </div>
            <div class="inputBlockContent">
            </div>
        </asp:Panel>
        <asp:Panel ID="TRRoles" CssClass="inputBlock" runat="server" Visible="false">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LableVisibilityRole" LanguageFile="UserControls.WebUI.Base" LabelKey="LableVisibilityRole" TooltipKey="TooltipVisibilityRole" runat="server" />
            </div>
            <div class="inputBlockContent">
                <telerik:RadComboBox ID="rcRoles" runat="server" Width="100%" />
            </div>
        </asp:Panel>
        <asp:Panel ID="TRShowState" CssClass="inputBlock" runat="server" Visible="false">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LableVisibilityStatus" LanguageFile="UserControls.WebUI.Base" LabelKey="LableVisibilityStatus" TooltipKey="TooltipVisibilityStatus" runat="server" />
            </div>
            <div class="inputBlockContent">
                <telerik:RadComboBox ID="rdST" runat="server" Width="100%" />
            </div>
        </asp:Panel>
        <asp:Panel ID="TRManaged" CssClass="inputBlock" runat="server" Visible="false">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LableVisibilityRelease" LanguageFile="UserControls.WebUI.Base" LabelKey="LableVisibilityRelease" TooltipKey="TooltipVisibilityRelease" runat="server" />
            </div>
            <div class="inputBlockContent">
                <telerik:RadComboBox ID="rcMng" runat="server" Width="100%" />
            </div>
        </asp:Panel>
        <div class="inputBlock">
            <asp:Label ID="lblInfo" runat="server" />
        </div>
    </fieldset>
</asp:Panel>
