// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Linq;
using System.Web;
using System.Web.Services;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.DataObj.Business;

namespace _4screen.CSB.DataObj.Pages.Other
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http:/www.sieme.net/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AddToCartHandler : IHttpHandler
    {

        private HttpResponse Response;
        private HttpRequest Request;
        private string RetURL;

        public void ProcessRequest(HttpContext context)
        {
            Response = context.Response;
            Request = context.Request;
            RetURL = Request.QueryString["ReturnUrl"];
            Guid? productID = Request.QueryString["OID"].ToNullableGuid();
            string Comments = HttpUtility.UrlDecode(Request.QueryString["COM"]);
            int? quantiyty = null;
            Guid currentUser = Request.IsAuthenticated ? UserDataContext.GetUserDataContext().UserID : UserDataContext.GetUserDataContext().AnonymousUserId;
            if(!string.IsNullOrEmpty(Request.QueryString["QTY"]))
            {
                quantiyty = Convert.ToInt32(Request.QueryString["QTY"]);
            }
            int addQuantity = 1;
            if (!string.IsNullOrEmpty(Request.QueryString["AQTY"]))
                addQuantity = Convert.ToInt32(Request.QueryString["AQTY"]);

            bool AddToCartOnlyAuthenticated = Convert.ToBoolean(ShopSection.CachedInstance.ShopSettings["AddToCartOnlyAuthenticated"]);
            if (AddToCartOnlyAuthenticated && !Request.IsAuthenticated)
            {
                //go to the LoginPage

                string LoginURL = string.Format("/Pages/Other/Login.aspx?ReturnUrl={0}", context.Server.UrlEncode(Request.RawUrl));
                Response.Redirect(LoginURL);
            }

            //DELETE ALL ACTIVE CARTS FORM THE USER; THAT ARE NOT IN THE TIMERANGE
            ShopDataContext csb = new ShopDataContext();
            hitbl_ShoppingCart_Cart_SHO currentCart = null;
            var activeCart = from allCarts in csb.hitbl_ShoppingCart_Cart_SHOs.Where(x => x.SHO_CARTSTATUS == 0 && x.SHO_USERID == currentUser)
                             select allCarts;
            foreach (var cart in activeCart)
            {
                if (cart.SHO_DATEADDED.AddMinutes(Convert.ToDouble(ShopSection.CachedInstance.ShopSettings["CartActiveRangeMinutes"])) < DateTime.Now)
                {
                    csb.hitbl_ShoppingCart_Cart_SHOs.DeleteOnSubmit(cart);
                }
            }
            csb.SubmitChanges();
            currentCart = activeCart.SingleOrDefault();
            if (currentCart == null)
            {
                currentCart = new hitbl_ShoppingCart_Cart_SHO();
                currentCart.SHO_CARTSTATUS = 0;
                currentCart.SHO_DATEADDED = DateTime.Now;
                currentCart.SHO_CARTID = Guid.NewGuid();
                currentCart.SHO_USERID = currentUser;
                currentCart.SHO_TAXES = Convert.ToDouble(ShopSection.CachedInstance.ShopSettings["TaxesPercent"]);
                //currentCart.SHO_TRANSPORT = Convert.ToDouble(ShopSection.CachedInstance.ShopSettings["TransportCosts"]);
                csb.hitbl_ShoppingCart_Cart_SHOs.InsertOnSubmit(currentCart);

            }
            //Check if an Item allready exists, in that case add the addQuantity
            //else add a new Item with the addQuantity
            hitbl_ShoppingCart_Items_ITE currItem;
            if (productID.HasValue)
            {
                currItem = (from items in currentCart.hitbl_ShoppingCart_Items_ITEs.Where(x => x.OBJ_ID == productID.Value)
                            select items).SingleOrDefault();
                if (currItem == null )
                {
                    if (addQuantity <= 0 || (quantiyty.HasValue && quantiyty.Value < 0))
                        return;
                    DataObjectProduct doProduct =DataObject.Load<DataObjectProduct>(productID);
                    if (doProduct != null)
                    {
                        currItem = new hitbl_ShoppingCart_Items_ITE();
                        currItem.ITE_ITEM_ID = Guid.NewGuid();
                        currItem.ITE_QUANTITY = quantiyty.HasValue ? quantiyty.Value : addQuantity;
                        currItem.ITE_REFNR = doProduct.ProductRef;
                        currItem.ITE_TEXT = doProduct.Description;
                        currItem.ITE_TITEL = doProduct.Title;
                        currItem.ITE_PRICE1 = doProduct.Price1;
                        currItem.ITE_PRICE2 = doProduct.Price2;
                        currItem.ITE_PRICE3 = doProduct.Price3;
                        currItem.ITE_Porto = doProduct.Porto;
                        currItem.OBJ_ID = productID.Value;
                        if (!string.IsNullOrEmpty(Comments))
                        {
                            currItem.ITE_COMENT = Comments.CropString(256);
                        }
                        currentCart.hitbl_ShoppingCart_Items_ITEs.Add(currItem);
                    }
                    
                }
                else
                {
                    currItem.ITE_QUANTITY = quantiyty.HasValue ? quantiyty.Value : currItem.ITE_QUANTITY + addQuantity;
                    if (!string.IsNullOrEmpty(Comments))
                    {
                        currItem.ITE_COMENT = Comments.CropString(256);
                    }
                    if (currItem.ITE_QUANTITY <= 0)
                        currentCart.hitbl_ShoppingCart_Items_ITEs.Remove(currItem);
                }
                currentCart.SHO_DATEADDED = DateTime.Now; //Keep the Cart Time alive!
                csb.SubmitChanges();
            }

            Response.Redirect(RetURL);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}