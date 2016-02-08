using DAL.Interface.DTO;
using System.Collections.Generic;

namespace DAL.Interface.Repository
{
    public interface ICommentRepository : IRepository<DalComment>
    {
        IEnumerable<DalComment> GetByArticleId(int articleId);
    }
}
