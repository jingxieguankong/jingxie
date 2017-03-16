namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class EquipmentAllopatryExcept: Entity
    {
         
        /// <summary>
        /// 关联警员主键，标识被布控的警员，同时标识警员布控
        /// </summary>
        public string OfficerId{ get; set; }
         
        /// <summary>
        /// 描述警械布控或者撤控发生的时间
        /// </summary>
        public long CTime{ get; set; }
         
        /// <summary>
        /// 描述当前异常的处理状态。0：未处理；1：已处理。
        /// </summary>
        public short Status{ get; set; }
         
        /// <summary>
        /// 处理时间
        /// </summary>
        public long DTime{ get; set; }
    }
}