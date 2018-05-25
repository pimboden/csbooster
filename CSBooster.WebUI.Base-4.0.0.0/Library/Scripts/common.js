function OnWidgetDrop(sender, e) {
    var container = e.get_container();
    var item = e.get_droppedItem();
    var rowNo = e.get_position();
    var columnNo = parseInt(container.getAttribute("columnNo"));

    if (item.getAttribute("IsTemplate") != null && item.getAttribute("IsTemplate") == "true") {
        var pageId = container.getAttribute("PageId");

        if (item.getAttribute("ObjectId") != null) {
            var objectId = item.getAttribute("ObjectId");
            var objectType = item.getAttribute("ObjectType");
            _4screen.CSB.WebServices.WidgetService.AddWidgetInstanceByObjectType(objectId, objectType, pageId, columnNo, rowNo, OnWidgetDropSuccess, OnWidgetDropFail);
        }
        else {
            var widgetId = item.getAttribute("InstanceId");
            _4screen.CSB.WebServices.WidgetService.AddWidgetInstance(widgetId, pageId, columnNo, rowNo, OnWidgetDropSuccess, OnWidgetDropFail);
        }
    }
    else {
        var instanceId = item.getAttribute("InstanceId");
        _4screen.CSB.WebServices.WidgetService.MoveWidgetInstance(instanceId, columnNo, rowNo, OnWidgetDropSuccess, OnWidgetDropFail);
    }
}

function OnWidgetDropSuccess(result) {
    __doPostBack('ctl00$Cnt$WCUP', '');
}

function OnWidgetDropFail(error) {
    alert("Widget drop failed!");
}

function OnMapNavigationClick(sender, eventArgs) {
    var node = eventArgs.get_node();
    if (node.get_checked()) {
        node.uncheck();
    }
    else {
        node.check();
    }
    node.set_selected(false);
    OnMapNavigationChange();
}

function OnMapNavigationChange() {
    var objectTypesAndTags = new Object();

    for (var i = 0; i < mapTree.get_nodes().get_count(); i++) {
        var objectNode = mapTree.get_nodes().getNode(i);

        var tagsSelected = 0;
        var tags = "";
        for (var j = 0; j < objectNode.get_nodes().get_count(); j++) {
            var tagNode = objectNode.get_nodes().getNode(j);

            if (tagNode.get_checked()) {
                tags += tagNode.get_value() + ",";
                tagsSelected++;
            }
        }
        /*if (tagsSelected == objectNode.get_nodes().get_count()) {
        tags = "";
        }*/

        if (objectNode.get_checked() || tags.length > 0) {
            objectTypesAndTags[objectNode.get_value()] = tags.substring(0, tags.lastIndexOf(","));
        }
    }

    _4screen.CSB.WebServices.GoogleMapServices.LoadMarkers(objectTypesAndTags, OnLoadMarkersSuccess, OnLoadMarkersFail);
}

function OnLoadMarkersSuccess(result) {
    for (var i in mapMarkers) {
        mapMarkers[i].setMap(null);
    }
    eval(result);
}

function OnLoadMarkersFail(error) {
}

function AddMapMarker(latitude, longitude, tag, objectId, objectType) {
    if (mapMarkers[objectId] == null) {
        var mapMarker;
        if (mapMarkerOptions[objectType + tag] != null)
            mapMarker = new google.maps.Marker({ position: new google.maps.LatLng(latitude, longitude), icon: mapMarkerOptions[objectType + tag].icon, shadow: mapMarkerOptions['0'].shadow });
        else
            mapMarker = new google.maps.Marker({ position: new google.maps.LatLng(latitude, longitude), icon: mapMarkerOptions['0'].icon, shadow: mapMarkerOptions['0'].shadow });
        google.maps.event.addListener(mapMarker, "click", function() { LoadMarkerDetails(objectId, latitude, longitude); });
        mapMarkers[objectId] = mapMarker;
    }
    mapMarkers[objectId].setMap(map);
}

function LoadMarkerDetails(objectId, latitude, longitude) {
    _4screen.CSB.WebServices.GoogleMapServices.LoadMarkerDetails(objectId, latitude, longitude, mapObjectTypes, OnLoadMarkerDetailsSuccess, OnLoadMarkerDetailsFail);
}

