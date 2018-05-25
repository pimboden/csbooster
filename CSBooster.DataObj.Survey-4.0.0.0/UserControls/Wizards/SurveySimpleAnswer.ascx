<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SurveySimpleAnswer.ascx.cs"
    Inherits="_4screen.CSB.DataObj.UserControls.Wizards.SurveySimpleAnswer" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadMultiPage ID="radMP" runat="server" SelectedIndex="0" RenderSelectedPageOnly="true">
    <telerik:RadPageView ID="listItem" runat="server">
        <div class='CSB_Survey_QuestionItem'>
            <div class='col1'>
                <asp:Literal ID="litQ" runat="server" />
            </div>
            <div class='col2'>
                <div class="func">
                    <asp:LinkButton CssClass="icon edit" runat="server" ID="lbEdit" OnClick="lbEdit_Click" /><asp:LinkButton
                        CssClass="icon delete" runat="server" ID="lbDel" OnClick="lbDel_Click" /></div>
            </div>
        </div>
        <div class="clearBoth">
        </div>
    </telerik:RadPageView>
    <telerik:RadPageView ID="editMask" runat="server">
        <div class="CSB_wi_settings">
            <div class="item_header">
                <asp:Literal ID="litAnswerTitle" runat="server" /><br />
            </div>
            <div class="item">
                <telerik:RadEditor ID="txtQ" runat="server" Style="z-index: 10000;"
                    Width="95%" Height="180px" ToolsFile="~/Configurations/RadEditorToolsFile1.config"
                    Language="de-DE" EditModes="Design,Html" StripFormattingOptions="AllExceptNewLines" />
            </div>
        </div>
        <div class="CSB_wi_settings">
            <div class="item_header">
                <%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("LabelSurveyAnswerWeigth")%><br />
            </div>
            <div class="item">
                <asp:TextBox ID="txtWeigth" runat="server" />
            </div>
        </div>
        <div class="CSB_wi_settings">
            <div class="item">
                <div style="float: left">
                    <asp:LinkButton ID="lbtnES" runat="server" OnClick="lbtnES_Click" CssClass="inputButton"
                        Style="float: left"><%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("ButtonSave")%></asp:LinkButton>
                    <asp:LinkButton ID="lbtnEC" runat="server" OnClick="lbtnEC_Click" CssClass="inputButton"
                        Style="float: left"><%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("ButtonCancel")%></asp:LinkButton>
                </div>
                <div class="clearBoth">
                </div>
            </div>
        </div>
        <div class="CSB_wi_settings">
            <div class="item_header">
                <%=GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("LabelSurveyAnswers")%><br />
            </div>
            <div class="item">
                <asp:PlaceHolder ID="phAnsList" runat="server" />
            </div>
        </div>
    </telerik:RadPageView>
</telerik:RadMultiPage>