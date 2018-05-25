/*
PhotoNotes v2.0

Copyright (c) 2007 Guillaume Texier - gig6@free.fr
(Based on original PhotoNotes Copyright (c) 2006 Dusty Davidson - http://www.dustyd.net)

- toXML() added
- getTransfertObject() added
- transfertObject now used
- PhotoNoteContainer read-only mode added
- PhotoNoteContainer maximum number of notes added
- The note text is now only selected when edit mode start
- The note text now keeps new line
- The note text is now HTML proof
- The note clickable zone is now the external area (user could clic on area border and get only dragresize funcions without note edition)
- DragResize code is now in its own file (dragresize.js)
- DragResize v1.0 now used (fix Internet explorer stack overflow bug when number of notes exeeds 15)

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the "Software"),
to deal in the Software without restriction, including without limitation
the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the Software
is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

/*

Copyright (c) 2007 4 screen AG
(Based on original PhotoNotes Copyright (c) 2006 Dusty Davidson - http://www.dustyd.net and 2007 Guillaume Texier - gig6@free.fr)

Some project specific extensions (search for [4s])
- do not hide notetexts
- extended visual with 'note creator' and 'whois' user
- done some bugfixes

*/


/*********************************************************/
/*** Photo Notes Container *******************************/
/*********************************************************/
function PhotoNoteContainer(element, config) {
    var props = {
        element: element,
        dragresize: null,
        notes: new Array(),
        editing: false,
        maxNotes: 999,
        readonly: false
    };

    for (var p in props) {
        this[p] = (!config || typeof config[p] == 'undefined') ? props[p] : config[p];
    }

};

PhotoNoteContainer.prototype.DeleteNote = function(note) {

    note.UnSelect();

    /* remove from the DOM */
    this.element.removeChild(note.gui.ElementRect);
    this.element.removeChild(note.gui.ElementNote);

    /* remove from the array... */
    try {
        this.notes.remove(note);
    }
    catch (e) { }
}

PhotoNoteContainer.prototype.AddNote = function(note) {
    if (!this.editing && this.notes.length < this.maxNotes) {
        /* add the note to the array of notes, and set its container */
        this.notes[this.notes.length] = note;
        note.container = this;

        /* add the note to the DOM */
        this.element.appendChild(note.gui.ElementRect);
        this.element.appendChild(note.gui.ElementNote);
        return true
    }
    return false

};

/* hide all of the "text" parts of the notes. Primarily called when hovering a note, at which
point we want to hide all of the other note texts */
PhotoNoteContainer.prototype.HideAllNoteTexts = function() {
    for (var i = 0; i < this.notes.length; i++)
        this.notes[i].HideNoteText();
};


PhotoNoteContainer.prototype.DisableAllNotes = function() {
    for (var i = 0; i < this.notes.length; i++)
        this.notes[i].DisableNote();
};

PhotoNoteContainer.prototype.HideAllNotes = function() {
    for (var i = 0; i < this.notes.length; i++)
        this.notes[i].HideNote();
};

PhotoNoteContainer.prototype.ShowAllNotes = function() {
    for (var i = 0; i < this.notes.length; i++)
        this.notes[i].ShowNote();
};

PhotoNoteContainer.prototype.EnableAllNotes = function() {
    for (var i = 0; i < this.notes.length; i++)
        this.notes[i].EnableNote();
};

PhotoNoteContainer.prototype.setReadonly = function(readonly) {
    this.readonly = readonly
};

PhotoNoteContainer.prototype.toggleReadonly = function() {
    this.readonly = !this.readonly
};

PhotoNoteContainer.prototype.isReadonly = function() {
    return this.readonly
};



/*********************************************************/
/*** Photo Note ******************************************/
/*********************************************************/
function PhotoNote(transfertObject) {
    // Declaration
    this.text = '';
    this.id = -1;
    this.whoisid = ''; 	// [4s]
    this.whois = ''; 	// [4s]
    this.fromid = ''; 	// [4s]
    this.from = ''; 	// [4s]
    this.rect = { 'left': 10, 'top': 10, 'width': 50, 'height': 50 };
    this.selected = false;
    this.container = null;
    this.dragresize = null;
    this.oldText = null;
    this.oldRect = null;
    this.YOffset = 10;
    this.XOffset = 0;
    this.onsave = null;
    this.ondelete = null;
    this.gui = null
    this.state = 0; 		// [4s]
    this.canSave = 1; 	// [4s]
    this.canDel = 1; 	// [4s]
    this.canFree = 0;

    // Initialization
    for (var p in transfertObject) {
        this[p] = transfertObject[p];
    }

    // Action
    this.CreateElements();
}


