using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalUser:IEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int? RoleId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool? Ban { get; set; }
    }
}
