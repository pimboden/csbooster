// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Web;
using _4screen.Utils.Web;
using System.Web.UI.WebControls;

namespace _4screen.CSB.Widget.UserControls.Templates
{
    public partial class CacheInfo : System.Web.UI.UserControl, ISettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetCacheInfo");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        public Dictionary<string, object> Settings { get; set; }
        public string Key { get; private set; }
        public string Type { get; private set; }
        public long Size { get; private set; }

        protected global::System.Web.UI.WebControls.LinkButton lbtnRemove;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Key = "n/a";
            this.Size = -1;
            this.Type = "n/a";
            if (Settings.ContainsKey("Key"))
            {
                this.Key = Settings["Key"].ToString();
                object cacheObject = HttpRuntime.Cache[this.Key];
                if (cacheObject != null)
                {
                    try
                    {
                        System.IO.MemoryStream m = new System.IO.MemoryStream();

                        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter b = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        b.Serialize(m, cacheObject);
                        this.Size = m.Length;
                    }
                    catch
                    {
                    }
                    try
                    {
                        this.lbtnRemove.CommandArgument = this.Key;
                    }
                    catch
                    { }
                    //try
                    //{
                    //    this.Type = typeof(object).ToString();
                    //}
                    //catch
                    //{ 
                    //}

                }
            }
        }

        protected void OnRemoveClick(object sender, EventArgs e)
        {
            if (sender != null)
            {
                LinkButton lbtn = sender as LinkButton;
                if (lbtn != null)
                {
                    object cacheObject = HttpRuntime.Cache[lbtn.CommandArgument];
                    if (cacheObject != null)
                    {
                        try
                        {
                            HttpRuntime.Cache.Remove(lbtn.CommandArgument);
                        }
                        catch
                        { }
                    }
                }
            }
        }
    }
}
