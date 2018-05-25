<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.ProfileBasisInfo" CodeBehind="ProfileBasisInfo.ascx.cs" %>
<%@ Register Src="~/UserControls/Templates/SmallOutputUser2.ascx" TagName="SmallUserOutput" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="PnlPicCol" runat="server">
    <fieldset>
        <legend>
            <%=languageProfile.GetString("TitleUserPicture")%>
        </legend>
        <asp:Panel ID="PnlPic" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <%=languageShared.GetString("LablePicture")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=languageProfile.GetStringForTooltip("TooltipUploadPicture")%>">&nbsp;&nbsp;&nbsp;</a>
            </div>
            <div class="inputBlockContent">
                <div style="float: left; margin-right: 10px;">
                    <asp:PlaceHolder ID="phU" runat="server"></asp:PlaceHolder>
                </div>
                <asp:LinkButton CssClass="inputButton" Style="float: left;" ID="lbtnDelete" runat="server" OnClientClick="return" OnClick="lbtnDelete_Click"><%=languageProfile.GetString("CommandRemovePicture")%></asp:LinkButton>
                <asp:HyperLink CssClass="inputButton" Style="float: left;" ID="btnAddImage" runat="server"><%=languageProfile.GetString("CommandUploadPicture")%></asp:HyperLink>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlCol" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("LableColors")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=languageProfile.GetStringForTooltip("TooltipColors")%>">&nbsp;&nbsp;&nbsp;</a>
            </div>
            <div class="inputBlockContent">
                <div style="float: left; margin-right: 10px; position: relative; left: -5px;">
                    <asp:Literal ID="litPC" runat="server"></asp:Literal>
                </div>
                <div style="float: left;">
                    <asp:Literal ID="litSC" runat="server"></asp:Literal>
                </div>
                <asp:TextBox ID="txtPC" runat="server" Text="1" CssClass="hidden" /> <asp:TextBox ID="txtSC" runat="server" Text="1" CssClass="hidden" />
            </div>
        </asp:Panel>
    </fieldset>
</asp:Panel>
<asp:Panel ID="PnlNam" runat="server">
    <fieldset>
        <legend>
            <%=languageProfile.GetString("TitleNames")%>
        </legend>
        <asp:Panel ID="PnlSN" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("FirstName")%>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox runat="server" class="" ID="txtVorname" TabIndex="101" CssClass=""></asp:TextBox>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlLN" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("Name")%>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="txtName" class="" runat="server" TabIndex="102" CssClass=""></asp:TextBox>
            </div>
        </asp:Panel>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("LableShow")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=languageProfile.GetStringForTooltip("TooltipShow")%>">&nbsp;&nbsp;&nbsp;</a>
            </div>
            <div class="inputBlockContent">
                <asp:CheckBox ID="cbxName" TabIndex="103" runat="server" />
            </div>
        </div>
    </fieldset>
</asp:Panel>
<asp:Panel ID="PnlGenBir" runat="server">
    <fieldset>
        <legend>
            <%=languageProfile.GetString("TitleData")%>
        </legend>
        <asp:Panel ID="PnlGen" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("Sex")%>
            </div>
            <div class="inputBlockContent">
                <asp:DropDownList ID="rblGender" runat="server" TabIndex="111" CssClass="inputme">
                    <asp:ListItem Selected="True" Value="-1">Keine Angabe</asp:ListItem>
                    <asp:ListItem Value="1">Frau</asp:ListItem>
                    <asp:ListItem Value="0">Mann</asp:ListItem>
                </asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlBir" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("Birthday")%>
            </div>
            <div class="inputBlockContent">
                <telerik:RadDatePicker ID="datBirthday" TabIndex="112" runat="server" MinDate="1880-01-01" />
            </div>
        </asp:Panel>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("LableShow")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=languageProfile.GetStringForTooltip("TooltipShow")%>">&nbsp;&nbsp;&nbsp;</a>
            </div>
            <div class="inputBlockContent">
                <asp:CheckBox ID="cbxGeb" TabIndex="114" runat="server" />
            </div>
        </div>
    </fieldset>