PhotoNote.prototype.Select = function() {
    if (!this.container.editing && !this.container.readonly) {
        this.ShowNoteText();
        this.dragresize.select(this.gui.ElementRect);
        this.selected = true;
        this.SetEditable(true);
    }
}


PhotoNote.prototype.UnSelect = function() {
    this.dragresize.deselect(true);
    this.selected = false;
    this.SetEditable(false);
    this.HideNoteText();
}

PhotoNote.prototype.p_encodeXMLChars = function(value) {
    var xmlCharsMapping = new Array(["&", "&amp;"], ['"', "&quot;"], ["<", "&lt;"], [">", "&gt;"], ["'", "&#39;"], ["|", " "]);

    var res = value;
    for (var i = 0; i < xmlCharsMapping.length; i++) {
        var re = new RegExp("(" + xmlCharsMapping[i][0] + ")", "g");
        res = res.replace(re, xmlCharsMapping[i][1]);
    }
    return res;
}

PhotoNote.prototype.Save = function() {
    this.oldText = null;
    this.oldRect = null;
    //[4s]	this.gui.TextTitle.innerHTML = this.p_encodeXMLChars(this.gui.TextBox.value).replace(/\n/gi,'<br/>');
    this.gui.TextTitle.innerHTML = this.gui.TextBox.value.replace(/\n/gi, '<br/>');
    this.gui.TextTitle.innerHTML += "<div style='padding-top: 3px'></div>";

    //+ [4s]
    var w = this.gui.DropDown.selectedIndex; 				// [4s]
    var selected_text = this.gui.DropDown.options[w].text; 	// [4s]

    if (this.gui.DropDown.value.length > 0 && this.gui.DropDown.value != "-") {
        this.gui.TextTitle.innerHTML += "<div id='fn-note-whois'>Dies ist <a id='fn-note-whois-link' href='/Default.aspx?ui=" + this.gui.DropDown.value + "'>" + selected_text + "</a></div>";
    }

    if (this.from != null && this.from.length > 0 && this.from != "") {
        this.gui.TextTitle.innerHTML += "<div id='fn-note-from'>Notiz von: <a id='fn-note-from-link' href='/Default.aspx?ui=" + this.fromid + "'>" + this.from + "</a></div>";
    }
    //- [4s]

    this.UnSelect();
}

PhotoNote.prototype.Cancel = function() {

    //if the note is still new, then we actually want to delete it, not cancel it..
    if (this.id < 0) {
        this.container.DeleteNote(this)
    }
    else {
        //reset the node to it's old position
        if (this.oldRect != null) {
            this.text = this.oldText;
            this.rect = this.oldRect;
            this.oldText = null;
            this.oldRect = null;
        }
        this.gui.TextBox.value = this.text;
        this.PositionNote();
        this.UnSelect();
    }

}


PhotoNote.prototype.ShowNoteText = function() {
    if (!this.container.editing) {
        this.container.HideAllNoteTexts();
        this.container.DisableAllNotes();
        this.EnableNote();

        this.gui.ElementRect.style.border = '1px solid #D4D82D';
        this.gui.ElementRect.style.margin = '0';
        this.gui.ElementNote.style.display = 'block';
    }
}


PhotoNote.prototype.DisableNote = function() {
    this.dragresize.enabled = false;
}


PhotoNote.prototype.EnableNote = function() {
    if (!this.container.readonly) {
        this.dragresize.enabled = true;
    }
}


PhotoNote.prototype.HideNoteText = function() {
    this.gui.ElementRect.style.border = '0px solid #D4D82D';
    this.gui.ElementRect.style.margin = '1px';
    // [4s]    this.gui.ElementNote.style.display='none';
}


PhotoNote.prototype.HideNote = function() {
    this.gui.ElementRect.style.display = 'none';
    this.gui.ElementNote.style.display = 'none';
}


PhotoNote.prototype.ShowNote = function() {
    this.gui.ElementRect.style.display = 'block';
    this.gui.ElementNote.style.display = 'none';
}


PhotoNote.prototype.SetEditable = function(editable) {
    this.container.editing = editable;

    if (editable) {
        //the first child of the note is the text
        this.gui.TextTitle.style.display = 'none';

        //the second child is the edit area...
        this.gui.EditArea.style.display = 'block';

        //if this is a "new" note, then hide the delete button
        if (this.id <= 0)
            this.gui.DeleteButton.style.display = 'none';
        else
            this.gui.DeleteButton.style.display = 'inline';

        // get the textarea and select the text...
        this.HighlightTextbox();
    }
    else {
        //the first child of the note is the text
        this.gui.TextTitle.style.display = 'block';

        //the second child is the edit area...
        this.gui.EditArea.style.display = 'none';
    }
}


