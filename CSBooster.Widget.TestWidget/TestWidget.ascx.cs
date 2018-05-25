using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Xml;
using _4screen.CSB.DataAccess;
using _4screen.CSB.Common;

namespace _4screen.CSB.Widget
{
   public partial class TestWidget : WidgetBase
   {
      public override void ShowObject(string strXml)
      {
         try
         {
            XmlDocument xmlDom = new XmlDocument();
            xmlDom.LoadXml(strXml);

            Control objectDetails = this.LoadControl("~/UserControls/ObjectDetail.ascx");

            ((IObjectDetail)objectDetails).ObjectID = XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtImg", "");
            ((IObjectDetail)objectDetails).ShowAuthor = XmlHelper.GetElementValue(xmlDom.DocumentElement, "cbxUsr", true);
            ((IObjectDetail)objectDetails).ShowTitle = XmlHelper.GetElementValue(xmlDom.DocumentElement, "cbxTit", false);
            ((IObjectDetail)objectDetails).ShowDesc = XmlHelper.GetElementValue(xmlDom.DocumentElement, "cbxDesc", false);
            ((IObjectDetail)objectDetails).ShowRating = XmlHelper.GetElementValue(xmlDom.DocumentElement, "cbxRating", true);
            ((IObjectDetail)objectDetails).ImageWidth = XmlHelper.GetElementValue(xmlDom.DocumentElement, "ddlImgWidth", "");
            ((IObjectDetail)objectDetails).CommunityID = this.CommunityID.ToString();
            ((IObjectDetail)objectDetails).ObjType = ObjectType.Picture;

            pnlResult.Controls.Add(objectDetails);
         }
         catch { }
      }
   }
}
