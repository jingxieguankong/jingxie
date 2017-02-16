namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class EquipmentDispatch:LogicDeletableEntity                                                                                                                                         
    {
         
        /// <summary>
        /// 关联警械ID，标识布控的警械
        /// </summary>
        public string EquipId{ get; set; }
         
        /// <summary>
        /// 关联警员主键，标识被布控的警员，同时标识警员布控
        /// </summary>
        public string OfficerId{ get; set; }
         
        /// <summary>
        /// 描述警械布控或者撤控发生的时间
        /// </summary>
        public long DispatchTime{ get; set; }
         
        /// <summary>
        /// 是否撤控。0：未撤控；1：已撤控。
        /// </summary>
        public short IsCancel{ get; set; }
         
        /// <summary>
        /// 撤控时间
        /// </summary>
        public long CancelTime{ get; set; }
         
        /// <summary>
        /// 撤控原因
        /// </summary>
        public string CancelMsg{ get; set; }
    }
}