PhotoNote.prototype.HighlightTextbox = function() {
    // get the textarea and select the text...
    if (this.gui.EditArea.style.display == 'block') {
        var textfield = this.gui.TextBox;
        setTimeout(function() {
            try {
                textfield.focus();
                textfield.select();
            }
            catch (e) { }
        }, 200);
    }

}


PhotoNote.prototype.CreateElements = function() {

    this.gui = new PhotoNoteGUI();

    var newArea = document.createElement('div');
    this.dragresize = new DragResize('dragresize', { allowBlur: false });
    /*
    this.dragresize = new Object();

this.dragresize.enabled = true
    this.dragresize.select = function () {return true}
    this.dragresize.deselect = function () {return true}
    */

    newArea.className = 'fn-area';
    newArea.id = 'fn-area-new';

    var newAreaBlack = document.createElement('div');
    newAreaBlack.className = 'fn-area-blackborder';
    var newAreaWhite = document.createElement('div');
    newAreaWhite.className = 'fn-area-whiteborder';


    var currentNote = this;


    var newAreaInner = document.createElement('div');
    newAreaInner.className = 'fn-area-inner';
    newAreaWhite.appendChild(newAreaInner);


    //attach mouse events to this element...
    addEvent(newAreaInner, 'mouseover', function() {
        currentNote.ShowNoteText();
    });
    addEvent(newAreaInner, 'mouseout', function() {

        if (!currentNote.selected) {
            setTimeout(function() {
                currentNote.HideNoteText();
            }, 250);

        }
    });

    addEvent(newArea, 'mousedown', function() {
        if (!currentNote.selected) {
            //window.status = 'mouseDown2!';
            currentNote.Select();
        }
    });


    newAreaBlack.appendChild(newAreaWhite);
    newArea.appendChild(newAreaBlack);

    // add the notes area
    var noteArea = document.createElement('div');
    noteArea.className = 'fn-note';

    var titleArea = document.createElement('div');
    titleArea.className = 'fn-note-text';
    //var t = document.createTextNode(this.text + " - " + this.whois);  // [4s]

    var temp = this.text;
    if (this.whois.length > 0 && this.whoisid.length > 1) {
        temp += "<div id='fn-note-whois'>Dies ist <a id='fn-note-whois-link' href='/User/" + this.whoisid + "'>" + this.whois + "</a></div>";
    }

    if (this.from != null && this.from.length > 0) {
        temp += "<div id='fn-note-from'>Notiz von: <a id='fn-note-from-link' href='/User/" + this.fromid + "'>" + this.from + "</a></div>";
    }

    var t = document.createTextNode(temp);  // [4s]

    titleArea.innerHTML = temp;
    //titleArea.appendChild(t);

    noteArea.appendChild(titleArea);

    var editArea = document.createElement('div');
    editArea.className = 'fn-note-edit';

    var editAreaText = document.createElement('div');
    editAreaText.className = 'fn-note-edit-text';

    var newTextbox = document.createElement('textarea');
    newTextbox.value = this.text;
    editAreaText.appendChild(newTextbox);
    editArea.appendChild(editAreaText);

    //+ [4s]
    //debugger;
    var newState = null;
    if (this.state == 1 && this.canFree == 1) {
        var editState = document.createElement('div');
        editState.className = 'fn-note-edit-select';
        newState = document.createElement('input');
        newState.type = 'checkbox';
        newState.checked = true;
        //newState.innerText='Status';
        editState.appendChild(newState);

        var newStateTxt = document.createElement('span');
        newStateTxt.className = 'fn-note-publ';
        newStateTxt.innerText = 'Notiz publizieren?';
        editState.appendChild(newStateTxt);

        editArea.appendChild(editState);
    }

    var editSelect = document.createElement('div');
    editSelect.className = 'fn-note-edit-select';
    var newSelect = document.createElement('select');
    editSelect.appendChild(newSelect);
    editArea.appendChild(editSelect);

    var newOption = document.createElement('option');
    newOption.text = "Das ist...";
    newOption.value = "-";
    newOption.selected = false;
    newSelect.options[0] = newOption;

    for (var i = 0; i < uidArray.length; i++) {
        var newOption = document.createElement('option');
        newOption.text = nnArray[i];
        newOption.value = uidArray[i];
        if (this.whoisid == uidArray[i])
            newOption.selected = true;
        newSelect.options[i + 1] = newOption;
    }

    //- [4s]

    var buttonsDiv = document.createElement('div');
    var newButtonOK = document.createElement('input');
    newButtonOK.type = 'button';
    newButtonOK.value = 'Speichern';

    //+ [4s]   disabled="disabled"
    if (currentNote.canSave == 0) {
        newButtonOK.className = 'inputButton';
        newButtonOK.disabled = 'disabled';
    }
    //- [4s]
    else {
        newButtonOK.className = 'inputButton';
        newButtonOK.onclick = function() {
            if (!currentNote.onsave) {
                alert("onsave must be implemented in order to *actually* save");
                currentNote.Cancel();
            } else {
                // Store the new text
                currentNote.text = currentNote.gui.TextBox.value;

                currentNote.whoisid = currentNote.gui.DropDown.value; 			// [4s]

                var w = currentNote.gui.DropDown.selectedIndex; 					// [4s]
                var selected_text = currentNote.gui.DropDown.options[w].text; 	// [4s]
                currentNote.whois = selected_text; 								// [4s]		
                if (currentNote.gui.State != null && currentNote.gui.State.checked == true)
                    currentNote.state = 2;

                // Call the abstract function
                var res = currentNote.onsave(currentNote);
                // Save or cancel saving according to abstract saving function result
                if (res > 0) {
                    currentNote.id = res;
                    currentNote.Save();
                } else {
                    alert("error saving note");
                    currentNote.Cancel();
                }
            }

        };
    }
    buttonsDiv.appendChild(newButtonOK);

    var newButtonCancel = document.createElement('input');
    newButtonCancel.type = 'button';
    newButtonCancel.className = 'inputButton';
    newButtonCancel.value = 'Abbrechen';
    newButtonCancel.onclick = function() {
        currentNote.Cancel();

    };
    buttonsDiv.appendChild(newButtonCancel);

    var newButtonDelete = document.createElement('input');
    newButtonDelete.type = 'button';
    newButtonDelete.value = 'Entfernen';

    //+ [4s]   disabled="disabled"
    if (currentNote.canDel == 0) {
        newButtonDelete.className = 'inputButton';
        newButtonDelete.disabled = 'disabled';
    }
    //- [4s]
    else {
        newButtonDelete.className = 'inputButton';
        newButtonDelete.onclick = function() {

            if (currentNote.ondelete) {
                var res = currentNote.ondelete(currentNote);
                if (res) {
                    currentNote.container.DeleteNote(currentNote);
                }
                else {
                    alert("error deleting note");
                }
            }
            else {
                alert("ondelete must be implemented in order to *actually* delete");
            }
        };
    }
    buttonsDiv.appendChild(newButtonDelete);

    editArea.appendChild(buttonsDiv);
    noteArea.appendChild(editArea);



    /********* DRAG & RESIZE EVENTS **********************/

    this.dragresize.isElement = function(elm) {
        if (elm.className == 'fn-area') {
            this.maxLeft = currentNote.container.element.offsetWidth; 	// [4s]
            this.maxTop = currentNote.container.element.offsetHeight - 3; // [4s]

            return true;
        }
    };
    this.dragresize.isHandle = function(elm) {
        if (elm.className == 'fn-area')
            return true;
    };
    this.dragresize.ondragfocus = function() {
        currentNote.gui.ElementRect.style.cursor = 'move';
    };
    this.dragresize.ondragblur = function() {
        currentNote.gui.ElementRect.style.cursor = 'pointer';
    };
    this.dragresize.ondragstart = function() {
        if (currentNote.oldRect == null) {
            currentNote.oldText = currentNote.text
            var r = currentNote.rect;
            currentNote.oldRect = new PhotoNoteRect(r.left, r.top, r.width, r.height);

        }
    };
    this.dragresize.ondragend = function() {
    };
    this.dragresize.ondragmove = function() {
        currentNote.rect.left = parseInt(this.element.style.left);
        currentNote.rect.top = parseInt(this.element.style.top);
        currentNote.rect.width = parseInt(this.element.style.width);
        currentNote.rect.height = parseInt(this.element.style.height);
        currentNote.PositionNote();
    };

    this.dragresize.apply(document);


    /* setup the GUI object */
    this.gui.ElementRect = newArea;
    this.gui.ElementNote = noteArea;
    this.gui.EditArea = editArea;
    this.gui.TextBox = newTextbox;
    this.gui.TextTitle = titleArea;
    this.gui.DeleteButton = newButtonDelete;
    this.gui.DropDown = newSelect; // [4s]
    this.gui.State = newState;  	// [4s]

    /* position the note text below the note area */
    this.PositionNote();

}

