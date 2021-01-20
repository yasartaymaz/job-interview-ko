using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()//TEntity'nin class, IEntity interface'inden türemiş olması ve newlenebilir olması lazım
        where TContext : DbContext, new()//TContext'in EntitryFrameworkcore'dan gelen DbContext'ten türemiş olması ve newlenebilir olması lazım
    {
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                try
                {
                    return context.Set<TEntity>().Where(filter).ToList();
                }
                catch (Exception)
                {
                    //todo: log
                    return null;
                }
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                try
                {
                    return context.Set<TEntity>().FirstOrDefault(filter);
                }
                catch (Exception ex)
                {
                    //todo: log
                    return null;
                }
            }
        }

        public int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                try
                {
                    return context.Set<TEntity>().Where(filter).Count();
                }
                catch (Exception)
                {
                    //todo: log
                    return 0;
                }
            }
        }

        public TEntity Insert(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                try
                {
                    addedEntity.State = EntityState.Added;
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    //todo: log
                }
                return entity;
            }
        }

        public TEntity Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                try
                {
                    updatedEntity.State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    //todo: log
                    throw;
                }
                return entity;
            }
        }

        public bool Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                try
                {
                    deletedEntity.State = EntityState.Deleted;
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    //todo: log
                    return false;
                }
            }
        }
    }
}
