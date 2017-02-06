using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DAL.Interfaces;
using WeatherApp.DAL.Repository;
using WeatherApp.Infrastructure.DAL;

namespace WeatherApp.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private Dictionary<string, object> _repositories;
        private readonly IWeatherContext _context;

        public UnitOfWork(IWeatherContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var entityType = typeof(TEntity);

            var type = entityType.Name;

            if (_repositories.ContainsKey(type))
            {
                return (IRepository<TEntity>)_repositories[type];
            }

            var repositoryType = typeof(GenericRepository<>);

            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(entityType), _context));

            return (IRepository<TEntity>)_repositories[type];
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_context != null)
                    {
                        _context.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}