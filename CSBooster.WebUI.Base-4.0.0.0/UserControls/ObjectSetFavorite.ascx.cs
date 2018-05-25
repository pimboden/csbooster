//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		06.09.2007 / AW
//******************************************************************************

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class ObjectSetFavorite : System.Web.UI.UserControl
    {
        public DataObject DataObject { get; set; }
        private UserDataContext udc = UserDataContext.GetUserDataContext();
        private GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.DataObject != null)
            {
                if (IsFavorite())
                {
                    this.LbtFlipFlop.CommandArgument = "remove";
                    this.LbtFlipFlop.CssClass = "CSB_btn_is_favorite";
                    this.LbtFlipFlop.ToolTip = language.GetString("TooltipIsFavorite");
                }
                else
                {
                    this.LbtFlipFlop.CommandArgument = "add";
                    this.LbtFlipFlop.CssClass = "CSB_btn_isnot_favorite";
                    this.LbtFlipFlop.ToolTip = language.GetString("TooltipIsNotFavorite");
                }
            }

            if (UserProfile.Current.IsAnonymous)
            {
                this.LbtFlipFlop.ToolTip = language.GetString("TooltipLoginForFavorites");
            }
        }

        private bool IsFavorite()
        {
            return DataObject.IsObjectFavaorite(this.DataObject.ObjectID.Value, udc.UserID);
            //return (DataObjects.Load<DataObjectUser>(new QuickParameters() {
            //    Amount = 1, 
            //    IgnoreCache = true,
            //    DisablePaging = true,
            //    SortBy = QuickSort.NotSorted,
            //    Udc = udc,
            //    RelationParams = new RelationParams() {
            //        ChildObjectID = this.DataObject.ObjectID.Value,
            //        RelationType = "Favorites",
            //        Udc = udc
            //    }
            //}).Count > 0);
        }

        protected void LbtFlipFlop_Click(object sender, EventArgs e)
        {
            if (!UserProfile.Current.IsAnonymous)
            {
                LinkButton lbt = sender as LinkButton;
                if (lbt != null && !string.IsNullOrEmpty(lbt.CommandArgument))
                {
                    if (lbt.CommandArgument == "remove")
                    {
                        DataObject.RemoveFromFavorite(udc, this.DataObject.ObjectID.Value);
                        //DataObject.RelDelete(new RelationParams() { Udc = udc, ChildObjectID = this.DataObject.ObjectID.Value, ParentObjectID = udc.UserID, RelationType = "Favorites" });
                        this.LbtFlipFlop.CommandArgument = "add";
                        this.LbtFlipFlop.CssClass = "CSB_btn_isnot_favorite";
                        this.LbtFlipFlop.ToolTip = language.GetString("TooltipIsNotFavorite");
                    }
                    else
                    {
                        DataObject.AddToFavorite(udc, this.DataObject.ObjectID.Value, this.DataObject.ObjectType);
                        //DataObject.RelInsert(new RelationParams() { Udc = udc, ChildObjectID = this.DataObject.ObjectID.Value, ChildObjectType = this.DataObject.ObjectType, ParentObjectID = udc.UserID, ParentObjectType = 2, RelationType = "Favorites" }, 0);
                        this.LbtFlipFlop.CommandArgument = "remove";
                        this.LbtFlipFlop.CssClass = "CSB_btn_is_favorite";
                        this.LbtFlipFlop.ToolTip = language.GetString("TooltipIsFavorite");
                    }
                }
            }
        }
    }
}