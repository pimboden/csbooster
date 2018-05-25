<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Form" CodeBehind="Form.ascx.cs" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<asp:Panel ID="pnlResult" runat="server" Style="float: left; width: 100%;">
    <asp:Panel ID="PnlText" runat="server" class="inputBlock" Visible="false">
        <div class="inputBlock">
            <asp:Literal ID="LitText" runat="server" Visible="false"></asp:Literal>
        </div>
        <div class="inputBlock">
            <asp:TextBox ID="TxtText" runat="server" Visible="false" MaxLength="500" Width="99%" TextMode="MultiLine" Rows="4"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RfvText" runat="server" ControlToValidate="TxtText" Display="Dynamic" Enabled="false" ValidationGroup="GrpForm"><%=languageShared.GetString("MessageMustField")%></asp:RequiredFieldValidator>
        </div>
    </asp:Panel>
    <asp:Panel ID="PnlOption" runat="server" class="inputBlock" Visible="false">
        <asp:CheckBoxList ID="CblOption" runat="server" Visible="false">
        </asp:CheckBoxList>
        <asp:RadioButtonList ID="RblOption" runat="server" Visible="false">
        </asp:RadioButtonList>
    </asp:Panel>
    <asp:Panel ID="PnlFields" runat="server" Visible="false" class="inputBlock">
        <asp:Panel ID="PnlField0" runat="server" Visible="false" class="inputBlock">
            <div class="inputBlockLabel">
                <asp:Literal ID="LitField0" runat="server"></asp:Literal>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtField0" runat="server" MaxLength="100" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RfvField0" runat="server" ControlToValidate="TxtField0" Display="Dynamic" ValidationGroup="GrpForm"><%=languageShared.GetString("MessageMustField")%></asp:RequiredFieldValidator>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlField1" runat="server" Visible="false" class="inputBlock">
            <div class="inputBlockLabel">
                <asp:Literal ID="LitField1" runat="server"></asp:Literal>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtField1" runat="server" MaxLength="100" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RfvField1" runat="server" ControlToValidate="TxtField1" Display="Dynamic" ValidationGroup="GrpForm"><%=languageShared.GetString("MessageMustField")%></asp:RequiredFieldValidator>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlField2" runat="server" Visible="false" class="inputBlock">
            <div class="inputBlockLabel">
                <asp:Literal ID="LitField2" runat="server"></asp:Literal>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtField2" runat="server" MaxLength="100" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RfvField2" runat="server" ControlToValidate="TxtField2" Display="Dynamic" ValidationGroup="GrpForm"><%=languageShared.GetString("MessageMustField")%></asp:RequiredFieldValidator>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlField3" runat="server" Visible="false" class="inputBlock">
            <div class="inputBlockLabel">
                <asp:Literal ID="LitField3" runat="server"></asp:Literal>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtField3" runat="server" MaxLength="100" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RfvField3" runat="server" ControlToValidate="TxtField3" Display="Dynamic" ValidationGroup="GrpForm"><%=languageShared.GetString("MessageMustField")%></asp:RequiredFieldValidator>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlField4" runat="server" Visible="false" class="inputBlock">
            <div class="inputBlockLabel">
                <asp:Literal ID="LitField4" runat="server"></asp:Literal>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtField4" runat="server" MaxLength="100" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RfvField4" runat="server" ControlToValidate="TxtField4" Display="Dynamic" ValidationGroup="GrpForm"><%=languageShared.GetString("MessageMustField")%></asp:RequiredFieldValidator>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlField5" runat="server" Visible="false" class="inputBlock">
            <div class="inputBlockLabel">
                <asp:Literal ID="LitField5" runat="server"></asp:Literal>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtField5" runat="server" MaxLength="100" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RfvField5" runat="server" ControlToValidate="TxtField5" Display="Dynamic" ValidationGroup="GrpForm"><%=languageShared.GetString("MessageMustField")%></asp:RequiredFieldValidator>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlField6" runat="server" Visible="false" class="inputBlock">
            <div class="inputBlockLabel">
                <asp:Literal ID="LitField6" runat="server"></asp:Literal>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtField6" runat="server" MaxLength="100" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RfvField6" runat="server" ControlToValidate="TxtField6" Display="Dynamic" ValidationGroup="GrpForm"><%=languageShared.GetString("MessageMustField")%></asp:RequiredFieldValidator>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlField7" runat="server" Visible="false" class="inputBlock">
            <div class="inputBlockLabel">
                <asp:Literal ID="LitField7" runat="server"></asp:Literal>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtField7" runat="server" MaxLength="100" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RfvField7" runat="server" ControlToValidate="TxtField7" Display="Dynamic" ValidationGroup="GrpForm"><%=languageShared.GetString("MessageMustField")%></asp:RequiredFieldValidator>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlField8" runat="server" Visible="false" class="inputBlock">
            <div class="inputBlockLabel">
                <asp:Literal ID="LitField8" runat="server"></asp:Literal>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtField8" runat="server" MaxLength="100" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RfvField8" runat="server" ControlToValidate="TxtField8" Display="Dynamic" ValidationGroup="GrpForm"><%=languageShared.GetString("MessageMustField")%></asp:RequiredFieldValidator>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlField9" runat="server" Visible="false" class="inputBlock">
            <div class="inputBlockLabel">
                <asp:Literal ID="LitField9" runat="server"></asp:Literal>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="TxtField9" runat="server" MaxLength="100" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RfvField9" runat="server" ControlToValidate="TxtField9" Display="Dynamic" ValidationGroup="GrpForm"><%=languageShared.GetString("MessageMustField")%></asp:RequiredFieldValidator>
            </div>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="PnlAdress" runat="server" Visible="false" class="inputBlock">
        <asp:Panel ID="PnlSex" runat="server" Visible="false" class="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("LableTitle")%>:
            </div>
            <div class="inputBlockContent">
                <asp:DropDownList ID="Sex" runat="server" Width="99%">
                    <asp:ListItem Text="(keine)" Value="-1" />
                    <asp:ListItem Text="Frau" Value="1" />
                    <asp:ListItem Text="Herr" Value="0" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RfvSex" runat="server" ControlToValidate="Sex" Display="Dynamic" Visible="false" Enabled="false" InitialValue="-1" ValidationGroup="GrpForm"><%=languageShared.GetString("MessageMustField")%></asp:RequiredFieldValidator>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlName" runat="server" Visible="false" class="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("Name")%>:
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="Name" runat="server" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RfvName" runat="server" ControlToValidate="Name" Display="Dynamic" Visible="false" Enabled="false" ValidationGroup="GrpForm"><%=languageShared.GetString("MessageMustField")%></asp:RequiredFieldValidator>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlVorname" runat="server" Visible="false" class="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("FirstName")%>:
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="Vorname" runat="server" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RfvVorname" runat="server" ControlToValidate="Vorname" Display="Dynamic" Visible="false" Enabled="false" ValidationGroup="GrpForm"><%=languageShared.GetString("MessageMustField")%></asp:RequiredFieldValidator>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlAddressStreet" runat="server" Visible="false" class="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("Street")%>:
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="AddressStreet" runat="server" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RfvAddressStreet" runat="server" ControlToValidate="AddressStreet" Display="Dynamic" Visible="false" Enabled="false" ValidationGroup="GrpForm"><%=languageShared.GetString("MessageMustField")%></asp:RequiredFieldValidator>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlAddressZip" runat="server" Visible="false" class="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("Zip")%> / <%=languageProfile.GetString("City")%>:
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="AddressZip" MaxLength="10" runat="server" Width="20%"></asp:TextBox>&nbsp;&nbsp;<asp:TextBox ID="AddressCity" runat="server" Width="54%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RfvAddressZip" runat="server" ControlToValidate="AddressZip" Display="Dynamic" Visible="false" Enabled="false" ValidationGroup="GrpForm"><%=languageShared.GetString("MessageMustField")%></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RfvAddressCity" runat="server" ControlToValidate="AddressCity" Display="Dynamic" Visible="false" Enabled="false" ValidationGroup="GrpForm"><%=languageShared.GetString("MessageMustField")%></asp:RequiredFieldValidator>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlEMail" runat="server" Visible="false" class="inputBlock">
            <div class="inputBlockLabel">
                <%=languageShared.GetString("LabelEmail")%>:
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="EMail" runat="server" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RfvEMail" runat="server" ControlToValidate="EMail" Display="Dynamic" Visible="false" Enabled="false" ValidationGroup="GrpForm"><%=languageShared.GetString("MessageMustField")%></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RevEMail" runat="server" ControlToValidate="EMail" Display="Dynamic" Visible="false" Enabled="false" ValidationGroup="GrpForm"><%=languageShared.GetString("MessageEmailInvalidFormat")%></asp:RegularExpressionValidator>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlPhone" runat="server" Visible="false" class="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("Phone")%>:
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="Phone" runat="server" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RfvPhone" runat="server" ControlToValidate="Phone" Display="Dynamic" Visible="false" Enabled="false" ValidationGroup="GrpForm"><%=languageShared.GetString("MessageMustField")%></asp:RequiredFieldValidator>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlMobile" runat="server" Visible="false" class="inputBlock">
            <div class="inputBlockLabel">
                <%=languageProfile.GetString("Mobile")%>:
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="Mobile" runat="server" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RfvMobile" runat="server" ControlToValidate="Mobile" Display="Dynamic" Visible="false" Enabled="false" ValidationGroup="GrpForm"><%=languageShared.GetString("MessageMustField")%></asp:RequiredFieldValidator>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlComment" runat="server" Visible="false" class="inputBlock">
            <div class="inputBlockLabel">
                <%=languageShared.GetString("LableAddressComment")%>:
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="Comment" runat="server" MaxLength="500" TextMode="MultiLine" Rows="4" Width="99%"></asp:TextBox>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlAdressSave" runat="server" Visible="false" class="inputBlock">
            <div class="inputBlockLabel">
                &nbsp;
            </div>
            <div class="inputBlockContent">
                <asp:CheckBox ID="CbxAdressSave" runat="server"/>
            </div>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="PnlSend" runat="server">
        <asp:Panel ID="PnlCheck" runat="server" Visible="false">
            <div class="inputBlock">
                <div class="inputBlockLabel">
                    <%=language.GetString("LableFormCode")%>:
                </div>
                <div class="inputBlockContent">
                    <cc1:CaptchaControl ID="CCCheck" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5" CaptchaHeight="60" CaptchaWidth="200" CaptchaLineNoise="Low" CaptchaMinTimeout="5" CaptchaMaxTimeout="240" />
                </div>
            </div>
            <div class="inputBlock">
                <div class="inputBlockLabel">
                    <%=language.GetString("LableFormEnterCode")%>:
                </div>
                <div class="inputBlockContent">
                    <asp:TextBox ID="FormShieldTextBox" Width="99%" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ControlToValidate="FormShieldTextBox" Display="Dynamic" ErrorMessage='<%=language.GetString("MessageFormCodeMust")%>' />
                    <asp:Literal ID="LitCheck" runat="server" Visible="false" Text='<%=language.GetString("MessageFormCodeWrong")%>'></asp:Literal>
                </div>
            </div>
        </asp:Panel>
        <div class="inputBlock">
            <asp:LinkButton ID="lbtnSend" runat="server" CssClass="CTY-btn-150" OnClick="lbtnSend_Click" ValidationGroup="GrpForm"><%=languageShared.GetString("CommandSend")%></asp:LinkButton>
        </div>
    </asp:Panel>
    <asp:Panel ID="PnlLogin" runat="server" class="inputBlock" Visible="false">
        <asp:HyperLink ID="HplLogin" runat="server"><%=languageShared.GetString("CommandLogin")%></asp:HyperLink>
    </asp:Panel>
</asp:Panel>
<div class="clearBoth">
    <asp:Literal ID="LitUserFeedback" runat="server" Visible="false"/>
</div>
