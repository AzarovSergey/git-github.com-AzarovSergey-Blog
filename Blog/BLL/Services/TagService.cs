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
    public class TagService : ITagService
    {
        private readonly ITagRepository tagRepository;
        private readonly IUnitOfWork uow;

        public TagService(IUnitOfWork uow,ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
            this.uow = uow;
        }


        #region get methods

        /// <summary>
        /// The method returns all tags.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TagEntity> GetAll()
        {
            return tagRepository.GetAll().Select(tag => tag.ToBllTag());
        }

        /// <summary>
        /// Returns all tags for specified article.
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public IEnumerable<TagEntity> GetByArticleId(int articleId)
        {
            var tags = tagRepository.GetByArticleId(articleId);
            return tags?.Select(x => x.ToBllTag());
        }
        #endregion

        #region other
        /// <summary>
        /// Add new tag to database.
        /// </summary>
        /// <param name="tag"></param>
        public void Add(TagEntity tag)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));
            tagRepository.Create(tag.ToDalTag());
            uow.Commit();
        }
        #endregion
    }
}
