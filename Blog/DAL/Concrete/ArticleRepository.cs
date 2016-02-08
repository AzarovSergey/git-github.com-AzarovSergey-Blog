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
    public class ArticleRepository:IArticleRepository
    {
        private readonly DbContext context;

        public ArticleRepository(DbContext dbContext)
        {
            this.context = dbContext;
        }


        #region get methods
        /// <summary>
        /// Get all articles from database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DalArticle> GetAll()
        {
            return GetManyByPredicate(x => true);
        }

        /// <summary>
        /// Get article with specified id.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public DalArticle GetById(int key)
        {
            return context.Set<Article>()
                .Find(key)?
                .ToDalArticle();
        }

        public DalArticle GetByPredicate(Expression<Func<DalArticle, bool>> expression)
        {
            return GetManyByPredicate(expression).FirstOrDefault(expression);
        }


        public IQueryable<DalArticle> GetManyByPredicate(Expression<Func<DalArticle, bool>> f)
        {
            return context.Set<Article>().Select(article => new DalArticle()
            {
                Id = article.Id,
                AuthorId=article.AuthorId,
                Content=article.Content,
                CreationDateTime=article.CreationDateTime,
                Title=article.Title,
            }).Where(f);
        }

        /// <summary>
        /// Get all articles with specified author.
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns></returns>
        public IEnumerable<DalArticle> GetByAuthorId(int authorId)
        {
            return GetManyByPredicate(artice => artice.AuthorId == authorId);
        }

        /// <summary>
        /// Get all articles with specified tag.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public IEnumerable<DalArticle> GetByTag(string tag)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));
            Tag tagEntity = context.Set<Tag>().FirstOrDefault(x => x.Value == tag);
            if (tagEntity == null)
                return new DalArticle[0];
            return tagEntity.Articles.ToArray().Select(x => x.ToDalArticle());
        }
        #endregion

        #region create
        /// <summary>
        /// Create new article and add it to database.
        /// </summary>
        /// <param name="article"></param>
        public void Create(DalArticle article)
        {
            Create(article, null);
        }

        /// <summary>
        /// Create new article with specified tags and add it to database.
        /// </summary>
        /// <param name="article"></param>
        /// <param name="tags"></param>
        public void Create(DalArticle article, string[] tags)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));
            article.Id = 0;
            article.CreationDateTime = DateTime.Now;
            Article ormArticle = article.ToOrmArticle();
            if (tags != null)
            {
                var tagsDbSet = context.Set<Tag>();
                foreach (string tag in tags)
                {
                    Tag tagEntity = tagsDbSet.FirstOrDefault(x => x.Value == tag);
                    if (tagEntity == null)//if tag not in DB -> add it
                    {
                        tagsDbSet.Add(new Tag()
                        {
                            Articles = new Article[] { ormArticle },
                            Value = tag
                        });
                    }
                    else
                    {
                        //TODO optimize here
                        var list = tagEntity.Articles.ToList();
                        list.Add(ormArticle);
                        tagEntity.Articles = list;
                        //tagEntity.Articles.Add(ormArticle);
                    }
                }//foreach
            }//if
            context.Set<Article>().Add(ormArticle);
        }
        #endregion

        #region update delete

        /// <summary>
        /// Remove the article from the database.
        /// </summary>
        /// <param name="article"></param>
        public void Delete(DalArticle article)
        {
            Article ormArticle = context.Set<Article>().Find(article.Id);
            if (ormArticle != null)
            {
                context.Set<Article>().Remove(ormArticle);
            }
        }

        /// <summary>
        /// Change properties for specified article.
        /// </summary>
        /// <param name="entity"></param>
        public void Update(DalArticle entity)
        {
            Edit(entity, null);
        }


        /// <summary>
        /// Change properties for specified article and set new tags.
        /// </summary>
        /// <param name="article"></param>
        /// <param name="tags"></param>
        public void Edit(DalArticle article, string[] tags)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));
            if (tags == null)
                tags = new string[0];
            Article ormArticle = context.Set<Article>().Find(article.Id);
            if (article.Content != null)
                ormArticle.Content = article.Content;
            if (article.Title != null)
                ormArticle.Title = article.Title;
            DbSet<Tag> tagSet = context.Set<Tag>();

            foreach (Tag tag in ormArticle.Tags)
            {
                //TODO optimize here
                //tag.Articles.Remove(ormArticle);
                var t = tag.Articles.ToList();
                t.Remove(ormArticle);
                tag.Articles = t;
            }
            ormArticle.Tags = new List<Tag>();

            foreach (string tag in tags)
            {
                Tag ormTag = tagSet.FirstOrDefault(x => x.Value == tag);
                if (ormTag == null)
                {
                    ormTag = new Tag() { Value = tag };
                    tagSet.Add(ormTag);
                }
                //TODO optimize here
                var list = ormTag.Articles.ToList();
                list.Add(ormArticle);
                ormTag.Articles = list;
                //tagSet.Add(ormTag);
                ormArticle.Tags.Add(ormTag);
            }
        }
        #endregion

       
        
        #region search
        /// <summary>
        /// Search specified string into articles's title and content.
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>Articles which contain specific string.</returns>
        public IEnumerable<DalArticle> SearchInContent(string searchString)
        {
            return GetManyByPredicate(article => article.Content.Contains(searchString) || article.Title.Contains(searchString));
        }
        #endregion

        
    }
}
