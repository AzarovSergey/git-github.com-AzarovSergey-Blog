using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using DAL.Interface.Repository;
using BLL.Mappers;

namespace BLL.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository repository;
        private readonly IUnitOfWork uow;

        public ArticleService(IUnitOfWork uow,IArticleRepository repository)
        {
            this.repository = repository;
            this.uow = uow;
        }

        #region get
        /// <summary>
        /// Get all articles.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ArticleEntity> GetAll()
        {
            return repository.GetAll().Select(article => article.ToBllArticle());
        }

        /// <summary>
        /// The method returns all articles created by user with specified id.
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns></returns>
        public IEnumerable<ArticleEntity> GetByAuthorId(int authorId)
        {
            var article = repository.GetByAuthorId(authorId);
            return article?.Select(x => x.ToBllArticle());
        }

        /// <summary>
        /// The method returns an article with specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ArticleEntity GetById(int id)
        {
            var article = repository.GetById(id);
            return article?.ToBllArticle();
        }


        /// <summary>
        /// The method returns all articles with specified tag value.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public IEnumerable<ArticleEntity> GetByTag(string tag)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));
            return repository.GetByTag(tag).Select(x=>x.ToBllArticle());
        }
        #endregion

        #region search
        /// <summary>
        /// Returns all articles which contain specified string in title or content.
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public IEnumerable<ArticleEntity> SearchInContent(string searchString)
        {
            if (searchString == null)
                throw new ArgumentNullException(nameof(searchString));
            return repository.SearchInContent(searchString).Select(x => x.ToBllArticle());
        }
        #endregion

        #region create update delete
        /// <summary>
        /// The method creates new article and adds it in database.
        /// </summary>
        /// <param name="article"></param>
        /// <param name="tags">Tags for article.</param>
        public void CreateArticle(ArticleEntity article, string tags)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));
            if (tags == null)
                tags = string.Empty;
            repository.Create(article.ToDalArticle(), SplitTags(tags));
            uow.Commit();
        }

        /// <summary>
        /// Change values for article in database.  
        /// </summary>
        /// <param name="article"></param>
        /// <param name="tags"></param>
        public void Edit(ArticleEntity article, string tags)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));
            if (tags == null)
                throw new ArgumentNullException(nameof(tags));
            repository.Edit(article.ToDalArticle(), SplitTags(tags));
            uow.Commit();
        }

        /// <summary>
        /// Method removes article with specified id. 
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id)
        {
            repository.Delete(new DAL.Interface.DTO.DalArticle() { Id = id });
            uow.Commit();
        }
        #endregion


        #region other
        /// <summary>
        /// Check that user with specified id is author of specified article.
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool IsUserAuthor(int articleId, int UserId)
        {
            var article = repository.GetById(articleId);
            return (article!=null && article.AuthorId == UserId);
        }
        #endregion

        #region privare methods
        /// <summary>
        /// Method for getting an array of tags from single string. 
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        private string[] SplitTags(string tags)
        {
            return tags.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }
        #endregion
    }
}
