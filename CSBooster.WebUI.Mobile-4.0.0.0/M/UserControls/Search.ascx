<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Search.ascx.cs" Inherits="_4screen.CSB.WebUI.M.UserControls.Search" %>

<script type="text/javascript">
    function RedirectOnClick(url, txtBoxId) {
        if (document.getElementById(txtBoxId).value.length > 0) {
            if (document.getElementById(txtBoxId).value.indexOf('*') > -1 &&
                document.getElementById(txtBoxId).value.indexOf('*') == document.getElementById(txtBoxId).value.length - 1)
                window.location = url + document.getElementById(txtBoxId).value;
            else
                window.location = url + document.getElementById(txtBoxId).value + '*';
        }
    }

    function RedirectOnEnterKey(e, url, txtBoxId) {
        if (!e) var e = window.event;
        if (e.keyCode) code = e.keyCode;
        else if (e.which) code = e.which;
        if (parseInt(code) == 13 && document.getElementById(txtBoxId).value.length > 0) {
            if (e.stopPropagation) {
                e.stopPropagation();
                e.preventDefault();
            } else {
                e.cancelBubble = true;
                e.returnValue = false;
            }
            if (document.getElementById(txtBoxId).value.indexOf('*') > -1 &&
                document.getElementById(txtBoxId).value.indexOf('*') == document.getElementById(txtBoxId).value.length - 1)
                window.location = url + document.getElementById(txtBoxId).value;
            else
                window.location = url + document.getElementById(txtBoxId).value + '*';
        }
    }
</script>

<div class="headerSearchBox">
    <div class="headerSearchTitle">
        <%=language.GetString("TitleSearch") %>
    </div>
    <asp:TextBox ID="txtSearch" runat="server" />
    <asp:HyperLink runat="server" CssClass="headerSearchButton" NavigateUrl="javascript:void(0);" ID="hlkSearh" />
    <div class="clearBoth">
    </div>
</div>
