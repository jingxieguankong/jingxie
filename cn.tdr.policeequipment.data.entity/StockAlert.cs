namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class StockAlert:LogicDeletableEntity                                                                                                                                         
    {
         
        /// <summary>
        /// 关联组织机构表主键，标识所属组织机构
        /// </summary>
        public string OrgId{ get; set; }
         
        /// <summary>
        /// 关联警械库表主键，标识所属警械库
        /// </summary>
        public string LibId{ get; set; }
         
        /// <summary>
        /// 关联警械类型表主键，标识预警的警械装备的类型
        /// </summary>
        public string CateId{ get; set; }
         
        /// <summary>
        /// 警械类型的库存最低数量
        /// </summary>
        public int MinCount{ get; set; }
    }
}