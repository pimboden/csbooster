using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class StyleSettings : System.Web.UI.UserControl
    {
        public string Type { get; set; }
        public string TargetClass { get; set; }
        public bool LinkColorEnabled { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.RcbBorder.Items.FindItemByValue("solid").Text = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("LabelLineStyleSolid");
            this.RcbBorder.Items.FindItemByValue("dashed").Text = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("LabelLineStyleDashed");

            this.RcpText.OnClientColorChange = string.Format("On{0}Change", Type);
            this.RcpLink.OnClientColorChange = string.Format("On{0}Change", Type);
            this.RcpBackground.OnClientColorChange = string.Format("On{0}Change", Type);
            this.CbBackground.Attributes.Add("onClick", string.Format("On{0}Change()", Type));
            this.CbVertical.Attributes.Add("onClick", string.Format("On{0}Change()", Type));
            this.CbHorizontal.Attributes.Add("onClick", string.Format("On{0}Change()", Type));
            this.RcpBorder.OnClientColorChange = string.Format("On{0}Change", Type);
            this.RcbBorder.OnClientSelectedIndexChanged = string.Format("On{0}Change", Type);
            this.RntbBorder.Attributes.Add("onChange", string.Format("On{0}Change()", Type));

            LnkBackground.NavigateUrl = string.Format("javascript:radWinOpen('/Pages/popups/SimplePictureUploadGallery.aspx?ParentId={0}&Callback=Set{1}Background', 'Bild auswählen', 630, 460, false, null, 'PictureWindow')", Request.QueryString["ParentId"], Type);

            PnlLinkColor.Visible = LinkColorEnabled;
        }

        public void SetStyleSettings(_4screen.CSB.DataAccess.Business.StyleSettings settings)
        {
            if (!string.IsNullOrEmpty(settings.TextColor))
                this.RcpText.SelectedColor = settings.TextColor.ToColor();
            if (!string.IsNullOrEmpty(settings.LinkColor))
                this.RcpLink.SelectedColor = settings.LinkColor.ToColor();
            if (!string.IsNullOrEmpty(settings.BackgroundColor))
                this.RcpBackground.SelectedColor = settings.BackgroundColor.ToColor();
            if (!string.IsNullOrEmpty(settings.BackgroundImage))
                this.TxtBackground.Text = settings.BackgroundImage;
            this.CbBackground.Checked = settings.BackgroundImageActive;
            this.CbVertical.Checked = settings.VerticalRepetition;
            this.CbHorizontal.Checked = settings.HorizontalRepetition;
            if (!string.IsNullOrEmpty(settings.BorderColor))
                this.RcpBorder.SelectedColor = settings.BorderColor.ToColor();
            this.RcbBorder.SelectedValue = settings.BorderType;
            if (settings.BorderWidth > 0)
                this.RntbBorder.Value = settings.BorderWidth;
        }

        public _4screen.CSB.DataAccess.Business.StyleSettings GetStyleSettings()
        {
            _4screen.CSB.DataAccess.Business.StyleSettings settings = new _4screen.CSB.DataAccess.Business.StyleSettings();
            if (this.RcpText.SelectedColor != Color.Empty)
                settings.TextColor = this.RcpText.SelectedColor.ToHex();
            if (this.RcpLink.SelectedColor != Color.Empty)
                settings.LinkColor = this.RcpLink.SelectedColor.ToHex();
            if (this.RcpBackground.SelectedColor != Color.Empty)
                settings.BackgroundColor = this.RcpBackground.SelectedColor.ToHex();
            if (!string.IsNullOrEmpty(this.TxtBackground.Text))
                settings.BackgroundImage = this.TxtBackground.Text;
            settings.BackgroundImageActive = this.CbBackground.Checked;
            settings.VerticalRepetition = this.CbVertical.Checked;
            settings.HorizontalRepetition = this.CbHorizontal.Checked;
            if (this.RcpText.SelectedColor != Color.Empty)
            {
                settings.TextColor = Common.Extensions.ToHex(this.RcpText.SelectedColor);
                settings.BorderType = this.RcbBorder.SelectedValue;
                settings.BorderWidth = (int)this.RntbBorder.Value.Value;
            }
            return settings;
        }
    }
}