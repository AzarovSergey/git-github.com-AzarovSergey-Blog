using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using DAL.Interface.Repository;
using BLL.Mappers;
using DAL.Interface.DTO;


namespace BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository repository;
        private readonly IUnitOfWork uow;

        public CommentService(IUnitOfWork uow,ICommentRepository repository)
        {
            this.repository = repository;
            this.uow = uow;
        }
        
        #region create edit remove
        /// <summary>
        /// Add new instance of comment in database.
        /// </summary>
        /// <param name="comment"></param>
        public void Create(CommentEntity comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));
            repository.Create(comment.ToDalComment());
            uow.Commit();
        }

        /// <summary>
        /// Set for existing comment specified value.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        public void Edit(int id, string message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            repository.Update(new DalComment() { Id = id, Message = message });
            uow.Commit();
        }

        /// <summary>
        /// Remove comment with specified id.
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id)
        {
            repository.Delete(new DalComment() { Id = id });
            uow.Commit();
        }
        #endregion

        #region get methods
        /// <summary>
        /// Method returns all comments in database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CommentEntity> GetAll()
        {
            return repository.GetAll().Select(x=>x.ToBllComment());
        }

        /// <summary>
        /// Method returns all comments for article with specified id.
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public IEnumerable<CommentEntity> GetByArticleId(int articleId)
        {
            return repository.GetByArticleId(articleId).Select(x=>x.ToBllComment());
        }

        /// <summary>
        /// Get comment with specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CommentEntity GetById(int id)
        {
            return repository.GetById(id).ToBllComment();
        }
        #endregion

        
    }
}
