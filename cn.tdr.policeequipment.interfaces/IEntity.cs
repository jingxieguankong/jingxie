namespace cn.tdr.policeequipment.interfaces
{
    /// <summary>
    /// 实体对象接口
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        string Id { get; set; }
    }

    /// <summary>
    /// 可逻辑删除实体对象
    /// </summary>
    public interface ILogicDeletableEntity : IEntity
    {
        /// <summary>
        /// 逻辑删除标识。1：标识已删除；0：标识未删除
        /// </summary>
        short IsDel { get; set; }
    }
}
