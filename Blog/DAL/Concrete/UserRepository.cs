using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using ORM;
using DAL.Mapper;

namespace DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext context;

        public UserRepository(DbContext dbContext)
        {
            this.context = dbContext;
        }


        #region get methods
        /// <summary>
        /// Get all users from database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DalUser> GetAll()
        {
            return context.Set<User>().ToArray().Select(user => user.ToDalUser());
        }

        /// <summary>
        /// Get user with specified id.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public DalUser GetById(int key)
        {
            return context.Set<User>().Find(key)?.ToDalUser();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public DalUser GetByPredicate(System.Linq.Expressions.Expression<Func<DalUser, bool>> expression)
        {
            return context.Set<User>().Select(x => new DalUser()
            {
                Id = x.Id,
                Login = x.Login,
                Password = x.Password,
                RoleId = x.RoleId,
                UserName = x.UserName,
                Ban=x.Ban,
            }).FirstOrDefault(expression);
        }
        #endregion


        #region create update delete
        public void Create(DalUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            context.Set<User>().Add(user.ToOrmUser());
        }

        public void Delete(DalUser e)
        {
            throw new NotImplementedException();
        }

        public void Update(DalUser entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            User user = context.Set<User>().Find(entity.Id);
            if (user == null)
                return;
            if (entity.Ban != null)
                user.Ban = (bool)entity.Ban;
            if (entity.Login != null)
                user.Login = entity.Login;
            if (entity.Password != null)
                user.Password = entity.Password;
            if (entity.UserName != null)
                user.UserName = entity.UserName;
            if (entity.RoleId != null)
                user.RoleId = (int)entity.RoleId;
        }
        #endregion

    }
}
