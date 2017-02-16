namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class DispatchEquipmentPosition:LogicDeletableEntity                                                                                                                                         
    {
         
        /// <summary>
        /// 关联警械布控主键，描述当前位置变化隶属于那一次警械布控
        /// </summary>
        public string AeId{ get; set; }
         
        /// <summary>
        /// 关联警械主键，描述当前位置出现的警械
        /// </summary>
        public string EquipId{ get; set; }
         
        /// <summary>
        /// 描述警械出现位置的 GPS 经度
        /// </summary>
        public double Lon{ get; set; }
         
        /// <summary>
        /// 描述警械出现位置的 GPS 维度
        /// </summary>
        public double Lat{ get; set; }
         
        /// <summary>
        /// 描述当前内容的处理状态。0：未处理；1：已处理。
        /// </summary>
        public short HdStatus{ get; set; }
         
        /// <summary>
        /// 关联基站主键，标识当前警械出现的基站范围
        /// </summary>
        public string SiteId{ get; set; }
         
        /// <summary>
        /// 描述警械进入或者离开当前位置。1：进入；2：离开。
        /// </summary>
        public short PtStatus{ get; set; }
    }
}