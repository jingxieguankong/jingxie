namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class Organization:LogicDeletableEntity                                                                                                                                         
    {
         
        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string Name{ get; set; }
         
        /// <summary>
        /// 组织机构代码，总是等于上一级组织机构代码+当前组织机构代码
        /// </summary>
        public string Code{ get; set; }
         
        /// <summary>
        /// 自引用外键，上一级组织机构主键，“0” 标识顶级
        /// </summary>
        public string Pid{ get; set; }
         
        /// <summary>
        /// 标识当前组织机构在组织机构树种的位置，它总是上一级组织机构的层级 + 1
        /// </summary>
        public short Layer{ get; set; }
    }
}