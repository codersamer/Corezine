using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Corezine.Domain.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        TEntity Get(Int32 id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, Boolean>> predicate);
    }
}
