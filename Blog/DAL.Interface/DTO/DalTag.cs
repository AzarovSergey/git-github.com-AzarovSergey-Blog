using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalTag : IEntity
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
