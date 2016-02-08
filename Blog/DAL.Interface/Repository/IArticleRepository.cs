using DAL.Interface.DTO;
using System.Collections.Generic;

namespace DAL.Interface.Repository
{
    public interface IArticleRepository:IRepository<DalArticle>
    {
        IEnumerable<DalArticle> GetByAuthorId(int authorId);
        void Create(DalArticle article, string[] tags);
        IEnumerable<DalArticle> SearchInContent(string searchString);
        IEnumerable<DalArticle> GetByTag(string tag);
        void Edit(DalArticle article, string[] tags);
    }
}
