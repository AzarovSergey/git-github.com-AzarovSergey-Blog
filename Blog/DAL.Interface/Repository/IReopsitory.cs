using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using System.Linq;

namespace DAL.Interface.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int key);
        TEntity GetByPredicate(Expression<Func<TEntity, bool>> expression);
        void Create(TEntity e);
        void Delete(TEntity e);
        void Update(TEntity entity);
        IQueryable<TEntity> GetManyByPredicate(Expression<Func<TEntity, bool>> expression);
    }
}
