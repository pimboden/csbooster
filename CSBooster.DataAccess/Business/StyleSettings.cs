using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace _4screen.CSB.DataAccess.Business
{
    public class StyleSettings
    {
        [XmlAttribute("TextColor")]
        public string TextColor { get; set; }

        [XmlAttribute("LinkColor")]
        public string LinkColor { get; set; }

        [XmlAttribute("BackgroundColor")]
        public string BackgroundColor { get; set; }

        [XmlAttribute("BackgroundImageActive")]
        public bool BackgroundImageActive { get; set; }

        [XmlAttribute("BackgroundImage")]
        public string BackgroundImage { get; set; }

        [XmlAttribute("VerticalRepetition")]
        public bool VerticalRepetition { get; set; }

        [XmlAttribute("HorizontalRepetition")]
        public bool HorizontalRepetition { get; set; }

        [XmlAttribute("BorderColor")]
        public string BorderColor { get; set; }

        [XmlAttribute("BorderType")]
        public string BorderType { get; set; }

        [XmlAttribute("BorderWidth")]
        public int BorderWidth { get; set; }
    }
}
