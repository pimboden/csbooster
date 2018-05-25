// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
namespace _4screen.CSB.Common
{
    public interface ISerializableSection
    {
        void Deserialize(System.Xml.XmlReader reader);

        string Serialize(System.Configuration.ConfigurationElement parentElement, string name, System.Configuration.ConfigurationSaveMode saveMode);
    }
}
