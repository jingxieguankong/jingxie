namespace cn.tdr.policeequipment.configuration
{    
    /// <summary>
    /// 配置文件 configurationSection 节点读取器
    /// </summary>
    public interface  IConfigurationSectionReader
    {
        /// <summary>
        /// 读取指定配置节点
        /// </summary>
        /// <param name="sectionGroupNameOrSectionName">configurationSection 节点名称</param>
        /// <returns></returns>
        object Read(string sectionGroupNameOrSectionName);
    }
}
