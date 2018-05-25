<%@ Page Language="C#" MasterPageFile="~/MasterPages/Empty.master" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.Pages.Popups.PlaceOnMap" CodeBehind="PlaceOnMap.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cnt1" runat="Server">
    <div id="popup" style="width: 640px;">
        <script type="text/javascript">
            AttachEvent(window, "load", initialize);
            var map = null;
            var geocoder = null;
            var currentMarker = null;

            function initialize() {
                currentMarker = new google.maps.Marker();
                geocoder = new google.maps.Geocoder();
                var mapOptions = {
                    mapTypeId: google.maps.MapTypeId.SATELLITE,
                    style: google.maps.NavigationControlStyle.DEFAULT
                }
                map = new google.maps.Map(document.getElementById("map"), mapOptions);
                google.maps.event.addListener(map, 'click', setPosition);
                if (document.getElementById('<%=txtLong.ClientID %>').value == '') {
                    map.setZoom(8);
                    map.setCenter(new google.maps.LatLng(47.05, 8.3));
                }
                else {
                    currentMarker.setMap(map);
                    currentMarker.setPosition(new google.maps.LatLng(document.getElementById('<%=txtLat.ClientID %>').value, document.getElementById('<%=txtLong.ClientID %>').value));
                    map.setZoom(14);
                    map.setCenter(currentMarker.position);
                }
            }

            function setPosition(event) {
                currentMarker.setMap(map);
                currentMarker.setPosition(event.latLng);
                geocoder.geocode({ 'latLng': event.latLng, 'language': '<%=_4screen.Utils.Web.CultureHandler.GetCurrentNeutralLanguageCode()%>' }, function(results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        showAddressInformation(results[0].address_components, event.latLng);
                    } else {
                        alert("Geocoding failed: " + status);
                    }
                });
            }

            function showAddressOnEnterKey(e, address) {
                if (!e) var e = window.event;
                if (e.keyCode) code = e.keyCode;
                else if (e.which) code = e.which;
                if (parseInt(code) == 13) {
                    showAddress(address);
                    return false;
                }
            }

            function showAddress(address) {
                geocoder.geocode({ 'address': address, 'language': '<%=_4screen.Utils.Web.CultureHandler.GetCurrentNeutralLanguageCode()%>', 'region': 'CH' }, function(results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        map.setCenter(results[0].geometry.location);
                        map.setZoom(14);
                        currentMarker.setMap(map);
                        currentMarker.setPosition(results[0].geometry.location);
                        showAddressInformation(results[0].address_components, results[0].geometry.location);
                    } else {
                        alert("Geocoding failed: " + status);
                    }
                });
            }

            function showAddressInformation(components, location) {
                var street = null;
                var streetNumber = null;
                var zipCode = null;
                var city = null;
                var countryCode = null;
                for (var c in components) {
                    for (var t in components[c].types) {
                        if (components[c].types[t] == "route") {
                            street = components[c].long_name;
                        }
                        if (components[c].types[t] == "street_number") {
                            streetNumber = components[c].long_name;
                        }
                        if (components[c].types[t] == "postal_code") {
                            zipCode = components[c].long_name;
                        }
                        if (components[c].types[t] == "locality") {
                            city = components[c].long_name;
                        }
                        if (components[c].types[t] == "country") {
                            countryCode = components[c].short_name;
                        }
                    }
                }
                document.getElementById('<%=txtLong.ClientID %>').value = location.lng();
                document.getElementById('<%=txtLat.ClientID %>').value = location.lat();
                document.getElementById('<%=txtLongH.ClientID %>').value = location.lng();
                document.getElementById('<%=txtLatH.ClientID %>').value = location.lat();
                document.getElementById('<%=ddlLand.ClientID %>').options[0].selected = true;
                document.getElementById('<%=txtStreet.ClientID %>').value = "";
                document.getElementById('<%=txtOrt.ClientID %>').value = "";
                document.getElementById('<%=txtPlz.ClientID %>').value = "";
                if (street != null) {
                    if (streetNumber != null) {
                        street += " " + streetNumber;
                    }
                    document.getElementById('<%=txtStreet.ClientID %>').value = street;
                }
                if (zipCode != null) {
                    document.getElementById('<%=txtPlz.ClientID %>').value = zipCode;
                }
                if (city != null) {
                    document.getElementById('<%=txtOrt.ClientID %>').value = city;
                }
                if (countryCode != null) {
                    var countryDrowDown = document.getElementById('<%=ddlLand.ClientID %>');
                    for (var i = 0; i < countryDrowDown.options.length; i++) {
                        if (countryDrowDown.options[i].value == countryCode)
                            countryDrowDown.options[i].selected = true;
                    }
                }
            }
        </script>
        <div class="inputBlock">
            <div style="float: left; height: 22px; line-height: 22px;">
                <web:LabelControl ID="LableMapAdress" runat="server" LanguageFile="Pages.Popups.WebUI.Base" LabelKey="LableMapAdress" TooltipKey="TooltipMapAdress" />
            </div>
            <div style="float: left; margin-left: 5px;">
                <input type="text" size="60" id="address" onkeypress="return showAddressOnEnterKey(event, document.getElementById('address').value);" value="" style="width: 400px; height: 17px;" />
            </div>
            <div style="float: left; margin-left: 5px;">
                <asp:HyperLink ID="HyperLink1" CssClass="inputButton" NavigateUrl="javascript:void(0)" onclick="showAddress(document.getElementById('address').value);" runat="server">
                    <web:TextControl ID="CommandSearch" runat="server" LanguageFile="Shared" TextKey="CommandSearch" />
                </asp:HyperLink>
            </div>
        </div>
        <div class="inputBlock">
            <div id="map" style="width: 640px; height: 300px">
            </div>
        </div>
        <div class="inputBlock">
            <web:TextControl ID="TextMapUsage" runat="server" LanguageFile="Pages.Popups.WebUI.Base" TextKey="TextMapUsage" AllowHtml="true" />
        </div>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LabelMapLatLong" runat="server" LanguageFile="Pages.Popups.WebUI.Base" LabelKey="LableMapLatLong" TooltipKey="TooltipMapLatLong" />
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="txtLong" runat="server" Width="100" ReadOnly="true" Enabled="false" />&nbsp;<asp:TextBox ID="txtLat" runat="server" Width="100" ReadOnly="true" Enabled="false" />
            </div>
            <div class="inputBlockError">
                <asp:RequiredFieldValidator ID="Rfv" runat="server" CssClass="inputErrorTooltip" ControlToValidate="txtLat" ValidationGroup="ValMap" Display="Dynamic"><%=language.GetString("MessageMapMustSelect")%></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LableMapStreet" runat="server" LanguageFile="Pages.Popups.WebUI.Base" LabelKey="LableMapStreet" TooltipKey="TooltipMapStreet" />
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="txtStreet" runat="server" Width="200"></asp:TextBox>
            </div>
        </div>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LableMapZIPCity" runat="server" LanguageFile="Pages.Popups.WebUI.Base" LabelKey="LableMapZIPCity" TooltipKey="TooltipMapZIPCity" />
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="txtPlz" runat="server" Width="60"></asp:TextBox> <asp:TextBox ID="txtOrt" runat="server" Width="140"></asp:TextBox>
            </div>
        </div>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="LableMapLand" runat="server" LanguageFile="Pages.Popups.WebUI.Base" LabelKey="LableMapLand" TooltipKey="TooltipMapLand" />
            </div>
            <div class="inputBlockContent">
                <asp:DropDownList ID="ddlLand" runat="server" Width="200" />
            </div>
            <div class="inputBlockError">
                <asp:RequiredFieldValidator ID="RfvLand" runat="server" ControlToValidate="ddlLand" InitialValue="--" ValidationGroup="ValMap" Display="Dynamic"><%=language.GetString("MessageMapLandMust")%></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="inputBlock">
            <div class="inputBlockContent">
                <asp:LinkButton ID="ibtnS" CssClass="inputButton" OnClick="ibtnS_Click" ValidationGroup="ValMap" runat="server">
                    <web:TextControl ID="CommandApply" runat="server" LanguageFile="Shared" TextKey="CommandApply" />
                </asp:LinkButton>
                <a href="javascript:void(0)" class="inputButtonSecondary" onclick="GetRadWindow().Close();">
                    <web:TextControl ID="CommandCancel" runat="server" LanguageFile="Shared" TextKey="CommandCancel" />
                </a>
            </div>
            <asp:HiddenField ID="txtLongH" runat="server" />
            <asp:HiddenField ID="txtLatH" runat="server" />
            <asp:Literal ID="litScript" runat="server" />
        </div>
    </div>
</asp:Content>
