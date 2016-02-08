using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IUserService
    {
        IEnumerable<UserEntity> GetAllUserEntities();
        void CreateUser(UserEntity user);
        UserEntity GetByLogin(string login);
        UserEntity GetById(int id);
        void SetBan(int id, bool isBan);
    }
}