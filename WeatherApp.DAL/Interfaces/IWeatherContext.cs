using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WeatherApp.Infrastructure.DAL;

namespace WeatherApp.DAL.Interfaces
{
    public interface IWeatherContext : IDisposable
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity;

        void SetModified<TEntity>(TEntity entity) where TEntity : class, IEntity;

        void SetUnchanged<TEntity>(TEntity entity) where TEntity : class, IEntity;

        void SetDeleted<TEntity>(TEntity entity) where TEntity : class, IEntity;

        void SetAdded<TEntity>(TEntity entity) where TEntity : class, IEntity;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class, IEntity;

        void SaveChanges();
    }
}