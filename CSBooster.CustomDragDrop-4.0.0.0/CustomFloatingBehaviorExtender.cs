// Copyright (c) Omar AL Zabir. All rights reserved.
// For continued development and updates, visit http://msmvps.com/omar

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

[assembly: System.Web.UI.WebResource("CustomDragDrop.CustomFloatingBehavior.js", "text/javascript")]

namespace CustomDragDrop
{
    [Designer(typeof (CustomFloatingBehaviorDesigner))]
    [ClientScriptResource("CustomDragDrop.CustomFloatingBehavior", "CustomDragDrop.CustomFloatingBehavior.js")]
    [TargetControlType(typeof (WebControl))]
    [RequiredScript(typeof (DragDropScripts))]
    public class CustomFloatingBehaviorExtender : ExtenderControlBase
    {
        [ExtenderControlProperty]
        [IDReferenceProperty(typeof (WebControl))]
        public string DragHandleID
        {
            get { return GetPropertyValue<String>("DragHandleID", string.Empty); }
            set { SetPropertyValue<String>("DragHandleID", value); }
        }
    }
}