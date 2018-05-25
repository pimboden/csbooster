﻿//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		26.03.2007 / PI
//  Updated:   
//******************************************************************************

using System.Collections.Generic;
using _4screen.CSB.Common;
using System.Text;
using System;
using System.Diagnostics;
using System.IO;

namespace _4screen.CSB.Widget.UserControls.Templates
{
    public partial class AssemblyInfo : System.Web.UI.UserControl, ISettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetAssemblyInfo");
        protected string SiteVRoot = SiteConfig.SiteVRoot;

        public Dictionary<string, object> Settings { get; set; }
        public string FileNameOnly { get; private set; }
        public string FileName { get; private set; }
        public string FileDescription { get; private set; }
        public string Type { get; private set; }
        public string FileVersion { get; private set; }
        public string ProductVersion { get; private set; }
        public string ProductName { get; private set; }
        public string Copyright { get; private set; }
        public long Size { get; private set; }
        public DateTime DateModified { get; private set; }
        public DateTime CreationTime { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Settings.ContainsKey("File"))
            {
                FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(Settings["File"].ToString());
                this.FileName = fileInfo.FileName;
                this.FileDescription = fileInfo.FileDescription;
                this.Type = fileInfo.GetType().ToString();
                this.FileVersion = fileInfo.FileVersion;
                this.ProductVersion = fileInfo.ProductVersion;
                this.ProductName = fileInfo.ProductName;
                this.Copyright = fileInfo.LegalCopyright;
                FileInfo info = new FileInfo(Settings["File"].ToString());
                this.DateModified = info.LastWriteTime;
                this.CreationTime = info.CreationTime;
                this.Size = info.Length;
                this.FileNameOnly = info.Name;
            }
        }

   }
}
