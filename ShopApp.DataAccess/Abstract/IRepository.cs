using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ShopApp.DataAccess.Abstract
{
    public interface IRepository<TEntity>
    {
        TEntity Get(int id);
        TEntity Get(Expression<Func<TEntity, bool>> filters = null);
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> filters = null);
        void Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