function OnLoadMarkerDetailsSuccess(result) {
    mapInfoWindow.setContent(result.Content);
    mapInfoWindow.open(map, mapMarkers[result.ObjectId]);
}

function OnLoadMarkerDetailsFail(error) {
    alert("Loading marker content failed!");
}

function initializeObjectRelator() {
    $('#dropContainer').sortable({
        placeholder: 'ui-state-placeholder',
        deactivate: function(event, ui) {
            addObjectRelation(ui.item);
        }
    }).disableSelection();

    $("li.draggable").draggable({
        revert: 'invalid',
        helper: 'clone',
        connectToSortable: '#dropContainer',
        start: function(event, ui) {
            $('#dropContainer').addClass('ui-state-active');
        },
        stop: function(event, ui) {
            $('#dropContainer').removeClass('ui-state-active');
        }
    });

    $('#dropContainer > li').children('a').click(function(ev) {
        removeObjectRelation(ev);
    });
}

function addObjectRelation($item) {
    if ($item.parent().attr('id') == "dropContainer") {
        $item.children('a').remove();
        $item.children('input').remove();
        $item.removeClass('draggable');
        var removeIcon = '<a href="javascript:void(0)" class="ui-icon-remove"></a>';
        var hiddenField = '<input type="hidden" name="relatedObjectId" value="' + $item.attr('objectid') + '" />';
        $item.append(removeIcon).append(hiddenField);
        $item.children('a').click(function(ev) {
            removeObjectRelation(ev);
        });
    }
}

function removeObjectRelation($triggerItem) {
    $($triggerItem.currentTarget).parent().remove();
}

