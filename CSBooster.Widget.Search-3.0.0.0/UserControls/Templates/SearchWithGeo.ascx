<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.Widget.UserControls.Templates.SearchWithGeo" CodeBehind="SearchWithGeo.cs" %>
<script type="text/javascript" src="http://dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.1&mkt=de-de"></script>

<script type="text/javascript">
    var map;
    var mapLocationElementId;
    var mapCountryElementId;
    var mapLongLatElementId;
    var mapFindPostbackId;

    function LoadVEMap() {
        map = new VEMap('myMap');
        map.LoadMap();
        map.SetMapStyle(VEMapStyle.Road);
    }

    function DoMapFindOnEnterKey(e, locationElementId, countryElementId, longLatElementId, postbackId) {
        if (!e) var e = window.event;
        if (e.keyCode) code = e.keyCode;
        else if (e.which) code = e.which;
        if (parseInt(code) == 13) {
            DoFind(locationElementId, countryElementId, longLatElementId, postbackId)
            return false;
        }
    }

    function DoFind(locationElementId, countryElementId, longLatElementId, postbackId) {
        try {
            mapLocationElementId = locationElementId;
            mapCountryElementId = countryElementId;
            mapLongLatElementId = longLatElementId;
            mapFindPostbackId = postbackId;

            if (document.getElementById(locationElementId) != null && document.getElementById(countryElementId) != null) {
                var location = document.getElementById(locationElementId).value;
                var country = document.getElementById(countryElementId).options[document.getElementById(countryElementId).selectedIndex].text;
                if (location != "") {
                    if (location.indexOf(",") == -1 && country == "Schweiz")
                        location = location + ", " + location;

                    if (map == null) {
                        LoadVEMap();
                    }

                    map.Find(null, location + ", " + country, null, null, null, null, true, true, false, true, ProcessResults);
                }
                else {
                    __doPostBack(mapFindPostbackId, '');
                }
            }
            else {
                __doPostBack(mapFindPostbackId, '');
            }
        }
        catch (ex) {
        }
    }

    function ProcessResults(layers, findResults, places, hasMoreResults, errorMessage) {
        if (places != null) {
            var showAmbiguous = true;
            var trueResults = 0;
            var placesArray = new Array();
            for (var r = 0; r < places.length; r++) {
                if (places[r].Name != null && places[r].Name.indexOf("(") == -1) {
                    if (places[r].MatchConfidence <= VEMatchConfidence.Medium) {
                        placesArray.push(places[r]);
                        trueResults++;
                        showAmbiguous = false;
                    }
                    if (showAmbiguous == true) {
                        placesArray.push(places[r]);
                    }
                }
            }
            if (placesArray.length > 1) {
                var content = "";
                for (var r = 0; r < placesArray.length; r++) {
                    content += "<a href=\"javascript:void(0);\" onClick=\"SaveAndExecuteSearch('ambiguousResults', '" + places[r].LatLong + "', '" + places[r].Name + "')\">" + places[r].Name + "</a><br/>";
                }
                SetPopupWindow("ambiguousResults", 400, 525, 500, "Mehrere Orte gefunden", content, false);
            }
            else if (placesArray.length == 1) {
                SaveSearch(places[0].LatLong, places[0].Name);
                __doPostBack(mapFindPostbackId, '');
            }
            else {
                content = "Ort nicht gefunden!";
                SetPopupWindow("ambiguousResults", 400, 525, 500, "Ort nicht gefunden", content, false);
            }
        }
        else {
            content = "Ort nicht gefunden!";
            SetPopupWindow("ambiguousResults", 400, 525, 500, "Ort nicht gefunden", content, false);
        }
    }

    function SaveAndExecuteSearch(popupId, longLat, locationName) {
        document.getElementById(popupId).style.display = "none";
        SaveSearch(longLat, locationName);

        __doPostBack(mapFindPostbackId, '');
    }

    function SaveSearch(longLat, locationName) {
        document.getElementById(mapLongLatElementId).value = longLat;

        var country = document.getElementById(mapCountryElementId).options[document.getElementById(mapCountryElementId).selectedIndex].text;
        var countrySuffix = locationName.indexOf(", " + country, 0);
        if (countrySuffix > 0)
            document.getElementById(mapLocationElementId).value = locationName.toString().substring(0, countrySuffix);
        else
            document.getElementById(mapLocationElementId).value = locationName.toString();
    }
</script>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td colspan="2" height="20"><%=language.GetString("LableSearchSearch")%></td>
    </tr>
    <tr>
        <td colspan="2"><asp:TextBox ID="TxtSearch" runat="server" Width="97%" /></td>
    </tr>
    <tr>
        <td height="20"><%=language.GetString("LableSearchCity")%></td>
        <td><%=language.GetString("LableSearchNear")%></td>
    </tr>
    <tr>
        <td width="70%"><asp:TextBox ID="TxtLoc" Width="95%" runat="server" /></td>
        <td>
            <asp:DropDownList ID="DDRadius" EnableViewState="true" Width="100%" runat="server">
                <asp:ListItem Value="1" Text="<1 km" />
                <asp:ListItem Value="5" Text="5 km" Selected="True" />
                <asp:ListItem Value="10" Text="10 km" />
                <asp:ListItem Value="25" Text="25 km" />
                <asp:ListItem Value="50" Text="50 km" />
                <asp:ListItem Value="100" Text="100 km" />
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="2" height="20"><%=language.GetString("LableSearchCountry")%></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:DropDownList ID="DDCountry" runat="server" Width="100%" />
        </td>
    </tr>
    <tr>
        <td colspan="2" align="right" valign="bottom" height="26">
            <asp:HiddenField ID="HidCoords" runat="server" />
            <asp:LinkButton ID="LbtnFind" runat="server" Text="" />
            <div style="float: right;">
                <asp:HyperLink ID="LnkFind" Style="margin-top: 5px; margin-left: 5px;" CssClass="CSB_btn_150" runat="server" NavigateUrl="javascript:void(0);"><%=languageShared.GetString("CommandSearch")%></asp:HyperLink>
            </div>
            <div style="float: right;">
                <asp:HyperLink ID="LnkReset" Style="margin-top: 5px; margin-left: 5px;" runat="server"><%=languageShared.GetString("CommandReset")%></asp:HyperLink>
            </div>
        </td>
    </tr>
</table>
