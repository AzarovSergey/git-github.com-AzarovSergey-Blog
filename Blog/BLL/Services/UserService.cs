using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using DAL.Interface.Repository;
using DAL.Interface.DTO;
using BLL.Mappers;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;

        public UserService(IUnitOfWork uow, IUserRepository repository)
        {
            this.uow = uow;
            this.userRepository = repository;
        }
                

        #region get methods
        /// <summary>
        /// Get all existing users.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetAllUserEntities()
        {
            return userRepository.GetAll().Select(user => user.ToBllUser());
        }


        /// <summary>
        /// Get user with specified login.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public UserEntity GetByLogin(string login)
        {
            if (login == null)
                return null;
            var user = userRepository.GetByPredicate(x=>x.Login==login);
            if (user==null)
                return null;
            return user.ToBllUser();
        }


        /// <summary>
        /// Get user with specified login.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserEntity GetById(int id)
        {
            var user = userRepository.GetById(id);
            return user?.ToBllUser();
        }
        #endregion

        #region other
        /// <summary>
        /// Create new user and save it to database.
        /// </summary>
        /// <param name="user"></param>
        public void CreateUser(UserEntity user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            userRepository.Create(user.ToDalUser());
            uow.Commit();
        }

        /// <summary>
        /// Ban or unban user with specified id.
        /// This method change ban value only.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isBan"></param>
        public void SetBan(int id, bool isBan)
        {
            userRepository.Update(new DalUser() { Ban = isBan, Id = id });
            uow.Commit();
        }
        #endregion

    }
}
