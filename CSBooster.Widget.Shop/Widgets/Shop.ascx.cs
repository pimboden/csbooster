using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.DataAccess;
using _4screen.CSB.Widget;
using System.Xml;

namespace CSBooster.Widget.Shop
{
    public partial class Shop : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            bool hasContent = false;
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(settingsXml);
                string basketType = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "rcbBasketType", "Small");

                Control control = null;
                if (basketType=="Small")
                {
                    control = LoadControl("~/UserControls/BasketSmall.ascx");
                }
               else
                {
                    control = LoadControl("~/UserControls/BasketComplete.ascx");
                }

                phBasket.Controls.Add(control);
                IMinimalControl iMinCtrl = control as IMinimalControl;
                return iMinCtrl.HasContent;

            }
            catch
            {
            }
            return hasContent;

        }
    }
}