</asp:Panel>
<asp:Panel ID="PnlAdr" runat="server">
    <fieldset>
        <legend>
            <%=languageProfile.GetString("TitleAddress")%>
        </legend>
        <asp:Panel ID="PnlAdr2" runat="server">
            <div class="inputBlock">
                <div class="inputBlockLabel">
                    <%=languageProfile.GetString("Street")%>
                </div>
                <div class="inputBlockContent">
                    <asp:TextBox ID="txtStreet" class="" runat="server" TabIndex="121" CssClass=""></asp:TextBox>
                </div>
            </div>
            <div class="inputBlock">
                <div class="inputBlockLabel">
                    <%=languageProfile.GetString("Zip")%>
                </div>
                <div class="inputBlockContent">
                    <asp:TextBox ID="txtZip" class="" runat="server" TabIndex="122" MaxLength="6"></asp:TextBox>
                </div>
            </div>
            <div class="inputBlock">
                <div class="inputBlockLabel">
                    <%=languageProfile.GetString("City")%>
                </div>
                <div class="inputBlockContent">
                    <asp:TextBox ID="txtCity" class="" runat="server" TabIndex="123"></asp:TextBox>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlCou" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("Country")%>
            </div>
            <div class="inputBlockContent">
                <asp:DropDownList ID="ddlLand" runat="server" TabIndex="124" class="inputme">
                    <asp:ListItem Selected="True" Value="CH">Schweiz</asp:ListItem>
                    <asp:ListItem Value="DE">Deutschland</asp:ListItem>
                    <asp:ListItem Value="AT">&#214;sterreich</asp:ListItem>
                    <asp:ListItem Value="Andere">Anderes</asp:ListItem>
                </asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlLang" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("LableInfoStartLang")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=languageProfile.GetStringForTooltip("TooltipLang")%>">&nbsp;&nbsp;&nbsp;</a>
            </div>
            <div class="inputBlockContent">
                <asp:DropDownList ID="ddlLanguages" runat="server" TabIndex="125" class="inputme">
                    <asp:ListItem Selected="True" Value="Deutsch">Deutsch</asp:ListItem>
                    <asp:ListItem Value="Franz&#246;sisch">Franz&#246;sisch</asp:ListItem>
                    <asp:ListItem Value="Italienisch">Italienisch</asp:ListItem>
                    <asp:ListItem Value="Englisch">Englisch</asp:ListItem>
                    <asp:ListItem Value="Andere">Andere</asp:ListItem>
                </asp:DropDownList>
            </div>
        </asp:Panel>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("LableShow")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=languageProfile.GetStringForTooltip("TooltipShow")%>">&nbsp;&nbsp;&nbsp;</a>
            </div>
            <div class="inputBlockContent">
                <asp:CheckBox ID="cbxStr" TabIndex="126" runat="server" />
            </div>
        </div>
    </fieldset>
</asp:Panel>
<asp:Panel ID="PnlPer" runat="server">
    <fieldset>
        <legend>
            <%=languageProfile.GetString("TitleProperties")%>
        </legend>
        <asp:Panel ID="PnlRel" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <asp:Literal ID="LitStatus" runat="server" />
            </div>
            <div class="inputBlockContent">
                <asp:DropDownList ID="DdlStatus" runat="server" TabIndex="131" CssClass="inputme" />
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlAtr" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <asp:Literal ID="LitAttractedTo" runat="server" />
            </div>
            <div class="inputBlockContent">
                <asp:DropDownList ID="DdlAttractedTo" runat="server" TabIndex="132" CssClass="inputme" />
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlEye" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <asp:Literal ID="LitEyeColor" runat="server" />
            </div>
            <div class="inputBlockContent">
                <asp:DropDownList ID="DdlEyeColor" runat="server" TabIndex="133" CssClass="inputme" />
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlHai" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <asp:Literal ID="LitHairColor" runat="server" />
            </div>
            <div class="inputBlockContent">
                <asp:DropDownList ID="DdlHairColor" runat="server" TabIndex="134" CssClass="inputme" />
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlHei" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <asp:Literal ID="LitBodyHeight" runat="server" />
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtBodyHeight" runat="server" TabIndex="135" MaxLength="3" Width="40" CssClass="inputme" /> cm
                <asp:RangeValidator ID="RvBodyHeight" ControlToValidate="TxtBodyHeight" Type="Integer" MinimumValue="1" MaximumValue="999" ErrorMessage="<br/>Bitte Grösse in cm angeben!" Display="Dynamic" EnableViewState="false" runat="server" />
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlWei" runat="server" CssClass="inputBlock">
            <div class="inputBlockLabel">
                <asp:Literal ID="LitBodyWeight" runat="server" />
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtBodyWeight" runat="server" TabIndex="136" MaxLength="3" Width="40" CssClass="inputme" /> kg
                <asp:RangeValidator ID="RvBodyWeight" ControlToValidate="TxtBodyWeight" Type="Integer" MinimumValue="1" MaximumValue="999" ErrorMessage="<br/>Bitte Gewicht in kg angeben!" Display="Dynamic" EnableViewState="false" runat="server" />
            </div>
        </asp:Panel>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("LableShow")%><a href="javascript:void(0)" class="inputHelp" tabindex="-1" title="<%=languageProfile.GetStringForTooltip("TooltipShow")%>">&nbsp;&nbsp;&nbsp;</a>
            </div>
            <div class="inputBlockContent">
                <asp:CheckBox ID="CbPersonalShow" TabIndex="137" runat="server" />
            </div>
        </div>
    </fieldset>
</asp:Panel>
