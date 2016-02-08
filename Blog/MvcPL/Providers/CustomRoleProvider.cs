using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Services;
//using DalToWeb;
//using DalToWeb.Interfacies;
//using DalToWeb.Repositories;

namespace MvcPL.Providers
{
    //провайдер ролей указывает системе на статус пользователя и наделяет 
    //его определенные правами доступа
    public class CustomRoleProvider : RoleProvider
    {
        public IUserService UserService = (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));

        public IRoleService RoleService = (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));

        public override bool IsUserInRole(string email, string roleName)
        {
            var user = UserService.GetAllUserEntities().FirstOrDefault(u => u.Login == email);

            if (user == null) return false;

            var userRole = RoleService.GetById(user.RoleId);

            if (userRole != null && userRole.Name == roleName)
            {
                return true;
            }

            return false;
        }

        public override string[] GetRolesForUser(string email)
        {
            var roles = new string[] { };
            var user = UserService.GetByLogin(email);

            if (user == null) return roles;

            var userRole = user.RoleId;

            if (userRole != 0)
            {
                roles = new string[] { RoleService.GetById(userRole).Name };
            }
            return roles;
        }

        //TODO remove method
        public override void CreateRole(string roleName)
        {
            throw new Exception();
            //var newRole = new Role() {Name = roleName};
            //using (var context = new UserContext())
            //{
            //    context.Roles.Add(newRole);
            //    context.SaveChanges();
            //}
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}