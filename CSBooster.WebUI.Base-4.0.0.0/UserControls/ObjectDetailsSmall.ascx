<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.ObjectDetailsSmall" CodeBehind="ObjectDetailsSmall.ascx.cs" %>
<div>
    <div class="CSB_box_title">
        <%=language.GetString("TitleDetail")%>
    </div>
    <div class="CSB_box">
        <asp:Literal ID="LitCnt" EnableViewState="false" runat="server"></asp:Literal>
    </div>
</div>