PhotoNote.prototype.PositionNote = function() {
    /* outer most box */
    this.gui.ElementRect.style.left = this.rect.left + 'px';
    this.gui.ElementRect.style.top = this.rect.top + 'px';
    this.gui.ElementRect.style.width = this.rect.width + 'px';
    this.gui.ElementRect.style.height = this.rect.height + 'px';

    // black border
    this.gui.ElementRect.firstChild.style.width = parseInt(this.gui.ElementRect.style.width) - 2 + 'px';
    this.gui.ElementRect.firstChild.style.height = parseInt(this.gui.ElementRect.style.height) - 2 + 'px';

    // white border
    this.gui.ElementRect.firstChild.firstChild.style.width = parseInt(this.gui.ElementRect.style.width) - 4 + 'px';
    this.gui.ElementRect.firstChild.firstChild.style.height = parseInt(this.gui.ElementRect.style.height) - 4 + 'px';

    // inner box
    this.gui.ElementRect.firstChild.firstChild.firstChild.style.width = parseInt(this.gui.ElementRect.style.width) - 6 + 'px';
    this.gui.ElementRect.firstChild.firstChild.firstChild.style.height = parseInt(this.gui.ElementRect.style.height) - 6 + 'px';

    this.gui.ElementNote.style.left = this.rect.left + this.XOffset + 'px';
    this.gui.ElementNote.style.top = this.rect.top + this.YOffset + this.rect.height + 'px';

}

