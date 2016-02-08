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
    public class TagRepository : ITagRepository
    {
        private readonly DbContext dbContext;

        public TagRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        #region create update delete
        /// <summary>
        /// Create new tag entity and add it to database.
        /// </summary>
        /// <param name="tag"></param>
        public void Create(DalTag tag)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));
            dbContext.Set<Tag>().Add(tag.ToOrmTag());
        }

        public void Delete(DalTag e)
        {
            throw new NotImplementedException();
        }

        public void Update(DalTag entity)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region get methods
        /// <summary>
        /// Get all tags from database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DalTag> GetAll()
        {
            return dbContext.Set<Tag>().ToArray().Select(tag => tag.ToDalTag());
        }

        /// <summary>
        /// Get all tags for article with specified id.
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public IEnumerable<DalTag> GetByArticleId(int articleId)
        {
            return dbContext.Set<Article>().Find(articleId)?.Tags.Select(x=>x.ToDalTag());
        }


        public DalTag GetById(int key)
        {
            throw new NotImplementedException();
        }


        public DalTag GetByPredicate(Expression<Func<DalTag, bool>> expression)
        {
            throw new NotImplementedException();
        }
        #endregion

        
    }
}
