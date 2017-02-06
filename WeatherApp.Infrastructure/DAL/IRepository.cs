using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WeatherApp.Infrastructure.DAL
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(long id);

        TEntity Get(Func<TEntity, bool> filter);

        IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, object>> order = null, int? page = null, int? pageSize = null);

        int Count();

        int FilteredCount(Expression<Func<TEntity, bool>> filter);
    }
}