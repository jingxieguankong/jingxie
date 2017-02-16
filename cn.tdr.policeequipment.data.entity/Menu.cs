namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class Menu:LogicDeletableEntity                                                                                                                                         
    {
         
        /// <summary>
        /// 标题
        /// </summary>
        public string Title{ get; set; }
         
        /// <summary>
        /// 序列标识，用于排序
        /// </summary>
        public short Order{ get; set; }
         
        /// <summary>
        /// 功能页面路径
        /// </summary>
        public string Src{ get; set; }
         
        /// <summary>
        /// 菜单的备注内容
        /// </summary>
        public string Remarks{ get; set; }
         
        /// <summary>
        /// 上一级菜单主键，“0” 标识顶级
        /// </summary>
        public string Pid{ get; set; }
    }
}