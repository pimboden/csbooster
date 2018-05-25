using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using System;
using System.Reflection;
using System.Data;

namespace _4screen.CSB.Widget
{
    public partial class UploadContainer : WidgetBase
    {
        protected GuiLanguage languageProfile = GuiLanguage.GetGuiLanguage("ProfileData");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        public override bool ShowObject(string settingsXml)
        {
            //DataObject dataObject = null;
            //if (this._Host.ParentPageType == PageType.Detail && !string.IsNullOrEmpty(Request.QueryString["OID"]))
            //{ 
            //    if (string.IsNullOrEmpty(Request.QueryString["OT"])) 
            //        dataObject = DataObject.LoadByReflection(Request.QueryString["OID"].ToGuid());   
            //    else
            //        dataObject = DataObject.LoadByReflection(Request.QueryString["OID"].ToGuid(), Helper.GetObjectTypeNumericID(Request.QueryString["OT"]));   
            //}
            //else if (this._Host.ParentPageType == PageType.None)
            //{
            //    dataObject = DataObject.Load<DataObject>(CommunityID);
            //    if (dataObject.ObjectType == 19)
            //        dataObject = DataObject.Load<DataObjectUser>(dataObject.UserID);   
            //    else if (dataObject.ObjectType != 1)
            //        return false;
            //}
            //else
            //{
            //    return false;
            //}

            //if (dataObject != null)
            //{
            //    Fill(dataObject);
                return true;
            //}
            //else
            //    return false;
          }



    }   // END CLASS
}   // END NAMESPACE