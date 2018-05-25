// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace _4screen.CSB.Common
{
    public partial class CustomizationSection : ISerializableSection
    {
        public static CustomizationSection CachedInstance
        {
            get { return (CustomizationSection)Helper.LoadSectionFromFile(string.Format(@"{0}\Configurations\Customization.config", WebRootPath.Instance.ToString()), "customizationSection", Constants.CUSTOMIZATION_SECTION_CACHE_KEY); }
        }

        public void Deserialize(System.Xml.XmlReader reader)
        {
            DeserializeSection(reader);
        }

        protected override void DeserializeSection(System.Xml.XmlReader reader)
        {
            if (!reader.Read() || (reader.NodeType != System.Xml.XmlNodeType.Element))
            {
                throw new System.Configuration.ConfigurationErrorsException("Configuration reader expected to find an element", reader);
            }
            this.DeserializeElement(reader, false);
        }

        public string Serialize(System.Configuration.ConfigurationElement parentElement, string name, System.Configuration.ConfigurationSaveMode saveMode)
        {
            return SerializeSection(parentElement, name, saveMode);
        }

        protected override string SerializeSection(System.Configuration.ConfigurationElement parentElement, string name, System.Configuration.ConfigurationSaveMode saveMode)
        {
            System.IO.StringWriter sWriter = new System.IO.StringWriter(System.Globalization.CultureInfo.InvariantCulture);
            System.Xml.XmlTextWriter xWriter = new System.Xml.XmlTextWriter(sWriter);
            xWriter.Formatting = System.Xml.Formatting.None;
            xWriter.Indentation = 4;
            xWriter.IndentChar = ' ';
            this.SerializeToXmlElement(xWriter, name);
            xWriter.Flush();
            return sWriter.ToString();
        }
    }

    public partial class CustomizationTabElementCollection
    {
        public new CustomizationTabElement this[string objectKey]
        {
            get { return (CustomizationTabElement)this.BaseGet(objectKey); }
        }
    }

    public partial class ProfileSettingsElementCollection
    {
        public new ProfileSettingsElement this[string objectKey]
        {
            get { return (ProfileSettingsElement)this.BaseGet(objectKey); }
        }
    }

    public partial class ModuleElementCollection
    {
        public new ModuleElement this[string objectKey]
        {
            get { return (ModuleElement)this.BaseGet(objectKey); }
        }
    }

    public partial class LoginElementCollection
    {
        public new LoginElement this[string objectKey]
        {
            get { return (LoginElement)this.BaseGet(objectKey); }
        }
    }

    public partial class WidgetSection : ISerializableSection
    {
        public static WidgetSection CachedInstance
        {
            get { return (WidgetSection)Helper.LoadSectionFromFile(string.Format(@"{0}\Configurations\Widgets.config", WebRootPath.Instance.ToString()), "widgetSection", Constants.WIDGET_SECTION_CACHE_KEY); }
        }

        public void Deserialize(System.Xml.XmlReader reader)
        {
            DeserializeSection(reader);
        }

        protected override void DeserializeSection(System.Xml.XmlReader reader)
        {
            if (!reader.Read() || (reader.NodeType != System.Xml.XmlNodeType.Element))
            {
                throw new System.Configuration.ConfigurationErrorsException("Configuration reader expected to find an element", reader);
            }
            this.DeserializeElement(reader, false);
        }

        public string Serialize(System.Configuration.ConfigurationElement parentElement, string name, System.Configuration.ConfigurationSaveMode saveMode)
        {
            return SerializeSection(parentElement, name, saveMode);
        }

        protected override string SerializeSection(System.Configuration.ConfigurationElement parentElement, string name, System.Configuration.ConfigurationSaveMode saveMode)
        {
            System.IO.StringWriter sWriter = new System.IO.StringWriter(System.Globalization.CultureInfo.InvariantCulture);
            System.Xml.XmlTextWriter xWriter = new System.Xml.XmlTextWriter(sWriter);
            xWriter.Formatting = System.Xml.Formatting.None;
            xWriter.Indentation = 4;
            xWriter.IndentChar = ' ';
            this.SerializeToXmlElement(xWriter, name);
            xWriter.Flush();
            return sWriter.ToString();
        }
    }

    public partial class WidgetElementCollection
    {
        public IEnumerable<WidgetElement> LINQEnumarable
        {
            get
            {
                return from WidgetElement widgets in this select widgets;
            }
        }

        public WidgetElement this[Guid objectKey]
        {
            get { return (WidgetElement)this.BaseGet(objectKey); }
        }
    }

    public partial class WidgetStepElementCollection
    {
        public new WidgetStepElement this[string objectKey]
        {
            get { return (WidgetStepElement)this.BaseGet(objectKey); }
        }
    }

    public partial class SecuritySection : ISerializableSection
    {
        public static SecuritySection CachedInstance
        {
            get { return (SecuritySection)Helper.LoadSectionFromFile(string.Format(@"{0}\Configurations\Security.config", WebRootPath.Instance.ToString()), "securitySection", Constants.SECURITY_SECTION_CACHE_KEY); }
        }

        public void Deserialize(System.Xml.XmlReader reader)
        {
            DeserializeSection(reader);
        }

        protected override void DeserializeSection(System.Xml.XmlReader reader)
        {
            if (!reader.Read() || (reader.NodeType != System.Xml.XmlNodeType.Element))
            {
                throw new System.Configuration.ConfigurationErrorsException("Configuration reader expected to find an element", reader);
            }
            this.DeserializeElement(reader, false);
        }

        public string Serialize(System.Configuration.ConfigurationElement parentElement, string name, System.Configuration.ConfigurationSaveMode saveMode)
        {
            return SerializeSection(parentElement, name, saveMode);
        }

        protected override string SerializeSection(System.Configuration.ConfigurationElement parentElement, string name, System.Configuration.ConfigurationSaveMode saveMode)
        {
            System.IO.StringWriter sWriter = new System.IO.StringWriter(System.Globalization.CultureInfo.InvariantCulture);
            System.Xml.XmlTextWriter xWriter = new System.Xml.XmlTextWriter(sWriter);
            xWriter.Formatting = System.Xml.Formatting.None;
            xWriter.Indentation = 4;
            xWriter.IndentChar = ' ';
            this.SerializeToXmlElement(xWriter, name);
            xWriter.Flush();
            return sWriter.ToString();
        }

        public bool CurrentUserHasAccess(string securityArea)
        {
            SecurityAreaElement currentSecurityArea = this.SecurityAreas[securityArea];
            if (currentSecurityArea != null && currentSecurityArea.Roles.Contains(UserDataContext.GetUserDataContext().UserRole))
                return true;
            else
                return false;
        }
    }

    public partial class SecurityAreaElementCollection
    {
        public new SecurityAreaElement this[string objectKey]
        {
            get { return (SecurityAreaElement)this.BaseGet(objectKey); }
        }
    }

    public partial class SiteObjectSection : ISerializableSection
    {
        public static SiteObjectSection CachedInstance
        {
            get { return (SiteObjectSection)Helper.LoadSectionFromFile(string.Format(@"{0}\Configurations\SiteObjects.config", WebRootPath.Instance.ToString()), "siteObjectSection", Constants.SITEOBJECT_SECTION_CACHE_KEY); }
        }

        public void Deserialize(System.Xml.XmlReader reader)
        {
            DeserializeSection(reader);
        }

        protected override void DeserializeSection(System.Xml.XmlReader reader)
        {
            if (!reader.Read() || (reader.NodeType != System.Xml.XmlNodeType.Element))
            {
                throw new System.Configuration.ConfigurationErrorsException("Configuration reader expected to find an element", reader);
            }
            this.DeserializeElement(reader, false);
        }

        public string Serialize(System.Configuration.ConfigurationElement parentElement, string name, System.Configuration.ConfigurationSaveMode saveMode)
        {
            return SerializeSection(parentElement, name, saveMode);
        }

        protected override string SerializeSection(System.Configuration.ConfigurationElement parentElement, string name, System.Configuration.ConfigurationSaveMode saveMode)
        {
            System.IO.StringWriter sWriter = new System.IO.StringWriter(System.Globalization.CultureInfo.InvariantCulture);
            System.Xml.XmlTextWriter xWriter = new System.Xml.XmlTextWriter(sWriter);
            xWriter.Formatting = System.Xml.Formatting.None;
            xWriter.Indentation = 4;
            xWriter.IndentChar = ' ';
            this.SerializeToXmlElement(xWriter, name);
            xWriter.Flush();
            return sWriter.ToString();
        }
    }

    public partial class SiteObjectType
    {
        public override string ToString()
        {
            return this.Id;
        }
    }
    public partial class SiteObjectElementCollection
    {
        public IEnumerable<SiteObjectType> LINQEnumarable
        {
            get
            {
                return from SiteObjectType sObjects in this select sObjects;
            }
        }

        public global::_4screen.CSB.Common.SiteObjectType this[string id]
        {
            get
            {
                return ((global::_4screen.CSB.Common.SiteObjectType)(base.BaseGet(Helper.GetObjectTypeNumericID(id))));
            }
        }
    }


    public partial class SiteLinkSection : ISerializableSection
    {
        public static SiteLinkSection CachedInstance
        {
            get { return (SiteLinkSection)Helper.LoadSectionFromFile(string.Format(@"{0}\Configurations\Links.config", WebRootPath.Instance.ToString()), "siteLinkSection", Constants.SITELINK_SECTION_CACHE_KEY); }
        }

        public void Deserialize(System.Xml.XmlReader reader)
        {
            DeserializeSection(reader);
        }

        protected override void DeserializeSection(System.Xml.XmlReader reader)
        {
            if (!reader.Read() || (reader.NodeType != System.Xml.XmlNodeType.Element))
            {
                throw new System.Configuration.ConfigurationErrorsException("Configuration reader expected to find an element", reader);
            }
            this.DeserializeElement(reader, false);
        }

        public string Serialize(System.Configuration.ConfigurationElement parentElement, string name, System.Configuration.ConfigurationSaveMode saveMode)
        {
            return SerializeSection(parentElement, name, saveMode);
        }

        protected override string SerializeSection(System.Configuration.ConfigurationElement parentElement, string name, System.Configuration.ConfigurationSaveMode saveMode)
        {
            System.IO.StringWriter sWriter = new System.IO.StringWriter(System.Globalization.CultureInfo.InvariantCulture);
            System.Xml.XmlTextWriter xWriter = new System.Xml.XmlTextWriter(sWriter);
            xWriter.Formatting = System.Xml.Formatting.None;
            xWriter.Indentation = 4;
            xWriter.IndentChar = ' ';
            this.SerializeToXmlElement(xWriter, name);
            xWriter.Flush();
            return sWriter.ToString();
        }
    }

    public partial class SiteLink
    {
        public override string ToString()
        {
            return this.Url;
        }
    }
    public partial class SiteLinkElementCollection
    {
        public IEnumerable<SiteLink> LINQEnumarable
        {
            get
            {
                return from SiteLink sObjects in this select sObjects;
            }
        }

        public SiteLink this[string linkKey]
        {
            get
            {
                SiteLink sl = null;
                sl = (from SiteLink sLink in this.LINQEnumarable.Where(x => x.Key.ToLower() == linkKey.ToLower())
                      select sLink).SingleOrDefault();

                return sl;
            }
        }
    }

    public partial class WizardSection : ISerializableSection
    {
        public static WizardSection CachedInstance
        {
            get { return (WizardSection)Helper.LoadSectionFromFile(string.Format(@"{0}\Configurations\Wizards.config", WebRootPath.Instance.ToString()), "wizardSection", Constants.WIZARD_SECTION_CACHE_KEY); }
        }

        public void Deserialize(System.Xml.XmlReader reader)
        {
            DeserializeSection(reader);
        }

        protected override void DeserializeSection(System.Xml.XmlReader reader)
        {
            if (!reader.Read() || (reader.NodeType != System.Xml.XmlNodeType.Element))
            {
                throw new System.Configuration.ConfigurationErrorsException("Configuration reader expected to find an element", reader);
            }
            this.DeserializeElement(reader, false);
        }

        public string Serialize(System.Configuration.ConfigurationElement parentElement, string name, System.Configuration.ConfigurationSaveMode saveMode)
        {
            return SerializeSection(parentElement, name, saveMode);
        }

        protected override string SerializeSection(System.Configuration.ConfigurationElement parentElement, string name, System.Configuration.ConfigurationSaveMode saveMode)
        {
            System.IO.StringWriter sWriter = new System.IO.StringWriter(System.Globalization.CultureInfo.InvariantCulture);
            System.Xml.XmlTextWriter xWriter = new System.Xml.XmlTextWriter(sWriter);
            xWriter.Formatting = System.Xml.Formatting.None;
            xWriter.Indentation = 4;
            xWriter.IndentChar = ' ';
            this.SerializeToXmlElement(xWriter, name);
            xWriter.Flush();
            return sWriter.ToString();
        }
    }

    public partial class WizardElementCollection
    {
        public IEnumerable<WizardElement> LINQEnumarable
        {
            get
            {
                return from WizardElement sWizards in this select sWizards;
            }
        }
        public WizardElement this[string id]
        {
            get
            {
                WizardElement wiz = null;
                wiz = (from WizardElement wizardElems in this.LINQEnumarable.Where(x => x.Id.ToLower() == id.ToLower())
                       select wizardElems).SingleOrDefault();

                return wiz;
            }
        }
    }

    public partial class WizardStepElementCollection
    {
        public IEnumerable<WizardStepElement> LINQEnumarable
        {
            get
            {
                return from WizardStepElement steps in this select steps;
            }
        }
    }

    public partial class WizardStepSettingsElementCollection
    {
        public IEnumerable<WizardStepSettingsElement> LINQEnumarable
        {
            get
            {
                return from WizardStepSettingsElement settings in this select settings;
            }
        }
    }

    public partial class ShopSection : ISerializableSection
    {
        public static ShopSection CachedInstance
        {
            get { return (ShopSection)Helper.LoadSectionFromFile(string.Format(@"{0}\Configurations\Shop.config", WebRootPath.Instance.ToString()), "shopSection", "ShopSection"); }
        }

        public void Deserialize(System.Xml.XmlReader reader)
        {
            DeserializeSection(reader);
        }

        protected override void DeserializeSection(System.Xml.XmlReader reader)
        {
            if (!reader.Read() || (reader.NodeType != System.Xml.XmlNodeType.Element))
            {
                throw new System.Configuration.ConfigurationErrorsException("Configuration reader expected to find an element", reader);
            }
            this.DeserializeElement(reader, false);
        }

        public string Serialize(System.Configuration.ConfigurationElement parentElement, string name, System.Configuration.ConfigurationSaveMode saveMode)
        {
            return SerializeSection(parentElement, name, saveMode);
        }

        protected override string SerializeSection(System.Configuration.ConfigurationElement parentElement, string name, System.Configuration.ConfigurationSaveMode saveMode)
        {
            System.IO.StringWriter sWriter = new System.IO.StringWriter(System.Globalization.CultureInfo.InvariantCulture);
            System.Xml.XmlTextWriter xWriter = new System.Xml.XmlTextWriter(sWriter);
            xWriter.Formatting = System.Xml.Formatting.None;
            xWriter.Indentation = 4;
            xWriter.IndentChar = ' ';
            this.SerializeToXmlElement(xWriter, name);
            xWriter.Flush();
            return sWriter.ToString();
        }
    }

    public partial class ShopSettingElementCollection
    {
        public IEnumerable<ShopSetting> LINQEnumarable
        {
            get
            {
                return from ShopSetting sObjects in this select sObjects;
            }
        }
        public string this[string key]
        {
            get
            {
                ShopSetting setting = null;
                setting = (from ShopSetting settings in this.LINQEnumarable.Where(x => string.Compare(x.key, key, true) == 0)
                           select settings).SingleOrDefault();
                return setting.value;
            }
        }
    }

    public partial class ShopTagElementCollection
    {
        public IEnumerable<ShopTagElement> LINQEnumarable
        {
            get
            {
                return from ShopTagElement sObjects in this select sObjects;
            }
        }
    }

    public partial class ContentReports
    {
        public IEnumerable<ContentReport> LINQEnumarable
        {
            get
            {
                return from ContentReport sObjects in this select sObjects;
            }
        }
    }

    public partial class OutputTemplatesSection : ISerializableSection
    {
        public static OutputTemplatesSection CachedInstance
        {
            get { return (OutputTemplatesSection)Helper.LoadSectionFromFile(string.Format(@"{0}\Configurations\OutputTemplates.config", WebRootPath.Instance.ToString()), "outputTemplatesSection", Constants.OUTPUTTEMPALTES_SECTION_CACHE_KEY); }
        }

        public void Deserialize(System.Xml.XmlReader reader)
        {
            DeserializeSection(reader);
        }

        protected override void DeserializeSection(System.Xml.XmlReader reader)
        {
            if (!reader.Read() || (reader.NodeType != System.Xml.XmlNodeType.Element))
            {
                throw new System.Configuration.ConfigurationErrorsException("Configuration reader expected to find an element", reader);
            }
            this.DeserializeElement(reader, false);
        }

        public string Serialize(System.Configuration.ConfigurationElement parentElement, string name, System.Configuration.ConfigurationSaveMode saveMode)
        {
            return SerializeSection(parentElement, name, saveMode);
        }

        protected override string SerializeSection(System.Configuration.ConfigurationElement parentElement, string name, System.Configuration.ConfigurationSaveMode saveMode)
        {
            System.IO.StringWriter sWriter = new System.IO.StringWriter(System.Globalization.CultureInfo.InvariantCulture);
            System.Xml.XmlTextWriter xWriter = new System.Xml.XmlTextWriter(sWriter);
            xWriter.Formatting = System.Xml.Formatting.None;
            xWriter.Indentation = 4;
            xWriter.IndentChar = ' ';
            this.SerializeToXmlElement(xWriter, name);
            xWriter.Flush();
            return sWriter.ToString();
        }
    }

    public partial class OutputTemplateElementCollection
    {
        public IEnumerable<OutputTemplateElement> LINQEnumarable
        {
            get
            {
                return from OutputTemplateElement templates in this select templates;
            }
        }

        public new OutputTemplateElement this[Guid objectKey]
        {
            get { return (OutputTemplateElement)this.BaseGet(objectKey); }
        }
    }

    public partial class MapSection : ISerializableSection
    {
        public static MapSection CachedInstance
        {
            get { return (MapSection)Helper.LoadSectionFromFile(string.Format(@"{0}\Configurations\DefaultMap.config", WebRootPath.Instance.ToString()), "mapSection", Constants.MAP_SECTION_CACHE_KEY); }
        }

        public void Deserialize(System.Xml.XmlReader reader)
        {
            DeserializeSection(reader);
        }

        protected override void DeserializeSection(System.Xml.XmlReader reader)
        {
            if (!reader.Read() || (reader.NodeType != System.Xml.XmlNodeType.Element))
            {
                throw new System.Configuration.ConfigurationErrorsException("Configuration reader expected to find an element", reader);
            }
            this.DeserializeElement(reader, false);
        }

        public string Serialize(System.Configuration.ConfigurationElement parentElement, string name, System.Configuration.ConfigurationSaveMode saveMode)
        {
            return SerializeSection(parentElement, name, saveMode);
        }

        protected override string SerializeSection(System.Configuration.ConfigurationElement parentElement, string name, System.Configuration.ConfigurationSaveMode saveMode)
        {
            System.IO.StringWriter sWriter = new System.IO.StringWriter(System.Globalization.CultureInfo.InvariantCulture);
            System.Xml.XmlTextWriter xWriter = new System.Xml.XmlTextWriter(sWriter);
            xWriter.Formatting = System.Xml.Formatting.None;
            xWriter.Indentation = 4;
            xWriter.IndentChar = ' ';
            this.SerializeToXmlElement(xWriter, name);
            xWriter.Flush();
            return sWriter.ToString();
        }
    }

    public partial class MapObjectTypeElementCollection
    {
        public IEnumerable<MapObjectTypeElement> LINQEnumarable
        {
            get
            {
                return from MapObjectTypeElement element in this select element;
            }
        }
    }

    public partial class MapObjectTypeTagElementCollection
    {
        public IEnumerable<MapObjectTypeTagElement> LINQEnumarable
        {
            get
            {
                return from MapObjectTypeTagElement element in this select element;
            }
        }
    }
}
