namespace cn.tdr.policeequipment.interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// 数据仓储接口
    /// </summary>
    public interface IRepository : IUnitWork, IDisposable
    {
        /// <summary>
        /// 插入新的实体对象
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="entity">等待插入的实体对象</param>
        /// <returns></returns>
        TEntity Insert<TEntity>(TEntity entity) where TEntity : class, IEntity, new();

        /// <summary>
        /// 更新指定的实体对象
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="entity">等待更新的实体对象</param>
        /// <returns></returns>
        TEntity Update<TEntity>(TEntity entity) where TEntity : class, IEntity, new();

        /// <summary>
        /// 批量更新指定条件的实体对象
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="reValue">重新赋值语句</param>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        IEnumerable<TEntity> Update<TEntity>(Func<TEntity, TEntity> reValue, Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity, new();

        /// <summary>
        /// 删除指定条件的实体对象信息
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        IEnumerable<TEntity> Delete<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity, new();

        /// <summary>
        /// 查询制定条件的实体对象
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity, new();
    }
}
