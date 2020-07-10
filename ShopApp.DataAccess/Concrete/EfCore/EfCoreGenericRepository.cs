using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    /// <summary>
    /// Generic Repository
    /// </summary>
    public class EfCoreGenericRepository<TEntity, TContext> : IRepository<TEntity> where TEntity : class
                                                                                   where TContext : DbContext, new()
    {
        /// <summary>
        /// Gets entities
        /// </summary>
        /// <param name="filters">Entity Expression Filters</param>
        /// <returns>List of entities</returns>
        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filters = null)
        {
            using (var context = new TContext())
            {
                return filters == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filters).ToList();
            }
        }

        /// <summary>
        /// Get entity
        /// </summary>
        /// <param name="id">Entity identifier</param>
        /// <returns>Entity</returns>
        public TEntity Get(int id)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().Find(id);
            }
        }

        /// <summary>
        /// Get entity
        /// </summary>
        /// <param name="filters">Entity Expression Filters</param>
        /// <returns>Entity</returns>
        public TEntity Get(Expression<Func<TEntity, bool>> filters = null)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().Where(filters).SingleOrDefault();
            }
        }

        /// <summary>
        /// Insert an entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Insert(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Insert an entities
        /// </summary>
        /// <param name="entity">Entities</param>
        public void Insert(IEnumerable<TEntity> entity)
        {
            using (var context = new TContext())
            {
                context.Set<TEntity>().AddRange(entity);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates the entitiy
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entity">entity</param>
        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Set<TEntity>().Remove(entity);
                context.SaveChanges();
            }
        }
    }
}