PhotoNote.prototype.toXML = function() {

    return '<image:hasPart>'
		+ '<image:Rectangle rdf:ID="' + this.id + '">'
			+ '<image:points>' + this.rect.left + ',' + this.rect.top + ' ' + this.rect.width + ',' + this.rect.height + '</image:points>'
    //+'<dc:title>'+this.p_encodeXMLChars(this.title)+'</dc:title>'
    //+'<dc:description>'+this.p_encodeXMLChars(this.description)+'</dc:description>'
			+ '<image:depicts rdf:parseType="Resource">'
				+ '<dc:description>' + this.p_encodeXMLChars(this.text) + '</dc:description>'
			+ '</image:depicts>'
		+ '</image:Rectangle>'
	+ '</image>'
}

PhotoNote.prototype.getTransfertObject = function() {
    // Declaration
    var transfertObject = new Object()

    // Initialization
    transfertObject.id = this.id
    //transfertObject.title			= this.title
    transfertObject.text = this.text
    transfertObject.whoisid = this.whoisid		// [4s]
    transfertObject.whois = this.whois		// [4s]
    transfertObject.fromid = this.fromid		// [4s]
    transfertObject.from = this.from			// [4s]
    transfertObject.state = this.state		// [4s]
    transfertObject.rect = new Object()
    transfertObject.rect.left = this.rect.left
    transfertObject.rect.top = this.rect.top
    transfertObject.rect.width = this.rect.width
    transfertObject.rect.height = this.rect.height
    transfertObject.canSave = this.canSave
    transfertObject.canDel = this.canDel
    transfertObject.canFree = this.canFree

    // Everything is OK !
    return transfertObject

}

PhotoNote.prototype.p_genUUID = function() {
    var result, i, j;
    result = '';
    for (j = 0; j < 32; j++) {
        if (j == 8 || j == 12 || j == 16 || j == 20)
            result = result + '-';
        i = Math.floor(Math.random() * 16).toString(16).toUpperCase();
        result = result + i;
    }
    return result
}

/*********************************************************/
/*** Photo Note GUI Object *******************************/
/*********************************************************/
function PhotoNoteGUI() {
    this.ElementRect = null;

    // the note text area...
    this.ElementNote = null;
    this.TextTitle = null;
    this.EditArea = null;
    this.TextBox = null;
    this.DropDown = null; // [4s]
    this.State = null;

    // buttons
    this.DeleteButton = null;
}



/*********************************************************/
/*** Rectangle *******************************************/
/*********************************************************/
function PhotoNoteRect(left, top, width, height) {
    this.left = left;
    this.top = top;
    this.width = width;
    this.height = height;
}

/* for debugging purposes */
PhotoNoteRect.prototype.toString = function() {
    return 'left: ' + this.left + ', top: ' + this.top + ', width: ' + this.width + ', height: ' + this.height;
}
