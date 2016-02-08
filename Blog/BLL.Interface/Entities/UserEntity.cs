using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Collections;
using System.Collections.Generic;

namespace BLL.Interface.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Ban { get; set; }
    }
}
