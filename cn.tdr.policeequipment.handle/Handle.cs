namespace cn.tdr.policeequipment.handle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using interfaces;

    /// <summary>
    /// 基础处理器
    /// </summary>
    public abstract class Handle<TEntity>: IDisposable
            where TEntity : class, IEntity, new()
    {
        protected readonly IRepository Repository;

        protected Handle(IRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }
            Repository = repository;
        }

        /// <summary>
        /// 查询指定条件的实体数据集合，并获取集合的第一条实体数据
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        public TEntity First(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.Query<TEntity>(predicate).FirstOrDefault();
        }

        /// <summary>
        /// 查询指定条件的实体数据集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        public IEnumerable<TEntity> All(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.Query<TEntity>(predicate);
        }

        // 自动提交
        private TObject AutoCommit<TObject>(bool autoCommit, 
            TObject entity, 
            Func<TObject, TObject> failedHandle)
        {
            if (autoCommit && 0 >= Repository.Commit())
            {
                entity = failedHandle(entity);
            }
            return entity;
        }

        /// <summary>
        /// 添加实体数据到指定的集合
        /// </summary>
        /// <typeparam name="TEntity">实体数据类型</typeparam>
        /// <param name="entity">被添加的实体数据</param>
        /// <param name="autoCommit">自动提交标识。true 标识自动提交。默认为 false</param>
        /// <returns></returns>
        public TEntity Add(TEntity entity, bool autoCommit = false)
        {
            var e = Repository.Insert<TEntity>(entity);
            return AutoCommit<TEntity>(autoCommit, e, t => null);
        }

        /// <summary>
        /// 更新指定的实体数据
        /// </summary>
        /// <typeparam name="TEntity">实体数据类型</typeparam>
        /// <param name="entity">被更新的实体数据</param>
        /// <param name="autoCommit">自动提交标识。true 标识自动提交。默认为 false</param>
        /// <returns></returns>
        public TEntity Modify(TEntity entity, bool autoCommit = false)
        {
            var e = Repository.Update<TEntity>(entity);
            return AutoCommit<TEntity>(autoCommit, e, t => null);
        }

        /// <summary>
        /// 批量更新达成指定条件的实体数据
        /// </summary>
        /// <typeparam name="TEntity">实体数据类型</typeparam>
        /// <param name="reValueHandle">重新赋值操作</param>
        /// <param name="predicate">指定查询条件</param>
        /// <param name="autoCommit">自动提交标识。true 标识自动提交。默认为 false</param>
        /// <returns></returns>
        public IEnumerable<TEntity> ModifyAny(Func<TEntity, TEntity> reValueHandle, 
            Expression<Func<TEntity, bool>> predicate, 
            bool autoCommit = false)
        {
            var items = Repository.Update<TEntity>(reValueHandle, predicate);
            return AutoCommit<IEnumerable<TEntity>>(autoCommit, items, t => new TEntity[0]);
        }

        /// <summary>
        /// 批量删除达成指定条件的实体数据
        /// </summary>
        /// <typeparam name="TEntity">实体数据类型</typeparam>
        /// <param name="predicate">指定查询条件</param>
        /// <param name="autoCommit">自动提交标识。true 标识自动提交。默认为 false</param>
        /// <returns></returns>
        public IEnumerable<TEntity> RemoveAny(Expression<Func<TEntity, bool>> predicate,
            bool autoCommit = false)
        {
            var items = Repository.Delete<TEntity>(predicate);
            return AutoCommit<IEnumerable<TEntity>>(autoCommit, items, t => new TEntity[0]);
        }
        
        private bool _disposed = false;
        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing">是否释放非托管资源，true 标识释放非托管资源</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                Repository.Dispose();
            }            
            _disposed = true;
        }

        /// <summary>
        /// 释放占用的内部资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Handle()
        {
            Dispose(false);
        }
    }
}
