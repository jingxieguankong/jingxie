namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class Feature:LogicDeletableEntity                                                                                                                                         
    {
         
        /// <summary>
        /// 角色主键
        /// </summary>
        public string RoleId{ get; set; }
         
        /// <summary>
        /// 菜单主键
        /// </summary>
        public string MenuId{ get; set; }
         
        /// <summary>
        /// 功能键标识。例如：添加，编辑，删除，查看，导入，导出 ... 等等
        /// </summary>
        public string ActId{ get; set; }
         
        /// <summary>
        /// 功能状态。0：正常；-1：禁止；
        /// </summary>
        public short Status{ get; set; }
         
        /// <summary>
        /// 当前操作功能说明
        /// </summary>
        public string ActRemark{ get; set; }
    }
}