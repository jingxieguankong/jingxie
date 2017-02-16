namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class AllopatryEquipmentPosition:LogicDeletableEntity                                                                                                                                         
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
        /// 描述警械出现位置的 GPS 经度
        /// </summary>
        public double Lon{ get; set; }
         
        /// <summary>
        /// 描述警械出现位置的 GPS 维度
        /// </summary>
        public double Lat{ get; set; }
    }
}