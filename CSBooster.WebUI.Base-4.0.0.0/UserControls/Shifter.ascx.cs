// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class Shifter : System.Web.UI.UserControl
    {
        private int pageSize;
        private string itemNameSingular;
        private string itemNamePlural;
        private string customText;
        private Control shiftableControl;
        private string buttonNextUrlActive = "~/Library/Images/Layout/cmd_next.png";
        private string buttonLastUrlActive = "~/Library/Images/Layout/cmd_last.png";
        private string buttonPrevUrlActive = "~/Library/Images/Layout/cmd_prev.png";
        private string buttonFirstUrlActive = "~/Library/Images/Layout/cmd_first.png";
        private string buttonNextUrlInactive = "~/Library/Images/Layout/cmd_next_i.png";
        private string buttonLastUrlInactive = "~/Library/Images/Layout/cmd_last_i.png";
        private string buttonPrevUrlInactive = "~/Library/Images/Layout/cmd_prev_i.png";
        private string buttonFirstUrlInactive = "~/Library/Images/Layout/cmd_first_i.png";
        private string cssClassPager = "CSB_msg_pag";
        private string cssClassButtons = "CSB_msg_pag_btn";

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        public string ItemNameSingular
        {
            get { return itemNameSingular; }
            set { itemNameSingular = value; }
        }

        public string ItemNamePlural
        {
            get { return itemNamePlural; }
            set { itemNamePlural = value; }
        }

        public string CustomText
        {
            get { return customText; }
            set { customText = value; }
        }

        public Control ShiftableControl
        {
            get { return shiftableControl; }
            set { shiftableControl = value; }
        }

        public string ButtonNextUrlActive
        {
            get { return buttonNextUrlActive; }
            set { buttonNextUrlActive = value; }
        }

        public string ButtonLastUrlActive
        {
            get { return buttonLastUrlActive; }
            set { buttonLastUrlActive = value; }
        }

        public string ButtonPrevUrlActive
        {
            get { return buttonPrevUrlActive; }
            set { buttonPrevUrlActive = value; }
        }

        public string ButtonFirstUrlActive
        {
            get { return buttonFirstUrlActive; }
            set { buttonFirstUrlActive = value; }
        }

        public string ButtonNextUrlInactive
        {
            get { return buttonNextUrlInactive; }
            set { buttonNextUrlInactive = value; }
        }

        public string ButtonLastUrlInactive
        {
            get { return buttonLastUrlInactive; }
            set { buttonLastUrlInactive = value; }
        }

        public string ButtonPrevUrlInactive
        {
            get { return buttonPrevUrlInactive; }
            set { buttonPrevUrlInactive = value; }
        }

        public string ButtonFirstUrlInactive
        {
            get { return buttonFirstUrlInactive; }
            set { buttonFirstUrlInactive = value; }
        }

        public string CssClassPager
        {
            get { return cssClassPager; }
            set { cssClassPager = value; }
        }

        public string CssClassButtons
        {
            get { return cssClassButtons; }
            set { cssClassButtons = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.PAGER.CssClass = cssClassPager;
            this.PAGGOFIRST.CssClass = cssClassButtons;
            this.PAGGOLAST.CssClass = cssClassButtons;
            this.PAGGONEXT.CssClass = cssClassButtons;
            this.PAGGOPREV.CssClass = cssClassButtons;
        }

        protected void OnShiftClick(object sender, EventArgs e)
        {
            int numberItems = 0;
            if (shiftableControl == null)
                numberItems = ((IShiftable)this.Page).GetNumberItems();
            else
                numberItems = ((IShiftable)shiftableControl).GetNumberItems();
            int currentItem = int.Parse(((ImageButton)sender).CommandArgument);

            this.InitShifter(currentItem, numberItems);

            if (shiftableControl == null)
                ((IShiftable)this.Page).SetCurrentItem(currentItem);
            else
                ((IShiftable)shiftableControl).SetCurrentItem(currentItem);
        }

        public int CheckShiftRange(int currentItem, int numberItems)
        {
            if (currentItem < 0)
                return 0;
            else if (currentItem >= numberItems)
                return numberItems - 1;
            else
                return currentItem;
        }

        public void InitShifter(int currentItem, int numberItems)
        {
            if (string.IsNullOrEmpty(customText))
            {
                if (numberItems == 1)
                    this.PAGTEXT.Text = numberItems + " " + itemNameSingular;
                else if (numberItems > 1)
                    this.PAGTEXT.Text = numberItems + " " + itemNamePlural;
                else
                    this.PAGTEXT.Text = GuiLanguage.GetGuiLanguage("Shared").GetString("LableNonePlural") + " " + itemNamePlural;
            }
            else
            {
                this.PAGTEXT.Text = customText;
            }

            if (currentItem <= 1 || numberItems == 0 || numberItems <= pageSize)
            {
                this.PAGGOPREV.Enabled = false;
                this.PAGGOPREV.ImageUrl = buttonPrevUrlInactive;
                this.PAGGOFIRST.Enabled = false;
                this.PAGGOFIRST.ImageUrl = buttonFirstUrlInactive;
            }
            else
            {
                this.PAGGOPREV.Enabled = true;
                this.PAGGOPREV.ImageUrl = buttonPrevUrlActive;
                this.PAGGOFIRST.Enabled = true;
                this.PAGGOFIRST.ImageUrl = buttonFirstUrlActive;
            }
            if (currentItem >= (numberItems - 2) || numberItems == 0 || numberItems <= pageSize)
            {
                this.PAGGONEXT.Enabled = false;
                this.PAGGONEXT.ImageUrl = buttonNextUrlInactive;
                this.PAGGOLAST.Enabled = false;
                this.PAGGOLAST.ImageUrl = buttonLastUrlInactive;
            }
            else
            {
                this.PAGGONEXT.Enabled = true;
                this.PAGGONEXT.ImageUrl = buttonNextUrlActive;
                this.PAGGOLAST.Enabled = true;
                this.PAGGOLAST.ImageUrl = buttonLastUrlActive;
            }

            this.PAGGOPREV.CommandArgument = "" + Math.Max(currentItem - pageSize, 0);
            this.PAGGOFIRST.CommandArgument = "0";
            this.PAGGONEXT.CommandArgument = "" + Math.Min(currentItem + pageSize, numberItems - 1);
            this.PAGGOLAST.CommandArgument = "" + (numberItems - 1);
        }
    }
}