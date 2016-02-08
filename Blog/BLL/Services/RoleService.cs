using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using DAL.Interface.Repository;
using BLL.Mappers;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;
        public RoleService(IRoleRepository repository)
        {
            roleRepository = repository;
        }

        #region get methods
        /// <summary>
        /// The method returns all roles.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetAll()
        {
            return roleRepository.GetAll().Select(role => role.ToBllRole());
        }

        /// <summary>
        /// Get role with specified id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public RoleEntity GetById(int Id)
        {
            return roleRepository.GetById(Id).ToBllRole();
        }

        /// <summary>
        /// Get role by its name.
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public RoleEntity GetByRoleName(string roleName)
        {
            if (roleName == null)
                throw new ArgumentNullException(nameof(roleName));
            return roleRepository.GetByPredicate(role => role.Name == roleName).ToBllRole();
        }
        #endregion
        
    }
}
