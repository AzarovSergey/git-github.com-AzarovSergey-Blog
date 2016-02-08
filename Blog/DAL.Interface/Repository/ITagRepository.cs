using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface ITagRepository:IRepository<DalTag>, IUnitOfWork
    {
        IEnumerable<DalTag> GetByArticleId(int articleId);
    }
}
