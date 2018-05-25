// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Linq;
using System.Xml.Linq;
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class Copyrights : System.Web.UI.UserControl
    {
        private string selectetValue;
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");
        private XDocument copyrightsConfig;
        private XElement config;

        public string SelectedValue
        {
            get { return DDCopyrights.SelectedValue; }
            set { selectetValue = value; }
        }

        public bool IsVisible
        {
            get { return Fs.Visible; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            copyrightsConfig = Helper.LoadConfig("Copyrights.config", string.Format("{0}/Configurations/Copyrights.config", WebRootPath.Instance.ToString()));
            config = copyrightsConfig.Element("Copyrights");
            if (config.HasAttributes)
                Fs.Visible = config.Attribute("Visible").Value.ToLower() == "true";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var copyrights = (from copyright in config.Elements("Copyright")
                              select new
                              {
                                  Value = copyright.Attribute("Value").Value,
                                  Name = copyright.Attribute("Name").Value,
                                  Selected = copyright.Attribute("Selected") != null ? bool.Parse(copyright.Attribute("Selected").Value) : false
                              }).ToList();
            var copyrightList = copyrights.ToList();

            DDCopyrights.DataSource = copyrightList;
            DDCopyrights.DataBind();

            if (!string.IsNullOrEmpty(selectetValue))
                DDCopyrights.SelectedIndex = DDCopyrights.Items.IndexOf(DDCopyrights.Items.FindItemByValue(selectetValue));
            else
                DDCopyrights.SelectedValue = copyrights.Find(x => x.Selected).Value;

            Fs.ID = null;
        }
    }
}