<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="RelatedObjectListsAdv.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.RelatedObjectListsAdv" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="PnlByUrl" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="Label4" LanguageFile="WidgetRelatedObjectLists" LabelKey="LabelByUrl" TooltipKey="TooltipByUrl" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <asp:CheckBox ID="CbxByUrl" runat="server" AutoPostBack="true" />
            </div>
            <div class="inputBlockError">
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlParentOID" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="Lbl1" LanguageFile="WidgetRelatedObjectLists" LabelKey="LabelParentOID" TooltipKey="TooltipParentOID" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <div>
                    <asp:TextBox ID="TxtParentOID" runat="server" Width="97%"></asp:TextBox>
                </div>
            </div>
            <div class="inputBlockError">
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlFeatured" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LabelControl4" LanguageFile="WidgetRelatedObjectLists" LabelKey="LabelFeatured" TooltipKey="TooltipFeatured" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <telerik:RadComboBox ID="CbxFeatured" runat="server">
                    <Items>
                        <telerik:RadComboBoxItem Text="all" Value="0" Selected= "true" />  
                        <telerik:RadComboBoxItem Text="high" Value="1" Selected= "true" />  
                        <telerik:RadComboBoxItem Text="medium" Value="2" Selected= "true" />  
                        <telerik:RadComboBoxItem Text="low" Value="3" Selected= "true" />  
                    </Items> 
                </telerik:RadComboBox>
            </div>
            <div class="inputBlockError">
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlDataSource" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="Label5" LanguageFile="WidgetRelatedObjectLists" LabelKey="LabelDataSource" TooltipKey="TooltipDataSource" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <telerik:RadComboBox ID="CbxDataSource" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CbxDataSource_SelectedIndexChanged">
                </telerik:RadComboBox>
            </div>
            <div class="inputBlockError">
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlCommunity" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LabelControl1" LanguageFile="WidgetRelatedObjectLists" LabelKey="LabelCommunity" TooltipKey="TooltipCommunity" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <div>
                    <asp:Literal ID="LitCommunity" runat="server"></asp:Literal>
                </div>
                <div>
                    <asp:TextBox ID="TxtCommunity" runat="server" Width="97%"></asp:TextBox>
                </div>
            </div>
            <div class="inputBlockError">
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlUser" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LabelControl2" LanguageFile="WidgetRelatedObjectLists" LabelKey="LabelUser" TooltipKey="TooltipUser" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtUser" runat="server" Width="40%"></asp:TextBox>
            </div>
            <div class="inputBlockError">
            </div>
        </asp:Panel>
        
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LabelControl3" LanguageFile="WidgetRelatedObjectLists" LabelKey="LabelTagList" TooltipKey="TooltipTagList" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <div>
                    <asp:TextBox ID="TxtTagList1" runat="server" Width="97%"></asp:TextBox>
                </div>
                <div>
                    <%=language.GetString("LabelTagAnd")%>
                </div>
                <div>
                    <asp:TextBox ID="TxtTagList2" runat="server" Width="97%"></asp:TextBox>
                </div>
                <div>
                    <%=language.GetString("LabelTagAnd")%>
                </div>
                <div>
                    <asp:TextBox ID="TxtTagList3" runat="server" Width="97%"></asp:TextBox>
                </div>
            </div>
            <div class="inputBlockError">
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>