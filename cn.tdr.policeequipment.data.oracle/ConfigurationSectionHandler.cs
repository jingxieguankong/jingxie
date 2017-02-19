namespace cn.tdr.policeequipment.data.oracle
{
    using System.Xml;

    sealed class ConfigurationSectionHandler:configuration.ConfigurationSectionReader
    {
        public static RepositoryConfig RepositoryConfig(string sectionName)
        {
            var reader = new ConfigurationSectionHandler();
            var section = (reader as configuration.IConfigurationSectionReader).Read(sectionName) as XmlNode;
            var cfg = new RepositoryConfig(section);
            return cfg;
        }
    }

    sealed class RepositoryConfig
    {
        public string Owner { get; private set; }

        public string ConnectionStringOrName { get; private set; }

        public RepositoryConfig(XmlNode configSection)
        {
            Owner = configSection.SelectSingleNode("owner").InnerText;
            ConnectionStringOrName = configSection.SelectSingleNode("connectionStringOrName").InnerText;
        }
    }
}
