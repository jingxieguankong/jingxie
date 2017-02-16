namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class Role:LogicDeletableEntity                                                                                                                                         
    {
         
        /// <summary>
        /// 名称
        /// </summary>
        public string Name{ get; set; }
         
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks{ get; set; }
         
        /// <summary>
        /// 状态，0：正常；-1：异常；-2：冻结。
        /// </summary>
        public short Status{ get; set; }
         
        /// <summary>
        /// 管理组织机构表主键，标识角色所属组织机构
        /// </summary>
        public string OrgId{ get; set; }
    }
}