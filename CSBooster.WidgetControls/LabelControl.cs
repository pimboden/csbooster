using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;

namespace _4screen.CSB.WidgetControls
{
    [DefaultProperty("LabelKey")]
    [ToolboxData("<{0}:LabelControl runat=server></{0}:LabelControl>")]
    public class LabelControl : Control
    {
        [Category("Appearance")]
        [DefaultValue("")]
        public string LabelKey
        {
            get;
            set;
        }

        [Category("Appearance")]
        [DefaultValue("")]
        public string LabelFile
        {
            get;
            set;
        }

        [Category("Appearance")]
        [DefaultValue("")]
        public string ToolTipKey
        {
            get;
            set;
        }

        [Category("Appearance")]
        [DefaultValue("")]
        public string ToolTipFile
        {
            get;
            set;
        }

        [Category("Appearance")]
        [DefaultValue(false)]
        public bool ToolTipLeft
        {
            get;
            set;
        }

        [Category("Misc")]
        [DefaultValue("")]
        public string CssClass
        {
            get;
            set;
        }

        public override void RenderControl(HtmlTextWriter output)
        {
            if (!string.IsNullOrEmpty(this.LabelKey) && !string.IsNullOrEmpty(this.LabelFile))
            {
                string text = GuiLanguage.GetGuiLanguage(this.LabelFile).GetString(this.LabelKey).Trim();
                string tooltip = null;
                if (!string.IsNullOrEmpty(this.ToolTipKey))
                {
                    if (!string.IsNullOrEmpty(this.ToolTipFile))
                        tooltip = GuiLanguage.GetGuiLanguage(ToolTipFile).GetStringForTooltip(ToolTipKey);
                    else
                        tooltip = GuiLanguage.GetGuiLanguage(LabelFile).GetStringForTooltip(ToolTipKey);
                }

                if (!this.ToolTipLeft)
                {
                    if (!string.IsNullOrEmpty(text))
                    {
                        if (string.IsNullOrEmpty(this.CssClass))
                            output.Write(text);
                        else
                            output.Write(string.Format("<span class=\"{0}\">{1}</span>", this.CssClass, text));
                    }

                    if (!string.IsNullOrEmpty(this.ToolTipKey))
                    {
                        if (!string.IsNullOrEmpty(tooltip))
                            output.Write(string.Format("<a href=\"javascript:void(0)\" class=\"CSB_help2\" tabindex=\"-1\" onmouseover=\"tooltip.show('{0}')\" onmouseout=\"tooltip.hide()\"><img src=\"{1}/Library/Images/Layout/cmd_help.gif\"/></a>", tooltip, SiteConfig.SiteVRoot));
                        else
                            output.Write(string.Format("<img class=\"CSB_nohelp2\" src=\"{0}/Library/Images/Layout/cmd_nohelp.gif\"/>", SiteConfig.SiteVRoot));
                    }
                    else
                    {
                        output.Write(string.Format("<img class=\"CSB_nohelp2\" src=\"{0}/Library/Images/Layout/cmd_nohelp.gif\"/>", SiteConfig.SiteVRoot));
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.ToolTipKey))
                    {
                        if (!string.IsNullOrEmpty(tooltip))
                            output.Write(string.Format("<a href=\"javascript:void(0)\" class=\"CSB_help2\" tabindex=\"-1\" onmouseover=\"tooltip.show('{0}')\" onmouseout=\"tooltip.hide()\"><img src=\"{1}/Library/Images/Layout/cmd_help.gif\"/></a>", tooltip, SiteConfig.SiteVRoot));
                        else
                            output.Write(string.Format("<img class=\"CSB_nohelp2\" src=\"{0}/Library/Images/Layout/cmd_nohelp.gif\"/>", SiteConfig.SiteVRoot));
                    }
                    else
                    {
                        output.Write(string.Format("<img class=\"CSB_nohelp2\" src=\"{0}/Library/Images/Layout/cmd_nohelp.gif\"/>", SiteConfig.SiteVRoot));
                    }

                    if (!string.IsNullOrEmpty(text))
                    {
                        if (string.IsNullOrEmpty(this.CssClass))
                            output.Write("&nbsp;" + text);
                        else
                            output.Write(string.Format("&nbsp;<span class=\"{0}\">{1}</span>", this.CssClass, text));
                    }
                }
            }
        }
    }
}
