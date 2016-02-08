using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface ICommentService 
    {
        IEnumerable<CommentEntity> GetAll();
        IEnumerable<CommentEntity> GetByArticleId(int articleId);
        void Create(CommentEntity comment);
        CommentEntity GetById(int id);
        void Edit(int id,string message);
        void Remove(int id);

    }
}
