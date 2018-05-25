<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="_4screen.CSB.WebUI.Admin.Admin" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FullWindow</title>
    <style type="text/css">
    html, body, form
    {
        height: 100%;
        margin: 0px;
        padding: 0px;
        overflow: hidden;
    }
    </style>
</head>
<body>
    <form id="form2" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div id="ParentDivElement" style="height: 100%;">
            <telerik:RadSplitter ID="MainSplitter" runat="server" Height="100%" Width="100%"
                Orientation="Horizontal">
                <telerik:RadPane ID="TopPane" runat="server" Height="100" MinHeight="85" MaxHeight="150" Scrolling="none">
                    <!-- Place the content of the pane here -->
                </telerik:RadPane>
                <telerik:RadPane ID="MainPane" runat="server" Scrolling="none" MinWidth="500">
                    <telerik:RadSplitter ID="NestedSplitter" runat="server" LiveResize="true">
                        <telerik:RadPane ID="LeftPane" runat="server" Width="200" MinWidth="150" MaxWidth="400">
                            <!-- Place the content of the pane here -->
                        </telerik:RadPane>
                        <telerik:RadSplitBar ID="VerticalSplitBar" runat="server" CollapseMode="Forward" />
                        <telerik:RadPane ID="ContentPane" runat="server">
                            <!-- Place the content of the pane here -->
                        </telerik:RadPane>
                    </telerik:RadSplitter>
                </telerik:RadPane>
            </telerik:RadSplitter>
        </div>
    </form>
</body>
</html>