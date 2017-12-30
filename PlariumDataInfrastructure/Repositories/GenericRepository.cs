using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using PlariumDomain.Abstraction.Base;
using Plarium.Abstraction.Repository;
using Plarium.Domain.Entities;

namespace Plarium.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>, IDisposable where TEntity : class, IBaseEntity
    {
        private readonly DbContext _dbContext;

        public GenericRepository(DbContext context)
        {
            this._dbContext = context;
        }

        #region Public methods
        public TEntity GetById(Guid id)
        {
            return this.GetDbSet().FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return this.GetDbSet().AsNoTracking().AsQueryable();
        }

        public TEntity Add(TEntity entity)
        {
            return this.GetDbSet().Add(entity);
        }

        public IEnumerable<TEntity> Add(ICollection<TEntity> entities)
        {
            return this.GetDbSet().AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            DbContext dbContext = this.GetDbContext();
            dbContext.Entry(entity).State = EntityState.Modified;
        }


        public void Delete(TEntity entity)
        {
            DbSet<TEntity> dbSet = this.GetDbSet();

            dbSet.Attach(entity);

            dbSet.Remove(entity);
        }

        public void Delete(ICollection<TEntity> entities)
        {
            DbSet<TEntity> dbSet = this.GetDbSet();

            foreach (TEntity entity in entities)
            {
                dbSet.Attach(entity);
            }

            dbSet.RemoveRange(entities);
        }

        public virtual void SaveChanges()
        {
            this._dbContext.SaveChanges();
        }

        protected DbContext GetDbContext()
        {
            if (typeof(BaseEntity).IsAssignableFrom(typeof(TEntity)))
            {
                return this._dbContext;
            }

            throw new InvalidOperationException("The database context not found for entity " + typeof(TEntity).FullName);
        }

        protected DbSet<TEntity> GetDbSet()
        {
            return this.GetDbContext().Set<TEntity>();
        }

        public void Dispose()
        {
            if (this._dbContext != null)
            {
                this._dbContext.Dispose();
            }
        }

        #endregion

    }
}
