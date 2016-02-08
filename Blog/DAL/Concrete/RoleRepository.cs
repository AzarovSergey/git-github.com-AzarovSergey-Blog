using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using ORM;
using DAL.Mapper;
using System.Linq.Expressions;

namespace DAL.Concrete
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DbContext context;

        public RoleRepository(DbContext dbContext)
        {
            this.context = dbContext;
        }

        #region create update delete
        public void Create(DalRole e)
        {
            throw new NotImplementedException();
        }

        public void Delete(DalRole e)
        {
            throw new NotImplementedException();
        }
        public void Update(DalRole entity)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region get methods
        public IEnumerable<DalRole> GetAll()
        {
            return context.Set<Role>().ToArray().Select(role => role.ToDalRole());
        }

        public DalRole GetById(int key)
        {
            return context.Set<Role>().Find(key)?.ToDalRole();
        }

        public DalRole GetByPredicate(Expression<Func<DalRole, bool>> f)
        {
            return context.Set<Role>().Select(role=>new DalRole()
            {
                Id = role.Id,
                Name = role.Name,
            }) .FirstOrDefault(f);
        }
        #endregion

        
    }
}
