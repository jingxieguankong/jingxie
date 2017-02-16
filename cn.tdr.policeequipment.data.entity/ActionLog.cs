namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionLog:LogicDeletableEntity                                                                                                                                         
    {
         
        /// <summary>
        /// 当前操作开始发生的时间
        /// </summary>
        public long StartTime{ get; set; }
         
        /// <summary>
        /// 当前操作结束的时间
        /// </summary>
        public long EndTime{ get; set; }
         
        /// <summary>
        /// 当前操作设备IPV4地址
        /// </summary>
        public string IPv4{ get; set; }
         
        /// <summary>
        /// 当前操作设备IPV6地址
        /// </summary>
        public string IPv6{ get; set; }
         
        /// <summary>
        /// 当前操作设备MAC地址
        /// </summary>
        public string Mac{ get; set; }
         
        /// <summary>
        /// 关联菜单功能主键，描述当前操作发生哪一个业务功能
        /// </summary>
        public string FeatureId{ get; set; }
         
        /// <summary>
        /// 关联操作用户主键，描述当前操作是哪一个用户发生的
        /// </summary>
        public string UserId{ get; set; }
         
        /// <summary>
        /// 描述当前操作完成状态。0：成功；-1：失败。
        /// </summary>
        public short IsOk{ get; set; }
         
        /// <summary>
        /// 操作失败原因
        /// </summary>
        public string ErrorMsg{ get; set; }
    }
}