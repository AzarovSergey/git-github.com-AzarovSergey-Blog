using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using ORM;
using DAL.Mapper;
using System.Linq.Expressions;

namespace DAL.Concrete
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DbContext context;

        public CommentRepository(DbContext dbContext)
        {
            this.context = dbContext;
        }
        
        #region create update delete
        /// <summary>
        /// Create new comment and add it to database.
        /// </summary>
        /// <param name="comment"></param>
        public void Create(DalComment comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));
            comment.CreationDateTime = DateTime.Now;
            context.Set<Comment>().Add(comment.ToOrmComment());
        }

        /// <summary>
        /// Remove specified comment from database.
        /// </summary>
        /// <param name="comment"></param>
        public void Delete(DalComment comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));
            Comment commentInDB = context.Set<Comment>().Find(comment.Id);
            if (commentInDB != null)
            {
                context.Set<Comment>().Remove(commentInDB);
            }
        }

        /// <summary>
        /// Set specified properties for existing comment.
        /// </summary>
        /// <param name="entity"></param>
        public void Update(DalComment entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            Comment comment = context.Set<Comment>().Find(entity.Id);
            if (comment != null)
                comment.Message = entity.Message;
        }
        #endregion

        #region get methods
        /// <summary>
        /// Get all coments from database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DalComment> GetAll()
        {
            return GetManyByPredicate(x => true);
        }

        /// <summary>
        /// Get comments for article with specified id.
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public IEnumerable<DalComment> GetByArticleId(int articleId)
        {
            return GetManyByPredicate(comment => comment.ArticleId == articleId)
                .OrderBy(x => x.CreationDateTime);
        }

        /// <summary>
        /// Get comment with specified id.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public DalComment GetById(int key)
        {
             return context
                .Set<Comment>()
                .Find(key)?
                .ToDalComment();
        }


        public DalComment GetByPredicate(Expression<Func<DalComment, bool>> expression)
        {
            return GetManyByPredicate(expression).FirstOrDefault(expression);
        }
        

        public IQueryable<DalComment> GetManyByPredicate(Expression<Func<DalComment, bool>> f)
        {
            return context.Set<Comment>().Select(comment => new DalComment()
            {
                Id=comment.Id,
                ArticleId=comment.ArticleId,
                AuthorId=comment.AuthorId,
                CreationDateTime=comment.CreationDateTime,
                Message=comment.Message,
            }).Where(f);
        }
        #endregion


    }
}
