// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System.Collections.Generic;
using System.IO;

namespace _4screen.CSB.Common
{
    public class Layouts
    {
        private static List<Layout> layouts = new List<Layout>();

        static Layouts()
        {
            string[] layoutFolders = Directory.GetDirectories(string.Format("{0}\\App_Layouts", WebRootPath.Instance));
            foreach (string layoutFolder in layoutFolders)
            {
                FileInfo fileInfo = new FileInfo(layoutFolder);
                Layout layout = new Layout(fileInfo.Name);
                layouts.Add(layout);
            }
        }

        public static List<Layout> GetLayouts()
        {
            return layouts;
        }

        public static Layout GetLayout(string layoutName)
        {
            return layouts.Find(x => x.Name == layoutName);
        }
    }
}
