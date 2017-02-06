using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using WeatherApp.DAL.Interfaces;
using WeatherApp.Infrastructure.DAL;

namespace WeatherApp.DAL.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly IWeatherContext _context;
        private readonly IDbSet<TEntity> _db;

        public GenericRepository(IWeatherContext context)
        {
            this._context = context;
            this._db = _context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _db.Add(entity);
        }

        public int Count()
        {
            return _db.Count();
        }

        public void Delete(long id)
        {
            var entity = _db.First(x => x.Id == id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public void Delete(TEntity entity)
        {
            _db.Remove(entity);
        }

        public int FilteredCount(Expression<Func<TEntity, bool>> filter)
        {
            return _db.Count(filter);
        }

        public TEntity Get(Func<TEntity, bool> filter)
        {
            return _db.FirstOrDefault(filter);
        }

        public IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order = null, int? page = default(int?), int? pageSize = default(int?))
        {
            IQueryable<TEntity> query = _db;

            query = query.Where(filter);

            if (order != null)
            {
                query = query.OrderBy(order);
            }

            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return query.ToList();
        }

        public void Update(TEntity entity)
        {
            _db.Attach(entity);
            _context.SetModified(entity);
        }
    }
}