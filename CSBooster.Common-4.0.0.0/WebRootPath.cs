// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;

namespace _4screen.CSB.Common
{
    public class WebRootPath
    {
        private static WebRootPath objInstance = null;
        private static string strPath;
        private static string strAppPath;
        private static bool blnIsWebApp;

        private WebRootPath()
        {
        }

        public static WebRootPath Instance
        {
            get
            {
                if (objInstance == null)
                    Initialize();
                return objInstance;
            }
        }

        public override string ToString()
        {
            return strPath;
        }

        public bool IsWebApp
        {
            get { return blnIsWebApp; }
        }

        public string ApplicationPath
        {
            get { return strAppPath; }
        }

        private static void Initialize()
        {
            objInstance = new WebRootPath();
            strPath = string.Empty;
            blnIsWebApp = false;
            try
            {
                if (System.Web.HttpContext.Current != null)
                {
                    strPath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath;
                    strAppPath = strPath;
                    if (System.IO.Directory.Exists(strPath))
                    {
                        blnIsWebApp = true;
                        return;
                    }
                }
                else
                {
                    strAppPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, @"\");
                }

                strPath = System.Configuration.ConfigurationManager.AppSettings["PhysicalApplicationPath"];
                if (System.IO.Directory.Exists(strPath))
                    return;

                strPath = System.Configuration.ConfigurationManager.AppSettings["PhysicalApplicationPath_Secondary"];
                if (System.IO.Directory.Exists(strPath))
                    return;

                strPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, @"\");
            }
            catch
            {
                strAppPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, @"\");

                strPath = System.Configuration.ConfigurationManager.AppSettings["PhysicalApplicationPath"];
                if (System.IO.Directory.Exists(strPath))
                    return;

                strPath = System.Configuration.ConfigurationManager.AppSettings["PhysicalApplicationPath_Secondary"];
                if (System.IO.Directory.Exists(strPath))
                    return;

                strPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, @"\");
            }
        }
    }
}