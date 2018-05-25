// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Web;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using SiteConfig=_4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.DataObj.UserControls.Templates
{
    public partial class SmallOutputProduct : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        #region ISettings Members

        public Dictionary<string, object> Settings { get; set; }

        #endregion

        protected string WidthColumn = "100%";
        protected string WidthImg = "158px";
        protected string WidthCnt = "464px";

        private Business.DataObjectProduct DataObjectProd { get; set; }
        public DataAccess.Business.DataObject DataObject { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Settings != null)
            {
                if (Settings.ContainsKey("Width"))
                {
                    WidthImg = string.Format("{0}px", 140);
                    WidthCnt = string.Format("{0}px", (int)Settings["Width"] - 160);
                }
            }

            if (DataObject is Business.DataObjectProduct)
                DataObjectProd = (Business.DataObjectProduct)DataObject;
            else
                DataObjectProd = DataAccess.Business.DataObject.Load<Business.DataObjectProduct>(DataObject.ObjectID, null, false);

            if (DataObjectProd.Price1.HasValue)
                LitPrice1.Text = string.Format("CHF {0}", DataObjectProd.Price1.Value.ToString("0.00"));
            else
            {
                PhPrice1.Visible = false;
            }
            if (DataObjectProd.Price1.HasValue && DataObjectProd.Price2.HasValue && DataObjectProd.Price1 != DataObjectProd.Price2)
            {
                LitPrice2.Text = string.Format("Spezialpreis CHF {0}", DataObjectProd.Price2.Value.ToString("0.00"));
            }
            else
            {
                PhPrice2.Visible = false;
            }
            if (DataObjectProd.Price1.HasValue && DataObjectProd.Price2.HasValue && DataObjectProd.Price1 != DataObjectProd.Price2)
            {
                litAbonSavings.Text = string.Format("<div style=\"font-weight: bolder\">Sie sparen CHF {0}</div>", (DataObjectProd.Price1.Value - DataObjectProd.Price2.Value).ToString("0.00"));
            }
            else
            {
                litAbonSavings.Visible = false;
            }
            LbtnAdd.ToolTip = GuiLanguage.GetGuiLanguage("DataObjectProduct").GetString("TooltipAddToCart");
            LnkTitle.Text = DataObjectProd.Title;
            LnkTitle.NavigateUrl = Helper.GetDetailLink(DataObjectProd.ObjectType, DataObjectProd.ObjectID.Value.ToString());
            LnkMoreInfo.NavigateUrl = LnkTitle.NavigateUrl;
            LnkMoreInfo.CssClass = "CSB_link2";
            //if (ShowDesc)
            //{
            LitDesc.Text = DataObjectProd.DescriptionLinked;
            if (string.IsNullOrEmpty(LitDesc.Text))
                LitDesc.Text = "-";
            //}
            Img1.ImageUrl = SiteConfig.MediaDomainName + DataObjectProd.GetImage(PictureVersion.S);

            LnkImg.NavigateUrl = LnkTitle.NavigateUrl;
            //LnkTitle.Visible = ShowTitle;
            //LitDesc.Visible = ShowDesc;

            PhTitle.Visible = true;
            PhDesc.Visible = true;
            bool AddToCartOnlyAuthenticated = Convert.ToBoolean(ShopSection.CachedInstance.ShopSettings["AddToCartOnlyAuthenticated"]);
            if (AddToCartOnlyAuthenticated)
                phOrder.Visible = false;
            try
            {
                litAbonSavings.ID = null;
                LitDesc.ID = null;
                LitPrice1.ID = null;
                LitPrice2.ID = null;
                LnkMoreInfo.ID = null;
                LnkImg.ID = null;
                LnkTitle.ID = null;
                Img1.ID = null;
            }
            catch { }
        }

        protected void OnBasketAddClick(object sender, EventArgs e)
        {
            string url = string.Format("{0}?AQTY=1&OID={1}&COM={2}&ReturnURL={3}", ShopSection.CachedInstance.ShopSettings["AddToCartHanlderURL"], DataObjectProd.ObjectID.Value, HttpUtility.UrlEncode(TxtCom.Text.Substring(0, Math.Min(TxtCom.Text.Length, 256))), Server.UrlEncode(Request.RawUrl));
            Response.Redirect(url, true);
        }
    }
}
