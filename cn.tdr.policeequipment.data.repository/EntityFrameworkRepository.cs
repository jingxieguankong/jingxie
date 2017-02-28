namespace cn.tdr.policeequipment.data.repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using interfaces;

    public abstract class EntityFrameworkRepository : IRepository, IUnitWork, IDisposable
    {
        private DbContext _db;
        private bool _disposed;
        
        protected EntityFrameworkRepository()
        {
            this._disposed = false;
            this._db = GetDbContext();
        }

        protected abstract DbContext GetDbContext();

        TEntity IRepository.Insert<TEntity>(TEntity entity)
        {
            var set = _db.Set<TEntity>();
            return set.Add(entity);
        }
        
        protected virtual TEntity Update<TEntity>(TEntity entity, DbSet<TEntity> set = null)
            where TEntity:class,IEntity,new()
        {
            set = set ?? _db.Set<TEntity>();
            var e = set.Attach(entity);
            var entry = _db.Entry<TEntity>(entity);
            entry.State = EntityState.Modified;
            return e;
        }

        TEntity IRepository.Update<TEntity>(TEntity entity)
        {
            return this.Update<TEntity>(entity);
        }

        IEnumerable<TEntity> IRepository.Update<TEntity>(Func<TEntity, TEntity> reValue, Expression<Func<TEntity, bool>> predicate)
        {
            var set = _db.Set<TEntity>();
            var query = set.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return
                query.ToArray().Select(t => this.Update<TEntity>(reValue(t), set)).ToArray();
        }

        IEnumerable<TEntity> IRepository.Delete<TEntity>(Expression<Func<TEntity, bool>> predicate)
        {
            var set = _db.Set<TEntity>();
            var items = ((IRepository)this).Query<TEntity>(predicate);
            return set.RemoveRange(items);
        }

        IQueryable<TEntity> IRepository.Query<TEntity>(Expression<Func<TEntity, bool>> predicate)
        {
            var query = _db.Set<TEntity>().AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query;
        }

        protected virtual int Commit()
        {
            return _db.SaveChanges();
        }

        int IUnitWork.Commit()
        {
            return this.Commit();
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            //if (disposing)
            //{
            //    // 释放非托管资源
            //}

            _db.Dispose();
            _db = null;
            _disposed = true;
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~EntityFrameworkRepository()
        {
            Dispose(false);
        }
    }
}
