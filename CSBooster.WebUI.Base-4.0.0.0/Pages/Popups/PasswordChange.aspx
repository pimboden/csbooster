<%@ Page Language="C#" MasterPageFile="~/MasterPages/Empty.master" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.Pages.Popups.PasswordChange" CodeBehind="PasswordChange.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cnt1" runat="Server">
    <div id="popup" style="width: 500px;">
        <asp:Panel ID="pnlLoggedIn" runat="server">
            <asp:ChangePassword ID="ChangePassword" runat="server" Width="100%">
                <ChangePasswordTemplate>
                    <div class="inputBlock">
                        <div class="inputBlockLabel">
                            <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword"><%=language.GetString("LablePassword")%></asp:Label>
                        </div>
                        <div class="inputBlockContent">
                            <asp:TextBox ID="CurrentPassword" Width="99%" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="inputBlockError">
                            <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" CssClass="inputErrorTooltip" ControlToValidate="CurrentPassword" ValidationGroup="ChangePassword" Display="Dynamic">
                            <%=language.GetString("MessageCurrentPasswordRequired")%>
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="inputBlock">
                        <div class="inputBlockLabel">
                            <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword"><%=language.GetString("LablePasswordNew")%></asp:Label>
                        </div>
                        <div class="inputBlockContent">
                            <asp:TextBox ID="NewPassword" Width="99%" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="inputBlockError">
                            <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" CssClass="inputErrorTooltip" ControlToValidate="NewPassword" ValidationGroup="ChangePassword" Display="Dynamic">
                            <%=language.GetString("MessageNewPasswordRequired")%>
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="inputBlock">
                        <div class="inputBlockLabel">
                            <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword"><%=language.GetString("LablePasswordRepeat")%></asp:Label>
                        </div>
                        <div class="inputBlockContent">
                            <asp:TextBox ID="ConfirmNewPassword" Width="99%" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="inputBlockError">
                            <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" CssClass="inputErrorTooltip" ControlToValidate="ConfirmNewPassword" ValidationGroup="ChangePassword" Display="Dynamic">
                            <%=language.GetString("MessageConfirmNewPasswordRequired")%>
                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="NewPasswordCompare" runat="server" CssClass="inputErrorTooltip" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" Display="Dynamic" ValidationGroup="ChangePassword">
                            <%=language.GetString("MessageNewPasswordCompare")%>
                            </asp:CompareValidator>
                        </div>
                    </div>
                    <div class="inputBlock">
                        <div class="inputBlockContent errorText">
                            <asp:Literal ID="FailureText" runat="server" />
                        </div>
                    </div>
                    <div class="inputBlock">
                        <div class="inputBlockContent">
                            <asp:LinkButton ID="saveButton" CssClass="inputButton" CommandName="ChangePassword" ValidationGroup="ChangePassword" runat="server">
                                <web:TextControl ID="CommandSave" runat="server" LanguageFile="Shared" TextKey="CommandSave" />
                            </asp:LinkButton>
                            <a href="javascript:GetRadWindow().Close();" class="inputButtonSecondary">
                                <web:TextControl ID="CommandCancel" runat="server" LanguageFile="Shared" TextKey="CommandCancel" />
                            </a>
                        </div>
                    </div>
                </ChangePasswordTemplate>
                <SuccessTemplate>
                    <div class="inputBlock">
                        <web:TextControl ID="TextPasswordChanged" runat="server" LanguageFile="Pages.Popups.WebUI.Base" TextKey="TextPasswordChanged" />
                    </div>
                    <div class="inputBlock">
                        <a href="javascript:GetRadWindow().Close();" class="inputButton">
                            <web:TextControl ID="CommandClose" runat="server" LanguageFile="Shared" TextKey="CommandClose" />
                        </a>
                    </div>
                </SuccessTemplate>
            </asp:ChangePassword>
        </asp:Panel>
    </div>
</asp:Content>
