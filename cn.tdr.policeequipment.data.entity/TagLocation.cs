namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class TagLocation: Entity
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
        /// 描述当前是进入或者离开当前基站。1：进入；2：离开。
        /// </summary>
        public short Status{ get; set; }
         
        /// <summary>
        /// 描述基站上传标签的时间，标签的运动位置的排序应该以当前字段为准
        /// </summary>
        public long UpTime{ get; set; }
    }
}