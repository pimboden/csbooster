<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoogleMap.aspx.cs" Inherits="_4screen.CSB.WebUI.GoogleMap" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .map
        {
            float: left;
        }
        .map .RadTreeView img
        {
            height: 20px;
        }
    </style>
    <script type="text/javascript" src="/Library/Scripts/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm" runat="server" />
        <div>
            <asp:PlaceHolder ID="Ph" runat="server" />
        </div>
    </form>
</body>
</html>
