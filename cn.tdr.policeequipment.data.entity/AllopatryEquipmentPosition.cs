namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class AllopatryEquipmentPosition: Entity
    {
         
        /// <summary>
        /// 关联警员警械异地异常主键，描述当前警械位置变化隶属于那一次警械异地变化异常
        /// </summary>
        public string AeId{ get; set; }
         
        /// <summary>
        /// 关联警械主键，标识当前位置出现的警械
        /// </summary>
        public string EquipId{ get; set; }
         
        /// <summary>
        /// 描述当前标签在哪一个位置的基站范围内
        /// </summary>
        public string SiteId{ get; set; }
         
        /// <summary>
        /// 标签上报时间
        /// </summary>
        public long UpTime{ get; set; }
    }
}