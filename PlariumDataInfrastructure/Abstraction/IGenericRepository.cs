using System;
using System.Collections.Generic;
using System.Linq;

namespace Plarium.Abstraction.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity GetById(Guid id);
        IQueryable<TEntity> GetAll();
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> Add(ICollection<TEntity> entities);
        void Update(TEntity entity);      
        void Delete(TEntity entity) ;
        void Delete(ICollection<TEntity> entities) ;
        void SaveChanges();
      }
}
