<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.Widget.UserControls.Templates.SearchWithoutGeo" CodeBehind="SearchWithoutGeo.cs" %>

<script type="text/javascript">
    var mapFindPostbackId;

    function DoFindOnEnterKey(e, postbackId) {
        if (!e) var e = window.event;
        if (e.keyCode) code = e.keyCode;
        else if (e.which) code = e.which;
        if (parseInt(code) == 13) {
            DoFind(postbackId)
            return false;
        }
    }

    function DoFind(postbackId) {
        try {
            mapFindPostbackId = postbackId;
            __doPostBack(mapFindPostbackId, '');
        }
        catch (ex) {
        }
    }

</script>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td colspan="2" height="20"><%=language.GetString("LableSearchSearch")%></td>
    </tr>
    <tr>
        <td width="70%"><asp:TextBox ID="TxtSearch" runat="server" Width="97%" /></td>
        <td align="right" valign="bottom" height="26">
            <asp:LinkButton ID="LbtnFind" runat="server" Text="" />
            <div style="float: right;">
                <asp:HyperLink ID="LnkFind" Style="margin-top: 5px; margin-left: 5px;" CssClass="CSB_btn_150" runat="server" NavigateUrl="javascript:void(0);"><%=languageShared.GetString("CommandSearch")%></asp:HyperLink>
            </div>
        </td>
    </tr>
</table>
