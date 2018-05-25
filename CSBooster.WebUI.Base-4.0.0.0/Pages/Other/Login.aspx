<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="_4screen.CSB.WebUI.Pages.Other.Login" %>

<%@ Register Src="~/UserControls/Templates/Login.ascx" TagName="Login" TagPrefix="csb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <csb:Login ID="Login1" runat="server" />
        </div>
    </form>
</body>
</html>
