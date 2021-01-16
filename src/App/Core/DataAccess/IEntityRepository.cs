using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        List<T> GetList(Expression<Func<T, bool>> filter = null);

        T Get(Expression<Func<T, bool>> filter);

        int Count(Expression<Func<T, bool>> filter = null);

        T Insert(T entity);

        T Update(T entity);

        bool Delete(T entity);
    }
}
