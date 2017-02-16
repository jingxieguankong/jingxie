namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class StandardEquipment:LogicDeletableEntity                                                                                                                                         
    {
         
        /// <summary>
        /// 关联警种表主键，标识所属警种类型
        /// </summary>
        public string PtId{ get; set; }
         
        /// <summary>
        /// 关联警械类型表主键，标识警械装备的类型
        /// </summary>
        public string CateId{ get; set; }
         
        /// <summary>
        /// 描述所属装备类型的应该装备的数量，默认值为 0
        /// </summary>
        public short Num{ get; set; }
         
        /// <summary>
        /// 标识所属的装备类型为主要装备，主要装备总是必须装备的。0：标识非主装备；1：标识主装备
        /// </summary>
        public short IsPrimary{ get; set; }
         
        /// <summary>
        /// 标识所属的装备类型装备是否必须装备。0：非必须装备；1：标识必须装备；
        /// </summary>
        public short IsRequire{ get; set; }
    }
}