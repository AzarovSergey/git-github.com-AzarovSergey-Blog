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

        #region uow
        public void Commit()
        {
            context.SaveChanges();
        }
        #endregion

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
            return context.Set<Comment>().ToArray().Select(comment => comment.ToDalComment());
        }

        /// <summary>
        /// Get comments for article with specified id.
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public IEnumerable<DalComment> GetByArticleId(int articleId)
        {
            return context
                .Set<Comment>()
                .Where(x => x.ArticleId == articleId)
                .OrderBy(x=>x.CreationDateTime)
                .ToArray()
                .Select(x=>x.ToDalComment());
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
        /// <summary>
        /// The method returns the first article according to predicate.
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public DalComment GetByPredicate(Expression<Func<DalComment, bool>> expression)
        {
            throw new NotImplementedException();
        }
        #endregion

        
    }
}
