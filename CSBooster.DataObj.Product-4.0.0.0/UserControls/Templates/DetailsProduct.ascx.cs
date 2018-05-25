// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using Telerik.Web.UI;
using SiteConfig=_4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.DataObj.UserControls.Templates
{
    public partial class DetailsProduct : UserControl, ISettings, IDataObjectWorker
    {
        private Business.DataObjectProduct dataObjectProduct;
        public DataAccess.Business.DataObject DataObject { get; set; }

        private DataObjectList<DataObjectPicture> prodPictures;
        private Dictionary<Guid, string> OffersByTag;
        protected string WidthColumn = "100%";
        protected string WidthImg = "158px";
        protected string WidthCnt = "464px";
        #region ISettings Members

        public Dictionary<string, object> Settings { get; set; }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Settings != null)
                {
                    if (Settings.ContainsKey("Width"))
                    {
                        WidthImg = string.Format("{0}px", 325);
                        WidthCnt = string.Format("{0}px", (int)Settings["Width"] - 325);
                    }
                }

                if (DataObject is Business.DataObjectProduct)
                    dataObjectProduct = (Business.DataObjectProduct)DataObject;
                else
                    dataObjectProduct = DataAccess.Business.DataObject.Load<Business.DataObjectProduct>(DataObject.ObjectID, null, false);


                PrintOutput();

                // Set widget title
                Control widgetControl = WidgetHelper.GetWidgetHost(this, 0, 5);
                ((Literal)widgetControl.FindControl("LitTitle")).Text = string.Format("<h1>{0}</h1>", dataObjectProduct.Title);

                if ((PageType)Settings["ParentPageType"] == PageType.Detail)
                {
                    ((IWidgetPageMaster)Page.Master).BreadCrumb.RenderDetailPageBreadCrumbs(dataObjectProduct);
                }
            }
            catch
            {
            }
        }



        private void PrintOutput()
        {
            RTTM.Visible = false;
            RTTMIMG.Visible = false;
            if (dataObjectProduct != null)
            {
                foreach (var tooltipId in AdWordHelper.GetCampaignObjectIds(dataObjectProduct.ObjectID.Value))
                {
                    RTTM.TargetControls.Add(tooltipId, true);
                    RTTM.Visible = true;
                }
            }


            prodPictures =
                DataObjects.Load<DataObjectPicture>(new QuickParameters
                                                        {
                                                            ObjectType = Helper.GetObjectTypeNumericID("Picture"),
                                                            RelationParams =
                                                                new RelationParams
                                                                    {
                                                                        ParentObjectID = dataObjectProduct.ObjectID,
                                                                    },
                                                            ShowState = ObjectShowState.Published,
                                                            Amount = 0,
                                                            Direction = QuickSortDirection.Asc,
                                                            PageNumber = 0,
                                                            PageSize = 999999,
                                                            SortBy = QuickSort.RelationSortNumber,
                                                            Udc = UserDataContext.GetUserDataContext()
                                                        });
            if (prodPictures.Count > 0)
            {
                LitContent.Text = string.Format("{0}", dataObjectProduct.ProductTextLinked);

                Image img = new Image();
                img.ImageUrl = string.Format("{0}{1}", SiteConfig.MediaDomainName, prodPictures[0].GetImage(PictureVersion.L));
                img.AlternateText = prodPictures[0].Title;
                img.ToolTip = prodPictures[0].Title;
                img.Width = 305;
                ProdImg1.Controls.Add(img);

                ProdImg1.Visible = true;


                for (int i = 1; i < prodPictures.Count; i++)
                {
                    var picture = prodPictures[i];
                    string imageId = "Img_" + picture.ObjectID.Value.ToString();
                    RTTMIMG.TargetControls.Add(imageId, true);
                    RTTMIMG.Visible = true;
                    LiteralControl image =
                        new LiteralControl(
                            string.Format(
                                "<div style=\"float:left;width:110px;\"><div><img class ='articlepic' src=\"{0}{1}\" id=\"{2}\" /></div><div>{3}</div></div>",
                                SiteConfig.MediaDomainName, picture.GetImage(PictureVersion.XS), imageId, picture.Title));
                    PhProdImgs.Controls.Add(image);
                }
            }
            else
            {
                ProdImg1.Visible = false;
                LitContent.Text = string.Format("{0}", dataObjectProduct.ProductTextLinked);

            }
            if (dataObjectProduct.Price1.HasValue && dataObjectProduct.Price2.HasValue && dataObjectProduct.Price1 != dataObjectProduct.Price2)
            {
                litSM.Text = string.Format("Sie sparen CHF {0} ", (dataObjectProduct.Price1.Value - dataObjectProduct.Price2.Value).ToString("0.00"));
                PnlSpecPrice.Visible = true;
            }
            else
            {
                litSM.Visible = false;
            }
            litP1.Text = dataObjectProduct.Price1.HasValue ? dataObjectProduct.Price1.Value.ToString("0.00") : "0.00";
            litP2.Text = dataObjectProduct.Price2.HasValue ? dataObjectProduct.Price2.Value.ToString("0.00") : "0.00";
            LbtnAdd.ToolTip = GuiLanguage.GetGuiLanguage("dataObjectProductuct").GetString("TooltipAddToCart");
        }

        protected void OnBasketAddClick(object sender, EventArgs e)
        {
            string url = string.Format("{0}?AQTY=1&OID={1}&COM={2}&ReturnURL={3}", ShopSection.CachedInstance.ShopSettings["AddToCartHanlderURL"], dataObjectProduct.ObjectID.Value, HttpUtility.UrlEncode(TxtCom.Text.Substring(0, Math.Min(TxtCom.Text.Length, 256))), Server.UrlEncode(Request.RawUrl));
            Response.Redirect(url, true);
        }

        protected void OnAjaxUpdate(object sender, ToolTipUpdateEventArgs e)
        {
            string[] tooltipId = e.TargetControlID.Split(new char[] { '_' });
            if (tooltipId.Length == 4)
            {
                Literal literal = new Literal
                                      {
                                          Text =
                                              AdWordHelper.GetCampaignContent(new Guid(tooltipId[0]),
                                                                              new Guid(tooltipId[1]),
                                                                              UserDataContext.GetUserDataContext(),
                                                                              tooltipId[2],
                                                                              "Popup")
                                      };
                literal.Text = Regex.Replace(literal.Text,
                                             @"(/Pages/Other/AdCampaignRedirecter.aspx\?CID=\w{8}-\w{4}-\w{4}-\w{4}-\w{12})",
                                             "$1&OID=" + tooltipId[1] + "&Word=" + tooltipId[2] + "&Type=PopupLink");
                e.UpdatePanel.ContentTemplateContainer.Controls.Add(literal);
            }
            else if (e.TargetControlID.IndexOf("Img_") > -1)
            {
                try
                {
                    string pictureId = e.TargetControlID.Substring(4);
                    DataObjectPicture picture = prodPictures.Find(x => x.ObjectID.Value == pictureId.ToGuid());
                    Literal literal = new Literal
                                          {
                                              Text =
                                                  string.Format(
                                                  "<div><div><img src=\"{0}{1}\"></div><div>{2}</div></div>",
                                                  SiteConfig.MediaDomainName, picture.GetImage(PictureVersion.L),
                                                  picture.Title)
                                          };
                    e.UpdatePanel.ContentTemplateContainer.Controls.Add(literal);
                }
                catch
                {
                }
            }
        }
    }
}