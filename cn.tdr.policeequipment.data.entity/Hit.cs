namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class Hit:LogicDeletableEntity                                                                                                                                         
    {
         
        /// <summary>
        /// 描述当前记录隶属于哪一个标签
        /// </summary>
        public string TagId{ get; set; }
         
        /// <summary>
        /// 描述当前标签在哪一个位置的基站范围内
        /// </summary>
        public string SiteId{ get; set; }
         
        /// <summary>
        /// 描述基站上传标签的时间，标签的运动位置的排序应该以当前字段为准
        /// </summary>
        public long UpTime{ get; set; }
         
        /// <summary>
        /// 描述系统记录当前标签位置信息的时间
        /// </summary>
        public long CTime{ get; set; }
    }
}