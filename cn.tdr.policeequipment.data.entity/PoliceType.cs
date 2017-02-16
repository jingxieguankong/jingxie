namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class PoliceType:LogicDeletableEntity                                                                                                                                         
    {
         
        /// <summary>
        /// 组织机构主键
        /// </summary>
        public string OrgId{ get; set; }
         
        /// <summary>
        /// 名称
        /// </summary>
        public string Name{ get; set; }
    }
}