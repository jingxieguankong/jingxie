namespace cn.tdr.policeequipment.configuration
{
    using System.Configuration;
    using System.Xml;

    public class ConfigurationSectionHandler : IConfigurationSectionHandler
    {
        object IConfigurationSectionHandler.Create(object parent, object configContext, XmlNode section)
        {
            return section;
        }
    }
}
