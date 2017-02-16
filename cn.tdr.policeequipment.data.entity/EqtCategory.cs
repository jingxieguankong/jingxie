namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class EqtCategory:LogicDeletableEntity                                                                                                                                         
    {
         
        /// <summary>
        /// 当前类型名称
        /// </summary>
        public string Name{ get; set; }
         
        /// <summary>
        /// 当前类型编码。规则：为父级代码+当前类型代码
        /// </summary>
        public string Code{ get; set; }
         
        /// <summary>
        /// 自引用外键，上一级类型主键
        /// </summary>
        public string Pid{ get; set; }
         
        /// <summary>
        /// 标识当前类型在类型关系树中的位置，总是等于上一级层级数 + 1
        /// </summary>
        public short Layer{ get; set; }
         
        /// <summary>
        /// 强撞击次数。这是一个大于 0 的值，0 ：不限制，其它表示最大被允许撞击的次数。
        /// </summary>
        public short HitCount{ get; set; }
    }
}