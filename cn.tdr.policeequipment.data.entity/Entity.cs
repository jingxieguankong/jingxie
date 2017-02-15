namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 基础实体
    /// </summary>
    public abstract class Entity : interfaces.IEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [System.ComponentModel.DataAnnotations.Key]
        public string Id { get; set; } = common.Utility.GuidToString();
    }

    /// <summary>
    /// 可逻辑删除实体对象
    /// </summary>
    public abstract class LogicDeletableEntity : Entity, interfaces.ILogicDeletableEntity
    {
        /// <summary>
        /// 逻辑删除标识。1：标识已删除；0：标识未删除
        /// </summary>
        public short IsDel { get; set; } = 0;
    }
}
