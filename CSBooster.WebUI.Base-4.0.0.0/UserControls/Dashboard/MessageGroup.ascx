<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.MessageGroup" Codebehind="MessageGroup.ascx.cs" %>
<div>
   <div class="CSB_box_title">
      <%=language.GetString("LableMessageGroup")%>
   </div>
   <div class="CSB_box">

      <script language="javascript">
        function clean(txtBx)
        {
          txtBx.innerText = ''; 
        }
      </script>

      <asp:Panel ID="pnlGrp" runat="server" />
      <div>
         <asp:TextBox ID="txtAdd" runat="server" Style="margin-top:5px;" Width="97%" MaxLength="25" onfocus="javascript:clean(this);"></asp:TextBox>
         <asp:LinkButton CssClass="inputButton" Style="margin-top:5px;" ID="btnAdd" runat="server" OnClick="btnAdd_Click" EnableViewState="false"><%=language.GetString("CommandMessageGroupAdd")%></asp:LinkButton>
      </div>
   </div>
</div>
