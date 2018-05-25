<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LanguageChanger.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.LanguageChanger1" %>
<div id="languageBar">
    <ul>
        <asp:Repeater ID="repLang" runat="server">
            <ItemTemplate>
                <li class="<%#GetClass(Eval("Key"))%>"><a href="<%#GetLink(Eval("Key")) %>">
                    <%#GetNeutralCode(Eval("Key"))%>
                </a></li>
            </ItemTemplate>
            <SeparatorTemplate>
                <li class="languageGap">|</li>
            </SeparatorTemplate>
        </asp:Repeater>
    </ul>
</div>
