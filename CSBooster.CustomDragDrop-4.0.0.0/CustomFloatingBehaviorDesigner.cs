// Copyright (c) Omar AL Zabir. All rights reserved.
// For continued development and updates, visit http://msmvps.com/omar

using AjaxControlToolkit;

namespace CustomDragDrop
{
    internal class CustomFloatingBehaviorDesigner : AjaxControlToolkit.Design.ExtenderControlBaseDesigner<CustomFloatingBehaviorExtender>
    {
    }

    [ClientScriptResource(null, "CustomDragDrop.CustomFloatingBehavior.js")]
    public static class CustomFloatingBehaviorScript
    {
    }
}