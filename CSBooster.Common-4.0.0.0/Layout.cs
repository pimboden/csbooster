// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace _4screen.CSB.Common
{
    public class Layout
    {
        private Dictionary<int, int> columnWidths = new Dictionary<int, int>();
        private string dragDropTemplate;
        private string displayTemplate;

        public int NumberDropZones { get; set; }
        public string Roles { get; set; }
        public string PageTypes { get; set; }
        public string Name { get; set; }

        public Dictionary<int, int> ColumnWidths
        {
            get { return columnWidths; }
            set { columnWidths = value; }
        }

        public string DragDropTemplate
        {
            get { return dragDropTemplate; }
            set { dragDropTemplate = value; }
        }

        public string DisplayTemplate
        {
            get { return displayTemplate; }
            set { displayTemplate = value; }
        }

        public Layout(string name)
        {
            this.Name = name;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(string.Format("{0}\\App_Layouts\\{1}\\settings.config", WebRootPath.Instance, name));
            XmlNode xmlNode = xmlDoc.SelectSingleNode("//Layout");
            Roles = xmlNode.Attributes["Roles"].Value;
            PageTypes = xmlNode.Attributes["PageTypes"].Value;

            TextReader textReader = new StreamReader(string.Format("{0}\\App_Layouts\\{1}\\layout.html", WebRootPath.Instance, name));
            dragDropTemplate = textReader.ReadToEnd();
            displayTemplate = dragDropTemplate;
            textReader.Close();

            MatchCollection matches = Regex.Matches(dragDropTemplate, @"<%DROPZONE index=""(\d*?)"" width=""(\d*?)""%>", RegexOptions.IgnoreCase);

            foreach (Match match in matches)
            {
                int index = int.Parse(match.Groups[1].Value);
                columnWidths.Add(index, int.Parse(match.Groups[2].Value));

                string dragDropZone = string.Format(@"
                            <asp:Panel ID=""WCP{0}"" runat=""server"" name=""widgetHolder"" columnNo=""{0}"">
                                <div id=""WCDC{0}"" class=""widgetDropCue"">
                                </div>
                            </asp:Panel>
                            <cdd:CustomDragDropExtender ID=""CDDE{0}"" runat=""server"" TargetControlID=""WCP{0}"" DragItemClass=""widget"" DragItemHandleClass=""widgetHeaderMove"" DropCueID=""WCDC{0}"" OnClientDrop=""OnWidgetDrop"" EnableViewState=""false"" />
                    ", index);
                dragDropTemplate = Regex.Replace(dragDropTemplate, string.Format("<%DROPZONE index=\"{0}\" width=\"{1}\"%>", index, columnWidths[index]), dragDropZone, RegexOptions.IgnoreCase);

                string displayZone = string.Format(@"<asp:Panel ID=""WCP{0}"" runat=""server"" />", index);
                displayTemplate = Regex.Replace(displayTemplate, string.Format("<%DROPZONE index=\"{0}\" width=\"{1}\"%>", index, columnWidths[index]), displayZone, RegexOptions.IgnoreCase);

                NumberDropZones++;
            }
            dragDropTemplate = Regex.Replace(dragDropTemplate, @"\r\n", "");
            dragDropTemplate = Regex.Replace(dragDropTemplate, @">\s*?<", "><");
            displayTemplate = Regex.Replace(displayTemplate, @"\r\n", "");
            displayTemplate = Regex.Replace(displayTemplate, @">\s*?<", "><");

            dragDropTemplate = string.Format(@"<%@ Register Assembly=""CSBooster.CustomDragDrop"" Namespace=""CustomDragDrop"" TagPrefix=""cdd"" %>{0}", dragDropTemplate);
        }
    }
}