function setQMCookie(name, widgetInstanceId) {
    var params = document.getElementById(widgetInstanceId).innerHTML.replace(/"/g, "&quot;");
    setCookie(name, params, 1);
}

function $hide(id) {
    document.getElementById(id).style.display = "none";
}

function dump(arr, level) {
    var dumped_text = "";
    if (!level) level = 0;

    //The padding given at the beginning of the line.
    var level_padding = "";
    for (var j = 0; j < level + 1; j++) level_padding += "    ";

    if (typeof (arr) == 'object') { //Array/Hashes/Objects 
        for (var item in arr) {
            var value = arr[item];

            if (typeof (value) == 'object') { //If it is an array,
                dumped_text += level_padding + "'" + item + "' ...\n";
                dumped_text += dump(value, level + 1);
            } else {
                dumped_text += level_padding + "'" + item + "' => \"" + value + "\"\n";
            }
        }
    } else { //Stings/Chars/Numbers etc.
        dumped_text = "===>" + arr + "<===(" + typeof (arr) + ")";
    }
    return dumped_text;
}

var VideoDiv;
function DisableVideoDiv() {
    if (VideoDiv != undefined) {
        VideoDiv.style.visibility = 'hidden';
        VideoDiv.style.display = 'none';
    }
}
function EnableVideoDiv() {
    if (VideoDiv != 'undefined') {
        VideoDiv.style.visibility = 'visible';
        VideoDiv.style.display = 'inline';
    }
}

function DisableEnterKey(e) {
    var key;
    if (window.event)
        key = window.event.keyCode;
    else
        key = e.which;

    return (key != 13);
}

function RedirectOnClick(url, txtBoxId) {
    if (document.getElementById(txtBoxId).value.length > 0) {
        window.location = url + document.getElementById(txtBoxId).value;
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
        window.location = url + document.getElementById(txtBoxId).value;
    }
}

function DoPostbackOnEnterKey(e, postbackId) {
    if (!e) var e = window.event;
    if (e.keyCode) code = e.keyCode;
    else if (e.which) code = e.which;
    if (parseInt(code) == 13) {
        __doPostBack(postbackId, '');
        return false;
    }
}

function DoLoginOnEnterKey(e, postbackId) {
    if (!e) var e = window.event;
    if (e.keyCode) code = e.keyCode;
    else if (e.which) code = e.which;
    if (parseInt(code) == 13) {
        if (document.getElementById('SecXmlToken') != null) {
            RemoveNodeByID('SecXmlToken');
        }
        __doPostBack(postbackId, '');
        return false;
    }
}

function SelectReceiver(userId, nickname) {
    var receiverList = document.getElementById("receiverList");
    var receivers = document.getElementsByName("receiver");
    var receiverAlreadyAdded = false;
    for (var i = 0; i < receivers.length; i++) {
        if (receivers[i].value.toLowerCase() == userId.toLowerCase())
            receiverAlreadyAdded = true;
    }

    var receiverTextBox = document.getElementById("receiverTextBox");

    if (!receiverAlreadyAdded) {
        document.getElementById("receiverSuggestions").style.display = "none";
        var receiver = document.createElement('div');
        receiver.setAttribute("class", "receiver");
        receiver.setAttribute("className", "receiver");
        receiver.innerHTML = "<span>" + nickname + "</span><input type='hidden' name='receiver' value='" + userId + "' /><a href='javascript:void(0)' onClick='RemoveReceiver(this)'></a>";
        receiverList.insertBefore(receiver, receiverTextBox);
        GetRadWindow().autoSize();
    }

    var input = receiverTextBox.getElementsByTagName("input")[0];
    input.value = "";
    input.focus();
}

function RemoveReceiver(removeButton) {
    var receiverList = document.getElementById("receiverList");
    receiverList.removeChild(removeButton.parentNode);
}

function ShowReceiverSuggestions(e, postbackId) {
    if (!e) var e = window.event;
    if (e.keyCode) code = e.keyCode;
    else if (e.which) code = e.which;

    if (parseInt(code) != 13 && parseInt(code) != 9) {
        document.getElementById("receiverSuggestions").style.display = "";
        __doPostBack(postbackId, '');
    }
}

function AddEmailReceiver(e, postbackId) {
    if (!e) var e = window.event;
    if (e.keyCode) code = e.keyCode;
    else if (e.which) code = e.which;

    var input = document.getElementById("receiverTextBox").getElementsByTagName("input")[0];

    if (parseInt(code) == 13 || parseInt(code) == 9) {
        var text = input.value;
        var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
        if (emailPattern.test(text)) {
            SelectReceiver(text, text);
        }
        else {
            input.value = "";
        }
    }
}

function SelectTag(tagId, tagName) {
    var tagList = document.getElementById("tagHintsList");
    var tags = document.getElementsByName("tagHint");
    var tagAlreadyAdded = false;
    for (var i = 0; i < tags.length; i++) {
        if (tags[i].value.toLowerCase() == tagId.toLowerCase())
            tagAlreadyAdded = true;
    }

    var tagHintsTextBox = document.getElementById("tagHintsTextBox");

    if (!tagAlreadyAdded) {
        document.getElementById("tagHints").style.display = "none";
        var tagHint = document.createElement('div');
        tagHint.setAttribute("class", "tagHint");
        tagHint.setAttribute("className", "tagHint");
        tagHint.innerHTML = "<span>" + tagName + "</span><input type='hidden' name='tagHint' value='" + tagId + "' /><a href='javascript:void(0)' onClick='RemoveTag(this)'></a>";
        tagList.insertBefore(tagHint, tagHintsTextBox);
        GetRadWindow().autoSize();
    }

    var input = tagHintsTextBox.getElementsByTagName("input")[0];
    input.value = "";
    input.focus();
}

function RemoveTag(removeButton) {
    var tagList = document.getElementById("tagHintsList");
    tagList.removeChild(removeButton.parentNode);
}

function ShowTagSuggestions(e, postbackId) {
    if (!e) var e = window.event;
    if (e.keyCode) code = e.keyCode;
    else if (e.which) code = e.which;

    if (parseInt(code) != 13 && parseInt(code) != 9) {
        document.getElementById("tagHints").style.display = "";
        __doPostBack(postbackId, '');
    }
}

function AddTag(e, postbackId) {
    if (!e) var e = window.event;
    if (e.keyCode) code = e.keyCode;
    else if (e.which) code = e.which;

    var input = document.getElementById("tagHintsTextBox").getElementsByTagName("input")[0];

    if (parseInt(code) == 13 || parseInt(code) == 9) {
        var text = input.value;
        if (text.length > 0) {
            SelectTag(text, text);
        }
    }
}

function RemoveNodeByID(nodeID) {
    var node = document.getElementById(nodeID);
    if (node != null && node.parentNode != null) {
        node.parentNode.removeChild(node);
    }
}

function DeleteDataObject(url) {
    if (confirm(unescape("Soll dieser Inhalt wirklich gel%F6scht werden?"))) {
        radWinOpen(url, unescape("Inhalt l%F6schen"), 300, 150, false, 'RefreshMyContent')
    }

    return false;
}

function SelectTextBox(textboxId) {
    document.getElementById(textboxId).focus();
    document.getElementById(textboxId).select();
}

function SelectAll(checkbox, childName) {
    if (checkbox.checked == true) {
        var i;
        for (i = 0; i < document.forms[0].elements.length; i++)
            if ((document.forms[0].elements[i].type == 'checkbox') && (document.forms[0].elements[i].name.indexOf(childName) > -1))
            document.forms[0].elements[i].checked = true;
    }
    else {
        var i;
        for (i = 0; i < document.forms[0].elements.length; i++)
            if ((document.forms[0].elements[i].type == 'checkbox') && (document.forms[0].elements[i].name.indexOf(childName) > -1))
            document.forms[0].elements[i].checked = false;
    }
}

function SelectElement(newSelectedElementId, currentSelectedElementId) {
    var currentSelectedElement = document.getElementById(currentSelectedElementId);
    if (currentSelectedElement.value.length > 0 && document.getElementById(currentSelectedElement.value) != null)
        document.getElementById(currentSelectedElement.value).className = "itemNotSelected";

    currentSelectedElement.value = newSelectedElementId;
    document.getElementById(newSelectedElementId).className = "itemSelected";
}

Array.prototype.contains = function(element) {
    for (var i = 0; i < this.length; i++) {
        if (this[i].toLowerCase() == element.toLowerCase()) {
            return true;
        }
    }
    return false;
};

Array.prototype.remove = function(subject) {
    for (var i = 0; i < this.length; i++) {
        if (this[i].toLowerCase() == subject.toLowerCase())
            this.splice(i, 1);
    }
};

function UpdateTagsTextbox(checkbox, textboxId, tags) {
    var textbox = document.getElementById(textboxId);

    tags = tags.toLowerCase();
    var tagsToAddOrRemove = new Array();
    tagsToAddOrRemove = tags.split(";");
    var currentTags = new Array();
    if (textbox.value.length > 0) {
        currentTags = textbox.value.split(";");
    }

    for (var i = 0; i < tagsToAddOrRemove.length; i++) {
        if (checkbox.checked) {
            checkbox.parentNode.className = "CSB_cb_selected";
            if (!currentTags.contains(tagsToAddOrRemove[i])) {
                currentTags.push(tagsToAddOrRemove[i]);
            }
        }
        else {
            checkbox.parentNode.className = "CSB_cb_unselected";
            if (currentTags.contains(tagsToAddOrRemove[i])) {
                currentTags.remove(tagsToAddOrRemove[i]);
            }
        }
    }

    textbox.value = "";
    currentTags.sort();
    for (var i = 0; i < currentTags.length; i++) {
        textbox.value += currentTags[i];
        if (i < currentTags.length - 1)
            textbox.value += ";";
    }
}

function stopPropagation(e) {
    e.cancelBubble = true;
    if (e.stopPropagation) {
        e.stopPropagation();
    }
}

function setComboboxText(id) {
    var combobox = document.getElementById(id + "_Input");
    combobox.value = "";
    var checkboxIndex = 0;
    do {
        var checkbox = document.getElementById(id + "_i" + checkboxIndex + "_CheckBox");
        checkboxIndex++;
        if (checkbox != null) {
            if (checkbox.checked) {
                combobox.value += checkbox.nextSibling.firstChild.nodeValue + ",";
            }
        }
    } while (checkbox != null)
    combobox.value = combobox.value.replace(/,$/, "");
}

function LayoutChange(currentDropzones, newDropzones) {
    if (newDropzones < currentDropzones)
        alert('Das neue Layout hat weniger Spalten. Die Widgets werden automatisch verschoben.');
}

function ReloadPageWithTheme(theme) {
    var urlParts = window.location.href.split("?");
    if (urlParts.length == 2) {
        var queryStringParts = urlParts[1].split("&");
        var url = urlParts[0];
        var queryString = "";
        for (var i = 0; i < queryStringParts.length; i++) {
            if (queryStringParts[i].indexOf("Theme") != 0) {
                queryString += queryStringParts[i] + "&";
            }
        }
        window.location.href = url + "?" + queryString + "Theme=" + theme.value;
    }
}

function StyleSwitchMode(previewElementID, rbNewID, rbColorID, rbImageID, tbColorID, tbImageID, pnlColorolorID, pnlImagemageID, cbRepeatHID, cbRepeatVID) {
    var previewElement = document.getElementById(previewElementID);
    var rbColor = document.getElementById(rbColorID);
    var rbImage = document.getElementById(rbImageID);
    var tbColor = document.getElementById(tbColorID);
    var tbImage = document.getElementById(tbImageID);
    var pnlColor = document.getElementById(pnlColorolorID);
    var pnlImage = document.getElementById(pnlImagemageID);

    previewElement.style.backgroundImage = "none";
    previewElement.style.backgroundRepeat = "no-repeat";
    previewElement.style.backgroundColor = "transparent";

    if (rbNewID == rbColorID) // Switch to color mode
    {
        pnlColor.style.display = "inline";
        pnlImage.style.display = "none";

        if (tbColor.value.indexOf('#') != -1) {
            try { previewElement.style.backgroundColor = tbColor.value; }
            catch (error) { }
        }
    }
    else // Switch to image mode
    {
        pnlImage.style.display = "inline";
        pnlColor.style.display = "none";

        var imageLarge = tbImage.value.replace(/BG\/S/i, "BG/L");
        previewElement.style.backgroundImage = "url(" + imageLarge + ")";

        StyleSetRepeating(previewElementID, cbRepeatHID, cbRepeatVID);
    }
}

function StyleColorChange(previewElementID, colorBoxID, tbColorID) {
    var previewElement = document.getElementById(previewElementID);
    var colorBox = document.getElementById(colorBoxID);
    var tbColor = document.getElementById(tbColorID);

    try {
        previewElement.style.backgroundColor = tbColor.value;
        colorBox.style.backgroundColor = tbColor.value;
    }
    catch (error) {
        previewElement.style.backgroundColor = "transparent";
        colorBox.style.backgroundColor = "transparent";
        tbColor.value = "Keine Farbe ausgewählt";
    }
}

function StyleSetRepeating(previewElementID, cbRepeatHID, cbRepeatVID) {
    var previewElement = document.getElementById(previewElementID);
    var cbRepeatH = document.getElementById(cbRepeatHID);
    var cbRepeatV = document.getElementById(cbRepeatVID);

    previewElement.style.backgroundRepeat = "no-repeat";
    if (cbRepeatH.checked)
        previewElement.style.backgroundRepeat = "repeat-x";
    if (cbRepeatV.checked)
        previewElement.style.backgroundRepeat = "repeat-y";
    if (cbRepeatH.checked && cbRepeatV.checked)
        previewElement.style.backgroundRepeat = "repeat";
}

function StyleSetFooterHeight(previewElementID, tbFooterHeightID) {
    var previewElement = document.getElementById(previewElementID);
    var tbFooterHeight = document.getElementById(tbFooterHeightID);
    var validChars = "0123456789";
    var isNumber = true;
    var character;

    for (var i = 0; i < tbFooterHeight.value.length && isNumber == true; i++) {
        character = tbFooterHeight.value.charAt(i);
        if (validChars.indexOf(character) == -1) {
            isNumber = false;
        }
    }
    if (isNumber) {
        if (tbFooterHeight.value > 500)
            tbFooterHeight.value = 500;
        previewElement.style.height = tbFooterHeight.value + "px";
    }
}

function ChangeRatingImg(clientId, i) {
    for (var x = 1; x <= i; x++) {
        var img = document.getElementById(clientId + '_RAT' + x);
        img.src = img.getAttribute('nsrc');
    }
}

function RestoreRatingImg(clientId, i) {
    for (var x = 1; x <= i; x++) {
        var img = document.getElementById(clientId + '_RAT' + x);
        img.src = img.getAttribute('ssrc');
    }
}

function OnTextboxFocus(textbox, defaultValue) {
    if (textbox.value == (defaultValue)) {
        textbox.value = "";
    }
}

function AttachEvent(parentObject, eventName, eventHandler) {
    eventName = GetEventName(eventName);
    if (parentObject.addEventListener) {
        parentObject.addEventListener(eventName, eventHandler, false);
    }
    else {
        if (parentObject.attachEvent) {
            parentObject.attachEvent(eventName, eventHandler);
        }
    }
}

function DetachEvent(parentObject, eventName, eventHandler) {
    eventName = GetEventName(eventName);
    if (parentObject.addEventListener) {
        parentObject.removeEventListener(eventName, eventHandler, false);
    }
    else {
        if (parentObject.detachEvent) {
            parentObject.detachEvent(eventName, eventHandler);
        }
    }
}

function GetEventName(eventName) {
    eventName = eventName.toLowerCase();
    if (document.addEventListener) {
        if (0 == eventName.indexOf('on')) {
            return eventName.substr(2);
        }
        else {
            return eventName;
        }
    }
    else {
        if (document.attachEvent && !(0 == eventName.indexOf('on'))) {
            return 'on' + eventName;
        }
        else {
            return eventName;
        }
    }
}

function GetRadWindow() {
    var oWindow = null;
    if (window.radWindow)
        oWindow = window.radWindow;
    else if (window.frameElement != null && window.frameElement.radWindow)
        oWindow = window.frameElement.radWindow;
    return oWindow;
}

function CloseWindow(CWParams) {
    if (CWParams == 'undefined' || CWParams == null) {
        GetRadWindow().Close();
    }
    else {
        GetRadWindow().Close(CWParams);
    }
}

function OnLocationAdded(sender, eventArgs) {
    var manager = GetRadWindowManager();
    var targetWindow = manager.GetWindowByName(eventArgs.TargetWindow);
    targetWindow.get_contentFrame().contentWindow.OnLocationAdded(sender, eventArgs);
}

function OpenGeoTagWindow(controlId, windowTitle) {
    var txtLong = document.getElementById(controlId + '_TxtGeoLong').value;
    var txtLat = document.getElementById(controlId + '_TxtGeoLat').value;
    var txtZip = document.getElementById(controlId + '_HFZip').value;
    var txtCity = document.getElementById(controlId + '_HFCity').value;
    var txtRegion = document.getElementById(controlId + '_HFStreet').value;
    var txtCountry = document.getElementById(controlId + '_HFCountry').value;
    var url = "/Pages/Popups/PlaceOnMap.aspx?CtrlID=" + controlId + "&GC=" + txtLat + "," + txtLong + "&ZP=" + txtZip + "&CI=" + txtCity + "&RE=" + txtRegion + "&CO=" + txtCountry;
    radWinOpen(url, windowTitle, 400, 400, false, 'GeoInfoCB', 'mapWin');
}

function GeoInfoCB(sender, geoInfo) {
    var manager = GetRadWindowManager();
    var wizardWin = manager.GetWindowByName("locationWin");
    if (wizardWin == null) {
        wizardWin = manager.GetWindowByName("imageWin");
    }
    if (wizardWin == null) {
        wizardWin = manager.GetWindowByName("WidgetSettings");
    }
    if (wizardWin == null) {
        wizardWin = manager.GetWindowByName("wizardWin2");
    }
    if (wizardWin == null) {
        wizardWin = manager.GetWindowByName("wizardWin");
    }
    var currentDocument = null;
    if (wizardWin != null && wizardWin.get_contentFrame() != null && wizardWin.get_contentFrame().contentWindow != null) {
        currentDocument = wizardWin.get_contentFrame().contentWindow.document;
    }
    else {
        currentDocument = window.document;
    }
    if (geoInfo != null) {
        var geoInfoArray = geoInfo.toString().split(",");
        if (geoInfoArray.length == 7) {
            var controlId = geoInfoArray[0];
            var txtLong = currentDocument.getElementById(controlId + '_TxtGeoLong');
            var txtLat = currentDocument.getElementById(controlId + '_TxtGeoLat');
            var txtZip = currentDocument.getElementById(controlId + '_HFZip');
            var txtCity = currentDocument.getElementById(controlId + '_HFCity');
            var txtRegion = currentDocument.getElementById(controlId + '_HFStreet');
            var txtCountry = currentDocument.getElementById(controlId + '_HFCountry');
            txtLat.value = geoInfoArray[1];
            txtLong.value = geoInfoArray[2];
            txtZip.value = geoInfoArray[3];
            txtCity.value = geoInfoArray[4];
            txtRegion.value = geoInfoArray[5];
            txtCountry.value = geoInfoArray[6];
        }
    }
}

function RefreshRadWindow(radWindowId) {
    var radWin = null;
    var currentRadWin = GetRadWindow();
    if (currentRadWin != null) {
        var manager = currentRadWin.get_windowManager();
        if (manager != null)
            var radWin = manager.GetWindowByName(radWindowId);
    }
    if (radWin != null) {
        radWin.reload();
    }
    else {
        RefreshParentPage();
    }
}

function RefreshParentPage(ignoreCache) {
    if (ignoreCache != null)
        GetRadWindow().BrowserWindow.location.reload(ignoreCache);
    else
        GetRadWindow().BrowserWindow.location = GetRadWindow().BrowserWindow.location;
}

function RedirectParentPage(newUrl) {
    GetRadWindow().BrowserWindow.document.location.href = newUrl;
}

function CallFunctionOnRadWindow(windowId, functionName) {
    var radWin = null;
    var currentRadWin = GetRadWindow();
    if (currentRadWin != null) {
        var manager = currentRadWin.get_windowManager();
        if (manager != null)
            var radWin = manager.GetWindowByName(windowId);
    }
    if (radWin != null) {
        var functionReference = radWin.get_contentFrame().contentWindow[functionName];
        if (typeof (functionReference) == "function")
            functionReference();
    }
}

function CallFunctionOnParentPage(fnName) {
    var oWindow = GetRadWindow();
    if (oWindow.BrowserWindow[fnName] && typeof (oWindow.BrowserWindow[fnName]) == "function") {
        oWindow.BrowserWindow[fnName](oWindow);
    }
}

function radWinOpen(url, title, width, height, isCentered, CallBackFunc, windowId) {
    var oWindow;

    if (GetRadWindow() == null) {
        oWindow = window.radopen(url, windowId);
    }
    else {
        var oBrowserWnd = GetRadWindow().BrowserWindow;
        oWindow = oBrowserWnd.radopen(url, windowId);
    }
    oWindow.set_modal(true);
    oWindow.SetSize(width, height);
    oWindow.SetTitle(title);
    oWindow.Center();

    if (CallBackFunc != 'undefined' && CallBackFunc != null) {
        oWindow.set_clientCallBackFunction(CallBackFunc);
    }
}

function GetXmlHttp() {
    http_request = false;

    if (window.XMLHttpRequest) { // Mozilla, Safari,...
        http_request = new XMLHttpRequest();
        if (http_request.overrideMimeType) {
            http_request.overrideMimeType('text/xml');
        }
    } else if (window.ActiveXObject) { // IE
        try {
            http_request = new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (e) {
            try {
                http_request = new ActiveXObject("Microsoft.XMLHTTP");
            }
            catch (e)
			{ }
        }
    }

    if (!http_request) {
        return null;
    }
    else {
        return http_request;
    }
}

function GetXmlHttp_Old() {
    var x = null;
    try {
        x = new ActiveXObject("Msxml2.XMLHTTP");
    }
    catch (e) {
        try {
            x = new ActiveXObject("Microsoft.XMLHTTP");
        }
        catch (e) {
            x = null;
        }
    }

    if (!x && typeof XMLHttpRequest != "undefined") {
        x = new XMLHttpRequest();
    }

    return x;
}

function DateAdd(date, interval, value) {

    if (interval == "Y")
        return new Date(date.getFullYear() - 0 + value, date.getMonth(), date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds());
    else if (interval == "M")
        return new Date(date.getFullYear(), date.getMonth() - 0 + value, date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds());
    else if (interval == "D")
        return new Date(date.getFullYear(), date.getMonth(), date.getDate() - 0 + value, date.getHours(), date.getMinutes(), date.getSeconds());
    else
        return new Date(date.getFullYear(), date.getMonth(), date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds());
}

function ReturnOptionAuswahl(id, anzahl) {
    for (i = 0; i < anzahl; ++i) {
        var opt = document.getElementById(id + "_" + i);
        if (opt.checked == true) {
            return (opt.value);
        }
    }
    return 0;
}

function formatParaDate(date) {
    return date.getFullYear() + "." + (date.getMonth() + 1) + "." + date.getDate();
}

var xmlHttp = null;

function GetJson(urlBase, fromId, toId) {
    var url = buildDataUrl(urlBase, fromId, toId);

    xmlHttp = GetXmlHttp();
    xmlHttp.onreadystatechange = ProcessRequest;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function buildDataUrl(urlBase, fromId, toId) {
    var url = urlBase;

    var ctrl = $find(fromId);
    if (ctrl != null) {
        var value = ctrl.get_selectedDate();
        url += "&DateFrom=" + formatParaDate(value);
    }

    ctrl = $find(toId);
    if (ctrl != null) {
        value = ctrl.get_selectedDate();
        url += "&DateTo=" + formatParaDate(value);
    }

    return url;
}

function ProcessRequest() {
    if (xmlHttp.readyState == 4) {
        var ids = xmlHttp.getResponseHeader("IDS").split("|");
        if (xmlHttp.status == 200) {
            var info = xmlHttp.responseText;
            var tmp = $get(ids[0]);
            x = tmp.load(info);
            hideErrorMessage(ids[1]);
        }
        else if (xmlHttp.status == 501) {
            showErrorMessage(ids[1], xmlHttp.responseText);
        }
        else {
            showErrorMessage(ids[1], xmlHttp.status);
        }
    }
}

function hideErrorMessage(errorId) {
    var ctrl = document.getElementById(errorId);
    ctrl.innerHTML = "";
}

function showErrorMessage(errorId, message) {
    var ctrl = document.getElementById(errorId);
    ctrl.innerHTML = message;
}

function showMore(hideElementId, showElementId) {
    var hideElement = document.getElementById(hideElementId);
    var showElement = document.getElementById(showElementId);
    hideElement.style.display = "none";
    showElement.style.display = "block";
}

function setFromToDate(dataRange, fromId, toId) {
    var to = new Date();
    var from = new Date();

    if (dataRange == 1) {
        from.setDate(1);
    }
    else if (dataRange == 2) {
        to.setDate(1);
        to = DateAdd(to, "D", -1);
        from = DateAdd(to, "Copy", 0);
        from.setDate(1);
    }
    else if (dataRange == 3) {
        from.setDate(1);
        from = DateAdd(from, "M", -1);
    }

    var ctrlFrom = $find(fromId);
    var ctrlTo = $find(toId);

    ctrlFrom.set_selectedDate(from);
    ctrlTo.set_selectedDate(to);
}

function setCookie(cookieName, value, expiredays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + expiredays);
    document.cookie = cookieName + "=" + escape(value) + ((expiredays == null) ? "" : ";expires=" + exdate.toUTCString()) + ";path=/";
}

function getCookie(cookieName) {
    if (document.cookie.length > 0) {
        var start = document.cookie.indexOf(cookieName + "=");
        if (start != -1) {
            start = start + cookieName.length + 1;
            var end = document.cookie.indexOf(";", start);
            if (end == -1) end = document.cookie.length;
            return unescape(document.cookie.substring(start, end));
        }
    }
    return "";
}

/* 4screen webcontrols */
Type.registerNamespace("_4screen.WebControls");

_4screen.WebControls.closeCurrentAndRefreshActiveWindow = function() {
    var oWnd = _4screen.WebControls.getRadWindow();
    var oManager = oWnd.get_windowManager();
    oWnd.Close();
    var oActive = oManager.getActiveWindow();
    if (oActive != null) {
        oActive.reload();
    }
    else {
        window.location.reload(true);
    }
}

_4screen.WebControls.getRadWindow = function() {
    var oWindow = null;
    if (window.radWindow)
        oWindow = window.radWindow;
    else if (window.frameElement != null && window.frameElement.radWindow)
        oWindow = window.frameElement.radWindow;
    return oWindow;
}

_4screen.WebControls.openRadWindowWithEvent = function(e, url, title, callBackFunction, windowId) {
    if (!e) var e = window.event;
    e.cancelBubble = true;
    if (e.stopPropagation) e.stopPropagation();

    _4screen.WebControls.openRadWindow(url, title, callBackFunction, windowId);
}

_4screen.WebControls.openRadWindow = function(url, title, callBackFunction, windowId) {
    var radWindow;

    if (_4screen.WebControls.getRadWindow() == null) {
        radWindow = window.radopen(url, windowId);
    }
    else {
        var oBrowserWnd = _4screen.WebControls.getRadWindow().BrowserWindow;
        radWindow = oBrowserWnd.radopen(url, windowId);
    }
    radWindow.set_modal(true);
    radWindow.SetTitle(title);

    if (callBackFunction != 'undefined' && callBackFunction != null) {
        radWindow.set_clientCallBackFunction(callBackFunction);
    }
}
