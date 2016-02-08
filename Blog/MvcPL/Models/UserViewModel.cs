using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Login { get; set; }
        public RoleViewModel role { get; set; }
        public int RoleId { get; set; }
        public bool Ban { get; set; }
        public string Password { get; set; }
    }
}