namespace cn.tdr.policeequipment.configuration
{
    using System;
    using System.Configuration;

    public abstract class ConfigurationSectionReader : IConfigurationSectionReader
    {
        object IConfigurationSectionReader.Read(string sectionGroupNameOrSectionName)
        {
            if (string.IsNullOrWhiteSpace(sectionGroupNameOrSectionName))
            {
                throw new ArgumentNullException(nameof(sectionGroupNameOrSectionName));
            }

            return ConfigurationManager.GetSection(sectionGroupNameOrSectionName);
        }
    }
}
