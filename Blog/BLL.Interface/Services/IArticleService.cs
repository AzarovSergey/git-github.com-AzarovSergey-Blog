using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IArticleService
    {
        IEnumerable<ArticleEntity> GetAll();
        IEnumerable<ArticleEntity> GetByAuthorId(int authorId);
        void CreateArticle(ArticleEntity article,string tags);
        ArticleEntity GetById(int id);
        IEnumerable<ArticleEntity> GetByTag(string tag);
        IEnumerable<ArticleEntity> SearchInContent(string searchString);
        void Edit(ArticleEntity article,string tags);
        void Remove(int id);
        bool IsUserAuthor(int articleId, int UserId);
    }
}
